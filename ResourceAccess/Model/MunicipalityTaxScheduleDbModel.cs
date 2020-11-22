using BusinessLogic.Contract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceAccess.Model
{
	[Table("MunicipalityTaxSchedule")]
	public class MunicipalityTaxScheduleDbModel : IMunicipalityTaxScheduleModel
	{
		[Required]
		[Column("Municipality")]
		public string Municipality { get; set; }
		[Required]
		[Column("TaxScheduleType")]
		public TaxScheduleType TaxScheduleType { get; set; }
		[Required]
		[Column("ValidFrom")]
		public DateTime ValidFrom { get; set; }
		[Required]
		[Column("ValidTo")]
		public DateTime ValidTo { get; set; }

		[Required]
		[Column("Tax", TypeName = "decimal(9,4)")]
		public decimal Tax { get; set; }
	}
}