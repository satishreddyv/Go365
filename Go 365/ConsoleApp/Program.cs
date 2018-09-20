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

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient client = new CrmServiceClient("Url=https://lexmarkcrm.crm8.dynamics.com; Username=satish@lexmarkcrm.onmicrosoft.com; Password=Pearl@123; authtype=Office365");
            IOrganizationService service = (IOrganizationService)
                client.OrganizationWebProxyClient != null ? (IOrganizationService)client.OrganizationWebProxyClient : (IOrganizationService)client.OrganizationServiceProxy;

            //Extension.DoSomething();

            CalService.CalculatorSoapClient soapClient = new CalService.CalculatorSoapClient();

            Console.WriteLine(soapClient.Multiply(100, 30));
            Console.Read();

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

                Console.Read();










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

        }
    }
}
