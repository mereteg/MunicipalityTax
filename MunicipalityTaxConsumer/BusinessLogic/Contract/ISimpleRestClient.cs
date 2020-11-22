using System.Threading.Tasks;

namespace BusinessLogic.Contract
{
    public interface ISimpleRestClient
    {
        Task<IRestServiceResponse> InvokeServiceAsync(IRestServiceRequest request);

    }
}
