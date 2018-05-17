﻿using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;

namespace CommunityCenterBundleOverhaul_SDV_13.Framework
{
    internal class ImageEditor : IAssetEditor
    {
        /*********
        ** Properties
        *********/
        private readonly IModHelper Helper;
        private readonly IMonitor Monitor;
        private readonly ModConfig Config;


        /*********
        ** Public methods
        *********/
        public ImageEditor(IModHelper helper, IMonitor monitor, ModConfig config)
        {
            this.Monitor = monitor;
            this.Helper = helper;
            this.Config = config;
        }

        public bool CanEdit<T>(IAssetInfo asset)
        {
            return asset.AssetNameEquals(@"LooseSprites\JunimoNote");
        }

        public void Edit<T>(IAssetData asset)
        {
            this.GetBundleTexturefromJson(this.Config.SelectionID, asset, this.Helper.Translation.Locale);
        }


        /*********
        ** Private methods
        *********/
        private void GetBundleTexturefromJson(int selInd, IAssetData asset, string locale)
        {
            // get bundle
            Bundle[] data = this.Helper.ReadJsonFile<Bundle[]>(@"bundles\bundles.json");
            Bundle bundle = data.FirstOrDefault(p => p.ID == selInd);
            if (bundle == null)
                return;

            // edit asset
            switch (selInd)
            {
                case 0:
                    {
                        string filenameLocale = this.GetFilenameLocale(locale);
                        string filename = filenameLocale != null
                            ? $@"LooseSprites\\JunimoNote.{filenameLocale}.xnb"
                            : @"LooseSprites\\JunimoNote.xnb";
                        Texture2D texture = this.Helper.Content.Load<Texture2D>(filename, ContentSource.GameContent);
                        asset.AsImage().PatchImage(texture);
                    }
                    break;

                case 1:
                case 2:
                case 3:
                case 4:
                    {
                        string filename = locale != "en-en"
                            ? $"JunimoNote.{locale}.{bundle.Name}.png"
                            : $"JunimoNote.{bundle.Name}.png";
                        this.Monitor.Log(filename);
                        Texture2D texture = this.Helper.Content.Load<Texture2D>($@"bundles\images\{filename}");
                        asset.AsImage().PatchImage(texture);
                    }
                    break;
            }
        }

        private string GetFilenameLocale(string locale)
        {
            switch (locale)
            {
                case "en-en":
                    return null;
                case "de-de":
                    return "de-DE";
                case "ja-jp":
                    return "ja-JP";
                case "es-es":
                    return "es-ES";
                case "pt-br":
                    return "pt-BR";
                case "ru-ru":
                    return "ru-RU";
                default:
                    return null;
            }
        }
    }
}
