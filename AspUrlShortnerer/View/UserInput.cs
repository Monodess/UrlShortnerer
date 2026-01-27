namespace AspUrlShortnerer.View
{
   static public class UserInput
    {
       static public Uri? InputUrl()
        {
            string stringUrl = Console.ReadLine();

            Uri.TryCreate(stringUrl, UriKind.Absolute, out Uri uri);
            return uri;

        }
    }
}
