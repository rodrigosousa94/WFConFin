using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WFConFin.Data;
using WFConFin.Models;

namespace WFConFin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContaController : Controller
    {
        private readonly WFConFinDbContext _context;

        public ContaController(WFConFinDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetContas()
        {
            try
            {
                var contas = _context.Conta.ToList();
                return Ok(contas);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpPost]
        [Authorize(Roles = "Gerente,Empregado")]
        public async Task<IActionResult> CreateConta([FromBody] Conta conta)
        {
            try
            {
                await _context.Conta.AddAsync(conta);
                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {
                    return Ok("Conta registrada");
                }
                else
                {
                    return BadRequest("Houve um problema ao salvar a conta");
                }
            }
            catch (Exception err)
            {
                return StatusCode(500, err.InnerException?.Message ?? err.Message);
            }
        }


        [HttpPut]
        [Authorize(Roles = "Gerente,Empregado")]
        public async Task<IActionResult> UpdateConta([FromBody] Conta conta)
        {
            try
            {
                _context.Conta.Update(conta);
                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {
                    return Ok("Conta atualizada com sucesso");
                }
                else
                {
                    return BadRequest("Houve um problema ao atualizar a conta");
                }
            }
            catch (Exception err)
            {
                return StatusCode(500, err.InnerException?.Message ?? err.Message);
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> DeleteConta([FromRoute] Guid id)
        {
            try
            {
                Conta conta = await _context.Conta.FindAsync(id);
                if(conta != null)
                {
                    _context.Conta.Remove(conta);
                    var valor = await _context.SaveChangesAsync();

                    if(valor == 1)
                    {
                        return Ok("Conta excluida com sucesso");
                    }
                    else
                    {
                        return BadRequest("Houve um problema ao excluir a conta");
                    }
                }
                else
                {
                    return NotFound("Conta não encontrada");
                }
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetContaPorId([FromRoute] Guid id)
        {
            try
            {
                var conta = await _context.Conta.FindAsync(id);
                if (conta != null)
                {
                    return Ok(conta);
                }
                else
                {
                    return NotFound("Conta não encontrada");
                }
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpGet("Pesquisa")]
        public async Task<IActionResult> GetContaPorPesquisa([FromQuery] string valor)
        {
            try
            {
                var contas = _context.Conta.Include(o => o.Pessoa)
                    .Where(c => c.Descricao.ToUpper().Contains(valor.ToUpper())
                    || c.Pessoa.Nome.ToUpper().Contains(valor.ToUpper())
                    ).ToList();
                return Ok(contas);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpGet("Paginacao")]
        public async Task<IActionResult> GetContaPorPaginacao([FromQuery] string? valor, int skip, int take, bool ordemDesc)
        {
            try
            {

                var lista = from o in _context.Conta.ToList()
                            select o;

                if (!String.IsNullOrEmpty(valor))
                {
                    lista = from o in lista
                            where o.Descricao.ToUpper().Contains(valor.ToUpper())
                            || o.Pessoa.Nome.ToUpper().Contains(valor.ToUpper())
                            select o;
                }

                if (ordemDesc)
                {
                    lista = lista.OrderByDescending(c => c.Descricao).ToList();
                }
                else
                {
                    lista = lista.OrderBy(c => c.Descricao).ToList();
                }

                var qtde = lista.Count();

                lista = lista.Skip((skip - 1) * take).Take(take).ToList();

                var paginacaoResponse = new PaginacaoResponse<Conta>(lista, qtde, skip, take);

                return Ok(paginacaoResponse);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpGet("Pessoa/{pessoaId}")]
        public async Task<IActionResult> GetContasPessoa([FromRoute] Guid pessoaId)
        {
            try
            {
                var conta = _context.Conta.Include(o => o.Pessoa)
                    .Where(c => c.PessoaId == pessoaId).ToList();

                return Ok(conta);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}
