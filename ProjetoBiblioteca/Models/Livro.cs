using System.ComponentModel.DataAnnotations;

namespace ProjetoBiblioteca.Models
{
    public class Livro
    {
        public int Id { get; set; }
        [Required]
        public string NomeLivro { get; set; }
        [Required]
        public string Autor { get; set; }
        
        [Range(0, int.MaxValue,
            ErrorMessage =
                "Quantidade não pode ser negativa.")]
        public int QuantidadeEstoque { get; set; }
        
        [Range(0, 18,
            ErrorMessage =
                "Faixa etária inválida.")]
        public int FaixaEtariaPermitida { get; set; }
        public string Categoria { get; set; }
        
        [Range(0, int.MaxValue,
            ErrorMessage =
                "Ano não pode ser negativa.")]
        public int AnoPublicacao { get; set; }
    }
}