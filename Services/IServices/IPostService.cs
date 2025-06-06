using async.dto;

namespace async.Services.IServices
{
    public interface IPostService
    {
        public Task<IEnumerable<PostDto>> Get();
    }
}
