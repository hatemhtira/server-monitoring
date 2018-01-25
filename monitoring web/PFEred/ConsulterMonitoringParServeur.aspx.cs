using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PFEred
{
    public partial class ConsulterMonitoringParServeur : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=h2bm;User Id=test;Password = 12345; Initial Catalog=Monitoringdb");
        string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
       new ScriptResourceDefinition
       {
           Path = "~/scripts/jquery-1.7.2.min.js",
           DebugPath = "~/scripts/jquery-1.7.2.min.js",
           CdnPath = "~/scripts/jquery-1.7.2.min.js",
           CdnDebugPath = "~/scripts/jquery-1.7.2.min.js"
       });

            if (!IsPostBack)
            {
                BindEmployeeDetails();
            }
        }

        protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetails.PageIndex = e.NewPageIndex;
            BindEmployeeDetails();
        }
        protected void BindEmployeeDetails()
        {
            id = Request.QueryString["id"];
            con.Open();
            //  SqlCommand cmd = new SqlCommand("Select * from monitoring where codeServeur="+Convert.ToInt32(id), con);
            //  SqlCommand cmd = new SqlCommand("Select * from monitoring where codeServeur=" + 5, con);
            SqlCommand cmd = new SqlCommand(" SELECT [codeM],[monitoring].CodeServeur,[monitoring].codeservice,[priorite]  , CONCAT([monitoring].CodeServeur, ' ', nom) as conServr,CONCAT([monitoring].CodeService, ' ', type)  as conServc FROM [MonitoringDB].dbo.[monitoring], [MonitoringDB].dbo.[Serveur], [MonitoringDB].dbo.[Service] where[monitoring].CodeServeur="+ Convert.ToInt32(id) + " and ([monitoring].CodeServeur=[Serveur].CodeServeur  and [MonitoringDB].dbo.[monitoring].codeservice=[Service].CodeService) union select[codeM],[monitoring].CodeServeur,[monitoring].codeservice,[priorite], null, null  FROM[MonitoringDB].dbo.[monitoring], [MonitoringDB].dbo.[Serveur], [MonitoringDB].dbo.[Service] where[monitoring].CodeServeur is NULL ; ", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                int columncount = gvDetails.Rows[0].Cells.Count;
                gvDetails.Rows[0].Cells.Clear();
                gvDetails.Rows[0].Cells.Add(new TableCell());
                gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                gvDetails.Rows[0].Cells[0].Text = "No Records Found";
            }
        }
        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            BindEmployeeDetails();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int userid = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Value.ToString());
            TextBox txtPriorite = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtPriorite");
 
            int c = Convert.ToInt32((gvDetails.Rows[e.RowIndex].FindControl("DropDownService") as DropDownList).SelectedItem.Value);
   
            int a = Convert.ToInt32(txtPriorite.Text);

            con.Open();
            SqlCommand cmd = new SqlCommand("update monitoring set priorite=" + a + " ,codeService=" + c + " where codeM = " + userid, con);
            cmd.ExecuteNonQuery();
            con.Close();
            lblresult.ForeColor = Color.Green;
            //  lblresult.Text = username + " Details Updated successfully";
            gvDetails.EditIndex = -1;
            BindEmployeeDetails();
        }
        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            BindEmployeeDetails();
        }
        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Value.ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Monitoring where CodeM=" + id, con);
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result == 1)
            {
                BindEmployeeDetails();
                lblresult.ForeColor = Color.Red;
                lblresult.Text = id + " details deleted successfully";
            }
        }

    }
}