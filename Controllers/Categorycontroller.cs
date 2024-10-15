using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreshCard.Api.Models;

using FreshCart.Api.Database;

namespace FreshCard.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly FreshCartDbContext _context;

        public CategoriesController(FreshCartDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        [Route("api/GetCategories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        [HttpPost]
        [Route("api/CreateCategories")]
        public async Task<Category> PostCategory(Category category)
        {
            if (category.CategoryId !=0)
            {
                _context.Categories.Update(category);
            }
            else
            {
                _context.Categories.Add(category);

            }
            await _context.SaveChangesAsync();

            return category;
        }


        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("{id}")]
        public List<Product> GetCategoryById(int id)
        {
            List<Product> lstProduct = new List<Product>();
            lstProduct =  _context.Products.Where(c=>c.CategoryId == id).ToList();
            if (lstProduct == null)
            {
                //return NotFound();
            }
            return lstProduct;
        }
        [HttpGet]
        [Route("API/GetByCategoryID")]
        public List<Product> GetProductById(int id,string name)
        {
            List<Product> products = new List<Product>();
            products = _context.Products.Where(item => item.ShortName.ToLower().Contains(name.ToLower()) && item.CategoryId == id).ToList();

            if (products == null)
            {
                return null;
            }

            return products;
        }
    }
}
