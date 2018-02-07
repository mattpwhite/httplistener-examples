using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;

namespace TestHttpListenerNetFx
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

            foreach (AuthenticationLevel level in Enum.GetValues(typeof(AuthenticationLevel)))
            {
                try
                {
                    Console.WriteLine($"\nTesting WebRequest with authentication level {level}");
                    var req = WebRequest.Create(url);
                    req.UseDefaultCredentials = true;
                    req.AuthenticationLevel = level;

                    using (var resp = req.GetResponse())
                    {
                        var httpResp = (HttpWebResponse)resp;
                        Console.WriteLine($"Status: {httpResp.StatusCode}");
                        Console.WriteLine($"IsMutuallyAuthenticated: {httpResp.IsMutuallyAuthenticated}");
                        Console.WriteLine($"WWW-Authenticate: {httpResp.Headers[HttpResponseHeader.WwwAuthenticate]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error - {ex}");
                }
            }

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
