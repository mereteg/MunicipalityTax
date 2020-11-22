using BusinessLogic.Contract;

namespace BusinessLogic.Model
{
	public class UpdateMunicipalityTaxScheduleRequest : IUpdateMunicipalityTaxScheduleRequest
	{
		public IMunicipalityTaxScheduleModel MunicipalityTaxSchedule { get ; set; }
	}
}
