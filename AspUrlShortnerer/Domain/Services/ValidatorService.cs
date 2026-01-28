namespace AspUrlShortnerer.Domain.Services
{
    static public class ValidatorService
    {
        static public async Task<bool> IsUrlValid(string url)
        {

            // Проверяем формат строки, чтобы это был хотя бы похожий на URL текст
            if (Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                || (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
            {

                return false;
            }
            

            using var client = new HttpClient();
            try
            {
                // Создаем запрос HEAD (только заголовки, без тела страницы)
                var request = new HttpRequestMessage(HttpMethod.Head, url);
                var response = await client.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                // Если сайт лежит, или возникла ошибка сети — считаем URL невалидным
                return false;
            }
        }

        public static Uri? StrToUri(string url)
        {
            // Проверяем формат строки, чтобы это был хотя бы похожий на URL текст
            if (Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                || (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
            {

                return uriResult;
            }
            return null;
        }
    }
}
