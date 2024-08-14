using clase08.Model;
using clase08.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clase08.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService ser;
        public UsuariosController(IUsuarioService usuarioService)
        {
            ser = usuarioService;
        }

        [HttpGet("Saludar/{name}")]
        public IActionResult Saludar(string name)
        {
            return Ok(name);
        }


        [HttpPost("confirmData")]
        public IActionResult confirmaData([FromBody] Usuario usuario)
        {
            
            
            if (usuario == null)
            {
                BadRequest();
            }
            return Ok(ser.getNombre(usuario!));
        }
        [HttpPost]
        public async Task<IActionResult> addUsuario([FromBody] Usuario usuario)
        {
            
            await ser.addUsuario(usuario);

            return Ok("Ingreso exitoso");
        }

        [HttpPut("{UsuarioPK}")]
        public async Task<IActionResult> update(int UsuarioPK, Usuario usuario)
        {
            await ser.update(UsuarioPK, usuario);
            return Ok("actualización exitosa");
        }

        [HttpDelete("{UsuarioPK}")]
        public async Task<IActionResult> delete(int UsuarioPK)
        {
            await ser.delete(UsuarioPK);
            return Ok("Usuario eliminado");
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var users = await ser.getAll();
            return Ok(users);
        }
        [HttpGet("{UsuarioPK}")]
        public async Task<IActionResult> getByPK(int UsuarioPK)
        {
            var user = await ser.getByPK(UsuarioPK);
            return Ok(user);
        }

    }
}
