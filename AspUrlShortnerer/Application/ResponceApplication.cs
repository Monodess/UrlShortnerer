using AspUrlShortnerer.Domain.entities;
using AspUrlShortnerer.Services;
using System.Security.Policy;

namespace AspUrlShortnerer.Application
{
    public class ResponceApplication
    {
        public ResponceApplication()
        {
        }

        public string SendCode(ShortenUrl url)
        {
            if (DAL.DoesCodeExist(url.Code))
            {
                return url.ShortUrl;
            }
            else return null; 
        }

        public string Redir(string code)
        {
            //5jkZEc
            if (DAL.DoesCodeExist(code))
            {
                string target = DBcontextApplication.GetOrigLink(code);

                if (string.IsNullOrWhiteSpace(target)) return null;

                target = target.Trim();

                // If it's missing 'http', the browser thinks 'google.com' is a local file
                // or that 'm3maaw' is a global domain name.
                if (!target.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    target = "https://" + target;
                }

                Console.WriteLine($"Corrected Redirect: {target}");
                return target;
            }
            return null;
        }
    }
}
