using AspUrlShortnerer.Domain.entities;
using MySql.Data.MySqlClient; 
namespace AspUrlShortnerer.Services
{

    public partial class DAL
    {
        static public partial class ConnectionData {
            public static string insertField = "INSERT INTO shortenurls (ShortUrl, LongUrl, Code, CreatedOnUtc) VALUES (@short, @orig, @code, @date);";
            public static string deleteField = "DELETE FROM shortenurls (ShortUrl, LongUrl, Code, CreatedOnUtc) WHERE Id = @x";
        }
        // TODO: complete DB writing logic (inserting and deleting field) 
        public static bool InsertField(ShortenUrl url)
        {
            // 1. Establish the connection
            using var connection = new MySqlConnection(ConnectionData.baseConnect);

            // 2. Prepare the command using your SQL string and the open connection
            // Note: We use the 'url' object properties to fill the parameters
            using var command = new MySqlCommand(ConnectionData.insertField, connection);

            // 3. Map the data from the 'url' object fields
            command.Parameters.AddWithValue("@short", url.ShortUrl);
            command.Parameters.AddWithValue("@orig", url.LongUrl);
            command.Parameters.AddWithValue("@code", url.Code);
            command.Parameters.AddWithValue("@date", url.CreatedOnUtc);

            try
            {
                connection.Open(); // Open as late as possible
                int rowsAffected = command.ExecuteNonQuery();

                // If rowsAffected > 0, the insert was successful
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Data saved successfully!");
                    return true;
                }
                return false;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database Error: {ex.Message}");
                return false; // Return false so the caller knows it failed
            }
        }
        public static bool DeleteField(ShortenUrl url)
        {
            // 1. Establish the connection
            using var connection = new MySqlConnection(ConnectionData.baseConnect);

            // 2. Prepare the command using your SQL string and the open connection
            // Note: We use the 'url' object properties to fill the parameters
            using var command = new MySqlCommand(ConnectionData.deleteField, connection);

            // 3. Map the data from the 'url' object fields
            command.Parameters.AddWithValue("@x", url.Id); 

            try
            {
                connection.Open(); // Open as late as possible
                int rowsAffected = command.ExecuteNonQuery();

                // If rowsAffected > 0, the insert was successful
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Data saved successfully!");
                    return true;
                }
                return false;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Database Error: {ex.Message}");
                return false; // Return false so the caller knows it failed
            }
        }

    }
}
