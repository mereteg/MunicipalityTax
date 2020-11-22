using BusinessLogic.Contract;

namespace ServiceInterface.Model
{
	public class UpdateMunicipalityTaxRequest : IUpdateMunicipalityTaxRequest
	{
		public IMunicipalityTaxScheduleModel MunicipalityTaxSchedule { get; set; }
	}
}
