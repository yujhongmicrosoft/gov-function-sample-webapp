using gov_moderator.Models;
using gov_moderator.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;

namespace gov_moderator
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
            services.AddSingleton<IConfiguration>(this.Configuration);

            services.AddMvc();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.ConfigurePOCO<GMConfig>(this.Configuration.GetSection("GovModeratorConfig"));


            // Set up DI
            services.AddTransient<IStorageClient, StorageClient>();
            services.AddTransient<ServiceFactory>();
            services.AddTransient<CloudStorageAccount>(p => p.GetService<ServiceFactory>().CreateCloudStorageAccount());
            services.AddTransient<CloudBlobClient>(p => p.GetService<ServiceFactory>().CreateCloudBlobClient());
            services.AddTransient<CloudQueueClient>(p => p.GetService<ServiceFactory>().CreateCloudQueueClient());
            services.AddTransient<DocumentClient>(p => p.GetService<ServiceFactory>().CreateDocumentClient());
            services.AddTransient<ImageManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var initTask = serviceScope.ServiceProvider.GetService<ImageManager>().Initialize();
                initTask.Wait();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
