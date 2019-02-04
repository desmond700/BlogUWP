using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogUWP.Model
{
    class Comment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(Post))]
        public int Post_id { get; set; }

        [ForeignKey(typeof(User))]
        public int Blogger_id { get; set; }

        public string User_img { get; set; }

        public string Username { get; set; }

        [ForeignKey(typeof(Comment))]
        public int Reply_to_id { get; set; }

        public int CommentLikeCount { get; set; }

        public string Comment_text { get; set; }

        [NotNull, SQLite.Net.Attributes.Default(value: true)]
        public string Comment_date { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeDelete)]
        public List<Comment> Reply { get; set; }
    }
}
