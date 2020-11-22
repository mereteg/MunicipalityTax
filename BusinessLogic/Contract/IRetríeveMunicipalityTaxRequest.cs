using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Contract
{
	public interface IRetríeveMunicipalityTaxRequest
	{
		DateTime TaxDate { get; set; }
		string Municipality { get; set; }
	}
}
