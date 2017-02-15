using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string clientId = "600937105632-ipjq643g534m6dm0ficbk59co8sgoo71.apps.googleusercontent.com";
            string code = Console.ReadLine();

            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://api.flirsty.com/api/Auth/Google");

            var body = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("ClientId", clientId),
                new KeyValuePair<string, string>("Code", code)
            };

            var form = new FormUrlEncodedContent(body);
            var tokenResponse = client.PostAsync(uri, form);
            tokenResponse.Wait();

            if (tokenResponse.Result.IsSuccessStatusCode == false)
            {
                Console.WriteLine("Error");
            }

            var result = tokenResponse.Result.Content.ReadAsStringAsync();

            result.Wait();

            string token = result.Result;


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
