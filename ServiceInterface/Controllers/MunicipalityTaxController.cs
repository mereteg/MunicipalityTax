using BusinessLogic.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceInterface.Model;
using System;
using System.Threading.Tasks;


namespace MunicipalityTax.ServiceInterface.Controllers
{
	[ApiController]
	[Route("municipalitytax")]
	public class MunicipalityTaxController : ControllerBase
	{


		private readonly ILogger<MunicipalityTaxController> _logger;
		private readonly IBusinessMunicipalityTaxScheduleRetrieverEngine businessMunicipalityTaxScheduleRetrieverEngine;
		private readonly IBusinessMunicipalityTaxScheduleUpdaterEngine businessMunicipalityTaxScheduleUpdaterEngine;

		public MunicipalityTaxController(ILogger<MunicipalityTaxController> logger,
			IBusinessMunicipalityTaxScheduleRetrieverEngine businessMunicipalityTaxScheduleRetrieverEngine,
			IBusinessMunicipalityTaxScheduleUpdaterEngine businessMunicipalityTaxScheduleUpdaterEngine)
		{
			this._logger = logger;
			this.businessMunicipalityTaxScheduleRetrieverEngine = businessMunicipalityTaxScheduleRetrieverEngine;
			this.businessMunicipalityTaxScheduleUpdaterEngine = businessMunicipalityTaxScheduleUpdaterEngine;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(decimal))]
		public async Task<ActionResult<decimal>> Get([FromQuery] RetríeveMunicipalityTaxRequest request)
		{

			// TODO implmement ExceptionHandlerFilter
			try
			{
				IRetríeveMunicipalityTaxResponse response = await businessMunicipalityTaxScheduleRetrieverEngine.RetríeveMunicipalityTax(request);
				if (response == null)
					return NotFound();
				return Ok(response);
			}
			catch (Exception)
			{
				return Problem();
			}

		}

		[HttpPut]
		[Authorize(Policy ="SecureUpdatePolicy")]
		[ProducesResponseType(200, Type = typeof(void))]
		public async Task<ActionResult> Put([FromBody] UpdateMunicipalityTaxScheduleModel request)
		{
			// TODO implmement ExceptionHandlerFilter
			try
			{
				var updateRequest = new UpdateMunicipalityTaxRequest { MunicipalityTaxSchedule = request };
				IUpdateMunicipalityTaxResponse response = await businessMunicipalityTaxScheduleUpdaterEngine.UpdateMunicipalityTax(updateRequest);
				if (response.Status == UpdateMunicipalityTaxStatus.ILLEGAL_INPUT)
					return BadRequest();
				return Ok();
			}
			catch (Exception)
			{
				return Problem();
			}
		}
	}
}
