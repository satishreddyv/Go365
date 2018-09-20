using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace MyPlugins
{
    public class AccountCreate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            //Extract the tracing service for use in debugging sandboxed plug-ins.
            ITracingService tracingService =
                (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the execution context from the service provider.
            IPluginExecutionContext context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
 
            

         //   exp.FilterOperator = LogicalOperator.Or;

            // The InputParameters collection contains all the data passed in the message request.
            if (context.InputParameters.Contains("Target") &&
            context.InputParameters["Target"] is Entity)
            {
                // Obtain the target entity from the input parameters.
                Entity account = (Entity)context.InputParameters["Target"];

                account.Attributes.Add("lexmark_id", account.Id.ToString());

                string number = account.Attributes["accountnumber"].ToString();

                // Retrieve accounts with same account number

                QueryByAttribute query = new QueryByAttribute("account");
                query.ColumnSet = new ColumnSet(new string[] { "accountnumber" });
                query.AddAttributeValue("accountnumber", number);

                EntityCollection collection = service.RetrieveMultiple(query);

                if(collection.Entities.Count > 0)
                {
                    throw new InvalidPluginExecutionException("Account with same number already exists!!!");
                }

            }
        }


    }
}
