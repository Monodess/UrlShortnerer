namespace AspUrlShortnerer.Domain.entities
{
    public class Url
    {
            public Uri? _url { get; set; } 
          
        public Url () { }
        public Url (Uri? url)
        {
            _url = url; 
        }
    }
}
