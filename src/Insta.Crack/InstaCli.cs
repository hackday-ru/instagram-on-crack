using System;
using System.Collections.Generic;
using System.IO;
using Insta.Crack.Commands;
using Insta.Crack.Model;
using Insta.Crack.Services;
using Insta.Crack.Views;
using System.Drawing;
using ColorConsole = Colorful.Console;

namespace Insta.Crack
{
	public class InstaCli
	{
		private readonly InstaServerApi _api = new InstaServerApi();
		private readonly LoginView _login = new LoginView();
		private readonly SplashView _spash = new SplashView();
		private readonly ImageView imageView = new ImageView();
		private ButtonBar imageBtns;
		private ButtonBar navBtns;
		private int _currentImage = 0;
		private IList<InstaMedia> _feed;

		public void Run()
		{
			var userName = _login.Run();

			imageView.UserName = userName;
			_spash.Run(() => _feed = _api.GetMyFeed(userName));
			imageView.Media = _feed[_currentImage];
			imageView.Title = "Моя лента";
			imageBtns = ImageButtons();
			navBtns = NavigateButtons();
			DisplayView();

			while (true)
			{
				var key = Console.ReadKey();
				imageBtns.Key(key.Key);
				navBtns.Key(key.Key);
			}
		}

		private void DisplayView()
		{
			Console.Clear();
			Console.BackgroundColor = ConsoleColor.Black;
			imageView.Media = _feed[_currentImage];
			imageView.View();
			imageBtns.Run();
			navBtns.Run();
		}

		private ButtonBar NavigateButtons()
		{
			return new ButtonBar(
				new List<ButtonCommand>
				{
					new ButtonCommand(ConsoleKey.Home, "Поиск по тегу", ConsoleColor.Yellow, GoTagSearch),
					new ButtonCommand(ConsoleKey.End, "Моя лента", ConsoleColor.Magenta, GoFeed),
				},
				imageView.imageHeight + imageView.titleHeight + 3);
		}

		private ButtonBar ImageButtons()
			=> new ButtonBar(
				new List<ButtonCommand>
				{
					new ButtonCommand(ConsoleKey.LeftArrow, "Предыдущая", ConsoleColor.DarkGreen, PrevImage),
					new ButtonCommand(ConsoleKey.UpArrow, "Лайк", ConsoleColor.Red, LikeCurrentImage),
					new ButtonCommand(ConsoleKey.RightArrow, "Следующая",ConsoleColor.DarkGreen, NextImage)
				},
				imageView.imageHeight + imageView.titleHeight + 4);

		private void GoFeed()
		{
			imageView.Title = "Моя лента";

			_spash.Run(() => _feed = _api.GetMyFeed(""));
			imageView.Media = _feed[_currentImage];
			DisplayView();
		}

		private void GoTagSearch()
		{
			Console.Clear();
			Console.SetCursorPosition(20, 30);
			Console.WriteLine("Имя тега:", ConsoleColor.Magenta);
			Console.SetCursorPosition(20, 36);
			var tag = Console.ReadLine();
			_currentImage = 0;
			_spash.Run(() => _feed = _api.GetTagFeed(tag));
			imageView.Media = _feed[_currentImage];
			imageView.Title = "Поиск по тегу - " + tag;

			DisplayView();
		}

		private void LikeCurrentImage()
		{
			DisplayView();
		}

		private void NextImage()
		{
			if (this._currentImage < _feed.Count - 1)
			{
				this._currentImage++;
			}

			DisplayView();
		}

		private void PrevImage()
		{
			if (this._currentImage > 0)
			{
				this._currentImage--;
			}

			DisplayView();
		}
	}
}