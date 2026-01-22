using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        public int? Price { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public int? CategoryId { get; set; }

        public int? UserId { get; set; }

    }
}
