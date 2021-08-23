using System.ComponentModel.DataAnnotations;

namespace ReactTravelAPI.Models
{
  public class Review
  {
    public int ReviewId { get; set; }
    [Range(0, 5, ErrorMessage = "rating must be between 0 and 5")]
    [Required]
    public Rating Rating { get; set; }

    public string UserName { get; set; }
    [Required]
    public string Country { get; set; }
    [Required]
    public string City { get; set; }
  }
  public enum Rating
  {
    ZERO, ONE, TWO, THREE, FOUR, FIVE
  }
}