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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly FarmAppDBContext dbContext;

        public ProductsController(FarmAppDBContext context)
        {
            dbContext = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductP>>> GetProducts()
        {
            return await dbContext.Products
                .Select(s => new ProductP
                {
                    Id = s.IdProduct,
                    Name = s.ProductName,
                    Mg = s.Mg,
                    AdministrationForm = s.IdAdministrationFormNavigation.AdministrationName,
                    AdministrationVia = s.IdAdministrationFormNavigation.AdministrationVia
                })
                .ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("search")]
        public async Task<ActionResult<List<ProductP>>> SearchProduct(string name)
        {

            var prods = await dbContext.Products
                .Where(s => s.ProductName.Contains(name))
                .Select(s => new ProductP
                {
                    Id = s.IdProduct,
                    Name = s.ProductName,
                    Mg = s.Mg,
                    AdministrationForm = s.IdAdministrationFormNavigation.AdministrationName,
                    AdministrationVia = s.IdAdministrationFormNavigation.AdministrationVia
                })
                .ToListAsync();

            if (prods == null)
            {
                return NotFound();
            }

            return prods;
        }

        // GET: api/Products/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductP>> GetProduct(int id)
        {
            var product = await dbContext.Products
                .Where(s => s.IdProduct == id)
                .Select(s => new ProductP
                {
                    Id = s.IdProduct,
                    Name = s.ProductName,
                    Mg = s.Mg,
                    AdministrationForm = s.IdAdministrationFormNavigation.AdministrationName,
                    AdministrationVia = s.IdAdministrationFormNavigation.AdministrationVia
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // GET: api/Products/5
        [HttpGet("{name}")]
        public async Task<ActionResult<ProductP>> GetProductByName(string name)
        {
            var product = await dbContext.Products
                .Where(p => p.ProductName == name)
                .Select(s => new ProductP
                {
                    Id = s.IdProduct,
                    Name = s.ProductName,
                    Mg = s.Mg,
                    AdministrationForm = s.IdAdministrationFormNavigation.AdministrationName,
                    AdministrationVia = s.IdAdministrationFormNavigation.AdministrationVia
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }



        // GET: api/Products/5
        [HttpGet("{id:int}/pharmacies")]
        public async Task<ActionResult<IEnumerable<Pharmacy>>> GetProductPharmacies(int id)
        {

            var productPharmacies = await dbContext.PharmacyProducts.Where(s => s.IdProduct == id)
                .Select(s => new Pharmacy
                {
                    Id = s.IdPharmacyBranch,
                    Name = s.IdPharmacyBranchNavigation.PharmacyName,
                    PharmacyChain = s.IdPharmacyBranchNavigation.IdPharmacyChainNavigation.PharmacyName,
                    PhoneNumber = s.IdPharmacyBranchNavigation.PhoneNumber,
                    Address = s.IdPharmacyBranchNavigation.AddressName,
                    Latitude = s.IdPharmacyBranchNavigation.Latitude,
                    Longitude = s.IdPharmacyBranchNavigation.Longitude

                }).ToListAsync();

            if (productPharmacies == null)
            {
                return NotFound();
            }

            return productPharmacies;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.IdProduct)
            {
                return BadRequest();
            }

            dbContext.Entry(product).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.IdProduct }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return dbContext.Products.Any(e => e.IdProduct == id);
        }
    }
}
