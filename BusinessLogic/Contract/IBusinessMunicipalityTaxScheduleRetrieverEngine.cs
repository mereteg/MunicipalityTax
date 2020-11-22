using System.Threading.Tasks;

namespace BusinessLogic.Contract
{
	public interface IBusinessMunicipalityTaxScheduleRetrieverEngine
	{
		Task<IRetríeveMunicipalityTaxResponse> RetríeveMunicipalityTax(IRetríeveMunicipalityTaxRequest request);
	}
}
