using BusinessLogic.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceAccess.Engines
{
	public class RetríeveMunicipalityTaxSchedulesResponse : IRetríeveMunicipalityTaxSchedulesResponse
	{
		public IEnumerable<IMunicipalityTaxScheduleModel> TaxSchedules { get; set; }
	}
}
