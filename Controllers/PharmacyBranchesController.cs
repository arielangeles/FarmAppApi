using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmAppApi.Models;
using FarmAppApi.DTO;

namespace FarmAppApi.Controllers
{
    [Route("api/pharmacies")]
    [ApiController]
    public class PharmacyBranchesController : ControllerBase
    {
        private readonly FarmAppDBContext dbContext;

        public PharmacyBranchesController(FarmAppDBContext context)
        {
            dbContext = context;
        }

        // GET: api/PharmacyBranches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PharmacyBranch>>> GetPharmacyBranches()
        {
            return await dbContext.PharmacyBranches.ToListAsync();
        }
        
        // GET: api/PharmacyBranches/5

        [HttpGet("{id:int}/products")]
        public async Task<ActionResult<List<ProductPharmacy>>> GetPharmacyBranchProducts(int id)
        {
            var pharmacyProducts = await dbContext.PharmacyProducts.Where(s => s.IdPharmacyBranch == id)
                .Select(s => new ProductPharmacy
                {
                    Id = s.IdProductNavigation.IdProduct,
                    Name = s.IdProductNavigation.ProductName,
                    Mg = s.IdProductNavigation.Mg,
                    AdministrationForm = s.IdProductNavigation.IdAdministrationFormNavigation.AdministrationName,
                    AdministrationVia = s.IdProductNavigation.IdAdministrationFormNavigation.AdministrationVia,
                    Quantity = s.Quantity,
                    Price = s.IdProductNavigation.Price

                }).ToListAsync();

            if (pharmacyProducts == null)
            {
                return NotFound();
            }

            return pharmacyProducts;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pharmacy>> GetPharmacyBranch(int id)
        {

            var pharmacy = await dbContext.PharmacyBranches
                .Where(s => s.IdPharmacyBranch == id)
                .Select(p => new Pharmacy
                {
                    Id = p.IdPharmacyBranch,
                    Name = p.PharmacyName,
                    PharmacyChain = p.IdPharmacyChainNavigation.PharmacyName,
                    Address = p.AddressName,
                    PhoneNumber = p.PhoneNumber,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                })
               .FirstOrDefaultAsync();

            if (pharmacy == null)
            {
                return NotFound();
            }

            return pharmacy;
        }

        // PUT: api/PharmacyBranches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPharmacyBranch(int id, PharmacyBranch pharmacyBranch)
        {
            if (id != pharmacyBranch.IdPharmacyBranch)
            {
                return BadRequest();
            }

            dbContext.Entry(pharmacyBranch).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PharmacyBranchExists(id))
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

        // POST: api/PharmacyBranches
        [HttpPost]
        public async Task<ActionResult<PharmacyBranch>> PostPharmacyBranch(PharmacyBranch pharmacyBranch)
        {
            dbContext.PharmacyBranches.Add(pharmacyBranch);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction("GetPharmacyBranch", new { id = pharmacyBranch.IdPharmacyBranch }, pharmacyBranch);
        }

        // DELETE: api/PharmacyBranches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePharmacyBranch(int id)
        {
            var pharmacyBranch = await dbContext.PharmacyBranches.FindAsync(id);
            if (pharmacyBranch == null)
            {
                return NotFound();
            }

            dbContext.PharmacyBranches.Remove(pharmacyBranch);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool PharmacyBranchExists(int id)
        {
            return dbContext.PharmacyBranches.Any(e => e.IdPharmacyBranch == id);
        }
    }
}
