using AspUrlShortnerer.Domain.entities;
using MySql.Data.MySqlClient; 
using System.Data;
namespace AspUrlShortnerer.Services
{
    //1) create database
    //2) make vars in request
    //3) seletct id and tund it into base62; 
    //4)
   

    public partial class DAL
    {
        static public class ConnectionData
        {
            public static string domain = "https://localhost:5020";
            public static HashSet<string> columnsNames = new HashSet<string> { "Id", "ShortUrl", "LongUrl", "Code", "CreatedOnUtc" };
            public static string baseConnect = "Server=localhost;Port=3306;Database=practice_platform;Uid=root;Pwd=savepass;SslMode=Disabled;";
            public static string dataAccessGetField = "Select * from shortenurls where Id = @x;";
            public static string dataAccessGetCode = "Select 1 from shortenurls where Code = @x LIMIT 1;";
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
       
        

        public ShortenUrl GetField(int id)
        {
            //using closes itself
            using var connection = new MySqlConnection(ConnectionData.baseConnect);
            connection.Open();

            MySqlCommand command = new MySqlCommand(ConnectionData.dataAccessGetField, connection);
            command.Parameters.AddWithValue("x", id); 
            using MySqlDataReader reader = command.ExecuteReader();
            ShortenUrl url = new ShortenUrl(); 
            if(reader.Read())
            {
                return url = new ShortenUrl(reader.GetInt32("Id"), reader.GetString("ShortUrl"), reader.GetString("Code"), reader.GetString("LongUrl"), reader.GetDateTime("CreatedOnUtc"));
         
            }
            return null; 
            
            
        }

        static public bool DoesCodeExist(string code)
        {
            
            //using closes itself
            using var connection = new MySqlConnection(ConnectionData.baseConnect);
            connection.Open();

            MySqlCommand command = new MySqlCommand(ConnectionData.dataAccessGetField, connection);
            command.Parameters.AddWithValue("x", code);
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
