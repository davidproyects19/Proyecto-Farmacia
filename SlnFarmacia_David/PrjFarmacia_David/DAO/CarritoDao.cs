using PrjFarmacia_David.Models;

namespace PrjFarmacia_David.DAO
{
    public class CarritoDao
    {
        private string cad_cn = "";
        public CarritoDao(IConfiguration cfg)
        {
            cad_cn = cfg.GetConnectionString("cn1");
        }
        public string GrabarVenta_Web(int cod_cli,
                                     decimal tot_vta,
                                     List<Carrito> lista_car)
        {
            // 
            string numero_vta =
                SqlHelper.ExecuteScalar(cad_cn, "PA_GRABAR_WEB_VENTAS_CAB",
                                       cod_cli, tot_vta).ToString()!;
            foreach (var item in lista_car)
            {
                SqlHelper.ExecuteNonQuery(cad_cn, "PA_GRABAR_WEB_VENTAS_DETA",
                    numero_vta, item.Codigo, item.Cantidad, item.Precio);
            }
            //
            return numero_vta;

        }
    }
}
