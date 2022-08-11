using ManagementAPI.Data.DAL;
using ManagementAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IntersectionController : ControllerBase
    {
        private TrafficManagementContext db;
        private readonly ILogger<IntersectionController> _logger;

        public IntersectionController(TrafficManagementContext database, ILogger<IntersectionController> logger)
        {
            _logger = logger;
            db = database;
        }

        [HttpGet(Name = "GetIntersections")]
        public IEnumerable<Intersection> Get()
        {
            return db.Intersections.ToArray();
        }

     
        [HttpPost(Name = "CreateIntersection")]
        public IActionResult Post(Intersection model)
        {
            db.Intersections.Add(model);
            db.SaveChanges();

            return Ok(model);
        }

        [HttpPut(Name = "UpdateIntersection")]
        public IActionResult Put(Intersection model)
        {
            var intersection = db.Intersections.FirstOrDefault(x => x.Id == model.Id);
            if (intersection == null)
                return NotFound();
            intersection.Longitude = model.Longitude; ;

            db.Update(intersection);
            db.SaveChanges();

            return Ok(model);
        }
    }
}