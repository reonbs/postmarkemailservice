namespace Postmark.Email.Service.Services
{
    public class ExecutionResponse<T> where T : class
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int Code { get; set; }
    }
}
