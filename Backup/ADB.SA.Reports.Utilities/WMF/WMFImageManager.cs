using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using ADB.SA.Reports.Global;
using ADB.SA.Reports.Entities.DTO;

namespace ADB.SA.Reports.Utilities.WMF
{
    public class WmfImageManager : ImageManagerBase
    {
        private EntityDTO dto;
        private byte[] data;
        private string fileName;
        private int type;
        private int poolCount;
        private bool isReport;
        private float newpercentage;

        public WmfImageManager(EntityDTO dto)
        {
            this.dto = dto;
        }

        public WmfImageManager(EntityDTO dto, byte[] data, string fileName,
            int type, int poolCount, bool isReport)
        {
            this.dto = dto;
            this.data = data;
            this.fileName = fileName;
            this.type = type;
            this.poolCount = poolCount;
            this.isReport = isReport;
        }

        public WmfImageManager(EntityDTO dto, byte[] data, string fileName,
            int type, int poolCount, bool isReport, float newpercentage)
        {
            this.dto = dto;
            this.data = data;
            this.fileName = fileName;
            this.type = type;
            this.poolCount = poolCount;
            this.isReport = isReport;
            this.newpercentage = newpercentage;
        }

        public override string ProcessImage()
        {
            if (this.data != null)
            {
                SaveRawFile();
                return ResizeImage();
            }
            else
            {
                return string.Empty;
            }
        }

        public string ProcessImageWithPercentage(float percentage)
        {
            if (this.data != null)
            {
                SaveRawFile();
                return ResizeImage(percentage);
            }
            else
            {
                return string.Empty;
            }
        }

        public string SaveAndResizeImage(byte[] data, string fileName, 
            int type, int poolCount, bool isReport)
        {
            if (data != null)
            {
                SaveRawFile();
                return ResizeImage();
            }
            else
            {
                return string.Empty;
            }
        }

        private void SaveRawFile()
        {
            File.WriteAllBytes(
                PathResolver.MapPath(
                    string.Format(@"Diagrams\wmf_temp\{0}", this.fileName)), 
                    this.data
                );
        }

        private string ResizeImage()
        {

            Image imgToResize = Image.FromFile(
                    PathResolver.MapPath(
                        string.Format(@"Diagrams\wmf_temp\{0}", this.fileName)) 
                                );

            float nPercent = 0;
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            ResizeStrategyParameter parameter = new ResizeStrategyParameter() 
            {
                Type = type,
                Width = sourceWidth,
                Height = sourceHeight,
                PoolCount = poolCount,
                IsReport = isReport,
            };

            if (this.dto.DiagramPercentage <= 0 || this.dto.DiagramPercentage == 5.0f)
            {
                nPercent = DiagramResizer.GetPercentage(parameter);
            }
            else
            {
                nPercent = (float)this.dto.DiagramPercentage;
            }

            int destWidth = (int)(sourceWidth * (nPercent * .01));
            int destHeight = (int)(sourceHeight * (nPercent * .01));

            Bitmap plain = new Bitmap(destWidth, destHeight);

            Graphics imageContainer = Graphics.FromImage((Image)plain);
            imageContainer.InterpolationMode = InterpolationMode.HighQualityBicubic;
            imageContainer.CompositingMode = CompositingMode.SourceCopy;
            imageContainer.CompositingQuality = CompositingQuality.HighQuality;
            imageContainer.SmoothingMode = SmoothingMode.HighQuality;
            imageContainer.PixelOffsetMode = PixelOffsetMode.HighQuality;
            imageContainer.Clear(Color.White);
            imageContainer.DrawImage(imgToResize, 0, 0, destWidth, destHeight);

            imageContainer.Dispose();
            imgToResize.Dispose();
            
            string fileName = FileManager.GetFileName(this.fileName);


            //excess white space removal
            Image cropped = WhiteSpaceRemover.AutoCrop(plain);
            plain = (Bitmap)cropped;
            //excess white space removal

            string savePath = string.Empty;

            if (!isReport)
            {
                savePath = string.Format(@"{0}\{1}",
                    PathResolver.MapPath(GlobalStringResource.DiagramsFolder),
                    fileName);
            }
            else
            {
                savePath = string.Format(@"{0}\{1}",
                    PathResolver.MapPath(GlobalStringResource.SlicedResizedFolder), 
                    fileName);
            }

            plain.Save(savePath);

            cropped.Dispose();
            plain.Dispose();

            return string.Format(
                GlobalStringResource.ImageSavePathFormat,
                isReport ? GlobalStringResource.SlicedResizedFolder : GlobalStringResource.DiagramsFolder,
                fileName);
        }

        private string ResizeImage(float percent)
        {

            Image imgToResize = Image.FromFile(
                    PathResolver.MapPath(
                        string.Format(@"Diagrams\wmf_temp\{0}", this.fileName))
                                );

            float nPercent = 0;
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            ResizeStrategyParameter parameter = new ResizeStrategyParameter()
            {
                Type = type,
                Width = sourceWidth,
                Height = sourceHeight,
                PoolCount = poolCount,
                IsReport = isReport,
            };


            nPercent = percent;


            int destWidth = (int)(sourceWidth * (nPercent * .01));
            int destHeight = (int)(sourceHeight * (nPercent * .01));

            Bitmap plain = new Bitmap(destWidth, destHeight);

            Graphics imageContainer = Graphics.FromImage((Image)plain);
            imageContainer.InterpolationMode = InterpolationMode.HighQualityBicubic;
            imageContainer.CompositingMode = CompositingMode.SourceCopy;
            imageContainer.CompositingQuality = CompositingQuality.HighQuality;
            imageContainer.SmoothingMode = SmoothingMode.HighQuality;
            imageContainer.PixelOffsetMode = PixelOffsetMode.HighQuality;
            imageContainer.Clear(Color.White);
            imageContainer.DrawImage(imgToResize, 0, 0, destWidth, destHeight);

            imageContainer.Dispose();
            imgToResize.Dispose();

            string fileName = FileManager.GetFileName(this.fileName);


            //excess white space removal
            Image cropped = WhiteSpaceRemover.AutoCrop(plain);
            plain = (Bitmap)cropped;
            //excess white space removal

            string savePath = string.Empty;

            if (!isReport)
            {
                savePath = string.Format(@"{0}\{1}",
                    PathResolver.MapPath(GlobalStringResource.DiagramsFolder),
                    fileName);
            }
            else
            {
                savePath = string.Format(@"{0}\{1}",
                    PathResolver.MapPath(GlobalStringResource.SlicedResizedFolder),
                    fileName);
            }

            plain.Save(savePath);

            cropped.Dispose();
            plain.Dispose();

            return string.Format(
                GlobalStringResource.ImageSavePathFormat,
                isReport ? GlobalStringResource.SlicedResizedFolder : GlobalStringResource.DiagramsFolder,
                fileName);
        }
    }
}
