namespace Dev.visitante.Domain.Models;
public class Pessoa
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Profissao { get; set; }
    public string Descricao { get; set; }
    public int Idade { get; set; }

    public Pessoa(string nome, string descricao, string profissao, int idade)
    {
        Nome = nome;
        Idade = idade;
        Profissao = profissao;
        Descricao = descricao;
    }

    public void UpDate(string nome, string descricao, string profissao, int idade)
    {
        Nome = nome;
        Idade = idade;
        Profissao = profissao;
        Descricao = descricao;
    }
}