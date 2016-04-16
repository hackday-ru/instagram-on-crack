using System.Collections.Generic;
using System.Drawing;
using InstaSharp.Models;

namespace Insta.Crack.Model
{
	public class InstaMedia
	{
		public Media Media { get; set; }
		public IEnumerable<ASCIILine> Data { get; set; }
	}

	public class ASCIILine
	{
		public string Line { get; set; }
		public IList<Color> Colors { get; set; }
	}
}