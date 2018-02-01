using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RulesService.Application;
using RulesService.Data.InMemoryRepositories;
using RulesService.Domain;

namespace RulesService.Presentation.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(suio =>
            {
                suio.SwaggerEndpoint("/swagger/v1/swagger.json", "Rules Service V1 API");
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDomain()
                .AddInMemoryRepositories()
                .AddApplication();

            services.AddMvc();

            services.AddSwaggerGen(sgo =>
            {
                sgo.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Rules Service V1 API",
                    Version = "v1"
                });
            });
        }
    }
}