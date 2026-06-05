using System.ComponentModel.DataAnnotations;

namespace ProjetoBiblioteca.Models
{
    // Representa os livros cadastrados na biblioteca.
    // Armazena informações utilizadas nos empréstimos e no controle de estoque.
    public class Livro
    {
        // Identificador único do livro.
        public int Id { get; set; }
        
        [Display(Name = "Nome do Livro")]
        [Required(ErrorMessage = "Informe o nome do livro.")]
        public string NomeLivro { get; set; }
        
        [Display(Name = "Autor")]
        [Required(ErrorMessage = "Informe o autor.")]
        public string Autor { get; set; }
        
        // Quantidade de exemplares disponíveis para empréstimo.
        [Display(Name = "Quantidade em Estoque")]
        [Required(ErrorMessage = "Informe a quantidade em estoque.")]
        [Range(0, int.MaxValue,
            ErrorMessage = "Quantidade não pode ser negativa.")]
        public int QuantidadeEstoque { get; set; }
        
        // Define a idade mínima recomendada para leitura.
        [Display(Name = "Faixa Etária Permitida")]
        [Required(ErrorMessage = "Informe a faixa etária permitida.")]
        [Range(0, 18,
            ErrorMessage = "Faixa etária inválida.")]
        public int FaixaEtariaPermitida { get; set; }
        
        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Informe a categoria.")]
        public string Categoria { get; set; }
        
        // Ano em que o livro foi publicado.
        [Display(Name = "Ano de Publicação")]
        [Required(ErrorMessage = "Informe o ano de publicação.")]
        [Range(0, int.MaxValue,
            ErrorMessage = "Ano não pode ser negativo.")]
        public int AnoPublicacao { get; set; }
    }
}