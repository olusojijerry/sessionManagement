using System;
using System.Collections.Generic;
using System.Text;

namespace session.Logic.Domain
{
	public class SessionSetup : BaseDomain
	{
		public string SecretKey { get; set; }
		//defines the mins the token wil remain unused before it expires
		public int ExpiryMin { get; set; }
		//states who created the token
		public string Issuer { get; set; }
		//this is the date the user was created
		public DateTime DateCreated { get; set; }
	}
}
