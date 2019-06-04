using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AGLCodingExercise.Data;
using AGLCodingExercise.Domain.Abstractions;
using AGLCodingExercise.Domain.Model;
using AGLCodingExercise.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AGLCodingExercise.Web
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
            services.AddMvc();

            services.AddSingleton<IPetsService, PetsService>();
            services.AddSingleton<IPeopleRepository, PeopleRepository>();
            services.AddHttpClient<IPeopleRepository, PeopleRepository>(client =>
                client.BaseAddress = new Uri(Configuration.GetValue<string>("DataProviderBaseUrl")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMvc();
        }
    }
}
