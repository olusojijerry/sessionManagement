using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using session.Logic.Domain;
using session.Logic.Services;
using voucherz.sessionMgmnt.Model;

namespace voucherz.sessionMgmnt.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SessionController : ControllerBase
	{
		private readonly ITokenHandler _handler;
		public SessionController(ITokenHandler handler)
		{
			_handler = handler;
		}
		// GET api/values
		[HttpGet]
		public async Task<ActionResult> Get()
		{
			var setup = await _handler.GetSetup();

			await _handler.PostTrailActivity(new TrailResponse("200", "Successful", "Error"), "Get Token Setup");
			return new JsonResult(setup);
		}
		[HttpGet("getByEmail/{email}")]
		public async Task<ActionResult> GetByEmail(string email)
		{
			var setup = await _handler.GetTokensByEmail(email);

			await _handler.PostTrailActivity(new TrailResponse("200", "Successful", "Error"), "Get Tokens By Email");
			return new JsonResult(setup);
		}
		[HttpGet("token")]
		public async Task<ActionResult> GetAllToken()
		{
			var tokens = await _handler.GetAuthTokens();

			await _handler.PostTrailActivity(new TrailResponse("200", "Successful", "Error"), "Get Tokens");
			return new JsonResult(tokens);
		}

		// GET api/values/5
		[HttpGet("{token}")]
		public async Task<IActionResult> Get(string token)
		{
			var tokenDetails = await _handler.GetTokenDetails(token);

			await _handler.PostTrailActivity(new TrailResponse("200", "Successful", "Error"), "Get Tokens");
			return new JsonResult(tokenDetails);
		}

		// POST api/values/email
		[HttpPost("{email}")]
		public async Task<ActionResult> Post(string email)
		{
			Console.WriteLine(email);
			var createToken = await _handler.Create(email);
			Console.WriteLine(createToken.Token);
			await _handler.PostTrailActivity(new TrailResponse("200", "Successful", "Error"), "Create Token");
			return new OkObjectResult(new { Token = createToken.Token, Email = createToken.Email }); ;
		}

		// PUT api/values/5
		[HttpPost("{token}/{microservice}")]
		public async Task<ActionResult> ConfirmToken(string token, string microservice)
		{
			var renewToken = await _handler.Confirm(token);

			await _handler.PostTrailActivity(new TrailResponse("200", "Successful", "Error"), "Check and Update Tokens");
			return new OkObjectResult(new { Token = renewToken.Token, Email = renewToken.Email });
		}

	}
}
