using System.Collections.Generic;
using InstaSharp.Models;

namespace Insta.Crack.Model
{
	public class InstaMedia
	{
		public Media Media { get; set; }
		public IEnumerable<string> Data { get; set; }
	}
}