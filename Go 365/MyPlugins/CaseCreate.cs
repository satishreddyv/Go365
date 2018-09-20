using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using MyPlugins.Calculator;

namespace MyPlugins
{
    public class CaseCreate : IPlugin
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


            // The InputParameters collection contains all the data passed in the message request.
            if (context.InputParameters.Contains("Target") &&
            context.InputParameters["Target"] is Entity)
            {
                // Obtain the target entity from the input parameters.
                Entity incident = (Entity)context.InputParameters["Target"];

                if (incident.Attributes.Contains("description"))
                {
                    incident.Attributes["description"] = "Sample description";
                }
                else

                {
                    incident.Attributes.Add("description", "Sample description");
                }

                


                // Creating Entity Object for Task

                Entity task = new Entity();
                task.LogicalName = "task";
                // String
                task.Attributes.Add("subject", "Follow up with customer");
                task.Attributes.Add("description", "Arrange a meeting with customer");

                // Optionset
                // 0 = Low, 1 = Normal, 2 = High
                task.Attributes.Add("prioritycode", new OptionSetValue(2));

                // Lookup
                task.Attributes.Add("regardingobjectid", 
                    new EntityReference("incident", incident.Id));

                // Datetime
                task.Attributes.Add("scheduledend", DateTime.Now.AddDays(3));

                // Two Options
                // task.Attributes.Add("somefield", true);

                // Currency
                //task.Attributes.Add("somefield", new Money(1300));

                AddRequest addRequest = new AddRequest();
                addRequest.intA = 100;
                addRequest.intB = 200;
                
                

                service.Create(task);



            }
        }


    }
}
