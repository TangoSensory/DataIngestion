using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataIngestion.TestAssignment.Model.Search
{
    /// <summary>
    /// I suppose technically these should be DTOs
    /// </summary>
    public class Album
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public long Upc { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsCompilation { get; set; }
        public string Label { get; set; }
        public string ImageUrl { get; set; }
        public IList<Artist> Artists { get; set; }
    }
}
