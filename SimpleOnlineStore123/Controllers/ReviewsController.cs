using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleOnlineStore123.Data;
using SimpleOnlineStore123.Models.Entities;

namespace SimpleOnlineStore123.Controllers
{
    [Route("SimpleOnlineStore/[controller]")]
    [ApiController]
    public class ReviewsController : Controller
    {
        ApplicationDBContext applicationDBContext;
        public ReviewsController(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews(int productId)
        {
            return await applicationDBContext.Reviews.Where(r => r.ProductId == productId).ToListAsync();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            review.UserId = User.Identity.Name;
            applicationDBContext.Reviews.Add(review);
            await applicationDBContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReviews), new { productId = review.ProductId }, review);
        }
    }
}
