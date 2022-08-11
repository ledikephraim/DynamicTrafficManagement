using ManagementAPI.Data.DAL;
using ManagementAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionController : ControllerBase
    {
        private TrafficManagementContext db;
        private readonly ILogger<RegionController> _logger;

        public RegionController(TrafficManagementContext database, ILogger<RegionController> logger)
        {
            _logger = logger;
            db = database;
        }

        [HttpGet(Name = "GetRegions")]
        public IEnumerable<Region> Get()
        {
            return db.Regions.ToArray();
        }

        [HttpPost(Name = "CreateRegion")]
        public IActionResult Post(Region model)
        {
            db.Regions.Add(model);
            db.SaveChanges();

            return Ok(model);
        }

        [HttpPut(Name = "UpdateRegion")]
        public IActionResult Put(Region model)
        {
            var region = db.Regions.FirstOrDefault(x => x.Id == model.Id);
            if (region == null)
                return NotFound();
            region.Name = model.Name;
            region.Intersections = model.Intersections;

            db.Update(region);
            db.SaveChanges();

            return Ok(model);
        }
    }
}