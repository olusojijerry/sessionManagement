using System;
using System.Collections.Generic;
using System.Text;

namespace session.Logic.Domain
{
	public class AuthToken : BaseDomain
	{
		public string Token { get; set; }
		//this defines the time the token will take before it expires
		public DateTime Expires { get; set; }
		//states the time the token is started
		public DateTime Start { get; set; }
		public string Email { get; set; }
	}
}
