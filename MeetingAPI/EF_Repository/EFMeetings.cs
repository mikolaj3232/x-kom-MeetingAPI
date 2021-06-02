using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingAPI.EF_Repository
{
    public class EFMeeting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public DateTime date_of_meeting { get; set; }
        public int participants_count { get; set; }
       
    }
    public class RequestInputArg
    {
        [StringLength(200, MinimumLength = 3)]
        public string name { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public DateTime date_of_meeting { get; set; }
        public static EFMeeting MapRequestArgToEFClass(RequestInputArg arg)
        {
            var tmp = new EFMeeting();
            tmp.name = arg.name;
            tmp.subject = arg.subject;
            tmp.description = arg.description;
            tmp.date_of_meeting = arg.date_of_meeting;
            return tmp;
        }
    }
}
