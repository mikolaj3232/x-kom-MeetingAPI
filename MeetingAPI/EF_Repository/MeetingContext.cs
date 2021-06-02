using Microsoft.EntityFrameworkCore;

namespace MeetingAPI.EF_Repository
{
    public class MeetingContext : DbContext 
    {
        public DbSet<EFMeeting> meeting { get; set; }
        public DbSet<EFParticipant> participant { get; set; }
        public MeetingContext(DbContextOptions<MeetingContext> options)
        : base(options)
        {
        }
    }
}