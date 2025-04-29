using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WFConFin.Data;
using WFConFin.Models;

namespace WFConFin.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EstadoController : Controller
    {

        private readonly WFConFinDbContext _context;

        public EstadoController(WFConFinDbContext context)
        {
            _context = context;
        }


        //rotas abaixo


        [HttpGet]
        public async Task<IActionResult> GetEstados()
        {
            try 
            {
                var estados = _context.Estado.ToList();
                return Ok(estados);
            }
            catch(Exception err)
            {
                return BadRequest("Erro ao buscar estados " + err.Message);
            }
        }


        [HttpGet("{sigla}")]
        public async Task<IActionResult> GetEstadoPorSigla([FromRoute] string sigla)
        {
            try
            {
                var estado = await _context.Estado.FindAsync(sigla);
                if (estado == null)
                {
                    return NotFound("Estado não encontrado");
                }
                return Ok(estado);
            }
            catch (Exception err)
            {
                return BadRequest("Erro ao Listar estado: " + err.Message);
            }
        }



        [HttpGet("Pesquisa")]
        public async Task<IActionResult> GetEstadoPorPesquisa([FromQuery] string valor)
        {
            try
            {
                var lista = from o in _context.Estado.ToList()
                            where o.Sigla.ToUpper().Contains(valor.ToUpper())
                                  || o.Nome.ToUpper().Contains(valor.ToUpper())
                            select o;
                return Ok(lista);
            }
            catch (Exception err)
            {
                return BadRequest("Erro ao pesquisar estado: " + err.Message);
            }
        }


        [HttpGet("Paginacao")]
        public async Task<IActionResult> GetEstadoPorPaginacao([FromQuery] string valor, int skip, int take, bool ordemDesc)
        {
            try
            {
                var lista = from o in _context.Estado.ToList()
                            where o.Sigla.ToUpper().Contains(valor.ToUpper())
                                  || o.Nome.ToUpper().Contains(valor.ToUpper())
                            select o;

                if (ordemDesc)
                {
                    lista = from o in lista
                            orderby o.Nome descending
                            select o;
                }
                else
                {
                    lista = from o in lista
                            orderby o.Nome ascending
                            select o;
                }

                var qtde = lista.Count();

                lista = lista.Skip(skip).Take(take).ToList();

                var paginacaoResponse = new PaginacaoResponse<Estado>(lista, qtde, skip, take);

                return Ok(paginacaoResponse);
            }
            catch (Exception err)
            {
                return BadRequest("Erro ao pesquisar estado: " + err.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostEstado([FromBody] Estado estado)
        {
            try
            {
                await _context.Estado.AddAsync(estado);
                var valor = await _context.SaveChangesAsync();
                if (valor == 1)
                {
                    return Ok("Estado criado com sucesso!");
                }
                else
                {
                    return BadRequest("Erro ao criar estado!");
                }
            }
            catch (Exception err)
            {
                return BadRequest("Erro ao criar estado: " + err.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> PutEstado([FromBody] Estado estado)
        {
            try
            {
                _context.Estado.Update(estado);
                var valor = await _context.SaveChangesAsync();
                if (valor == 1)
                {
                    return Ok("Estado alterado com sucesso");
                }
                else
                {
                    return BadRequest("Erro ao alterar estado");
                }
            }
            catch(Exception err)
            {
                return BadRequest("Erro ao alterar estado: " + err.Message);
            }
        }


        [HttpDelete("{sigla}")]
        public async Task<IActionResult> DeleteEstado([FromRoute] string sigla)
        {
            try
            {
                var estado = await _context.Estado.FindAsync(sigla);
                if (estado.Sigla == sigla && !string.IsNullOrEmpty(estado.Sigla))
                {
                    _context.Estado.Remove(estado);
                    var valor = await _context.SaveChangesAsync();

                    if (valor == 1)
                    {
                        return Ok("Estado deletado com sucesso");
                    }
                    else
                    {
                        return BadRequest("Erro ao deletar estado");
                    }
                }
                else
                {
                    return NotFound("Estado não encontrado");
                }
            }
            catch (Exception err)
            {
                return BadRequest("Erro ao deletar estado: " + err.Message);
            }
        }
    }
}
