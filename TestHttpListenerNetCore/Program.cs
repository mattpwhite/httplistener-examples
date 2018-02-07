using System;
using System.Net.Http;

namespace TestHttpListenerNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Provide URL as argument");
                Environment.Exit(1);
            }

            var url = args[0];
            Console.WriteLine($"Testing {url}");

            using (var httpClient = new HttpClient(new HttpClientHandler { UseDefaultCredentials = true }))
            {
                try
                {
                    Console.WriteLine($"\nTesting HttpClient");
                    using (var result = httpClient.GetAsync(url).GetAwaiter().GetResult())
                    {
                        Console.WriteLine($"Status: {result.StatusCode}");
                        Console.WriteLine($"WWW-Authenticate: {result.Headers.WwwAuthenticate}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error - {ex}");
                }
            }
        }
    }
}
