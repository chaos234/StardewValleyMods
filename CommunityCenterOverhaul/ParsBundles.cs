namespace CommunityCenterBundleOverhaul
{
    using Newtonsoft.Json;

    public partial class ParsBundles
    {
        [JsonProperty("Content")]
        public Content[] Content { get; set; }

        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }

    public partial class Content
    {
        [JsonProperty("BundleContent")]
        public string BundleContent { get; set; }

        [JsonProperty("BundleName")]
        public string BundleName { get; set; }

        [JsonProperty("Key")]
        public string Key { get; set; }
    }

    public partial class ParsBundles
    {
        public static ParsBundles[] FromJson(string json) => JsonConvert.DeserializeObject<ParsBundles[]>(json, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
