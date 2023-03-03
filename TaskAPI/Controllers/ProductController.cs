using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskAPI.Models;
using TaskAPI.Data;
using TaskAPI.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using TaskAPI.DTO;
using Microsoft.EntityFrameworkCore;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IRegistrationRepository _RegistrationRepository;
        private readonly IMapper _mapper;
        string userId;
        public ProductController(IProductRepository ProductRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _ProductRepository = ProductRepository;
            _mapper = mapper;
        

        }

        [Authorize]
        [HttpPost("createProduct")]
        public IActionResult CreateProduct(ProductDTO productdto)
        {
        
          

            productdto.PhotoUrl = productdto.PhotoUrl == "" ? productdto.PhotoUrl = "DefualtImage.png" : productdto.PhotoUrl;
            var product = _mapper.Map<Product>(productdto);
            _ProductRepository.AddAsync(product);
            try
            {

                _ProductRepository.SaveChanges();
            }
            catch
            {
                BadRequest();
            }

            return NoContent();
          
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _ProductRepository.GetAllAsync();

        }

        [Authorize]
        [HttpPost("updateProduct")]
        public async Task<IActionResult> UpdateProduct(int id,Product product)
        {
            if (id != product.id)
            {
                return BadRequest();
            }

            _ProductRepository.UpdateAsync(product);

            try
            {

                _ProductRepository.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!IsProductExists(id))
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
        private bool IsProductExists(int id)
        {
            return _ProductRepository.IsProductExists(id);
        }

        [Authorize]
        [HttpDelete("deleteProduct/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            

          
            var product = _ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _ProductRepository.DeleteAsync(id);

          try
            {

                _ProductRepository.SaveChanges();
            }
            catch
            {
                BadRequest();
            }

            return NoContent();
        }
    }
}
