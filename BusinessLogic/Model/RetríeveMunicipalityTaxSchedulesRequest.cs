using BusinessLogic.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Model
{
	public class RetríeveMunicipalityTaxSchedulesRequest: IRetríeveMunicipalityTaxSchedulesRequest
	{
		public DateTime TaxDate { get; set; }
		public string Municipality { get; set; }
	}
}
