using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NatureWalks.API.Data;

namespace NatureWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultiesController : ControllerBase
    {
        private readonly WalksDbContext dbContext;

        public DifficultiesController(WalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var difficulties = dbContext.Difficulties.ToList();
            return Ok(difficulties);
        }
    }
}
