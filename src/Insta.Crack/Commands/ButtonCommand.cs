using System;

namespace Insta.Crack.Commands
{
	public class ButtonCommand
	{
		private readonly ConsoleKey _trigger;
		private readonly string _name;
		private readonly ConsoleColor _color;

		public ButtonCommand(ConsoleKey trigger)
		{
			_trigger = trigger;
		}

		public void Display(int left, int top)
		{
			Console.SetCursorPosition(left, top);
			Console.ForegroundColor = _color;
			Console.Write("{0} - {1}", _trigger, _name);
		}

		public bool Run(ConsoleKey key, Action callback)
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