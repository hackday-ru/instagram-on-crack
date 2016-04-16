using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Insta.Server.Models
{
	public class ASCIILine
	{
		public string Line { get; set; }
		public IList<Color> Colors { get; set; } = new List<Color>();
	}
}