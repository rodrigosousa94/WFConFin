using Microsoft.AspNetCore.Mvc;
using WFConFin.Data;
using WFConFin.Models;

namespace WFConFin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly WFConFinDbContext _context;

        public UsuarioController(WFConFinDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                var usuarios = _context.Usuario.ToList();
                return Ok(usuarios);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var listUsuario = _context.Usuario.Where(x => x.Login == usuario.Login).ToList();

                if (listUsuario.Count > 0)
                {
                    return BadRequest("Login já existe!");
                }

                await _context.Usuario.AddAsync(usuario);
                var valor = await _context.SaveChangesAsync();

                if (valor == 1)
                {
                    return Ok("Usuário registrado com sucesso");
                }
                else
                {
                    return BadRequest("Houve um problema ao salvar o usuário");
                }
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUsuario([FromBody] Usuario usuario)
        {
            try
            {
                _context.Usuario.Update(usuario);
                var valor = await _context.SaveChangesAsync();
                if (valor == 1)
                {
                    return Ok("Usuário atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Houve um problema ao atualizar o usuário");
                }
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            try
            {
                var usuario = await _context.Usuario.FindAsync(id);
                if (usuario != null)
                {
                    _context.Usuario.Remove(usuario);
                    var valor = await _context.SaveChangesAsync();
                    if (valor == 1)
                    {
                        return Ok("Usuário deletado com sucesso");
                    }
                    else
                    {
                        return BadRequest("Houve um problema ao deletar o usuário");
                    }
                }
                else
                {
                    return NotFound("Usuário não encontrado");
                }
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}
