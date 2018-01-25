using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace registre
{
    public partial class seConnecter : System.Web.UI.Page
    {
        PFEred.services.Service1 s = new PFEred.services.Service1();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int id = 0;
            Boolean v=false;
            try
            { SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["test"].ToString());
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT userid FROM employe where login='" + txLogin.Value + "' and pwd='" + txPwd.Value + "'", connection);
                object ido = cmd.ExecuteScalar();
                 id = (int)ido;


                
                SqlCommand cmd1 = new SqlCommand("SELECT access FROM employe where login='" + txLogin.Value + "' and pwd='" + txPwd.Value + "'", connection);
                object access = cmd1.ExecuteScalar();
              v = (Boolean)access;
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert message", @"alert('lock ::::" + access + "')", true);

            }
            catch (Exception ex) {
                Response.Redirect("SeConnecter.aspx");
            }

          

            if (s.ValidOTP((txLogin.Value).Trim(), (txPwd.Value).Trim(), (txtToken.Value).Trim()) && v )

            {
                Session["userid"] = id;
                s.wcfMochard(id, "log in");
                Response.Redirect("Accueil.aspx");

            }
            else
            {
                Response.Redirect("SeConnecter.aspx");
            }




        }
    }
}