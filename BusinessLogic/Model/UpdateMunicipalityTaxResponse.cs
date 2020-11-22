using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Contract
{
	public class UpdateMunicipalityTaxResponse: IUpdateMunicipalityTaxResponse
	{
		public UpdateMunicipalityTaxStatus Status { get; set; }
	}
}
