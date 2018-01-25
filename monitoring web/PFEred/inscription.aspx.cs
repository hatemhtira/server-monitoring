using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace registre
{
    public partial class inscription : System.Web.UI.Page
    {

        PFEred.services.Service1 s=new PFEred.services.Service1();
        protected void Page_Load(object sender, EventArgs e)
        {
            iimmgg.Visible = false;
            lblpsk.Visible = false;
            LblMail .Visible= false;

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            aDesparaitre.Visible = false;
            iimmgg.Visible = true;
            string psk=null;
            try
            {     
               string  img= s.inscription(txNom.Value,TxtPrenom.Value,Txtbirth.Value,TxtAdresse.Value,txEmail.Value,Convert.ToInt32(txTel.Value),txLogin.Value,txPwd.Value);
           //// foreach (DataRow p in ds.Tables[0].Rows) { 
           //   string  img =(string) p["SourceImage"];
                 iimmgg.Attributes["src"]= "data: image / gif; base64," + img;
                // }

              

            } catch(Exception a)
            {
                lblpsk.Visible = true;
                lblpsk.Text =a.Message;

            }
            psk = s.rechPSK((txLogin.Value), txPwd.Value);
            lblpsk.Visible = true;
            lblpsk.Text = psk;
            LblMail.Visible = true;



            try {
                s.wcfMailSent(txNom.Value, TxtPrenom.Value, txEmail.Value, psk);
                LblMail.Text = "Your PSK has been sent to your Mail";
            }
            catch(Exception ee)
            {
                LblMail.Text = "a problem has occured when sending your PSK";
            }


        }
    }
}