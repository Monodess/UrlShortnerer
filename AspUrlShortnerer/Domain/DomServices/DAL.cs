using AspUrlShortnerer.Domain.entities;
using MySql.Data.MySqlClient; 
using System.Data;
namespace AspUrlShortnerer.Services
{
  
    public partial class DAL
    {
        static public partial class ConnectionData
        {
            public static string domain = "https://localhost:5020";
            public static HashSet<string> columnsNames = new HashSet<string> { "Id", "ShortUrl", "LongUrl", "Code", "CreatedOnUtc" };
            public static string baseConnect = "Server=localhost;Port=3306;Database=practice_platform;Uid=root;Pwd=savepass;SslMode=Disabled;AllowPublicKeyRetrieval=True;";
            public static string dataAccessGetById = "Select * from shortenurls where Id = @x LIMIT 1;";
            public static string dataAccessGetByCode = "Select 1 from shortenurls where Code = @x LIMIT 1;";
            public static string dataAccessSelectEverything = "Select * from shortenurls;";
        }
        public List<ShortenUrl> ShortenUrls { get; set; } = new List<ShortenUrl>();
        private MySqlConnection _connection;

        public DAL() {
           
        }

        

      
        
        //checks state
        public bool Connect()
        {
            try
            {
          
                _connection = new MySqlConnection(ConnectionData.baseConnect);
                _connection.Open();
                return true; 
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Connection failed: {}", e.Message);
                return false;  
            }
        }

        //selects everything from the only table "shortenurls"
        public List<ShortenUrl> GetAll()
        {
            var result = new List<ShortenUrl>();

            using var connection = new MySqlConnection(ConnectionData.baseConnect);
            connection.Open();

            using var command = new MySqlCommand(ConnectionData.dataAccessSelectEverything, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
                result.Add(new ShortenUrl(
                    reader.GetInt32("Id"),
                    reader.GetString("ShortUrl"),
                    reader.GetString("Code"),
                    reader.GetString("LongUrl"),
                    reader.GetDateTime("CreatedOnUtc")
                ));

            return result;
        }


        public static ShortenUrl GetByCode(string code) => GetField(ConnectionData.dataAccessGetByCode, code);
        public static ShortenUrl GetById(int id) => GetField(ConnectionData.dataAccessGetById, id);
       
        public static ShortenUrl GetField(string sql, object value)
        {
            //using closes itself
            using var connection = new MySqlConnection(ConnectionData.baseConnect);
            connection.Open();

            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@x", value); 
            using MySqlDataReader reader = command.ExecuteReader();
            ShortenUrl url = new ShortenUrl(); 
            
            if(reader.Read())
            {
                return url = new ShortenUrl(reader.GetInt32("Id"), reader.GetString("ShortUrl"), reader.GetString("Code"), reader.GetString("LongUrl"), reader.GetDateTime("CreatedOnUtc"));
         
            }
            return null; 
            
            
        }
        //TODO: create transaction, resshape db into varchars rather than long text ; 


        static public bool DoesCodeExist(string code)
        {
            
            //using closes itself
            using var connection = new MySqlConnection(ConnectionData.baseConnect);
            connection.Open();

            MySqlCommand command = new MySqlCommand(ConnectionData.dataAccessGetByCode, connection);
            command.Parameters.Add("@x", MySqlDbType.VarChar).Value = code.Trim();
            using MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return true; 
            }
            return false;
        }
        public void DisplayVal(Object value)
        {
            if (value is System.Collections.IEnumerable e && value is not string)
            {
                foreach (var item in e)
                {
                    Console.WriteLine(item);
                }
            }
            else Console.WriteLine(value);
        }


    }
}
