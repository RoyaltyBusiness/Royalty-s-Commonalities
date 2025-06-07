using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nautilus.Crafting;
using static CraftData;
using Nautilus.Assets.Gadgets;
using RoyalCommonalities;
using static PerformanceConsoleCommands;
using Nautilus.Handlers;
using Nautilus.Utility;
using Nautilus.Extensions;
using UnityEngine;
using RoyalCommonalities.Buildables.Crafting;

namespace RoyalCommonalities.Items.Materials
{
    public class EnhancedWiringKit
    {
        //TechType
        public static PrefabInfo Info { get; private set; }

        public static void Register()
        {
            Info = PrefabInfo.WithTechType("enhancedwiringkit", "Enhanced wiring kit", "wiring kit with enchanced functionalitiy").WithIcon(Plugin.Bundle.LoadAsset<Sprite>("EnhancedWiringKit"));
            var enhancedwiringkitPrefab = new CustomPrefab(Info);


            // The model
            var enhancedwiringkitObj = new CloneTemplate(Info, TechType.AdvancedWiringKit);
            enhancedwiringkitPrefab.SetGameObject(enhancedwiringkitObj);

            var recipe = new RecipeData(
                new Ingredient(TechType.WiringKit, 1),
                new Ingredient(TechType.Lithium, 1),
                new Ingredient(Ionite.Info.TechType, 1)
                ){ craftAmount = 1 };


            enhancedwiringkitPrefab.SetRecipe(recipe)
                .WithFabricatorType(AdvancedCraftingStation.TreeType)
                .WithStepsToFabricatorTab(CraftTreeHandler.rootRCPrecursorTab);

            //Unlocks at start ^-^
            //You don't have to put this here i think but i do it here.
            KnownTechHandler.UnlockOnStart(EnhancedWiringKit.Info.TechType);

            // register to the game
            enhancedwiringkitPrefab.Register();
        }
    }
}
