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
    public partial class indexM : System.Web.UI.Page
    {
        PFEred.services.Service1 s = new services.Service1();
        public DataTable lastScans;
        public DateTime lastDown;
        public DateTime lastUp;
        public DateTime lastScan;
        public DataSet dsf;
        public DataSet dsServiceM;
        public DataSet dsServiceReussi;

        public DataSet dsRessource;
        public int tttt;
        public int x;
        public int y;
        public double ram;
        public double cpu;
        public double ds1;
        public double ds2;
        public string id;
        public int etat;
        public string test;
        public DataSet logSr;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Test"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["id"];


            Boolean v =  Convert.ToBoolean( Session["verifadmin"]);
            if (!v)
            {
                int id1 = Convert.ToInt32(Session["userid"]);
                s.wcfMochard(id1, "logged into server " + id);
            }
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["test"].ToString());
            connection.Open();
            SqlDataAdapter logAdap = new SqlDataAdapter("SELECT top 5 * FROM log where CodeServeur="+id+"order by codelog desc", connection);
            logSr = new DataSet();

            logAdap.Fill(logSr, "log");



         



            con.Open();
            SqlCommand cmd = new SqlCommand("Select codeservice from monitoring where CodeServeur="+ Convert.ToInt32(id), con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
             dsServiceM = new DataSet();
            da.Fill(dsServiceM,"dsservicem");
            int j = 1; foreach (DataRow p in dsServiceM.Tables["dsservicem"].Rows)
            {
                int codeservice = (int)p["codeservice"];
                SqlCommand cmd2 = new SqlCommand("Select reussi ,codelog from log where CodeServeur=" + Convert.ToInt32(id) + "and codeService=" + codeservice+" and idscan="+s.idscanmax()+" ", con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                dsServiceReussi = new DataSet();
                da2.Fill(dsServiceReussi,"dsreussi");
                foreach (DataRow p1 in dsServiceReussi.Tables["dsreussi"].Rows)
                {
                   int a= (int)p1["reussi"];
                    if (a == 0) a = 10;
                    else if (a == 1) a = 50;
                    else a = 100;



                    if (j == 1)
                    {
                        HiddenField g1 = Master.FindControl("gauge1") as HiddenField;
                        g1.Value =a.ToString() ;
                    }
                    else if (j == 2)
                    {
                        HiddenField g2 = Master.FindControl("gauge2") as HiddenField;
                        g2.Value = a.ToString();
                    } else if (j == 3)
                    {
                        HiddenField g3 = Master.FindControl("gauge3") as HiddenField;
                        g3.Value = a.ToString();
                    }else if (j == 4)
                    {
                        HiddenField g4 = Master.FindControl("gauge4") as HiddenField;
                        g4.Value = a.ToString();
                    }

                    j++;
                }

            }    
           
           
           


            DataSet ds = s.StatServeur(Convert.ToInt32(id));
            foreach (DataRow p in ds.Tables[1].Rows)
            {
                lastUp = (DateTime)p["lastup"];
                lastDown = (DateTime)p["lastdown"];
                lastScan = (DateTime)p["lastscan"];
                etat = (int)p["etat"];
                if (etat == 2)
                {
                    test = "reussi";
                }
                else if (etat == 1) { test = "lent"; }
                else test = "echec";
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

            SqlCommand cmd3 = new SqlCommand("Select ram ,cpu,disque1,disque2 from serveur where CodeServeur=" + Convert.ToInt32(id) +  " ", con);
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
            dsRessource = new DataSet();
            da3.Fill(dsRessource, "dsRessource");
            foreach (DataRow p1 in dsRessource.Tables["dsRessource"].Rows)
            {
               ram = Convert.ToDouble(p1["ram"]);
               cpu = Convert.ToDouble(p1["cpu"]);
               ds1 = Convert.ToDouble(p1["disque1"]);
               ds2 = Convert.ToDouble(p1["disque2"]);


            }
 


        }
    }
}
