using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SMS365
{
    [DataContract]
    public class SMSMessage
    {
        [DataMember]
        public string src { get; set; }

        [DataMember]
        public string dst { get; set; }

        [DataMember]
        public string text { get; set; }

        [DataMember]
        public string url { get; set; }

        [DataMember]
        public string method { get; set; }
    }

    [DataContract]
    public class SMSMessageResult
    {
        [DataMember]
        public string api_id { get; set; }

        [DataMember]
        public string message { get; set; }

        [DataMember]
        public string[] message_uuid { get; set; }

    }

    [DataContract]
    public class GetSMSMessageResult
    {
        [DataMember]
        public string message_state { get; set; }
        [DataMember]
        public string message_uuid { get; set; }

        [DataMember]
        public string total_amount { get; set; }
        [DataMember]
        public string to_number { get; set; }

        [DataMember]
        public string total_rate { get; set; }
        [DataMember]
        public string api_id { get; set; }

        [DataMember]
        public string message_direction { get; set; }
        [DataMember]
        public string from_number { get; set; }

        [DataMember]
        public string message_time { get; set; }

        [DataMember]
        public int units { get; set; }
        [DataMember]
        public string message_type { get; set; }

        [DataMember]
        public string resource_uri { get; set; }

        [DataMember]
        public string error_code { get; set; }


    }

    [DataContract]
    public enum MessageStatus
    {
        errorwhilesubmitting = 180610006,
        submitted = 180610000,
        queued = 180610001,
        sent = 180610010,
        failed = 180610011,
        delivered = 2,
        undelivered = 180610012,
        rejected = 180610013
    }
}
