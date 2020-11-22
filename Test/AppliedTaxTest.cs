using BusinessLogic.Contract;
using BusinessLogic.Engines;
using ServiceInterface.Model;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
	public class AppliedTaxTest
	{
		private IBusinessMunicipalityTaxScheduleRetrieverEngine businessMunicipalityTaxScheduleRetrieverEngine;
		public AppliedTaxTest()
		{
			businessMunicipalityTaxScheduleRetrieverEngine = new BusinessMunicipalityTaxScheduleRetrieverEngine(
				new ResourceMunicipalityTaxScheduleEngineMock()
				);
		}

		[Fact]
		public async Task NoTaxSceduleForDate()
		{
			var request = new RetríeveMunicipalityTaxRequest { Municipality = "Vaerloese", TaxDate = new DateTime(2016, 1, 1) };
			var response = await businessMunicipalityTaxScheduleRetrieverEngine.RetríeveMunicipalityTax(request);
			Assert.True(response == null);
		}

		[Fact]
		public async Task DaySchedulePreferredForWeekWhenDateInDaySchedule()
		{
			var request = new RetríeveMunicipalityTaxRequest { Municipality = "Vaerloese", TaxDate = new DateTime(2020, 11, 25) };
			var response = await businessMunicipalityTaxScheduleRetrieverEngine.RetríeveMunicipalityTax(request);
			Assert.True(response != null);
		 Assert.Equal(0.4M, response.Tax);
		}

		
		[Fact]
		public async Task WeekSchedulePreferredForMonthWhenDateInWeekSchedule()
		{
			var request = new RetríeveMunicipalityTaxRequest { Municipality = "Vaerloese", TaxDate = new DateTime(2020, 11, 26) };
			var response = await businessMunicipalityTaxScheduleRetrieverEngine.RetríeveMunicipalityTax(request);
			Assert.True(response != null);
			Assert.Equal(0.3M, response.Tax);
		}

	
		[Fact]
		public async Task MonthSchedulePreferredForYearWhenDateInMonthSchedule()
		{
			var request = new RetríeveMunicipalityTaxRequest { Municipality = "Vaerloese", TaxDate = new DateTime(2020, 11, 10) };
			var response = await businessMunicipalityTaxScheduleRetrieverEngine.RetríeveMunicipalityTax(request);
			Assert.True(response != null);
			Assert.Equal(0.2M, response.Tax);
		}

		[Fact]
		public async Task YearScheduleWhenOnlyYearSchedule()
		{
			var request = new RetríeveMunicipalityTaxRequest { Municipality = "Vaerloese", TaxDate = new DateTime(2020, 12, 24) };
			var response = await businessMunicipalityTaxScheduleRetrieverEngine.RetríeveMunicipalityTax(request);
			Assert.True(response != null);
			Assert.Equal(0.1M, response.Tax);
		}

			[Fact]
		public async Task ExamplesForCopenhagen()
		{
			var request = new RetríeveMunicipalityTaxRequest { Municipality = "Copenhagen" , TaxDate = new DateTime(2016,1,1)} ;
			var response = await businessMunicipalityTaxScheduleRetrieverEngine.RetríeveMunicipalityTax(request);
			Assert.True(response != null);
			Assert.Equal(0.1M, response.Tax);
			request.TaxDate = new DateTime(2016, 5, 2);
			response = await businessMunicipalityTaxScheduleRetrieverEngine.RetríeveMunicipalityTax(request);
			Assert.True(response != null);
			Assert.Equal(0.4M, response.Tax);
			request.TaxDate = new DateTime(2016, 7, 10);
			response = await businessMunicipalityTaxScheduleRetrieverEngine.RetríeveMunicipalityTax(request);
			Assert.True(response != null);
			Assert.Equal(0.2M, response.Tax);
			request.TaxDate = new DateTime(2016, 3, 16);
			response = await businessMunicipalityTaxScheduleRetrieverEngine.RetríeveMunicipalityTax(request);
			Assert.True(response != null);
			Assert.Equal(0.2M, response.Tax);
		}
	}
}
