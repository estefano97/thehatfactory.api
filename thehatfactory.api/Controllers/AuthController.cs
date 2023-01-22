using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thehatfactory.api.DTOs;
using thehatfactory.api.Models;

namespace thehatfactory.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly the_hat_factoryContext _context;

        public AuthController(the_hat_factoryContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO data)
        {
            try
            {
                var datos = await _context.Usuarios
                .Where(el => el.Email == data.Email && el.UserPassword == data.Password)
                .FirstOrDefaultAsync();

                return Ok(datos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO data)
        {
            try
            {
                var user = new Usuario();
                user.Email = data.Email;
                user.UserPassword = data.UserPass;
                user.NombreCompleto = data.NombreCompleto;
                user.Telefono = data.Telefono;

                await _context.Usuarios.AddAsync(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("changePassword")]
        public async Task<ActionResult>ChangePassword([FromBody]RestartPasswordDTO data)
        {
            try
            {

                var user = await _context.Usuarios.Where(el => el.Email == data.Email && el.UserPassword == data.Password).FirstOrDefaultAsync();

                user.UserPassword = data.NewPassword;

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
