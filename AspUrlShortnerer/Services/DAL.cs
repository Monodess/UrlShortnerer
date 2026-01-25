using AspUrlShortnerer.Domain.entities;
using MySql.Data.MySqlClient; 
using System.Data;
namespace AspUrlShortnerer.Services
{
   public  class ConnectionData
    {
        public string baseConnect = "Server=localhost;Port=3306;Database=practice_platform;Uid=root;Pwd=savepass;SslMode=None;";
        public string dataAccess = "Select * from UrlShortener";
        
    }

    public class DAL
    {
        public DAL() {
           
        }
      public ConnectionData connectionData {  get; set; }
        public List<ShortenUrl> ShortenUrls { get; set; }
        
        //checks state
        public ConnectionState Connect()
        {
            using var connection = new MySqlConnection(connectionData.baseConnect);
            connection.Open();
            return connection.State;
        }

        public List<ShortenUrl> Access()
        {
            using var connection = new MySqlConnection(connectionData.baseConnect);
            connection.Open();
           
            
            MySqlCommand command = new MySqlCommand(connectionData.dataAccess, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            List<ShortenUrl> urls = new List<ShortenUrl>();  
            while(reader.Read())
            {
                ShortenUrl url = new ShortenUrl(reader.GetGuid("id"), reader.GetString("shortUrl"), reader.GetString("code"), reader.GetString("longUrl"), reader.GetDateTime("createdOnUtc")); 
                urls.Add(url); 
            }

            command.ExecuteNonQuery();

           
            
            
    }
}
