using NUnit.Framework;
using FluentAssertions;
using Moq;
using NDDTwitter.Application.Features.Posts;
using NDDTwitter.Domain.Features.Posts;
using NDDTwitter.Domain;
using System;
using System.Collections.Generic;
using NDDTwitter.Domain.Exceptions;

namespace NDDTwitter.Application.Tests.PostServiceTests
{
    [TestFixture]
    public class PostServiceTest
    {
        IPostService _postService;
        private Mock<IPostRepository> _postRepository;

        [SetUp]
        public void Initilaze()
        {
            _postRepository = new Mock<IPostRepository>();
            _postService = new PostService(_postRepository.Object);
        }

        [Test]
        public void PostService_Add_ShouldBeOk()
        {
            Post post = new Post()
            {
                Message = "olaoaoaoaooa",
                PostDate = DateTime.Now.AddMinutes(-10),
            };
            post.GetDisplayPostDate();
            _postRepository.Setup(x => x.Save(post)).Returns(new Post { Id = 1, Message = post.Message, PostDate = post.PostDate, DisplayPostDate = post.DisplayPostDate });
            Post posted = _postService.Add(post);
            _postRepository.Verify(x => x.Save(post));
            posted.Should().NotBeNull();
            posted.Id.Should().Be(1);
        }

        [Test]
        public void PostService_Add_ShouldBeFail()
        {
            Post post = new Post()
            {
                Message = "",
                PostDate = DateTime.Now.AddMinutes(-10),
            };
            post.GetDisplayPostDate();

            Action action = () => _postService.Add(post);

            action.Should().Throw<MessageIsNullOrEmpty>();
            _postRepository.VerifyNoOtherCalls();

        }
        [Test]
        public void PostService_Update_ShouldBeOk()
        {
            Post post = new Post()
            {
                Id = 1,
                Message = "olaoaoaaaaaaaaaa",
                PostDate = DateTime.Now.AddMinutes(-15),
            };
            post.GetDisplayPostDate();
            _postRepository.Setup(x => x.Update(post));
            _postService.Update(post);
            _postRepository.Verify(x => x.Update(post));
            post.Should().NotBeNull();
            post.Id.Should().Be(1);
        }

        [Test]
        public void PostService_Update_ShouldBeFail()
        {
            Post post = new Post()
            {
                Id = 1,
                Message = "",
                PostDate = DateTime.Now.AddMinutes(-15),
            };
            post.GetDisplayPostDate();

            Action action = () => _postService.Update(post);

            action.Should().Throw<MessageIsNullOrEmpty>();
            _postRepository.VerifyNoOtherCalls();

        }

        [Test]
        public void PostService_Delete_ShouldBeOk()
        {
            Post post = new Post()
            {
                Id = 1,
                Message = "olaoaoaaaaaaaaaa",
                PostDate = DateTime.Now.AddMinutes(-15),
            };
            post.GetDisplayPostDate();
            _postRepository.Setup(x => x.Delete(post));
            _postService.Delete(post);
            _postRepository.Verify(x => x.Delete(post));
        }
        

        [Test]
        public void PostService_Get_ShouldBeOk()
        {
            Post post = new Post()
            {
                Id = 1,
                Message = "olaoaoaaaaaaaaaa",
                PostDate = DateTime.Now.AddMinutes(-15),
            };
            post.GetDisplayPostDate();
            _postRepository.Setup(x => x.Get(1)).Returns(post);
            post = _postService.Get(1);
            _postRepository.Verify(x => x.Get(1));
            post.Should().NotBeNull();
        }

        [Test]
        public void PostService_GetAll_ShouldBeOk()
        {
            IEnumerable<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Message = "olaoaoaaaaaaaaaa",
                    PostDate = Convert.ToDateTime("2018-02-10 00:01:01"),
                },
                new Post()
                {
                    Id = 2,
                    Message = "laaalalalalalallala",
                    PostDate = DateTime.Now.AddMinutes(-25),
                },
                new Post()
                {
                    Id = 3,
                    Message = "bebebebebebebebebebebebbe",
                    PostDate = DateTime.Now.AddMinutes(-5),
                }
            };
            _postRepository.Setup(x => x.GetAll()).Returns(posts);
            posts = _postService.GetAll();
            _postRepository.Verify(x => x.GetAll());
            posts.Should().NotBeNull();
            posts.Should().HaveCount(3);
            //posts.Should().HaveElementAt(0, new Post() { Id = 1, Message = "olaoaoaaaaaaaaaa", PostDate = Convert.ToDateTime("2018-02-10 00:01:01") });
        }
    }
}
