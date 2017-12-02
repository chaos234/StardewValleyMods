using System.Linq;
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
            // get bundle
            ParsBundles[] data = this.Helper.ReadJsonFile<ParsBundles[]>(@"bundles\bundles.json");
            ParsBundles bundle = data.FirstOrDefault(p => p.ID == this.DropDown.SelectionIndex);
            if (bundle == null)
                return;

            // edit asset
            foreach (Content content in bundle.Content)
            {
                if (!content.Key.Contains("Vault"))
                {
                    string translation = this.Mod.Translations.Get(content.BundleName);
                    this.Mod.Monitor.Log($"[{content.Key}] = {content.BundleName}{content.BundleContent}/{translation}");
                    asset.AsDictionary<string, string>().Set(content.Key, content.BundleName + content.BundleContent + "/" + translation);
                }
                else
                {
                    this.Mod.Monitor.Log($"[{content.Key}] = {content.BundleName}{content.BundleContent}");
                    asset.AsDictionary<string, string>().Set(content.Key, content.BundleName + content.BundleContent);
                }
            }
        }
    }
}
