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
    public partial class DashboardScan : System.Web.UI.Page
    {
        public object a;
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
        public DataSet serveur;
        public SqlConnection connection;
        public string idscan;
        public int v;
        protected void Page_Load(object sender, EventArgs e)
        {
            idscan = Request.QueryString["id"];

            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["test"].ToString());
            connection.Open();
            SqlDataAdapter logAdap = new SqlDataAdapter("SELECT top 5 * FROM log where idscan="+idscan+"order by codelog desc  ", connection);
            log = new DataSet();

            logAdap.Fill(log, "log");


            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["test"].ToString());
            cn.Open();
            int nbre = 0;

            SqlDataAdapter serveurAdap = new SqlDataAdapter("SELECT  * from serveur", cn);
            serveur = new DataSet();
            serveurAdap.Fill(serveur, "dsserveur");


        }
    }
}