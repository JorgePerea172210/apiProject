using async.dto;

namespace async.Services
{
    public interface IPostService
    {
        public Task<IEnumerable<PostDto>> Get();
    }
}
