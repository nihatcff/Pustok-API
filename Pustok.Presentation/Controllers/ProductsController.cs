using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pustok.Business.Dtos;
using Pustok.Business.Services.Abstractions;
using System.Threading.Tasks;

namespace Pustok.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService _productService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto dto)
        {
            await _productService.CreateAsync(dto);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateDto dto)
        {
            await _productService.UpdateAsync(dto);
            return Ok();
        }





        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }
    }
}
