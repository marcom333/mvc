using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public int Price { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; }


    public Category? Category { get; set; }
}
