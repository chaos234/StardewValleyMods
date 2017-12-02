using Microsoft.Xna.Framework.Graphics;
using StardewConfigFramework;
using StardewModdingAPI;

namespace CommunityCenterBundleOverhaul.Framework
{
    internal class ImageEditor : IAssetEditor
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
        public ImageEditor(CommunityCenterBundleOverhaul communityCenterBundleOverhaul, IModHelper helper, ModOptionSelection dropDown)
        {
            this.Mod = communityCenterBundleOverhaul;
            this.Helper = helper;
            this.DropDown = dropDown;
        }

        public bool CanEdit<T>(IAssetInfo asset)
        {
            return this.DropDown.SelectionIndex != 0 && asset.AssetNameEquals(@"LooseSprites\JunimoNote");
        }

        public void Edit<T>(IAssetData asset)
        {
            this.GetBundleTexturefromJson(this.DropDown.SelectionIndex, asset, this.Mod.Locale);
        }


        /*********
        ** Private methods
        *********/
        private void GetBundleTexturefromJson(int selInd, IAssetData asset, string lng)
        {
            string ext = "png";
            string fName = "JunimoNote";

            string bundelsJson = System.IO.File.ReadAllText(this.Mod.Helper.DirectoryPath + "\\bundles\\bundles.json");
            var data = ParsBundles.FromJson(bundelsJson);

            string image = "";

            foreach (var key in data)
            {
                if (key.ID == selInd)
                {
                    var bundleName = key.Name;
                    image = lng != "en-en" ? $"{fName}.{lng}.{bundleName}.{ext}" : $"{fName}.{bundleName}.{ext}";
                    this.Mod.Monitor.Log(image);
                }
            }

            switch (selInd)
            {
                case 0:
                    switch (this.Mod.Locale)
                    {
                        case "en-en":
                            this.InvalidateImage(null, asset);
                            break;
                        case "de-de":
                            this.InvalidateImage("de-DE", asset);
                            break;
                        case "ja-jp":
                            this.InvalidateImage("ja-JP", asset);
                            break;
                        case "es-es":
                            this.InvalidateImage("es-ES", asset);
                            break;
                        case "pt-br":
                            this.InvalidateImage("pt-BR", asset);
                            break;
                        case "ru-ru":
                            this.InvalidateImage("ru-RU", asset);
                            break;
                    }
                    break;
                case 1:
                case 4:
                    Texture2D cT = this.Mod.Helper.Content.Load<Texture2D>("bundles\\images\\" + image);
                    asset
                        .AsImage()
                        .PatchImage(cT);
                    break;
                case 3:
                case 2:
                    Texture2D cT2 = this.Mod.Helper.Content.Load<Texture2D>("bundles\\images\\" + image);
                    asset
                        .AsImage()
                        .PatchImage(cT2);
                    break;
            }
        }

        private void InvalidateImage(string v, IAssetData asset)
        {

            if (v != null)
            {
                Texture2D cT1 = this.Mod.Helper.Content.Load<Texture2D>(@"LooseSprites\\JunimoNote." + v + ".xnb", ContentSource.GameContent);
                asset
                    .AsImage()
                    .PatchImage(cT1);
            }
            else
            {
                Texture2D cT1 = this.Mod.Helper.Content.Load<Texture2D>(@"LooseSprites\\JunimoNote.xnb", ContentSource.GameContent);
                asset
                    .AsImage()
                    .PatchImage(cT1);
            }

        }
    }
}