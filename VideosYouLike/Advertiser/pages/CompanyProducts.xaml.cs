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
    public partial class CompanyProducts : PhoneApplicationPage
    {
        public CompanyProducts()
        {
            InitializeComponent();
            this.ApplicationTitle.Text = resources.DBRes.applicationTitle;
            this.Loaded += new RoutedEventHandler(CompanyProducts_Loaded);
        }

        void CompanyProducts_Loaded(object sender, RoutedEventArgs e)
        {

            libraries.Database db = libraries.Database.getDatabase();

            System.Collections.Generic.List<libraries.ProductInformation.ProductInformationData> listdata = new List<libraries.ProductInformation.ProductInformationData>();
            string companyID = "";
            
            foreach (System.Collections.Generic.KeyValuePair<string, string> item in NavigationContext.QueryString)
            {
                if (item.Key == "companyid")
                {
                    companyID = item.Value;
                }
            }
            if (companyID.Length > 0)
            {
                listdata = db.getProductInfoDB().getProductListByCompany(companyID);
                libraries.CompanyInformation.CompanyInformationData cid = libraries.Database.getDatabase().getCompaniesDB().getCompany(companyID);
                this.PageTitle.Text = cid.CompanyName.ToUpper() + " Products";
            }
            else
                return;
            
            if (listdata != null)
            {
                Uri image;
                Uri company;
                Uri product;
                foreach (libraries.ProductInformation.ProductInformationData item in listdata)
                {
                    if (item.imageurl == null || item.imageurl.Length < 2)
                        image = null;
                    else
                        image = new Uri(item.imageurl);

                    company = new Uri(resources.DBRes.companyProductsListPage + "?companyid=" + item.CompanyID, UriKind.Relative);
                    product = new Uri(resources.DBRes.productVideoPage + "?productid=" + item.ProductID, UriKind.Relative);

                    Advertiser.controls.ProductItem pi = new controls.ProductItem(company, image, product, this.NavigationService);

                    pi.initData(libraries.Database.getDatabase().getCompaniesDB().getCompany(item.CompanyID).CompanyName, item.ProductName, item.ProductDescription);
                    companyProductsList.Children.Add(pi);
                }

            }
        }
    }
}