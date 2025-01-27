using CatalogoAPI.Validations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CatalogoAPI.Models;

public class Categoria
{
    public int CategoriaId { get; set; }
    [Required]
    [PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }
    public string? Descricao { get; set; }

    [JsonIgnore]
    public ICollection<Produto>? Produtos { get; set; }
}
