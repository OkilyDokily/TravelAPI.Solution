using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using TravelAPI.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace TravelAPI.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class ReviewsController : ControllerBase
  {
    private readonly TravelAPIContext _db;
    public ReviewsController(TravelAPIContext db)
    {
      _db = db;
    }
    // GET api/Reviews
    [HttpGet]
    public ActionResult<IEnumerable<Review>> Get(string country, string city, string option)
    {

      if (option == "rating")
      {
        return _db.Reviews.GroupBy(x => new { x.Country, x.City }).ToList().GroupBy(x => x.Sum(xa => (int)xa.Rating) / x.Count()).OrderByDescending(x => x.Key).First().SelectMany(x => x).ToList();
      }

      if (option == "number")
      {
        return _db.Reviews.GroupBy(x => new { x.Country, x.City }).ToList().GroupBy(x => x.Count()).OrderByDescending(x => x.Key).First().SelectMany(x => x).ToList();
      }

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

    //Return random destination
    [HttpGet]
    [Route("/api/reviews/random")]
    public ActionResult<string> Get()
    {
      Random rnd = new Random();
      List<string> reviews = _db.Reviews.GroupBy(x => new { x.Country, x.City }).Select(x => $"{x.Key.City} {x.Key.Country}").ToList();
      return reviews[rnd.Next(reviews.Count)];
    }

    [HttpGet("{id}")]
    public ActionResult<Review> Get(int id)
    {
      return _db.Reviews.FirstOrDefault(entry => entry.ReviewId == id);
    }

    [HttpGet]
    [Route("/api/reviews/popular")]
    public ActionResult<IEnumerable<string>> Get(string option)
    {
      if (option == "number")
      {
        return _db.Reviews.GroupBy(x => new { x.Country, x.City }).ToList()
       .GroupBy(x => x.Count()).
       OrderByDescending(x => x.Key)
       .SelectMany(x => x,
             (countgroup, countrycitygroup) => $"{countrycitygroup.Key.City} {countrycitygroup.Key.Country} {countgroup.Key}")
        .ToList();
      }
      else if (option == "rating")
      {
        return _db.Reviews.GroupBy(x => new { x.Country, x.City }).ToList()
        .GroupBy(x => x.Sum(xa => (int)xa.Rating) / x.Count()).
        OrderByDescending(x => x.Key)
        .SelectMany(x => x,
              (countgroup, countrycitygroup) => $"{countrycitygroup.Key.City} {countrycitygroup.Key.Country} {countgroup.Key}")
         .ToList();
      }
      return new List<string>();
    }

    // POST api/reviews
    [HttpPost]
    public void Post([FromBody] Review review)
    {
      _db.Reviews.Add(review);
      _db.SaveChanges();
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Review review, string user_name)
    {
      Review detach = _db.Reviews.FirstOrDefault(entry => entry.ReviewId == id);
      review.ReviewId = id;
      if (user_name == detach.UserName)
      {
        _db.Entry(detach).State = EntityState.Detached;
        _db.Entry(review).State = EntityState.Modified;
        _db.SaveChanges();
      }
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id, string user_name)
    {
      Review review = _db.Reviews.FirstOrDefault(entry => entry.ReviewId == id);
      if (user_name == review.UserName)
      {
        _db.Reviews.Remove(review);
        _db.SaveChanges();
      }
    }
  }
}
