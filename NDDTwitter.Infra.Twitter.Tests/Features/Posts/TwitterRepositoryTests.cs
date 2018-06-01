using FluentAssertions;
using Moq;
using NDDTwitter.Domain;
using NDDTwitter.Domain.Exceptions;
using NDDTwitter.Domain.Features.Posts;
using NDDTwitter.Infra.Twitter.Base;
using NDDTwitter.Infra.Twitter.Features.Posts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;

namespace NDDTwitter.Infra.Twitter.Tests.Features.Posts
{
    [TestFixture]
    public class TwitterRepositoryTests
    {
        private Mock<ITwitterService> _twitterService;
        private Mock<ITweet> _tweet;
        private Mock<List<ITweet>> _tweets; 
        private PostTwitterRepository _repository;

        [SetUp]
        public void Initilaze()
        {
            _twitterService = new Mock<ITwitterService>();
            _repository = new PostTwitterRepository(_twitterService.Object);
            _tweet =  new Mock<ITweet>();
            _tweets = new Mock<List<ITweet>>();
        }

        [Test]
        public void TwitterService_Add_ShouldBeOk()
        {
            Post post = new Post()
            {
                Message = "Um post qualquer!",
            };
            _twitterService.Setup(ts => ts.SendTweet(post.Message)).Returns(_tweet.Object);
            Post postReturn = _repository.Save(post);
            postReturn.Should().NotBeNull();
        }

        [Test]
        public void TwitterService_Add_ShouldBeFail()
        {
            Post post = new Post()
            {
                Message = "",
            };
            Action action = () => _repository.Save(post);
            action.Should().Throw<MessageIsNullOrEmpty>();
            _twitterService.VerifyNoOtherCalls();
        }

        [Test]
        public void TwitterService_Update_ShouldBeOk()
        {
            Post post = new Post()
            {
                Id = 1,
                Message = "tweet de Bom dia!",
                PostDate = DateTime.Now.AddMinutes(-15),
            };
            post.GetDisplayPostDate();
            Action action = () => _repository.Update(post);
            action.Should().Throw<UnsupportedOperationException>();
        }

        [Test]
        public void TwitterService_Delete_ShouldBeOk()
        {
            Post post = new Post()
            {
                Id = 1,
                Message = "olaoaoaaaaaaaaaa",
                PostDate = DateTime.Now.AddMinutes(-15),
            };
            post.GetDisplayPostDate();
            _twitterService.Setup(ts => ts.DeleteTweet(post.Id));
            _repository.Delete(post);
            _twitterService.Verify(ts => ts.DeleteTweet(post.Id));
        }

        [Test]
        public void TwitterService_Delete_ShouldBeFail()
        {
            Post post = new Post()
            {
                Id = 0,
                Message = "olaoaoaaaaaaaaaa",
                PostDate = DateTime.Now.AddMinutes(-15),
            };
            post.GetDisplayPostDate();
            _twitterService.Setup(ts => ts.DeleteTweet(post.Id));
            Action action = () => _repository.Delete(post);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void TwitterService_Get_ShouldBeOk()
        {
            Post post = new Post()
            {
                Id = 1,
                Message = "tchaaaaaaau!",
                PostDate = DateTime.Now.AddMinutes(-15),
            };
            post.GetDisplayPostDate();
            _twitterService.Setup(ts => ts.GetTweet(It.IsAny<long>())).Returns(_tweet.Object);
            Post postReturn =_repository.Get(post.Id);
            _twitterService.Verify(x => x.GetTweet(post.Id));
            postReturn.Should().NotBeNull();
        }
     
        [Test]
        public void TwitterService_GetAll_ShouldBeOk()
        {
            _twitterService.Setup(ts => ts.ListTweetsOnHomeTimeLine()).Returns(_tweets.Object);
            IEnumerable<Post> postReturn = _repository.GetAll();
            _twitterService.Verify(x => x.ListTweetsOnHomeTimeLine());
            postReturn.Should().NotBeNull();
        }

    }
}
