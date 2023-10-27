using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VigilantCore.Web.AspNet.Models
{
    [Table("WANTED")]
    public class WantedModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Column("FBI_UID")]
        public string? Uid { get; set; }

        [Column("TITLE")]
        public string? Title { get; set; }

        [Column("LOCATIONS")]
        public string? Locations { get; set; }

        [Column("SEX")]
        public string? Sex { get; set; }

        [Column("NATIONALITY")]
        public string? Nationality { get; set; }

        [Column("AGE_MIN")]
        public string? Age_Min { get; set; }

        [Column("AGE_MAX")]
        public string? Age_Max { get; set; }

        [Column("SUBJECTS")]
        public string? Subjects { get; set; }

        [Column("IMAGE_URL")]
        public string? Images { get; set; }

        [Column("URL")]
        public string? Url { get; set; }

        [Column("RACE")]
        public string? Race { get; set; }

        [Column("PLACE_OF_BIRTH")]
        public string? Place_Of_Birth { get; set; }

        public WantedModel()
        {
        }

        public WantedModel(string? uid, string? title, List<string>? locations, string? sex, string? nationality, string? age_Min, string? age_Max, string? subjects, string? images, string? url, string? race, string? place_Of_Birth)
        {
            Uid = uid;
            Title = title;
            Locations = string.Join(",", locations ?? new List<string>());
            Sex = sex;
            Nationality = nationality;
            Age_Min = age_Min;
            Age_Max = age_Max;
            Subjects = subjects;
            Images = images;
            Url = url;
            Race = race;
            Place_Of_Birth = place_Of_Birth;
        }
    }
}
