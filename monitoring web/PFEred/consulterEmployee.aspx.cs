using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;

namespace PFEred
{
    public partial class consulterEmployeP : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Test"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
    

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
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Employe", con);
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
            string username = gvDetails.DataKeys[e.RowIndex].Values["Nom"].ToString();
            string lastname = gvDetails.DataKeys[e.RowIndex].Values["Prenom"].ToString();

            TextBox txtadress = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtadress");
         //   TextBox txtlastname = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtln");
            TextBox txttel = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txttel");
            TextBox txtlogin = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtlogin");
            TextBox txtpwd = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtpwd");
            TextBox txtpsk = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtpsk");
           // TextBox txtaccess = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtaccess");
            int a= Convert.ToInt32((gvDetails.Rows[e.RowIndex].FindControl("DropDownaccess") as DropDownList).SelectedItem.Value);
            
            con.Open();
            SqlCommand cmd = new SqlCommand("update Employe set access="+ a+",adresse='" + txtadress.Text + "',tel="+txttel.Text+ ", login='" + txtlogin.Text + "',pwd= '" + txtpwd.Text + "',psk='" + txtpsk.Text + "' where UserId=" + userid, con);
            cmd.ExecuteNonQuery();
            con.Close();
            lblresult.ForeColor = Color.Green;
            lblresult.Text = username + " Details Updated successfully";
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
            int userid = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Values["UserId"].ToString());
            string username = gvDetails.DataKeys[e.RowIndex].Values["UserName"].ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Employe where UserId=" + userid, con);
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result == 1)
            {
                BindEmployeeDetails();
                lblresult.ForeColor = Color.Red;
                lblresult.Text = username + " details deleted successfully";
            }
        }
        

    }
}