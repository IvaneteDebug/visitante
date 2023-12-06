using Dev.visitante.Aplication.Services;
using Dev.visitante.Handlers;
using Dev.visitante.Infrastructe.Persistence;
using Dev.visitante.Infrastructe.Repositories;
using Microsoft.EntityFrameworkCore;
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
            services.AddDbContext<PessoaDbContext>(options =>
                options.UseInMemoryDatabase("PessoaDbContext"));

            services.AddControllers(options =>
            {
                services.AddSingleton<IPessoaHandlerException, PessoaHandlerException>();

                services.AddScoped<IPessoaService, PessoaService>();
                services.AddScoped<IPessoaRepository, PessoaRepository>();
            });

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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
