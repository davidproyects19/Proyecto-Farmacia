using PrjFarmaciaWebAPI.DAO;
using PrjFarmaciaWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PrjFarmaciaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteVentasAPIController : ControllerBase
    {
        private readonly VentasDAO vta_dao;
        public ReporteVentasAPIController(VentasDAO dao)
        {
            vta_dao = dao;
        }

        // GET: api/<ReporteVentasAPIController>
        [HttpGet("GetReporteVentas")]
        public async Task<ActionResult<List<ReporteVentas>>> GetReportesVentas()
        {
            var listado = await Task.Run(() => vta_dao.GetReportesVentas());

            return Ok(listado);
        }

        // GET api/<ReporteVentasAPIController>/5
        [HttpGet("GetVentasCliente/{Id}")]
        public async Task<ActionResult<List<ReporteVentasPorCliente>>> GetVentasCliente(int Id)
        {
            var listado = await Task.Run(() => vta_dao.GetVentasCliente(Id));

            return Ok(listado);
        }
        [HttpGet("GetCliente")]
        public async Task<ActionResult<List<Cliente>>> GetCliente()
        {
            var listado = await Task.Run(() => vta_dao.GetCliente());

            return Ok(listado);
        }


    }
}
