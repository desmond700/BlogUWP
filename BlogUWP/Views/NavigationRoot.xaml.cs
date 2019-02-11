using BlogUWP.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BlogUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NavigationRoot : Page
    {
        private static NavigationRoot _instance;
        //private INavigationService _navigationService;
        //private bool hasLoadedPreviously;
        User user;

        public NavigationRoot()
        {
            _instance = this;
            this.InitializeComponent();

        }

        public static NavigationRoot Instance
        {
            get
            {
                return _instance;
            }
        }

        public Frame AppFrame
        {
            get
            {
                return appNavFrame;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is User user)
            {
                this.user = user;
                username_txt.Content = user.Username;
                if(user.Image != null)
                {
                    userImg.UriSource = new Uri("ms-appx:///Assets/" + user.Image);
                }
                
                AppFrame.Navigate(typeof(ProfilePage), e.Parameter);
            }
        }

        private void AppNavFrame_Navigated(object sender, NavigationEventArgs e)
        {
            switch (e.SourcePageType)
            {
                case Type c when e.SourcePageType == typeof(ProfilePage):
                    ((NavigationViewItem)navview.MenuItems[3]).IsSelected = true;
                    break;
                case Type c when e.SourcePageType == typeof(CreatePostPage):
                    ((NavigationViewItem)navview.MenuItems[5]).IsSelected = true;
                    break;
                case Type c when e.SourcePageType == typeof(ViewPostsPage):
                    ((NavigationViewItem)navview.MenuItems[6]).IsSelected = true;
                    break;

            }
        }

        private void Navview_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            /*if (args.IsSettingsInvoked)
            {
                _navigationService.NavigateToSettingsAsync();
                return;
            }*/

            switch (args.InvokedItem as string)
            {
                case "Profile":
                    //_navigationService.NavigateToPodcastsAsync();
                    AppFrame.Navigate(typeof(ProfilePage));
                    break;
                case "Create Post":
                    //_navigationService.NavigateToFavoritesAsync();
                    AppFrame.Navigate(typeof(CreatePostPage), user);
                    break;
                case "View Posts":
                    //_navigationService.NavigateToNotesAsync();
                    AppFrame.Navigate(typeof(ViewPostsPage));
                    break;
         
            }
        }
    }
}
