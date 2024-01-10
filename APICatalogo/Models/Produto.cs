using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]  // Valida como chave primaria
        public int ProdutoId { get; set; }

        [Required]  // Valida que não pode ser nulo "NOT NULL"
        [StringLength(80)] // Valida o tamanho da string 
        public string? Nome { get; set; }

        [Required]
        [StringLength(300)]
        public string? Descricao { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]  // Valida o tamanho do decimal e suas casas
        public decimal? Preco {  get; set; }

        [Required]
        [StringLength(300)]
        public string? ImagenUrl { get; set; }

        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
