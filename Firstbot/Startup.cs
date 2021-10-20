using Firstbot.Adapter;
using Firstbot.Bots;
using Firstbot.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Runtime.Extensions;
using Microsoft.Bot.Builder.Integration;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Firstbot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Use In case you add a new bot using AddBot in services.AddBot and adding middlewares in it

            //services.AddBot<SimpleBot>(options =>
            //{
            //    options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);

            //    options.Middleware.Add(new SimpleMiddleware1());
            //    options.Middleware.Add(new SimpleMiddleware2());
            //    options.Middleware.Add(new MiddlewareLogger());
            //    options.Middleware.Add(new Middleware_LowerCase());

            //});

            services.AddSingleton<IMiddleware, Middleware_LowerCase>();
            services.AddSingleton<IMiddleware, MiddlewareLogger>();
            services.AddSingleton<IMiddleware, SimpleMiddleware2>();

            services.AddControllers().AddNewtonsoftJson();
            services.AddBotRuntime(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();

            // Set up custom content types - associating file extension to MIME type.
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".lu"] = "application/vnd.microsoft.lu";
            provider.Mappings[".qna"] = "application/vnd.microsoft.qna";

            // Expose static files in manifests folder for skill scenarios.
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });
            app.UseWebSockets();
            app.UseRouting();
            app.UseAuthorization();

            // Use In case you add a new bot using AddBot in services.AddBot and adding middlewares in it
            //app.UseBotFramework();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
