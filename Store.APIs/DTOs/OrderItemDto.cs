﻿namespace Store.APIs.DTOs
{
	public class OrderItemDto
	{
		public int Id { get; set; }
		public string ProductName { get; set; }
		public string PictureUrl { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}
}