using System.Collections.Generic;
using NDDTwitter.Domain;
using NDDTwitter.Domain.Exceptions;
using NDDTwitter.Domain.Features.Posts;

namespace NDDTwitter.Application.Features.Posts
{
    public class PostService : IPostService
    {
        IPostRepository _repository;
        public PostService(IPostRepository repository)
        {
            _repository = repository;
        }

        public Post Add(Post post)
        {
            post.Validate();
            return _repository.Save(post);
        }
        public Post Update(Post post)
        {
            if (post.Id == 0)
            {
                throw new IdentifierUndefinedException();
            }
            post.Validate();
            return _repository.Update(post);
        }
        public void Delete(Post post)
        {
            _repository.Delete(post);
        }

        public Post Get(long id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _repository.GetAll();
        }

    }
}
