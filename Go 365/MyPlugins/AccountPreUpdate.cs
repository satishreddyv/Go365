using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Crm.Sdk.Messages;
using System.Xml;
using System.ServiceModel;

namespace MyPlugins
{
    public class AccountPreUpdate : IPlugin
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
            IOrganizationService service =
                serviceFactory.CreateOrganizationService(context.UserId);

            // The InputParameters collection contains all the data passed in the message request.

            if (context.InputParameters.Contains("Target") &&
            context.InputParameters["Target"] is Entity)
            {
                // Obtain the target entity from the input parameters.
                Entity account = (Entity)context.InputParameters["Target"];

                try
                {
                    decimal revenue = 0;
                    string country = string.Empty;

                    if (account.Attributes.Contains("revenue")
                         && account.Attributes.Contains("address1_country"))
                    {
                        revenue = ((Money)account.Attributes["revenue"]).Value;
                        country = account.Attributes["address1_country"].ToString();


                    }
                    else
                    {
                        return;
                    }

                    string name = string.Empty;
                    switch (country)
                    {
                        case "US":
                            name = "USTax";
                            break;

                        case "Canada":
                            name = "CanTax";
                            break;
                    }

                    string tax = Helper.GetConfiguration(name, service);
                    tracingService.Trace("Retrived tax from config is " + tax);
                    decimal revenueAfterTax = revenue + revenue * Convert.ToDecimal(tax) / 100;
                    tracingService.Trace("Updating account with tax");
                    account.Attributes.Add("lexmark_revenueaftertax", new Money(revenueAfterTax));
                }
                catch(FaultException<OrganizationServiceFault> faultException)
                {
                    tracingService.Trace(faultException.Message);
                }
                catch(Exception ex)
                {
                    tracingService.Trace(ex.Message);
                }
            }
        }


    }
}