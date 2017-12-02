using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewConfigFramework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using CommunityCenterBundleOverhaul.Framework;
using StardewValley.Menus;

namespace CommunityCenterBundleOverhaul
{
    /// <summary>The mod entry point.</summary>
    public class CommunityCenterBundleOverhaul : Mod
    {
        // Initialising Variables
        internal static IModSettingsFramework Settings;
        internal ModOptionSelection dropDown;
        public String lang;
        public ITranslationHelper i18n;

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            bool isLoaded = this.Helper.ModRegistry.IsLoaded("Juice805.StardewConfigMenu");
            lang = helper.Translation.Locale;
            i18n = helper.Translation;
            
            if (!isLoaded)
            {
                this.Monitor.Log("Initialisation failed because StardewConfigMenu seems not to be correctly installed. This Mod will now do nothing.", LogLevel.Error);
            }
            else
            {
                this.Monitor.Log("Initialisation finished. Bundels are now out of control :D", LogLevel.Info);
                
                Settings = IModSettingsFramework.Instance;
                var options = ModOptions.LoadUserSettings(this);
                Settings.AddModOptions(options);

                String original = "";
                String original_desc = "";
                String v02 = "";
                String v02_desc = "";
                String v02a = "";
                String v02a_desc = "";
                String v02b = "";
                String v02b_desc = "";
                String v02c = "";
                String v02c_desc = "";
                String okButton = "";

                var list = new ModSelectionOptionChoices();
                list.Add("default", getTranslation(helper, "default", original));
                list.Add("v02", getTranslation(helper, "v02", v02));
                list.Add("v02a", getTranslation(helper, "v02a", v02a));
                list.Add("v02b", getTranslation(helper, "v02b", v02b));
                list.Add("v02c", getTranslation(helper, "v02c", v02c));

                dropDown = options.GetOptionWithIdentifier<ModOptionSelection>("bundle") ?? new ModOptionSelection("bundle", "Bundels", list, 0, true);
                options.AddModOption(dropDown);

                dropDown.hoverTextDictionary = new Dictionary<string, string>
                {
                    {"default", getTranslation(helper, "default.desc", original_desc)},
                    {"v02", getTranslation(helper, "v02.desc", v02_desc)},
                    {"v02a", getTranslation(helper, "v02a.desc", v02a_desc)},
                    {"v02b", getTranslation(helper, "v02b.desc", v02b_desc)},
                    {"v02c", getTranslation(helper, "v02c.desc", v02c_desc)},
                };                            

                var saveButton = new ModOptionTrigger("okButton", getTranslation(helper, "okButton", okButton), OptionActionType.OK);
                options.AddModOption(saveButton);

                saveButton.ActionTriggered += (id) => {
                    this.Monitor.Log("[CCBO] Changing Bundle ...");

                    options.SaveUserSettings();

                    this.Monitor.Log(lang);

                    invalidateCache(this.Helper);
                    this.Helper.Content.AssetEditors.Add(new BundleEditor(this, this.Helper, dropDown));
                    Game1.addHUDMessage(new HUDMessage("Changed Community Center Bundle to: " + dropDown.Selection, 3) { noIcon = true, timeLeft = HUDMessage.defaultTime });
                    this.Monitor.Log("[CCBO] Bundle changed successfully. If smth. is missing, you must restart your game.");
                };
                SaveEvents.AfterLoad += SaveEvents_AfterLoad;
            }
            
        }

        private void invalidateCache(IModHelper helper)
        {
            string bundleXnb = "Data\\Bundle.xnb";
            string JunimoNoteXnb = "LooseSprites\\JunimoNote.xnb";

            this.Monitor.Log(bundleXnb + " " + JunimoNoteXnb);

            helper.Content.InvalidateCache(bundleXnb);
            helper.Content.InvalidateCache(JunimoNoteXnb);
        }

        /*********
        ** Private methods
        *********/
        private String getTranslation(IModHelper helper, String identifier, String var1)
        {
            var1 = helper.Translation.Get(identifier);
            return var1;
        }      

        private void SaveEvents_AfterLoad(object sender, EventArgs e)
        {
            if (Context.IsWorldReady)
            {
                this.Helper.Content.AssetEditors.Add(new ImageEditor(this, this.Helper, dropDown));
                this.Helper.Content.AssetEditors.Add(new BundleEditor(this, this.Helper, dropDown));
            }
        }
    }
}
