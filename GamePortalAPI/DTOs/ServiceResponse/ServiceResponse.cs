using System;
namespace GamePortalAPI.DTOs.ServiceResponse
{
	public class ServiceResponse<T>
	{
		public T? Data { get; set; }
		public string Message { get; set; } = String.Empty;
		public bool IsSuccessful { get; set; } = true;
	}
}

