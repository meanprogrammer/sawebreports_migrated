using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.Enums;

namespace ADB.SA.Reports.Entities.DTO
{
    /// <summary>
    /// Class item that is used in the homepage of the site.
    /// </summary>
    public class AsIsItemEntity
    {
        /// <summary>
        /// The definition dto.
        /// </summary>
        public EntityDTO DefinitionDTO { get; set; }

        /// <summary>
        /// The diagram dto.
        /// </summary>
        public List<EntityDTO> DiagramDTO { get; set; }

        /// <summary>
        /// The item order (sequence).
        /// </summary>
        public int ItemOrder { get; set; }

        public string FontColor
        {
            get
            {
                this.DefinitionDTO.ExtractProperties();
                return this.DefinitionDTO.RenderHTML("Symbol Font Color", RenderOption.None);
            }
        }
        public string BoxColor 
        {
            get 
            {
                this.DefinitionDTO.ExtractProperties();
                return this.DefinitionDTO.RenderHTML("Symbol Box Color", RenderOption.None);
            }
        }
    }
}
