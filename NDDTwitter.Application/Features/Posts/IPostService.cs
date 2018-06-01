using NDDTwitter.Domain;
using System.Collections.Generic;

namespace NDDTwitter.Application.Features.Posts
{
    public interface IPostService
    {
        Post Add(Post post);
        Post Update(Post post);
        Post Get(long id);
        IEnumerable<Post> GetAll();
        void Delete(Post post);
    }
}
