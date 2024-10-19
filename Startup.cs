using Dev.visitante.Aplication.Services;
using Dev.visitante.Handlers;
using Dev.visitante.Infrastructe.Persistence;
using Dev.visitante.Infrastructe.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Dev.visitante.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuração para usar SQL
            services.AddDbContext<PessoaDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")!));

            services.AddControllers();

            services.AddSingleton<IPessoaHandlerException, PessoaHandlerException>();
            services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();

            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Visitante.Api",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Ivanete Silva",
                        Email = "ivanetevieira1000@gmail.com"
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Visitante.Api v1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Pessoas", action = "Index" });
            });
        }
    }
}