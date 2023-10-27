using System.Collections.Generic;

namespace VigilantCore.Web.AspNet.DTOs
{
    public class ResponseObject
    {
        public int Total { get; set; }
        public List<WantedObject> Items { get; set; }
    }
}

