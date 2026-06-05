using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProjetoBiblioteca.Models
{
    // Representa um empréstimo realizado por um usuário.
    // Armazena informações sobre datas, livro emprestado, usuário responsável, multa e situação do empréstimo.
    public class Emprestimo
    {
        // Identificador único do empréstimo.
        public int Id { get; set; }
        
        [Display(Name = "Data do Empréstimo")]
        [Required(ErrorMessage = "Informe a data do empréstimo.")]
        [DataType(DataType.Date)]
        public DateTime DataEmprestimo { get; set; }

        // Chave estrangeira do usuário.
        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "Selecione um usuário.")]
        public int UsuarioId { get; set; }
        // Usuário que realizou o empréstimo.
        public Usuario Usuario { get; set; }
        
        // Chave estrangeira do livro.
        [Display(Name = "Livro")]
        [Required(ErrorMessage = "Selecione um livro.")]
        public int LivroId { get; set; }
        // Livro emprestado.
        public Livro Livro { get; set; }
        
        [Display(Name = "Data Prevista de Devolução")]
        [Required(ErrorMessage = "Informe a data prevista de devolução.")]
        [DataType(DataType.Date)]
        public DateTime DataPrevistaDevolucao { get; set; }
        public DateTime? DataRealDevolucao { get; set; }
        
        // Valor da multa calculada em caso de atraso.
        [Range(0, 999999)]
        public decimal Multa { get; set; }
        
        // Situação atual do empréstimo.
        public string Status { get; set; }
    }
}