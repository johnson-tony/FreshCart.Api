using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreshCart.Api.Database;
using FreshCart.Api.Model;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace FreshCart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerRegistersController : ControllerBase
    {
        private readonly FreshCartDbContext _context;

        public CustomerRegistersController(FreshCartDbContext context)
        {
            _context = context;
        }

        // GET: api/CustomerRegisters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerRegister>>> GetCustomerRegister()
        {
            return await _context.CustomerRegister.ToListAsync();
        }

        // GET: api/CustomerRegisters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerRegister>> GetCustomerRegister(int id)
        {
            var customerRegister = await _context.CustomerRegister.FindAsync(id);

            if (customerRegister == null)
            {
                return NotFound();
            }

            return customerRegister;
        }

        // PUT: api/CustomerRegisters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerRegister(int id, CustomerRegister customerRegister)
        {
            if (id != customerRegister.custId)
            {
                return BadRequest();
            }

            _context.Entry(customerRegister).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerRegisterExists(id))
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

        // POST: api/CustomerRegisters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Registration")]
        public IActionResult PostCustomerRegister(CustomerRegister customerRegister)
        {
            if(customerRegister.phoneNumber != null && customerRegister.password != null)
            {
                var result =  _context.CustomerRegister.Where(c=>c.phoneNumber == customerRegister.phoneNumber).FirstOrDefault();
                if (result == null)
                {
                    _context.CustomerRegister.Add(customerRegister);
                    _context.SaveChanges();
                    return CreatedAtAction("GetCustomerRegister", new { id = customerRegister.custId }, customerRegister);
                }
            }
            return Ok("failure");
        }

        // DELETE: api/CustomerRegisters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerRegister(int id)
        {
            var customerRegister = await _context.CustomerRegister.FindAsync(id);
            if (customerRegister == null)
            {
                return NotFound();
            }

            _context.CustomerRegister.Remove(customerRegister);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerRegisterExists(int id)
        {
            return _context.CustomerRegister.Any(e => e.custId == id);
        }
        [HttpGet]
        [Route("CheckCredencial")]
        public IActionResult CheckCredencial(long PhoneNumber , string Password)
        {
            CustomerRegister customerRegister = new CustomerRegister(); 
            if (PhoneNumber != null && Password != null)
            {
                customerRegister = _context.CustomerRegister.Where(c=>c.phoneNumber ==  PhoneNumber && c.password == Password).FirstOrDefault();
                if(customerRegister != null)
                {
                    return Ok(customerRegister);
                }
            }
            return BadRequest();

        }
    }
}
