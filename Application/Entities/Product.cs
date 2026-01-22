namespace Application.Entities
{
    public class Product
    {
       public int Id { get; set; }              // PK
        public string Nombre { get; set; } = "";
        public string? Descripcion { get; set; } 
        public decimal Precio { get; set; }

        public int CategoryId { get; set; }     // FK
        public int UsuarioId { get; set; }       // FK   

    }
}
