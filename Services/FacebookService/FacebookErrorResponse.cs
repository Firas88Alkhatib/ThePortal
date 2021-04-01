using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ThePortal.Services.FacebookService
{
        public class FacebookErrorResponse
        {
            [JsonPropertyName("error")]
            public Error Error { get; set; }
        }

        public class Error
        {
            [JsonPropertyName("message")]
            public string Message { get; set; }
            [JsonPropertyName("type")]
            public string Type { get; set; }
            [JsonPropertyName("code")]
            public int Code { get; set; }
            [JsonPropertyName("error_subcode")]
            public int ErrorSubcode { get; set; }
            [JsonPropertyName("error_user_title")]
            public string ErrorUserTitle { get; set; }
            [JsonPropertyName("error_user_msg")]
            public string ErrorUserMsg { get; set; }
            [JsonPropertyName("fbtrace_id")]
            public string FBTraceId { get; set; }
        }
}
