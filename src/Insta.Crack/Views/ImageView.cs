using System;
using System.Collections.Generic;
using Insta.Crack.Commands;

namespace Insta.Crack
{
	public class ImageView
	{
		public int imageHeight = 50;
		public int titleHeight = 3;

		public string Title { get; set; }
		public string UserName { get; set; }
		public DateTime Date { get; set; }
		public IList<string> Image { get; set; }
		public int Likes { get; set; }
		public int Comments { get; set; }
		public string Caption { get; set; }
		public IList<string> Tags { get; set; }
		public ButtonBar Bar { get; set; }

		public void View()
		{
			Console.Clear();
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			this.DisplayTitle();
			this.DisplayImage();
			this.DisplayFooter();
			while (true)
			{
				var key = Console.ReadKey();
				this.Bar.Key(key.Key);
			}
		}

		private void DisplayTitle()
		{
			Console.SetCursorPosition(10, 0);
			Console.WriteLine(Title);
			Console.SetCursorPosition(10, 1);
			Console.WriteLine(UserName);
			Console.SetCursorPosition(10, 2);
			int[,] image = new int[4, 10]
			{
				{0,0,1,1,0,1,1,0,0,0},
				{0,0,1,1,1,1,1,0,0,0},
				{0,0,0,1,1,1,0,0,0,0},
				{0,0,0,0,1,0,0,0,0,0}
			};
			ColorImage(image, ConsoleColor.DarkRed);
		}

		private void DisplayImage()
		{
			for (int index = 0; index < imageHeight; index++)
			{
				Console.SetCursorPosition(0, titleHeight + index);
				if (Image.Count > index)
				{
					var line = Image[index];
					Console.WriteLine(line);
				}
			}
		}

		private void DisplayFooter()
		{
			Console.SetCursorPosition(0, imageHeight + titleHeight + 1);
			Console.WriteLine("Likes: {0}", Likes);
			Console.SetCursorPosition(0, imageHeight + titleHeight + 2);
			Console.WriteLine("Comments: {0}", Comments);
			this.Bar.Run();
		}

		private void ColorImage(int[,] image, ConsoleColor color)
		{
			for (int i = 0; i < image.GetLowerBound(0); i++)
			{
				for (int j = 0; j < image.GetLowerBound(1); j++)
				{
					Console.SetCursorPosition(i, j);
					Console.BackgroundColor = image[i, j] == 1 ? color : ConsoleColor.Black;
					Console.WriteLine("*");
				}
			}
		}
	}
}