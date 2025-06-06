using async.dto;
using async.Models;
using async.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace async.Services
{
    public class BeerService : IBeerService
    {

        private StoreContext _apDbContext;

        public BeerService(StoreContext context)
        {
            _apDbContext = context;
        }

        public Task<BeerDto> addBeer(BeerInsertDto insert)
        {
            throw new NotImplementedException();
        }

        public async Task<BeerDto> GetBeer(int id)
        {
            var beer = await _apDbContext.Beers.FindAsync(id);

            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandId = beer.BrandId
                };
                return beerDto;
            }

            return null;
        }

        public async Task<IEnumerable<BeerDto>> GetBeers() =>
            await _apDbContext.Beers.Select(b => new BeerDto
            {
                Id = b.BeerId,
                Name = b.Name,
                BrandId = b.BrandId,
                Alcohol = b.Alcohol
            }).ToListAsync();

        public Task<BeerDto> updateBeer(BeerUpdateDto update)
        {
            throw new NotImplementedException();
        }

        public Task<BeerDto> deleteBeer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
