using System.ComponentModel.DataAnnotations;

namespace ProjetoBiblioteca.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Informe o e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        public string Senha { get; set; }
    }
}