using RedditTextToSpeech.Core.Interfaces;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    /// <summary>
    /// The image service.
    /// Builds simple images which are used to represent different parts of a reddit post.
    /// </summary>
    public class ImageService : IImageService
    {
        private int maxWidth;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageService"/> class.
        /// </summary>
        /// <param name="maxWidth">The max width.</param>
        public ImageService(int maxWidth = 400)
        {
            this.maxWidth = maxWidth;
        }

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        public string Extension => ".png";

        /// <summary>
        /// Gets a image representing a reddit comment start.
        /// </summary>
        /// <param name="path">The image path to save to.</param>
        /// <param name="text">The text to display.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="subreddit">The subreddit.</param>
        /// <param name="avatar">Link to users avatar.</param>
        /// <returns>Awaitable task returning path.</returns>
        public async Task<string> GetImage(string path, IComment comment)
        {
            await Task.Run(() =>
            {
                var font = new Font(new FontFamily("Arial"), 17, FontStyle.Regular, GraphicsUnit.Pixel);
                var topFont = new Font(new FontFamily("Arial"), 14, FontStyle.Regular, GraphicsUnit.Pixel);
                var retBitmapGraphics = Graphics.FromImage(new Bitmap(1, 1));

                var bitmapHeight = (int)retBitmapGraphics.MeasureString(comment.First, font, maxWidth).Height;
                var usernameHeight = (int)retBitmapGraphics.MeasureString(comment.Username, topFont, maxWidth).Height;

                var rect = new Rectangle(0, 0, maxWidth + 10, bitmapHeight + usernameHeight + 5);
                using var graphicsPath = this.CreatePath(rect, 15, true);
                var retBitmap = new Bitmap(rect.Width, rect.Height);
                retBitmap.MakeTransparent();
                retBitmapGraphics = Graphics.FromImage(retBitmap);
                retBitmapGraphics.FillPath(new SolidBrush(Color.FromArgb(220, 26, 26, 27)), graphicsPath);
                retBitmapGraphics.DrawPath(new Pen(Color.FromArgb(220, 7, 7, 7), 1), graphicsPath);
                retBitmapGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                retBitmapGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                retBitmapGraphics.DrawString($"u/{comment.Username}", topFont, new SolidBrush(Color.FromArgb(127, 159, 227)),
                        new RectangleF(5, 5, maxWidth, usernameHeight));
                retBitmapGraphics.DrawString(comment.First, font, new SolidBrush(Color.FromArgb(215, 218, 220)),
                    new RectangleF(5, 5 + usernameHeight, maxWidth, bitmapHeight));
                retBitmapGraphics.Flush();

                retBitmap.Save(path, ImageFormat.Png);
            });
            return path;
        }

        /// <summary>
        /// Gets a image representing a reddit title.
        /// </summary>
        /// <param name="path">The image path to save to.</param>
        /// <param name="title">The title of the post.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="subreddit">The subreddit.</param>
        /// <returns>Awaitable task returning path.</returns>
        public async Task<string> GetImage(string path, IPost post)
        {
            await Task.Run(() =>
            {
                var font = new Font(new FontFamily("Arial"), 20, FontStyle.Bold, GraphicsUnit.Pixel);
                var topFont = new Font(new FontFamily("Arial"), 14, FontStyle.Regular, GraphicsUnit.Pixel);
                using var retBitmapGraphics = Graphics.FromImage(new Bitmap(1, 1));

                var bitmapHeight = (int)retBitmapGraphics.MeasureString(post.Title, font, maxWidth).Height;
                var usernameHeight = (int)retBitmapGraphics.MeasureString(post.Username, topFont, maxWidth).Height;
                var subredditHeight = (int)retBitmapGraphics.MeasureString(post.Subreddit, topFont, maxWidth).Height;

                var rect = new Rectangle(0, 0, maxWidth + 10, bitmapHeight + usernameHeight + subredditHeight + 5);
                using var graphicsPath = this.CreatePath(rect, 15, true);
                using var retBitmap = new Bitmap(rect.Width, rect.Height);
                retBitmap.MakeTransparent();
                using var retBitmapGraphicsDraw = Graphics.FromImage(retBitmap);
                retBitmapGraphicsDraw.FillPath(new SolidBrush(Color.FromArgb(220, 26, 26, 27)), graphicsPath);
                retBitmapGraphicsDraw.DrawPath(new Pen(Color.FromArgb(220, 7, 7, 7), 1), graphicsPath);
                retBitmapGraphicsDraw.SmoothingMode = SmoothingMode.AntiAlias;
                retBitmapGraphicsDraw.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                retBitmapGraphicsDraw.DrawString($"u/{post.Username}", topFont, new SolidBrush(Color.FromArgb(127, 159, 227)),
                        new RectangleF(5, 5 + subredditHeight, maxWidth, usernameHeight));
                retBitmapGraphicsDraw.DrawString($"r/{post.Subreddit}", topFont, new SolidBrush(Color.FromArgb(129, 131, 132)),
                        new RectangleF(5, 5, maxWidth, subredditHeight));
                retBitmapGraphicsDraw.DrawString(post.Title, font, new SolidBrush(Color.FromArgb(215, 218, 220)),
                    new RectangleF(5, usernameHeight + subredditHeight + 5, maxWidth, bitmapHeight));
                retBitmapGraphicsDraw.Flush();

                retBitmap.Save(path, ImageFormat.Png);
            });
            return path;
        }

        /// <summary>
        /// Gets a blank text image.
        /// </summary>
        /// <param name="path">The image path to save to.</param>
        /// <param name="text">The text to display.</param>
        /// <returns>Awaitable task returning path.</returns>
        public async Task<string> GetImage(string path, string text)
        {
            await Task.Run(() =>
            {
                var font = new Font(new FontFamily("Arial"), 17, FontStyle.Regular, GraphicsUnit.Pixel);
                var retBitmapGraphics = Graphics.FromImage(new Bitmap(1, 1));

                var bitmapHeight = (int)retBitmapGraphics.MeasureString(text, font, maxWidth).Height;

                var rect = new Rectangle(0, 0, maxWidth + 10, bitmapHeight + 10);
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

                retBitmap.Save(path, ImageFormat.Png);
            });
            return path;
        }

        /// <summary>
        /// Creates a path.
        /// </summary>
        /// <param name="rect">The path rect representing size and location.</param>
        /// <param name="radius">The corner radius.</param>
        /// <param name="hasOutline">Should an outline be applied.</param>
        /// <returns>Graph path instance.</returns>
        private GraphicsPath CreatePath(Rectangle rect, int radius, bool hasOutline)
        {
            int nShift = hasOutline ? 1 : 0;
            var path = new GraphicsPath();
            path.AddArc(rect.X + nShift, rect.Y, radius, radius, 180f, 90f);
            path.AddArc((rect.Right - radius) - nShift, rect.Y, radius, radius, 270f, 90f);
            path.AddArc((rect.Right - radius) - nShift, (rect.Bottom - radius) - nShift, radius, radius, 0f, 90f);
            path.AddArc(rect.X + nShift, (rect.Bottom - radius) - nShift, radius, radius, 90f, 90f);
            path.CloseFigure();
            return path;
        }
    }
}