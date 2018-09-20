using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System.Activities;




namespace Custom_Workflows
{
    public class CalculateInvoiceAmount : CodeActivity
    {
        [Input("Country")]
        public InArgument<string> Country { get; set; }

        [Output("Amount")]
        public OutArgument<string> Amount { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            //Create the tracing service
            ITracingService tracingService = executionContext.GetExtension<ITracingService>();

            //Create the context
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            // Custom logic starts here

            // Getting input arguments into the code
            string country = Country.Get(executionContext);

           // CalculatorSoapClient client = new CalculatorSoapClient();
          //  int amount = client.Add(100, 200);

            Random random = new Random();
            int amount = random.Next(1, 100000);

            Amount.Set(executionContext, amount.ToString());
        }
    }
}
