using PrjFarmacia_David.Models;

namespace PrjFarmacia_David.DAO
{
    public class UsuariosDAO
    {
        private string cad_cn = "";
        public UsuariosDAO(IConfiguration cfg)
        {
            cad_cn = cfg.GetConnectionString("cn1");
        }
        public string Validar_Usuario(Usuario obj)
        {
            int rpta = Convert.ToInt32(
                SqlHelper.ExecuteScalar(cad_cn, "pa_encontrar_usuario",
                                    obj.login_usu, obj.clave_usu)
                );

            if (rpta == 1)
                return "Bienvenido al Sistema: " + obj.login_usu;
            else
                return "Error, Login y/o Clave Incorrecta";
        }
    }
}
