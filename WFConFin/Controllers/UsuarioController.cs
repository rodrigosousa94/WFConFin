using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WFConFin.Data;
using WFConFin.Models;
using WFConFin.Services;

namespace WFConFin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly WFConFinDbContext _context;
        private readonly TokenService _service;

        public UsuarioController(WFConFinDbContext context, TokenService service)
        {
            _context = context;
            _service = service;
        }


        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UsuarioLogin usuarioLogin)
        {
            try
            {
                var usuario = _context.Usuario
                   .Where(x => x.Login == usuarioLogin.Login).FirstOrDefault();

                if (usuario == null)
                {
                    return NotFound("Usuario não encontrado");
                }

                if (usuario.Password != usuarioLogin.Password)
                {
                    return BadRequest("Senha inválida");
                }

                var token = _service.GerarToken(usuario);

                usuario.Password = "";

                var result = new UsuarioResponse
                {
                    Usuario = usuario,
                    Token = token
                };

                return Ok(result);

            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
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
        [Authorize(Roles = "Gerente,Empregado")]
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
        [Authorize(Roles = "Gerente,Empregado")]
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
        [Authorize(Roles = "Gerente")]
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
