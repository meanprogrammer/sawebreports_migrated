using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Presenter.Content;
using ADB.SA.Reports.Utilities.WMF;
using System.IO;
using ADB.SA.Reports.Utilities;
using ADB.SA.Reports.Presenter.Report;
using iTextSharp.text.pdf;
using ADB.SA.Reports.Global;

namespace ADB.SA.Reports.Presenter
{
    public class GenerateReportPresenter
    {
        IGenerateReportView view;
        EntityData entityData = null;
        FileData filesData = null;
        WmfImageManager imageManager;

        public GenerateReportPresenter(IGenerateReportView view)
        {
            this.view = view;
            this.entityData = new EntityData();
            this.filesData = new FileData();
            
        }

        public void GenerateReport(int id)
        {
            EntityDTO dto = entityData.GetOneEntity(id);
            dto.ExtractProperties();
            
            FileDTO file = filesData.GetFile(dto.DGXFileName);

            byte[] diagram = file.Data;
            //Save the raw .wmf file
            int poolCount = entityData.GetPoolCount(dto.ID);


            this.imageManager = new WmfImageManager(dto, diagram, dto.DGXFileName, dto.Type, poolCount, true );

            string resizedDiagram = imageManager.SaveAndResizeImage(diagram, dto.DGXFileName, dto.Type,
                                    poolCount, true);

            string[] images = new string[] { PathResolver.MapPath(resizedDiagram) };

            if (poolCount > 1 && dto.Type == 142)
            {
                //slice the wmf
                SubProcessSlicer slicer = new SubProcessSlicer(resizedDiagram);
                images = slicer.Slice();
            }

            List<PdfContentParameter> contents = ReportBuilder.BuildReport(dto);
            byte[] pdfBytes = PDFBuilder.CreatePDF(contents, images, dto.Type);
            this.view.RenderReport(pdfBytes);
        }
    }
}
