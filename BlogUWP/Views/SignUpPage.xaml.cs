using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
 
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using BlogUWP;
using BlogUWP.Helper;
using BlogUWP.Model;
using Windows.UI.Popups;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BlogUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpPage : Page
    {
        StorageFile file;
        //public static string DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "BlogManager.sqlite"));

        public SignUpPage()
        {
            this.InitializeComponent();
        }

        private async void ImageUploadBtn_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            file = await openPicker.PickSingleFileAsync();
            filename_txt.Text = file.Name;
        }

        private async void SignupBtn_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelper Db_Helper = new DatabaseHelper();

            /*using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
            {
                conn.DropTable<Post>();
            }*/

            if (fname_txt.Text != "" && lname_txt.Text != "" && username_txt.Text != "" &&
                email_txt.Text != "" && password_txt.Password != "" &&
                confirmPass_txt.Password != "")
            {

                if (file != null)
                {
                    StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                    //var files = await assets.GetFilesAsync();
                    //await file.CopyAsync(storageFolder, file.Name, NameCollisionOption.ReplaceExisting);
                }
                
                var user = new User()
                {
                    Firstname = fname_txt.Text,
                    Lastname = lname_txt.Text,
                    Username = username_txt.Text,
                    Email = email_txt.Text,
                    DateOfBirth = date_picker.SelectedDate ?? null,
                    Image = file.Name ?? null,
                    Password = password_txt.Password,
                };
                Db_Helper.Insert(user);
                Frame.Navigate(typeof(NavigationRoot), user);//after add contact redirect to contact listbox page    
            }
            else
            {
                MessageDialog messageDialog = new MessageDialog("Please fill two fields");//Text should not be empty    
                await messageDialog.ShowAsync();
            }
        }

        
    }
}
