using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FoodCourt.Models;
using Newtonsoft.Json;

namespace FoodCourt.Services
{
    public class HttpService
    {
        private HttpClient DefaultHttp()
        {
            return new HttpClient();
        }
        public async Task<ApiResponse<T>> SendAsync<T>(string url, HttpMethod method, HttpContent content = null)
        {
            var request = new HttpRequestMessage(method, url);

            if (content != null)
            {
                request.Content = content;
            }

            using (var client = DefaultHttp())
            {
                var response = await client.SendAsync(request);

                var body = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResponse<T>>(body);
            }
        }

        internal Task<ApiResponse<T>> PostAsync<T>(string url, object body)
        {
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            return SendAsync<T>(url, HttpMethod.Post, content);
        }
    }
}
