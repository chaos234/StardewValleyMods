using StardewConfigFramework;
using StardewModdingAPI;
using System;
using System.Collections.Generic;

namespace CommunityCenterBundleOverhaul
{
    internal class CCBOBundlesEditor : IAssetEditor
    {
        private IModHelper helper;
        private ModOptionSelection dropDown;

        private CommunityCenterBundleOverhaul ccbo;

        public CCBOBundlesEditor(CommunityCenterBundleOverhaul communityCenterBundleOverhaul, IModHelper helper, ModOptionSelection dropDown)
        {
            this.ccbo = communityCenterBundleOverhaul;
            this.helper = helper;
            this.dropDown = dropDown;
        }

        public bool CanEdit<T>(IAssetInfo asset)
        {
            return asset.AssetNameEquals(@"Data\Bundles");
        }

        public void Edit<T>(IAssetData asset)
        {
            string bundelsJson = System.IO.File.ReadAllText(ccbo.Helper.DirectoryPath + "\\bundles\\bundles.json");
            var data = ParsBundles.FromJson(bundelsJson);

            foreach (var key in data)
            {
                if (key.ID == dropDown.SelectionIndex)
                {
                    // Create new dictionary to replace the data bundles.xnb asset
                    Dictionary<string, string> bundle = new Dictionary<string, string>();
                    foreach (var key2 in key.Content)
                    {
                        if (!key2.Key.Contains("Vault"))
                        {
                            string translation = ccbo.i18n.Get(key2.BundleName);
                            ccbo.Monitor.Log("[" + key2.Key + "] = " + key2.BundleName + key2.BundleContent + "/" + translation);
                            asset.AsDictionary<string, string>().Set(key2.Key, key2.BundleName + key2.BundleContent + "/" + translation);
                        }
                        else
                        {
                            ccbo.Monitor.Log("[" + key2.Key + "] = " + key2.BundleName + key2.BundleContent);
                            asset.AsDictionary<string, string>().Set(key2.Key, key2.BundleName + key2.BundleContent);
                        }
                    }
                }
            }
        }
    }
}