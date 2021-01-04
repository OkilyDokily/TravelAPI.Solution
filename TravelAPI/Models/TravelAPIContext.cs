using Microsoft.EntityFrameworkCore;

namespace TravelAPI.Models
{
  public class TravelAPIContext : DbContext
  {
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Review>()
       .HasData(
         new Review
         {
           ReviewId = 1,
           Rating = Rating.THREE,
           UserName = "Pat Benatar",
           Country = "USA",
           City = "Portland"
         },

         new Review
         {
           ReviewId = 2,
           Rating = Rating.FOUR,
           UserName = "Pat Benatar",
           Country = "Russia",
           City = "Moskow"
         },

         new Review
         {
           ReviewId = 3,
           Rating = Rating.FIVE,
           UserName = "Charles Barkely",
           Country = "Australia",
           City = "Sydney"
         }
     );
    }
    public virtual DbSet<Review> Reviews { get; set; }

    public TravelAPIContext(DbContextOptions<TravelAPIContext> options) : base(options) { }
  }
}