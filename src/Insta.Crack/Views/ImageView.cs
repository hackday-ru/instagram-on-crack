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
		}

		private void DisplayImage()
		{
			for (int index = 0; index < imageHeight; index++)
			{
				Console.SetCursorPosition(0, titleHeight + index);
				if (Image.Count > imageHeight)
				{
					var line = Image[index];
					Console.BackgroundColor = ConsoleColor.Black;
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
	}
}