namespace Application.Entities;
public class Product {
    public int ProductId {get; set;}
    public string Name {get; set;} = "";
    public string? Description {get; set;}
    public int Price {get; set;}
    public int CategoryId {get; set;}
    public int UserId {get; set;}

    public Category? Category {get; set;}

    public string? CategoryName {get; set;}
    public string? CategoryDescription {get; set;}

    public User? User { get; set; }
    public string? UserName { get; set; }
}