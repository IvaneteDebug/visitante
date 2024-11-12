using System;
using System.Threading.Tasks;
using Dev.visitante.Aplication.Services; // Certifique-se de que o namespace está correto
using Dev.visitante.Domain.Models; // Namespace para acessar a classe Pessoa e outros enums
using Microsoft.AspNetCore.Mvc; // Namespace necessário para ASP.NET Core MVC
using Visitante.API.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Dev.visitante.Domain.Models;

public class Pessoa
{
    // Dados específicos para condomínios

    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O documento é obrigatório.")]
    public string Documento { get; set; } // CPF ou CNPJ

    [Required(ErrorMessage = "O tipo de pessoa é obrigatório.")]
    public TipoPessoa Tipo { get; set; }

    public string Telefone { get; set; }
    public string Email { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string Observacao { get; set; }

    // Dados específicos para condomínios
    public string Bloco { get; set; }
    public string Apartamento { get; set; }
    public string MoradorResponsavel { get; set; }
    public string MotivoVisita { get; set; }
    public DateTime DataVisita { get; set; } = DateTime.Now;
    public DateTime? HoraEntrada { get; set; } = DateTime.Now;
    public DateTime? HoraSaida { get; set; } = DateTime.Now;
    public bool VisitanteRecorrente { get; set; }
    public string PlacaCarro { get; set; }
    public string StatusAprovacao { get; set; }

    public Pessoa(string documento)
    {
        Documento = documento;
    }

    public Pessoa(
        string documento,
        string telefone,
        string observacao,
        string bloco,
        string moradorResponsavel,
        DateTime dataVisita,
        bool visitanteRecorrente,
        string statusAprovacao
    )
    {
        this.Documento = documento;
        this.Telefone = telefone;
        this.Observacao = observacao;
        this.Bloco = bloco;
        this.MoradorResponsavel = moradorResponsavel;
        this.DataVisita = dataVisita;
        this.VisitanteRecorrente = visitanteRecorrente;
        this.StatusAprovacao = statusAprovacao;
    }

    public void Update(Pessoa pessoaAlualizada)
    {
        Nome = pessoaAlualizada.Nome;
        Documento = pessoaAlualizada.Documento;
        Tipo = pessoaAlualizada.Tipo;
        Telefone = pessoaAlualizada.Telefone;
        Email = pessoaAlualizada.Email;
        DataNascimento = pessoaAlualizada.DataNascimento;
        Observacao = pessoaAlualizada.Observacao;

        // Atualiza dados específicos do condomínio
        Bloco = pessoaAlualizada.Bloco;
        Apartamento = pessoaAlualizada.Apartamento;
        MoradorResponsavel = pessoaAlualizada.MoradorResponsavel;
        MotivoVisita = pessoaAlualizada.MotivoVisita;
        DataVisita = pessoaAlualizada.DataVisita;
        HoraEntrada = pessoaAlualizada.HoraEntrada;
        HoraSaida = pessoaAlualizada.HoraSaida;
        VisitanteRecorrente = pessoaAlualizada.VisitanteRecorrente;
        PlacaCarro = pessoaAlualizada.PlacaCarro;
        StatusAprovacao = pessoaAlualizada.StatusAprovacao;
    }

    public bool ValidarDocumento()
    {
        if (Tipo == TipoPessoa.Fisica)
        {
            return Documento.Length == 11; // CPF tem 11 dígitos
        }
        else if (Tipo == TipoPessoa.Juridica)
        {
            return Documento.Length == 14; // CNPJ tem 14 dígitos
        }

        return false;
    }
}
