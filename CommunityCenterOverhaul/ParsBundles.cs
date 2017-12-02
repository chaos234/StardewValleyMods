using Newtonsoft.Json;

namespace CommunityCenterBundleOverhaul
{
    public partial class ParsBundles
    {
        [JsonProperty("Content")]
        public Content[] Content { get; set; }

        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }

    public partial class ParsBundles
    {
        public static ParsBundles[] FromJson(string json) => JsonConvert.DeserializeObject<ParsBundles[]>(json, Converter.Settings);
    }
}
