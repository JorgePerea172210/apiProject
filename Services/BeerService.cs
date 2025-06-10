using async.dto;
using async.Models;
using async.Repository;
using async.Services.IServices;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace async.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {

        private StoreContext _context;
        private readonly IRepository<Beer> _beerRepository;

        public BeerService(StoreContext context, IRepository<Beer> beerRepository)
        {
            _beerRepository = beerRepository;
            _context = context;
        }

        public async Task<BeerDto> addBeer(BeerInsertDto insert)
        {
            var beer = new Beer
            {
                Name = insert.Name,
                Alcohol = insert.Alcohol,
                BrandId = insert.BrandId
            };

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

            var beerDto = new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                BrandId = beer.BrandId,
                Alcohol = beer.Alcohol
            };

            return beerDto;
        }

        public async Task<BeerDto> GetBeer(int id)
        {
            var beer = await _beerRepository.GetById(id);

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

        public async Task<IEnumerable<BeerDto>> GetBeers()
        {
            var beers = await _beerRepository.Get();
            var beerDtos = beers.Select(beer => new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandId = beer.BrandId
            });
            return beerDtos;
        }

        public async Task<BeerDto> updateBeer(BeerUpdateDto update)
        {

            var beer = await _beerRepository.GetById(update.Id);
            if (beer != null)
            {
                beer.Name = update.Name;
                beer.Alcohol = update.Alcohol;
                beer.BrandId = update.BrandId;

                _beerRepository.Update(beer);
                await _beerRepository.Save();

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

        public async Task<BeerDto> deleteBeer(int id)
        {
            var beer = await _beerRepository.GetById(id);
            if (beer != null)
            {

                var beerDto = new BeerDto
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    BrandId = beer.BrandId,
                    Alcohol = beer.Alcohol
                };

                _beerRepository.Delete(beer);
                await _beerRepository.Save();
                return beerDto;
            }
            return null;
        }
    }
}
