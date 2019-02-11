using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogUWP.Model
{
    class Post
    {
        [PrimaryKey, AutoIncrement]
        public int Post_id { get; set; }

        public string Post_title { get; set; }

        public string Post_feature_img { get; set; }

        public string Post_date { get; set; }

        public string Article { get; set; }

        public string Post_status { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public int Post_like_count { get; set; }

        public int Post_comment_count { get; set; }

        [ForeignKey(typeof(User))]
        public int User_id { get; set; }

        //[OneToMany(CascadeOperations = CascadeOperation.CascadeDelete)]
        //public List<Comment> Comments { get; set; }
    }
}
