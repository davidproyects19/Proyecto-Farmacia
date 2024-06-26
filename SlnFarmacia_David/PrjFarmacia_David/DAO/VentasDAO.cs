using PrjFarmacia_David.Models;

namespace PrjFarmacia_David.DAO
{
    public class VentasDAO
    {
        private string cad_cn = "";
        public VentasDAO(IConfiguration cfg)
        {
            cad_cn = cfg.GetConnectionString("cn1");
        }
    }
}
