using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace Refurbishment.Harmony
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    [BepInDependency(LethalLevelLoader.Plugin.ModGUID)]
    public class Plugin : BaseUnityPlugin
    {
        public const string ModGUID = "IAmBatby.Refurbishment";
        public const string ModName = "Refurbishment";
        public const string ModVersion = "1.0.0.0";

        public static Plugin Instance;

        internal static readonly HarmonyLib.Harmony Harmony = new HarmonyLib.Harmony(ModGUID);

        public static AssetBundle AssetBundle;

        internal static ManualLogSource logger;

        private void Awake()
        {
            logger = Logger;

            Logger.LogMessage("Succesfully Loaded Refurbishment!");
        }

        public static void DebugLog(string log)
        {
            logger.LogInfo(log);
        }

        public static void DebugLogError(string log)
        {
            logger.LogError(log);
        }
    }
}