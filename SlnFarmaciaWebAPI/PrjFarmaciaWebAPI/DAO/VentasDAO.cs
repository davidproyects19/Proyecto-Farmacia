using System.Data.SqlClient;
using PrjFarmaciaWebAPI.Models;

namespace PrjFarmaciaWebAPI.DAO
{
    public class VentasDAO
    {
        private string cad_sql = "";
        public VentasDAO(IConfiguration cfg)
        {
            cad_sql = cfg.GetConnectionString("cn1");
        }
        public List<ReporteVentas> GetReportesVentas()
        {
            var lista = new List<ReporteVentas>();

            SqlDataReader dr =
                SqlHelper.ExecuteReader(cad_sql, "ReporteVentas");
            while (dr.Read())
            {
                lista.Add(
                    new ReporteVentas()
                    {
                        NumVenta = dr.GetString(0),
                        FechaVenta = dr.GetDateTime(1),
                        NombreCliente = dr.GetString(2),
                        NombreVendedor = dr.GetString(3),
                        TotalVenta = dr.GetDecimal(4)
                    });
            }
            dr.Close();
            return lista;
        }
        public List<Cliente> GetCliente()
        {
            var lista = new List<Cliente>();

            SqlDataReader dr =
                SqlHelper.ExecuteReader(cad_sql, "PA_CLIENTES");
            while (dr.Read())
            {
                lista.Add(
                    new Cliente()
                    {
                        Id = dr.GetInt32(0),
                        nombre = dr.GetString(1)
                    });
            }
            dr.Close();
            return lista;
        }
        public List<ReporteVentasPorCliente> GetVentasCliente(int Id)
        {
            var lista = new List<ReporteVentasPorCliente>();

            SqlDataReader dr =
                SqlHelper.ExecuteReader(cad_sql, "ReporteVentasPorCliente", Id);

            while (dr.Read())
            {
                lista.Add(
                    new ReporteVentasPorCliente()
                    {
                        NombreCliente = dr.GetString(0),
                        NumVenta = dr.GetString(1),
                        FechaVenta = dr.GetDateTime(2),
                        NombreProducto = dr.GetString(3),
                        Cantidad = dr.GetInt32(4),
                        Precio = dr.GetDecimal(5),
                        TotalVenta = dr.GetDecimal(6)

                    });
            }
            dr.Close();

            return lista;
        }

    }
}
