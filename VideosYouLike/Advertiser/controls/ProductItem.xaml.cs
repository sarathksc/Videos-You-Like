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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
namespace Advertiser.controls
{
    public partial class ProductItem : UserControl
    {
        NavigationService navigationService;
        Uri companyNavigationUri;
        Uri productVideoNavigationUri;
        public ProductItem()
        {
            InitializeComponent();
        }
        public ProductItem(Uri companyTarget, Uri productImageSource, Uri ProductVideoTarget, NavigationService nas)
        {
            InitializeComponent();
            initNavigation(nas, companyTarget, ProductVideoTarget, productImageSource);
        }
        public void initNavigation(NavigationService nas,Uri companyTarget,Uri ProductVideoTarget, Uri productImageSource=null )
        {
            companyNavigationUri = companyTarget;
            productVideoNavigationUri = ProductVideoTarget;
            
            navigationService = nas;
            if (productImageSource != null)
            {
                BitmapImage bImage = new BitmapImage(productImageSource);
                MessageBox.Show("" + bImage.PixelHeight);
                productImage.Source = bImage;
            }
            
            
            
            
        }
        public void initData(string companyName,string productTitle,string productDescription)
        {
            this.hb_companyname.Content = "Company:\t"+companyName;
            this.hb_pname.Content = productTitle;
            this.tb_pdesc.Text = "Description:"+productDescription;
        }
        private void hb_companyname_Click(object sender, RoutedEventArgs e)
        {
            if (companyNavigationUri != null&&navigationService!=null)
            {
                navigationService.Navigate(companyNavigationUri);
            }
        }

        private void hb_pname_Click(object sender, RoutedEventArgs e)
        {
            if (productVideoNavigationUri != null && navigationService != null)
            {
                navigationService.Navigate(productVideoNavigationUri);
            }
        }
    }
}
