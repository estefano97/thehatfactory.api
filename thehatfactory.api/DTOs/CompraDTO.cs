namespace thehatfactory.api.DTOs
{
    public class CompraDTO
    {

        public string IdPaypal { get; set; }
        public decimal TotalValue { get; set; }
        public Guid IdUsuarioCompra { get; set; }

        public List<ProductoCompraDTO> ProductoCompra { get; set; }
    }
}
