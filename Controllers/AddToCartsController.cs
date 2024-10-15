using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreshCart.Api.Database;
using FreshCart.Api.Model;
using System.Security.Cryptography.X509Certificates;

namespace FreshCart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddToCartsController : ControllerBase
    {
        private readonly FreshCartDbContext _context;

        public AddToCartsController(FreshCartDbContext context)
        {
            _context = context;
        }

        // GET: api/AddToCarts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddToCart>>> GetAddToCarts()
        {
            return await _context.AddToCarts.ToListAsync();
        }

        // GET: api/AddToCarts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddToCart>> GetAddToCart(int id)
        {
            var addToCart = await _context.AddToCarts.FindAsync(id);

            if (addToCart == null)
            {
                return NotFound();
            }

            return addToCart;
        }

        // PUT: api/AddToCarts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddToCart(int id, AddToCart addToCart)
        {
            if (id != addToCart.CartId)
            {
                return BadRequest();
            }

            _context.Entry(addToCart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddToCartExists(id))
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

        // POST: api/CreateAddToCarts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddToCart>> PostAddToCart(AddToCart addToCart)
        {
            _context.AddToCarts.Add(addToCart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddToCart", new { id = addToCart.CartId }, addToCart);
        }

        // DELETE: api/AddToCarts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddToCart(int id)
        {
            var addToCart = await _context.AddToCarts.FindAsync(id);
            if (addToCart == null)
            {
                return NotFound();
            }

            _context.AddToCarts.Remove(addToCart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddToCartExists(int id)
        {
            return _context.AddToCarts.Any(e => e.CartId == id);
        }

        // GET: api/AddToCarts/ByCustomerId/{customerId}
        [HttpGet("ByCustomerId/{customerId}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetAddToCartByCustomerId(int customerId)
       {
            List<Cart> cart = new List<Cart>();
            var getCardItem = from c in _context.AddToCarts
                              join p in _context.Products on c.ProductId equals p.ProductId
                              join ca in _context.Categories on p.CategoryId equals ca.CategoryId where c.CustId == customerId
                              select new Cart
                              {
                                  CartId = c.CartId,
                                  CustId = c.CustId,
                                  ProductId = c.ProductId,
                                  Quantity = c.Quantity,
                                  ShortName = p.ShortName,
                                  AddedDate = c.AddedDate,
                                  Price = p.Price,
                                  ImageUrl = p.ImageUrl,
                                  CategoryName = ca.CategoryName,
                                  ProductName = p.ProductName
                              };
            foreach (var result in getCardItem)
            {
                cart.Add(result);
            }


            if (cart == null || cart.Count == 0)
            {
                cart = new List<Cart>();
                return cart;
            }
            return cart;
        }


        [HttpDelete("DeleteCartItem/{cartId}")]
        public async Task<ActionResult> DeleteCartItem(int cartId)
        {
            var cartItem = await _context.AddToCarts.FindAsync(cartId);

            if (cartItem == null)
            {
                return NotFound();
            }

            _context.AddToCarts.Remove(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet]
        [Route("api/GetAllSales")]
        public async Task<ActionResult<IEnumerable<Sales>>> GetAllSales()
        {
            List<Sales> sales = new List<Sales>();
            var SalesItem = from c in _context.CustomerRegister
                              join p in _context.PlaceOrders on c.custId equals p.CustId
                              select new Sales
                              {
                                 
                                  CustId = c.custId,
                                  custName = c.custName,
                                  SaleId =  p.SaleId,
                                  SaleDate = p.SaleDate,
                                  TotalInvoiceAmount = p.TotalInvoiceAmount,
                                  DeliveryAddress1 = p.DeliveryAddress1,
                                  DeliveryCity = p.DeliveryCity,
                                  PaymentNaration = p.PaymentNaration,
                              };
            foreach(var sale in SalesItem)
            {
                sales.Add(sale);
            }
            if(sales.Count == 0 && sales ==null)
            {
                sales = new List<Sales>();
                return sales;
            }
            return sales;
        }

    }
}
