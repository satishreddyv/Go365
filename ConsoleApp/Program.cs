using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.CalculatorService;
using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Tooling;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;
using System.ServiceModel;
using Microsoft.Xrm.Sdk.Client;
using System.Security.Cryptography;
using SMS365;
using System.Net.Http;
using System.IO;

namespace ConsoleApp
{

    class Program
    {
        static void Main(string[] args)
        {

            CrmServiceClient client = new CrmServiceClient("Url=https://testcrm321.crm.dynamics.com; Username=jchen@testcrm321.onmicrosoft.com; Password=Micro4578; authtype=Office365");

            IOrganizationService service = (IOrganizationService)
            client.OrganizationWebProxyClient != null ? (IOrganizationService)client.OrganizationWebProxyClient : (IOrganizationService)client.OrganizationServiceProxy;


            Contact newContact = new Contact
            {
                LastName = "Test2"
            };
            Guid contactGuid = service.Create(newContact);

            // Call the Action Register from code

            // Early binding
            rev_RegisterRequest req = new rev_RegisterRequest();
            req.Target = new EntityReference("contact", contactGuid);
            req.Name = "Test";
            rev_RegisterResponse response = (rev_RegisterResponse)service.Execute(req);

            // Late
            OrganizationRequest req2 = new OrganizationRequest("rev_Register");
            req2.Parameters.Add("Name", "Test");
            req2.Parameters.Add("Target", new EntityReference("contact", contactGuid));
            OrganizationResponse response2 = service.Execute(req2);

            CrmContext context = new CrmContext(service);


            //Without Keys

            var cards = from c in context.rev_creditcardSet
                        where c.rev_number == "1"
                        select c;

            Entity cc = new Entity("rev_creditcard");
            cc.Attributes["rev_cvv"] = "123";
            cc.Id = cards.FirstOrDefault().Id;
            service.Update(cc);


            // With Keys
            Entity cc2 = new Entity("rev_creditcard", "rev_numberkey", "5487440000000000");
            cc2.Attributes.Add("rev_cvv","123");
            service.Update(cc2);




            //using (var reader = new StreamReader(@"C:\Users\Admin\Desktop\SDKV9\Data2.csv"))
            //{

            //    ExecuteMultipleRequest em = new ExecuteMultipleRequest();
            //    em.Requests = new OrganizationRequestCollection();
            //    em.Settings = new ExecuteMultipleSettings
            //    {
            //        ContinueOnError = true,
            //        ReturnResponses = true

            //    };
            //    CreateRequest req;

            //    while (!reader.EndOfStream)
            //    {
            //        var line = reader.ReadLine();
            //        var values = line.Split(',');
            //        string number = values[0];
            //        string cvv = values[1];
            //        string name = values[2];
            //        rev_creditcard cc = new rev_creditcard()
            //        {
            //            rev_CVV = cvv,
            //            rev_Name = name,
            //            rev_number = number
            //        };
            //        req = new CreateRequest();
            //        req.Target = cc;

            //        em.Requests.Add(req);

            //    }
            //    ExecuteMultipleResponse res = (ExecuteMultipleResponse)service.Execute(em);
            //}



            //Contact c = new Contact
            //{
            //    LastName = "c"
            //};

            //CreateRequest req1 = new CreateRequest();
            //req1.Target = c;
            ////     CreateResponse res1 = (CreateResponse)service.Execute(req1);

            //Account a = new Account
            //{
            //    Name = "a"
            //};
            //CreateRequest req2 = new CreateRequest();
            //req2.Target = a;
            ////    CreateResponse res2 = (CreateResponse)service.Execute(req2);

            //ExecuteMultipleRequest em = new ExecuteMultipleRequest();
            //em.Requests = new OrganizationRequestCollection();
            //em.Requests.Add(req1);
            //em.Requests.Add(req2);
            //em.Settings = new ExecuteMultipleSettings
            //{
            //    ContinueOnError = true,
            //    ReturnResponses = true

            //};

            //ExecuteMultipleResponse res = (ExecuteMultipleResponse)service.Execute(em);





            //CreateRequest request = new CreateRequest();
            //request.Target = contact;
            //CreateResponse response = (CreateResponse)service.Execute(request);
            //Guid contactGuid = response.id;

            //// Sharing a record with users
            //GrantAccessRequest request2 = new GrantAccessRequest();
            //request2.Target = new EntityReference("contact", contactGuid);

            //PrincipalAccess pa = new PrincipalAccess();
            //pa.AccessMask = AccessRights.WriteAccess;
            //pa.Principal = new EntityReference("systemuser", new Guid("GUID"));

            //request2.PrincipalAccess = pa;

            //GrantAccessResponse response2 = (GrantAccessResponse)service.Execute(request2);


            //CreateEntityRequest req = new CreateEntityRequest();
            //req.Entity = new Microsoft.Xrm.Sdk.Metadata.EntityMetadata
            //{
            //    SchemaName = "",
            //    EntityColor = "",

            //};





            // Using Fetch XML


            //            string query = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
            //  <entity name='account'>
            //    <attribute name='name' />
            //    <attribute name='primarycontactid' />
            //    <attribute name='telephone1' />
            //    <attribute name='accountid' />
            //    <order attribute='name' descending='false' />
            //    <filter type='and'>
            //      <condition attribute='address1_city' operator='eq' value='Redmond' />
            //    </filter>
            //  </entity>
            //</fetch>";

            //      EntityCollection collection = service.RetrieveMultiple(new FetchExpression(query));











            // Late binding code (default)
            //Entity contact = new Entity("contact");
            //contact.Attributes.Add("lastname", "Test contact from Console");
            //Guid contactGuid = service.Create(contact);

            //// Early binding code 
            //Contact obj = new Contact();
            //obj.FirstName = "First name";
            //obj.LastName = "Last name";



            //Contact obj2 = new Contact()
            //{
            //    FirstName = "First name",
            //    LastName = "Last name"
            //};

            //rev_creditcard card = new rev_creditcard()
            //{
            //    rev_Name = "name of the cc",
            //    rev_number = "3431343134313431",
            //    rev_CVV = "432",

            //};

            //  Guid cardGuid = service.Create(card);

            // Console.WriteLine(cardGuid);

            //using (CrmContext context = new CrmContext(service))
            //{

            //    var requests = from request in context.rev_reimbSet
            //                   where request.rev_ReimbursementStatus.Value == 283210002
            //                   select new
            //                   {
            //                       request.rev_name,
            //                       request.rev_totalprice
            //                   };

            //    decimal total = 0;
            //    foreach (var request in requests)
            //    {
            //        total += request.rev_totalprice.Value;
            //    }


            //    Console.WriteLine(total);

            //var contacts = from item in context.ContactSet
            //               where item.Address1_City == "Redmond"
            //               select new
            //               {
            //                   item.FullName
            //               };

            //foreach (var contact in contacts)
            //{
            //    Console.WriteLine(contact.FullName);
            //}

        }

