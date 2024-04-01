using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain
{
    [Serializable]
    public class ClassBase
    {
        [NotMapped, JsonIgnore]
        public string UserIdentification { get; set; }

        [NotMapped, JsonIgnore]
        public string IpAddress { get; set; }

        [NotMapped, JsonIgnore]
        public string Ubication { get; set; } = "CO - Colombia";

        [NotMapped, JsonIgnore]
        public string Header { get; set; }

        [NotMapped, JsonIgnore]
        public string Method { get; set; }
    }
}
