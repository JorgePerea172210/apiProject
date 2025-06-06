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
        private StoreContext _context;
        private IValidator<BeerInsertDto> _validator;
        private IValidator<BeerUpdateDto> _updateValidator;
        private readonly IBeerService _beerService;

        public BeerController(StoreContext context, IValidator<BeerInsertDto> beerInsertValidator, IValidator<BeerUpdateDto> beerUpdateValidator, IBeerService beerService)
        {
            _context = context;
            _beerService = beerService;
            _validator = beerInsertValidator;
            _updateValidator = beerUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> GetBeers() =>
            await _beerService.GetBeers();

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetBeer(int id)
        {
            var beerDto = await _beerService.GetBeer(id);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> addBeer(BeerInsertDto insert)
        {
            var valResult = await _validator.ValidateAsync(insert);

            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Errors);
            }

            var beer = new Beer()
            {
                Name = insert.Name,
                BrandId = insert.BrandId,
                Alcohol = insert.Alcohol
            };

            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                BrandId = beer.BrandId,
                Alcohol = beer.Alcohol
            };

            return CreatedAtAction(nameof(GetBeer), new { id = beer.BeerId }, beerDto);
        }

        [HttpPut]
        public async Task<ActionResult<BeerDto>> updateBeer(BeerUpdateDto update) 
        {

            var validationResult = await _updateValidator.ValidateAsync(update);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beer = await _context.Beers.FindAsync(update.Id);

            if (beer == null)
            {
                return NotFound();
            }

            beer.Name = update.Name;
            beer.Alcohol = update.Alcohol;
            beer.BrandId = update.BrandId;

            await _context.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                BrandId = beer.BrandId,
                Alcohol = beer.Alcohol
            };

            return Ok(beerDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> deleteBeer(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            var beerDto = new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                BrandId = beer.BrandId,
                Alcohol = beer.Alcohol
            };

            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();

            return Ok(beerDto);
        }
    }
}
