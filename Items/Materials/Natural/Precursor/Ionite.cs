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
using Story;

namespace RoyalCommonalities.Items.Materials
{
    class Ionite
    {
        //TechType
        public static PrefabInfo Info { get; private set; }
        public PDAEncyclopedia.EntryData EncyclopediaEntryData { get; set; }

        public static void Register()
        {
            Info = PrefabInfo.WithTechType("ionite", "Ionite", "High capacity alien energy conductor").WithIcon(Plugin.Bundle.LoadAsset<Sprite>("Ionite"));
            var ionitePrefab = new CustomPrefab(Info);

            // The model
            var ioniteObj = new CloneTemplate(Info, TechType.Kyanite);
            ionitePrefab.SetGameObject(ioniteObj);

            var recipe = new RecipeData(
                new Ingredient(TechType.PrecursorIonCrystal, 1),
                new Ingredient(TechType.Quartz, 4)
                )
            { craftAmount = 4 };
            ionitePrefab.SetRecipe(recipe)

                .WithFabricatorType(CraftTree.Type.Fabricator)
                .WithStepsToFabricatorTab("Resources", "BasicMaterials");



            //Unlocks at start ^-^
            //You don't have to put this here i think but i do it here.
            //ScanningGadget.requiredForUnlock(Ionite.Info.TechType);
            KnownTechHandler.UnlockOnStart(Ionite.Info.TechType);


            // register to the game
            ionitePrefab.Register();

        }
    }
}