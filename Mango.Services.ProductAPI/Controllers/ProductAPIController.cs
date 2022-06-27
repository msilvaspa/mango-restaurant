using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers;

[Route("api/products")]
public class ProductApiController: ControllerBase
{
    private ResponseDto response;
    private readonly IProductRepository _productRepository;
    public ProductApiController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        response = new ResponseDto();
    }
    
    [HttpGet]
    public async Task<object> GetAllProducts()
    {
        try
        {
            var productsDto = await _productRepository.GetProducts();
            response.Result = productsDto;
        }
        catch (Exception e)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string>(){e.ToString()};
        }

        return response;
    }
    
    [HttpGet("{id}")]
    public async Task<object> GetProductById(int id)
    {
        try
        {
            var productDto = await _productRepository.GetProductById(id);
            response.Result = productDto;
        }
        catch (Exception e)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string>(){e.ToString()};
        }

        return response;
    }
    
    [HttpPost]
    public async Task<object> CreateProduct([FromBody] ProductDto productDto)
    {
        try
        {
            var product = await _productRepository.CreateUpdateProduct(productDto);
            response.Result = product;
        }
        catch (Exception e)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string>(){e.ToString()};
        }

        return response;
    }
    
    [HttpPut]
    public async Task<object> UpdateProduct([FromBody] ProductDto productDto)
    {
        try
        {
            var product = await _productRepository.CreateUpdateProduct(productDto);
            response.Result = product;
        }
        catch (Exception e)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string>(){e.ToString()};
        }

        return response;
    }
    
    [HttpDelete]
    public async Task<object> DeleteProduct(int id)
    {
        try
        {
            var product = await _productRepository.DeleteProduct(id);
            response.Result = product;
        }
        catch (Exception e)
        {
            response.IsSuccess = false;
            response.ErrorMessage = new List<string>(){e.ToString()};
        }

        return response;
    }
}