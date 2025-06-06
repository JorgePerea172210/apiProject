using async.dto;
using Microsoft.AspNetCore.Mvc;

namespace async.Services.IServices
{
    public interface IBeerService
    {
        Task<IEnumerable<BeerDto>> GetBeers();
        Task<BeerDto> GetBeer(int id);
        Task<BeerDto> addBeer(BeerInsertDto insert);
        Task<BeerDto> updateBeer(BeerUpdateDto update);
        Task<BeerDto> deleteBeer(int id);
    }
}
