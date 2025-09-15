using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace NetProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.Title = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            var _url = "https://www.naver.com";

            using (var _httpClient = new HttpClient())
            {
                var _response = await _httpClient.GetAsync(_url);
               
                _response.EnsureSuccessStatusCode();

                var _message = _response.Content.ReadAsStringAsync();

                Console.WriteLine("Http response : {0}", _response);
            }

            Thread.Sleep(10000);
        }
    }
}
