using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrjFarmacia_David.DAO;
using PrjFarmacia_David.Models;

namespace PrjFarmacia_David.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClientesDAO daocli;
        public ClientesController(ClientesDAO cli)
        {
            daocli = cli;
        }


        // GET: ClientesController
        public ActionResult IndexClientes(int nropagina = 0)
        {
            var listado = daocli.getClientes();
            // PAGINACION
            int filas_pagina = 7;
            int cantidad = listado.Count;
            int paginas = 0;
            if (cantidad % filas_pagina > 0)
                paginas = (cantidad / filas_pagina) + 1;
            else
                paginas = cantidad / filas_pagina;
            //
            ViewBag.PAGINAS = paginas;
            //
            ViewBag.nropagina = nropagina;

            return View(listado.Skip(nropagina * filas_pagina).Take(filas_pagina));
        }

        // GET: ClientesController/Details/5
        public ActionResult DetailsCliente(int id)
        {
            var buscar = daocli.getClientes().Find(cli => cli.Id == id);
            return View(buscar);
        }

        // GET: ClientesController/Create
        public ActionResult CreateCliente()
        {
            Cliente nuevo = new Cliente();
            return View(nuevo);
        }

        // POST: ClientesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCliente(Cliente obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = daocli.GrabarCliente(obj);
                    return RedirectToAction(nameof(IndexClientes));
                }
            }
            catch(Exception ex)
            {
                ViewBag.mensaje = "Fail: " + ex.Message;
            }
            return View(obj);
        }

        // GET: ClientesController/Edit/5
        public ActionResult EditCliente(int id)
        {
            Cliente buscar = daocli.getClientes().Find(cli => cli.Id == id)!;
            return View(buscar);
        }

        // POST: ClientesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCliente(int id, Cliente obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = daocli.GrabarCliente(obj);
                    return RedirectToAction(nameof(IndexClientes));
                }
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = "Error: " + ex.Message;
            }
            return View(obj);
        }

        // GET: ClientesController/Delete/5
        public ActionResult DeleteCliente(int id)
        {
            Cliente buscar = daocli.getClientes().Find(cli => cli.Id == id)!;
            return View(buscar);
        }

        // POST: ClientesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCliente(int id, IFormCollection collection)
        {
            try
            {
                TempData["mensaje"] = daocli.EliminarCliente(id);
                return RedirectToAction(nameof(IndexClientes));
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = "Error: " + ex.Message;
            }
            return View();
        }
        public ActionResult ClientesxNombre(string letraNom = "")
        {
            var listado = daocli.getClientesNombre(letraNom);
            return View(listado);
        }
    }
}
