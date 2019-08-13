using Microsoft.EntityFrameworkCore;
using miprimerAPI.Entities;

namespace miprimerAPI.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options){

        }

       public DbSet<Artist> Artists { get; set; }
    }
}