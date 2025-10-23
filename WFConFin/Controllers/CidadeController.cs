using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WFConFin.Data;
using WFConFin.Models;

namespace WFConFin.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class CidadeController : Controller
    {
        private readonly WFConFinDbContext _context;

        public CidadeController(WFConFinDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCidades()
        {
            try
            {
                var cidades = _context.Cidade.ToList();
                return Ok(cidades);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpPost]
        [Authorize(Roles = "Gerente,Empregado")]
        public async Task<IActionResult> CreateCidade([FromBody] Cidade cidade)
        {
            try
            {
                await _context.Cidade.AddAsync(cidade);
                var valor = await _context.SaveChangesAsync();

                if(valor == 1)
                {
                    return Ok("Cidade registrada com sucesso");
                }
                else
                {
                    return BadRequest("Houve um problema ao salvar a cidade");
                }
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpPut]
        [Authorize(Roles = "Gerente,Empregado")]
        public async Task<IActionResult> UpdateCidade([FromBody] Cidade cidade)
        {
            try
            {
                _context.Cidade.Update(cidade);
                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {
                    return Ok("Cidade atualizada com sucesso");
                }
                else
                {
                    return BadRequest("Houve um problema ao atualizar a cidade");
                }
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> DeleteCidade([FromRoute] Guid id)
        {
            try
            {
                Cidade cidade = await _context.Cidade.FindAsync(id);

                if (cidade != null)
                {
                    _context.Cidade.Remove(cidade);
                    var valor = await _context.SaveChangesAsync();

                    if (valor == 1)
                    {
                        return Ok("Cidade removida com sucesso");
                    }
                    else
                    {
                        return BadRequest("Houve um problema ao remover a cidade");
                    }
                }
                else
                {
                    return NotFound("Cidade não encontrada");
                }
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> getCidadePorId([FromRoute] Guid id)
        {
            try
            {
                var cidade = await _context.Cidade.FindAsync(id);
                if (cidade != null)
                {
                    return Ok(cidade);
                }
                else
                {
                    return NotFound("Cidade não encontrada");
                }
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpGet("Pesquisa")]
        public async Task<IActionResult> GetCidadePorPesquisa([FromQuery] string valor)
        {
            try
            {
                var lista = _context.Cidade
                    .Where(c => c.Nome.ToUpper().Contains(valor.ToUpper())
                    || c.EstadoSigla.ToUpper().Contains(valor.ToUpper()))
                    .ToList();
                return Ok(lista);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpGet("Paginacao")]
        public async Task<IActionResult> GetCidadePorPaginacao([FromQuery] string valor, int skip, int take, bool ordemDesc)
        {
            try
            {
                var lista = _context.Cidade
                    .Where(c => c.Nome.ToUpper().Contains(valor.ToUpper())
                    || c.EstadoSigla.ToUpper().Contains(valor.ToUpper()))
                    .ToList();

                if (ordemDesc)
                {
                    lista = lista.OrderByDescending(c => c.Nome).ToList();
                }
                else
                {
                    lista = lista.OrderBy(c => c.Nome).ToList();
                }

                var qtde = lista.Count();

                lista = lista.Skip(skip).Take(take).ToList();

                var paginacaoResponse = new PaginacaoResponse<Cidade>(lista, qtde, skip, take);

                return Ok(paginacaoResponse);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}
