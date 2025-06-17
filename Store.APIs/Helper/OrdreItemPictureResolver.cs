using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.APIs.DTOs;
using Store.Core.Order_Aggragate;

namespace Store.APIs.Helper
{
	public class OrdreItemPictureResolver : IValueResolver<OrderItem, OrderItemDto, string>
	{
		private readonly IConfiguration _configuration;
		public OrdreItemPictureResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.product.PictureUrl))
			{
				return $"{_configuration["PectureUrl"]}{source.product.PictureUrl}";
			}
			return string.Empty;
		}
	}
}
