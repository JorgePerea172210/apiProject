using async.dto;
using Microsoft.AspNetCore.Mvc;

namespace async.Services.IServices
{
    public interface ICommonService<T, TI, TU>
    {
        Task<IEnumerable<T>> GetBeers();
        Task<T> GetBeer(int id);
        Task<T> addBeer(TI insert);
        Task<T> updateBeer(TU update);
        Task<T> deleteBeer(int id);
    }
}
