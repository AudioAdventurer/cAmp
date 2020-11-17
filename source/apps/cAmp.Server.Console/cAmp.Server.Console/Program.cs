using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using cAmp.Libraries.Common.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using cAmp.Libraries.Common.Objects;

namespace cAmp.Server.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            var config = Config.GetInstance();

            var host = new WebHostBuilder()
                .UseKestrel()
                .ConfigureServices(services =>
                {
                    services.AddCors();

                    services.AddMvc()
                        .AddNewtonsoftJson()
                        .AddApplicationPart(typeof(StatusController).Assembly)
                        .AddControllersAsServices();

                    services.AddAutofac(c =>
                    {

                    });

                    services.AddAuthentication(x =>
                    {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(config.JwtSecret)),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                return Task.CompletedTask;
                            }
                        };
                    });
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .ConfigureKestrel((context, options) =>
                {
                    // Set properties and call methods on options
                    options.Listen(IPAddress.Any, config.PortNumber, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                        //listenOptions.UseHttps("testCert.pfx", "testPassword");
                    });
                })
                .Build();

            var library = host.Services.GetRequiredService<Library>();

            host.Run();
        }
    }
}
