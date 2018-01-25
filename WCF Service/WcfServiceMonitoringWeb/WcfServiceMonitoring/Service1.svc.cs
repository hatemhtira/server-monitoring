using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using WcfService;

namespace WcfServiceMonitoring
{
    public class Service1 : IService1
    {
        SqlConnection connection = new SqlConnection("Data Source=h2bm;User Id=test;Password = 12345; Initial Catalog=Monitoringdb");

        public int reussi;
        public Boolean wcflog(int codeserveur, int codeservice, int idscan, string msg, bool test, TimeSpan tdr)
        {

            connection.Open();
            if (test == true)
            {
                DataSet service;
                SqlDataAdapter serviceAdap = new SqlDataAdapter("SELECT * FROM service  where codeService=" + codeservice + "", connection);
                service = new DataSet();
                serviceAdap.Fill(service, "dsService");
                //recuperer les donnés de service
                foreach (DataRow pRow2 in service.Tables["dsService"].Rows)
                {
                    if (tdr != TimeSpan.Zero)
                    {
                        TimeSpan seuil = TimeSpan.Parse((string)pRow2["seuil"]);
                        if (tdr < seuil)
                        {
                            reussi = 2;
                            msg += " fonctionne parfaitement";
                        }
                        else
                        {
                            reussi = 1;
                            msg += " trop lent";
                        }
                    }
                    else {
                        msg += " fonctionne parfaitement";
                        reussi = 2;
                    }
                }
            }
            else
            {
                msg += " ne fonctionne pas parfaitement";
                reussi = 0;

            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "INSERT INTO log (CodeService,idscan,Messagelog,TempsDeReponse,Temps,Reussi,CodeServeur) VALUES (@codeservice,@idscan,@message,@tempsreponse,@temps,@reussi,@codeserveur)";
            cmd.Parameters.AddWithValue("@codeservice", codeservice);
            cmd.Parameters.AddWithValue("@idscan", idscan);
            cmd.Parameters.AddWithValue("@message", msg);
            cmd.Parameters.AddWithValue("@tempsreponse", tdr.ToString());
            cmd.Parameters.AddWithValue("@codeserveur", codeserveur);
            cmd.Parameters.AddWithValue("@reussi", reussi);
            cmd.Parameters.AddWithValue("@temps", DateTime.Now);
            try
            {
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------
        public int idscanmax()
        {
            int idScanMax = 0;
            DataSet serveur;
            SqlDataAdapter serveurAdap = new SqlDataAdapter("SELECT  idscan FROM log  where idscan=(select max(idscan) from log)", connection);
            serveur = new DataSet();
            serveurAdap.Fill(serveur, "dslog");
            foreach (DataRow p in serveur.Tables["dslog"].Rows)


                idScanMax = (int)p["idscan"];

            return idScanMax;
        }
        public string noyau()
        {
            try
            {
                SqlConnection connection = new SqlConnection("Data Source=h2bm;User Id=test;Password = 12345; Initial Catalog=Monitoringdb");
                Boolean verif = true;
                connection.Open();
                int idscan = idscanmax() + 1;
                int id;
                DataSet serveur;
                SqlDataAdapter serveurAdap = new SqlDataAdapter("SELECT * FROM serveur  ", connection);
                serveur = new DataSet();
                serveurAdap.Fill(serveur, "dsserveur");
                //parcourir liste des serveurs
                foreach (DataRow p in serveur.Tables["dsserveur"].Rows)
                {
                    Boolean verifServeur = true;
                    id = (int)p["CodeServeur"];


                    DataSet monitor;
                    SqlDataAdapter monitorAdap = new SqlDataAdapter("SELECT * FROM monitoring  where codeServeur=" + id + " order by priorite", connection);
                    monitor = new DataSet();
                    monitorAdap.Fill(monitor, "dsmonitor");
                    //parcourir table monitoring pour determiner liste des services de ce serveur
                    foreach (DataRow pRow in monitor.Tables["dsmonitor"].Rows)
                    {
                        int id1 = (int)pRow["CodeService"];
                        DataSet service;
                        SqlDataAdapter serviceAdap = new SqlDataAdapter("SELECT * FROM service  where codeService=" + id1 + "", connection);
                        service = new DataSet();
                        serviceAdap.Fill(service, "dsService");
                        //recuperer les donnés de service
                        foreach (DataRow pRow2 in service.Tables["dsService"].Rows)
                        {
                            int code2 = (int)pRow2["CodeService"];
                            string type = ((string)pRow2["type"]).Trim();
                            string operation1 = ((string)pRow2["operation1"]).Trim();
                            string res = ((string)pRow2["resultatAttendu"]).Trim();
                            DataSet sv;
                            // detrminer type de service et appel au fonction adequate
                            if (type == "ping")
                            {
                                sv = wcfService(type.Trim(), 0, operation1.Trim(), null, null, 0);
                                foreach (DataRow i in sv.Tables[0].Rows)
                                {
                                    TimeSpan f = (TimeSpan)(i["Temps de reponse"]);
                                    if ((Boolean)i["resultat"])

                                        wcflog(id, code2, idscan, " service " + code2 + "ping", true, f);
                                    else {
                                        wcflog(id, code2, idscan, "service  " + code2 + " ping", false, f);
                                        verif = false;
                                        verifServeur = false;
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
                                    TimeSpan f = (TimeSpan)(i["Temps de reponse"]);
                                    if ((Boolean)i["resultat"])

                                        wcflog(id, code2, idscan, "service  " + code2 + " url", true, f);
                                    else {
                                        wcflog(id, code2, idscan, "service  " + code2 + " url", false, f);
                                        verif = false;
                                        verifServeur = false;
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
                                    TimeSpan f = (TimeSpan)(i["Temps de reponse"]);
                                    if ((Boolean)i["resultat"])

                                        wcflog(id, code2, idscan, "service  " + code2 + "port", true, f);
                                    else {
                                        wcflog(id, code2, idscan, "service  " + code2 + " port", false, f);
                                        verif = false;
                                        verifServeur = false;
                                    }
                                    //  float tdr = (float)i["resultat"];

                                    // verif = wcflog(31, 1, "jjjj", 9);
                                }
                            }
                            else if (type == "sql")
                            {
                                int codeserveur = (int)pRow2["CodeServeur"];
                                string operation = (string)pRow2["operation1"];
                                string resultat = (string)pRow2["resultat"];

                                sv = wcfService(type.Trim(), codeserveur, operation.Trim(), resultat.Trim(), null, 0);
                                foreach (DataRow i in sv.Tables[0].Rows)
                                {
                                    TimeSpan f = (TimeSpan)(i["Temps de reponse"]);

                                    if ((Boolean)i["resultat"])

                                        wcflog(id, code2, idscan, "service  " + code2 + "sql", true, f);
                                    else {
                                        wcflog(id, code2, idscan, "service " + code2 + " sql", false, f);
                                        verif = false;
                                        verifServeur = false;
                                    }
                                    //  float tdr = (float)i["resultat"];

                                    // verif = wcflog(31, 1, "jjjj", 9);
                                }
                            }
                            else if (type == "mail")
                            {
                                int codeservice = (int)pRow2["CodeService"];
                                string operation = (string)pRow2["operation1"];
                                string resultat = (string)pRow2["resultatAttendu"];

                                sv = wcfService(type.Trim(), codeservice, operation.Trim(), resultat.Trim(), null, 0);
                                foreach (DataRow i in sv.Tables[0].Rows)
                                {
                                    TimeSpan f = (TimeSpan)(i["Temps de reponse"]);

                                    if ((Boolean)i["resultat"])

                                        wcflog(id, code2, idscan, "service " + code2 + " mail", true, f);
                                    else {
                                        wcflog(id, code2, idscan, "service  " + code2 + " mail", false, f);
                                        verif = false;
                                        verifServeur = false;
                                    }
                                    //  float tdr = (float)i["resultat"];

                                    // verif = wcflog(31, 1, "jjjj", 9);
                                }
                            }
                            else sv = null;
                        }
                    }
                    if (verifServeur) wcflog(id, 0, idscan, "serveur " + id + " fonctionne parfaitement ", true, TimeSpan.Zero);
                    else
                        wcflog(id, 0, idscan, " problem dans le serveur " + id + " ", false, TimeSpan.Zero);


                }
                if (verif) wcflog(0, 0, idscan, "system fonctionne parfaitement ", true, TimeSpan.Zero);
                else
                    wcflog(0, 0, idscan, "problem dans le system ", false, TimeSpan.Zero);

                return "vrai";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------
        static System.Timers.Timer t;
        public void Timer(int duree)
        {

            t = new System.Timers.Timer();
            t.Start();
            Console.WriteLine(DateTime.Now.ToString("o"));
            noyau();
            t.AutoReset = false;
            t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
            t.Interval = 20000;
            Console.ReadLine();
        }
        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            t.Start();
            Console.WriteLine(DateTime.Now.ToString("o"));
            noyau();
        }
        //-----------------------------------------------------------------------------------------------------------------
        //public Boolean GetData(string url)
        //{
        //    //DataSet sv;
        //    //sv = ping(url);
        //    //Boolean verif = false; 
        //    //foreach (DataRow i in sv.Tables[0].Rows)
        //    //{
        //    //    verif = (Boolean)i["resultat"];
        //    //  //  float tdr = (float)i["resultat"];

        //    //       // verif = wcflog(31, 1, "jjjj", 9);

        //    //}
        //    //return verif;
        //    ////    //  wcflog(458,1,"true ping", 0.8f);
        //    ////    else
        //    ////        wcflog(458, 1, "true ping", (float)0.8);

        //    ////}
        //    return true;
        //}

        public DataSet ping(String url)
        {
            Boolean verif = true;
            DataTable dt = new DataTable();
            dt.Columns.Add("resultat", typeof(Boolean));
            dt.Columns.Add("Temps de reponse", typeof(TimeSpan));
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
        public DataSet wcfport(string ip, int port)
        {
            DateTime TimeStart = DateTime.Now;
            Boolean verif = true;
            DataTable dt = new DataTable();
            dt.Columns.Add("resultat", typeof(Boolean));
            dt.Columns.Add("Temps de reponse", typeof(TimeSpan));
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
            dt.Columns.Add("Temps de reponse", typeof(TimeSpan));
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
            res = "� Microsoft SQL Server 2012 - 11.0.2100.60 (Intel X86)";
            DataTable dt = new DataTable();
            dt.Columns.Add("resultat", typeof(Boolean));
            dt.Columns.Add("Temps de reponse", typeof(TimeSpan));
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

        //-------------------------------------------------------------------------------------------------
        public DataSet LastScans(int id)
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
            SqlDataAdapter Adap4 = new SqlDataAdapter("SELECT TOP 10 Temps,Reussi FROM log where codeServeur=" + id + "GROUP BY Temps,Reussi ORDER BY MAX(CodeLog) DESC", connection);
            Adap4.Fill(ds, "lastscans");

            foreach (DataRow p3 in ds.Tables["lastscans"].Rows)
            {
                temps = (DateTime)p3["Temps"];
                t = (DateTime.Now - temps).TotalMinutes;
                reussi = (int)p3["reussi"];
                lastScans.Rows.Add(t, reussi);
            }

            dsf.Tables.Add(lastScans);
            connection.Close();
            return dsf;
        }
        //------------------------------------------------------------------------------------------------
        public DataSet StatService(int id, int idservice)
        {
            DataTable dt = new DataTable();
            int idscan;
            DateTime lastDown = DateTime.Now;
            DateTime lastUp = DateTime.Now;
            DateTime lastScan = DateTime.Now;
            DataTable lastScans = new DataTable();
            DataSet dsf = new DataSet();
            DataSet ds = new DataSet();
            DateTime temps = DateTime.Now;
            int etat = 999;
            int reussi = 999;


            lastScans.Columns.Add("idscan", typeof(int));
            lastScans.Columns.Add("reussi", typeof(int));
            connection.Open();
            try
            {


                SqlDataAdapter Adap4 = new SqlDataAdapter("SELECT TOP 10 idscan,reussi FROM log where codeServeur=" + id + "and codeService=" + idservice + "GROUP BY idscan,reussi ORDER BY idscan  DESC", connection);
                Adap4.Fill(ds, "lastscans");

                foreach (DataRow p3 in ds.Tables["lastscans"].Rows)
                {
                    idscan = (int)p3["idscan"];

                    reussi = (int)p3["reussi"];
                    lastScans.Rows.Add(idscan, reussi);
                }

                dsf.Tables.Add(lastScans);



                dt.Columns.Add("lastdown", typeof(DateTime));
                dt.Columns.Add("lastup", typeof(DateTime));
                dt.Columns.Add("lastscan", typeof(DateTime));
                dt.Columns.Add("etat", typeof(int));


                SqlDataAdapter Adap = new SqlDataAdapter("SELECT  Temps FROM log where codeServeur=" + id + "and codeService=" + idservice + "and reussi=0", connection);

                Adap.Fill(ds, "lastdown");
                foreach (DataRow p in ds.Tables["lastdown"].Rows)
                {
                    lastDown = (DateTime)p["Temps"];
                }
                SqlDataAdapter Adap2 = new SqlDataAdapter("SELECT Temps FROM log where codeServeur=" + id + " and codeService=" + idservice + " and reussi=2", connection);

                Adap2.Fill(ds, "lastup");

                foreach (DataRow p1 in ds.Tables["lastup"].Rows)
                {
                    lastUp = (DateTime)p1["Temps"];
                }
                SqlDataAdapter Adap3 = new SqlDataAdapter("SELECT top 1 Temps,reussi FROM log where codeServeur=" + id + " and codeService=" + idservice + " GROUP BY Temps,reussi order by temps desc", connection);
                Adap3.Fill(ds, "lastscan");

                foreach (DataRow p2 in ds.Tables["lastscan"].Rows)
                {
                    lastScan = (DateTime)p2["Temps"];
                    etat = (int)p2["reussi"];
                }

                dt.Rows.Add(lastDown, lastUp, lastScan, etat);
                dsf.Tables.Add(dt);
                return dsf;
            }
            catch (Exception e)
            {

                dt.Rows.Add(e.Message, 0, 0, etat);
                dsf.Tables.Add(dt);
                return dsf;
            }
        }
        //------------------------------------------------------------------------------------------------
        public DataSet ConsulterAlert()
        {

            connection.Open();
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter Adap = new SqlDataAdapter("SELECT CodeServeur,CodeService,Messagelog,Temps fROM log  where  Reussi=0 OR Reussi=1", connection);
                Adap.Fill(ds, "log");
                connection.Close();
                return ds;
            }
            catch (Exception e)
            {
                return null;

            }

        }
        //-------------------------------------------------------------------------------------------------
        public DataSet StatAdmin()
        {


            int idscan = 999;
            int reussi = 999;
            DataSet dsf = new DataSet();
            DataSet ds = new DataSet();
            DataTable lastScans = new DataTable();
            DataTable dt = new DataTable();
            int etat = 54;
            lastScans.Columns.Add("idscan", typeof(int));
            lastScans.Columns.Add("reussi", typeof(int));
            try
            {
                connection.Open();
                SqlDataAdapter Adap4 = new SqlDataAdapter("SELECT TOP 10 idscan,reussi FROM log where codeServeur=0 and codeService=0 GROUP BY idscan,reussi ORDER BY idscan  DESC", connection);
                Adap4.Fill(ds, "lastscans");

                foreach (DataRow p3 in ds.Tables["lastscans"].Rows)
                {
                    idscan = (int)p3["idscan"];
                    reussi = (int)p3["reussi"];
                    lastScans.Rows.Add(idscan, reussi);
                }

                dsf.Tables.Add(lastScans);

                DateTime lastDown = DateTime.Now;
                DateTime lastUp = DateTime.Now;
                DateTime lastScan = DateTime.Now;


                dt.Columns.Add("lastdown", typeof(DateTime));
                dt.Columns.Add("lastup", typeof(DateTime));
                dt.Columns.Add("lastscan", typeof(DateTime));
                dt.Columns.Add("etat", typeof(int));
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
                    etat = (int)p2["reussi"];
                }

                dt.Rows.Add(lastDown, lastUp, lastScan, etat);
                dsf.Tables.Add(dt);

                return dsf;
            }
            catch (Exception e)
            {
                dt.Rows.Add(e.Message, 0, 0, etat);
                dsf.Tables.Add(dt);

                return dsf;
            }
        }
        //-------------------------------------------------------------------------------------------------
        public DataSet StatServeur(int id)
        {

            DateTime temps = DateTime.Now;
            int reussi = 999;
            int idscan = 999;
            DataTable dt = new DataTable();
            DataSet dsf = new DataSet();
            DataSet ds = new DataSet();
            DataTable lastScans = new DataTable();
            int etat = 999;
            lastScans.Columns.Add("idscan", typeof(int));
            lastScans.Columns.Add("reussi", typeof(int));
            try
            {
                connection.Open();
                SqlDataAdapter Adap4 = new SqlDataAdapter("SELECT TOP 10 idscan,reussi FROM log where codeServeur=" + id + " GROUP BY idscan,reussi ORDER BY idscan  DESC", connection);
                Adap4.Fill(ds, "lastscans");

                foreach (DataRow p3 in ds.Tables["lastscans"].Rows)
                {
                    idscan = (int)p3["idscan"];
                    reussi = (int)p3["reussi"];
                    lastScans.Rows.Add(idscan, reussi);
                }

                dsf.Tables.Add(lastScans);

                DateTime lastDown = DateTime.Now;
                DateTime lastUp = DateTime.Now;
                DateTime lastScan = DateTime.Now;


                dt.Columns.Add("lastdown", typeof(DateTime));
                dt.Columns.Add("lastup", typeof(DateTime));
                dt.Columns.Add("lastscan", typeof(DateTime));
                dt.Columns.Add("etat", typeof(int));
                SqlDataAdapter Adap = new SqlDataAdapter("SELECT TOP 1 Temps FROM log where codeServeur=" + id + " GROUP BY Temps order by temps desc ", connection);
                Adap.Fill(ds, "lastdown");
                foreach (DataRow p in ds.Tables["lastdown"].Rows)
                {
                    lastDown = (DateTime)p["Temps"];
                }
                SqlDataAdapter Adap2 = new SqlDataAdapter("SELECT top 1 Temps FROM log where codeServeur=" + id + "   GROUP BY Temps order by temps desc", connection);

                Adap2.Fill(ds, "lastup");

                foreach (DataRow p1 in ds.Tables["lastup"].Rows)
                {
                    lastUp = (DateTime)p1["Temps"];
                }
                SqlDataAdapter Adap3 = new SqlDataAdapter("SELECT top 1 Temps,reussi FROM log where codeServeur=" + id + "GROUP BY Temps,reussi order by temps desc", connection);
                Adap3.Fill(ds, "lastscan");

                foreach (DataRow p2 in ds.Tables["lastscan"].Rows)
                {
                    lastScan = (DateTime)p2["Temps"];
                    etat = (int)p2["reussi"];
                }

                dt.Rows.Add(lastDown, lastUp, lastScan, etat);
                dsf.Tables.Add(dt);

                return dsf;
            }
            catch (Exception e)
            {
                dt.Rows.Add(e.Message, 0, 0, 0);
                dsf.Tables.Add(dt);

                return dsf;
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
            dt.Columns.Add("Temps de reponse", typeof(TimeSpan));
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
                 return wcfMailSmtp(operation, res);
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
        //-----------------------------------------------------------------------------------------------------------------
        //public bool Smtp()
        //{
        //    var client = new TcpClient();
        //    var server = "smtp.gmail.com";
        //    var port = 465;
        //    try
        //    {
        //        client.Connect(server, port);
        //        // As GMail requires SSL we should use SslStream
        //        // If your SMTP server doesn't support SSL you can
        //        // work directly with the underlying stream
        //        var stream = client.GetStream();
        //        var sslStream = new SslStream(stream);
        //        sslStream.AuthenticateAsClient(server);
        //        var writer = new StreamWriter(sslStream);
        //        var reader = new StreamReader(sslStream);
        //        if (reader.ReadLine().IndexOf("220 smtp.gmail.com ") == 0)
        //        {
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }

        //}
        ////-----------------------------------------------------------------------------------------------------------------------
        //public bool testWeb(string url, string res)
        //{
        //    try
        //    {
        //        HttpWebRequest r = (HttpWebRequest)WebRequest.Create(url);
        //        HttpWebResponse response = (HttpWebResponse)r.GetResponse();
        //        //   request.Timeout = 3000;
        //        StreamReader reader = new StreamReader(response.GetResponseStream());
        //        string content = reader.ReadToEnd();
        //        if (content.IndexOf(res, StringComparison.CurrentCulture) > 0)
        //        {
        //            return true;
        //        }
        //        else return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        //-----------------------------------------------------------------------------------------------------------------------
        public string inscription(string username, string lastname, string adresse, string birthdate, string email, int tel, string login, string pwd)
        {
            int verif;
            string PSK = TimeSensitivePassCode.GeneratePresharedKey();
            string data = "otpauth://totp/" + login + "?secret=" + PSK;

            QRCodeGenerator qrg = new QRCodeGenerator();
            QRCodeGenerator.QRCode qc = qrg.CreateQrCode(data, QRCodeGenerator.ECCLevel.H);
            Bitmap bm = qc.GetGraphic(20);
            MemoryStream ms = new MemoryStream();
            bm.Save(ms, ImageFormat.Gif);
            Byte[] b = ms.ToArray();
            string bcd = Convert.ToBase64String(b);

            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "INSERT INTO Employe(Nom,Prenom,Adresse,DateDeNaissance,Email,Tel,Psk,Login,Pwd,access) VALUES (@username,@lastname,@adresse,@birthdate,@email,@tel,@psk,@login,@pwd,0)";
            cmd.Parameters.AddWithValue("@username", username.Trim());
            cmd.Parameters.AddWithValue("@lastname", lastname);
            cmd.Parameters.AddWithValue("@adresse", adresse);
            cmd.Parameters.AddWithValue("@birthdate", birthdate);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@tel", tel);
            cmd.Parameters.AddWithValue("@PSK", PSK);
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@pwd", pwd);
            DataTable dt = new DataTable();
            dt.Columns.Add("Error", typeof(string));
            DataSet ds = new DataSet();
            dt.Columns.Add("SKey", typeof(string));
            dt.Columns.Add("SourceImage", typeof(string));

            try
            {
                verif = cmd.ExecuteNonQuery();
                dt.Rows.Add("true", PSK, bcd);
            }
            catch (Exception ex)
            {
                dt.Rows.Add("Probl�me" + ex.Message, "fff", "******");
            }
            ds.Tables.Add(dt);
            return bcd;
        }

        public string rechPSK(string login, string pwd)
        {
            string psk;
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "select psk from Employe where Login = '" + login + "' and Pwd ='" + pwd + "'";
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return psk = reader.GetString(0);
        }

        public bool ValidOTP(string login, string pwd, String token)
        {
            string psk = rechPSK(login, pwd);
            return OtpHelper.HasValidTotp(token, psk);
        }



        //-----------------------------------------------------------------------------------------------------------------
        public DataSet wcfMailSent(string nom, string prenom, string destinataire, string psk)
        {
            Boolean verif = true;
            DataTable dt = new DataTable();
            dt.Columns.Add("resultat", typeof(Boolean));
            dt.Columns.Add("Temps de reponse", typeof(TimeSpan));
            dt.Columns.Add("ex", typeof(string));
            DataSet ds = new DataSet();
            try
            {
                DateTime TimeStart = DateTime.Now;
               

                MailMessage email = new MailMessage("htirahatem@gmail.com", "htirahatem@gmail.com");

                // information
                email.Subject = "Monitoring PSK Notification";
                email.IsBodyHtml = true;
                email.Body = " <img src=\"@@IMAGE@@\" alt=\"INLINE attachment\"> " +
                " <table><tr> <td> <div ><h3 style='text-align: center;'>banque de tunisie <br> IT department</h3></td> </div> </td></tr>  " + " </table>" +
             "<br><br><div style='text-align:center;font-size:20px;'>Hello Mrs "+nom +" "+prenom+" ,<br>" + "Thanks for signing up into our monitoring site<br>" + "Here is your personal PreShared Key :<h1>"+psk+"</h1><br>" + "Sincerely,<br>" + "BT Administration.<br></div>";


              
                // create the INLINE attachment
                string attachmentPath = Environment.CurrentDirectory + @"\bt.jpg";
                Attachment inline = new Attachment(attachmentPath);

                // generate the contentID string using the datetime
                string contentID = Path.GetFileName(attachmentPath).Replace(".", "") ;

                inline.ContentDisposition.Inline = true;
                inline.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
                inline.ContentId = contentID;
                inline.ContentType.MediaType = "image/jpg";
                inline.ContentType.Name = Path.GetFileName(attachmentPath);
                email.Attachments.Add(inline);

                // replace the tag with the correct content ID
                email.Body = email.Body.Replace("@@IMAGE@@", "cid:" + contentID);

                // sending the email with the SmtpDirect class (not using the System.Net.Mail.SmtpClient class)
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                email.From = new MailAddress("htirahatem@gmail.com");
                email.To.Add(destinataire);
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("htirahatem@gmail.com", "158115811581");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(email);

                email.Dispose();



                //  mail.Body = "<h1 style='color=red;'>This is for testing SMTP mail from GMAIL" + CodeServeur + "</h1> <br>";
                //mail.Body =   "<br><img src='C:/Users/hatem/Desktop/final 4/final 2/WcfServiceMonitoring/WcfServiceMonitoring\red.png ' />" + "<br><img src='" + Environment.CurrentDirectory + @"\red.png'; />" +






                //SmtpServer.Port = 587;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("htirahatem@gmail.com", "158115811581");
                //SmtpServer.EnableSsl = true;
                //SmtpServer.Send(mail);
                TimeSpan TimeDif = DateTime.Now - TimeStart;
                dt.Rows.Add(verif, TimeDif,"correct");
                ds.Tables.Add(dt);
                return ds;
            }
            catch (Exception ex)
            {
                DateTime TimeStart = DateTime.Now;
                TimeSpan TimeDif1 = DateTime.Now - TimeStart;
                dt.Rows.Add(verif, TimeDif1, ex.Message);
                ds.Tables.Add(dt);
                return ds;
            }

        }

        public Boolean wcfMochard(int CodeEmploye, string msg)
        {
            connection.Open();
            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "INSERT INTO Mochard (CodeEmploye,Msg,Date ) VALUES ( @cd,@msg,@date)";
            cmd.Parameters.AddWithValue("@cd", CodeEmploye);
            cmd.Parameters.AddWithValue("@msg", msg);
     
            
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            try
            {
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
