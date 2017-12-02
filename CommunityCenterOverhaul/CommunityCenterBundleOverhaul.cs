using System;
using System.Collections.Generic;
using CommunityCenterBundleOverhaul.Framework;
using StardewConfigFramework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace CommunityCenterBundleOverhaul
{
    /// <summary>The mod entry point.</summary>
    public class CommunityCenterBundleOverhaul : Mod
    {
        /*********
        ** Properties
        *********/
        internal static IModSettingsFramework Settings { get; private set; }
        internal ModOptionSelection DropDown { get; private set; }
        public string Locale { get; private set; }
        public ITranslationHelper Translations { get; private set; }


        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            bool isLoaded = this.Helper.ModRegistry.IsLoaded("Juice805.StardewConfigMenu");
            this.Locale = helper.Translation.Locale;
            this.Translations = helper.Translation;

            if (!isLoaded)
            {
                this.Monitor.Log("Initialisation failed because StardewConfigMenu seems not to be correctly installed. This Mod will now do nothing.", LogLevel.Error);
                return;
            }
            this.Monitor.Log("Initialisation finished. Bundels are now out of control :D", LogLevel.Info);

            CommunityCenterBundleOverhaul.Settings = IModSettingsFramework.Instance;
            var options = ModOptions.LoadUserSettings(this);
            CommunityCenterBundleOverhaul.Settings.AddModOptions(options);

            var list = new ModSelectionOptionChoices
            {
                {"default", GetTranslation(helper, "default", "")},
                {"v02", GetTranslation(helper, "v02", "")},
                {"v02a", GetTranslation(helper, "v02a", "")},
                {"v02b", GetTranslation(helper, "v02b", "")},
                {"v02c", GetTranslation(helper, "v02c", "")}
            };

            this.DropDown = options.GetOptionWithIdentifier<ModOptionSelection>("bundle") ?? new ModOptionSelection("bundle", "Bundels", list);
            options.AddModOption(this.DropDown);

            this.DropDown.hoverTextDictionary = new Dictionary<string, string>
            {
                {"default", GetTranslation(helper, "default.desc", "")},
                {"v02", GetTranslation(helper, "v02.desc", "")},
                {"v02a", GetTranslation(helper, "v02a.desc", "")},
                {"v02b", GetTranslation(helper, "v02b.desc", "")},
                {"v02c", GetTranslation(helper, "v02c.desc", "")},
            };

            var saveButton = new ModOptionTrigger("okButton", GetTranslation(helper, "okButton", ""), OptionActionType.OK);
            options.AddModOption(saveButton);

            saveButton.ActionTriggered += (id) =>
            {
                this.Monitor.Log("[CCBO] Changing Bundle ...");

                options.SaveUserSettings();

                this.Monitor.Log(this.Locale);

                InvalidateCache(this.Helper);
                this.Helper.Content.AssetEditors.Add(new BundleEditor(this, this.Helper, this.DropDown));
                Game1.addHUDMessage(new HUDMessage("Changed Community Center Bundle to: " + this.DropDown.Selection, 3) { noIcon = true, timeLeft = HUDMessage.defaultTime });
                this.Monitor.Log("[CCBO] Bundle changed successfully. If smth. is missing, you must restart your game.");
            };
            SaveEvents.AfterLoad += SaveEvents_AfterLoad;
        }


        /*********
        ** Private methods
        *********/
        private void SaveEvents_AfterLoad(object sender, EventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            this.Helper.Content.AssetEditors.Add(new ImageEditor(this, this.Helper, this.DropDown));
            this.Helper.Content.AssetEditors.Add(new BundleEditor(this, this.Helper, this.DropDown));
        }

        private void InvalidateCache(IModHelper helper)
        {
            string bundleXnb = "Data\\Bundle.xnb";
            string JunimoNoteXnb = "LooseSprites\\JunimoNote.xnb";

            this.Monitor.Log(bundleXnb + " " + JunimoNoteXnb);

            helper.Content.InvalidateCache(bundleXnb);
            helper.Content.InvalidateCache(JunimoNoteXnb);
        }

        private string GetTranslation(IModHelper helper, string identifier, string var1)
        {
            var1 = helper.Translation.Get(identifier);
            return var1;
        }
    }
}
