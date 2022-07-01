using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers;

[Route("api/products")]
public class ProductApiController: ControllerBase
{
    private ResponseDto _response;
    private readonly IProductRepository _productRepository;
    public ProductApiController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        _response = new ResponseDto();
    }
    
    [HttpGet]
    public async Task<object> GetAllProducts()
    {
        try
        {
            var productsDto = await _productRepository.GetProducts();
            _response.Result = productsDto;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>(){e.ToString()};
        }

        return _response;
    }
    
    [HttpGet("{id}")]
    public async Task<object> GetProductById(int id)
    {
        try
        {
            var productDto = await _productRepository.GetProductById(id);
            _response.Result = productDto;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>(){e.ToString()};
        }
        Console.WriteLine(_response.Result);
        return _response;
    }
    
    [HttpPost]
    public async Task<object> CreateProduct([FromBody] ProductDto productDto)
    {
        try
        {
            var product = await _productRepository.CreateUpdateProduct(productDto);
            _response.Result = product;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>(){e.ToString()};
        }

        return _response;
    }
    
    [HttpPut]
    public async Task<object> UpdateProduct([FromBody] ProductDto productDto)
    {
        try
        {
            var product = await _productRepository.CreateUpdateProduct(productDto);
            _response.Result = product;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = new List<string>(){e.ToString()};
        }

        return _response;
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<object> Delete(int id)
    {
        try
        {
            bool isSuccess = await _productRepository.DeleteProduct(id);
            _response.Result = isSuccess;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage
                = new List<string>() { ex.ToString() };
        }
        return _response;
    }
}