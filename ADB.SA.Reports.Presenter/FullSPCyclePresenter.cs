using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.View;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Entities.DTO;
using ADB.SA.Reports.Utilities.WMF;
using ADB.SA.Reports.Global;
using System.IO;

namespace ADB.SA.Reports.Presenter
{
    public class FullSPCyclePresenter
    {
        EntityData eData = new EntityData();
        EndToEndSubProcessData spData = new EndToEndSubProcessData();
        IFullSPCycle view;
        List<EntityDTO> ancestors = new List<EntityDTO>();
        bool showimage;
        public FullSPCyclePresenter(IFullSPCycle view)
        {
            this.view = view;
        }

        public void LoadSampleData(int id, bool showImage)
        {
            this.showimage = showImage;
            //BC-040-000 Approve Document
            //int id = 106571;

            //CE-010-000 Define Consulting Need
            //int id = 170160;

            //IS-121-000 Develop / Revise / Cancel Mission Plan
            //int id = 160843;

            //SM-010-000 Raise/Change/Cancel Service Request
            //int id = 194205;

            //RC-010-000 Initiate/Raise Change Request
            //int id = 188719;

            //Get all the "succeeding" event type 
            //from the sub process
            var events = spData.GetAllSucceedingEvent(id);
            EndToEndDTO main = CreateMainDTO(id, events);
            //This builds the remaining sub item 
            //till there is no more succeeding 
            //to handle
            foreach (var item in main.SubItems)
            {
                BuildSubItems(item);
            }

            StringBuilder html = new StringBuilder();
            html.Append("<table border='1' cellpadding='8'>");
            html.Append("<tr>");
            main.AssociatedEventDTO.ExtractProperties();

            //Slicing
            string imagePath = BuildDiagramImageReturningPath(main.AssociatedEventDTO);
            
            SubProcessSlicer slicer = new SubProcessSlicer(imagePath);
            string[] sliced = slicer.Slice();

            StringBuilder sb = new StringBuilder();
            foreach (var item in sliced)
	        {
                string vPath = ExtractVirtualPath(item);
                sb.AppendFormat("<td valign='top'><div class=\"diagram-image-holder\"><img id=\"diagram\" class=\"diagram\" src={0} /></div></td>", vPath);
	        }

            html.AppendFormat("<td  valign='top'><h3>{0}</h3></td>{1}", main.AssociatedEventDTO.Name, this.showimage ?  sb.ToString() : string.Empty);
            html.Append("<td>");

            BuildSequence(html, main);

            html.Append("</td>");
            html.Append("</tr>");
            html.Append("</table>");
            
            this.view.RenderFullCycle(html.ToString());
        }

        private string ExtractVirtualPath(string fullPath)
        {
            int diagramsIndex = fullPath.IndexOf("Diagrams");
            string path = fullPath.Substring(diagramsIndex);
            return path; //SAWebContext.Server.MapPath(path);
        }

        private void BuildSequence(StringBuilder html, EndToEndDTO current)
        {
            if (current.SubItems == null || current.SubItems.Count == 0) return;

            foreach (var item in current.SubItems)
            {
                if (item.ToDiagramDTO == null) continue;
                html.Append("<table border='1'>");
                html.Append("<tr>");
                html.Append("<td valign='top'>");
                item.ToDiagramDTO.ExtractProperties();
                html.AppendFormat("<h3>{0}</h3>{1}", item.ToDiagramDTO.Name, this.showimage ? BuildDiagramImage(item.ToDiagramDTO) : string.Empty);
                html.Append("</td>");
                html.Append("<td valign='top'>");
                BuildSequence(html, item);
                html.Append("</td>");
                html.Append("</tr>");
                html.Append("</table>");
            }
        }

        

        private void BuildSeries(StringBuilder html, EndToEndDTO current)
        {
            //html.Append("<table>");
            foreach (var item in current.SubItems)
            {
                //html.Append("<tr><td>");
                //html.Append("<div class='sequence'>");
                html.Append("<table>");
                item.ToDiagramDTO.ExtractProperties();
                html.Append("<tr><td>");
                html.AppendFormat("<h3>{0}</h3>{1}", item.ToDiagramDTO.Name, string.Empty); //BuildDiagramImage(item.ToDiagramDTO));
                html.Append("</td>");
                //html.Append("</div>");
                if (item.SubItems.Count == 0)
                {
                    //html.Append("<hr/>");
                    //html.Append("<br/>");
                    html.Append("</tr>");
                    html.Append("</table>");
                }
                //html.Append("</td></tr>");

                BuildSeries(html, item);
                html.Append("</table>");
            }
            
        }

