using System;

namespace BusinessLogic.Contract
{
	public interface IRetríeveMunicipalityTaxSchedulesRequest
	{
		DateTime TaxDate { get; set; }
		string Municipality { get; set; }
	}
}
