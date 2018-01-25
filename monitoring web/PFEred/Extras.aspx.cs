using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PFEred
{
    public partial class Extras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            lblIp.Text = localIP;

        }

        protected void btnTest_Click(object sender, EventArgs e)
        { int a = Convert.ToInt32(cmb.Value);
          
          switch (a) { 
                case 1: ping.Visible = true;port.Visible = false;my.Visible = false; break;
                case 2: ping.Visible = false; port.Visible = true; my.Visible = false; break;
                case 3: ping.Visible = false; port.Visible = false;my.Visible = true; break;


            }
        }

        protected void btnPing_Click(object sender, EventArgs e)
        {
            Ping requestServer = new Ping();
            try
            { PingReply serverResponse = requestServer.Send(txtping.Value);

                ping1.Text = "Status of Host:" + txtping.Value;

                if (serverResponse.Status == IPStatus.Success)
                {
                    ping2.Text = "IP Address: " + serverResponse.Address.ToString();
                    ping3.Text = "RoundTrip time: " + serverResponse.RoundtripTime;
                    ping4.Text = "Time to live: " + serverResponse.Options.Ttl;
                    ping5.Text = "Buffer size: " + serverResponse.Buffer.Length;
                }
                else
                    ping2.Text = serverResponse.Status.ToString();
            }catch(Exception ee)
            {
                ping2.Text = "host inconnu ";

            }
            
            }

        protected void btnPort_Click(object sender, EventArgs e)
        {
            services.Service1 s = new services.Service1();
            DataSet ds = s.wcfport(txtIpPort.Value, Convert.ToInt32(txtPort.Value));
            string a="";
            foreach (DataRow p in ds.Tables[0].Rows)
            {
                 a = Convert.ToString( p["resultat"]);

            }
            cp.Visible = false;
            resultat.Visible = true;
            aport.Visible = true;
            aport.Text = a.ToString();
           
        }

    }

}