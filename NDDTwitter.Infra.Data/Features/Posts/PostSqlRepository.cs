using NDDTwitter.Domain;
using NDDTwitter.Domain.Features.Posts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDDTwitter.Infra.Data.Features.Posts
{
    public class PostSqlRepository : IPostRepository
    {
        private string _sqlAdd = "insert into Posts(Message, PostDate) values (@Message, @PostDate)";
        private string _sqlUpdate = "update Posts set Message = @Message, PostDate = @PostDate where Id = @Id";
        private string _sqlGetById = "select *from Posts where Id = @Id";
        private string _sqlDelete = "delete from Posts where Id = @Id";
        private string _sqlGetAll = "select * from Posts";

        public Post Save(Post post)
        {
            post.Validate();
            post.Id = Db.Insert(_sqlAdd, Take(post));
            return post;
        }
        public Post Update(Post post)
        {
            post.Validate();
            Db.Update(_sqlUpdate, Take(post));
            return Db.Get(_sqlGetById, Make, new object[] { "@Id", post.Id });
        }
        public void Delete(Post post)
        {
            Db.Delete(_sqlDelete, Take(post));
        }
        public Post Get(long id)
        {
            Post post = Db.Get<Post>(_sqlGetById, Make, new object[] { "@Id", id });
            return post;
        }
        public IEnumerable<Post> GetAll()
        {
            return Db.GetAll<Post>(_sqlGetAll, Make);
        }

        /// <summary>
        /// Cria um objeto Customer baseado no DataReader.
        /// </summary>
        private static Func<IDataReader, Post> Make = reader =>
           new Post
           {
               Id = Convert.ToInt64(reader["Id"]),
               Message = reader["Message"].ToString(),
               PostDate = Convert.ToDateTime(reader["PostDate"])
           };

        /// <summary>
        /// Cria a lista de parametros do objeto Post para passar para o comando Sql
        /// </summary>
        /// <param name="post">Post.</param>
        /// <returns>lista de parametros</returns>
        private object[] Take(Post post)
        {
            return new object[]
            {
                "@Id", post.Id,
                "@Message", post.Message,
                "@PostDate", post.PostDate
            };
        }
    }
}
