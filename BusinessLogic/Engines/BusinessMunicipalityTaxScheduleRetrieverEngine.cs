using BusinessLogic.Contract;
using BusinessLogic.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Engines
{
	public class BusinessMunicipalityTaxScheduleRetrieverEngine : IBusinessMunicipalityTaxScheduleRetrieverEngine
	{
		private readonly IResourceMunicipalityTaxScheduleEngine resourceMunicipalityTaxScheduleEngine;
	
		public BusinessMunicipalityTaxScheduleRetrieverEngine(IResourceMunicipalityTaxScheduleEngine resourceMunicipalityTaxScheduleEngine)
		{
		this.resourceMunicipalityTaxScheduleEngine = resourceMunicipalityTaxScheduleEngine;
		}
		public async Task<IRetríeveMunicipalityTaxResponse> RetríeveMunicipalityTax(IRetríeveMunicipalityTaxRequest request)
		{
			// TODO Consider use of AutoMapper or eq. to map
			IRetríeveMunicipalityTaxSchedulesResponse response = await resourceMunicipalityTaxScheduleEngine.RetríeveMunicipalityTaxSchedules(
				new RetríeveMunicipalityTaxSchedulesRequest { Municipality = request.Municipality, TaxDate = request.TaxDate }
				);
			if (response.TaxSchedules.Count() == 0)
				return null;
			return DetermineTax(response.TaxSchedules);
		}

		private IRetríeveMunicipalityTaxResponse DetermineTax(IEnumerable<IMunicipalityTaxScheduleModel> taxSchedules)
		{
			IMunicipalityTaxScheduleModel scheduleModel;
			if ((scheduleModel = FindMunicipalityTaxSchedules(taxSchedules, TaxScheduleType.DAY)) != null)
				return new RetríeveMunicipalityTaxResponse { Tax = scheduleModel.Tax };
			if ((scheduleModel = FindMunicipalityTaxSchedules(taxSchedules, TaxScheduleType.WEEK )) != null)
				return new RetríeveMunicipalityTaxResponse { Tax = scheduleModel.Tax };
			if ((scheduleModel = FindMunicipalityTaxSchedules(taxSchedules, TaxScheduleType.MONTH)) != null)
				return new RetríeveMunicipalityTaxResponse { Tax = scheduleModel.Tax };
			if ((scheduleModel = FindMunicipalityTaxSchedules(taxSchedules, TaxScheduleType.YEAR)) != null)
				return new RetríeveMunicipalityTaxResponse { Tax = scheduleModel.Tax };
			return null;
		}

		private IMunicipalityTaxScheduleModel FindMunicipalityTaxSchedules(IEnumerable<IMunicipalityTaxScheduleModel> taxSchedules, TaxScheduleType scheduleType)
		{
			return taxSchedules.FirstOrDefault(s => s.TaxScheduleType == scheduleType);
		}
	}
}
