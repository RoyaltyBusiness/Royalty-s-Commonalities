﻿using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Nautilus.Utility;
using RoyalCommonalities;
using RoyalCommonalities.Buildables.Crafting;
using RoyalCommonalities.Items.Materials;
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
            AdvancedCraftingStation.CreateAndRegister();
            CraftTreeHandler.AddFabricatorMenus();
            ChromaticIngot.Register();
            EnhancedWiringKit.Register();
            LaminatedGlass.Register();
            ModularHullPiece.Register();
            
        }
    }
}