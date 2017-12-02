using System;
using System.Net;
using System.Collections.Generic;

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

    public partial class CCBOConfigParser
    {
        [JsonProperty("List")]
        public List[] List { get; set; }

        [JsonProperty("modManifest")]
        public ModManifest ModManifest { get; set; }

        [JsonProperty("OptionsVersion")]
        public Version OptionsVersion { get; set; }
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

    public partial class Choices
    {
        [JsonProperty("default")]
        public string Default { get; set; }

        [JsonProperty("v02")]
        public string V02 { get; set; }

        [JsonProperty("v02a")]
        public string V02a { get; set; }

        [JsonProperty("v02b")]
        public string V02b { get; set; }

        [JsonProperty("v02c")]
        public string V02c { get; set; }
    }

    public partial class List
    {
        [JsonProperty("Choices")]
        public Choices Choices { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("hoverTextDictionary")]
        public Choices HoverTextDictionary { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("LabelText")]
        public string LabelText { get; set; }

        [JsonProperty("Selection")]
        public string Selection { get; set; }

        [JsonProperty("SelectionIndex")]
        public long? SelectionIndex { get; set; }

        [JsonProperty("type")]
        public long? Type { get; set; }
    }

    public partial class ModManifest
    {
        [JsonProperty("Author")]
        public string Author { get; set; }

        [JsonProperty("Dependencies")]
        public Dependency[] Dependencies { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("EntryDll")]
        public string EntryDll { get; set; }

        [JsonProperty("MinimumApiVersion")]
        public object MinimumApiVersion { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("UniqueID")]
        public string UniqueID { get; set; }

        [JsonProperty("UpdateKeys")]
        public object UpdateKeys { get; set; }

        [JsonProperty("Version")]
        public Version Version { get; set; }
    }

    public partial class Version
    {
        [JsonProperty("Build")]
        public object Build { get; set; }

        [JsonProperty("MajorVersion")]
        public long MajorVersion { get; set; }

        [JsonProperty("MinorVersion")]
        public long MinorVersion { get; set; }

        [JsonProperty("PatchVersion")]
        public long PatchVersion { get; set; }
    }

    public partial class Dependency
    {
        [JsonProperty("IsRequired")]
        public bool IsRequired { get; set; }

        [JsonProperty("MinimumVersion")]
        public object MinimumVersion { get; set; }

        [JsonProperty("UniqueID")]
        public string UniqueID { get; set; }
    }

    public partial class ParsBundles
    {
        public static ParsBundles[] FromJson(string json) => JsonConvert.DeserializeObject<ParsBundles[]>(json, Converter.Settings);
    }

    public partial class CCBOConfigParser
    {
        public static CCBOConfigParser FromJson(string json) => JsonConvert.DeserializeObject<CCBOConfigParser>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ParsBundles[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this CCBOConfigParser self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
