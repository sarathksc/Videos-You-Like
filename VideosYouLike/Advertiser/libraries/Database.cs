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
using System.Xml;

namespace Advertiser.libraries
{
    public class Database
    {
        ProductInformation productInfoTable;
        CompanyInformation companyInfoTable;
        
        private static Database db=null;
        public static Database getDatabase()
        {
            if (db == null)
                db = new Database();
            return db;
        }
        
        private Database()
        {   
            updateData();
        }
        public void readUntil(XmlReader reader,String substr,bool elementType,bool needAttributes)
        {
            while (true)
            {
                if (reader.Name.Contains(substr))
                {
                    if (elementType)
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (needAttributes)
                            {
                                if (reader.HasAttributes)
                                    return;
                            }
                            else
                                return;
                        }
                    }
                    else
                        return;
                }
                reader.Read();
            }
        }
        public void updateData()
        {
            try
            {
                XmlReader reader = XmlReader.Create(new System.IO.StringReader(resources.DBRes.database));
                readUntil(reader, "Table", true,true);
                retrieveProductInfo(reader);
                readUntil(reader, "Table", true,true);
                retrieveCompanyInfo(reader);
                reader.Close();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public ProductInformation getProductInfoDB()
        {
            return productInfoTable;
        }
        public CompanyInformation getCompaniesDB()
        {
            return companyInfoTable;
        }
        #region Retrieving Product Info
        
        public void retrieveProductInfo(XmlReader reader)
        {
            string data = reader.Name;
            if (!reader.GetAttribute(0).Contains("product"))
                return;
            if (data.Contains("Table"))
            {
                reader.Read();
            }
            productInfoTable = new ProductInformation();

            data = reader.Name;
            string[] values=new string[7];
            int count=0;
            
            while (!data.Contains("Table"))
            {
                while(count<values.Length)
                {
                    values[count++]="";
                }
                count=0;
                data = reader.Name;
                while (!(data.Contains("row") && !reader.HasAttributes))
                {
                    
                    if(reader.NodeType == XmlNodeType.Element)
                    {
                        data = reader.Name;
                        count = -1;
                        if (data.Contains("row")&&reader.HasAttributes)
                        {
                            count = 0;
                        }
                        else
                        {
                            if (data.Contains("productname"))
                            {
                                count = 1;
                            }
                            else if (data.Contains("category"))
                            {
                                count = 2;
                            }
                            else if (data.Contains("description"))
                            {
                                count = 3;
                            }
                            else if (data.Contains("videolink"))
                            {
                                count = 4;
                            }
                            else if (data.Contains("companyid"))
                            {
                                count = 5;
                            }
                            else if (data.Contains("image"))
                            {
                                count = 6;
                            }
                        }
                        if (count != -1)
                        values[count] = reader.GetAttribute(0);
                        count = 0;
                    }

                    if (!reader.Read())
                        return;
                    
                    data = reader.Name;
                    if (data.Contains("Table")) return;
                    if (data.Contains("row") && reader.NodeType != XmlNodeType.Element)
                    {
                        productInfoTable.addProduct(new ProductInformation.ProductInformationData(values[0], values[1], values[2], values[3], values[4], values[5],values[6]));
                        break;
                    }
                }
                if (!reader.Read()) break;
                
            }
        }

        #endregion

        #region Retrieveing Company Info

        private void retrieveCompanyInfo(XmlReader reader)
        {
            string data = reader.Name;
            if (!data.Contains("Table"))
                return;
            if (!reader.GetAttribute(0).Contains("company"))
                return;
            if (data.Contains("Table"))
            {
                reader.Read();
            }
            companyInfoTable = new CompanyInformation();

            data = reader.Name;
            string[] values = new string[7];
            int count = 0;

            while (!data.Contains("Table"))
            {
                while (count < values.Length)
                {
                    values[count++] = "";
                }
                count = 0;
                data = reader.Name;
                while (!(data.Contains("row") && !reader.HasAttributes))
                {

                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        data = reader.Name;
                        count = -1;
                        if (data.Contains("row") && reader.HasAttributes)
                        {
                            count = 0;
                        }
                        else
                        {
                            if (data.Contains("companyname"))
                            {
                                count = 1;
                            }
                            else if (data.Contains("email"))
                            {
                                count = 2;
                            }
                            else if (data.Contains("phno"))
                            {
                                count = 3;
                            }
                            else if (data.Contains("username"))
                            {
                                count = 4;
                            }
                            else if (data.Contains("password"))
                            {
                                count = 5;
                            }
                            else if (data.Contains("address"))
                            {
                                count = 6;
                            }
                        }
                        if (count != -1)
                            values[count] = reader.GetAttribute(0);
                        count = 0;
                    }

                    if (!reader.Read())
                        return;

                    data = reader.Name;
                    if (data.Contains("Table")) 
                        return;
                    if (data.Contains("row") && reader.NodeType != XmlNodeType.Element)
                    {
                        companyInfoTable.addProduct(new CompanyInformation.CompanyInformationData(values[0], values[1], values[2], values[3], values[4], values[5],values[6]));
                        break;
                    }
                }
                if (!reader.Read()) break;

            }
        }
        #endregion
    }
}
