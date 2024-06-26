using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjFarmacia_David.DAO;
using PrjFarmacia_David.Models;

namespace PrjFarmacia_David.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ProductosDAO daopro;

        public ProductosController(ProductosDAO pro)
        {
            daopro = pro;
        }


        // GET: ProductosController
        public ActionResult IndexProductos(int nropagina=0)
        {
            var listado = daopro.getProductos();
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

        // GET: ProductosController/Details/5
        public ActionResult DetailsProducto(int id)
        {
            var buscar = daopro.getProductos().Find(pro => pro.Id == id);

            return View(buscar);
        }

        // GET: ProductosController/Create
        public ActionResult CreateProducto()
        {
            Producto nuevo = new Producto();

            return View(nuevo);
        }

        // POST: ProductosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProducto(Producto obj)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    TempData["mensaje"] = daopro.GrabarProducto(obj);
                    return RedirectToAction(nameof(IndexProductos));
                }
            }
            catch(Exception ex)
            {
                ViewBag.mensaje = "Error: " + ex.Message;
            }
            return View(obj);
        }

        // GET: ProductosController/Edit/5
        public ActionResult EditProducto(int id)
        {
            Producto buscar = daopro.getProductos().Find(pro => pro.Id == id)!;
            return View(buscar);
        }

        // POST: ProductosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProducto(int id, Producto obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = daopro.GrabarProducto(obj);
                    return RedirectToAction(nameof(IndexProductos));
                }                
            }
            catch(Exception ex)
            {
                ViewBag.mensaje = "Error: " + ex.Message;
            }
            return View(obj);
        }

        // GET: ProductosController/Delete/5
        public ActionResult DeleteProducto(int id)
        {
            Producto buscar = daopro.getProductos().Find(pro => pro.Id == id)!;
            return View(buscar);
        }

        // POST: ProductosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProducto(int id, IFormCollection collection)
        {
            try
            {
                TempData["mensaje"] = daopro.EliminarProducto(id);
                return RedirectToAction(nameof(IndexProductos));
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = "Error: " + ex.Message;
            }
            return View();
        }
        public ActionResult ProductosXNombre(string letraNom = "")
        {
            var listado = daopro.getProductosNombre(letraNom);
            return View(listado);
        }
    }
}
