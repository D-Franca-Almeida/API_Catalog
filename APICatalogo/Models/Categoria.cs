using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;

[Table("Categorias")]
public class Categoria
{
    public Categoria() { 

        Produtos = new Collection<Produto>();
    }
    [Key]  //Valida chave primary key
    public int CategoriaId { get; set; }

    [Required]  // descreve que não pode ser nulo "NOT NULL"
    [MaxLength(80)] // descreve o tamanho da string máximo 
    public string? Nome { get; set; }

    [Required]
    [MaxLength(300)] 
    public string? ImagemUrl { get; set; }

    public ICollection<Produto>? Produtos { get; set; }

}

