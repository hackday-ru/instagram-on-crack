using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Insta.Crack.Commands;

namespace Insta.Crack
{
	public class Program
	{
		private static int imageSize = 40;
		private static readonly ConsoleColor defaultColor = Console.ForegroundColor;

		public static void Main(string[] args)
		{
			Console.Clear();
			var cli = new InstaCli();
			cli.Run();

			var check = new RadioCommand();
			var selected = check.Run(imageSize + 3, "test checkbx", new List<string> {"first option", "sdasd", "asdasd"});
			Console.ForegroundColor = defaultColor;
			Console.WriteLine($"you select - {selected}");
			Console.ReadKey();
		}

	}
}
