using System.ComponentModel.DataAnnotations;

namespace Web.ViewModel;

public class ProductCreateViewModel
{
    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(50, ErrorMessage = "El máximo de caracteres es de 50")]
    public string Name {get; set;} = "";
    [StringLength(100, ErrorMessage = "El máximo de caracteres es de 100")]
    public string? Description {get; set;}
    [Required(ErrorMessage = "Este campo es requerido")]
    [Range(1, 500, ErrorMessage = "El rango de valores es de 1 a 500")]
    public int Price {get; set;}
    [Required(ErrorMessage = "Por favor seleccione una categoría")]
    public int CategoryId {get; set;}
    [Required(ErrorMessage = "Por favor seleccione un usuario")]
    public int UserId {get; set;}
}