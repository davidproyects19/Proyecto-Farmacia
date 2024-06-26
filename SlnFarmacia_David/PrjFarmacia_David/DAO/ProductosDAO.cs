using Microsoft.AspNetCore.Mvc.Routing;
using PrjFarmacia_David.Models;
using System.Data.SqlClient;

namespace PrjFarmacia_David.DAO
{
    public class ProductosDAO
    {
        private string cad_cn = "";
        public ProductosDAO(IConfiguration config)
        {
            cad_cn = config.GetConnectionString("cn1");
        }

        public List<Producto> getProductos()
        {
            var lista = new List<Producto>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_cn, "PA_PRODUCTOS");
            while (dr.Read())
            {
                lista.Add(
                    new Producto()
                    {
                        Id = dr.GetInt32(0),
                        Nombre = dr.GetString(1),
                        Descripcion = dr.GetString(2),
                        Precio = dr.GetDecimal(3),
                        Cantidad = dr.GetInt32(4),
                        EliPro = dr.GetString(5),
                    });
            }
            return lista;
        }
        public List<Producto> getProductosNombre(string letraNom)
        {
            List<Producto> lista = new List<Producto>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_cn, "PA_PRODUCTOS_NOMBRE", letraNom);
            while (dr.Read())
            {
                lista.Add(
                    new Producto()
                    {
                        Id = dr.GetInt32(0),
                        Nombre = dr.GetString(1),
                        Descripcion = dr.GetString(2),
                        Precio = dr.GetDecimal(3),
                        Cantidad = dr.GetInt32(4),
                        EliPro = dr.GetString(5),
                    });
            }
            dr.Close();
            return lista;
        }

        public string GrabarProducto(Producto obj)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_cn, "PA_GRABAR_PRODUCTO",
                                        obj.Id, obj.Nombre, obj.Descripcion,
                                        obj.Precio, obj.Cantidad);

                return $"El Producto: {obj.Nombre} " +
                        "fué Registrado/Actualizado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EliminarProducto(int id)
        {
            SqlHelper.ExecuteNonQuery(cad_cn, "PA_ELIMINAR_PRODUCTO", id);

            string cad = $"El producto: {id} fué eliminado correctamente";

            return cad;
        }

    }
}
