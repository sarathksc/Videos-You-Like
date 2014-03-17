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

namespace RatingControlSample
{
    public partial class CurrRating : PhoneApplicationPage
    {
        // Constructor
        //RatingControlSample r;
        Rating r = new Rating();
        RatingItem ri = new RatingItem();
        public string Val;
        //DependencyProperty dp;
        public CurrRating()
        {
            //Rating r = new Rating();

            InitializeComponent();

        }
        void buttonClick1(object sender, RoutedEventArgs args)
        {
            //string r1 = r.Val;.
            //r = (Rating)sender;
            // MessageBoxResult result = MessageBox.Show(r.setVal()+".0", "Rated", MessageBoxButton.OKCancel);

        }


    }
}