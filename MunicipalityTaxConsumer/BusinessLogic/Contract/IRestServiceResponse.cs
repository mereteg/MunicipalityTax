using System.Net;

namespace BusinessLogic.Contract
{
    public interface IRestServiceResponse
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Content { get; set; }
    }
}
