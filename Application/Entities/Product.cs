namespace Application.Entities;
public class Product
{
    public int ProductId { get; set; }
    public string Name {get; set;} = ""; //Propiedad not null para que siempre tenga un valor
    public string? Description {get; set;}
    public int Price { get; set; } 
    public int CategoryId { get; set; }
    public int UserId { get; set; }
}