namespace PrjFarmaciaWebAPI.Models
{
    public class ReporteVentas
    {
        public string NumVenta { get; set; } = "";
        public DateTime FechaVenta { get; set; }
        public string NombreCliente { get; set; } = "";
        public string NombreVendedor { get; set; } = "";
        public decimal TotalVenta { get; set; }
    }
}
