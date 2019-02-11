using BlogUWP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogUWP.Helper
{
    class DatabaseHelper
    {
        public void CreateDatabase(string DB_PATH)
        {
            if (!CheckFileExists(DB_PATH).Result)
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
                {
                    conn.CreateTable<User>();
                    //conn.CreateTable<Comment>();
                    conn.CreateTable<Post>();
                }
            }
        }

        private async Task<bool> CheckFileExists(string fileName)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Insert the new contact in the Contacts table.   
        public void Insert(dynamic objUser)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    int result = conn.Insert(objUser);
                    System.Diagnostics.Debug.WriteLine("INSERT: " + result); 
                });
            }
        }

        // Retrieve the specific contact from the database.     
        public User ReadUser(string username, string password)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingUser = conn.Query<User>("SELECT * FROM User WHERE Username = '"+username+"' AND Password = '"+password+"'").FirstOrDefault();
                return existingUser;
            }
        }

        private string ReduceTextLength(string str)
        {
            return str.Substring(0, 100);
        }

        // Retrieve the specific contact from the database.     
        public ObservableCollection<Post> GetAllPosts()
        {
            System.Diagnostics.Debug.WriteLine("get posts out ");
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var query = conn.Table<Post>().ToList<Post>();
                System.Diagnostics.Debug.WriteLine("query: "+query.ToString());
                ObservableCollection<Post> list = new ObservableCollection<Post>();
                foreach(var _item in query)
                {
                    System.Diagnostics.Debug.WriteLine("posts id: "+ _item.Post_id);
                    var item = new Post()
                    {
                        Post_id = _item.Post_id,
                        Post_title = _item.Post_title,
                        Post_feature_img = _item.Post_feature_img ?? null,
                        Post_date = _item.Post_date,
                        Article = ReduceTextLength(_item.Article),
                        Author = _item.Author,
                        Category = _item.Category,
                        Post_status = _item.Post_status,
                        Post_like_count = _item.Post_like_count,
                        Post_comment_count = _item.Post_comment_count
                    };
                    list.Add(item);
                }
                System.Diagnostics.Debug.WriteLine("get posts test: ");
                System.Diagnostics.Debug.WriteLine("get posts: "+list.ToString());
                return list;
            }
        }

        // Retrieve the specific contact from the database.     
        public Post GetPost(int post_id)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                var existingPost = (conn.Table<Post>().Where( c => c.Post_id == post_id)).Single();
                return existingPost;
            }
        }

        public ObservableCollection<User> ReadAllUsers()
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<User> myCollection = conn.Table<User>().ToList<User>();
                    ObservableCollection<User> usersList = new ObservableCollection<User>(myCollection);
                    return usersList;
                }
            }
            catch
            {
                return null;
            }

        }

        //Update existing conatct   
        public void UpdateDetails(User ObjUser)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                var existingconact = conn.Query<User>("select * from User where Id =" + ObjUser.Id).FirstOrDefault();
                if (existingconact != null)
                {

                    conn.RunInTransaction(() =>
                    {
                        conn.Update(ObjUser);
                    });
                }

            }
        }

        //Delete all contactlist or delete Contacts table     
        public void DeleteAllUser()
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                conn.DropTable<User>();
                conn.CreateTable<User>();
                conn.Dispose();
                conn.Close();

            }
        }

        //Delete specific contact     
        public void DeleteUser(int Id)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                var existingUser = conn.Query<User>("select * from User where Id =" + Id).FirstOrDefault();
                if (existingUser != null)
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Delete(existingUser);
                    });
                }
            }
        }
    }
}
