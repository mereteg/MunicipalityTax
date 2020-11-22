using BusinessLogic.Contract;
using BusinessLogic.Model;
using System.Threading.Tasks;
using System;

namespace BusinessLogic.Engines
{
	public class BusinessMunicipalityTaxScheduleUpdaterEngine : IBusinessMunicipalityTaxScheduleUpdaterEngine
	{
		private readonly IResourceMunicipalityTaxScheduleEngine resourceMunicipalityTaxScheduleEngine;
	
		public BusinessMunicipalityTaxScheduleUpdaterEngine(IResourceMunicipalityTaxScheduleEngine resourceMunicipalityTaxScheduleEngine)
		{
		this.resourceMunicipalityTaxScheduleEngine = resourceMunicipalityTaxScheduleEngine;
		}
		

		public async Task<IUpdateMunicipalityTaxResponse> UpdateMunicipalityTax(IUpdateMunicipalityTaxRequest request)
		{
			// TODO Consider if it should result in validation error instead
			RemoveTuimePartFromDate(request.MunicipalityTaxSchedule);
			bool valid = ValidateTaxSchedule(request.MunicipalityTaxSchedule);
			if (!valid)
				// TODO Consider to granulate illegal input response
				return new UpdateMunicipalityTaxResponse { Status = UpdateMunicipalityTaxStatus.ILLEGAL_INPUT };
			await resourceMunicipalityTaxScheduleEngine.UpdateMunicipalityTaxSchedule(
				new UpdateMunicipalityTaxScheduleRequest { 
					MunicipalityTaxSchedule = request.MunicipalityTaxSchedule 
				});
			return new UpdateMunicipalityTaxResponse { Status = UpdateMunicipalityTaxStatus.OK };
		}

		private void RemoveTuimePartFromDate(IMunicipalityTaxScheduleModel taxSchedule)
		{
			taxSchedule.ValidFrom = taxSchedule.ValidFrom.Date;
			taxSchedule.ValidTo = taxSchedule.ValidTo.Date;
		}
		private bool ValidateTaxSchedule(IMunicipalityTaxScheduleModel taxSchedule)
		{
			if (string.IsNullOrEmpty(taxSchedule.Municipality))
				return false;
			switch (taxSchedule.TaxScheduleType)
			{
				case TaxScheduleType.DAY:
					return taxSchedule.ValidFrom.Equals(taxSchedule.ValidTo);
				case TaxScheduleType.WEEK:
					// Week in Denmark starts on mondays
					if (taxSchedule.ValidFrom.DayOfWeek != DayOfWeek.Monday)
						return false;
					if (taxSchedule.ValidFrom.Date.AddDays(6) != taxSchedule.ValidTo)
						return false;
					return true;
				case TaxScheduleType.MONTH:
					if (taxSchedule.ValidFrom.Day != 1)
						return false;
					if (taxSchedule.ValidFrom.Month != taxSchedule.ValidTo.Month)
						return false;
					if (taxSchedule.ValidFrom.Year != taxSchedule.ValidTo.Year)
						return false;
					// Day after validto should be next month to ensure validto are last day in month
					if (taxSchedule.ValidTo.AddDays(1).Month == taxSchedule.ValidTo.Month)
						return false;
					return true;
				case TaxScheduleType.YEAR:
					if (taxSchedule.ValidFrom.Year != taxSchedule.ValidTo.Year)
						return false;
					int scheduleYear = taxSchedule.ValidFrom.Year;
					if (!taxSchedule.ValidFrom.Equals(new DateTime(scheduleYear, 1, 1)))
						return false;
					if (!taxSchedule.ValidTo.Equals(new DateTime(scheduleYear, 12, 31)))
						return false;
					return true;
				default:
					return false;
			}
		}
	}
}
