using AutoMapper;
using Store.APIs.DTOs;
using Store.Core.Entities;
using Store.Core.Entities.Identity;
using Store.Core.Order_Aggragate;


namespace Store.APIs.Helper
{
	public class Mapping_Profles :Profile
	{
        public Mapping_Profles()
        {
            CreateMap<Product, ProductToReturnDtos>()
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<PictureUrlResolver>());

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto, Core.Order_Aggragate.Address>();
            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto, BasktItem>().ReverseMap();
            CreateMap<Order,OrderToRetuernDto>()
                .ForMember(d=>d.DeliveryMethod,o=>o.MapFrom(s=>s.DeliveryMethod.ShortName))
                .ForMember(d=>d.DeliveryMethodCost,o=>o.MapFrom(s=>s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.product.Id))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.product.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrdreItemPictureResolver>());
                
        }

    }
}
