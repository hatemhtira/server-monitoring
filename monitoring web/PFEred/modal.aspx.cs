using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PFEred
{
    public partial class modal : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            //inputEmail4.Value = "7897879";
            //inputName4.Value = "9dddddddd";
            //var products = new List<log>();
            //products.Add(new log() { ProductID = 1, Name = "Bike", Price = 150.00 });
            //products.Add(new log() { ProductID = 2, Name = "Helmet", Price = 19.99 });
            //products.Add(new log() { ProductID = 3, Name = "Tire", Price = 10.00 });

            //ProductList.DataSource = products;
            //ProductList.DataBind();
        }

        //protected void Unnamed_Click(object sender, EventArgs e)
        //{
        //    inputEmail4.Value = "99999";
        //    inputName4.Value = "90990";

        //}
        public int k;
        protected void Unnamed_Click1(object sender, EventArgs e)
        {
             k = 5;
            k++;
            inputEmail4.Value = k.ToString();
        }
    }
}