using FluentAssertions;
using NDDTwitter.Domain.Exceptions;
using NUnit.Framework;
using System;

namespace NDDTwitter.Domain.Tests
{
    [TestFixture]
    public class PostTests
    {
        private Post _post;

        [SetUp]
        public void Initilaze()
        {
            _post = new Post();
        }

        [Test]
        public void MessageIsNull()
        {
            Assert.Throws<MessageIsNullOrEmpty>(() => _post.Validate());
        }

        [Test]
        public void MessageIsToLong()
        {
            _post.Message = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            Assert.Throws<MessageIsToLong>(() => _post.Validate());
        }

        [Test]
        public void DateTimeException()
        {
            _post.Message = "por que usar o twitter!";
            _post.PostDate = DateTime.Now.AddMinutes(10);
            //Assert.Throws<DateTimeException>(() => _post.Validate());
            Action action = _post.Validate;
            action.Should().Throw<DateTimeException>();
        }

        [Test]
        public void Post_Shoul_Be_Correct()
        {
            _post.Message = "por que usar o twitter!";
            _post.PostDate = DateTime.Now;
            Assert.DoesNotThrow(_post.Validate);
        }

        [Test]
        public void GetDisplayPostDate()
        {
            _post.Message = "por que usar o twitter!";
            _post.PostDate = DateTime.Now.AddMinutes(-5);
            _post.GetDisplayPostDate();
            _post.DisplayPostDate.Should().Be("5 minutos atrás");
        }

    }
}
