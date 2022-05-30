using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    public class ImageService : IImageService
    {
        private int maxWidth;

        public ImageService(int maxWidth)
        {
            this.maxWidth = maxWidth;
        }

        public string Extension => ".png";

        public async Task<string> GetImage(string path, string title, string username, string subreddit)
        {
            await Task.Run(() =>
            {
                var font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold, GraphicsUnit.Pixel);
                var topFont = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular, GraphicsUnit.Pixel);
                var retBitmapGraphics = Graphics.FromImage(new Bitmap(1, 1));

                var bitmapHeight = (int)retBitmapGraphics.MeasureString(title, font, maxWidth).Height;
                var usernameHeight = (int)retBitmapGraphics.MeasureString(username, topFont, maxWidth).Height;
                var subredditHeight = (int)retBitmapGraphics.MeasureString(subreddit, topFont, maxWidth).Height;

                var rect = new Rectangle(0, 0, maxWidth + 10, bitmapHeight + usernameHeight + subredditHeight + 5);
                using var graphicsPath = this.CreatePath(rect, 15, true);
                var retBitmap = new Bitmap(rect.Width, rect.Height);
                retBitmap.MakeTransparent();
                retBitmapGraphics = Graphics.FromImage(retBitmap);
                retBitmapGraphics.FillPath(new SolidBrush(Color.FromArgb(220, 26, 26, 27)), graphicsPath);
                retBitmapGraphics.DrawPath(new Pen(Color.FromArgb(220, 7, 7, 7), 1), graphicsPath);
                retBitmapGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                retBitmapGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                retBitmapGraphics.DrawString(username, topFont, new SolidBrush(Color.FromArgb(127, 159, 227)),
                        new RectangleF(5, 5 + subredditHeight, maxWidth, usernameHeight));
                retBitmapGraphics.DrawString(subreddit, topFont, new SolidBrush(Color.FromArgb(129, 131, 132)),
                        new RectangleF(5, 5, maxWidth, subredditHeight));
                retBitmapGraphics.DrawString(title, font, new SolidBrush(Color.FromArgb(215, 218, 220)),
                    new RectangleF(5, usernameHeight + subredditHeight + 5, maxWidth, bitmapHeight));
                retBitmapGraphics.Flush();

                retBitmap.Save(path + this.Extension, ImageFormat.Png);
            });
            return path + this.Extension;
        }

        public async Task<string> GetImage(string path, string text)
        {
            await Task.Run(() =>
            {
                var font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold, GraphicsUnit.Pixel);
                var retBitmapGraphics = Graphics.FromImage(new Bitmap(1, 1));

                var bitmapHeight = (int)retBitmapGraphics.MeasureString(text, font, maxWidth).Height;

                var rect = new Rectangle(0, 0, maxWidth + 10, bitmapHeight + 5);
                using var graphicsPath = this.CreatePath(rect, 15, true);
                var retBitmap = new Bitmap(rect.Width, rect.Height);
                retBitmap.MakeTransparent();
                retBitmapGraphics = Graphics.FromImage(retBitmap);
                retBitmapGraphics.FillPath(new SolidBrush(Color.FromArgb(220, 26, 26, 27)), graphicsPath);
                retBitmapGraphics.DrawPath(new Pen(Color.FromArgb(220, 7, 7, 7), 1), graphicsPath);
                retBitmapGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                retBitmapGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                retBitmapGraphics.DrawString(text, font, new SolidBrush(Color.FromArgb(215, 218, 220)),
                    new RectangleF(5, 5, maxWidth, bitmapHeight));
                retBitmapGraphics.Flush();

                retBitmap.Save(path + this.Extension, ImageFormat.Png);
            });
            return path + this.Extension;
        }

        private GraphicsPath CreatePath(Rectangle rect, int nRadius, bool bOutline)
        {
            int nShift = bOutline ? 1 : 0;
            var path = new GraphicsPath();
            path.AddArc(rect.X + nShift, rect.Y, nRadius, nRadius, 180f, 90f);
            path.AddArc((rect.Right - nRadius) - nShift, rect.Y, nRadius, nRadius, 270f, 90f);
            path.AddArc((rect.Right - nRadius) - nShift, (rect.Bottom - nRadius) - nShift, nRadius, nRadius, 0f, 90f);
            path.AddArc(rect.X + nShift, (rect.Bottom - nRadius) - nShift, nRadius, nRadius, 90f, 90f);
            path.CloseFigure();
            return path;
        }
    }
}