using Newtonsoft.Json;

namespace PongSignalR.Models
{
    public class GameObject
    {
        // We declare Left, Top and Id as lowercase with 
        // JsonProperty to sync the client and server models
        [JsonProperty("left")]
        public double Left { get; set; }

        [JsonProperty("top")]
        public double Top { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        // We don't want the client to get the "LastUpdatedBy" or "Moved" properties, so we mark them with JsonIgnore
        [JsonIgnore]
        public string LastUpdatedBy { get; set; }

        [JsonIgnore]
        public bool Moved { get; set; }
    }
}