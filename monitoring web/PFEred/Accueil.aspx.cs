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
    public partial class Accueil : System.Web.UI.Page
    {
        PFEred.services.Service1 s = new services.Service1();
        public DataTable lastScans;
        public DateTime lastDown;
        public DateTime lastUp;
        public DateTime lastScan;
        public DataSet dsf;
        public int tttt;
        public int x;
        public int y;
        public int etat;
        public DataSet log;
        public object a;
        public string test;
        public int cdS;
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["test"].ToString());
           

            //redirection au cas d employe normal
            int id = Convert.ToInt32(Session["userid"]);
          //  id = 1;
            SqlDataAdapter idServeur = new SqlDataAdapter("SELECT * FROM serveur", connection);

            DataSet ids = new DataSet();
            idServeur.Fill(ids, "ids");
            int nbreS = ids.Tables[0].Rows.Count;
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert message", @"alert('nbre de serveur "+ nbreS+"')", true);
            SqlDataAdapter privilege = new SqlDataAdapter("SELECT * FROM Droit where userId=" + id, connection);
            DataSet dsd = new DataSet();
            privilege.Fill(dsd, "d");
            int nbreSD = dsd.Tables[0].Rows.Count;
            
            
          
              //  if (nbreS != nbreSD)
                if (nbreS > nbreSD)
                {
                foreach (DataRow rowP in dsd.Tables["d"].Rows)
                {   cdS = (int)rowP["CodeServeur"]; }

                Response.Redirect("indexM.aspx?id="+cdS);
            }









                //------------------------------

             
            connection.Open();
            SqlDataAdapter logAdap = new SqlDataAdapter("SELECT top 5 * FROM log order by codelog desc", connection);
            log = new DataSet();

            logAdap.Fill(log, "log");


            DataSet ds = s.StatAdmin();
            foreach (DataRow p in ds.Tables[1].Rows)
            {
                lastUp = (DateTime)p["lastup"];
                lastDown = (DateTime)p["lastdown"];
                lastScan = (DateTime)p["lastscan"];
                etat = (int)p["etat"];
                if (etat == 2)
                {
                    test = "reussi";
                }else if (etat == 1) { test = "lent"; }
                else test = "echec";

            }


            if (!Page.IsPostBack)
            {
                Bind();
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
                    if (i == 5)
                    {
                        HiddenField x5 = Master.FindControl("x5") as HiddenField;
                        x5.Value = tttt.ToString();
                        HiddenField y5 = Master.FindControl("y5") as HiddenField;
                        y5.Value = y.ToString();
                    }
                }


            }
           
    }
        public void Bind()
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["test"].ToString());
            cn.Open();
            int nbre=0;
            DataSet serveur;
            SqlDataAdapter serveurAdap = new SqlDataAdapter("SELECT  count(*) as nbre from serveur", cn);
            serveur = new DataSet();
            serveurAdap.Fill(serveur, "dsserveur");
            foreach (DataRow p in serveur.Tables["dsserveur"].Rows)
           
                nbre= (int)p["nbre"];

            SqlDataAdapter da = new SqlDataAdapter("SELECT top "+ nbre +" * FROM log where codeservice=0 and codeserveur>0 order by codelog desc", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            PagedDataSource pgitems = new PagedDataSource();
            DataView dv = new DataView(dt);
            pgitems.DataSource = dv;
            ProductList.DataSource = pgitems;
            ProductList.DataBind();
            Repeater1.DataSource = pgitems;
            Repeater1.DataBind();
           
        }

        protected void refresh_Click(object sender, EventArgs e)
        {
          //  s.noyau();
        }
    }
}