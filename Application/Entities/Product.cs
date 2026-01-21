using System.ComponentModel.DataAnnotations;

namespace Application.Entities;

public class Product
{
    [Key]
    public int Id {get; set;}

    [Required(ErrorMessage = "El nombre es Obligatorio.")]
    public string? Name {get; set;}

    [Required(ErrorMessage = "La descripción es Obligatoria.")]
    public string? Description {get; set;}
    
    [Required(ErrorMessage = "El precio es Obligatorio.")]
    public int? Price {get; set;}
    
    [Required(ErrorMessage = "La categoría es Obligatoria.")]
    public int? CategoryId {get; set;}

    public int? UserId {get; set;}
}