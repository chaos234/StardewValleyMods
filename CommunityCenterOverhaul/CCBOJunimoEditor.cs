using Microsoft.Xna.Framework.Graphics;
using StardewConfigFramework;
using StardewModdingAPI;
using System;

namespace CommunityCenterBundleOverhaul
{
    internal class CCBOJunimoEditor : IAssetEditor
    {
        private IModHelper helper;
        private ModOptionSelection dropDown;
        private CommunityCenterBundleOverhaul ccbo;

        public CCBOJunimoEditor(CommunityCenterBundleOverhaul communityCenterBundleOverhaul, IModHelper helper, ModOptionSelection dropDown)
        {
            this.ccbo = communityCenterBundleOverhaul;
            this.helper = helper;
            this.dropDown = dropDown;
        }

        public bool CanEdit<T>(IAssetInfo asset)
        {
            if (dropDown.SelectionIndex == 0)
            {
                return false;
            }
            else
            {
                return asset.AssetNameEquals(@"LooseSprites\JunimoNote");
            }
        }

        public void Edit<T>(IAssetData asset)
        {
            getBundleTextureFromXML(dropDown.SelectionIndex, asset, ccbo.lang);
        }

        private void getBundleTextureFromXML(int selInd, IAssetData asset, String lng)
        {
            String ext = "png";
            String fName = "JunimoNote";

            String bundelsJson = System.IO.File.ReadAllText(ccbo.Helper.DirectoryPath + "\\bundles\\bundles.json");
            var data = ParsBundles.FromJson(bundelsJson);

            String image = "";

            foreach (var key in data)
            {
                if (key.ID == selInd)
                {
                    var bundleName = key.Name;
                    image = lng != "en-en" ? $"{fName}.{lng}.{bundleName}.{ext}" : $"{fName}.{bundleName}.{ext}";
                    ccbo.Monitor.Log(image);
                }
            }

            switch (selInd)
            {
                case 0:
                    switch (ccbo.lang)
                    {
                        case "en-en":
                            invalidateImage(null, asset);
                            break;
                        case "de-de":
                            invalidateImage("de-DE", asset);
                            break;
                        case "ja-jp":
                            invalidateImage("ja-JP", asset);
                            break;
                        case "es-es":
                            invalidateImage("es-ES", asset);
                            break;
                        case "pt-br":
                            invalidateImage("pt-BR", asset);
                            break;
                        case "ru-ru":
                            invalidateImage("ru-RU", asset);
                            break;
                    }
                    break;
                case 1:
                case 4:
                    Texture2D cT = ccbo.Helper.Content.Load<Texture2D>("bundles\\images\\" + image, ContentSource.ModFolder);
                    asset
                        .AsImage()
                        .PatchImage(cT);
                    break;
                case 3:
                case 2:
                    Texture2D cT2 = ccbo.Helper.Content.Load<Texture2D>("bundles\\images\\" + image, ContentSource.ModFolder);
                    asset
                        .AsImage()
                        .PatchImage(cT2);
                    break;
                default:
                    break;

            }
        }

        private void invalidateImage(string v, IAssetData asset)
        {

            if (v != null)
            {
                Texture2D cT1 = ccbo.Helper.Content.Load<Texture2D>(@"LooseSprites\\JunimoNote." + v + ".xnb", ContentSource.GameContent);
                asset
                    .AsImage()
                    .PatchImage(cT1);
            }
            else
            {
                Texture2D cT1 = ccbo.Helper.Content.Load<Texture2D>(@"LooseSprites\\JunimoNote.xnb", ContentSource.GameContent);
                asset
                    .AsImage()
                    .PatchImage(cT1);
            }

        }
    }
}