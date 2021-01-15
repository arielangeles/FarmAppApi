using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmAppApi.Models;

namespace FarmAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyProductsController : ControllerBase
    {
        private readonly FarmAppDBContext dbContext;

        public PharmacyProductsController(FarmAppDBContext context)
        {
            dbContext = context;
        }

        // GET: api/PharmacyProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PharmacyProduct>>> GetPharmacyProducts()
        {
            return await dbContext.PharmacyProducts
                .Include(pp => pp.IdProductNavigation)
                .ToListAsync();
        }

        // GET: api/PharmacyProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PharmacyProduct>> GetPharmacyProduct(int id)
        {
            var pharmacyProduct = await dbContext.PharmacyProducts.FindAsync(id);

            var pharmacy = await dbContext.PharmacyProducts
                .Include(pp => pp.IdPharmacyBranchNavigation)
                .Include(pp => pp.IdProductNavigation)
                .SingleAsync(pp => pp.IdPharmacyProduct == id);

            if (pharmacyProduct == null)
            {
                return NotFound();
            }

            return pharmacy;
        }



        // PUT: api/PharmacyProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPharmacyProduct(int id, PharmacyProduct pharmacyProduct)
        {
            if (id != pharmacyProduct.IdPharmacyProduct)
            {
                return BadRequest();
            }

            dbContext.Entry(pharmacyProduct).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PharmacyProductExists(id))
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

        // POST: api/PharmacyProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PharmacyProduct>> PostPharmacyProduct(PharmacyProduct pharmacyProduct)
        {
            dbContext.PharmacyProducts.Add(pharmacyProduct);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction("GetPharmacyProduct", new { id = pharmacyProduct.IdPharmacyProduct }, pharmacyProduct);
        }

        // DELETE: api/PharmacyProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePharmacyProduct(int id)
        {
            var pharmacyProduct = await dbContext.PharmacyProducts.FindAsync(id);
            if (pharmacyProduct == null)
            {
                return NotFound();
            }

            dbContext.PharmacyProducts.Remove(pharmacyProduct);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool PharmacyProductExists(int id)
        {
            return dbContext.PharmacyProducts.Any(e => e.IdPharmacyProduct == id);
        }
    }
}
