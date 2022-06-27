using AutoMapper;
using Mango.Services.ProductAPI.Models;

namespace Mango.Services.ProductAPI;

public static class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.CreateMap<ProductDto, Product>();
            mc.CreateMap<Product, ProductDto>();
        });
        return mappingConfig;
    }
}