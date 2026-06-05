using System.ComponentModel.DataAnnotations;

namespace ProjetoBiblioteca.Models
{
    // Representa os usuários cadastrados no sistema.
    // Contém os dados pessoais e de acesso utilizado para empréstimos e autenticação.
    public class Usuario
    {
        // Identificador único do usuário.
        public int Id { get; set; }

        [Display(Name = "Nome completo")]
        [Required(ErrorMessage = "Informe o nome completo.")]
        [StringLength(120)]
        public string NomeCompleto { get; set; } = string.Empty;

        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "Informe a data de nascimento.")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Informe o e-mail.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [StringLength(120)]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe a senha.")]
        [StringLength(50, MinimumLength = 6,
            ErrorMessage = "A senha deve ter entre 6 e 50 caracteres.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;
        
        // Define se o usuário está ativo ou inativo no sistema.
        [Display(Name = "Status")]
        [Required(ErrorMessage = "Informe o status.")]
        public bool Status { get; set; }
    }
}