using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using QRCoder;
using WcfService;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net.Security;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace WcfService3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        SqlConnection connection = new SqlConnection("Data Source=h2bm;User Id=test;Password = 12345; Initial Catalog=Monitoringdb");

        public string GetData(string value)
        {

            return string.Format("You entered: {0}", value);
        }
      
        public string ConsulterLog()
        {
            connection.Open();

            DataSet ds = new DataSet();
            SqlDataAdapter Adap = new SqlDataAdapter("SELECT top 20 CodeLog,CodeServeur,CodeService,Messagelog,Temps fROM log GROUP BY CodeLog,CodeServeur,CodeService,Messagelog,Temps ORDER BY CodeLog DESC ", connection);

            Adap.Fill(ds, "log");

            DataSet table = ds;
            string jsonString = string.Empty;
            connection.Close();
            jsonString = JsonConvert.SerializeObject(table);
            return jsonString;
        }

        public string ConsulterServeur()
        {
            connection.Open();

            DataSet ds = new DataSet();
            SqlDataAdapter Adap = new SqlDataAdapter("SELECT CodeServeur,AdresseServeur,Login,pwd,nom fROM serveur ", connection);

            Adap.Fill(ds, "serveur");

            DataSet table = ds;
            string jsonString = string.Empty;
            connection.Close();
            jsonString = JsonConvert.SerializeObject(table);
            return jsonString;
        }
        public string ConsulterEmploye()
        {
            connection.Open();

            DataSet ds = new DataSet();
            SqlDataAdapter Adap = new SqlDataAdapter("SELECT userid,username,lastname,adresse,email fROM employe ", connection);

            Adap.Fill(ds, "employe");

            DataSet table = ds;
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(table);
            connection.Close();
            return jsonString;
        }
        public string ConsulterService()
        {
            connection.Open();

            DataSet ds = new DataSet();
            SqlDataAdapter Adap = new SqlDataAdapter("SELECT CodeService,type,operation1,operation2,resultatAttendu fROM service ", connection);

            Adap.Fill(ds, "service");

            DataSet table = ds;
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(table);
            connection.Close();
            return jsonString;
        }
        public string StatServeur(string id)
        {

            DateTime temps = DateTime.Now;
            int reussi = 5;
            double t = 0;
            DataSet dsf = new DataSet();
            DataSet ds = new DataSet();
            DataTable lastScans = new DataTable();
            lastScans.Columns.Add("Temps", typeof(double));
            lastScans.Columns.Add("reussi", typeof(int));
            
            connection.Open();
            SqlDataAdapter Adap4 = new SqlDataAdapter("SELECT TOP 10 Temps,reussi FROM log where codeServeur=" + id + " GROUP BY Temps,reussi ORDER BY temps DESC", connection);
            Adap4.Fill(ds, "lastscans");

            foreach (DataRow p3 in ds.Tables["lastscans"].Rows)
            {
                temps = (DateTime)p3["Temps"];
                t = (DateTime.Now - temps).TotalMinutes;
                reussi = (int)p3["reussi"];
                lastScans.Rows.Add(t, reussi);
            }
            lastScans.Rows.Add(t, reussi);
            dsf.Tables.Add(lastScans);

            DateTime lastDown = DateTime.Now;
            DateTime lastUp = DateTime.Now;
            DateTime lastScan = DateTime.Now;

            DataTable dt = new DataTable();
            dt.Columns.Add("lastdown", typeof(DateTime));
            dt.Columns.Add("lastup", typeof(DateTime));
            dt.Columns.Add("lastscan", typeof(DateTime));
            SqlDataAdapter Adap = new SqlDataAdapter("SELECT top 1 Temps FROM log where codeServeur=" + id + "and reussi=0 GROUP BY Temps ORDER BY temps DESC", connection);
            Adap.Fill(ds, "lastdown");
            foreach (DataRow p in ds.Tables["lastdown"].Rows)
            {
                lastDown = (DateTime)p["Temps"];
            }
            SqlDataAdapter Adap2 = new SqlDataAdapter("SELECT top 1 Temps FROM log where codeServeur=" + id + "and reussi=2 GROUP BY Temps ORDER BY temps DESC", connection);

            Adap2.Fill(ds, "lastup");

            foreach (DataRow p1 in ds.Tables["lastup"].Rows)
            {
                lastUp = (DateTime)p1["Temps"];
            }
            SqlDataAdapter Adap3 = new SqlDataAdapter("SELECT top 1 Temps,reussi FROM log where codeServeur=" + id + " GROUP BY Temps,reussi ORDER BY temps DESC", connection);
            Adap3.Fill(ds, "lastscan");

            foreach (DataRow p2 in ds.Tables["lastscan"].Rows)
            {
                lastScan = (DateTime)p2["Temps"];
            }

            dt.Rows.Add(lastDown, lastUp, lastScan);
            dsf.Tables.Add(dt);

            DataSet table = ds;
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(table);
            connection.Close();
            return jsonString;
        }
        public string StatAdmin()
        {

            DateTime temps = DateTime.Now;
            int reussi = 5;
            double t = 0;
            DataSet dsf = new DataSet();
            DataSet ds = new DataSet();
            DataTable lastScans = new DataTable();
            lastScans.Columns.Add("Temps", typeof(double));
            lastScans.Columns.Add("reussi", typeof(int));
            connection.Open();
            SqlDataAdapter Adap4 = new SqlDataAdapter("SELECT TOP 10 Temps,reussi FROM log where codeServeur=0 and codeService=0 GROUP BY Temps,reussi ORDER BY temps  DESC", connection);
            Adap4.Fill(ds, "lastscans");

            foreach (DataRow p3 in ds.Tables["lastscans"].Rows)
            {
                temps = (DateTime)p3["Temps"];
                t = (DateTime.Now - temps).TotalMinutes;
                reussi = (int)p3["reussi"];
                lastScans.Rows.Add(t, reussi);
            }
            lastScans.Rows.Add(t, reussi);
            dsf.Tables.Add(lastScans);

            DateTime lastDown = DateTime.Now;
            DateTime lastUp = DateTime.Now;
            DateTime lastScan = DateTime.Now;

            DataTable dt = new DataTable();
            dt.Columns.Add("lastdown", typeof(DateTime));
            dt.Columns.Add("lastup", typeof(DateTime));
            dt.Columns.Add("lastscan", typeof(DateTime));
            SqlDataAdapter Adap = new SqlDataAdapter("SELECT TOP 1 Temps FROM log where codeServeur=0 and codeService=0 and reussi=0 GROUP BY Temps order by temps desc ", connection);
            Adap.Fill(ds, "lastdown");
            foreach (DataRow p in ds.Tables["lastdown"].Rows)
            {
                lastDown = (DateTime)p["Temps"];
            }
            SqlDataAdapter Adap2 = new SqlDataAdapter("SELECT top 1 Temps FROM log where codeServeur=0 and codeService=0 and reussi=2  GROUP BY Temps order by temps desc", connection);

            Adap2.Fill(ds, "lastup");

            foreach (DataRow p1 in ds.Tables["lastup"].Rows)
            {
                lastUp = (DateTime)p1["Temps"];
            }
            SqlDataAdapter Adap3 = new SqlDataAdapter("SELECT top 1 Temps,reussi FROM log where codeServeur=0 and codeService=0 GROUP BY Temps,reussi order by temps desc", connection);
            Adap3.Fill(ds, "lastscan");

            foreach (DataRow p2 in ds.Tables["lastscan"].Rows)
            {
                lastScan = (DateTime)p2["Temps"];
            }

            dt.Rows.Add(lastDown, lastUp, lastScan);
            dsf.Tables.Add(dt);

            DataSet table = ds;
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(table);
            connection.Close();
            return jsonString;
        }
        public string rechPSK(string login, string pwd)
        {
            string psk;
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "select psk from Employe where login = '" + login + "' and pwd ='" + pwd + "'";
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            return psk = reader.GetString(0);
        }
        public bool ValidOTP(string login, string pwd, String token)
        {
            string psk = rechPSK(login, pwd);
            return OtpHelper.HasValidTotp(token, psk);
        }
        //-------------------------------------------------------------------------
        public string ConsulterLogServeur(string id)
        {
            
            connection.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter Adap = new SqlDataAdapter("SELECT top 20 CodeServeur,CodeService,Messagelog,Temps fROM log  where codeServeur=" + id + "", connection);

            Adap.Fill(ds, "log");

            DataSet table = ds;
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(table);
            connection.Close();
            return jsonString;
        }
        //-------------------------------------------------------------------------
        public string ConsulterAlert()
        {

            connection.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter Adap = new SqlDataAdapter("SELECT CodeServeur,CodeService,Messagelog,Temps fROM log  where  Reussi=0 OR Reussi=1", connection);

            Adap.Fill(ds, "log");

            DataSet table = ds;
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(table);
            connection.Close();
            return jsonString;
        }
        //---------------------------------------------------------------------------------------
        public string ConsulterServiceID(string id)
        {
            connection.Open();

            DataSet ds = new DataSet();
            SqlDataAdapter Adap = new SqlDataAdapter("SELECT CodeService,type,operation1,operation2,resultatAttendu fROM service where CodeService in(SELECt CodeService from monitoring where CodeServeur= "+id+")", connection);

            Adap.Fill(ds, "service");

            DataSet table = ds;
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(table);
            connection.Close();
            return jsonString;
        }
        //----------------------------------------------------------------------------------------------
        public string ConsulterServeurID(string id)
        {
            connection.Open();

            DataSet ds = new DataSet();
            SqlDataAdapter Adap = new SqlDataAdapter("SELECT CodeServeur fROM droit where userId= " + id, connection);

            Adap.Fill(ds, "droit");

            DataSet table = ds;
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(table);
            connection.Close();
            return jsonString;
        }
        //--------------------------------------------------------------------------------------------
      //--------------------------------------------------------------------------------------
        public bool VerifAdmin(string id)
        {
            bool verif = false;
            SqlDataAdapter idServeur = new SqlDataAdapter("SELECT * FROM serveur", connection);

            DataSet ids = new DataSet();
            idServeur.Fill(ids, "ids");
            int nbreS = ids.Tables[0].Rows.Count;
            SqlDataAdapter privilege = new SqlDataAdapter("SELECT * FROM Droit where userId=" + id, connection);
            DataSet ds = new DataSet();
            privilege.Fill(ds, "p");
            int nbreSD = ds.Tables[0].Rows.Count;
            if (nbreS == nbreSD)
            {
                verif = true;
            }
            return verif;
        }
        //---------------------------------------------------------------------------------------------------
        public DataSet wcfport(string ip, int port)
        {
            DateTime TimeStart = DateTime.Now;
            Boolean verif = true;
            DataTable dt = new DataTable();
            dt.Columns.Add("resultat", typeof(Boolean));
            dt.Columns.Add("Temps de réponse", typeof(TimeSpan));
            DataSet ds = new DataSet();
            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(ip, port);
                verif = true;
                TimeSpan TimeDif = DateTime.Now - TimeStart;
                dt.Rows.Add(verif, TimeDif);
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception)
            {
                verif = false;
                TimeSpan TimeDif1 = DateTime.Now - TimeStart;
                dt.Rows.Add(verif, TimeDif1);
                ds.Tables.Add(dt);
                return ds;

            }
        }
        //-----------------------------------------------------------------------------------------------------------------
        public DataSet wcfMailSmtp(string operation, string res)
        {
            DateTime TimeStart = DateTime.Now;
            Boolean verif = true;
            DataTable dt = new DataTable();
            dt.Columns.Add("resultat", typeof(Boolean));
            dt.Columns.Add("Temps de réponse", typeof(TimeSpan));
            DataSet ds = new DataSet();
            var client = new TcpClient();
            //   operation = "smtp.gmail.com";
            //  res = "220 smtp.gmail.com ";
            var port = 465;
            try
            {
                client.Connect(operation, port);
                var stream = client.GetStream();
                var sslStream = new SslStream(stream);
                sslStream.AuthenticateAsClient(operation);
                var writer = new StreamWriter(sslStream);
                var reader = new StreamReader(sslStream);
                if (reader.ReadLine().IndexOf(res) == 0)
                {
                    verif = true;
                    TimeSpan TimeDif = DateTime.Now - TimeStart;
                    dt.Rows.Add(verif, TimeDif);
                    ds.Tables.Add(dt);
                    return ds;
                }
                else
                {
                    verif = false;
                    TimeSpan TimeDif = DateTime.Now - TimeStart;
                    dt.Rows.Add(verif, TimeDif);
                    ds.Tables.Add(dt);
                    return ds;
                }
            }
            catch (Exception e)
            {
                verif = false;
                TimeSpan TimeDif = DateTime.Now - TimeStart;
                dt.Rows.Add(verif, TimeDif);
                ds.Tables.Add(dt);
                return ds;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------
        public DataSet wcfSql(int CodeServeur, string operation, string res)
        {
            Boolean verif = true;
            operation = "SELECT @@VERSION ";
            res = "« Microsoft SQL Server 2012 - 11.0.2100.60 (Intel X86)";
            DataTable dt = new DataTable();
            dt.Columns.Add("resultat", typeof(Boolean));
            dt.Columns.Add("Temps de réponse", typeof(TimeSpan));
            DataSet ds = new DataSet();
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            try
            {
                DateTime TimeStart = DateTime.Now;
                cmd.CommandText = operation;
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                string ex = reader.GetString(0);
                if (ex == res)
                {
                    verif = true;
                    TimeSpan TimeDif = DateTime.Now - TimeStart;
                    dt.Rows.Add(verif, TimeDif);
                    ds.Tables.Add(dt);
                    return ds;
                }
                else
                    verif = false;
                TimeSpan TimeDif1 = DateTime.Now - TimeStart;
                dt.Rows.Add(verif, TimeDif1);
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception ex)
            {
                DateTime TimeStart = DateTime.Now;
                TimeSpan TimeDif1 = DateTime.Now - TimeStart;
                dt.Rows.Add(verif, TimeDif1);
                ds.Tables.Add(dt);
                return ds;
            }
        }

        public DataSet wcfMailSent(int CodeServeur, string destinataire)
        {
            Boolean verif = true;
            DataTable dt = new DataTable();
            dt.Columns.Add("resultat", typeof(Boolean));
            dt.Columns.Add("Temps de réponse", typeof(TimeSpan));
            DataSet ds = new DataSet();
            try
            {
                DateTime TimeStart = DateTime.Now;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("moncer.aymen@gmail.com");
                mail.To.Add(destinataire);
                mail.Subject = "Test Mail";
                mail.Body = "This is for testing SMTP mail from GMAIL";
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("aymen", "aymen");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                TimeSpan TimeDif = DateTime.Now - TimeStart;
                dt.Rows.Add(verif, TimeDif);
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception ex)
            {
                DateTime TimeStart = DateTime.Now;
                TimeSpan TimeDif1 = DateTime.Now - TimeStart;
                dt.Rows.Add(verif, TimeDif1);
                ds.Tables.Add(dt);
                return ds;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------
        public DataSet wcfUrl(string url, string res)
        {
            Boolean verif = true;
            string t = "http://";
            DateTime TimeStart = DateTime.Now;
            DataTable dt = new DataTable();
            dt.Columns.Add("resultat", typeof(Boolean));
            dt.Columns.Add("Temps de réponse", typeof(TimeSpan));
            DataSet ds = new DataSet();
            try
            {
                HttpWebRequest r = (HttpWebRequest)WebRequest.Create(t + url);
                HttpWebResponse response = (HttpWebResponse)r.GetResponse();
                //   request.Timeout = 3000;
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string content = reader.ReadToEnd();
                if (content.IndexOf(res, StringComparison.CurrentCulture) > 0)
                {
                    verif = true;
                    TimeSpan TimeDif = DateTime.Now - TimeStart;
                    dt.Rows.Add(verif, TimeDif);
                    ds.Tables.Add(dt);
                }
                return ds;
            }
            catch (Exception ex)
            {
                verif = false;
                TimeSpan TimeDif1 = DateTime.Now - TimeStart;
                dt.Rows.Add(verif, TimeDif1);
                ds.Tables.Add(dt);
                return ds;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------
        public DataSet ping(String url)
        {
            Boolean verif = true;
            DataTable dt = new DataTable();
            dt.Columns.Add("resultat", typeof(Boolean));
            dt.Columns.Add("Temps de réponse", typeof(TimeSpan));
            DataSet ds = new DataSet();
            try
            {
                DateTime TimeStart = DateTime.Now;
                Ping requestServer = new Ping();
                PingReply serverResponse = requestServer.Send(url);
                TimeSpan TimeDif = DateTime.Now - TimeStart;

                if (serverResponse.Status == IPStatus.Success)
                {
                    dt.Rows.Add(verif, TimeDif);
                    ds.Tables.Add(dt);
                }
                return ds;
            }
            catch (Exception ex)
            {
                verif = false;
                DateTime TimeStart = DateTime.Now;
                TimeSpan TimeDif1 = DateTime.Now - TimeStart;
                dt.Rows.Add(verif, TimeDif1);
                ds.Tables.Add(dt);
                return ds;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------

        public DataSet wcfService(string type, int CodeServeur, string operation, string res, string destinataire, int port)
        {
            DataSet sv;
            if (type == "sql")
            {
                sv = wcfSql(CodeServeur, operation, res);
                return sv;
            }
            else
                if (type == "mail")
            {
                return wcfMailSent(CodeServeur, destinataire);
                //  return wcfMailSmtp(operation, res);
            }
            else if (type == "url")
            {
                sv = wcfUrl(operation, res);
                return sv;
            }
            else if (type == "ping")
            {
                sv = ping(operation);
                return sv;
            }
            else if (type == "port")
            {
                sv = wcfport(operation, port);
                return sv;
            }
            else
                return null;
        }
        //----------------------------------------------------------------
        public Boolean wcflog(int codeserveur, int codeservice, string msg, int reussi, string tdr)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "INSERT INTO log (CodeService,Messagelog,TempsDeReponse,Temps,reussi,CodeServeur) VALUES (@codeservice,@message,@tempsreponse,@temps,@reussi,@codeserveur)";
            cmd.Parameters.AddWithValue("@codeservice", codeservice);
            cmd.Parameters.AddWithValue("@message", msg);
            cmd.Parameters.AddWithValue("@tempsreponse", tdr);
            cmd.Parameters.AddWithValue("@codeserveur", codeserveur);
            cmd.Parameters.AddWithValue("@reussi", reussi);
            cmd.Parameters.AddWithValue("@temps", DateTime.Now);

            cmd.ExecuteNonQuery();
            connection.Close();
            return true;
        }
        
        //----------------------------------------------------------------
        public string noyau()
        {
            try { 
            SqlConnection connection = new SqlConnection("Data Source=h2bm;User Id=test;Password = 12345; Initial Catalog=Monitoringdb");
                connection.Open();
            Boolean verif = true;
            int id = 5;
            DataSet monitor;
            SqlDataAdapter monitorAdap = new SqlDataAdapter("SELECT * FROM monitoring  where codeServeur=" + id + " order by priorite", connection);
            monitor = new DataSet();
            monitorAdap.Fill(monitor, "dsmonitor");
            foreach (DataRow pRow in monitor.Tables["dsmonitor"].Rows)
            {
                int id1 = (int)pRow["Codeservice"];
                DataSet service;
                SqlDataAdapter serviceAdap = new SqlDataAdapter("SELECT * FROM service  where codeService=" + id1 + "", connection);
                service = new DataSet();
                serviceAdap.Fill(service, "dsService");
                foreach (DataRow pRow2 in service.Tables["dsService"].Rows)
                {
                    int code2 = (int)pRow2["Codeservice"];
                    string type = ((string)pRow2["type"]).Trim();
                    string operation1 = ((string)pRow2["operation1"]).Trim();
                    string res = ((string)pRow2["resultatAttendu"]).Trim();
                    DataSet sv;

                    if (type == "ping")
                    {
                        sv = wcfService(type.Trim(), 0, operation1.Trim(), null, null, 0);
                        foreach (DataRow i in sv.Tables[0].Rows)
                        {
                            TimeSpan f = (TimeSpan)(i["Temps de réponse"]);
                            if ((Boolean)i["resultat"])

                                wcflog(id, code2, "ping lalal true ", 2, f.ToString());
                            else {
                                verif = false;
                                wcflog(id, code2, "ping lalal false ", 0, f.ToString());
                            }
                            //  float tdr = (float)i["resultat"];

                            // verif = wcflog(31, 1, "jjjj", 9);
                        }
                    }
                        else if (type == "url")
                        {
                            sv = wcfService(type.Trim(), 0, operation1.Trim(), res.Trim(), null, 0);
                            foreach (DataRow i in sv.Tables[0].Rows)
                            {
                                TimeSpan f = (TimeSpan)(i["Temps de réponse"]);
                                if ((Boolean)i["resultat"])

                                    wcflog(id, code2, "ping lalal true ", 2, f.ToString());
                                else
                                {
                                    verif = false;
                                    wcflog(id, code2, "ping lalal false ", 0, f.ToString());
                                }
                                //  float tdr = (float)i["resultat"];

                                // verif = wcflog(31, 1, "jjjj", 9);

                            }
                        }
                        else if (type == "port")
                        {
                            string operation2 = (string)pRow2["operation2"];
                            sv = wcfService(type.Trim(), 0, operation1.Trim(), null, null, Convert.ToInt32(operation2.Trim()));
                            foreach (DataRow i in sv.Tables[0].Rows)
                            {
                                TimeSpan f = (TimeSpan)(i["Temps de réponse"]);
                                if ((Boolean)i["resultat"])

                                    wcflog(id, code2, "ping lalal true ", 2, f.ToString());
                                else
                                {
                                    verif = false;
                                    wcflog(id, code2, "ping lalal false ", 0, f.ToString());
                                } //  float tdr = (float)i["resultat"];

                                // verif = wcflog(31, 1, "jjjj", 9);
                            }
                        }
                        else sv = null;
                }
            }
            if(verif)
           wcflog(0,0,"fonctionne parfaitement", 2, "0");
            else
                wcflog(0, 0, "probleme au niveau de fonctionnement", 0, "0");


                return "vrai";

        }
            catch (Exception e)
            {
                return e.Message;
            }
               }
    }

}
            

