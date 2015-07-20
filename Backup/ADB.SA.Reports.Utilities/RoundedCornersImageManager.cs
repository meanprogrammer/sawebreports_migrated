using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Utilities
{
    public class RoundedCornersImageManager
    {
        public static void CreateImage(string color)
        {
            Bitmap RoundedImage = new Bitmap(96, 61);
            Graphics g = Graphics.FromImage(RoundedImage);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;
            Color cc = (Color)new ColorConverter().ConvertFromString(color);
            DrawRoundedRectangle(g, new Rectangle(0, 0, 96, 61), 15, new Pen(new SolidBrush(Color.Black)), cc);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            string basePath = PathResolver.MapPath("images");
            color = color.Remove(0, 1);
            RoundedImage.Save(string.Format(@"{0}\{1}.png",basePath, color), ImageFormat.Png);
        }

        private static void DrawRoundedRectangle(Graphics gfx, Rectangle Bounds, int CornerRadius, Pen DrawPen, Color FillColor)
        {
            int strokeOffset = Convert.ToInt32(Math.Ceiling(DrawPen.Width));
            Bounds = Rectangle.Inflate(Bounds, -strokeOffset, -strokeOffset);

            DrawPen.EndCap = DrawPen.StartCap = LineCap.Round;

            GraphicsPath gfxPath = new GraphicsPath();
            gfxPath.AddArc(Bounds.X, Bounds.Y, CornerRadius, CornerRadius, 180, 90);
            gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y, CornerRadius, CornerRadius, 270, 90);
            gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
            gfxPath.AddArc(Bounds.X, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
            gfxPath.CloseAllFigures();

            gfx.FillPath(new SolidBrush(FillColor), gfxPath);
            gfx.DrawPath(DrawPen, gfxPath);
        }
    }



}
