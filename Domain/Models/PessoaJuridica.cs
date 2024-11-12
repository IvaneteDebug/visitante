using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  Dev.isitante.API.Domain.Models;

namespace Visitante.API.Domain.Models
{
    public class PessoaJuridica : Pessoa
    {
        public string CNPJ { get; set; } // CNPJ específico para Pessoa Jurídica
        public string RazaoSocial { get; set; } // Razão Social para PJ
        public string NomeFantasia { get; set; } // Nome Fantasia para PJ
    }

    public PessoaJuridica(string cnpj, string razaoSocial, string nomeFantasia, string documento)
        : base(documento) 
    {
        CNPJ = cnpj;
        RazaoSocial = razaoSocial;
        NomeFantasia = nomeFantasia;
    }
}
