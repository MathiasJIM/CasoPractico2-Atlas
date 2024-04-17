using Abstracciones.BW;

using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : Controller
    {
        public IAutenticacionBW _AutenticacionBW;

        public AutenticacionController(IAutenticacionBW autenticacionBW)
        {
            _AutenticacionBW = autenticacionBW;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Post([FromBody] LoginAutenticar loginRequest)
        {
            return Ok(_AutenticacionBW.Login(loginRequest));
        }

        [HttpGet]
        [Route("Validar")]
        public IActionResult GetValidarAutenticacion()
        {
            return Ok("Token Valido");
        }

        [HttpGet("ObtenerUsuarioPorCorreo")]
        public async Task<ActionResult<Login>> ObtenerUsuarioPorCorreo(string correoElectronico)
        {
            var usuario = await _AutenticacionBW.ObtenerUsuario(new Login() { Email = correoElectronico });
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

    }
}
