using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Crm.Sdk.Messages;
using System.Xml;
using Microsoft.Xrm.Sdk.Messages;

namespace MyPlugins
{
    public class AccountUpdate : IPlugin
    {
        public AccountUpdate(string unsecureConfig, string secureConfig)
        {
            //    XmlDocument doc = new XmlDocument();
            //    doc.Load(secureConfig);

            //    doc.GetElementsByTagName
        }

        public void Execute(IServiceProvider serviceProvider)
        {
            //Extract the tracing service for use in debugging sandboxed plug-ins.
            ITracingService tracingService =
                (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the execution context from the service provider.
            IPluginExecutionContext context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service =
                serviceFactory.CreateOrganizationService(context.UserId);

            // The InputParameters collection contains all the data passed in the message request.

            if (context.InputParameters.Contains("Target") &&
            context.InputParameters["Target"] is Entity)
            {
                // Obtain the target entity from the input parameters.
                Entity account = (Entity)context.InputParameters["Target"];

                string desc = account.Attributes["description"].ToString();
                decimal annualRevenue = 0;

                // If user updates revenue attribute
                if (account.Attributes.Contains("revenue"))
                {
                    annualRevenue = ((Money)account.Attributes["revenue"]).Value;
                }
                else
                {
                    // Handling when user does not update revenue attribute
                    Entity preAccount = (Entity)context.PreEntityImages["PreImage"];

                    if (preAccount.Attributes.Contains("revenue"))
                    {
                        annualRevenue = ((Money)preAccount.Attributes["revenue"]).Value;

                    }
                }

                if (context.Depth > 1)
                {
                    return;
                }
                account.Attributes["description"] = annualRevenue.ToString() + desc;
                service.Update(account);


                // Retrieve related benefits
                // Different ways of querying data from CRM
                // 1. Query Expression
                // 2. Query By Attribute
                // 3. Fetchxml
                // 4. LINQ

                // Trying 1st method: Query Expression

                QueryExpression query = new QueryExpression("lexmark_benefit");

                query.ColumnSet.AddColumn("lexmark_benefitid");


                ConditionExpression cond1 = new
                    ConditionExpression("attrbute1", ConditionOperator.Equal, "value");
                ConditionExpression cond2 = new ConditionExpression("attribute2", ConditionOperator.Equal, "");
                FilterExpression filter = new FilterExpression();
                filter.FilterOperator = LogicalOperator.Or;
                filter.Conditions.Add(cond1);
                filter.Conditions.Add(cond2);

                query.Criteria.AddFilter(filter);

                EntityCollection collection = service.RetrieveMultiple(query);

                ExecuteMultipleRequest mutipleReq = new ExecuteMultipleRequest();
                //req.Requests = 

               //service.RetrieveMultiple()

               // ExecuteWorkflowRequest workflowRequest = new ExecuteWorkflowRequest();
               // workflowRequest.EntityId = "";
               // workflowRequest.WorkflowId = 
               // workflowRequest.


                CreateEntityRequest req = new CreateEntityRequest()
                {
                    Entity = new Microsoft.Xrm.Sdk.Metadata.EntityMetadata()
                    {
                        LogicalName = "lexmark_benefit",
                        IsActivity = false,
                        IsAuditEnabled = new BooleanManagedProperty(true),
                        EntityColor = "x100000",
                        DisplayName = new Label("Benefit",1033),
                        DisplayCollectionName = new Label("Benefits", 1033),
                        IsBPFEntity = true,
                        IsDuplicateDetectionEnabled = new BooleanManagedProperty(true),
                        OwnershipType = Microsoft.Xrm.Sdk.Metadata.OwnershipTypes.UserOwned
                
                    }
                };

               // ExportSolutionRequest 


             //   UpdateRequest updateReq;
                
                foreach (Entity benefit in collection.Entities)
                {
                    updateReq = new UpdateRequest();
                    benefit.Attributes.Add("lexmark_description", annualRevenue.ToString()
                        + "\n" + desc);
                    updateReq.Target = benefit;
                    mutipleReq.Requests.Add(updateReq);
                    // service.Update(benefit);
                }
                ExecuteMultipleResponse res = (ExecuteMultipleResponse)
                    service.Execute(mutipleReq);


            }
        }


    }
}