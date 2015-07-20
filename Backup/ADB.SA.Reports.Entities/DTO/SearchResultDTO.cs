using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB.SA.Reports.Entities.DTO
{
    /// <summary>
    /// Class representing a search result row.
    /// </summary>
    public class SearchResultDTO
    {
        /// <summary>
        /// Entity ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Entity Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Entity type.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// The type name.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Entity properties.
        /// </summary>
        public string Properties { get; set; }
    }
}
