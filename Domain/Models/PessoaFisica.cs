using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  Dev.isitante.API.Domain.Models;

namespace Dev.visitante.Domain.Models;
{
    public class PessoaFisica : Pessoa
    {
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; } // Para PF, a data de nascimento pode ser obrigatória?
        public string Profissao { get; set; } // Profissão pode ser um campo específico para PF?
    }

    // Construtor de PessoaFisica
    public PessoaFisica(string cpf, string profissao, string documento)
        : base(documento)
    {
        CPF = cpf;
        Profissao = profissao;
    }
}
