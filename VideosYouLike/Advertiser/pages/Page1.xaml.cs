using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
using Microsoft.Xna.Framework.Media;
using Advertiser;
using Advertiser.libraries;

namespace Facebook.Samples.AuthenticationTool
{
    public partial class Page1 : PhoneApplicationPage
    {
        private const string _appId = "139904712746301";
        private readonly string[] _extendedPermissions = new[] { "user_about_me", "publish_stream" };

        private bool _loggedIn = false;


        int composerInfoIndex;
        int albumInfoIndex;
        CompanyInformation composerInfo;
        ProductInformation.ProductInformationData thisPagesAlbum;
        private FacebookClient _fbClient;

        // At this point we have an access token so we can get information from facebook
        private void loginSucceeded()
        {
            Post(_fbClient);
            //NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.RelativeOrAbsolute));
            this.NavigationService.GoBack();
            //TitlePanel.Visibility = Visibility.Visible;
            //FacebookLoginBrowser.Visibility = Visibility.Collapsed;
            //InfoPanel.Visibility = Visibility.Visible;

            //_fbClient.GetCompleted +=
            //    (o, e) =>
            //    {
            //        if (e.Error == null)
            //        {
            //            var result = (IDictionary<string, object>)e.GetResultData();
            //            Dispatcher.BeginInvoke(() => MyData.ItemsSource = result);
            //        }
            //        else
            //        {
            //            MessageBox.Show(e.Error.Message);
            //        }
            //    };

            //_fbClient.GetAsync("/me");
        }

        // Constructor
        public Page1()
        {
            InitializeComponent();
            _fbClient = new FacebookClient();
            FacebookLoginBrowser.Loaded += new RoutedEventHandler(FacebookLoginBrowser_Loaded);
            //Get_User(_fbClient);
            //Get_Post(_fbClient);
            //Post(_fbClient);

        }

        // Browser control is loaded and fully ready for use
        void FacebookLoginBrowser_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_loggedIn)
            {
                LoginToFacebook();
            }
        }

        // This handles the display a little and also creates the initial URL to navigate to in the browser control
        private void LoginToFacebook()
        {
            TitlePanel.Visibility = Visibility.Visible;
            FacebookLoginBrowser.Visibility = Visibility.Visible;
            InfoPanel.Visibility = Visibility.Collapsed;

            var loginParameters = new Dictionary<string, object>
                                      {
                                          { "response_type", "token" }
                                          // { "display", "touch" } // by default for wp7 builds only (in Facebook.dll), display is set to touch.
                                      };

            var navigateUrl = FacebookOAuthClient.GetLoginUrl(_appId, null, _extendedPermissions, loginParameters);

            FacebookLoginBrowser.Navigate(navigateUrl);
            if (this.NavigationContext.QueryString.ContainsKey("ComposerInfoIndex"))
            {
                composerInfoIndex = Int32.Parse(this.NavigationContext.QueryString["ComposerInfoIndex"]);
                albumInfoIndex = Int32.Parse(this.NavigationContext.QueryString["AlbumInfoIndex"]);
            }
            String productID = null;
            if (this.NavigationContext.QueryString.ContainsKey("ProductID"))
            {
                productID = this.NavigationContext.QueryString["ProductID"];
            }
            composerInfo = Database.getDatabase().getCompaniesDB();
            ProductInformation albumInfo = Database.getDatabase().getProductInfoDB();
            this.DataContext = albumInfo;

            thisPagesAlbum = albumInfo.getProductByID(productID);

        }


        private void Post(FacebookClient _fbClient)
        {
            var client = _fbClient;
            var parameters = new Dictionary<string, object>
       {
            { "message", "I am watching "+thisPagesAlbum.ProductName},
            { "link", "http://www.google.com/#sclient=psy&hl=en&q="+thisPagesAlbum.ProductName+"&aq=f&aqi=&aql=&oq=&gs_rfai=&pbx=1&fp=76258fd74ceb8990&cad=b&bav=on.2,or.r_gc.r_pw." },
            { "picture", "http://download.codeplex.com/Project/Download/FileDownload.aspx?ProjectName=facebooksdk&DownloadId=170794&Build=17672" },
            { "name", "Videos You Like" },
            { "caption", "Windows Phone 7 Video App" },
            { "description", "Developed for Windows Phone 7 applications that integrate with Facebook." },
            { "privacy", new Dictionary<string, object>
                {
                    { "value",  "ALL_FRIENDS" }
                }
            }
        };

            client.PostAsync("me/feed", parameters);
            MessageBox.Show("Sucessfully Posted");
        }

        private void FacebookLoginBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            FacebookOAuthResult oauthResult;
            if (FacebookOAuthResult.TryParse(e.Uri, out oauthResult))
            {
                if (oauthResult.IsSuccess)
                {
                    _fbClient = new FacebookClient(oauthResult.AccessToken);
                    _loggedIn = true;
                    loginSucceeded();
                }
                else
                {
                    MessageBox.Show(oauthResult.ErrorDescription);
                }
            }
        }


    }
}