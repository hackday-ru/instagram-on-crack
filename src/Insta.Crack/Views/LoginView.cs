using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insta.Crack.Views
{
	public class LoginView
	{
		public string Run()
		{
			Console.SetCursorPosition(0, 20);
			this.WriteLogo();

			Console.SetCursorPosition(20, 35);
			Console.BackgroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("Введите логин:");
			Console.SetCursorPosition(20, 36);
			var userName = Console.ReadLine();
			Console.SetCursorPosition(20, 38);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine("Введите пароль:");
			Console.SetCursorPosition(20, 39);
			var pass = Console.ReadLine();
			return userName;
		}

		private void WriteLogo()
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
