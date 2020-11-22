using System.Threading.Tasks;

namespace BusinessLogic.Contract
{
	public interface IBusinessMunicipalityTaxScheduleUpdaterEngine
	{
		Task<IUpdateMunicipalityTaxResponse> UpdateMunicipalityTax(IUpdateMunicipalityTaxRequest request);
	}
}
