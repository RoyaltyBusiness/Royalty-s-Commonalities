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
    public class LaminatedGlass
    {
        //TechType
        public static PrefabInfo Info { get; private set; }

        public static void Register()
        {
            Info = PrefabInfo.WithTechType("laminatedglass", "Laminated Glass", "Enchanced glass with distinctive transparency from one of its sides").WithIcon(Plugin.Bundle.LoadAsset<Sprite>("LaminatedGlass2"));
            var laminatedglassPrefab = new CustomPrefab(Info);

            // The model
            var laminatedObj = new CloneTemplate(Info, TechType.EnameledGlass);
            laminatedglassPrefab.SetGameObject(laminatedObj);

            var recipe = new RecipeData(
                new Ingredient(TechType.Glass, 2),
                new Ingredient(TechType.Gold, 2),
                new Ingredient(TechType.PrecursorIonCrystal, 1)
                );
            laminatedglassPrefab.SetRecipe(recipe)

                .WithFabricatorType(AdvancedCraftingStation.TreeType)
                .WithStepsToFabricatorTab(CraftTreeHandler.rootRCPrecursorTab);

            //Unlocks at start ^-^
            //You don't have to put this here i think but i do it here.
            KnownTechHandler.UnlockOnStart(LaminatedGlass.Info.TechType);

            // register to the game
            laminatedglassPrefab.Register();

        }
    }
}
