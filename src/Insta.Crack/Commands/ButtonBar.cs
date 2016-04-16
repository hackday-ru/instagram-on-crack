using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insta.Crack.Commands
{
	public class ButtonBar
	{
		private readonly int _top;
		private readonly IList<ButtonCommand> _btns;

		public ButtonBar(IList<ButtonCommand> btns, int top)
		{
			_btns = btns;
			_top = top;
		}

		public void Run()
		{
			int left = 0;
			foreach (var buttonCommand in _btns)
			{
				buttonCommand.Display(left, _top);
			}
		}
	}
}
