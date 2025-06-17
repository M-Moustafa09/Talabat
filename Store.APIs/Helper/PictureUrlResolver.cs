using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Store.APIs.DTOs;
using Store.Core.Entities;

namespace Store.APIs.Helper
{
	public class PictureUrlResolver : IValueResolver<Product, ProductToReturnDtos, string>
	{
		private readonly IConfiguration _configuration;

		public PictureUrlResolver(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public string Resolve(Product source, ProductToReturnDtos destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
			{
				return $"{_configuration["PectureUrl"]}{source.PictureUrl}";
			}
			return string.Empty;
		}
	}
}
