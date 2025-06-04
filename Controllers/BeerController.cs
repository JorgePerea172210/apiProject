using async.dto;
using async.Models;
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

        public BeerController(StoreContext context, IValidator<BeerInsertDto> beerInsertValidator)
        {
            _context = context;
            _validator = beerInsertValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> GetBeers() =>
            await _context.Beers.Select(b => new BeerDto
            {
                Id = b.BeerId,
                Name = b.Name,
                BrandId = b.BrandId,
                Alcohol = b.Alcohol
            }).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetBeer(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            var b = new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                BrandId = beer.BrandId,
                Alcohol = beer.Alcohol
            };

            return Ok(b);
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

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> updateBeer(int id, BeerUpdateDto update) 
        {
            var beer = await _context.Beers.FindAsync(id);

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
        public async Task<ActionResult> deleteBeer(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
