using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreshCart.Api.Database;
using FreshCart.Api.Model;

namespace FreshCart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceOrdersController : ControllerBase
    {
        private readonly FreshCartDbContext _context;

        public PlaceOrdersController(FreshCartDbContext context)
        {
            _context = context;
        }

        // GET: api/PlaceOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceOrder>>> GetPlaceOrders()
        {
            return await _context.PlaceOrders.ToListAsync();
        }

        // GET: api/PlaceOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceOrder>> GetPlaceOrder(int id)
        {
            var placeOrder = await _context.PlaceOrders.FindAsync(id);

            if (placeOrder == null)
            {
                return NotFound();
            }

            return placeOrder;
        }

        // PUT: api/PlaceOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaceOrder(int id, PlaceOrder placeOrder)
        {
            if (id != placeOrder.SaleId)
            {
                return BadRequest();
            }

            _context.Entry(placeOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceOrderExists(id))
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

        // POST: api/PlaceOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("PostPlaceOrder")]
        public async Task<ActionResult<PlaceOrder>> PostPlaceOrder(PlaceOrder placeOrder)
        {
            _context.PlaceOrders.Add(placeOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlaceOrder", new { id = placeOrder.SaleId }, placeOrder);
        }

        // DELETE: api/PlaceOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaceOrder(int id)
        {
            var placeOrder = await _context.PlaceOrders.FindAsync(id);
            if (placeOrder == null)
            {
                return NotFound();
            }

            _context.PlaceOrders.Remove(placeOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlaceOrderExists(int id)
        {
            return _context.PlaceOrders.Any(e => e.SaleId == id);
        }

     
    }
}
