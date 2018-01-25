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
    public partial class AssignerPrivilege : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=h2bm;User Id=test;Password = 12345; Initial Catalog=Monitoringdb");
         

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
            SqlCommand cmd = new SqlCommand("SELECT [idDroit],[Droit].[userId],[Droit].[codeServeur],[Privilege],CONCAT([Droit].CodeServeur, ' ', [Serveur].nom) as conServr, CONCAT([Droit].[userId], '', [Employe].nom, '',[Employe].Prenom) as conemploy FROM [Droit],[Employe],[Serveur] where [Droit].CodeServeur=[Serveur].CodeServeur and [Employe].userid=[Droit].userId union SELECT[idDroit],[Droit].[userId],[Droit].[codeServeur],[Privilege], null, null FROM [Droit], [Employe], [Serveur] where[Droit].CodeServeur is null", con);
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
            TextBox txtPriorite = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtprivilege");

            int b = Convert.ToInt32((gvDetails.Rows[e.RowIndex].FindControl("DropDownServeur") as DropDownList).SelectedItem.Value);

            int c = Convert.ToInt32((gvDetails.Rows[e.RowIndex].FindControl("DropDownEmploye") as DropDownList).SelectedItem.Value);
            
            string a = Convert.ToString((gvDetails.Rows[e.RowIndex].FindControl("DropDownprivilege") as DropDownList).SelectedItem.Value);

            con.Open();
            SqlCommand cmd = new SqlCommand("update Droit set privilege='" + a + "',codeServeur=" + b + " ,userId=" + c + "  where idDroit = " + userid, con);
            cmd.ExecuteNonQuery();
            con.Close();
         
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
            SqlCommand cmd = new SqlCommand("delete from Droit where idDroit=" + id, con);
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result == 1)
            {
                BindEmployeeDetails();
                
                lblresult.Text = id + " details deleted successfully";
            }
        }

        protected void btnAjout_Click(object sender, EventArgs e)
        {
            con.Open();
           // cmd.CommandText = "INSERT INTO Droit (codeServeur,adresseServeur,login,pwd,nom) VALUES (@code,@Nom,@Adresse,@login,@pwd)";
            SqlCommand cmd = new SqlCommand("INSERT INTO Droit (Privilege) VALUES (@p)", con);

            cmd.Parameters.AddWithValue("@p", "edit");
            cmd.ExecuteNonQuery();
            con.Close();
            BindEmployeeDetails();
        }
    }
}