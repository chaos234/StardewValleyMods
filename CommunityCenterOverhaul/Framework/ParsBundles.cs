using Newtonsoft.Json;

namespace CommunityCenterBundleOverhaul.Framework
{
    internal class ParsBundles
    {
        [JsonProperty("Content")]
        public Content[] Content { get; set; }

        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        public static ParsBundles[] FromJson(string json) => JsonConvert.DeserializeObject<ParsBundles[]>(json, Converter.Settings);
    }
}
