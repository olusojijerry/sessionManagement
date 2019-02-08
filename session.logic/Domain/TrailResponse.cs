using System;
using System.Collections.Generic;
using System.Text;

namespace session.Logic.Domain
{
	public class TrailResponse
	{
		public string _code { get; set; }
		public string _description { get; set; }
		public string _error { get; set; }
		public TrailResponse(string code, string description, string error)
		{
			_code = code;
			_description = description;
			_error = error;
		}
	}
}
