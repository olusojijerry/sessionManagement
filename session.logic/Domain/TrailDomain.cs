using System;
using System.Collections.Generic;
using System.Text;

namespace session.Logic.Domain
{
	class TrailDomain
	{
		public TrailDomain(string description, string category, string userId, TrailResponse response, DateTime dateCreated)
		{
			_description = description;
			_category = category;
			_userId = userId;
			_response = response;
			_creationDate = dateCreated;
		}
		public string _description { get; set; }
		public string _category { get; set; }
		public string _userId { get; set; }
		public TrailResponse _response { get; set; }
		public DateTime _creationDate { get; set; }
	}
}

