using Microsoft.AspNetCore.Mvc;
using PrjFarmacia_David.DAO;
using PrjFarmacia_David.Models;

namespace PrjFarmacia_David.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuariosDAO dao;
        public UsuarioController(UsuariosDAO usu)
        {
            dao = usu;
        }
        // GET
        public IActionResult ValidarIngreso()
        {

            return View();
        }
        // POST
        [HttpPost]
        public IActionResult ValidarIngreso(Usuario obj)
        {
            if (ModelState.IsValid)
            {
                string mensaje = dao.Validar_Usuario(obj);
                if (mensaje[0].ToString() == "B")
                {
                    TempData["mensaje"] = mensaje;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.mensaje = mensaje;
            }

            return View(obj);

        }
    }
}
