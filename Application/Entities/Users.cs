using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities;

public class User
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "El nombre es Obligatorio.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "El primer apellido es Obligatorio.")]
    public string Primer_Apellido { get; set; }

    [Required(ErrorMessage = "El segundo apellido es Obligatorio.")]
    public string Segundo_Apellido { get; set; }
}
