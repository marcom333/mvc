using System.ComponentModel.DataAnnotations;

namespace Application.Entities;

public class Category
{
    public int CategoryId { get; set; }
    
    [Required(ErrorMessage = "El nombre es Obligatorio.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "La descripción es Obligatoria.")]
    public string Description { get; set; }
}