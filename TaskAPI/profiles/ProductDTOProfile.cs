using AutoMapper;
using TaskAPI.DTO;
using TaskAPI.Models;

namespace TaskAPI.profiles
{
    public class ProductDTOProfile : Profile
    {
        public ProductDTOProfile()
        {

            CreateMap<ProductDTO, Product>();
        }

    }
}
