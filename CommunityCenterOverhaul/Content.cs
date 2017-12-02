using Newtonsoft.Json;

namespace CommunityCenterBundleOverhaul
{
    public partial class Content
    {
        [JsonProperty("BundleContent")]
        public string BundleContent { get; set; }

        [JsonProperty("BundleName")]
        public string BundleName { get; set; }

        [JsonProperty("Key")]
        public string Key { get; set; }
    }
}