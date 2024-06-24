using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleOnlineStore123.Data;
using SimpleOnlineStore123.Models.Entities;
using SimpleOnlineStore123.Data;
using SimpleOnlineStore123.Models.Entities;

namespace SimpleOnlineStore123.Controllers
{
    [Route("SimpleOnlineStore/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        public readonly ApplicationDBContext applicationDBContext;
        public ProductsController(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] int? categoryId = null)
        {
            var query = applicationDBContext.Products.AsQueryable();
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }
            var products = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await applicationDBContext.Products.Include(p => p.Reviews).FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            applicationDBContext.Entry(product).State = EntityState.Modified;
            try
            {
                await applicationDBContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await applicationDBContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            applicationDBContext.Products.Remove(product);
            await applicationDBContext.SaveChangesAsync();
            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return applicationDBContext.Products.Any(e => e.ProductId == id);
        }

    }
}
