using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Advertiser.pages
{
    public partial class VideoSearch : PhoneApplicationPage
    {
        public VideoSearch()
        {
            InitializeComponent();
            this.Title= resources.DBRes.applicationTitle;
           // init();
            this.Loaded += new RoutedEventHandler(VideoSearch_Loaded);
        }

        void VideoSearch_Loaded(object sender, RoutedEventArgs e)
        {
            init();
        }
        private void init()
        {
            List<string> cat=libraries.Database.getDatabase().getProductInfoDB().getCategories();
            if (pCategories == null)
            {
                MessageBox.Show("Null");
                pCategories = new ListBox();
            }
            pCategories.Items.Clear();
            pCategories.Items.Add("All");
            if (cat != null)
            {
                foreach (string item in cat)
                {
                    pCategories.Items.Add(item);
                }
            }
            pCategories.SelectionMode = SelectionMode.Single;
            
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string src = resources.DBRes.productSearchResultsPage;
            if (pName.Text.Length > 0)
                src += "?pname=" + pName.Text;
            if (pCategories.SelectedItems.Count > 0 && !(pCategories.SelectedItem.ToString() == "All"))
            {
                if (src.Length > resources.DBRes.productSearchResultsPage.Length)
                    src += "&";
                else
                    src += "?";
                src += "cat=" + pCategories.SelectedItem;
            }
            NavigationService.Navigate(new Uri(src, UriKind.Relative));
        }

       
    }
}