        //  Console.ReadLine();

        /*
        string text = "Hello world";// Message.Get(executionContext);
        string dst = "919985617470"; //DestinationNumber.Get(executionContext);
        string src = "14154847489";
        string accountSid = "MAMDQ0MDK0ODQYYTUWZD";
        string authToken = "NWMzYmZhMzMxODA4YjkyZTJmMmRmMDFkMzg0NTJk";

        using (var hclient = new HttpClient())
        {
            var url = "https://api.plivo.com/v1/Account/" + accountSid.Trim() + "/Message/";
            hclient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", accountSid.Trim(), authToken.Trim()))));

            //tracingService.Trace(url + text + dst + src);

            var SMSMessage = new SMSMessage
            {
                src = src.Trim(),
                dst = dst.Replace('+', ' ').Trim(),
                text = text,
                url = "http://messagingreport.cloudapp.net/report.aspx/",
                method = "POST"
            };

            // This will produce a JSON String
            var serialized = Helper.JSONSerializer<SMSMessage>.Serialize(SMSMessage);
            HttpContent content = new StringContent(serialized.ToString(), Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> tsk = hclient.PostAsync(url, content);
            tsk.Wait();

            if (tsk.Result.ReasonPhrase == "ACCEPTED")
            {
                string txtResult = tsk.Result.Content.ReadAsStringAsync().Result;

                SMSMessageResult smsResult = Helper.JSONSerializer<SMSMessageResult>.DeSerialize(tsk.Result.Content.ReadAsStringAsync().Result);


            }

*/
        //Entity smsEntity = new Entity("sms_sms");
        //    smsEntity.Attributes.Add("sms_smsmessage", "test:" + DateTime.Now);
        //    smsEntity.Attributes.Add("sms_to", "9199856174705");
        //    // smsEntity.Id = 
        //    Guid guid = service.Create(smsEntity);

        //    smsEntity.Attributes.Add("statecode", new OptionSetValue(1));
        //   // smsEntity.Attributes.Add("statuscode", new OptionSetValue(180610010));
        //smsEntity.Id = guid;
        //    service.Update(smsEntity);
        //  Entity smsEntity = new Entity("sms_sms");
        //smsEntity.Attributes.Add("statecode", new OptionSetValue(1));
        //smsEntity.Attributes.Add("statuscode", new OptionSetValue(180610000));
        //smsEntity.Id = guid;
        //service.Update(smsEntity);
















