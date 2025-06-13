using async.dto;
using async.Models;
using async.Repository;
using async.Services.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace async.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        public List<string> errors { get; }
        private readonly IRepository<Beer> _beerRepository;
        private IMapper _mapper;

        public BeerService(IMapper mapper, IRepository<Beer> beerRepository)
        {
            _mapper = mapper;
            _beerRepository = beerRepository;
        }

        public async Task<BeerDto> addBeer(BeerInsertDto insert)
        {
            var beer = _mapper.Map<Beer>(insert);

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

            var beerDto = _mapper.Map<BeerDto>(beer);

            return beerDto;
        }

        public async Task<BeerDto> GetBeer(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = _mapper.Map<BeerDto>(beer);
                return beerDto;
            }

            return null;
        }

        public async Task<IEnumerable<BeerDto>> GetBeers()
        {
            var beers = await _beerRepository.Get();
            var beerDtos = beers.Select(beer => _mapper.Map<BeerDto>(beer));
            return beerDtos;
        }

        public async Task<BeerDto> updateBeer(BeerUpdateDto update)
        {

            var beer = await _beerRepository.GetById(update.Id);
            if (beer != null)
            {
                //Mapeo de un objeto que ya existe
                beer = _mapper.Map<BeerUpdateDto, Beer>(update, beer);

                _beerRepository.Update(beer);
                await _beerRepository.Save();

                var beerDto = _mapper.Map<BeerDto>(beer);

                return beerDto;
            }
            return null;

        }

        public async Task<BeerDto> deleteBeer(int id)
        {
            var beer = await _beerRepository.GetById(id);
            if (beer != null)
            {

                var beerDto = _mapper.Map<BeerDto>(beer);

                _beerRepository.Delete(beer);
                await _beerRepository.Save();
                return beerDto;
            }
            return null;
        }

        public bool validate(BeerInsertDto dto)
        {
            return true;
        }

        public bool validate(BeerUpdateDto dto)
        {
            return true;
        }
    }
}
