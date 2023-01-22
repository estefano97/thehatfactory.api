using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thehatfactory.api.DTOs;
using thehatfactory.api.DTOs.Request;
using thehatfactory.api.Models;

namespace thehatfactory.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly the_hat_factoryContext _context;

        public ProductsController(the_hat_factoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery]GorrasRequest req)
        {
            try
            {
                var response = await _context.Productos.ToListAsync();

                if (req.Liga != null) response = response.Where(el => el.Liga.ToUpper() == req.Liga.ToUpper()).ToList();

                if (req.Nombre != null) response = response.Where(el => el.ProductName.ToUpper().Contains(req.Nombre.ToUpper())).ToList();

                if (req.OrderBy == "price")
                {
                    response = response.OrderBy(el => el.Precio).ToList();
                } 
                else if(req.OrderBy == "a-z")
                {
                    response = response.OrderByDescending(el => el.ProductName).ToList();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


        [HttpGet]
        [Route("getById")]
        public async Task<ActionResult> GetAll([FromQuery]Guid id)
        {
            try
            {
                var response = await _context.Productos.Where(el => el.Id == id).FirstOrDefaultAsync();

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
