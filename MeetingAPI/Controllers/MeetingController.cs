using MeetingAPI.EF_Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace MeetingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IDbContextFactory<MeetingContext> _context;
        private const int participant_limit = 25;
        public MeetingController(IDbContextFactory<MeetingContext> context)
        {
            _context = context;
        }

        [Route("create_meeting")]
        [HttpPost]
        public IActionResult CreateMeeting(RequestInputArg meet)
        {
            using(var con = _context.CreateDbContext())
            {
                con.Add(RequestInputArg.MapRequestArgToEFClass(meet));
                con.SaveChanges();
            }
            return Ok();
        }

        [Route("get_all_meeting")]
        [HttpGet]
        public string GetAllMeetings()
        {
            List<EFMeeting> result;
            using (var con = _context.CreateDbContext())
            {
                result = con.meeting.ToList();
            }
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            return JsonSerializer.Serialize(result,options);
        }

        [Route("add_participant")]
        [HttpPost]
        public IActionResult AddParticipant(RequestParticipant participant)
        {
            using (var con = _context.CreateDbContext())
            {
                EFMeeting tmp = con.meeting.Where(x => x.id == participant.meeting_id).FirstOrDefault();
                if (tmp == null) {
                    return NotFound("Meeting not found");
                }
                if (tmp.participants_count == participant_limit)
                {
                    return BadRequest("Meeting is fulll, you cant sign up");
                }
                        con.Add(RequestParticipant.RequestParamToObject(participant, participant.meeting_id));
                        tmp.participants_count++;
                        con.SaveChanges();
                    
            }
            return Ok();
        }
        [Route("delete_meeting")]
        [HttpDelete]
        public IActionResult DeleteMeeting(int meeting_id)
        {
            using (var con = _context.CreateDbContext())
            {
                EFMeeting tmp = con.meeting.Where(x => x.id == meeting_id).FirstOrDefault();
                if(tmp == null)
                {
                    return Ok();
                }
                con.Remove(tmp);
                con.SaveChanges();
            }
            return Ok();
        }
    }
}
