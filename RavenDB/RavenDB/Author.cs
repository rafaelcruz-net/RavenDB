using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDB
{
    public class Author
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public String Email { get; set; }

        public List<Post> Post { get; set; }
    }

    public class Post
    {
        public Guid Id { get; set; }

        public String Title { get; set; }

        public String Article { get; set; }

        public DateTime DtPost { get; set; }

        public List<Comment> Comments { get; set; }

    }

    public class Comment
    {
        public Guid Id { get; set; }

        public String Title { get; set; }

        public String Text { get; set; }

        public String CommentUser { get; set; }

        public DateTime DtComment { get; set; }

    }
}
