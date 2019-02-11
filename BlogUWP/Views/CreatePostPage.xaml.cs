using BlogUWP.Helper;
using BlogUWP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BlogUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public sealed partial class CreatePostPage : Page
    {
        public ObservableCollection<Category> categories = new ObservableCollection<Category>();
        public bool m_ignoreNextTextChanged = false;
        public string titleTxt = "";
        public string articleTxt = "";
        public string categoryTxt = "";
        public StorageFile file;
        private string Author;

        public CreatePostPage()
        {
            this.InitializeComponent();
            categories.Add(new Category()
            {
                ID = 1,
                Name = "Creativity"
            });
            categories.Add(new Category()
            {
                ID = 2,
                Name = "Technology"
            });
            categories.Add(new Category()
            {
                ID = 3,
                Name = "Lifestyle"
            });
            categories.Add(new Category()
            {
                ID = 4,
                Name = "Food"
            });
            categories.Add(new Category()
            {
                ID = 5,
                Name = "Travel"
            });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is User user)
            {
                Author = user.Firstname + " " + user.Lastname;
                System.Diagnostics.Debug.WriteLine("Author: " + Author);

                if (user.Image != null)
                {
                    
                }
            }
        }

        public void ComboBox_SelectItemChanged(object sender, SelectionChangedEventArgs e)
        {
            Category category = (Category)ComboBoxCategory.SelectedItem;
            categoryTxt = category.Name;
            System.Diagnostics.Debug.WriteLine("item name: "+category.Name);
        }
       
        private async void PublishButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelper Db_Helper = new DatabaseHelper();

            System.Diagnostics.Debug.WriteLine("titleTxt: " + titleTxt);
            System.Diagnostics.Debug.WriteLine("articleTxt: " + articleTxt);
            System.Diagnostics.Debug.WriteLine("categoryTxt: " + categoryTxt);

            if (titleTxt != "" && articleTxt != "" && categoryTxt != "")
            {
                if (file != null)
                {
                    StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                    //var files = await assets.GetFilesAsync();
                    await file.CopyAsync(storageFolder, file.Name, NameCollisionOption.ReplaceExisting);
                }

                var post = new Post()
                {
                    Post_title = titleTxt,
                    Post_feature_img = file.Name ?? null,
                    Post_date = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"),
                    Article = articleTxt,
                    Author = Author,
                    Category = categoryTxt,
                    Post_status = "Published",
                    Post_like_count = 0,
                    Post_comment_count = 0
                };
                Db_Helper.Insert(post);
                System.Diagnostics.Debug.WriteLine("Article Published");
            }
        }

        private void Article_TextChanging(RichEditBox sender, RichEditBoxTextChangingEventArgs args)
        {
            sender.Document.GetText(TextGetOptions.None, out articleTxt);

            System.Diagnostics.Debug.WriteLine("article: " + articleTxt);
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

        private void ArticleTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            titleTxt = articleTitle.Text;
            System.Diagnostics.Debug.WriteLine("Article Title: " + titleTxt);
        }
    }
}