        //string key = @"1234567891234567";
        //string temp = Encrypt("1000", key);
        //Console.WriteLine(temp);

        //string temp2 = Decrypt(temp, key);
        //Console.WriteLine(temp2);
        // Calling action

        //Contact con = new Contact()
        //{
        //    FirstName = "Action Test",
        //    LastName = "Smith",
        //    EMailAddress1 = "someemail@gmail.com"

        //};

        //Guid contactGuid = service.Create(con);

        //OrganizationRequest actionReq = new OrganizationRequest("lexmark_register");
        //actionReq.Parameters.Add("Source", "Website");
        //actionReq.Parameters.Add("Target", 
        //    new EntityReference(Contact.EntityLogicalName, contactGuid));
        //OrganizationResponse response = service.Execute(actionReq);
        //Console.WriteLine(response.Results["Status"].ToString());


        // 4th type of getting data : LINQ Query

        //using (OrganizationServiceContext context = new OrganizationServiceContext(service))
        //{

        //    var contacts = from item in context.CreateQuery("contact")
        //                   where item["address1_city"].Equals("Redmond") ||
        //                   item["address1_city"].Equals("Seattle")
        //                   select item["lastname"];

        //    foreach(var item in contacts)
        //    {
        //        Console.WriteLine(item.ToString());
        //    }

        //    var query_join2 = from c in context.CreateQuery("contact")
        //                      join a in context.CreateQuery("account")
        //                      on c["contactid"] equals a["primarycontactid"]
        //                      select new
        //                      {
        //                          contact_name = c["fullname"],
        //                          account_name = a["name"]
        //                      };
        //    foreach (var c in query_join2)
        //    {
        //        System.Console.WriteLine(c.contact_name + "  " + c.account_name);
        //    }
        //}



        //// During Create
        //Entity account = new Entity("account", "lexmark_accountnumberkey", "1234");
        //account.Attributes.Add("name", "Sample Account");
        //Guid accountGuid = service.Create(account);

        //try
        //{

        //    // During update
        //    Entity updateAccount = new Entity("account");
        //    // updateAccount.Id = accountGuid;
        //    updateAccount.KeyAttributes.Add("lexmark_accountnumberkey", "1234");
        //    updateAccount.Attributes.Add("description", "Test desc");
        //    service.Update(updateAccount);

        //    //service.Retrieve()
        //}
        //catch (FaultException<OrganizationServiceFault> ex)
        //{

        //}

        //RetrieveEntityChangesRequest req = new RetrieveEntityChangesRequest();
        //req.EntityName = "account";
        //req.DataVersion = "1552167!03 / 13 / 2018 06:41:13";
        //req.Columns = new ColumnSet(new string[] { "name" });
        //req.PageInfo = new PagingInfo()
        //{
        //   PageNumber = 1,
        //    ReturnTotalRecordCount = false,
        //    Count = 5000
        //};

        //RetrieveEntityChangesResponse response = (RetrieveEntityChangesResponse)service.Execute(req);

        //  Console.Read();










        // Custom code starts here

        //GrantAccessRequest req = new GrantAccessRequest();
        //req.PrincipalAccess = new PrincipalAccess()
        //{
        //    AccessMask = AccessRights.ReadAccess,
        //    Principal = new EntityReference("systemuser", new Guid(""))
        //};
        //req.Target = new EntityReference("account", new Guid());

        //GrantAccessResponse response =  (GrantAccessResponse)service.Execute(req);



        //Account acc = new Account()
        //{
        //    Name = "Sample Acc"
        //};

        //lexmark_benefit benefit = new lexmark_benefit()
        //{
        //    lexmark_name = "Sample ben",
        //    lexmark_StartDate = DateTime.Now,
        //    lexmark_AccountId = new EntityReference("account", service.Create(acc))

        //};

        //service.Create(benefit);




        //Entity contact = new Entity("contact");

        //contact.Attributes.Add("lastname", "Smith");
        //contact.Attributes.Add("firstname", "Mary");

        //Guid contactGuid = service.Create(contact);


        //Entity incident = new Entity("incident");
        //incident.Attributes.Add("title", "Sample title");
        //incident.Attributes.Add("customerid", new EntityReference("contact", contactGuid));

        //Guid incidentGuid = service.Create(incident);


        //ColumnSet cols = new ColumnSet();
        //cols.AddColumn("ticketnumber");

        //Entity retrived = service.Retrieve("incident", incidentGuid, cols);

        //Console.WriteLine(retrived.Attributes["ticketnumber"].ToString());





        //CalculatorSoapClient client = new CalculatorSoapClient();
        //CalculatorSoapClient

        //  }
        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            Console.WriteLine(UTF8Encoding.UTF8.GetBytes(key).Length);

            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

    }
}
