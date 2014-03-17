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
    public class CompanyInformation
    {
        System.Collections.Generic.List<CompanyInformationData> rows;

        public CompanyInformation()
        {
            rows = new System.Collections.Generic.List<CompanyInformationData>();
        }
        public void clear()
        {
            if(rows!=null)
            rows.Clear();
        }
        public System.Collections.Generic.List<CompanyInformationData> getCompaniesList()
        {
            return rows;
        }
        public void addProduct(CompanyInformationData pid)
        {
            if(rows==null)
                rows = new System.Collections.Generic.List<CompanyInformationData>();
            rows.Add(pid);
           // MessageBox.Show("Company Added\nid:" + pid.CompanyID);
        }
        public CompanyInformationData getCompany(string companyid)
        {
            if(rows!=null)
            foreach (CompanyInformationData ci in rows)
            {
                if (ci.CompanyID == companyid)
                {
                    return ci;
                }
            }
            return new CompanyInformationData();
        }
        public void updateData()
        {

        }
        public struct CompanyInformationData
        {
            public string CompanyName;
            public string CompanyEmail;
            public string CompanyPhno;
            public string username;
            public string password;
            public string CompanyAddress;
            public string CompanyID;

            public CompanyInformationData(string cid, string cname, string email, string phno, string user, string pass,string addr)
            {
                CompanyName = cname;
                CompanyEmail = email;
                CompanyPhno = phno;
                username = user;
                password = pass;
                CompanyAddress = addr;
                CompanyID = cid;
            }
        }
    }
}
