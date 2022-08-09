using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Plugins
{
    [Info("NoTCBlockEntity", "Agamemnon", "1.0.0")]
    [Description("Blocks players from placing certain entities unless they are in the vicinity of a Tool Cupboard.")]
    class NoTCBlockEntity : RustPlugin
    {
        private ConfigData _configData;
        private const string _permBypass = "notcblockentity.bypass";

        #region Oxide Hooks
        private void OnServerInitialized()
        {
            permission.RegisterPermission(_permBypass, this);

            if (!LoadConfigVariables())
            {
                PrintError("ERROR: The config file is corrupt. Either fix or delete it and restart the plugin.");
                PrintError("ERROR: Unloading plugin.");
                Interface.Oxide.UnloadPlugin(this.Title);
                return;
            }

        }

        private object CanBuild(Planner planner, Construction prefab, Construction.Target target)
        {
            if (!permission.UserHasPermission(planner.GetOwnerPlayer().UserIDString, _permBypass))
            {
                var cupboard = planner.GetOwnerPlayer().GetBuildingPrivilege();
                if (cupboard == null)
                {
                    string shortname = GetShortname(prefab.fullName);
                    shortname = Regex.Replace(shortname, ".deployed", "");
                    shortname = Regex.Replace(shortname, "_deployed", "");
    
                    foreach (string entity in _configData.BlockedEntities)
                    {
                        if (String.Equals(shortname, entity))  
                        {
                            SendReply(planner.GetOwnerPlayer(), Lang("BlockedMessage",
                                new KeyValuePair<string, string>("entity", entity)), _configData.ChatIcon);
                            return false;
                        }
                    }
                }
            }
            return null;
        }
        #endregion

        #region Helper Functions
        private static string GetShortname(string original)
        {
            var index = original.LastIndexOf("/", StringComparison.Ordinal) + 1;
            var name = original.Substring(index);
            return name.Replace(".prefab", string.Empty);
        }
        #endregion

        #region Configuration
        private class ConfigData
        {
            [JsonProperty(PropertyName = "Chat Icon (SteamID)")]
            public ulong ChatIcon { get; private set; } = 0;

            [JsonProperty(PropertyName = "Entities to Block (use shortnames)")]
            public List<string> BlockedEntities {get; private set; }

            public static ConfigData CreateDefault()
            {
                return new ConfigData
                {
                    BlockedEntities = new List<string>
                    {
                        "autoturret",
                        "bbq",
                        "mixingtable"
                    }
                };
            }
        }

        private bool LoadConfigVariables()
        {
            try
            {
                _configData = Config.ReadObject<ConfigData>();
            }
            catch
            {
                return false;
            }

            SaveConfig(_configData);
            return true;
        }

        protected override void LoadDefaultConfig()
        {
            PrintWarning("Creating new config file.");
            _configData = ConfigData.CreateDefault();
            SaveConfig(_configData);
        }

        private void SaveConfig(ConfigData config)
        {
            Config.WriteObject(config, true);
        }
        #endregion

        #region Language
        protected override void LoadDefaultMessages()
        {
            lang.RegisterMessages(new Dictionary<string, string>
            {
                ["BlockedMessage"] = "<color=#ff6969>WARNING:</color> You must be within range of a <color=#ffa500>Tool Cupboard</color> to place a <color=#ffa500>{entity}</color>!"

            }, this);
            lang.RegisterMessages(new Dictionary<string, string>
            {
                ["BlockedMessage"] = "<color=#ff6969>ATTENTION:</color> Vous devez être à portée d'une <color=#ffa500>Armoire à Outils</color> pour placer une <color=#ffa500>{entity}</color>!"
            }, this, "fr");
        }

        private string Lang(string key) => string.Format(lang.GetMessage(key, this));
        private string Lang(string key, params KeyValuePair<string, string>[] replacements)
        {
            var message = lang.GetMessage(key, this);

            foreach (var replacement in replacements)
                message = message.Replace($"{{{replacement.Key}}}", replacement.Value);

            return message;
        }
        #endregion
    }
}
