using BlogUWP.Helper;
using BlogUWP.Model;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// 

    public sealed partial class ViewPostsPage : Page
    {
        private ObservableCollection<Post> Posts = new ObservableCollection<Post>();
        private DatabaseHelper db_helper = new DatabaseHelper();

        public ViewPostsPage()
        {
            this.InitializeComponent();
            try
            {
                Posts = db_helper.GetAllPosts();
                System.Diagnostics.Debug.WriteLine("Post list size: " + Posts.Count());
            }
            catch (NullReferenceException ex)
            {
                System.Diagnostics.Debug.WriteLine("error: " + ex.Message);
            }

        }

        private void DataGrid1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //listBox.Items.Clear();
            // Get the selected items of SfDataGrid
            /*var reflector = this.dataGrid.View.GetPropertyAccessProvider();
            var row = this.dataGrid.SelectedItem;

            foreach (var column in dataGrid.Columns)
            {
                //Get the value from data object based on MappingName 
                var cellvalue = reflector.GetValue(row, column.MappingName);
                //Returns the display value of the cell from data object based on MappingName 
                //var displayValue = reflector.GetFormattedValue(row, column.MappingName);
                listBox.Items.Add(cellvalue.ToString());
            }*/

            System.Diagnostics.Debug.WriteLine("Grid tapped");
        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sender is DataGridCell)
            {
                
                System.Diagnostics.Debug.WriteLine("DataGridCell");
            }

            if (dataGrid1.SelectedItem != null)
            {
                Post row = (Post) dataGrid1.SelectedItem;
                System.Diagnostics.Debug.WriteLine("Grid tapped: " + row.Post_title);

                this.Frame.Navigate(typeof(ViewPostPage), row);
            }
        }
    }
}
