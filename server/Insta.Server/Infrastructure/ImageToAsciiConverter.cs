using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Insta.Server.Models;

namespace Insta.Server.Infrastructure
{
	public class ImageToAsciiConverter
	{
		private string[] _AsciiChars = { "#", "#", "@", "%", "=", "+", "*", ":", "-", ".", " " };

		public string GetImage(string path, int scale)
		{
			//Load the Image from the specified path
			var image = new Bitmap(path, true);

			image = GetReSizedImage(image, scale);


			return "<pre>" + "<Font size=0>" + ConvertToAscii(image) + "</Font></pre>";

		}

		public IEnumerable<ASCIILine> GetArrayImage(string path, int scale)
		{
			//Load the Image from the specified path
			var image = new Bitmap(path, true);

			return GetArrayImage(image, scale);
		}

		public IEnumerable<ASCIILine> GetArrayImage(Bitmap image, int scale)
		{

			image = GetReSizedImage(image, scale);

			return ConvertToAsciiList(image);
		}

		public string GetImage(string path)
		{
			//Load the Image from the specified path
			var image = new Bitmap(path, true);


			return "<pre>" + "<Font size=0>" + ConvertToAscii(image) + "</Font></pre>";

		}

		private IEnumerable<ASCIILine> ConvertToAsciiList(Bitmap image)
		{
			Boolean toggle = false;
			StringBuilder sb = new StringBuilder();
			var list = new List<ASCIILine>();
			for (int h = 0; h < image.Height; h++)
			{
				ASCIILine line = new ASCIILine();
				for (int w = 0; w < image.Width; w++)
				{
					Color pixelColor = image.GetPixel(w, h);
					//Average out the RGB components to find the Gray Color
					int red = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
					int green = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
					int blue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
					Color grayColor = Color.FromArgb(red, green, blue);

					//Use the toggle flag to minimize height-wise stretch
					if (!toggle)
					{
						int index = (grayColor.R * 10) / 255;
						line.Colors.Add(pixelColor);
						sb.Append(_AsciiChars[index]);
					}
				}
				if (!toggle)
				{
					line.Line = sb.ToString();
					list.Add(line);
					sb = new StringBuilder();
					toggle = true;
				}
				else
				{
					toggle = false;
				}
			}

			return list;
		}

		private string ConvertToAscii(Bitmap image)
		{
			Boolean toggle = false;
			StringBuilder sb = new StringBuilder();

			for (int h = 0; h < image.Height; h++)
			{
				for (int w = 0; w < image.Width; w++)
				{
					Color pixelColor = image.GetPixel(w, h);
					//Average out the RGB components to find the Gray Color
					int red = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
					int green = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
					int blue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
					Color grayColor = Color.FromArgb(red, green, blue);

					//Use the toggle flag to minimize height-wise stretch
					if (!toggle)
					{
						int index = (grayColor.R * 10) / 255;
						sb.Append(_AsciiChars[index]);
					}
				}
				if (!toggle)
				{
					sb.Append("<BR>");
					toggle = true;
				}
				else
				{
					toggle = false;
				}
			}

			return sb.ToString();
		}


		private Bitmap GetReSizedImage(Bitmap inputBitmap, int asciiWidth)
		{
			int asciiHeight = 0;
			//Calculate the new Height of the image from its width
			asciiHeight = (int)Math.Ceiling((double)inputBitmap.Height * asciiWidth / inputBitmap.Width);

			//Create a new Bitmap and define its resolution
			Bitmap result = new Bitmap(asciiWidth, asciiHeight);
			Graphics g = Graphics.FromImage((Image)result);
			//The interpolation mode produces high quality images 
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(inputBitmap, 0, 0, asciiWidth, asciiHeight);
			g.Dispose();
			return result;
		}



		//private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		//{
		//    saveFileDialog1.Filter = "Text File (*.txt)|.txt|HTML (*.htm)|.htm";
		//    DialogResult diag = saveFileDialog1.ShowDialog();
		//    if (diag == DialogResult.OK)
		//    {
		//        if (saveFileDialog1.FilterIndex == 1)
		//        {
		//            //If the format to be saved is HTML
		//            //Replace all HTML spaces to standard spaces
		//            //and all linebreaks to CarriageReturn, LineFeed
		//            _Content = _Content.Replace("&nbsp;", " ").Replace("<BR>","\r\n");
		//        }
		//        else
		//        {
		//            //use <pre></pre> tag to preserve formatting when viewing it in browser
		//            _Content = "<pre>" + _Content + "</pre>";
		//        }
		//        StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
		//        sw.Write(_Content);
		//        sw.Flush();
		//        sw.Close();
		//    }
		//}

	}
}