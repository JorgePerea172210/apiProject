using async.dto;
using async.Models;
using async.Services.IServices;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace async.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private IValidator<BeerInsertDto> _validator;
        private IValidator<BeerUpdateDto> _updateValidator;
        private readonly ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> _beerService;

        public BeerController(IValidator<BeerInsertDto> beerInsertValidator, IValidator<BeerUpdateDto> beerUpdateValidator, [FromKeyedServices("beerService")] ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> beerService)
        {
            _beerService = beerService;
            _validator = beerInsertValidator;
            _updateValidator = beerUpdateValidator;
        }

        [HttpGet("GetBeers")]
        public async Task<IEnumerable<BeerDto>> GetBeers() =>
            await _beerService.GetBeers();

        [HttpGet("GetBeerById/{id}")]
        public async Task<ActionResult<BeerDto>> GetBeer(int id)
        {
            var beerDto = await _beerService.GetBeer(id);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpPost("AddBeer")]
        public async Task<ActionResult<BeerDto>> addBeer(BeerInsertDto insert)
        {
            var valResult = await _validator.ValidateAsync(insert);

            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Errors);
            }

            var beerDto = await _beerService.addBeer(insert);

            return CreatedAtAction(nameof(GetBeer), new { id = beerDto.Id }, beerDto);
        }

        [HttpPut("UpdateBeer")]
        public async Task<ActionResult<BeerDto>> updateBeer(BeerUpdateDto update) 
        {

            var validationResult = await _updateValidator.ValidateAsync(update);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beerDto = await _beerService.updateBeer(update);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpDelete("DeleteBeer/{id}")]
        public async Task<ActionResult<BeerDto>> deleteBeer(int id)
        {
            var beerDto = await _beerService.deleteBeer(id);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }
    }
}
