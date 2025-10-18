using Microsoft.AspNetCore.Mvc;
using WFConFin.Data;
using WFConFin.Models;

namespace WFConFin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : Controller
    {
        private readonly WFConFinDbContext _context;

        public PessoaController(WFConFinDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetPessoas()
        {
            try
            {
                var pessoas = _context.Pessoa.ToList();
                return Ok(pessoas);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreatePessoa([FromBody] Pessoa pessoa)
        {
            try
            {
                await _context.Pessoa.AddAsync(pessoa);
                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {
                    return Ok("Pessoa registrada com sucesso");
                }
                else
                {
                    return BadRequest("Houve um problema ao salvar a pessoa");
                }

            }
            catch (Exception err)
            {
                return StatusCode(500, err.InnerException?.Message ?? err.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdatePessoa([FromBody] Pessoa pessoa)
        {
            try
            {
                _context.Pessoa.Update(pessoa);
                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {
                    return Ok("Pessoa atualizada com sucesso");
                }
                else
                {
                    return BadRequest("Houve um problema ao atualizar a pessoa");
                }
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoa([FromRoute] Guid id)
        {
            try
            {
                Pessoa pessoa = await _context.Pessoa.FindAsync(id);
                if (pessoa != null)
                {
                    _context.Pessoa.Remove(pessoa);
                    var valor = await _context.SaveChangesAsync();

                    if (valor == 1)
                    {
                        return Ok("Pessoa deletada com sucesso");
                    }
                    else
                    {
                        return BadRequest("Houve um problema ao deletar a pessoa");
                    }

                }
                else
                {
                    return NotFound("Pessoa não encontrada");
                }
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPessoaPorId([FromRoute] Guid id)
        {
            try
            {
                var pessoa = await _context.Pessoa.FindAsync(id);
                if (pessoa != null)
                {
                    return Ok(pessoa);
                }
                else
                {
                    return NotFound("Pessoa não encontrada");
                }
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpGet("Pesquisa")]
        public async Task<IActionResult> GetPessoaPorPesquisa([FromQuery] string valor)
        {
            try
            {
                var lista = _context.Pessoa
                    .Where(p => p.Nome.ToUpper().Contains(valor.ToUpper()))
                    .ToList();
                return Ok(lista);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpGet("Paginacao")]
        public async Task<IActionResult> GetPessoaPorPaginacao([FromQuery] string valor, int skip, int take, bool ordemDesc)
        {
            try
            {
                var lista = _context.Pessoa
                    .Where(p => p.Nome.ToUpper().Contains(valor.ToUpper()))
                    .ToList();

                if (ordemDesc)
                {
                    lista = lista.OrderByDescending(p => p.Nome).ToList();
                }
                else
                {
                    lista = lista.OrderBy(p => p.Nome).ToList();
                }

                var qtde = lista.Count();

                lista = lista.Skip(skip).Take(take).ToList();

                var paginacaoResponse = new PaginacaoResponse<Pessoa>(lista, qtde, skip, take);

                return Ok(paginacaoResponse);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}
