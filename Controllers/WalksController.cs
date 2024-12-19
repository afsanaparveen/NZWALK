using AutoMapper;
using Catel.Data;
using Microsoft.AspNetCore.Mvc;
using NzWalks.API.CustomActionFilters;
using NzWalks.API.models.domains;
using NzWalks.API.models.DTO;
using NzWalks.API.repositories;
using System.Collections.Generic;


namespace NzWalks.API.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private IMapper mapper;
        private IWalkRepository walkRepository;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;

        }


        //add a new walk
        [HttpPost]
        [ValidateModelAtrribute]
        public async Task<IActionResult> CreateAsync([FromBody] AddWalkRequestDto addWalkRequestDto)
        {

            //dto to domain model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
            await walkRepository.CreateAsync(walkDomainModel);
            //domain model to dto

            return Ok(mapper.Map<WalkDto>(walkDomainModel));



        }




        //GET ALL WALKS
        [HttpGet]
        [ValidateModelAtrribute]
        //filterOn=Name&filterQuery=Track
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,[FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize=1000)
        {

            //domain model to dto
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn,filterQuery,
                                                                    sortBy,isAscending ?? true,
                                                                    pageNumber,pageSize);


            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        //GET all WALKS BY ID
        [HttpGet]

        [Route("{id:Guid}")]
        [ValidateModelAtrribute]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            //dto to domain model
            var walkDomainModel = await walkRepository.GetByIDAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            //map
            return Ok(mapper.Map<WalkDto>(walkDomainModel));


        }

        //UPDATE A WALK
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModelAtrribute]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {


            //from dto to domain model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            //map domain model to dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));



        }

        //Delete Walk
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }
            //domain to dto
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
        }
    }
}
