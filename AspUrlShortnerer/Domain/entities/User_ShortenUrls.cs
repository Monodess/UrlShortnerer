namespace AspUrlShortnerer.Domain.entities
{
    public class User_ShortenUrls
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime DateTime { get; set; }
        public User_ShortenUrls(string Name, string Password)
        {
            this.Name = Name;
            this.Password = Password;
        }
    }
}
