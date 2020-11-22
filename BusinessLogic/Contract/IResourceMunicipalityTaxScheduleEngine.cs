using System.Threading.Tasks;

namespace BusinessLogic.Contract
{
	public interface IResourceMunicipalityTaxScheduleEngine
	{
		Task<IRetríeveMunicipalityTaxSchedulesResponse> RetríeveMunicipalityTaxSchedules(IRetríeveMunicipalityTaxSchedulesRequest request);
		Task UpdateMunicipalityTaxSchedule(IUpdateMunicipalityTaxScheduleRequest request);
	}
}
