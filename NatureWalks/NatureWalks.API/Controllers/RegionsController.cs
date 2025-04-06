using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NatureWalks.API.Data;
using NatureWalks.API.Models.Domain;
using NatureWalks.API.Models.DTOs;

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
            // Get Data from Database - Domain Models
            var regionsDomainModel = dbcontext.Regions.ToList();

            // Map Domain Models to DTOs
            var regionDto = new List<RegionDTO>();
            foreach (var regionDomain in regionsDomainModel)
            {
                regionDto.Add(new RegionDTO()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }
            
            // Return DTOs
            return Ok(regionDto);
        }

        // Get Regions By Id
        [HttpGet]
        [Route("{Id:Guid}")]
        public IActionResult GetById(Guid Id)
        {
            // Get Region Domain Model from Database
            var regionDomain = dbcontext.Regions.Find(Id);

            if(regionDomain == null)
            {
                return NotFound();
            }

            // Map/Convert Region Domain Model to Region DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            
            // Return DTO
            return Ok(regionDto);
        }

        // POST To Create New Region
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // Use Domain Model to create Region
            dbcontext.Regions.Add(regionDomainModel);
            dbcontext.SaveChanges();

            // Map Domain Model back to DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDomainModel);
        }

        // Update Region
        // Put:
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Check if region exists
            var regionDomainModel = dbcontext.Regions.FirstOrDefault(x =>x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            dbcontext.SaveChanges();

            // Convert Domain Model to DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl

            };

            return Ok(regionDto);
        }


        // Delete Region
        // DELETE: 

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomainModel = dbcontext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Delete region
            dbcontext.Regions.Remove(regionDomainModel);
            dbcontext.SaveChanges();

            // return deleted Region back
            // map Domain Model to DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }


    }
}
