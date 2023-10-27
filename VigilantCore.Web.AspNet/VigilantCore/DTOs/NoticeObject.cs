namespace VigilantCore.Web.AspNet.DTOs
{
    public class NoticeObject
    {
        public string date_of_birth { get; set; }
        public List<string> nationalities { get; set; }
        public string entity_id { get; set; }
        public string forename { get; set; }
        public string name { get; set; }
        public Links _links { get; set; }
    }
}
