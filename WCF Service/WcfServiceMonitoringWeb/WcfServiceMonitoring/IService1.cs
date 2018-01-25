using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceMonitoring
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.


    
    [ServiceContract]
     [XmlSerializerFormat]
      public interface IService1
    {
        [OperationContract]
        bool ValidOTP(string login, string pwd, String token);
        [OperationContract]
        DataSet StatService(int id, int idservice);
        [OperationContract]
        DataSet ConsulterAlert();
        [OperationContract]
        DataSet LastScans(int id);
        [OperationContract]
        DataSet StatAdmin();
        [OperationContract]
        DataSet StatServeur(int id);
        [OperationContract]
        int idscanmax();

        [OperationContract]
        string inscription(string username, string lastname, string adresse, string birthdate, string email, int tel, string login, string pwd);
        [OperationContract]
        string rechPSK(string login, String pwd);
        [OperationContract]
        Boolean wcflog(int codeserveur,int codeservice, int idscan, string msg,bool test, TimeSpan tdr);
        [OperationContract]
        string noyau();
        [OperationContract]
        void Timer(int a);
        [OperationContract]
        //Boolean GetData(string url);
        //[OperationContract]
        DataSet ping(String url);
        [OperationContract]
        DataSet wcfMailSmtp(string operation, string res);
        [OperationContract]
        DataSet wcfSql(int CodeServeur, string operation, string res);
        [OperationContract]
        DataSet wcfMailSent(string nom, string prenom ,string email, string psk);
        [OperationContract]
        DataSet wcfUrl(string url, string res);
        [OperationContract]
        DataSet wcfService(string type, int CodeServeur, string operation, string res, string destinataire,int port);
        [OperationContract]
        //bool Smtp();
        //[OperationContract]
        //bool testWeb(String url,String res);
        //[OperationContract]
        DataSet wcfport(String ip, int port);
        [OperationContract]
        Boolean wcfMochard(int CodeEmploye, string msg);
       


    }

 
}
