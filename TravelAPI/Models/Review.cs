namespace TravelAPI.Models
{
  public class Review
  {
    public int ReviewId { get; set; }
    public Rating Rating { get; set; }
    public string UserName { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
  }
  public enum Rating
  {
    ZERO, ONE, TWO, THREE, FOUR, FIVE
  }
}