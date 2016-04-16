using System;
using System.Collections.Generic;
using Insta.Crack.Commands;

namespace Insta.Crack.Views
{
	public class PageSelectView
	{
		private static int imageSize = 40;
		private static readonly ConsoleColor defaultColor = Console.ForegroundColor;

		public void Run()
		{
			var check = new RadioCommand();
			var selected = check.Run(imageSize + 3, "test checkbx", new List<string> { "Моя лента", "Теги" });
			Console.ForegroundColor = defaultColor;
		}
	}
}