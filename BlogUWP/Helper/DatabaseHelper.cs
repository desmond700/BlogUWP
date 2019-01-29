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
        public void AddUser(User objUser)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(objUser);
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

        public ObservableCollection<User> ReadAllUsers()
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<User> myCollection = conn.Table<User>().ToList<User>();
                    ObservableCollection<User> ContactsList = new ObservableCollection<User>(myCollection);
                    return ContactsList;
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
