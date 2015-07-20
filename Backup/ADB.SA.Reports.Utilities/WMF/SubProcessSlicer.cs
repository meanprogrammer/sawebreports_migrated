using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Utilities.WMF
{
    public class SubProcessSlicer
    {
        /// <summary>
        /// Internal variable for wmfPath.
        /// </summary>
        private string wmfPath = string.Empty;

        /// <summary>
        /// Constructor of the class taking up wmfPath as parameter.
        /// </summary>
        /// <param name="wmfPath"></param>
        public SubProcessSlicer(string wmfPath)
        {
            this.wmfPath = PathResolver.MapPath(wmfPath);
        }

        /// <summary>
        /// Slices the WMF image per pool.
        /// </summary>
        /// <returns>Returns an array of string with the path of sliced images.</returns>
        public string[] Slice()
        {
            if (string.IsNullOrEmpty(this.wmfPath))
            {
                throw new InvalidOperationException(GlobalStringResource.IMAGE_PATH_NOT_SUPPLIED);
            }

            List<int> whites = new List<int>();
            int x = 0;
            int width = 0;
            int height = 0;
            using (Bitmap baseImage = (Bitmap)Bitmap.FromFile(this.wmfPath))
            {
                height = baseImage.Height;
                width = baseImage.Width;
                for (int i = 0; i < baseImage.Height; i++)
                {
                    System.Drawing.Color c = baseImage.GetPixel(x, i);
                    if (c.A == 255 && c.B == 255 && c.R == 255 && c.G == 255)
                    {
                        if (i == 0) { continue; }
                        whites.Add(i);
                    }

                }
            }
            whites.Sort();
            List<System.Drawing.Rectangle> rectangles = CreateCoordinates(whites, width, height);
            return CropAndSave(rectangles);
        }

        /// <summary>
        /// Crops the rectagles.
        /// </summary>
        /// <param name="rectangles">Coordinates for cropping.</param>
        /// <returns>Path of the saved images in array.</returns>
        private string[] CropAndSave(List<System.Drawing.Rectangle> rectangles)
        {
            int fileNameCount = 0;
            string currentWorkingDirectory = string.Empty;
            List<string> imageFilenames = new List<string>();
            
            string targetFolderName = Path.GetFileNameWithoutExtension(this.wmfPath);
            string targetDirectoryPath = string.Format(PathResolver.MapPath(@"Diagrams\Sliced\{0}"), targetFolderName);

            if (Directory.Exists(targetDirectoryPath))
            {
                Directory.Delete(targetDirectoryPath, true);
            }

            DirectoryInfo info = Directory.CreateDirectory(targetDirectoryPath);
            currentWorkingDirectory = info.FullName;

            foreach (System.Drawing.Rectangle rect in rectangles)
            {
                
                Bitmap croppedImage = (Bitmap)Crop(this.wmfPath, rect.Width, rect.Height, rect.X, rect.Y);

                fileNameCount++;

                string fullNamePath = string.Format(@"{0}\{1}.WMF", currentWorkingDirectory, fileNameCount);
                croppedImage.Save(fullNamePath, ImageFormat.Wmf);
                imageFilenames.Add(fullNamePath);
                croppedImage.Dispose();
            }
            return imageFilenames.ToArray();
        }

        /// <summary>
        /// Creates the coordinates for cropping.
        /// </summary>
        /// <param name="whites">Coordinates of white pixels at the edge of the image.</param>
        /// <param name="width">Width that will be cropped.</param>
        /// <param name="height">Height that will be cropped.</param>
        /// <returns>List of Coordinates.</returns>
        private List<System.Drawing.Rectangle> CreateCoordinates(List<int> whites, int width, int height)
        {
            List<System.Drawing.Rectangle> rectangles = new List<System.Drawing.Rectangle>();
            for (int i = 0; i < whites.Count; i++)
            {
                if (i == 0)
                {
                    rectangles.Add(new System.Drawing.Rectangle(0, i, width, whites[i]));
                }
                else
                {
                    if (whites[i] == whites.Last())
                    {
                        int croppedHeight = height - whites[i];
                        rectangles.Add(new System.Drawing.Rectangle(0, whites[i], width, croppedHeight));
                        continue;
                    }

                    if (((whites[i] - 1) == whites[i - 1]) && ((whites[i] + 1) == whites[i + 1]))
                    {
                        continue;
                    }
                    else
                    {
                        int croppedHeight = whites[i + 1] - whites[i];
                        rectangles.Add(new System.Drawing.Rectangle(0, whites[i], width, croppedHeight));
                        i++;
                    }
                }
            }
            return rectangles;
        }

        /// <summary>
        /// Crops the image by all the parameters.
        /// </summary>
        /// <param name="img">Image instance.</param>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="x">x position.</param>
        /// <param name="y">y position.</param>
        /// <returns>Returns a cropped image.</returns>
        private System.Drawing.Image Crop(string img, int width, int height, int x, int y)
        {
            try
            {
                System.Drawing.Image baseImage = System.Drawing.Image.FromFile(img);
                Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                bmp.SetResolution(80, 60);

                Graphics graphicsContainer = Graphics.FromImage(bmp);
                graphicsContainer.SmoothingMode = SmoothingMode.AntiAlias;
                graphicsContainer.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsContainer.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphicsContainer.DrawImage(baseImage, new System.Drawing.Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
                // Dispose to free up resources
                baseImage.Dispose();
                graphicsContainer.Dispose();

                return bmp;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
