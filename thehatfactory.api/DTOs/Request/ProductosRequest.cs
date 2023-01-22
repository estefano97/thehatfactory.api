namespace thehatfactory.api.DTOs.Request
{
    public class ProductosRequest
    {
        public string imageURL {get; set;}
        public string productName { get; set; }
        public decimal precio { get; set; }
        public decimal descuento { get; set; }
        public string liga { get; set; }
        public string? categoria_id { get; set; }
        public string? equipo { get; set; }
    }
}
