using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebGet(UriTemplate = "alert", ResponseFormat = WebMessageFormat.Json)]
        string ConsulterAlert();
        [OperationContract]
        [WebGet(UriTemplate = "noyau", ResponseFormat = WebMessageFormat.Json)]
        string noyau();
        [OperationContract]
        [WebGet(UriTemplate = "VerifAdmin/{id}", ResponseFormat = WebMessageFormat.Json)]
        bool VerifAdmin(string id);
        [OperationContract]
        [WebGet(UriTemplate = "consulterdroit/{id}", ResponseFormat = WebMessageFormat.Json)]
        string ConsulterServeurID(string id);
        [OperationContract]
        [WebGet(UriTemplate = "Service/{id}", ResponseFormat = WebMessageFormat.Json)]
        string ConsulterServiceID(string id);
        [OperationContract]
        [WebGet(UriTemplate = "Log/{id}", ResponseFormat = WebMessageFormat.Json)]
        string ConsulterLogServeur(string id);
        //[OperationContract]
        ////[WebGet(UriTemplate = "ping/{url}", ResponseFormat = WebMessageFormat.Json)]
        //string ping(String url);
        [OperationContract]
        [WebGet(UriTemplate = "Data/{value}", ResponseFormat = WebMessageFormat.Json)]
        string GetData(string value);
        [OperationContract]
        [WebGet(UriTemplate = "ConsulterLog", ResponseFormat = WebMessageFormat.Json)]
        string ConsulterLog();
        [OperationContract]
        [WebGet(UriTemplate = "ConsulterServeur", ResponseFormat = WebMessageFormat.Json)]
        string ConsulterServeur();
        [OperationContract]
        [WebGet(UriTemplate = "ConsulterEmploye", ResponseFormat = WebMessageFormat.Json)]
        string ConsulterEmploye();
        [OperationContract]
        [WebGet(UriTemplate = "ConsulterService", ResponseFormat = WebMessageFormat.Json)]
        string ConsulterService();

        [OperationContract]
        [WebGet(UriTemplate = "StatAdmin", ResponseFormat = WebMessageFormat.Json)]
        string StatAdmin();
        [OperationContract]
        [WebGet(UriTemplate = "StatServeur/{id}", ResponseFormat = WebMessageFormat.Json)]
        string StatServeur(string id);
        [OperationContract]
        [WebGet(UriTemplate = "rechPSK/{login}/{pwd}",ResponseFormat = WebMessageFormat.Json)]
        string rechPSK(string login, string pwd);
        

        [OperationContract]
        [WebGet(UriTemplate = "ValidOTP/{login}/{pwd}/{token}", ResponseFormat = WebMessageFormat.Json)]
        bool ValidOTP(string login, string pwd, String token);
        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
