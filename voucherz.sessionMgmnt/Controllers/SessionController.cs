using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

			return new JsonResult(setup);
		}
		[HttpGet("token")]
		public async Task<ActionResult> GetAllToken()
		{
			var tokens = await _handler.GetAuthTokens();

			return new JsonResult(tokens);
		}

		// GET api/values/5
		[HttpGet("{token}")]
		public async Task<IActionResult> Get(string token)
		{
			var tokenDetails = await _handler.GetTokenDetails(token);

			return new JsonResult(tokenDetails);
		}

		// POST api/values/email
		[HttpPost("{email}")]
		public async Task<ActionResult> Post(string email)
		{
			var createToken = await _handler.Create(email);

			return new OkObjectResult(new { Token = createToken.Token, Email = createToken.Email }); ;
		}

		// PUT api/values/5
		[HttpPut("{token}/{microservice}")]
		public async Task<ActionResult> Put(string token, string microservice)
		{
			var renewToken = await _handler.Confirm(token);

			return new OkObjectResult(new { Token = renewToken.Token, Email = renewToken.Email });
		}

	}
}
