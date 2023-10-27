using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VigilantCore.Web.AspNet.Models
{
    [Table("NOTICES")] 
    public class NoticeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Column("DATE_OF_BIRTH")]
        public string? DateOfBirth { get; set; }

        [Column("NATIONALITIES")]
        public string? Nationalities { get; set; }

        [Column("ENTITY_ID")]
        public string? EntityId { get; set; }

        [Column("FORENAME")]
        public string? Forename { get; set; }

        [Column("NAME")]
        public string? Name { get; set; }

        [Column("THUMBNAIL_URL")]
        public string? ThumbnailUrl { get; set; }

        public NoticeModel() { }

        public NoticeModel(string dateOfBirth, List<string> nationalities, string entityId, string forename, string name, string thumbnailUrl)
        {
            DateOfBirth = dateOfBirth;
            Nationalities = string.Join(",", nationalities ?? new List<string>());
            EntityId = entityId;
            Forename = forename;
            Name = name;
            ThumbnailUrl = thumbnailUrl;
        }
    }

}