        protected string BuildDiagramImage(EntityDTO dto)
        {
            StringBuilder html = new StringBuilder();
            EntityData data = new EntityData();
            FileData files = new FileData();


            FileDTO file = files.GetFile(dto.DGXFileName);
            byte[] imageBytes = file.Data;

            string path = string.Format("{0}_{1}", file.Date.ToFileTime().ToString(), dto.DGXFileName);

            int poolCount = data.GetPoolCount(dto.ID);
            WmfImageManager imageManager = new WmfImageManager(dto, imageBytes,
                path, dto.Type, poolCount, false);
            path = imageManager.ProcessImage();


            html.AppendFormat(GlobalStringResource.Presenter_BuildDiagramImage_Tag, path);
            return html.ToString();
        }

        protected string BuildDiagramImageReturningPath(EntityDTO dto)
        {
            StringBuilder html = new StringBuilder();
            EntityData data = new EntityData();
            FileData files = new FileData();


            FileDTO file = files.GetFile(dto.DGXFileName);
            byte[] imageBytes = file.Data;

            string path = string.Format("{0}_{1}", file.Date.ToFileTime().ToString(), dto.DGXFileName);

            int poolCount = data.GetPoolCount(dto.ID);
            WmfImageManager imageManager = new WmfImageManager(dto, imageBytes,
                path, dto.Type, poolCount, false);
            path = imageManager.ProcessImage();


            return path;
        }

        private void BuildSubItems(EndToEndDTO current)
        {
            int eventId = current.AssociatedEventDTO.ID;
            List<EntityDTO> diagramDto = spData.GetRelatedDiagram(eventId);
            ExtractToAndFrom(current, diagramDto);

            if (current.ToDiagramDTO != null && !ancestors.Exists(c => c.ID == current.ToDiagramDTO.ID))
            {
                ancestors.Add(current.ToDiagramDTO);
            }
            else
            {
                return;
            }

            //if (ancestors.Exists(c => c.ID == current.ToDiagramDTO.ID))
            //{
            //    return;
            //}

            var events = spData.GetAllSucceedingEvent(current.ToDiagramDTO.ID);
            FillinSubItems(current, events);
            if (events.Count > 0)
            {
                foreach (var item in current.SubItems)
                {
                    //if (item.ToDiagramDTO == null || item.FromDiagramDTO == null) continue;

                    //if (!ancestors.Exists(c => c.ID == item.ToDiagramDTO.ID))
                    //{
                        BuildSubItems(item);
                    //}
                }
                //var events = spData.GetAllSucceedingEvent(id);
            }
            else
            {
                return;
            }

        }

        private void ExtractToAndFrom(EndToEndDTO current, List<EntityDTO> relatedDtos)
        {
            if (current.AssociatedEventDTO != null)
            {
                string[] fullname = current.AssociatedEventDTO.Name.ToUpper().Split(new string[] { "TO" }, StringSplitOptions.RemoveEmptyEntries);

                if (fullname.Length != 2)
                {
                    throw new Exception("The name must correspond to have to and from.");
                }

                string from = fullname[0].Trim().Remove(fullname[0].Length - 4);
                current.FromDiagramDTO = relatedDtos.Where(c => c.Name.StartsWith(from)).FirstOrDefault();

                string to = fullname[1].Trim().Remove(fullname[1].Length - 4);
                current.ToDiagramDTO = relatedDtos.Where(c => c.Name.StartsWith(to)).FirstOrDefault();

            }
        }

        
        /// <summary>
        /// This method creates the first and starting point Data object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subItems"></param>
        /// <returns></returns>
        private EndToEndDTO CreateMainDTO(int id, List<EntityDTO> subItems)
        {
            //Get the actual sub process
            EntityDTO actualSubProcess = eData.GetOneEntity(id);

            EndToEndDTO ee = new EndToEndDTO();
            //assign the AssociatedEventDTO
            ee.AssociatedEventDTO = actualSubProcess;
            ee.SubItems = new List<EndToEndDTO>();

            //WHY DO THIS???
            ancestors.Add(actualSubProcess);

            //Add all the sub items to the
            //subitems in the EndToEndDTO instance
            foreach (var item in subItems)
            {
                ee.SubItems.Add(new EndToEndDTO() 
                    {
                        AssociatedEventDTO = item,
                        FromDiagramDTO = null,
                        ToDiagramDTO = null

                    });
            }
            //Sort items
            ee.SubItems.Sort((x, y) => x.AssociatedEventDTO.Name.CompareTo(y.AssociatedEventDTO.Name));
            return ee;
        }

        private void FillinSubItems(EndToEndDTO current, List<EntityDTO> subItems)
        {
            //current.AssociatedEventDTO = dto;
            current.SubItems = new List<EndToEndDTO>();

            foreach (var item in subItems)
            {
                current.SubItems.Add(new EndToEndDTO()
                {
                    AssociatedEventDTO = item,
                    FromDiagramDTO = null,
                    ToDiagramDTO = null

                });
            }
        }
    }
}
