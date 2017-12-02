using System.Collections.Generic;
using StardewConfigFramework;
using StardewModdingAPI;

namespace CommunityCenterBundleOverhaul.Framework
{
    internal class BundleEditor : IAssetEditor
    {
        /*********
        ** Properties
        *********/
        private readonly IModHelper Helper;
        private readonly ModOptionSelection DropDown;
        private readonly CommunityCenterBundleOverhaul Mod;


        /*********
        ** Public methods
        *********/
        public BundleEditor(CommunityCenterBundleOverhaul communityCenterBundleOverhaul, IModHelper helper, ModOptionSelection dropDown)
        {
            this.Mod = communityCenterBundleOverhaul;
            this.Helper = helper;
            this.DropDown = dropDown;
        }

        public bool CanEdit<T>(IAssetInfo asset)
        {
            return asset.AssetNameEquals(@"Data\Bundles");
        }

        public void Edit<T>(IAssetData asset)
        {
            string bundelsJson = System.IO.File.ReadAllText(this.Mod.Helper.DirectoryPath + "\\bundles\\bundles.json");
            var data = ParsBundles.FromJson(bundelsJson);

            foreach (var key in data)
            {
                if (key.ID == this.DropDown.SelectionIndex)
                {
                    // Create new dictionary to replace the data bundles.xnb asset
                    Dictionary<string, string> bundle = new Dictionary<string, string>();
                    foreach (var key2 in key.Content)
                    {
                        if (!key2.Key.Contains("Vault"))
                        {
                            string translation = this.Mod.Translations.Get(key2.BundleName);
                            this.Mod.Monitor.Log("[" + key2.Key + "] = " + key2.BundleName + key2.BundleContent + "/" + translation);
                            asset.AsDictionary<string, string>().Set(key2.Key, key2.BundleName + key2.BundleContent + "/" + translation);
                        }
                        else
                        {
                            this.Mod.Monitor.Log("[" + key2.Key + "] = " + key2.BundleName + key2.BundleContent);
                            asset.AsDictionary<string, string>().Set(key2.Key, key2.BundleName + key2.BundleContent);
                        }
                    }
                }
            }
        }
    }
}