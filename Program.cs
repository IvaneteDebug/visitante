using Dev.visitante.Aplication.Services;
using Dev.visitante.Handlers;
using Dev.visitante.Infrastructe.Persitence;
using Dev.visitante.Infrastructe.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<PessoaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!)
);

// Configuração dos serviços da aplicação
builder.Services.AddControllers();

builder.Services
       .AddAplication();


// Configuração do Swagger
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Visitante.Api",
            Version = "v1",
            Contact = new OpenApiContact
            {
                Name = "Ivanete Silva",
                Email = "ivanetevieira1000@gmail.com"
            }
        }
    );
});

var app = builder.Build();

// Configuração do pipeline de tratamento de requisições
if (app.Environment.IsDevelopment())
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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}",
    defaults: new { controller = "Pessoas", action = "Index" }
);

app.Run();
