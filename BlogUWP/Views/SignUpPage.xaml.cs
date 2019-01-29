using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Threading.Tasks;
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
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                filename_txt.Text = file.Name;

                StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;

                StorageFolder assets = await appInstalledFolder.GetFolderAsync("Assets");
                //var files = await assets.GetFilesAsync();
                await file.CopyAsync(assets, file.Name, NameCollisionOption.ReplaceExisting);
                Debug.WriteLine("assets: " + assets.Path);
            }
            else
            {
                filename_txt.Text = "Try Again..";
            }
        }

        private async void SignupBtn_Click(object sender, RoutedEventArgs e)
        {
            
            DatabaseHelper Db_Helper = new DatabaseHelper();

            if (fname_txt.Text != "" && lname_txt.Text != "" && username_txt.Text != "" &&
                email_txt.Text != "" && password_txt.Password != "" &&
                confirmPass_txt.Password != "")
            {
                var user = new User()
                {
                    Firstname = fname_txt.Text,
                    Lastname = lname_txt.Text,
                    Username = username_txt.Text,
                    Email = email_txt.Text,
                    DateOfBirth = date_picker.SelectedDate ?? null,
                    Image = null,
                    Password = password_txt.Password,
                };
                Db_Helper.AddUser(user);
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
