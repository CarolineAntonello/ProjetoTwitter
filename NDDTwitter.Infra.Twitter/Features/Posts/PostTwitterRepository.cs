using NDDTwitter.Domain;
using NDDTwitter.Domain.Exceptions;
using NDDTwitter.Domain.Features.Posts;
using NDDTwitter.Infra.Twitter.Base;
using System;
using System.Collections.Generic;
using Tweetinvi.Models;

namespace NDDTwitter.Infra.Twitter.Features.Posts
{
    public class PostTwitterRepository : IPostRepository
    {
        ITwitterService _twitterService = new TwitterService();

        public PostTwitterRepository(ITwitterService twitterService)
        {
            _twitterService = twitterService;
        }

        public Post Save(Post post)
        {
            post.Validate();
            return Make(_twitterService.SendTweet(post.Message));
        }

        public Post Update(Post post)
        {
            throw new UnsupportedOperationException();
        }
        public void Delete(Post post)
        {
            if (post.Id == 0)
                throw new IdentifierUndefinedException();

            _twitterService.DeleteTweet(post.Id);
        }

        public Post Get(long id)
        {
            try
            {
                Post post = Make(_twitterService.GetTweet(id));
                return post;
            }
            catch (Exception)
            {

                throw new IdentifierUndefinedException();
            }
        }

        public IEnumerable<Post> GetAll()
        {
            List<Post> posts = new List<Post>();
            IEnumerable<ITweet> tweets = _twitterService.ListTweetsOnHomeTimeLine();
            foreach (var item in tweets)
            {
                posts.Add(Make(item));
            }
            return posts;
        }

        private Post Make(ITweet tweet)
        {
            return new Post()
            {
                Id = tweet.Id,
                Message = tweet.FullText,
                PostDate = tweet.CreatedAt,
            };
        }
    }
}
