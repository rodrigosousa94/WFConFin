using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WFConFin.Models;

namespace WFConFin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private static List<Estado> listaEstados = new List<Estado>();

        [HttpGet("estado")]
        public IActionResult GetEstados()
        {
            return Ok(listaEstados);
        }

        [HttpPost("create-estado")]
        public IActionResult CreateEstado([FromBody] Estado estado)
        {
            listaEstados.Add(estado);
            return Ok(listaEstados); 
        }
    }   
}
