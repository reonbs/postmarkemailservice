using System.Collections.Generic;
using System.Threading.Tasks;
using Postmark.Email.Model.Models;
using Postmark.Email.Service.Services;

namespace Postmark.Email.Service.Interfaces
{
    public interface IEmailService
    {
        Task<ExecutionResponse<object>> PublishSingleEmail(EmailRequest emailRequest);
        Task<ExecutionResponse<object>> PublishBatchEmail(List<EmailRequest> emailRequests);
    }
}
