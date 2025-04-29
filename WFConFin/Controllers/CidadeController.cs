using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WFConFin.Data;
using WFConFin.Models;

namespace WFConFin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CidadeController : Controller
    {
        private readonly WFConFinDbContext _context;

        public CidadeController(WFConFinDbContext context)
        {
            _context = context;
        }


        // rotas abaixo

        [HttpGet]
        public async Task<IActionResult> GetCidades()
        {
            try
            {
                var cidades = _context.Cidade.ToList();
                return Ok(cidades);
            }
            catch(Exception Err)
            {
                return BadRequest("Erro ao buscar cidades " + Err.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCidade([FromRoute] Guid id) 
        {
            try
            {
                var cidade = _context.Cidade.Find(id);
                return Ok(cidade);
            }
            catch (Exception Err)
            {
                return BadRequest("Erro ao buscar cidades " + Err.Message);
            }
        }


        [HttpGet("Pesquisa")]
        public async Task<IActionResult> GetCidadePorPesquisa([FromQuery] string valor)
        {
            try
            {
                var lista = from o in _context.Cidade.ToList()
                            where o.Nome.ToUpper().Contains(valor.ToUpper())
                                  || o.EstadoSigla.ToUpper().Contains(valor.ToUpper())
                            select o;
                return Ok(lista);
            }
            catch (Exception err)
            {
                return BadRequest("Erro ao pesquisar cidade: " + err.Message);
            }
        }




        [HttpGet("Paginacao")]
        public async Task<IActionResult> GetCidadePorPaginacao([FromQuery] string valor, int skip, int take, bool ordemDesc)
        {
            try
            {
                var lista = from o in _context.Cidade.ToList()
                            where o.EstadoSigla.ToUpper().Contains(valor.ToUpper())
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

                var paginacaoResponse = new PaginacaoResponse<Cidade>(lista, qtde, skip, take);

                return Ok(paginacaoResponse);
            }
            catch (Exception err)
            {
                return BadRequest("Erro ao pesquisar cidade: " + err.Message);
            }
        }



        [HttpPost]
        public async Task<IActionResult> PostCidades([FromBody] Cidade cidade)
        {
            try
            {
                await _context.Cidade.AddAsync(cidade);
                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {
                    return Ok("Cidade cadastrada com sucesso!");
                }
                else
                {
                    return BadRequest("Erro ao cadastrar cidade");
                }
            }
            catch (Exception Err)
            {
                return BadRequest("Erro ao buscar cidades " + Err.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> PutCidade([FromBody] Cidade cidade)
        {
            try
            {
                _context.Cidade.Update(cidade);
                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {
                    return Ok("Cidade atualizada com sucesso!");
                }
                else
                {
                    return BadRequest("Erro ao atualizar cidade");
                }
            }
            catch (Exception Err)
            {
                return BadRequest("Erro ao buscar cidades " + Err.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCidade([FromRoute] Guid id)
        {
            try
            {
                var cidade = await _context.Cidade.AddAsync(id);
                if (cidade == null)
                {
                    return NotFound("Cidade não encontrada");
                }

                _context.Cidade.Remove(cidade);
                var valor = await _context.SaveChangesAsync();

                if(valor == 1)
                {
                return Ok("Cidade deletada com sucesso");
                }
                else
                {
                    return BadRequest("Erro ao deletar cidade");
                }

            }
            catch (Exception err)
            {
                return BadRequest("Erro ao deletar cidade: " + err.Message);
            }
        }
    }
}
