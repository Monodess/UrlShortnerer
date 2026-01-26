using AspUrlShortnerer.Domain.entities;
using MySql.Data.MySqlClient; 
using System.Data;
namespace AspUrlShortnerer.Services
{
   public  class ConnectionData
    {
        public string baseConnect = "Server=localhost;Port=3306;Database=practice_platform;Uid=root;Pwd=savepass;SslMode=Disabled;";
        public string dataAccess = "Select * from UrlShortener";
        public string dataAccessTest = "Select * from user";
        
    }

    public class DAL
    {
        public DAL() {
            connectionData = new ConnectionData(); 
        }
        public DAL(string connectionString)
        {
            Console.WriteLine(connectionData.baseConnect == null); 
            connectionData.baseConnect = connectionString ?? throw new ArgumentNullException(nameof(connectionString)); 
        }

      public ConnectionData connectionData {  get; set; } = new ConnectionData();
        public List<ShortenUrl> ShortenUrls { get; set; } = new List<ShortenUrl>(); 
        private MySqlConnection _connection; 
        
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
        public bool Access()
        {
            //using closes itself
            using var connection = new MySqlConnection(connectionData.baseConnect);
            connection.Open();


            MySqlCommand command = new MySqlCommand(connectionData.dataAccessTest, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            List<ShortenUrl> urls = new List<ShortenUrl>();
            while (reader.Read())
            {
                ShortenUrl url = new ShortenUrl(reader.GetGuid("id"), reader.GetString("shortUrl"), reader.GetString("code"), reader.GetString("longUrl"), reader.GetDateTime("createdOnUtc"));
                urls.Add(url);
            }
            ShortenUrls = urls;
            return true;
            
        }

           
            
            
    }
}
