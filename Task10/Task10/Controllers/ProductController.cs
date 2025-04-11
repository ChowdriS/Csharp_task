using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task10.Data;
using Task10.DTO;
using Task10.Interface;
using Task10.Model;

namespace Task10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService service;

        public ProductController(IProductService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await service.GetAllProductsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await service.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // for testing exception middleware
        //[HttpGet("throw")]
        //public IActionResult Throw()
        //{
        //    throw new Exception("Simulated exception for testing.");
        //}


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto dto)
        {
            var created = await service.CreateProductAsync(dto);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductDto dto)
        {
            var updated = await service.UpdateProductAsync(id, dto);
            if (updated == null) return NotFound("Product not found");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await service.DeleteProductAsync(id);
            if (!result) return NotFound("Product not found");
            return Ok("Deleted successfully");
        }
    }
}
