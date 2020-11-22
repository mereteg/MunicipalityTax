using BusinessLogic.Contract;
using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceInterface.Model
{
	public class UpdateMunicipalityTaxScheduleModel : IMunicipalityTaxScheduleModel
	{
			
		[Required]
		public string Municipality { get; set; }
		[Required]
		public TaxScheduleType TaxScheduleType { get; set; }
		[Required]
		public DateTime ValidFrom { get; set; }
		[Required]
		public DateTime ValidTo { get; set; }
		[Required]
		public decimal Tax { get; set; }
	}
}
