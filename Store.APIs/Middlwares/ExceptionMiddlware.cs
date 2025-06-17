using AutoMapper.Configuration;
using Store.APIs.Errors;
using System.Text.Json;

namespace Store.APIs.Middlwares
{
	public class ExceptionMiddlware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddlware> _logger;
		private readonly IHostEnvironment _env;

		public ExceptionMiddlware(RequestDelegate Next ,ILogger<ExceptionMiddlware> logger,IHostEnvironment env)
        {
			_next = Next;
			_logger = logger;
			_env = env;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
					await _next.Invoke(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = 500;
				//if(_env.IsDevelopment())
				//{
				//	var response = new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString());
				//}
				//else
				//{
				//	var response = new ApiExceptionResponse(500);
				//}
				var response =_env.IsDevelopment()?new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString()): new ApiExceptionResponse(500);
				var jsonResopnse = JsonSerializer.Serialize(response);
				context.Response.WriteAsync(jsonResopnse);
			}
			

		}
		
	}
}
