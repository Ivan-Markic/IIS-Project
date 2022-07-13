using LibraryForIISProject.Models;
using LibraryForIISProject.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiService
{
    public class Startup
    {
        private const string XML_CUSTOMERS_FILEPATH = "../LibraryForIISProject/Resources/students.xml";

        private List<Student> databasestudents;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            databasestudents = XmlFileHandler.GetStudentsFromXml(XML_CUSTOMERS_FILEPATH);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllers().AddXmlDataContractSerializerFormatters();
            services.AddSingleton<List<Student>>(databasestudents);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
