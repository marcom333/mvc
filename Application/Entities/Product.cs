namespace Application.Entities;

public class Product
{
    public int producto_id {get; set;}
    public string Name {get; set;}
    public string? Description {get; set;}
    public int Precio {get; set;}
    public int CategoryId {get; set;}
    public int UserId {get; set;}
}