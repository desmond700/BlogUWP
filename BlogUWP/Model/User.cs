using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogUWP.Model
{
    class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTimeOffset? DateOfBirth { get; set; }

        public string Image { get; set; }

        public string Password { get; set; }

        public string Languages { get; set; }

        public string DateJoined { get; set; }

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string Linkedin { get; set; }

        public string Instagram { get; set; }

        public string Github { get; set; }
    }
}
