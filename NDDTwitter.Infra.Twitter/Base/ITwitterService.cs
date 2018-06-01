using NDDTwitter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Models;

namespace NDDTwitter.Infra.Twitter.Base
{
    public interface ITwitterService
    {
        ITweet SendTweet(string message);
        ITweet GetTweet(long id);
        IEnumerable<ITweet> ListTweetsOnHomeTimeLine();
        bool DeleteTweet(long id);
        void SetCredential();
    }
}
