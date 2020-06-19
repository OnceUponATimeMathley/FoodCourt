using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FoodCourt.Services
{
    public class HttpService
    {
        private HttpClient DefaultHttp()
        {
            return new HttpClient();
        }
        public async Task SendAsync(string url, HttpMethod method, HttpContent content = null)
        {
            var request = new HttpRequestMessage(method, url);

            if (content != null)
            {
                request.Content = content;
            }

            using (var client = DefaultHttp())
            {
                var response = await client.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var body = response.Content.ReadAsStringAsync();
                    //Read body as Json

                }
            }
        }
    }
}
