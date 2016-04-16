using System;
using System.Drawing;
using ColorConsole = Colorful.Console;

namespace Insta.Crack
{
	public class SplashView
	{
		public void Run(Action runningAction)
		{
			Console.Clear();
			ColorConsole.WriteAscii("We load".ToUpper(), Color.Chartreuse);
			ColorConsole.WriteAscii("Some Crack".ToUpper(), Color.Chartreuse);
			ColorConsole.WriteAscii("FOR U".ToUpper(), Color.Chartreuse);
			runningAction();
		}
	}
}