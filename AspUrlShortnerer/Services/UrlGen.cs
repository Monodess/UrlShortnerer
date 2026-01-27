using AspUrlShortnerer.Domain.entities; 
using System.Net;
using System.Security.Cryptography;
using System.Text;
namespace AspUrlShortnerer.Services
{
     public class UrlGen
    {
        public UrlGen(string origUrl)
        {
            _origUrl = origUrl; 
        }
        private string _origUrl;
        
        private int _codeLength = 6; 
       
      
       public string GenerateUniqueCode()
        {
                const string alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                var rnm = RandomNumberGenerator.Create();
                var bytes = new byte[_codeLength];
                var sb = new StringBuilder();
            while (true)
            {
                rnm.GetBytes(bytes);

                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(alphabet[bytes[i]] % alphabet.Length);
                }
                if (!DAL.DoesCodeExist(sb.ToString()))
                 return sb.ToString();
            }
        }
        public string GenerateShortUrl(string code)
        {
            return$"{DAL.ConnectionData.domain}/{code}"; 
        }


    }
}
