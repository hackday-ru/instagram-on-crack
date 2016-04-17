using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Insta.Crack.Services;
using ColorConsole = Colorful.Console;

namespace Insta.Crack.Views
{
	public class LoginView
	{
		public string Run()
		{
			Console.SetCursorPosition(0, 10);
			this.WriteLogo();

			Console.SetCursorPosition(20, 35);
			ColorConsole.WriteFormatted("Введите логин:".ToUpper(), Color.Chocolate);
			Console.SetCursorPosition(20, 36);
			var userName = Console.ReadLine();
			Console.SetCursorPosition(20, 38);
			Console.BackgroundColor = ConsoleColor.Black;
			ColorConsole.WriteFormatted("Введите пароль:".ToUpper(), Color.Chocolate);
			Console.ForegroundColor = ConsoleColor.Black;
			Console.SetCursorPosition(20, 39);
			var pass = Console.ReadLine();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("WE R LOGGINING U", ConsoleColor.Red);
			return new InstaServerApi().LoginUser(userName, pass);
		}

		private void WriteLogo()
		{
			ColorConsole.WriteAscii("   instagram".ToUpper(), Color.Chocolate);
			ColorConsole.WriteAscii("   on crack".ToUpper(), Color.Chocolate);
			ColorConsole.WriteAscii("   WITHOUT COCAINE".ToUpper(), Color.Chocolate);
		}
	}
}
