using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceInterface.Model
{
	public class RetríeveMunicipalityTaxRequest 
	{
		[Required]
		public DateTime TaxDate { get; set; }
		[Required]
		public string Municipality { get; set; }
	}
}
