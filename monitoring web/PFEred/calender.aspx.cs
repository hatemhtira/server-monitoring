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
    public partial class calender : System.Web.UI.Page
    {
        public DateTime D  ;
      public   DataSet Log;
     public    DateTime t;
        public string msg;
        protected void Page_Load(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Test"].ToString());
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter logAdap = new SqlDataAdapter("SELECT * FROM Log where reussi =0", conn);
            Log = new DataSet(); 
            logAdap.Fill(Log, "dsServeur");
            //foreach (DataRow pRow in Log.Tables["dsServeur"].Rows)
            //{

            //   t  = (DateTime)pRow["temps"] ;
            //    msg = (string)pRow["Message_log"];
            //    D = new DateTime(t.Year, t.Month, t.Day, t.Hour, t.Minute, t.Second);
            //}



        }
    }
}