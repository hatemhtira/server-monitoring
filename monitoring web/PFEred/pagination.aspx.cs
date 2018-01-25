using System;
using System.Collections;
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
    public partial class pagination : System.Web.UI.Page
    {public int total;
        public int PageNumber
        { 
            get
            {
                if (ViewState["PageNumber"] != null)
                    return Convert.ToInt32(ViewState["PageNumber"]);
                else
                    return 0;
            }
            set
            {
                ViewState["PageNumber"] = value;
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            rptPages.ItemCommand +=
               new RepeaterCommandEventHandler(rptPages_ItemCommand);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                LoadData();

        }
      public  PagedDataSource pgitems;
        private int LoadData()
        {
            SqlConnection cn= new SqlConnection(ConfigurationManager.ConnectionStrings["test"].ToString());
   cn.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM log", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            pgitems = new PagedDataSource();
            DataView dv = new DataView(dt);
            pgitems.DataSource = dv;
            pgitems.AllowPaging = true;
            pgitems.PageSize = 5;
            pgitems.CurrentPageIndex = PageNumber;
            total = pgitems.PageCount;
           
            if (pgitems.PageCount > 1)
            {
                rptPages.Visible = true;
                ArrayList pages = new ArrayList();
                for (int i = 0; i < pgitems.PageCount; i++)
                    pages.Add((i + 1).ToString());
                rptPages.DataSource = pages;
                rptPages.DataBind();
            }
            else
                rptPages.Visible = false;

            rptItems.DataSource = pgitems;
            rptItems.DataBind();

            return total;
        }
        void rptPages_ItemCommand(object source,
                                  RepeaterCommandEventArgs e)
        {
            PageNumber = Convert.ToInt32(e.CommandArgument) - 1;
            LoadData();
        }

        protected void prvs_Click(object sender, EventArgs e)
        {
            lbl.Text = PageNumber.ToString();
            if (PageNumber > 0) {
                PageNumber = PageNumber - 1;
                LoadData();
            }
        }

        protected void btnsuiv_Click(object sender, EventArgs e)
        {
            
            if (LoadData() > PageNumber+1)
            {
                PageNumber = PageNumber + 1;
                LoadData();
            }
        }
    }
}