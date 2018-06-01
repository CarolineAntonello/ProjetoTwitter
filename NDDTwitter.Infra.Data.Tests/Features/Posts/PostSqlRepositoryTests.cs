using FluentAssertions;
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
using System.Text;
using System.Threading.Tasks;

namespace NDDTwitter.Infra.Data.Tests.Features.Posts
{
    [TestFixture]
    public class PostSqlRepositoryTests
    {
        private IPostRepository _repository;
        private Post _post;

        [SetUp]
        public void Initilaze()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new PostSqlRepository();
        }

        [Test]
        public void PostSql_Save_ShouldBeOk()
        {
            _post = ObjectMother.GetPost();
            _post = _repository.Save(_post);
            _post.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void PostSql_Save_ShouldBefail()
        {
            _post = ObjectMother.GetEmptyPost();
            Action action = () => _repository.Save(_post);
            action.Should().Throw<MessageIsNullOrEmpty>(); 
        }

        [Test]
        public void PostSql_Update_ShouldBeOk()
        {
            _post = ObjectMother.GetUpdatePost();
            _repository.Update(_post);
            Post p = _repository.Get(_post.Id);
            _post.Message.Should().Be(p.Message);
            p.PostDate.ToShortDateString().Should().Be(_post.PostDate.ToShortDateString());
        }

        [Test]
        public void PostSql_Update_ShouldBeFail()
        {
            _post = ObjectMother.GetEmptyUpdatePost();
            Action action = () => _repository.Update(_post);
            action.Should().Throw<MessageIsNullOrEmpty>();
        }

        [Test]
        public void PostSql_Delete_ShouldBeOk()
        {
            _post = ObjectMother.GetUpdatePost();
            _repository.Delete(_post);
            Post p = _repository.Get(_post.Id);
            p.Should().BeNull();
        }

        [Test]
        public void PostSql_GetAll_ShouldBeOk()
        {
            IEnumerable<Post> posts = ObjectMother.GetAllPosts();
            foreach (var post in posts)
            {
                _repository.Save(post);
            }
            IEnumerable<Post> posted = _repository.GetAll();
            posted.Count().Should().Be(4);
        }

    }
}
