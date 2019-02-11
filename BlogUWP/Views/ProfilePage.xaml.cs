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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BlogUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is User user)
            {
                name.Text = user.Firstname+" "+user.Lastname+"("+user.Username+")";
                if (user.Image != null)
                {
                    userimg.Source = new BitmapImage(new Uri("ms-appdata:///" + user.Image));
                }
                if (user.Email != null)
                {
                    email_txt.Text = user.Email;
                }
                if (user.DateOfBirth != null)
                {
                    DateTimeOffset dateOfBirth = user.DateOfBirth.Value;
                    bday_txt.Text = dateOfBirth.Month.ToString() +", "+ dateOfBirth.Day.ToString() + ", " + dateOfBirth.Year.ToString();
                }
            }
        }
    }
}
