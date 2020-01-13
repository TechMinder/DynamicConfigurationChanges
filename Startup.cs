using DynamicConfigurationChanges.ConfigurationChanges;
using DynamicConfigurationChanges.HostedService;
using DynamicConfigurationChanges.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicConfigurationChanges
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.Configure<ExternalConfig>(Configuration.GetSection("external"));
            services.Configure<ConfiugrationApiSettings>(Configuration.GetSection("ConfiugrationApiSettings"));
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<ServiceClient>();
            services.AddSingleton<SingletonClass>();
            services.AddHostedService<ConfiugrationHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
