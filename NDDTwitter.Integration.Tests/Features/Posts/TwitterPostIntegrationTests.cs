using FluentAssertions;
using NDDTwitter.Application.Features.Posts;
using NDDTwitter.Common.Tests.Features.Posts;
using NDDTwitter.Domain;
using NDDTwitter.Domain.Exceptions;
using NDDTwitter.Domain.Features.Posts;
using NDDTwitter.Infra.Twitter.Base;
using NDDTwitter.Infra.Twitter.Features.Posts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NDDTwitter.Integration.Tests.Features.Posts
{
    [TestFixture]
    public class TwitterPostIntegrationTests
    {

        IPostService _service;
        IPostRepository _repository;
        ITwitterService _twitterService;
        [SetUp]
        public void Initilaze()
        {
            _twitterService = new TwitterService();
            _repository = new PostTwitterRepository(_twitterService);
            _service = new PostService(_repository);
        }

        [Test]
        public void TwitterIntegration_SendPost_ShouldBeOK()
        {
            Post post = ObjectMother.GetPost();
            Post postReceived = _service.Add(post);
            postReceived.Id.Should().NotBe(null);
            Post postGet = _service.Get(postReceived.Id);
            postGet.Message.Should().Be(postReceived.Message);
            IEnumerable<Post> postTimeline = _service.GetAll();
            postTimeline.First().Message.Should().Be(postReceived.Message);
        }

        [Test]
        public void TwitterIntegration_SendPost_ShouldBeFail()
        {
            Post post = ObjectMother.GetEmptyPost();
            Action action = () => _service.Add(post);
            action.Should().Throw<MessageIsNullOrEmpty>();
        }

        [Test]
        public void TwitterIntegration_UpdatePost_ShouldBeFail()
        {
            Post post = ObjectMother.GetUpdatePost();
            Action action = () => _service.Update(post);
            action.Should().Throw<UnsupportedOperationException>();
        }

        [Test]
        public void TwitterIntegration_DeletePost_ShouldBeOK()
        {
            Post post = ObjectMother.GetUpdatePost();
           Post postReceived = _service.Add(post);
            _service.Delete(postReceived);
            Action action = () => _service.Get(postReceived.Id);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void TwitterIntegration_DeletePost_ShouldBeFail()
        {
            Post post = ObjectMother.GetUpdatePost();
            post.Id = 0;
            Action action = () => _service.Delete(post);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void TwitterIntegration_GetPost_ShouldBeOK()
        {
            Post post = ObjectMother.GetUpdatePost();
            post = _service.Add(post);
            Post postReceived = _service.Get(post.Id);
            postReceived.Should().NotBeNull();
            postReceived.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void TwitterIntegration_GetPost_ShouldBeFail()
        {
            Post post = ObjectMother.GetUpdatePost();
            Action action = () => _service.Get(post.Id);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void TwitterIntegration_GetAllPost_ShouldBeOkay()
        {
            IEnumerable<Post> post = _service.GetAll();
            post.Count().Should().BeGreaterThan(0);
        }

    }
}
