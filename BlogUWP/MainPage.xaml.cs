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
using BlogUWP.Views;
using Windows.UI.Xaml.Navigation;
using BlogUWP.Helper;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BlogUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {
            
        }

        private async void Signin_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelper dbHelper = new DatabaseHelper();

            /*foreach (var user in dbHelper.ReadAllUsers())
            {
                System.Diagnostics.Debug.WriteLine(user.Username + "" + user.Password);
            };*/

            if (username_txt.Text != "" && password_txt.Password != "")
            {
                var user = dbHelper.ReadUser(username_txt.Text, password_txt.Password);
                if (user != null)
                {
                    this.Frame.Navigate(typeof(NavigationRoot), user);
                }
            }
            else
            {
                MessageDialog messageDialog = new MessageDialog("Please fill two fields");//Text should not be empty    
                await messageDialog.ShowAsync();
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SignUpPage));
        }
    }
}
