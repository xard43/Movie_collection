using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Movie_Collection.Logging
{
	public class RequestExceptionMessage : Exception
	{
		public HttpStatusCode StatusCode { get; set; }
		public string ExceptionMessage { get; set; }
		public int EventID { get; set; }

		public RequestExceptionMessage(HttpStatusCode statusCode, string message)
		{
			StatusCode = statusCode;
			ExceptionMessage = message;
		}
	}
}
