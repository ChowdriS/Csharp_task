using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task10.Data;
using Task10.Model;

namespace Task10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext db;
        public ProductController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await db.Product.ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product NewProduct)
        {
            await db.Product.AddAsync(NewProduct);
            await db.SaveChangesAsync();
            return Ok("Successfully added");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            var oldProduct = await db.Product.FindAsync(id);
            if (oldProduct == null) 
            {
                return NotFound("Id is not valid");    
            }
            oldProduct.Name = product.Name;
            oldProduct.Quantity = product.Quantity;
            oldProduct.Quantity = product.Quantity;

            await db.SaveChangesAsync();

            return Ok(oldProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var oldProduct = await db.Product.FindAsync(id);
            if (oldProduct == null) return NotFound("Id is not valid");

            db.Product.Remove(oldProduct);
            await db.SaveChangesAsync();
            return Ok("Successfully Deleted");
        }


    }
}
