using AspUrlShortnerer.Domain.entities;
using MySql.Data.MySqlClient; 
using System.Data;
namespace AspUrlShortnerer.Services
{
    //1) create database
    //2) make vars in request
    //3) seletct id and tund it into base62; 
    //4)
   public  class ConnectionData
    {
       public HashSet<string> columnsNames = new HashSet<string>{"Id", "ShortUrl", "LongUrl", "Code", "CreatedOnUtc"}; 
       public string baseConnect = "Server=localhost;Port=3306;Database=practice_platform;Uid=root;Pwd=savepass;SslMode=Disabled;";
       public string dataAccess = "Select * from UrlShortener;";
       public string dataAccessSelectEverything = "Select * from UrlShortener;";
        public string dataAccessSelectField = "Select * from UrlShortener where id = 1;";
        
      
    }

    public class DAL
    {
        public ConnectionData connectionData { get; set; } = new ConnectionData();
        public List<ShortenUrl> ShortenUrls { get; set; } = new List<ShortenUrl>();
        private MySqlConnection _connection;

        public DAL() {
            connectionData = new ConnectionData(); 
        }

        public DAL(string connectionString)
        {
            Console.WriteLine(connectionData.baseConnect == null); 
            connectionData.baseConnect = connectionString ?? throw new ArgumentNullException(nameof(connectionString)); 
        }

      
        
        //checks state
        public bool Connect()
        {
            try
            {
                Console.WriteLine(connectionData.baseConnect == null);
                _connection = new MySqlConnection(connectionData.baseConnect);
                _connection.Open();
                return true; 
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Connection failed: {}", e.Message);
                return false;  
            }
        }

        //selects everything from the only table "UrlShortener"
        public List<ShortenUrl> GetAll()
        {
            var result = new List<ShortenUrl>();

            using var connection = new MySqlConnection(connectionData.baseConnect);
            connection.Open();

            using var command = new MySqlCommand(connectionData.dataAccessSelectEverything, connection);
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

        public bool AccessField(int id)
        {
            //using closes itself
            using var connection = new MySqlConnection(connectionData.baseConnect);
            connection.Open();

            MySqlCommand command = new MySqlCommand(connectionData.dataAccessSelectEverything, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ShortenUrl url = new ShortenUrl(reader.GetInt32("Id"), reader.GetString("ShortUrl"), reader.GetString("Code"), reader.GetString("LongUrl"), reader.GetDateTime("CreatedOnUtc"));
                ShortenUrls.Add(url);
            }
           
            return true;
            
        }

           
            
            
    }
}
