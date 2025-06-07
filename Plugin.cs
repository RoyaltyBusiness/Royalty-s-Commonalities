using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Nautilus.Utility;
using RoyalCommonalities;
using RoyalCommonalities.Buildables.Crafting;
using RoyalCommonalities.Items.Materials;
using RoyalCommonalities.Items.Materials.Natural;
using RoyalCommonalities.WorldObjects.Precursor.Materials.Deposit;
using System;
using System.Reflection;
using UnityEngine;

namespace RoyalCommonalities
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.snmodding.nautilus")]
    public class Plugin : BaseUnityPlugin
    {
        public new static ManualLogSource Logger { get; private set; }

        private static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();
        //here is bundle. bundle contains all ur stuff.
        public static AssetBundle Bundle { get; } = AssetBundleLoadingUtils.LoadFromAssetsFolder(Assembly, "royalassets");


        private void Awake()
        {
            // set project-scoped logger instance
            Logger = base.Logger;

            // Initialize custom prefabs
            InitializePrefabs();

            // register harmony patches, if there are any
            Harmony.CreateAndPatchAll(Assembly, $"{PluginInfo.PLUGIN_GUID}");
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
        private void InitializePrefabs()
        {
            //REMEMBER
            //register things, that require other things, before the other things, or else it won't work.
            //usualy you put crafting station and craft tree handler above everything. with crafting station above since tree hander uses it.
            //but advanced crafting station uses platinum in its recipe so it needs to be above it.

            //don't make custom requirements be crafted at the station, when the station needs that thing to be crafted, or else ur code will eat itself :b

            Platinum.Register();

            AdvancedCraftingStation.CreateAndRegister();
            CraftTreeHandler.AddFabricatorMenus();

            Ionite.Register();

            ChromaticIngot.Register();
            EnhancedWiringKit.Register();
            LaminatedGlass.Register();

            HeavyModularHullPiece.Register();
            ModularHullPiece.Register();
            LightModularHullPiece.Register();

            DrillableIonite.Register();
            DrillablePlatinum.Register();
        }
    }
}