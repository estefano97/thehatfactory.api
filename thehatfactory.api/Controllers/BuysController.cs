using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using thehatfactory.api.DTOs;
using thehatfactory.api.Models;

namespace thehatfactory.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuysController : ControllerBase
    {
        private readonly the_hat_factoryContext _context;

        public BuysController(the_hat_factoryContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> PostCompra([FromBody]CompraDTO element)
        {
            try
            {
                var data = new Compra();
                data.Id = Guid.NewGuid();
                data.FechaCompra = DateTime.Now;
                data.IdPaypal = element.IdPaypal;
                data.TotalValue = element.TotalValue;
                data.IdUsuarioCompra = element.IdUsuarioCompra;

                var products = element.ProductoCompra;

                var savedProducts = new List<ProductoCompra>();

                products.ForEach(el =>
                {
                    var producto = new ProductoCompra();
                    producto.Id = Guid.NewGuid();
                    producto.ProductTalla = el.ProductTalla;
                    producto.Price = el.Price;
                    producto.CompraId = data.Id;
                    producto.ProductoId = el.ProductId;

                    savedProducts.Add(producto);
                });

                await _context.Compras.AddAsync(data);
                await _context.ProductoCompras.AddRangeAsync(savedProducts);

                await _context.SaveChangesAsync();

                var usuario = await _context.Usuarios.Where(el => el.Email == el.Email).FirstOrDefaultAsync();

                var texto = $"<h1>COMPRA REALIZADA CON EXITO POR {usuario.NombreCompleto}<h1> \n";
                texto += $"<h2>correo: {usuario.Email}<h2> \n";
                texto += $"<h2>telefono: {usuario.Telefono}<h2> \n";
                texto += $"<h2>id compra (PayPal): {element.IdPaypal}<h2> \n";
                texto += $"<h2>Productos Comprados<h2> \n\n";
                element.ProductoCompra.ForEach(el => {
                    var producto = _context.Productos.Where(el => el.Id == el.Id).FirstOrDefault();
                texto += $"<p><b>Producto comprado: </b> {producto.ProductName}<p> \n";
                texto += $"<p><b>Talla: </b> {el.ProductTalla}<p> \n";
                texto += $"<p><b>Precio comprado: </b> ${el.Price}<p> \n";
                texto += $"----------------------------------------------";

                });

                MailMessage oMailMessage = new MailMessage("thfbusiness22@gmail.com", "thehatfactorybycuatro@gmail.com", "COMPRA REGISTRADA", texto);

                oMailMessage.IsBodyHtml = true;

                SmtpClient oSmtpClient =  new SmtpClient("smtp.gmail.com");
                oSmtpClient.UseDefaultCredentials = false;
                oSmtpClient.EnableSsl = true;
                oSmtpClient.Host = "smtp.gmail.com";
                oSmtpClient.Port = 587;
                oSmtpClient.Credentials = new NetworkCredential("thfbusiness22@gmail.com", "cplckwuhyiqjkbzj");

                oSmtpClient.Send(oMailMessage);

                oSmtpClient.Dispose();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("GetAll")]
        public async Task<ActionResult> GetAllCompras([FromBody] LoginDTO data)
        {
            try
            {
                var user = await _context.Usuarios.Where(el => el.Email == data.Email && el.UserPassword == data.Password).FirstOrDefaultAsync();

                var datos = await _context.Compras.AsNoTracking().IgnoreAutoIncludes().Include(x => x.ProductoCompras).ThenInclude(x => x.Producto)
                    .Where(el => el.IdUsuarioCompra == user.Id).ToListAsync();

                return Ok(datos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
