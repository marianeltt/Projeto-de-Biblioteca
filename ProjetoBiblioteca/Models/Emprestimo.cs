using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProjetoBiblioteca.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public DateTime DataEmprestimo { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int LivroId { get; set; }
        public Livro Livro { get; set; }

        public DateTime DataPrevistaDevolucao { get; set; }
        public DateTime? DataRealDevolucao { get; set; }
        
        [Range(0, 999999)]
        public decimal Multa { get; set; }

        public string Status { get; set; }
    }
}