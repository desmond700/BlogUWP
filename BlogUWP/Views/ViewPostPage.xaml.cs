using BlogUWP.Helper;
using BlogUWP.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BlogUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewPostPage : Page
    {
        private DatabaseHelper db_helper = new DatabaseHelper();
        BitmapImage bmImage;
        public BitmapImage BMImage
        {
            get
            {
                return bmImage;
            }
        }

        public ViewPostPage()
        {
            this.InitializeComponent();

            

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is Post post)
            {
                


                post = db_helper.GetPost(post.Post_id);

                title_txt.Text = post.Post_title;
                date.Text = post.Post_date;
                category.Text = post.Category;
                //arthor_name.Text = post.Author;

                article_txt.Text = post.Article;
                System.Diagnostics.Debug.WriteLine("Author: ");

                if (post.Post_feature_img != null)
                {

                    Task.Run(async () => {

                        System.Diagnostics.Debug.WriteLine("Async Task begin");
                        System.Diagnostics.Debug.WriteLine("Post_feature_img: " + post.Post_feature_img);

                    /*var file = await ApplicationData.Current.LocalFolder.GetFileAsync(post.Post_feature_img);
                    var stream = await file.OpenAsync(FileAccessMode.Read);
                    var bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(stream);*/
                    StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/296915_286279161401439_1126264316_n.jpg"));
                        bmImage.UriSource = new Uri(file.Path);
                        using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                        {
                            BitmapImage image = new BitmapImage();
                            image.SetSource(fileStream);
                            post_featured_img.Source = new BitmapImage(new Uri(file.Path));//new Uri("c:\users\desmo\source\repos\BlogUWP\BlogUWP\bin\x86\Debug\AppX\Assets\296915_286279161401439_1126264316_n.jpg");
                        }
                        //post_featured_img.Source = new BitmapImage(new Uri("ms-appx:///Assets/296915_286279161401439_1126264316_n.jpg", UriKind.Absolute)); ;
                        //System.Diagnostics.Debug.WriteLine("UriSource"+bitmapImage.UriSource);
                    });

                    /*#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    ThreadPool.RunAsync(new WorkItemHandler(async handler =>
                     {
                         System.Diagnostics.Debug.WriteLine("Async Task begin");
                         //Task<Post> tmydata = new Task<Post>();
                         //tmydata.Wait();

                         var file = await ApplicationData.Current.LocalFolder.GetFileAsync(post.Post_feature_img);
                         var stream = await file.OpenAsync(FileAccessMode.Read);
                         var bitmapImage = new BitmapImage();
                         await bitmapImage.SetSourceAsync(stream);
                         post_featured_img.Source = bitmapImage;
                         // now show the data.
                         // use dispatcher to manipulate UI controls
                     })).Completed = new AsyncActionCompletedHandler((IAsyncAction source, AsyncStatus status) =>
                    {
                        System.Diagnostics.Debug.WriteLine("Async Task Completed");
                    });
                    #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed*/
                    
                }
            }
        }
    }
}
