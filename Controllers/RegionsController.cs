using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NzWalks.API.models.domains;
using global::NzWalks.API.models.DTO;
using global::NzWalks.API.repositories;
using NzWalks.API.Data;
using NzWalks.API.CustomActionFilters;
using NzWalks.API;
using Catel.Data;
using Microsoft.AspNetCore.Authorization;


namespace NzWalks.API.Controllers
{
    //https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        //using dependency injection callind
        private readonly NzWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;




        public RegionsController(NzWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        //GET verb for getting all regions
        //GET:https://
        [HttpGet]
        [Authorize(Roles ="Reader")]

        public async Task<IActionResult> GetAll()
        {
            //get data from database-domain models
            //var regionsDomain = await dbContext.Regions.ToListAsync();

            var regionDomain = await regionRepository.GetAllAsync();


            //map domain models to DTOS
            var regionsDto = mapper.Map<List<RegionDto>>(regionDomain);


            //return DTOS
            return Ok(regionsDto);

        }


        //get single Region by ID
        //GET: https://localhost/api/regions/{id}


        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //get Region domain model from database
            /*var Region=dbContext.Regions.Find(id);*/ //find method only used for unique key
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound(); //404 response
            }
            //map Region domain model to Region DTO

            //return dto back to client
            return Ok(mapper.Map<RegionDto>(regionDomain)); //src is regionDomainModel des=RegionDto


        }




        //CREATING NEW REGION

        [HttpPost]
        [ValidateModelAtrribute]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            var regionDomainModelC = mapper.Map<Region>(addRegionRequestDto);

            //use domain model to create Region
            //await dbContext.Regions.AddAsync(regionDomainModel);
            //await dbContext.SaveChangesAsync();
            regionDomainModelC = await regionRepository.CreateAsync(regionDomainModelC);
            //map domain models to dtos
            var regiondto = mapper.Map<RegionDto>(regionDomainModelC);
            //return 
            return CreatedAtAction(nameof(GetById), new { id = regiondto.Id }, regiondto);





        }





        //UPdate Region

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModelAtrribute]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {


            //map dtos to domain model
            var regionDomainModelU = mapper.Map<Region>(updateRegionRequestDto);
            regionDomainModelU = await regionRepository.UpdateAsync(id, regionDomainModelU);

            if (regionDomainModelU == null)
            {
                return NotFound();
            }



            //map domainmodel to dto
            var regionDto = mapper.Map<RegionDto>(regionDomainModelU);
            return Ok(regionDto);



        }


        //Delete particular id Region
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //map domain model to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);





        }

    }


}
