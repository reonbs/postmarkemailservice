using System.ComponentModel.DataAnnotations;

namespace Postmark.Email.Model.Models
{
    public class EmailRequest
    {
        [EmailAddress]
        public string From { get; set; }
        [EmailAddress]
        public string To { get; set; }
        [EmailAddress]
        public string Subject { get; set; }
        [EmailAddress]
        public string Body { get; set; }
    }
}
