using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Advertiser.libraries
{
    public class ProductInformation
    {
        System.Collections.Generic.List<ProductInformationData> rows;
        System.Collections.Generic.List<string> Categories;

        public ProductInformation()
        {
            rows = new System.Collections.Generic.List<ProductInformationData>();
            Categories = new System.Collections.Generic.List<string>();
        }
        public void clear()
        {
            if(rows!=null)
            rows.Clear();
        }
        public System.Collections.Generic.List<ProductInformationData> getProductList()
        {
            return rows;
        }
        public System.Collections.Generic.List<ProductInformationData> getProductListByCompany(string cid)
        {
            System.Collections.Generic.List<ProductInformationData> specific=new System.Collections.Generic.List<ProductInformationData>();
            foreach (ProductInformationData item in rows)
            {
                if (item.CompanyID.Trim().ToLower().Contains(cid.Trim().ToLower()))
                    specific.Add(item);
            }
            return specific;
        }
        public System.Collections.Generic.List<ProductInformationData> getProductListByName(string productName)
        {
            System.Collections.Generic.List<ProductInformationData> specific = new System.Collections.Generic.List<ProductInformationData>();
            foreach (ProductInformationData item in rows)
            {
                if (item.ProductName.Trim().ToLower().Contains(productName.Trim().ToLower()))
                    specific.Add(item);
            }
            return specific;
        }
        public System.Collections.Generic.List<ProductInformationData> getProductListByCategory(string category)
        {
            System.Collections.Generic.List<ProductInformationData> specific = new System.Collections.Generic.List<ProductInformationData>();
            foreach (ProductInformationData item in rows)
            {
                if (item.ProductCategory.Trim().ToLower().Contains(category.Trim().ToLower()))
                    specific.Add(item);
            }
            return specific;
        }
        public System.Collections.Generic.List<ProductInformationData> getProductListByNameAndCategory(string productName,string category)
        {
            System.Collections.Generic.List<ProductInformationData> specific = new System.Collections.Generic.List<ProductInformationData>();
            foreach (ProductInformationData item in rows)
            {
                if (item.ProductName.Trim().ToLower().Contains(productName.Trim().ToLower()) && item.ProductCategory.Trim().ToLower().Contains(category.Trim().ToLower()))
                    specific.Add(item);
            }
            return specific;
        }
        public ProductInformationData getProductByID(string productID)
        {
            ProductInformationData pid = new ProductInformationData();
            foreach (ProductInformationData item in rows)
            {
                if (item.ProductID.Trim().ToLower().Contains(productID.Trim().ToLower()))
                {
                    pid = item;
                    break;
                }
            }
            return pid;
        }
        public void addProduct(ProductInformationData pid)
        {
            if(rows==null)
                rows = new System.Collections.Generic.List<ProductInformationData>();
            rows.Add(pid);
            if (!Categories.Contains(pid.ProductCategory.Trim()))
            {
                Categories.Add(pid.ProductCategory);
               // MessageBox.Show("Category Added");
            }
           // MessageBox.Show("Item Added\nid:" + pid.ProductID);
        }
        public System.Collections.Generic.List<string> getCategories()
        {
            return Categories;
        }
        
        public void updateData()
        {

        }
        public struct ProductInformationData
        {
            public string ProductName;
            public string ProductCategory;
            public string ProductID;
            public string ProductDescription;
            public string ProductVideoUrl;
            public string CompanyID;
            public string imageurl;
            public ProductInformationData(string pid, string pname, string pcat, string pdesc, string purl, string cid,string imgurl)
            {
                ProductName = pname;
                ProductCategory = pcat;
                ProductID = pid;
                ProductDescription = pdesc;
                ProductVideoUrl = purl;
                CompanyID = cid;
                imageurl = imgurl;
            }
        }
    }
}
