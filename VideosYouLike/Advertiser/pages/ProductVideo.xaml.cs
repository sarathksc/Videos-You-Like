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
using Advertiser.libraries;
namespace Advertiser.pages
{
    public partial class ProductVideo : PhoneApplicationPage
    {
        private Boolean isPaused = false;
        private String productid = null;
        public ProductVideo()
        {
            InitializeComponent();
            this.ApplicationTitle.Text = resources.DBRes.applicationTitle;
            this.Loaded += new RoutedEventHandler(ProductVideo_Loaded);
        }

        void ProductVideo_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb=new TextBlock();
            tb.Text = "UserComments";
            productComments.Children.Add(tb);
            libraries.Database db = libraries.Database.getDatabase();
            
            
            string productID = "";
            
            foreach (System.Collections.Generic.KeyValuePair<string, string> item in NavigationContext.QueryString)
            {
                if (item.Key == "productid")
                {
                    productID = item.Value;
                }
            }
            if (productID.Length > 0)
            {
                productid = productID;
                libraries.ProductInformation.ProductInformationData pid = db.getProductInfoDB().getProductByID(productID);
                libraries.CompanyInformation.CompanyInformationData cid = libraries.Database.getDatabase().getCompaniesDB().getCompany(pid.CompanyID);
                this.PageTitle.Text = pid.ProductName;
                this.productDesc.Text = "Company:  " + cid.CompanyName + "\n" + pid.ProductDescription;
                if(pid.ProductVideoUrl.Trim().Length<=1)
                {
                    MessageBox.Show("Check videourl of product with id" + pid.ProductID);
                    return;
                }
                videoPlayer.Source = new Uri(pid.ProductVideoUrl, UriKind.Relative);
                videoPlayer.AutoPlay = true;
            }
            else
                return;
        }

        private void mediaElement1_MediaOpened(object sender, RoutedEventArgs e)
        {
            ((MediaElement)sender).Play();
        }

        private void videoPlayer_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void image1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isPaused)
                this.videoPlayer.Play();
            isPaused = false;
        }

        private void pause_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            if(!isPaused)
            this.videoPlayer.Pause();
            isPaused = true;
        }

        private void stop_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!isPaused)
                this.videoPlayer.Stop();
            isPaused = true;
        }

        private void OnAppbarShareButtonClick(object sender, EventArgs e)
        {
            string destinationUri =
                     String.Format("/pages/Page1.xaml?ProductID={0}",productid);
            
            this.NavigationService.Navigate(new Uri(destinationUri, UriKind.Relative));
        
        }

        private void OnAppbarSetRatingsClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/CurrRating.xaml", UriKind.RelativeOrAbsolute));
        }

        private void OnAppbarSwapColorsClick(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to upload the video and save as a backup?", "Windows Azure",
                                                       MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {

            }
        }

        private void OnAppbarSetStrokeWidthClick(object sender, EventArgs e)
        {
            videoPlayer.Play();
        }

        
    }
}