using System.ComponentModel.DataAnnotations;
using Application.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Web.ViewModels;

public class ProductViewModel
{
    public int ProductId {get; set;}

    [Required(ErrorMessage = "El nombre es Obligatorio.")]
    public string? Name {get; set;}

    [Required(ErrorMessage = "La descripción es Obligatoria.")]
    public string? Description {get; set;}
    
    [Required(ErrorMessage = "El precio es Obligatorio.")]
    public int? Price {get; set;}
    
    [Required(ErrorMessage = "La categoría es Obligatoria.")]
    public int? CategoryId {get; set;}

    public List<Category>? categories = new List<Category>();

    public List<User>? users = new List<User>();

    [ValidateNever]
    public string? action { get; set;} = "store";
}