using Microsoft.AspNetCore.Mvc;
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
                return BadRequest(err.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateEstado([FromBody] Estado estado)
        {
            try
            {
                await _context.Estado.AddAsync(estado);
                var valor = await _context.SaveChangesAsync();

                if(valor == 1)
                {
                    return Ok("Estado registrado com sucesso");
                }
                else
                {
                    return BadRequest("Houve um problema ao salvar o estado");

                }
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateEstado([FromBody] Estado estado)
        {
            try
            {
                _context.Estado.Update(estado);
                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {
                    return Ok("Estado atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Houve um problema ao atualizar o estado");
                }
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpDelete("{sigla}")]
        public async Task<IActionResult> DeleteEstado([FromRoute] string sigla)
        {
            try
            {
                var estado = await _context.Estado.FindAsync(sigla);

                if(estado.Sigla == sigla && !string.IsNullOrEmpty(estado.Sigla))
                {

                    _context.Estado.Remove(estado);
                    var valor = await _context.SaveChangesAsync();

                    if (valor == 1)
                    {
                        return Ok("Estado excluído com sucesso");
                    }
                    else
                    {
                        return BadRequest("Houve um problema ao excluir o estado");
                    }

                }
                else
                {
                    return NotFound("Estado não encontrado");
                }

            }
            catch(Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpGet("{sigla}")]
        public async Task<IActionResult> GetEstadoPorSigla([FromRoute] string sigla)
        {
            try
            {
                var estado = await _context.Estado.FindAsync(sigla);

                if (estado.Sigla == sigla && !string.IsNullOrEmpty(estado.Sigla))
                {
                    return Ok(estado);
                }
                else
                {
                    return NotFound("Estado não encontrado");
                }
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpGet("Pesquisa")]
        public async Task<IActionResult> GetEstadoPorPesquisa([FromQuery] string valor)
        {
            try
            {
                var lista = _context.Estado
                            .Where(o => o.Sigla.ToUpper().Contains(valor.ToUpper())
                                    || o.Nome.ToUpper().Contains(valor.ToUpper())
                                   )
                            .ToList();
                return Ok(lista);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("Paginacao")]
        public async Task<IActionResult> GetEstadoPaginacao([FromQuery] string valor, int skip, int take, bool ordemDesc )
        {
            try
            {
                var lista = _context.Estado
                            .Where(o => o.Sigla.ToUpper().Contains(valor.ToUpper())
                                    || o.Nome.ToUpper().Contains(valor.ToUpper())
                                   )
                            .ToList();

                if (ordemDesc)
                {
                    lista = lista.OrderByDescending(o => o.Nome).ToList();
                }
                else
                {
                    lista = lista.OrderBy(o => o.Nome).ToList();
                }

                var qtde = lista.Count();

                lista = lista.Skip(skip).Take(take).ToList();

                var paginacaoResponse = new PaginacaoResponse<Estado>(lista, qtde, skip, take);

                return Ok(paginacaoResponse);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}
