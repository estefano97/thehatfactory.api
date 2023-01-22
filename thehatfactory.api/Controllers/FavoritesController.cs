using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thehatfactory.api.DTOs.Request;
using thehatfactory.api.Models;

namespace thehatfactory.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly the_hat_factoryContext _context;

        public FavoritesController(the_hat_factoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetFavoritesByUser([FromQuery]Guid idUser)
        {
            try
            {
                var response = await _context.FavoritosUsuarios.Where(el => el.IdUsuario == idUser)
                    .Include(el => el.IdProductoNavigation).IgnoreAutoIncludes()
                    .ToListAsync();

                response.ForEach(el =>
                {
                    el.IdProductoNavigation.FavoritosUsuarios = null;
                });

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddFavoriteProduct([FromBody]FavoriteRequest req)
        {
            try
            {
                var element = new FavoritosUsuario();
                element.Id = Guid.NewGuid();
                element.IdUsuario = req.idUser;
                element.IdProducto = req.idProduct;

                await _context.FavoritosUsuarios.AddAsync(element);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        [Route("remove")]
        public async Task<ActionResult> DeleteFavoriteProduct([FromBody] FavoriteRequest req)
        {
            try
            {
                var element = await _context.FavoritosUsuarios
                    .Where(el => el.IdProducto == req.idProduct && el.IdUsuario == req.idUser).FirstOrDefaultAsync();

                if (element == null) return NotFound();

                _context.FavoritosUsuarios.Remove(element);

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
