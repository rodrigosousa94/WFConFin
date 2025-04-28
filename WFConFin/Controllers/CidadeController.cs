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
        public IActionResult GetCidades()
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
        public IActionResult GetCidade([FromRoute] Guid id) 
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
        public IActionResult GetCidadePorPesquisa([FromQuery] string valor)
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
        public IActionResult GetCidadePorPaginacao([FromQuery] string valor, int skip, int take, bool ordemDesc)
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
        public IActionResult PostCidades([FromBody] Cidade cidade)
        {
            try
            {
                _context.Cidade.Add(cidade);
                var valor = _context.SaveChanges();

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
        public IActionResult PutCidade([FromBody] Cidade cidade)
        {
            try
            {
                _context.Cidade.Update(cidade);
                var valor = _context.SaveChanges();

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
        public IActionResult DeleteCidade([FromRoute] Guid id)
        {
            try
            {
                var cidade = _context.Cidade.Find(id);
                if (cidade == null)
                {
                    return NotFound("Cidade não encontrada");
                }

                _context.Cidade.Remove(cidade);
                var valor = _context.SaveChanges();

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
