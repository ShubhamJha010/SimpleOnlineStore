using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleOnlineStore123.Data;
using SimpleOnlineStore123.Models.Entities;

namespace SimpleOnlineStore123.Controllers
{
    [Route("SimpleOnlineStore/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        ApplicationDBContext applicationDBContext;
        public CategoriesController(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await applicationDBContext.Categories.ToListAsync();
        }
      
    }
}
