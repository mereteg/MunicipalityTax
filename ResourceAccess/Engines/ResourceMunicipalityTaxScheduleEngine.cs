using BusinessLogic.Contract;
using Microsoft.EntityFrameworkCore;
using ResourceAccess.Context;
using ResourceAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ResourceAccess.Engines
{
	public class ResourceMunicipalityTaxScheduleEngine : IResourceMunicipalityTaxScheduleEngine
	{
		private readonly MunicipalityTaxScheduleDbContext context;
		public ResourceMunicipalityTaxScheduleEngine(MunicipalityTaxScheduleDbContext context)
		{
			this.context = context;
		}
		public async Task<IRetríeveMunicipalityTaxSchedulesResponse> RetríeveMunicipalityTaxSchedules(IRetríeveMunicipalityTaxSchedulesRequest request)
		{
			List<IMunicipalityTaxScheduleModel> schedules =  await context.Set<MunicipalityTaxScheduleDbModel>().Where(RetrievePredicate(request)).ToListAsync();
			return new RetríeveMunicipalityTaxSchedulesResponse { TaxSchedules = schedules };
		}

		public async Task UpdateMunicipalityTaxSchedule(IUpdateMunicipalityTaxScheduleRequest request)
		{
			// TODO Consider use of AutoMapper or eq. to map
			MunicipalityTaxScheduleDbModel modelToUpdate = CreateUpdateModel(request);
			IMunicipalityTaxScheduleModel existingModel = await context.Set<MunicipalityTaxScheduleDbModel>().AsNoTracking().Where(RetrieveByKeyPredicate(modelToUpdate)).SingleOrDefaultAsync();
			if (existingModel != null)
				context.Set<MunicipalityTaxScheduleDbModel>().Update(modelToUpdate);
			else
				context.Set<MunicipalityTaxScheduleDbModel>().Add(modelToUpdate);
			await context.SaveChangesAsync();
		}

		private static MunicipalityTaxScheduleDbModel CreateUpdateModel(IUpdateMunicipalityTaxScheduleRequest request)
		{
			return new MunicipalityTaxScheduleDbModel
			{
				Tax = request.MunicipalityTaxSchedule.Tax,
				TaxScheduleType = request.MunicipalityTaxSchedule.TaxScheduleType,
				ValidTo = request.MunicipalityTaxSchedule.ValidTo,
				ValidFrom = request.MunicipalityTaxSchedule.ValidFrom,
				Municipality = request.MunicipalityTaxSchedule.Municipality
			};
		}

		private Expression<Func<IMunicipalityTaxScheduleModel, bool>> RetrievePredicate(IRetríeveMunicipalityTaxSchedulesRequest request)
		{
			return (entity => entity.Municipality.Equals(request.Municipality) 
				&& (entity.ValidFrom <= request.TaxDate) 
				&& (entity.ValidTo >= request.TaxDate));
		}

		private Expression<Func<IMunicipalityTaxScheduleModel, bool>> RetrieveByKeyPredicate(IMunicipalityTaxScheduleModel taxSchedule)
		{
			return (entity => entity.Municipality.Equals(taxSchedule.Municipality) 
				&& entity.ValidFrom.Equals(taxSchedule.ValidFrom) 
				&& entity.TaxScheduleType.Equals(taxSchedule.TaxScheduleType));
		}
	}
}
