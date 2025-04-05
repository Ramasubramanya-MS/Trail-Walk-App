using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NatureWalks.API.Data;
using NatureWalks.API.Models.Domain;

namespace NatureWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly WalksDbContext dbcontext;

        public RegionsController(WalksDbContext dbContext)
        {
            this.dbcontext = dbContext;
        }

        // Get All Regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dbcontext.Regions.ToList();
            return Ok(regions);
        }

        // Get Regions By Id
        [HttpGet]
        [Route("{Id:Guid}")]
        public IActionResult GetById(Guid Id)
        {
            var region = dbcontext.Regions.Find(Id);
            if(region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }
    }
}
