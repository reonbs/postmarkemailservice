using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Postmark.Email.Model.Models;
using Postmark.Email.Service.Interfaces;

namespace Postmark.Email.Service.Services
{
    public class EmailService : IEmailService
    {
        #region declares
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<EmailRequest> _logger;
        private readonly IResponseService _responseService;
        #endregion

        public EmailService(IPublishEndpoint publishEndpoint, ILogger<EmailRequest> logger, IResponseService responseService)
        {
            _responseService = responseService;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<ExecutionResponse<object>> PublishSingleEmail(EmailRequest emailRequest)
        {
            try
            {
                if (emailRequest == null)
                    return _responseService.Response<object>("invalid request",code:400);

                if(string.IsNullOrWhiteSpace(emailRequest.From))
                    return _responseService.Response<object>("from email is required", code: 400);

                await _publishEndpoint.Publish(emailRequest);

                return _responseService.Response<object>("email sent",null,true);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "an error occurred while publishing a single email");
                return _responseService.Response<object>("an error occurred while publishing a single email", null,code: 500);
            }
        }

        public async Task<ExecutionResponse<object>> PublishBatchEmail(List<EmailRequest> emailRequests)
        {
            try
            {
                if (!emailRequests.Any())
                    return _responseService.Response<object>("invalid request", code: 400);

                foreach (var emailRequest in emailRequests.Where(emailRequest => !string.IsNullOrWhiteSpace(emailRequest.From)))
                {
                    await _publishEndpoint.Publish(emailRequest);
                }

                return _responseService.Response<object>("email's sent",null, true);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "an error occurred while publishing batch email");
                return _responseService.Response<object>("An error occurred while sending batch email", null, code:500);
            }
        }
    }
}
