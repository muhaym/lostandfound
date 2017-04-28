using System;
using System.Collections.Generic;

namespace lostandfound
{
	public class Matched
	{
		public string id { get; set; }
		public string image { get; set; }
	}

	public class RootObject
	{
		public List<Matched> matched { get; set; }
		public string message { get; set; }
		public string prediction { get; set; }
		public int status { get; set; }
	}
}
