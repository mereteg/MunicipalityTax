using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Contract
{
	public interface IRetríeveMunicipalityTaxSchedulesResponse
	{
		IEnumerable<IMunicipalityTaxScheduleModel> TaxSchedules { get; set; }
	}
}
