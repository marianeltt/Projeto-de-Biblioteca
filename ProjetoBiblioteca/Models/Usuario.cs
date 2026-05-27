using System.ComponentModel.DataAnnotations;

namespace ProjetoBiblioteca.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string NomeCompleto { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        public bool Ativo { get; set; }
    }
}