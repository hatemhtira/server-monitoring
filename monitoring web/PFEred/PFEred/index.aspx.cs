using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PFEred
{
    public partial class index : System.Web.UI.Page
    {
     
        protected void Page_Load(object sender, EventArgs e)
        {

            hfServerValue.Value = "20";
            for (int i = 1; i < 3; i++)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                sub.Controls.Add(li);
                HtmlGenericControl anchor = new HtmlGenericControl("a");
                anchor.Attributes.Add("href", "aboutme.aspx");
                anchor.InnerText = "Tab Text";
                li.Controls.Add(anchor);
            }
        }
    }
}