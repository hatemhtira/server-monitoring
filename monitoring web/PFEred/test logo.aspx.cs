using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PFEred
{
    public partial class test_logo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbLogo.Text = "ff";
        }

        protected void txIp_TextChanged(object sender, EventArgs e)
        {
            lbLogo.Text = "jjjj" ;
            Image1.ImageUrl = "~/img/check.png";

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            
        }
    }
}