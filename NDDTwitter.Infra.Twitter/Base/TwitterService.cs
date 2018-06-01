using System;
using System.Collections.Generic;
using System.Configuration;
using NDDTwitter.Domain;
using Tweetinvi;
using Tweetinvi.Models;

namespace NDDTwitter.Infra.Twitter.Base
{
    public class TwitterService : ITwitterService
    {
        private string ConsumerKey = ConfigurationManager.AppSettings.Get("ConsumerKey");
        private string ConsumerSecret = ConfigurationManager.AppSettings.Get("ConsumerSecret");
        private string AccessToken = ConfigurationManager.AppSettings.Get("AccessToken");
        private string AccessTokenSecret =  ConfigurationManager.AppSettings.Get("AccessTokenSecret");
        public TwitterService()
        {
            SetCredential();
        }

        public void SetCredential()
        {
            Auth.SetUserCredentials(ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret);

        }
        public ITweet SendTweet(string message)
        {
            try
            {
                var t =  Tweet.PublishTweet(message);
                return t;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool DeleteTweet(long id)
        {
            try
            {
                return Tweet.DestroyTweet(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ITweet GetTweet(long id)
        {
            try
            {
                return Tweet.GetTweet(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public IEnumerable<ITweet> ListTweetsOnHomeTimeLine()
        {
            try
            {
                return Timeline.GetHomeTimeline();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public IEnumerable<ITweet> GetPosts()
        //{
        //    return Timeline.GetUserTimeline(736995487,0);
        //}


    }
}
