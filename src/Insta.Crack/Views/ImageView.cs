using System;
using System.Collections.Generic;
using System.Linq;
using Insta.Crack.Commands;
using Insta.Crack.Model;
using InstaSharp.Models;
using ColorConsole = Colorful.Console;

namespace Insta.Crack
{
	public class ImageView
	{
		public int imageHeight = 50;
		public int titleHeight = 3;

		public string Title { get; set; }
		public string UserName { get; set; }
		public DateTime Date => Media.Media.CreatedTime;
		public IList<ASCIILine> Image => Media.Data.ToList();
		public int Likes => Media.Media.Likes.Count;
		public Comments Comments => Media.Media.Comments;
		public string Caption => Media.Media.Caption.Text;
		public IList<string> Tags { get; set; }
		public InstaMedia Media { get; set; }

		public void View()
		{
			Console.Clear();
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			this.DisplayTitle();
			this.DisplayImage();
			this.DisplayFooter();
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
					for (int i = 0; i < line.Line.Length; i++)
					{
						Console.ForegroundColor = ConsoleColor.Gray;
						Console.BackgroundColor = ClosestConsoleColor(line.Colors[i].R, line.Colors[i].G, line.Colors[i].B);
						Console.Write(line.Line[i]);
					}
				}
			}

			Console.BackgroundColor = ConsoleColor.Black;
		}

		private void DisplayFooter()
		{
			Console.SetCursorPosition(0, imageHeight + titleHeight + 1);
			Console.WriteLine("Likes: {0}", Likes);
			Console.SetCursorPosition(0, imageHeight + titleHeight + 2);
			Console.WriteLine("Comments: {0} - От {1}: {2}", Comments?.Count, Comments?.Data?.FirstOrDefault()?.From.Username, Comments?.Data?.FirstOrDefault()?.Text);
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

		private static ConsoleColor ClosestConsoleColor(byte r, byte g, byte b)
		{
			ConsoleColor ret = 0;
			double rr = r, gg = g, bb = b, delta = double.MaxValue;

			foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
			{
				var n = Enum.GetName(typeof(ConsoleColor), cc);
				var c = System.Drawing.Color.FromName(n == "DarkYellow" ? "Orange" : n); // bug fix
				var t = Math.Pow(c.R - rr, 2.0) + Math.Pow(c.G - gg, 2.0) + Math.Pow(c.B - bb, 2.0);
				if (t == 0.0)
					return cc;
				if (t < delta)
				{
					delta = t;
					ret = cc;
				}
			}
			return ret;
		}
	}
}