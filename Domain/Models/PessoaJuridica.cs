using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  Dev.isitante.API.Domain.Models;

namespace Visitante.API.Domain.Models
{
    public class PessoaJuridica : Pessoa
    {
        public string CNPJ { get; set; } 
        public string RazaoSocial { get; set; } 
        public string NomeFantasia { get; set; } 
    }

    public PessoaJuridica(string cnpj, string razaoSocial, string nomeFantasia, string documento)
        : base(documento) 
    {
        CNPJ = cnpj;
        RazaoSocial = razaoSocial;
        NomeFantasia = nomeFantasia;
    }
}
