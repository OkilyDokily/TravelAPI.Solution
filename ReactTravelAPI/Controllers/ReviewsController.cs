using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using ReactTravelAPI.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;


namespace ReactTravelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {

        private readonly ReactTravelAPIContext _db;
        public ReviewsController(ReactTravelAPIContext db)
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        public void Post([FromBody] Review review)
        {
            System.Diagnostics.Debug.WriteLine(review.ReviewId);
            review.UserName = GetPayloadObject().aud;
            _db.Reviews.Add(review);
            _db.SaveChanges();
        }

        private Payload GetPayloadObject()
        {
            string header = Request.Headers[HeaderNames.Authorization];
            string payload = header.Split(" ")[1].Split(".")[1];
            string jsonstring = Encoding.UTF8.GetString(Payload.Decode(payload));
            return JsonSerializer.Deserialize<Payload>(jsonstring);
        }

        // PUT api/reviews/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public void Put(int id, [FromBody] Review review)
        {
            Review detach = _db.Reviews.FirstOrDefault(entry => entry.ReviewId == id);
            string username = GetPayloadObject().aud;
            if (username == detach.UserName)
            {
                review.ReviewId = id;
                review.UserName = username;
                _db.Entry(detach).State = EntityState.Detached;
                _db.Entry(review).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public void Delete(int id)
        {
            Review review = _db.Reviews.FirstOrDefault(entry => entry.ReviewId == id);
            if (GetPayloadObject().aud == review.UserName)
            {
                _db.Reviews.Remove(review);
                _db.SaveChanges();
            }
        }
    }
}
