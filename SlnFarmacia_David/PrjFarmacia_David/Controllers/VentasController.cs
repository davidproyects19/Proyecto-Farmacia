using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrjFarmacia_David.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Transactions;

namespace PrjFarmacia_David.Controllers
{
    public class VentasController : Controller
    {
        List<ReporteVentas> listaventas = new List<ReporteVentas>();

        public async Task<List<ReporteVentas>> traerVentas()
        {
            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud
                var respuesta = await cliente.GetAsync(
                    "http://localhost:5031/api/ReporteVentasAPI/GetReporteVentas");
                // convertir el contenido devuelto a un string
                string cadena = await respuesta.Content.ReadAsStringAsync();
                // deserializamos la cadena json a una lista de medicos 
                return JsonConvert.DeserializeObject<List<ReporteVentas>>(cadena)!;
            }
        }
        public async Task<List<Cliente>> traerCliente()
        {
            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud
                var respuesta = await cliente.GetAsync(
                    "http://localhost:5031/api/ReporteVentasAPI/GetCliente");
                // convertir el contenido devuelto a un string
                string cadena = await respuesta.Content.ReadAsStringAsync();
                // deserializamos la cadena json a una lista de especialidades 
                return JsonConvert.DeserializeObject<List<Cliente>>(cadena)!;
            }
        }
        public async Task<ActionResult> IndexVentas(int nropagina=0)
        {
            listaventas = await traerVentas();
            //
            ViewBag.contador = listaventas.Count;
            //
            // PAGINACION
            int filas_pagina = 7;
            int cantidad = listaventas.Count;
            int paginas = 0;
            if (cantidad % filas_pagina > 0)
                paginas = (cantidad / filas_pagina) + 1;
            else
                paginas = cantidad / filas_pagina;
            //
            ViewBag.PAGINAS = paginas;
            //
            ViewBag.nropagina = nropagina;

            return View(listaventas.Skip(nropagina * filas_pagina).Take(filas_pagina));
        }

        //
        public async Task<List<ReporteVentasPorCliente>> traerClientexNombre(int id)
        {
            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud
                var respuesta = await cliente.GetAsync(
                    "http://localhost:5031/api/ReporteVentasAPI/GetVentasCliente/" + id);
                // convertir el contenido devuelto a un string
                string cadena = await respuesta.Content.ReadAsStringAsync();
                // deserializamos la cadena json a una lista de medicos 
                return JsonConvert.DeserializeObject<List<ReporteVentasPorCliente>>(cadena)!;
            }
        }

        // GET
        public async Task<IActionResult> ClienteNombre(int id)
        {
            var listado = await traerClientexNombre(id);

            if (listado == null) listado = new List<ReporteVentasPorCliente>();

            ViewBag.nombreCliente = new SelectList(
                await traerCliente(), "Id", "Nombre");

            return View(listado);
        }

    }
}
