using Microsoft.EntityFrameworkCore;
using Teched2018.ApiModels;

namespace Teched2018.Repositories
{
    public class Context : DbContext
    {
		public DbSet<Product> Products { get; set; }
		public DbSet<Tag> Tags { get; set; }

        public Context() : base()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
    }
}
