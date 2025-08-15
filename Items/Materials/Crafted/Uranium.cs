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

namespace RoyalCommonalities.Items.Materials.Crafted
{
    class Uranium
    {
        //TechType
        public static PrefabInfo Info { get; private set; }

        public static void Register()
        {
            Info = PrefabInfo.WithTechType("royaluranium", "Uranium", "Radioactive material usualy used in nuclear reactors. Don't lick it.").WithIcon(SpriteManager.Get(TechType.Uranium));
            var UraniumPrefab = new CustomPrefab(Info);

            var UraniumObj = new CloneTemplate(Info, TechType.Uranium);
            UraniumPrefab.SetGameObject(UraniumObj);

            var recipe = new RecipeData(
                new Ingredient(TechType.UraniniteCrystal, 2)
                )
            { craftAmount = 1 };


            UraniumPrefab.SetRecipe(recipe)
                .WithFabricatorType(AdvancedCraftingStation.TreeType)
                .WithStepsToFabricatorTab(CraftTreeHandler.rootRCConvertTab)
                .WithCraftingTime(2f)
                ;

            //Unlocks at start ^-^
            //You don't have to put this here i think but i do it here.
            //KnownTechHandler.UnlockOnStart(Uranium.Info.TechType);
            //KnownTech.AnalysisTech(TechType.UraniniteCrystal);

            // register to the game
            UraniumPrefab.Register();
        }
    }
}