using NDDTwitter.Domain;
using System;
using System.Collections.Generic;

namespace NDDTwitter.Common.Tests.Features.Posts
{
    public static partial class ObjectMother
    {
        public static Post GetPost()
        {
            Post post = new Post
            {
                Message = "Buenas Tchê!",
                PostDate = DateTime.Now.AddMinutes(-10),
            };
            return post;
        }

        public static Post GetEmptyPost()
        {
            Post post = new Post
            {
                Message = "",
                PostDate = DateTime.Now.AddMinutes(-10),
            };
            return post;
        }

        public static Post GetUpdatePost()
        {
            Post post = new Post
            {
                Id = 1,
                Message = "olaoaoaoaooa",
                PostDate = DateTime.Now.AddMinutes(-10),
            };
            return post;
        }

        public static Post GetEmptyUpdatePost()
        {
            Post post = new Post
            {
                Id = 1,
                Message = "",
                PostDate = DateTime.Now.AddMinutes(-10),
            };
            return post;
        }

        public static IEnumerable<Post> GetAllPosts()
        {
            IEnumerable<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Message = "olaoaoaaaaaaaaaa",
                    PostDate = Convert.ToDateTime("2018-02-10 00:01:01"),
                },
                new Post()
                {
                    Message = "laaalalalalalallala",
                    PostDate = DateTime.Now.AddMinutes(-25),
                },
                new Post()
                {
                    Message = "bebebebebebebebebebebebbe",
                    PostDate = DateTime.Now.AddMinutes(-5),
                }
            };
            return posts;
        }

        public static Post SendPosts()
        {
            Post post = new Post
            {
                Message = "#AGORAVAI",
            };
            return post;
        }
    }
}
