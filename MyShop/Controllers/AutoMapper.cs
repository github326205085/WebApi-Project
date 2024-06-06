using AutoMapper;
using DTO;
using Entities;

namespace MyShop
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<UserRegisterDTO, User>();

            CreateMap<User, UserGetDTO>();

            CreateMap<UserLoginDTO, User>();
            
            CreateMap<User, UserGetRegisterDTO>().ReverseMap(); 
            CreateMap<User, UserGetUpdateDTO>().ReverseMap();


            CreateMap<Category, CategoryDTO>().ReverseMap();




            CreateMap<Product, ProductDTO>()
                .ForMember
                (catdto => catdto.CategoryName,
                           opts => opts.MapFrom(cat => cat.Category.CategoryName)).ReverseMap();


            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();

            CreateMap<OrderGetDTO, Order>()
                .ForMember(
                dest => dest.OrderDate,
                opts => opts.MapFrom(src =>DateTime.UtcNow))
                .ForMember(
                dest => dest.OrderItems,
                opts => opts.MapFrom(src =>src.orderItemDTOs));

            CreateMap<Order, OrderReturnDTO>();
        }

    }
}
