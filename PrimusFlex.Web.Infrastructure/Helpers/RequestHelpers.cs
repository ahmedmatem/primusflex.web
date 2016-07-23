namespace PrimusFlex.Web.Infrastructure.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    public static class RequestHelpers
    {
        public static async Task<string> GetTokenAsync(string userName, string password, string uri)
        {
            string token;

            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(uri));
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            string postString = string.Format("grant_type={0}&username={1}&password={2}", "password", userName, password);
            request.ContentLength = postString.Length;

            CookieContainer cookies = new CookieContainer();
            request.CookieContainer = cookies;

            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
            requestWriter.Write(postString);
            requestWriter.Close();

            try
            {
                HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // extract access_token from content
                token = ExtractAccessToken(responseFromServer);
                // Clean up the streams and the response.
                reader.Close();
                response.Close();
            }
            catch (WebException ex)
            {
                var errorMessage = (new StreamReader(ex.Response.GetResponseStream())).ReadToEnd();
                return null;
            }

            return token;
        }

        private static string ExtractAccessToken(string responseFromServer)
        {
            var splitedResponse = responseFromServer.Split(new char[] { ',' });
            var token = splitedResponse[0];
            var value = token.Split(new char[] { ':', '\"' }, StringSplitOptions.RemoveEmptyEntries);
            return value[value.Length - 1];
        }
    }
}
