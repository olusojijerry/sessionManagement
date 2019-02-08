using session.Logic.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace session.Logic.Services
{
	public interface ITokenHandler
	{
		Task<Response> Create(string merchantMail);
		Task<Response> Confirm(string token);
		Task<SessionSetup> GetSetup();
		Task<AuthToken> GetTokenDetails(string token);
		Task<IEnumerable<AuthToken>> GetAuthTokens();
	}
}
