using System;
using System.Net;
using System.Text;
//using Microsoft.AspNetCore;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.DependencyInjection;

namespace HttpListenerServer
{
    public class Program
    {
        public const string Prefix = "http://+:9002/test/";
        public const string ResponseString = "Hello world";
        public static readonly byte[] Buffer = Encoding.UTF8.GetBytes(ResponseString);

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"PID: {System.Diagnostics.Process.GetCurrentProcess().Id}");
				UseHttpListener();

                /*if (args.Length == 0 || !"httpsyslistener".Equals(args[0], StringComparison.OrdinalIgnoreCase))
                    UseHttpListener();
                else
                    UseHttpSysListener();*/
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static void UseHttpListener()
        {
            var listener = new HttpListener { AuthenticationSchemes = AuthenticationSchemes.Negotiate };
            listener.Prefixes.Add(Prefix);
            listener.Start();
            Console.WriteLine($"Listening on {Prefix} with HttpListener...");

            while (true)
            {
                var context = listener.GetContext();
                var request = context.Request;
                Console.WriteLine($"Hit from {request.RemoteEndPoint.Address}...");
                var response = context.Response;
                response.ContentLength64 = Buffer.Length;
                var output = response.OutputStream;
                output.Write(Buffer, 0, Buffer.Length);
                output.Close();
            }
        }
/*
        public static void UseHttpSysListener()
        {
            Console.WriteLine($"Listening on {Prefix} with HttpSysListener...");
            WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseHttpSys(options =>
                {
                    options.Authentication.Schemes = Microsoft.AspNetCore.Server.HttpSys.AuthenticationSchemes.Negotiate;
                    options.Authentication.AllowAnonymous = false;
                    options.UrlPrefixes.Add(Prefix);
                })
                .Build().Run();
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync(Program.ResponseString);
            });
        }*/
    }
}
