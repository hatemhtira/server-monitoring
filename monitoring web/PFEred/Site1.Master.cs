using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PFEred
{
    public partial class Site1testestes : MasterPage
    {
        public string NP;
        public int cdS;
        services.Service1 s = new services.Service1();
        public DataSet log;
        public int id;
        // public int id { get; set; }



        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["userid"] == null)
            {
                Response.Redirect("SeConnecter.aspx");
            }
            id = Convert.ToInt32(Session["userid"]);

            // id = 1;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["test"].ToString());

            connection.Open();
            SqlDataSource2.SelectParameters.Add("id", id.ToString());
            //SqlDataAdapter logAdap = new SqlDataAdapter("SELECT * FROM log", connection);
            //log = new DataSet();

            //logAdap.Fill(log, "lloogg");
            //  var a = log.Tables[0].Rows.Count;

            SqlCommand i = new SqlCommand("SELECT CONCAT(Nom, ' ', Prenom ) as NP FROM Employe where userid=" + id, connection);
            object t = i.ExecuteScalar();
            NP = (string)t;

            SqlDataAdapter idServeur = new SqlDataAdapter("SELECT * FROM serveur", connection);

            DataSet ids = new DataSet();
            idServeur.Fill(ids, "ids");
            // int nbreS = ids.Tables[0].Rows.Count;
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert message", @"alert('nbre de serveur "+ nbreS+"')", true);
            SqlDataAdapter privilege = new SqlDataAdapter("SELECT * FROM Droit where userId=" + id, connection);
            DataSet ds = new DataSet();
            privilege.Fill(ds, "p");
            int nbreSp = ds.Tables[0].Rows.Count;
            //verification admin
            if (nbreSp > 1)
            {
                Session["verifadmin"] = true;

                SqlDataAdapter logAdap = new SqlDataAdapter("SELECT * FROM [MonitoringDB].[dbo].[log] where reussi=0 and DATEDIFF(YEAR,Temps,GETDATE())<3  order by Codelog desc;", connection);
                log = new DataSet();

                logAdap.Fill(log, "lloogg");
                // var a = log.Tables[0].Rows.Count;
                aAccuei.Visible = true;
                aDashboardServer.Visible = true;
                aGestionEmploye.Visible = true;
                aLog.Visible = true;
                aConsulterLesService.Visible = true;
                aService.Visible = true;
                aAjoutService.Visible = true;
                aMonitoring.Visible = true;
                aconsulterMonitoring.Visible = true;
                aServeur.Visible = true;
                aConsulterServeur.Visible = true;
                aAjoutServeur.Visible = true;
                aConsulterMochard.Visible = true;
                aConsulterIntervantion.Visible = true;

                foreach (DataRow p in ids.Tables["ids"].Rows)
                {
                    int ID = (int)p["CodeServeur"];
                    String n = ((string)p["nom"]).Trim();

                    HtmlGenericControl li = new HtmlGenericControl("li");
                    sub.Controls.Add(li);
                    HtmlGenericControl anchor = new HtmlGenericControl("a");
                    anchor.Attributes.Add("href", "indexM.aspx?id=" + ID);
                    anchor.InnerText = "serveur " + ID + "--" + n;
                    li.Controls.Add(anchor);

                }
            }
            else {


                foreach (DataRow rowP in ds.Tables["p"].Rows)
                {
                    Session["verifadmin"] = false;
                    cdS = (int)rowP["CodeServeur"];
                    String n = ((string)rowP["Privilege"]).Trim();


                    SqlDataAdapter logAdap = new SqlDataAdapter(" SELECT * FROM [MonitoringDB].[dbo].[log] where reussi = 0 and codeserveur=" + cdS + "and DATEDIFF(YEAR, Temps, GETDATE()) < 3  order by Codelog desc;", connection);
                    log = new DataSet();

                    logAdap.Fill(log, "lloogg");
                    // var a = log.Tables[0].Rows.Count;


                    HtmlGenericControl li = new HtmlGenericControl("li");
                    sub.Controls.Add(li);
                    HtmlGenericControl anchor = new HtmlGenericControl("a");
                    anchor.Attributes.Add("href", "indexM.aspx?id=" + cdS);
                    anchor.InnerText = "serveur " + cdS + "--" + n;
                    li.Controls.Add(anchor);

                    HtmlGenericControl li1 = new HtmlGenericControl("li");
                    subMonitor.Controls.Add(li1);
                    HtmlGenericControl anchor1 = new HtmlGenericControl("a");
                    anchor1.Attributes.Add("href", "ConsulterMonitoringParServeur.aspx?id= " + cdS);
                    anchor1.InnerText = "consulter Monitoring de serveur : " + cdS + "--" + n;
                    li1.Controls.Add(anchor1);


                    aDashboardServer.Visible = true;
                    aService.Visible = true;

                    HtmlGenericControl li2 = new HtmlGenericControl("li");
                    subService.Controls.Add(li2);
                    HtmlGenericControl anchor2 = new HtmlGenericControl("a");
                    anchor2.Attributes.Add("href", "ConsulterServiceParServeur.aspx?id= " + cdS);
                    anchor2.InnerText = "consulter les service  de serveur : " + cdS + "--" + n;
                    li2.Controls.Add(anchor2);


                    if (n == "edit")
                    {



                        aAjoutService.Visible = true;
                        aMonitoring.Visible = true;



                    }


                }
            }
        }
        protected void btn(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("seConnecter.aspx");
        }
        protected void btnAjout_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Test"].ToString());
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Serveur ( AdresseServeur,Login,pwd,nom) VALUES (@Nom,@Adresse,@login,@pwd)";

                cmd.Parameters.AddWithValue("@Nom", txtNom.Value);
                cmd.Parameters.AddWithValue("@Adresse", txAdress.Text);
                cmd.Parameters.AddWithValue("@login", txtlogin.Value);
                cmd.Parameters.AddWithValue("@pwd", txtpwd.Value);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert message", @"alert('ajout reussi')", true);


                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ee)
            {

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert message", @"alert('echec" + ee.Message + "')", true);
            }

        }

        protected void BtnAjoutService_Click(object sender, EventArgs e)
        {
            try
            {
                string s = "00:00:05.4575699";
                string type = DdlType.SelectedItem.Text;
                if (type == "url")
                {
                    s = "00:00:05.4575699";

                }
                else if (type == "sql")
                {
                    s = "00:00:05.4575699";

                }
                else if (type == "ping")
                {
                    s = "00:00:05.4575699";
                }
                else if (type == "port")
                {
                    s = "00:00:05.4575699";
                }

                else if (type == "mail")
                {
                    s = "00:00:05.4575699";

                }
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Test"].ToString());
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Service(Type,Operation1,operation2,ResultatAttendu,seuil) VALUES (@type,@operation1,@operation2,@ResultatAttendu,@seuil)";
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@operation1", txtOp1.Value);
                if (TxtOp2.Text == "") TxtOp2.Text = null;
                cmd.Parameters.AddWithValue("@operation2", TxtOp2.Text);
                cmd.Parameters.AddWithValue("@ResultatAttendu", txtres.Value);
                cmd.Parameters.AddWithValue("@seuil", s);


                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert message", @"alert('Ajout reussi')", true);

                conn.Close();
            }
            catch (Exception ee)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert message", @"alert('Echec Ajout" + ee.Message + "')", true);
            }

        }


        protected void logout_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Session["userid"]);
            s.wcfMochard(id, "log out");
            Session.RemoveAll();
            Response.Redirect("seconnecter.aspx");


        }

        protected void BtnInter_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Test"].ToString());
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                
              
                 string idServer = DropDownServeur.SelectedItem.Value;
              

                cmd.CommandText = "INSERT INTO intervenir(CodeEmploye,CodeServeur,problem,solution,Temps) VALUES (@cdEmpl,@cdServeur,@problem,@inter,@Temps)";

                 cmd.Parameters.AddWithValue("@cdEmpl", id);
                cmd.Parameters.AddWithValue("@cdServeur", idServer);

                cmd.Parameters.AddWithValue("@problem", txtProblem.Text);
                cmd.Parameters.AddWithValue("@inter", TxtInter.Text);
                cmd.Parameters.AddWithValue("@temps", DateTime.Now);

                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert message", @"alert('Ajout reussi')", true);


            }
            catch (Exception ee)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert message", @"alert('Echec Ajout" + ee.Message + "')", true);
            }

        }
    }
}