using CollageMng.Models;
using Microsoft.EntityFrameworkCore;

namespace CollageMng.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
            
        }
        public DbSet<Login> Login_Data { get; set; }
        public DbSet<Timetable> Timetable { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Syllabus> Syllabus { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<MbaCource> MbaCource { get; set; }
        public DbSet<McaCource> McaCources { get; set; }
        public DbSet<ImbaCource> ImbaCource { get; set; }
        public DbSet<ImcaCource> ImcaCource { get; set; }
        public DbSet<Mammarks> Mammarks { get; set; }
        public DbSet<Icmarks> Icmarks { get; set; }
        public DbSet<Tt> Tt { get; set; }
        public DbSet<Studata> Studata { get; set; }
        public DbSet<Facdata> Facdata { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
    }
}
