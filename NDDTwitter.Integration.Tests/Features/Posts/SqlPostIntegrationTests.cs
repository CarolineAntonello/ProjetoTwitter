using FluentAssertions;
using NDDTwitter.Application.Features.Posts;
using NDDTwitter.Common.Tests.Base;
using NDDTwitter.Common.Tests.Features.Posts;
using NDDTwitter.Domain;
using NDDTwitter.Domain.Exceptions;
using NDDTwitter.Domain.Features.Posts;
using NDDTwitter.Infra.Data.Features.Posts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NDDTwitter.Integration.Tests.Features
{
    [TestFixture]
    public class SqlPostIntegrationTests
    {
        IPostService _service;
        IPostRepository _repository;

        [SetUp]
        public void Initilaze()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new PostSqlRepository();
            _service = new PostService(_repository);
        }

        [Test]
        public void Integration_AddPost_ShouldBeOK()
        {
            Post post = ObjectMother.GetPost();
            Post postReceived = _service.Add(post);
            var postVerify = _service.Get(post.Id);
            postVerify.Should().NotBeNull();
            postVerify.Id.Should().Be(post.Id);
        }

        [Test]
        public void Integration_AddPost_ShouldBeFail()
        {
            Post post = ObjectMother.GetEmptyPost();
            Action action = () => _service.Add(post);
            action.Should().Throw<MessageIsNullOrEmpty>();
        }

        [Test]
        public void Integration_UpdatePost_ShouldBeOK()
        {
            Post post = ObjectMother.GetUpdatePost();
            Post postReceived = _service.Update(post);
            postReceived.Id.Should().Be(post.Id);
            postReceived.Message.Should().Be(post.Message);
        }

        [Test]
        public void Integration_UpdatePost_ShouldBeFail()
        {
            Post post = ObjectMother.GetEmptyUpdatePost();
            Action action = () => _service.Add(post);
            action.Should().Throw<MessageIsNullOrEmpty>();
        }

        [Test]
        public void Integration_DeletePost_ShouldBeOK()
        {
            Post post = ObjectMother.GetUpdatePost();
            _service.Delete(post);
            Post postReceived =_service.Get(post.Id);
            postReceived.Should().BeNull();
        }

        [Test]
        public void Integration_GetPost_ShouldBeOK()
        {
            Post post  = _service.Get(1);
            post.Should().NotBeNull();
            post.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Integration_GetPost_ShouldBeFail()
        {
            Post post = _service.Get(2);
            post.Should().BeNull();
        }

        [Test]
        public void Integration_GetAllPost_ShouldBeOkay()
        {
            IEnumerable<Post> post = _service.GetAll();
            post.Count().Should().BeGreaterThan(0);
        }

    }
}
