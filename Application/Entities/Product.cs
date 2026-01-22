using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class Product
    {
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public int Precio { get; set; }
        public  int CategoryId { get; set; }
        public int UserId { get; set; }

    }
}
