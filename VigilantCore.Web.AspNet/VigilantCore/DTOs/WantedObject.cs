using System.Collections.Generic;

namespace VigilantCore.Web.AspNet.DTOs
{
    public class WantedObject
    {
        public string? Uid { get; set; }
        public string? Title { get; set; }
        public List<string>? Locations { get; set; }
        public string? Sex { get; set; }
        public string? Nationality { get; set; }
        public string? Age_Min { get; set; }
        public string? Age_Max { get; set; }
        public List<string>? Subjects { get; set; }
        public List<ImageInfo> Images { get; set; }
        public string? Url { get; set; }
        public string? Race { get; set; }
        public string? Place_Of_Birth { get; set; }
    }
}
