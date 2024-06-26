using System.Configuration;
using System.Data;
using PrjFarmacia_David.Models;
using System.Data.SqlClient;

namespace PrjFarmacia_David.DAO
{
    public class ClientesDAO
    {
        private string cad_cn = "";
        public ClientesDAO(IConfiguration config)
        {
            cad_cn = config.GetConnectionString("cn1");
        }

        public List<Cliente> getClientes()
        {
            var lista = new List<Cliente>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_cn, "PA_CLIENTES");
            while (dr.Read())
            {
                lista.Add(
                    new Cliente()
                    {
                        Id = dr.GetInt32(0),
                        Nombre = dr.GetString(1),
                        Direccion = dr.GetString(2),
                        Telefono = dr.GetString(3),
                        EliCli = dr.GetString(4),
                    });
            }
            return lista;
        }
        public List<Cliente> getClientesNombre(string letraNom)
        {
            List<Cliente> lista = new List<Cliente>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_cn, "PA_CLIENTES_NOMBRE", letraNom);
            while (dr.Read())
            {
                lista.Add(
                    new Cliente()
                    {
                        Id = dr.GetInt32(0),
                        Nombre = dr.GetString(1),
                        Direccion = dr.GetString(2),
                        Telefono = dr.GetString(3),
                        EliCli = dr.GetString(4),
                    });
            }
            dr.Close();
            return lista;
        }

        public string GrabarCliente(Cliente obj)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_cn, "PA_GRABAR_CLIENTE",
                                        obj.Id, obj.Nombre, obj.Direccion,
                                        obj.Telefono);

                return $"El cliente: {obj.Nombre} " +
                        "fué Registrado/Actualizado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EliminarCliente(int id)
        {
            SqlHelper.ExecuteNonQuery(cad_cn, "PA_ELIMINAR_CLIENTE", id);

            string cad = $"El cliente: {id} fué eliminado correctamente";

            return cad;
        }

    }
}
