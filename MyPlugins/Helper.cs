using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace MyPlugins
{
    class Helper
    {
        public static string GetConfiguration(string name, IOrganizationService service)
        {
            try
            {
                // Different ways of querying data from CRM
                // 1. Query Expression
                // 2. Query By Attribute
                // 3. Fetchxml
                // 4. LINQ

                // Using FetchXML

                string query = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
  <entity name='lexmark_configuration'>
    <attribute name='lexmark_value' />
    <filter type='and'>
      <condition attribute='lexmark_name' operator='eq' value='" + name + @"' />
    </filter>
  </entity>
</fetch>";

                EntityCollection collection = service.RetrieveMultiple(new FetchExpression(query));

                Entity config = collection.Entities.FirstOrDefault();

                return config.Attributes["lexmark_value"].ToString();
            }
            catch 
            {
                throw;
            }
        }
    }
}
