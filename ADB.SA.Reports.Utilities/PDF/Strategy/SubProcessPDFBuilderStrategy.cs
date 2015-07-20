using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Utilities
{
    public class SubProcessPDFBuilderStrategy : PDFBuilderStrategyBase
    {
        public override byte[] CreatePdf(List<PdfContentParameter> contents, string[] images, int type)
        {
            var document = new Document();
            float docHeight = document.PageSize.Height - heightOffset;
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            document.SetMargins(50, 50, 10, 40);

            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            writer.PageEvent = new HeaderFooterHandler(type);
            document.Open();

            //Initialize templates from contents
            InitializeTemplatesForTOC(contents, writer);

            //add the cover page
            document.Add(contents[0].Table);

            //write the change history,reviewers and approvers
            document.NewPage();
            for (int i = 1; i <= 3; i++)
            {
                document.Add(contents[i].Table);
            }

            //write the table of contents
            WriteTOC(contents, writer, document);

            //objectives, roles and resp and sub process dependency
            document.NewPage();
            for (int i = 4; i <= 6; i++)
            {
                contents[i].PageNumber = writer.PageNumber;
                document.Add(contents[i].Table);
            }

            //write the images
            for (int i = 0; i < images.Length; i++)
            {
                document.NewPage();
                float subtrahend = document.PageSize.Height - heightOffset;
                iTextSharp.text.Image pool = iTextSharp.text.Image.GetInstance(images[i]);
                if (i == 0)
                {
                    contents[7].PageNumber = writer.PageNumber;
                    document.Add(contents[7].Table);
                    subtrahend -= contents[7].Table.TotalHeight;
                }
                pool.Alignment = 3;
                pool.ScaleToFit(document.PageSize.Width - (document.RightMargin * 2), subtrahend);
                //pool.ScaleAbsolute(document.PageSize.Width - (document.RightMargin * 2), subtrahend);
                document.Add(pool);
            }

            //document.NewPage();

            //write all the activity overview
            for (int i = 8; i < contents.Count; i++)
            {
                //the first item is the header
                //the header must be with the first activity
                if (i == 8)
                {
                    document.NewPage();
                    contents[i].PageNumber = writer.PageNumber;
                    document.Add(contents[i].Table);
                    try
                    {
                        contents[i + 1].PageNumber = writer.PageNumber;
                        document.Add(contents[i + 1].Table);
                        i++;

                        continue;
                    }
                    catch
                    {
                        continue;
                    }
                }
                document.NewPage();
                contents[i].PageNumber = writer.PageNumber;
                document.Add(contents[i].Table);
            }

            //write the values for the templates
            WritePageNumbers(contents);

            //cleanup images
            CleanUp(images);

            //close document
            document.Close();
            return output.ToArray();
        }

        private void WritePageNumbers(List<PdfContentParameter> contents)
        {
            foreach (PdfContentParameter item in contents)
            {
                if (item.PageNumber > 0)
                {
                    ColumnText.ShowTextAligned(item.Template, Element.ALIGN_RIGHT,
                        new Phrase(item.PageNumber.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8)),
                        10, 7, 0);
                }
            }
        }

        public override void CleanUp(string[] images)
        {
            if (images.Length <= 0)
            {
                return;
            }

            if (images.Length == 1)
            {
                File.Delete(images[0]);
                return;
            }

            string slicedDirectory = Path.GetDirectoryName(images[0]);
            string parentImagePath = string.Empty;
            if (!string.IsNullOrEmpty(slicedDirectory))
            {
                parentImagePath = slicedDirectory.Split(new string[] { GlobalStringResource.PathSpliterDash }, 
                    StringSplitOptions.RemoveEmptyEntries).Last();

                Directory.Delete(slicedDirectory, true);
            }

            if (!string.IsNullOrEmpty(parentImagePath))
            {
                parentImagePath = string.Format(
                    PathResolver.MapPath(GlobalStringResource.SlicedDirectoryPath),
                    parentImagePath
                    );
                File.Delete(parentImagePath);
            }
        }
    }
}
