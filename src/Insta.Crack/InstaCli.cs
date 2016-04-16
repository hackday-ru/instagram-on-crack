using System;
using System.Collections.Generic;
using System.IO;
using Insta.Crack.Commands;
using Insta.Crack.Views;

namespace Insta.Crack
{
	public class InstaCli
	{
		private readonly LoginView _login = new LoginView();
		private readonly ImageView imageView = new ImageView();

		public void Run()
		{
			var userName = _login.Run();
			imageView.UserName = userName;
			imageView.Likes = 3;
			imageView.Comments = 2;
			imageView.Title = "Просмотр картинка";
			imageView.Image = File.ReadAllLines("./samples/cat.txt");
			imageView.Bar = ImageButtons();
			imageView.View();

			Console.ReadKey();
		}

		private ButtonBar ImageButtons()
		{
			return new ButtonBar(
				new List<ButtonCommand>
				{
					new ButtonCommand(ConsoleKey.LeftArrow, "Предыдущая", ConsoleColor.DarkGreen,PrevImage),
					new ButtonCommand(ConsoleKey.UpArrow, "Лайк", ConsoleColor.Red,LikeCurrentImage),
					new ButtonCommand(ConsoleKey.RightArrow, "Следующая",ConsoleColor.DarkGreen,NextImage)
				},
				60);
		}

		private void LikeCurrentImage()
		{
			imageView.Likes++;
			imageView.View();
		}

		private void NextImage()
		{
			imageView.Image = File.ReadAllLines("./samples/cat2.txt");
			imageView.View();
		}

		private void PrevImage()
		{
			imageView.Image = File.ReadAllLines("./samples/cat.txt");
			imageView.View();
		}
	}
}