
using System;
using System.Diagnostics;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Laboratory2OOP
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<string> urls = new List<string> {
                "https://api.hh.ru/openapi/redoc#section/Obshaya-informaciya/Vybor-s",
                "https://gzmland.ru/"

            };
            var client = new HttpClient();
            var tasks = new List<Task<string>>();

            foreach (string url in urls)
            {
                tasks.Add(GetResponse(client,url));

            }
            var responces = await Task.WhenAll(tasks);

            foreach (var responce in responces) 
            {
                var jsonContent = JsonSerializer.Serialize(responce);
                Console.WriteLine(responce);
                Console.WriteLine("-------------------------------------");
            }
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения программы (в миллисекундах) : {stopwatch.ElapsedMilliseconds}");
        }

        static async Task<string> GetResponse(HttpClient Client, string url)
        {
            try
            {
                var response = await Client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return $"Fail. Status code: {response.StatusCode}";
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Ошибка получения данных {url}: {ex.Message}");
            }
        }
    }
}

