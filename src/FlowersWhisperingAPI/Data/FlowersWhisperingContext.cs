using Microsoft.EntityFrameworkCore;
using FlowersWhisperingAPI.Extensions;
namespace FlowersWhisperingAPI.Data
{
    public class FlowersWhisperingContext : DbContext
    {
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        //     modelBuilder.ApplyShortNames();
        //     modelBuilder.Entity<User>()
        //             .Property(u => u.UserId)
        //             .HasDefaultValueSql("user_id_seq.NEXTVAL");
        // }

        public FlowersWhisperingContext(DbContextOptions<FlowersWhisperingContext> options)
            : base(options)
        {
        }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
        //                   .EnableSensitiveDataLogging();
        // }  
    }
}
