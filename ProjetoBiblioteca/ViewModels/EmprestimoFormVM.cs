using System.ComponentModel.DataAnnotations;

namespace ProjetoBiblioteca.ViewModels
{
    // ViewModel utilizada para cadastro e edição de empréstimos.
    public class EmprestimoFormVM
    {
        public int Id { get; set; }
        
        // Identificador do usuário que realizará o empréstimo.
        [Required(ErrorMessage = "Selecione um usuário.")]
        public int UsuarioId { get; set; }

        // Identificador do livro emprestado.
        [Required(ErrorMessage = "Selecione um livro.")]
        public int LivroId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataEmprestimo { get; set; }

        [Required(ErrorMessage = "Informe a data prevista de devolução.")]
        [DataType(DataType.Date)]
        public DateTime DataPrevistaDevolucao { get; set; }
    }
}