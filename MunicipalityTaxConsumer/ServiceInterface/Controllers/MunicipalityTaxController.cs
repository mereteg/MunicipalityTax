using BusinessLogic.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceInterface.Model;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace MunicipalityTax.ServiceInterface.Controllers
{
	[ApiController]
	[Route("municipalitytaxconsumer")]
	public class MunicipalityTaxController : ControllerBase
	{


		private readonly ILogger<MunicipalityTaxController> _logger;
		private readonly ISimpleRestClient simpleRestClient;	

		public MunicipalityTaxController(ILogger<MunicipalityTaxController> logger,
			ISimpleRestClient simpleRestClient)
		{
			this._logger = logger;
			this.simpleRestClient = simpleRestClient; 
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(decimal))]
		public async Task<ActionResult<decimal>> Get([FromQuery] RetríeveMunicipalityTaxRequest request)
		{
			try
			{
				// TODO implement and use BusinessLogic Engines 
				// TODO read host and path info from config
				// TODO Implement ExceptionHandlerFilter
				var result = await simpleRestClient.InvokeServiceAsync(new RestServiceRequest
				{
					Method = "get",
					Path = $"/municipalitytax?Municipality={request.Municipality}&TaxDate={request.TaxDate.ToString("yyyy-MM-dd")}",
					Host = "https://localhost:6001"
				});
				return HandleResponse(result);
			}
			catch (Exception)
			{
				return Problem();
			}

		}

		[HttpPut]
		[ProducesResponseType(200, Type = typeof(void))]
		public async Task<ActionResult> Put([FromBody] MunicipalityTaxScheduleModel request)
		{
			try
			{
				// TODO implement and use BusinessLogic Engines
				// TODO read host and path info from config
				// TODO Implement ExceptionHandlerFilter
				var result = await simpleRestClient.InvokeServiceAsync(new RestServiceRequest
				{
					Method = "put",
					Path = "/municipalitytax",
					Host = "https://localhost:6001",
					Body = JsonSerializer.Serialize<MunicipalityTaxScheduleModel>(request),
					ClientId = "taxconsumer",
					ClientSecret = "taxconsumersecret",
					Scope = "municipalitytaxapi"
				});
				return HandleResponse(result);
			}
			catch (Exception)
			{
				return Problem();
			}
		}

		private ActionResult HandleResponse(IRestServiceResponse result)
		{
			switch (result.StatusCode)
			{
				case HttpStatusCode.OK:
					return Ok(result.Content);
				case HttpStatusCode.BadRequest:
					return BadRequest();
				case HttpStatusCode.NotFound:
					return NotFound();
				case HttpStatusCode.Unauthorized:
					return Unauthorized();
				default:
					return Problem();
			}
		}
	}
}
