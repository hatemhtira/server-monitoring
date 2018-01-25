using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PFEred
{
    public partial class DashboardService : System.Web.UI.Page
    {
        PFEred.services.Service1 s = new services.Service1();
        public string id1;
        public string id2;

        public DataTable lastScans;
        public DateTime lastDown;
        public DateTime lastUp;
        public DateTime lastScan;
        public DataSet dsf;
        public DataSet logSc;
        public int etat;
        public int tttt;
        public int x;
        public int y;
        protected void Page_Load(object sender, EventArgs e)
        {
        

            id1 = Request.QueryString["id1"];
            id2 = Request.QueryString["id2"];
            Boolean v = (Boolean)Session["verifadmin"];
            if (!v)
            {
                int id = Convert.ToInt32(Session["userid"]);
                s.wcfMochard(id, "log into service " + id1 + ",in server " + id2);
            }
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["test"].ToString());
            connection.Open();
            SqlDataAdapter logAdap = new SqlDataAdapter("SELECT top 5 * FROM log where CodeServeur=" + id2+"and codeservice="+id1+"order by codelog desc", connection);
            logSc = new DataSet();

            logAdap.Fill(logSc, "log");



            DataSet ds = s.StatService(Convert.ToInt32(id2), Convert.ToInt32(id1));
            foreach (DataRow p in ds.Tables[1].Rows)
            {
                lastUp = (DateTime)p["lastup"];
                lastDown = (DateTime)p["lastdown"];
                lastScan = (DateTime)p["lastscan"];
                etat = (int)p["etat"];
            }


            if (!Page.IsPostBack)
            {
                int i = 0;
                
                foreach (DataRow p1 in ds.Tables[0].Rows)
                {
                    i++;
                    x = (int)p1["idscan"];
                    y = (int)p1["reussi"];
                    tttt = Convert.ToInt32(x);

                    if (i == 1)
                    {
                        HiddenField x1 = Master.FindControl("x1") as HiddenField;
                        x1.Value = tttt.ToString();
                        HiddenField y1 = Master.FindControl("y1") as HiddenField;
                        y1.Value = y.ToString();
                    }
                    if (i == 2)
                    {
                        HiddenField x2 = Master.FindControl("x2") as HiddenField;
                        x2.Value = x2.Value = tttt.ToString();
                        HiddenField y2 = Master.FindControl("y2") as HiddenField;
                        y2.Value = y.ToString();
                    }
                    if (i == 3)
                    {
                        HiddenField x3 = Master.FindControl("x3") as HiddenField;
                        x3.Value = tttt.ToString();
                        HiddenField y3 = Master.FindControl("y3") as HiddenField;
                        y3.Value = y.ToString();
                    }
                    if (i == 4)
                    {
                        HiddenField x4 = Master.FindControl("x4") as HiddenField;
                        x4.Value = tttt.ToString();
                        HiddenField y4 = Master.FindControl("y4") as HiddenField;
                        y4.Value = y.ToString();
                    }
                }
            }
        }
    }
}