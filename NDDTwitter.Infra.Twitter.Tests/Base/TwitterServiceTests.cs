using NDDTwitter.Infra.Twitter.Base;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Tweetinvi.Models;
using FluentAssertions;

namespace NDDTwitter.Infra.Twitter.Tests.Base
{
    [TestFixture]
    public class TwitterServiceTests
    {
        private TwitterService _twitterService;
        ITweet _currentTweet;
        [SetUp]
        public void Initilaze()
        {
            _twitterService = new TwitterService();
        }

        [Test]
        public void TwitterService_PublishTweet_ShouldBeOk()
        {
            _currentTweet = _twitterService.SendTweet("#Bug");
            _currentTweet.Text.Should().Be("#Bug");
        }

        [Test]
        public void TwitterService_DeleteTweet_ShouldBeOk()
        {
            _currentTweet = _twitterService.SendTweet("Teste de tweet");
            System.Threading.Thread.Sleep(5000);
            bool deletedSuccessfully = _twitterService.DeleteTweet(_currentTweet.Id);
            deletedSuccessfully.Should().Be(true);
            _currentTweet = null;
        }

        [Test]
        public void TwitterService_GetTweetById_ShouldBeOk()
        {
            _currentTweet = _twitterService.SendTweet("Já passou minha fome!!");
            System.Threading.Thread.Sleep(5000);
            ITweet postedTweet = _twitterService.GetTweet(_currentTweet.Id);
            postedTweet.Text.Should().Be("Já passou minha fome!!");
        }

        [Test]
        public void TwitterService_GetTweets_ShouldBeOk()
        {
            IEnumerable<ITweet> postedTweets = _twitterService.ListTweetsOnHomeTimeLine();
            postedTweets.Count().Should().BeGreaterThan(0);
            _currentTweet = null;
        }

        //[Test]
        //public void Deletando_Tweets()
        //{
        //    var tw =_twitterService.GetPosts();
        //    foreach (var item in tw)
        //    {
        //        _twitterService.DeleteTweet(item.Id);
        //    }
        //}

       // [TearDown]
        public void CleanTweet()
        {
            System.Threading.Thread.Sleep(1000);
            if (_currentTweet != null)
                _twitterService.DeleteTweet(_currentTweet.Id);
            _currentTweet = null;
        }

    }
}
