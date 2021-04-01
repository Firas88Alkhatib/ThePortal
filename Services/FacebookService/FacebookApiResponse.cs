using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ThePortal.Services.FacebookService
{
    public class FacebookApiResponse<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
        [JsonPropertyName("paging")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Paging Paging { get; set; }
    }

    public class Paging
    {
        [JsonPropertyName("cursors")]
        public Cursors Cursors { get; set; }
    }

    public class Cursors
    {
        [JsonPropertyName("before")]
        public string Before { get; set; }
        [JsonPropertyName("after")]
        public string After { get; set; }
    }

}


