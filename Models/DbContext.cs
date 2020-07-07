using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExcel.Models
{
    public class DbContextApi:DbContext
    {
        public DbContextApi(DbContextOptions<DbContextApi> options)
            : base(options)
        {

        }

        public DbSet<Videos> Videos { get; set; }
        public DbSet<Genres> Genres { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Videos>().ToTable("Videos").HasKey(p => p.Id);
            builder.Entity<Genres>()
        .HasMany(c => c.Videos)
        .WithOne(e => e.Genres).HasForeignKey(f => f.Genre).IsRequired();
            builder.Entity<Genres>().HasKey(p => p.Id);
          //  builder.HasOne<User>(h => h.User)
          //.WithMany(w => w.BusinessFulls).HasForeignKey(f => f.UserId);
        }
    }
}
