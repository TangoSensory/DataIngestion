using System;
using System.Collections.Generic;
using System.Text;

namespace DataIngestion.TestAssignment.Model.Dtos
{
    public class CollectionDto
    {
        public long CollectionId { get; set; }
        public string Name { get; set; }
        public string ViewUrl { get; set; }
        public string ArtworkUrl { get; set; }
        public DateTime OriginalReleaseDate { get; set; }
        public string LabelStudio { get; set; }
        public bool IsCompilation { get; set; }
        // Other properties not required
    }
}
