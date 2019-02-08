using Dapper;
using session.Logic.ClientCommunication;
using session.Logic.Domain;
using session.Logic.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace session.Logic.Services
{
	public class TokenHandler : ITokenHandler
	{
		private readonly IBaseDAO<AuthToken> _dao;
		private readonly IBaseDAO<SessionSetup> _dao1;
		public TokenHandler(IBaseDAO<AuthToken> dao, IBaseDAO<SessionSetup> dao1)
		{
			_dao = dao;
			_dao1 = dao1;
		}
		public async Task<Response> Confirm(string token)
		{
			var response = new Response();
			DynamicParameters para = new DynamicParameters();

			var sessions = await _dao1.Find("uspFindSetupByMicroservice", para);

			para.Add("@token", token);
			var tokenDetails = _dao.Find("uspGetAToken", para).Result;

			if (tokenDetails != null)
			{
				if(tokenDetails.Expires >= tokenDetails.Start)
				{
					var UtcNow = DateTime.UtcNow;
					var expires = UtcNow.AddMinutes(sessions.ExpiryMin);

					DynamicParameters parameters = new DynamicParameters();

					parameters.Add("@token", token);
					parameters.Add("@expires", expires);
					parameters.Add("@start", UtcNow);
					parameters.Add("@email", tokenDetails.Email);

					int value = await _dao.Create("uspCreateToken", parameters);

					if (value > 0)
						return new Response { Token = token, Email = tokenDetails.Email };
					else
						return await Task.Run(() => Confirm(token));

				}
			}
			throw new NotImplementedException();
		}

		public async Task<Response> Create(string merchantMail)
		{
			DynamicParameters para = new DynamicParameters();
			
			var sessions = await _dao1.Find("uspFindSetupByMicroservice", para);

			var UtcNow = DateTime.UtcNow;
			var expires = UtcNow.AddMinutes(sessions.ExpiryMin);

			string token = GenerateUnique();

			DynamicParameters parameters = new DynamicParameters();

			parameters.Add("@token", token);
			parameters.Add("@expires", expires);
			parameters.Add("@start", UtcNow);
			parameters.Add("@email", merchantMail);

			int value = await _dao.Create("uspCreateToken", parameters);

			//check to confirm if record was created 
			if (value > 0)
				return new Response { Token = token, Email = merchantMail };
			else
				return await Task.Run(() => Create(merchantMail));
		}

		public void Reset(long startTime, long endTime)
		{
			//here you save to the database the reset time
		}

		public string GenerateUnique()
		{
			//here you generate a unique token
			Guid unque = Guid.NewGuid();

			string Token = unque.ToString();
			return Token;
		}

		public async Task<SessionSetup> GetSetup()
		{
			DynamicParameters para = new DynamicParameters();

			var sessions = await _dao1.Find("uspFindSetupByMicroservice", para);
			return sessions;
		}

		public async Task<AuthToken> GetTokenDetails(string token)
		{
			DynamicParameters parameters = new DynamicParameters();

			parameters.Add("@token", token);

			var details = await _dao.Find("uspGetAToken", parameters);

			return details;
		}

		public async Task<IEnumerable<AuthToken>> GetAuthTokens()
		{
			DynamicParameters parameters = new DynamicParameters();
			

			var details = await _dao.FindAll("uspGetAllToken", parameters);

			return details;
		}

		public async Task PostTrailActivity(TrailResponse response, string action)
		{
			TrailDomain trail;
			trail = new TrailDomain("Redemption", "Redemption Microservice", action, response, DateTime.UtcNow);
			//ensure you get the path to the microservice
			await ClientConnection<TrailDomain>.CreateTrailAsync(trail,
				string.Format("http://172.20.20.127:9000/voucherz/audittrails/{0}/{1}/{2}/{3}/{4}/{5}",
				"Audit Trail", "Redemption Microservice", action, response._code, response._description, response._error));

		}

		public async Task<IEnumerable<AuthToken>> GetTokensByEmail(string email)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@email", email);

			var findAllByEmail = await _dao.FindAll("uspGetAllTokenByEmail", parameters);

			return findAllByEmail;
		}
	}
}
