using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alltrades_bot.Core.Options;
using alltrades_bot.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace alltrades_bot
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

            services.AddHttpClient();

            ConfigureSettings(services);

            services.AddTransient<ITwitterRepository, TwitterRepository>();
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

        private void ConfigureSettings(IServiceCollection services)
        {
            services.Configure<TwitterOptions>(Configuration.GetSection(nameof(TwitterOptions)));
            // ITwitterOptions twitterConfig = new TwitterOptions();
            // Configuration.Bind(nameof(TwitterOptions), twitterConfig);

            // services.AddSingleton(twitterConfig);
        }
    }
}
