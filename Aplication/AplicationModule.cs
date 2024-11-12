using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Visitante.API.Aplication
{
    public class AplicationModule
    {
        public static IServiceColection AddAplication(this IServiceColection services)
        {
            services.AddServices();
            return services;
        }
        private static IServiceColection AddServices(this IServiceColection services)
        {
            services.AddScoped<IPessoaService, PessoaService>();
            Services.AddScoped<IPessoaRepository, PessoaRepository>();
            Services.AddSingleton<IPessoaHandlerException, PessoaHandlerException>();
            return services;
        }
    }
}