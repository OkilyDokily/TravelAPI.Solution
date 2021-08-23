using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ReactTravelAPI.Models
{
  public class ReactTravelAPIContext : IdentityDbContext<ApplicationUser>
  {
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
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
           UserName = "Yolo Banksy",
           Country = "Australia",
           City = "Sydney"
         }
         ,
          new Review
          {
            ReviewId = 4,
            Rating = Rating.FIVE,
            UserName = "Kate Austen",
            Country = "Australia",
            City = "Sydney"
          },
           new Review
           {
             ReviewId = 5,
             Rating = Rating.FIVE,
             UserName = "Kaitlinn Bennet",
             Country = "Australia",
             City = "Sydney"
           },
            new Review
            {
              ReviewId = 6,
              Rating = Rating.FIVE,
              UserName = "Hosia",
              Country = "Australia",
              City = "Sydney"
            },
             new Review
             {
               ReviewId = 7,
               Rating = Rating.FIVE,
               UserName = "Charlie Bonkadonk",
               Country = "Australia",
               City = "Sydney"
             }
     );
    }
    public virtual DbSet<Review> Reviews { get; set; }

    public ReactTravelAPIContext(DbContextOptions<ReactTravelAPIContext> options) : base(options) { }
  }
}