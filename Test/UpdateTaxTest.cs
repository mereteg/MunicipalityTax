using BusinessLogic.Contract;
using BusinessLogic.Engines;
using ServiceInterface.Model;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
	public class UpdateTaxTest
	{
		private BusinessMunicipalityTaxScheduleUpdaterEngine businessMunicipalityTaxScheduleuUpdaterEngine;
		public UpdateTaxTest()
		{
			businessMunicipalityTaxScheduleuUpdaterEngine = new BusinessMunicipalityTaxScheduleUpdaterEngine(
				new ResourceMunicipalityTaxScheduleEngineMock()
				);
		}

		[Fact]
		public async Task IllegalValidScheduleWithEmptyMunicipality()
		{
			var request = new UpdateMunicipalityTaxRequest
			{
				MunicipalityTaxSchedule = new UpdateMunicipalityTaxScheduleModel
				{
					Municipality = "",
					ValidFrom = new DateTime(2020, 1, 1),
					ValidTo = new DateTime(2020, 1, 1),
					Tax = 0.4M,
					TaxScheduleType = TaxScheduleType.DAY
				}
			};
			var response = await businessMunicipalityTaxScheduleuUpdaterEngine.UpdateMunicipalityTax(request);
			Assert.Equal(UpdateMunicipalityTaxStatus.ILLEGAL_INPUT, response.Status);
		}

		[Fact]
		public async Task ValidScheduleOfTypeDay()
		{
			var request = new UpdateMunicipalityTaxRequest {
				MunicipalityTaxSchedule = new UpdateMunicipalityTaxScheduleModel
				{
					Municipality = "Vaerloese",
					ValidFrom = new DateTime(2020, 1, 1, 12, 12, 12),
					ValidTo = new DateTime(2020, 1, 1, 17, 15, 13),
					Tax = 0.4M,
					TaxScheduleType = TaxScheduleType.DAY
				}
			};
			var response = await businessMunicipalityTaxScheduleuUpdaterEngine.UpdateMunicipalityTax(request);
			Assert.Equal(UpdateMunicipalityTaxStatus.OK, response.Status);
		}

		[Fact]
		public async Task IllegalScheduleOfTypeDay()
		{
			var request = new UpdateMunicipalityTaxRequest
			{
				MunicipalityTaxSchedule = new UpdateMunicipalityTaxScheduleModel
				{
					Municipality = "Vaerloese",
					ValidFrom = new DateTime(2020, 1, 1),
					ValidTo = new DateTime(2020, 1, 2),
					Tax = 0.4M,
					TaxScheduleType = TaxScheduleType.DAY
				}
			};
			var response = await businessMunicipalityTaxScheduleuUpdaterEngine.UpdateMunicipalityTax(request);
			Assert.Equal(UpdateMunicipalityTaxStatus.ILLEGAL_INPUT, response.Status);
			request.MunicipalityTaxSchedule.ValidTo = new DateTime(2019, 1, 1);
			response = await businessMunicipalityTaxScheduleuUpdaterEngine.UpdateMunicipalityTax(request);
			Assert.Equal(UpdateMunicipalityTaxStatus.ILLEGAL_INPUT, response.Status);
			request.MunicipalityTaxSchedule.ValidTo = new DateTime(2020, 2, 1);
			response = await businessMunicipalityTaxScheduleuUpdaterEngine.UpdateMunicipalityTax(request);
			Assert.Equal(UpdateMunicipalityTaxStatus.ILLEGAL_INPUT, response.Status);
		}

		[Fact]
		public async Task ValidScheduleOfTypeWeek()
		{
			var request = new UpdateMunicipalityTaxRequest
			{
				MunicipalityTaxSchedule = new UpdateMunicipalityTaxScheduleModel
				{
					Municipality = "Vaerloese",
					ValidFrom = new DateTime(2020, 11, 23),
					ValidTo = new DateTime(2020, 11, 29),
					Tax = 0.3M,
					TaxScheduleType = TaxScheduleType.WEEK
				}
			};
			var response = await businessMunicipalityTaxScheduleuUpdaterEngine.UpdateMunicipalityTax(request);
			Assert.Equal(UpdateMunicipalityTaxStatus.OK, response.Status);
		}

		[Fact]
		public async Task IllegalScheduleOfTypeWeek()
		{
			var request = new UpdateMunicipalityTaxRequest
			{
				MunicipalityTaxSchedule = new UpdateMunicipalityTaxScheduleModel
				{
					Municipality = "Vaerloese",
					ValidFrom = new DateTime(2020, 11, 23),
					ValidTo = new DateTime(2020, 11, 30),
					Tax = 0.3M,
					TaxScheduleType = TaxScheduleType.WEEK
				}
			};
			var response = await businessMunicipalityTaxScheduleuUpdaterEngine.UpdateMunicipalityTax(request);
			Assert.Equal(UpdateMunicipalityTaxStatus.ILLEGAL_INPUT, response.Status);
			request.MunicipalityTaxSchedule.ValidTo = new DateTime(2019, 11, 24);
			response = await businessMunicipalityTaxScheduleuUpdaterEngine.UpdateMunicipalityTax(request);
			Assert.Equal(UpdateMunicipalityTaxStatus.ILLEGAL_INPUT, response.Status);
			request.MunicipalityTaxSchedule.ValidTo = new DateTime(2020, 11, 28);
			request.MunicipalityTaxSchedule.ValidFrom = new DateTime(2020, 11, 22);
			response = await businessMunicipalityTaxScheduleuUpdaterEngine.UpdateMunicipalityTax(request);
			Assert.Equal(UpdateMunicipalityTaxStatus.ILLEGAL_INPUT, response.Status);
		}

		[Fact]
		public async Task ValidScheduleOfTypeMonth()
		{
			var request = new UpdateMunicipalityTaxRequest
			{
				MunicipalityTaxSchedule = new UpdateMunicipalityTaxScheduleModel
				{
					Municipality = "Vaerloese",
					ValidFrom = new DateTime(2020, 2, 1),
					ValidTo = new DateTime(2020, 2, 29),
					Tax = 0.2M,
					TaxScheduleType = TaxScheduleType.MONTH
				}
			};
			var response = await businessMunicipalityTaxScheduleuUpdaterEngine.UpdateMunicipalityTax(request);
			Assert.Equal(UpdateMunicipalityTaxStatus.OK, response.Status);
		}

		[Fact]
		public async Task IllegalScheduleOfTypeMonth()
		{
			var request = new UpdateMunicipalityTaxRequest
			{
				MunicipalityTaxSchedule = new UpdateMunicipalityTaxScheduleModel
				{
					Municipality = "Vaerloese",
					ValidFrom = new DateTime(2020, 11, 1),
					ValidTo = new DateTime(2020, 11, 29),
					Tax = 0.2M,
					TaxScheduleType = TaxScheduleType.MONTH
				}
			};
			var response = await businessMunicipalityTaxScheduleuUpdaterEngine.UpdateMunicipalityTax(request);
			Assert.Equal(UpdateMunicipalityTaxStatus.ILLEGAL_INPUT, response.Status);
			request.MunicipalityTaxSchedule.ValidTo = new DateTime(2019, 11, 30);
			response = await businessMunicipalityTaxScheduleuUpdaterEngine.UpdateMunicipalityTax(request);
			Assert.Equal(UpdateMunicipalityTaxStatus.ILLEGAL_INPUT, response.Status);
			request.MunicipalityTaxSchedule.ValidFrom = new DateTime(2020, 12, 31);
			response = await businessMunicipalityTaxScheduleuUpdaterEngine.UpdateMunicipalityTax(request);
			Assert.Equal(UpdateMunicipalityTaxStatus.ILLEGAL_INPUT, response.Status);
		}

		[Fact]
		public async Task ValidScheduleOfTypeYear()
		{
			var request = new UpdateMunicipalityTaxRequest
			{
				MunicipalityTaxSchedule = new UpdateMunicipalityTaxScheduleModel
				{
					Municipality = "Vaerloese",
					ValidFrom = new DateTime(2020, 1, 1),
					ValidTo = new DateTime(2020, 12, 31),
					Tax = 0.1M,
					TaxScheduleType = TaxScheduleType.YEAR
				}
			};
			var response = await businessMunicipalityTaxScheduleuUpdaterEngine.UpdateMunicipalityTax(request);
			Assert.Equal(UpdateMunicipalityTaxStatus.OK, response.Status);
		}
		[Fact]
		public async Task IllegalScheduleOfTypeYear()
		{
			var request = new UpdateMunicipalityTaxRequest
			{
				MunicipalityTaxSchedule = new UpdateMunicipalityTaxScheduleModel
				{
					Municipality = "Vaerloese",
					ValidFrom = new DateTime(2020, 1, 1),
					ValidTo = new DateTime(2021, 12, 31),
					Tax = 0.1M,
					TaxScheduleType = TaxScheduleType.YEAR
				}
			};
			var response = await businessMunicipalityTaxScheduleuUpdaterEngine.UpdateMunicipalityTax(request);
			Assert.Equal(UpdateMunicipalityTaxStatus.ILLEGAL_INPUT, response.Status);
			request.MunicipalityTaxSchedule.ValidTo = new DateTime(2020, 12, 30);
			response = await businessMunicipalityTaxScheduleuUpdaterEngine.UpdateMunicipalityTax(request);
			Assert.Equal(UpdateMunicipalityTaxStatus.ILLEGAL_INPUT, response.Status);
			request.MunicipalityTaxSchedule.ValidFrom = new DateTime(2019, 12, 31);
			request.MunicipalityTaxSchedule.ValidTo = new DateTime(2020, 12, 31);
			response = await businessMunicipalityTaxScheduleuUpdaterEngine.UpdateMunicipalityTax(request);
			Assert.Equal(UpdateMunicipalityTaxStatus.ILLEGAL_INPUT, response.Status);
		}

	}
}
