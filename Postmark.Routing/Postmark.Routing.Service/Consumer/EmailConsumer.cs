using System;
using System.Threading.Tasks;
using MassTransit;
using Postmark.Email.Model.Models;

namespace Postmark.Routing.Service.Consumer
{
    public class EmailConsumer : IConsumer<EmailRequest>
    {
        public async Task Consume(ConsumeContext<EmailRequest> context)
        {
            //if the email is qualified based on the logic i will publish the message using the qualified queue.
            //the qualifies message will be send off to the recipients

            //if the email is disqualified based on the login i will publish the message usingthe disqaulified queue.
            //the sender will the notified of the disqualified messages
        }
    }
}
