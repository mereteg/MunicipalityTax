using BusinessLogic.Contract;
using System.Net;

namespace ResourceAccess.Model
{
	public class RestServiceResponse : IRestServiceResponse
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Content { get; set; }
    }
}
