using Postmark.Email.Service.Services;

namespace Postmark.Email.Service.Interfaces
{
    public interface IResponseService
    {
        ExecutionResponse<T> Response<T>(string message, T data = null, bool status = false, int code = 200) where T : class;
    }
}
