using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ThePortal.Models.Google
{
    public class GoogleData
    {
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public string AccountName { get; set; }
        [JsonIgnore]
        public ApplicationUser User { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
    }
}

