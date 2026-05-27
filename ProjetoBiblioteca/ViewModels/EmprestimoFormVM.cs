using System.ComponentModel.DataAnnotations;

namespace ProjetoBiblioteca.ViewModels
{
    public class EmprestimoFormVM
    {
        [Required(ErrorMessage = "Selecione um usuário.")]
        public int UsuarioId { get; set; }

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