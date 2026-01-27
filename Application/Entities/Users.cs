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
    public string Name { get; set; } = "";
    
    public string Segundo_Apellido { get; set; } = "";
    public string Primer_Apellido { get; set; } = "";


}
