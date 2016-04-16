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
			Console.WriteLine("test");
			WriteLogo();
			var check = new RadioCommand();
			var selected = check.Run(imageSize + 3, "test checkbx", new List<string> {"first option", "sdasd", "asdasd"});
			Console.ForegroundColor = defaultColor;
			Console.WriteLine($"you select - {selected}");
			Console.ReadKey();
		}

		private static void WriteLogo()
		{
			const string Bold = "\x1b[1m";
			const string Normal = "\x1b[22m";
			const string Magenta = "\x1b[35m";
			const string White = "\x1b[37m";
			const string Default = "\x1b[39m";

			Console.WriteLine();
			Console.WriteLine(@"                                  ");
			Console.WriteLine(@"   __   __     ON  CRACK          ");
			Console.WriteLine(@"  /  \ /  \                       ");
			Console.WriteLine(@"  \       /    WITHOUT COCAINE    ");
			Console.WriteLine(@"   \     /                        ");
			Console.WriteLine(@"    \   /                         ");
			Console.WriteLine(@"     \_/                          ");
			Console.WriteLine();
		}
	}
}
