using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Library.Models
{
  public class LibraryContext : IdentityDbContext<ApplicationUser>
  {
    // public DbSet<Item> Items { get; set; }
    // public DbSet<Category> Categories { get; set; }
    // public DbSet<Tag> Tags { get; set; }
    // public DbSet<ItemTag> ItemTags { get; set; }

    public LibraryContext(DbContextOptions options) : base(options) { }
  }
}