using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using TravelAPI.Models;
using System.Linq;

namespace TravelAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReviewsController : ControllerBase
  {
    private readonly TravelAPIContext _db;
    public ReviewsController(TravelAPIContext db)
    {
      _db = db;
    }
    // GET api/values
    [HttpGet]
    public ActionResult<IEnumerable<Review>> Get(string country, string city)
    {

      if ((country != null) && (city != null))
      {
        return _db.Reviews.Where(x => x.Country == country && x.City == city).ToList();
      }
      if (country != null)
      {
        return _db.Reviews.Where(x => x.Country == country).ToList();
      }

      return _db.Reviews.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Review> Get(int id)
    {
      return _db.Reviews.FirstOrDefault(entry => entry.ReviewId == id);
    }


    // POST api/reviews
    [HttpPost]
    public void Post([FromBody] Review review)
    {
      Console.WriteLine(review.Rating);
      _db.Reviews.Add(review);
      _db.SaveChanges();
    }

    //api/reviews/MostPopular
    [HttpGet]
    public ActionResult<IEnumerable<Review>> MostPopular(string option)
    {
      if (option == "rating")
      {
        return _db.Reviews.OrderBy(x => x.Rating).ToList();
      }
      if (option == "number")
      {
        return _db.Reviews.OrderBy(x => x.Rating).ToList();
      }
      return _db.Reviews.OrderBy(x => x.Rating).ToList();
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
