namespace Shop.WebApi.Common.Models
{
    public interface IApiResponse
    {
        bool Success { get; }
    }

    public class ApiResponse : IApiResponse
    {
        public bool Success { get; }

        public ApiResponse(bool success)
        {
            this.Success = success;
        }
    }
}
