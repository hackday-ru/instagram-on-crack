using System;
using System.Collections.Generic;

namespace Insta.Crack.Commands
{
	public class RadioCommand
	{
		private int selectedIndex = 0;
		private int topPosition = 0;
		public string Run(int startX, string name, IList<string> options)
		{
			topPosition = startX;
			Console.SetCursorPosition(0, topPosition);
			Console.WriteLine(name);

			DisplayOptions(options);
			var finalSelection = false;
			while (!finalSelection)
			{
				var key = Console.ReadKey();
				if (key.Key == ConsoleKey.UpArrow)
				{
					selectedIndex = selectedIndex > 0 ? selectedIndex - 1 : selectedIndex;
				}
				if (key.Key == ConsoleKey.DownArrow)
				{
					selectedIndex = selectedIndex < options.Count ? selectedIndex + 1 : selectedIndex;
				}

				if (key.Key == ConsoleKey.Enter)
				{
					finalSelection = true;
				}
				else
				{
					DisplayOptions(options);
				}
			}

			return options[selectedIndex];
		}

		private void DisplayOptions(IList<string> options)
		{
			Console.SetCursorPosition(0, topPosition + 1);
			for (int index = 0; index < options.Count; index++)
			{
				var option = options[index];
				Console.ForegroundColor = index == selectedIndex ? ConsoleColor.Magenta : ConsoleColor.White;
				Console.WriteLine(option);
			}
		}
	}
}