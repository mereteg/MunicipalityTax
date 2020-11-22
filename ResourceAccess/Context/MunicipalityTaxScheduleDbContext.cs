using Microsoft.EntityFrameworkCore;
using ResourceAccess.Model;

namespace ResourceAccess.Context
{

	/// <summary>
	/// Entity Framework database context which holds MunicipalityTaxSchedule models.
	/// </summary>
	public partial class MunicipalityTaxScheduleDbContext : DbContext
	{
		public MunicipalityTaxScheduleDbContext(DbContextOptions<MunicipalityTaxScheduleDbContext> options) : base(options) { }

		public DbSet<MunicipalityTaxScheduleDbModel> Request { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<MunicipalityTaxScheduleDbModel>()
				.HasKey(k => new { k.Municipality, k.TaxScheduleType, k.ValidFrom });
		}
	}

}
