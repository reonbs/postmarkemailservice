using Postmark.Email.Service.Interfaces;

namespace Postmark.Email.Service.Services
{
    public class ResponseService : IResponseService
    {
        public ExecutionResponse<T> Response<T>(string message, T data = null, bool status = false, int code = 200) where T : class
        {
            return new ExecutionResponse<T>
            {
                Status = status,
                Message = message,
                Data = data,
                Code = code
            };
        }
    }
}
