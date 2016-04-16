using System;

namespace Insta.Crack
{
	public class InstaCrack
	{
		public InstaCrack(int height, int width)
		{
			Height = height;
			Width = width;
		}

		public int Height { get; }
		public int Width { get; }

		public void Clear()
		{
			for (int i = 0; i < Height; i++)
			{
				for (int j = 0; j < Width; j++)
				{
					Console.SetCursorPosition(j, i);
					Console.Write(' ');
				}
			}
		}
	}
}