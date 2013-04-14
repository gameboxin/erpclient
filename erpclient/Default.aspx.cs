using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Data;
using Newtonsoft.Json;

namespace erpclient
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create("http://localhost:3351/Default.aspx");
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream datastream = response.GetResponseStream();
            StreamReader reader = new StreamReader(datastream);
            string resp = reader.ReadToEnd();
            reader.Close();
            datastream.Close();
            response.Close();
            
            DataSet ds = new DataSet();
            System.IO.StringReader xmlSR = new System.IO.StringReader(resp);
            DataTable myTable = new DataTable("Table");
            myTable.Columns.Add("username", typeof(string));
            myTable.Columns.Add("pass", typeof(string));
            myTable.Columns.Add("dept", typeof(string));
            ds.Tables.Add(myTable);
            ds.ReadXml(xmlSR, XmlReadMode.IgnoreSchema);
            
           // DataSet dt = JsonConvert.DeserializeObject<DataSet>(test);
            GridView1.DataSource = ds;
            GridView1.DataBind();
           //Response.Write(test);
        }
    }
}
