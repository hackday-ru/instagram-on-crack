using System;

namespace Insta.Crack.Commands
{
	public class ButtonCommand
	{
		public readonly ConsoleKey _trigger;
		public readonly string _name;
		public readonly ConsoleColor _color;
		public readonly Action callback;

		public ButtonCommand(ConsoleKey trigger, string name, ConsoleColor color, Action callback)
		{
			_trigger = trigger;
			_name = name;
			_color = color;
			this.callback = callback;
		}

		public void Display(int left, int top)
		{
			Console.SetCursorPosition(left, top);
			Console.ForegroundColor = _color;
			Console.Write(" {0} - {1} |", _trigger, _name);
		}

		public bool Run(ConsoleKey key)
		{
			if (key == _trigger)
			{
				callback();
				return true;
			}

			return false;
		}
	}
}