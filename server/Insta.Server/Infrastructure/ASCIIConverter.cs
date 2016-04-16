using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Text;

/// <copyright>
/// This code may be used and distributed freely. No warranty is made by the
/// author, implied or otherwise. This code comes as-is with no guarantee.
/// (c) 2007 by Matthew Martin. http://www.usualdosage.com.
/// </copyright>

/// <summary>
/// Contains various static methods for converting an image to an HTML document
/// containing an ASCII representation of the image.
/// </summary>
public class ASCIIConverter
{
    #region [ Private Members ]

    /// <summary>
    /// These constants are used to convert an image to a "true" ASCII drawing.
    /// Each constant is selected by it's relative grayscale value to represent
    /// a single pixel in a bitmap.
    /// </summary>
    
    private const string BLACK = "@";
    private const string CHARCOAL = "#";
    private const string DARKGRAY = "8";
    private const string MEDIUMGRAY = "&";
    private const string MEDIUM = "o";
    private const string GRAY = ":";
    private const string SLATEGRAY = "*";
    private const string LIGHTGRAY = ".";
    private const string WHITE = "&nbsp;";

    #endregion 

    #region [ Private Methods ]

    /// <summary>
    /// Macro to return one of the ASCII constants based on the relative
    /// "darkness" of the red value.
    /// </summary>
    /// <param name="redValue">The red value of the pixel</param>
    /// <returns>System.String</returns>
    private static string getGrayShade(int redValue)
    {
        string asciival = "&nbsp;";

        if (redValue >= 230)
        {
            asciival = WHITE;
        }
        else if (redValue >= 200)
        {
            asciival = LIGHTGRAY;
        }
        else if (redValue >= 180)
        {
            asciival = SLATEGRAY;
        }
        else if (redValue >= 160)
        {
            asciival = GRAY;
        }
        else if (redValue >= 130)
        {
            asciival = MEDIUM;
        }
        else if (redValue >= 100)
        {
            asciival = MEDIUMGRAY;
        }
        else if (redValue >= 70)
        {
            asciival = DARKGRAY;
        }
        else if (redValue >= 50)
        {
            asciival = CHARCOAL;
        }
        else
        {
            asciival = BLACK;
        }

        return asciival;
    }

    #endregion

    #region [ Public Methods ]

    /// <summary>
    /// Converts an image (jpg, gif, etc) to a colored ASCII drawing using
    /// the specified input character.
    /// </summary>
    /// <param name="img">The input image.</param>
    /// <param name="strInputChar">The character to use in the drawing.</param>
    /// <returns>HTML formatted text</returns>
    public static string ImageToText(System.Drawing.Image img, string strInputChar)
    {
        Bitmap bmp = null;
        StringBuilder html = new StringBuilder();

        try
        {
            // Create a bitmap from the image
            bmp = new Bitmap(img);

            // The text will be enclosed in a paragraph tag <p> with the class
            // ascii_art so that we can apply CSS styles to it.
            html.Append("<p class='ascii_art'>");

            // Loop through each pixel in the bitmap
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    // Nest each character inside an anchor tag so we can use
                    // inline styles to set the color.
                    html.Append("<a style='color: ");

                    Color fontColor = bmp.GetPixel(x, y);

                    // Convert the color to hexadecimal
                    string color = System.Drawing.ColorTranslator.ToHtml(fontColor);

                    html.Append(color + ";'>" + strInputChar + "</a>");

                    // If we're at the width, insert a line break
                    if (x == bmp.Width - 1)
                        html.Append("<br/>");
                }
            }

            // Close the paragraph tag, and return the html string.
            html.Append("</p>");
            return html.ToString();
        }
        catch (Exception exc)
        {
            return exc.ToString();
        }
        finally
        {
            bmp.Dispose();
        }
    }

    /// <summary>
    /// Converts an image (jpg, gif, etc) to a grayscale ASCII drawing using
    /// the specified input character.
    /// </summary>
    /// <param name="img">The input image.</param>
    /// <param name="strInputChar">The character to use in the drawing.</param>
    /// <returns>HTML formatted text</returns>
    public static string GrayscaleImageToText(System.Drawing.Image img, string strInputChar)
    {
        StringBuilder html = new StringBuilder();
        Bitmap bmp = null;

        try
        {
            // Create a bitmap from the image
            bmp = new Bitmap(img);
            
            // The text will be enclosed in a paragraph tag <p> with the class
            // ascii_art so that we can apply CSS styles to it.
            html.Append("<p class='ascii_art'>");

            // Loop through each pixel in the bitmap
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    // Get the color of the current pixel
                    Color col = bmp.GetPixel(x, y);

                    // To convert to grayscale, the easiest method is to add
                    // the R+G+B colors and divide by three to get the gray
                    // scaled color.
                    bmp.SetPixel(x, y,
                        Color.FromArgb((col.R + col.G + col.B) / 3,
                        (col.R + col.G + col.B) / 3,
                        (col.R + col.G + col.B) / 3));

                    // Nest each character inside an anchor tag so we can use
                    // inline styles to set the color.
                    html.Append("<a style='color: ");

                    // Get the new pixel color since we changed it to gray
                    Color fontColor = bmp.GetPixel(x, y);
                    string color = System.Drawing.ColorTranslator.ToHtml(fontColor);
                    html.Append(color + ";'>" + strInputChar + "</a>");

                    // If we're at the width, insert a line break
                    if (x == bmp.Width - 1)
                        html.Append("<br/>");
                }
            }

            // Close the paragraph tag, and return the html string.
            html.Append("</p>");
            return html.ToString();
        }
        catch (Exception exc)
        {
            return exc.ToString();
        }
        finally
        {
            bmp.Dispose();
        }
    }

    public static string GrayscaleImageToASCII(System.Drawing.Image img)
    {
        StringBuilder html = new StringBuilder();
        Bitmap bmp = null;

        try
        {
            // Create a bitmap from the image
            bmp = new Bitmap(img);

            // The text will be enclosed in a paragraph tag <p> with the class
            // ascii_art so that we can apply CSS styles to it.
            html.Append("<p class='ascii_art'>");

            // Loop through each pixel in the bitmap
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    // Get the color of the current pixel
                    Color col = bmp.GetPixel(x, y);

                    // To convert to grayscale, the easiest method is to add
                    // the R+G+B colors and divide by three to get the gray
                    // scaled color.
                    col = Color.FromArgb((col.R + col.G + col.B) / 3,
                        (col.R + col.G + col.B) / 3,
                        (col.R + col.G + col.B) / 3);

                    // Get the R(ed) value from the grayscale color,
                    // parse to an int. Will be between 0-255.
                    int rValue = int.Parse(col.R.ToString());

                    // Append the "color" using various darknesses of ASCII
                    // character.
                    html.Append(getGrayShade(rValue));

                    // If we're at the width, insert a line break
                    if (x == bmp.Width - 1)
                        html.Append("<br/>");
                }
            }

            // Close the paragraph tag, and return the html string.
            html.Append("</p>");

            return html.ToString();
        }
        catch (Exception exc)
        {
            return exc.ToString();
        }
        finally
        {
            bmp.Dispose();
        }
    }

    #endregion
}
