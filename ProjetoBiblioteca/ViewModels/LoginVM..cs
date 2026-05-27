using System.ComponentModel.DataAnnotations;

namespace ProjetoBiblioteca.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}