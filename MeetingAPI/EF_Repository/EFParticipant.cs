using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.Json;

namespace MeetingAPI.EF_Repository
{
    public class EFParticipant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int meeting_id { get; set; }
    }
    public class RequestParticipant
    {
        public int meeting_id { get; set; }
        [StringLength(50, MinimumLength =3)]
        public string name { get; set; }
        [EmailAddress]
        public string email { get; set; }
        public static EFParticipant RequestParamToObject(RequestParticipant req,int meet_id)
        {
            EFParticipant tmp = new EFParticipant();
            tmp.name = req.name;
            tmp.email = req.email;
            tmp.meeting_id = meet_id;
            Debug.WriteLine(JsonSerializer.Serialize(tmp));
            return tmp;
        }
    }
}
