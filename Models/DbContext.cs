using Microsoft.EntityFrameworkCore;

namespace ApiExcel.Models
{
    public class DbContextApi : DbContext
    {
        public DbContextApi(DbContextOptions<DbContextApi> options)
            : base(options) { }
        public DbSet<Videos> Videos { get; set; }
        public DbSet<Genres> Genres { get; set; }
    }
}
