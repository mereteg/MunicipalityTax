using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Contract
{
	public interface IMunicipalityTaxScheduleModel
	{
		string Municipality { get; set; }
		TaxScheduleType TaxScheduleType { get; set; }
		DateTime ValidFrom { get; set; }
		DateTime ValidTo { get; set; }
		decimal Tax { get; set; }
	}
}