using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities;

public class Category
{
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "El nombre es Obligatorio.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "La descripción es Obligatoria.")]
    public string Description { get; set; }
}
