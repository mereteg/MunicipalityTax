using BusinessLogic.Contract;
using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceInterface.Model
{
	public class RetríeveMunicipalityTaxRequest : IRetríeveMunicipalityTaxRequest
	{
		[Required]
		public DateTime TaxDate { get; set; }
		[Required]
		public string Municipality { get; set; }
	}
}
