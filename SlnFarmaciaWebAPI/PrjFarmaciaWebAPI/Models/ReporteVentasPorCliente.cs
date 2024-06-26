namespace PrjFarmaciaWebAPI.Models
{
    public class ReporteVentasPorCliente
    {
        public string NombreCliente { get; set; } = "";
        public string NumVenta { get; set; } = "";
        public DateTime FechaVenta { get; set; }
        public string NombreProducto { get; set; } = "";
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal TotalVenta { get; set; }
    }
}
