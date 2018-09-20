using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SMS365
{
    class Helper
    {
        public static class JSONSerializer<TType> where TType : class
        {
            /// <summary>
            /// Serializes an object to JSON
            /// </summary>
            public static string Serialize(TType instance)
            {
                var serializer = new DataContractJsonSerializer(typeof(TType));
                using (var stream = new MemoryStream())
                {
                    serializer.WriteObject(stream, instance);
                    return Encoding.Default.GetString(stream.ToArray());
                }
            }

            /// <summary>
            /// DeSerializes an object from JSON
            /// </summary>
            public static TType DeSerialize(string json)
            {
                using (var stream = new MemoryStream(Encoding.Default.GetBytes(json)))
                {
                    var serializer = new DataContractJsonSerializer(typeof(TType));
                    return serializer.ReadObject(stream) as TType;
                }
            }
        }


        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
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


        public static Dictionary<string, string> GetConfig(IOrganizationService service, ITracingService tracingService)
        {
            try
            {
                string xml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
    <entity name='sms_smsconfig'>
    <attribute name='sms_smsconfigid' />
    <attribute name='sms_key' />
    <attribute name='sms_value' />
  </entity>
</fetch>";

                EntityCollection collection = service.RetrieveMultiple(new FetchExpression(xml));

                if (collection.Entities.Count == 0)
                {
                    tracingService.Trace("Configuration keys missing");
                    throw new Exception("Configuration keys missing");
                }

                Dictionary<string, string> keys = new Dictionary<string, string>();

                foreach(Entity item in collection.Entities)
                {
                    keys.Add(item.Attributes["sms_key"].ToString(), item.Attributes["sms_value"].ToString());
                    if(item.Attributes["sms_key"].ToString() == "trial")
                    {

                        keys.Add("sms_smsconfigid", item.Id.ToString());

                    }
                }

                return keys;
            }
            catch
            {
                throw;
            }
        }

    }
}
