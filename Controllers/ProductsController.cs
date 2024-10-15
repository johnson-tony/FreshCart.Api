using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreshCard.Api.Models;
using FreshCart.Api.Database;

namespace FreshCart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly FreshCartDbContext _context;

        public ProductsController(FreshCartDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpGet]
        [Route("GetProductByName/{name}")]
        public List<Product>GetProductByName(string name)
        {
            List<Product> products = new List<Product>();
             products =  _context.Products.Where(item => item.ShortName.ToLower().Contains(name.ToLower())).ToList();

            if (products == null)
            {
                return null;
            }

            return products;
        }

        
        // GET: api/Products/Category/5
        [HttpGet("Category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryId(int categoryId)
        {
            var productsInCategory = await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();

            if (!productsInCategory.Any())
            {
                return NotFound();
            }

            return productsInCategory;
        }




        // PUT: api/allProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // PUT: api/Products
        [HttpPut]
        public async Task<IActionResult> PutProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                _context.Entry(product).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is Product)
                    {
                        var product = (Product)entry.Entity;
                        if (!ProductExists(product.ProductId))
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        throw new NotSupportedException($"Entity type {entry.Entity.GetType().FullName} not supported for update");
                    }
                }
                throw;
            }
        }





        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if(product.ProductId != 0)
            {
                _context.Products.Update(product);
            }
            else
            {
                _context.Products.Add(product);
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
