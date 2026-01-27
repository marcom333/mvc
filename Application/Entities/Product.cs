/* 
    nombre
    descripcion
    precio
    categoria_id
    usuario_id
*/

namespace Application.Entities;

public class Product {
    public int ProductId {get; set;}
    public string Name {get; set;} = "";
    public string? Description {get; set;}
    public int Price {get; set;}
    public int CategoryId {get; set;}
    public int UserId {get; set;}
}
