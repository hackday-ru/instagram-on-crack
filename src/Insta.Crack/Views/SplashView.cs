using System;

namespace Insta.Crack
{
	public class SplashView
	{
		public void Run(Action runningAction)
		{
			Console.Clear();
			Console.SetCursorPosition(10, 20);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine();
			Console.WriteLine(@"                                                       ");
			Console.WriteLine(@"                        __   __     ON  CRACK          ");
			Console.WriteLine(@"                       /  \ /  \                       ");
			Console.WriteLine(@"                       \       /    WE RECIEVE SOME    ");
			Console.WriteLine(@"                        \     /     CRACK FOR YOU      ");
			Console.WriteLine(@"                         \   /                         ");
			Console.WriteLine(@"                          \_/        BE PATIENT        ");
			Console.WriteLine(@"                                                       ");
			Console.WriteLine(@"                                                       ");
			Console.WriteLine(@"                                                       ");
			Console.WriteLine(@"                                                       ");
			Console.WriteLine(@"                                                       ");
			Console.WriteLine(@"                                     LITTLE JUNK       ");
			Console.WriteLine();

			runningAction();
		}
	}
}