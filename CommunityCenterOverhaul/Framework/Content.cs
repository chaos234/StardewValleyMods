using Newtonsoft.Json;

namespace CommunityCenterBundleOverhaul.Framework
{
    internal class Content
    {
        [JsonProperty("BundleContent")]
        public string BundleContent { get; set; }

        [JsonProperty("BundleName")]
        public string BundleName { get; set; }

        [JsonProperty("Key")]
        public string Key { get; set; }
    }
}