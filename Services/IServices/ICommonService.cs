using async.dto;
using Microsoft.AspNetCore.Mvc;

namespace async.Services.IServices
{
    public interface ICommonService<T, TI, TU>
    {
        public List<string> errors { get; }
        Task<IEnumerable<T>> GetBeers();
        Task<T> GetBeer(int id);
        Task<T> addBeer(TI insert);
        Task<T> updateBeer(TU update);
        Task<T> deleteBeer(int id);
        Boolean validate(TI dto);
        Boolean validate(TU dto);
    }
}
