using BusinessLogic.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Model
{
	public class RetríeveMunicipalityTaxResponse : IRetríeveMunicipalityTaxResponse
	{
		public decimal Tax { get; set; }
	}
}
