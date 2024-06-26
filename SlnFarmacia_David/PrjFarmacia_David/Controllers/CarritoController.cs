using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PrjFarmacia_David.DAO;
using PrjFarmacia_David.Models;
using System.Transactions;

namespace PrjFarmacia_David.Controllers
{
    public class CarritoController : Controller
    {
        private readonly FARMACIABDContext db;
        private readonly CarritoDao dao;
        private readonly ProductosDAO dao_pro;
        public CarritoController(FARMACIABDContext ctx, CarritoDao _dao,ProductosDAO daopro)
        {
            db = ctx;
            dao = _dao;
            dao_pro = daopro; 
        }

        List<Carrito> listacarrito = new List<Carrito>();
        void GrabarCarrito()
        {
            HttpContext.Session.SetString("Carrito",
                JsonConvert.SerializeObject(listacarrito));
        }
        List<Carrito> RecuperarCarrito()
        {
            return JsonConvert.DeserializeObject<List<Carrito>>(
                HttpContext.Session.GetString("Carrito")!)!;
        }

        // GET: CarritoController
        public ActionResult IndexProductos()
        {
            if (HttpContext.Session.GetString("Carrito") == null)
                GrabarCarrito();

            // traer todos los articulos
            var listado = dao_pro.getProductos();

            return View(listado);
        }

        // GET: CarritoController/Details/5
        public ActionResult AgregarProductoCarrito(int id)
        {
            Producto buscado = db.Productos.Find(id)!;

            return View(buscado);
        }

        // POST: CarritoController/Create
        [HttpPost]
        public ActionResult AgregarProductoCarrito(int id, int cant)
        {
            Producto buscado = db.Productos.Find(id)!;
            Carrito car = new Carrito()
            {
                Codigo = buscado.Id,
                Nombre = buscado.Nombre,
                Precio = buscado.Precio,
                Cantidad = cant
            };

            listacarrito = RecuperarCarrito();

            var encontrado = listacarrito.Find(c => c.Codigo == id);

            if (encontrado == null)
            {
                listacarrito.Add(car);
                ViewBag.mensaje = "Producto agregado correctamente";
            }
            else 
            {
                encontrado!.Cantidad += cant; 
                ViewBag.mensaje = $"Cantidad del Producto: {encontrado.Nombre} " +
                                  $"fue actualizada a: {encontrado.Cantidad}";
            }

            GrabarCarrito();
            //
            return View(buscado);
        }
        // GET
        public IActionResult VerCarritoCompra()
        {
            // Recuperar el Carrito de Compra
            listacarrito = RecuperarCarrito();
            if (listacarrito.Count == 0)
                return RedirectToAction("IndexProductos");
            ViewBag.suma_importes = listacarrito.Sum(c => c.Importe);

            return View(listacarrito);
        }
        // GET: CarritoController/Delete/5
        public ActionResult EliminarArticuloCarrito(int id)
        {
            listacarrito = RecuperarCarrito();

            var buscado = listacarrito.Find(c => c.Codigo == id);
            listacarrito.Remove(buscado!);
            GrabarCarrito();

            return RedirectToAction("VerCarritoCompra");
        }

        // POST: CarritoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarArticuloCarrito(int id, IFormCollection collection)
        {
            listacarrito = RecuperarCarrito();

            listacarrito.RemoveAll(a => a.Codigo == id);

            GrabarCarrito();

            return RedirectToAction("VerCarritoCompra");
        }


        // GET: List de Carrito/
        public ActionResult PagarCarrito()
        {
            listacarrito = RecuperarCarrito();

            ViewBag.clientes = new SelectList(
                db.Clientes.ToList(), "Id", "Nombre");
            //
            ViewBag.suma_importes = listacarrito.Sum(c => c.Importe);
            //
            return View(listacarrito);
        }

        // POST: CarritoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PagarCarrito(int codigo)
        {
            listacarrito = RecuperarCarrito();
            try
            {
                decimal total = listacarrito.Sum(c => c.Importe);

                string numero = dao.GrabarVenta_Web(codigo, total, listacarrito);

                string nomcli = db.Clientes.Find(codigo)!.Nombre;

                TempData["cliente"] = nomcli;
                TempData["mensaje"] =
                    $"Tu venta: {numero} fué registrada correctamente";

                HttpContext.Session.Clear();
            }
            catch (Exception ex)
            {
                TempData["mensaje"] = ex.Message;
            }
            //
            return RedirectToAction("ResultadoPagarCarrito");
        }

        public IActionResult ResultadoPagarCarrito()
        {
            return View();
        }

        //

    }
}
