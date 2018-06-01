using NDDTwitter.Domain.Exceptions;
using NDDTwitter.Infra;
using System;

namespace NDDTwitter.Domain
{
    public class Post
    {
        public long Id;
        public string Message;
        public DateTime PostDate;
        public string DisplayPostDate;

        public void Validate()
        {
            if (string.IsNullOrEmpty(Message))
                throw new MessageIsNullOrEmpty();

            if (Message.Length > 140)
                throw new MessageIsToLong();

            if (PostDate > DateTime.Now)
                throw new DateTimeException();
        }

        public void GetDisplayPostDate()
        {
            CalcTime calctime = new CalcTime();
            DisplayPostDate = calctime.CalculateTime(PostDate);
        }
    }
}
