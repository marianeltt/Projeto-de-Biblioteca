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
        [Range(0, int.MaxValue)]
        public int QuantidadeEstoque { get; set; }
        public int FaixaEtariaPermitida { get; set; }
        public string Categoria { get; set; }
        public int AnoPublicacao { get; set; }
    }
}