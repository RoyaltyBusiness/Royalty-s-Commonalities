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
    public class HeavyModularHullPiece
    {
        //TechType
        public static PrefabInfo Info { get; private set; }

        public static TechType Register()
        {
            Info = PrefabInfo.WithTechType("heavymodularhullpeace", "Heavy Modular Hull Piece", "Modular hull peace, made for the pourpose of building large armored vehicle hulls.").WithIcon(SpriteManager.Get(TechType.CyclopsHullBlueprint));
            var HeavyModularHullPiecePrefab = new CustomPrefab(Info);



            // The model of our coal will use the same one as Nickel's. (edited to Titanium)
            var HeavyModularHullPieceObj = new CloneTemplate(Info, TechType.CyclopsHullFragment);
            HeavyModularHullPiecePrefab.SetGameObject(HeavyModularHullPieceObj);

            var recipe = new RecipeData(
                new Ingredient(TechType.TitaniumIngot, 4),
                new Ingredient(TechType.Lead, 5),
                new Ingredient(TechType.Lithium, 2)
                );

            HeavyModularHullPiecePrefab.SetRecipe(recipe)
                .WithFabricatorType(AdvancedCraftingStation.TreeType)
                .WithStepsToFabricatorTab(CraftTreeHandler.rootRCVehicleIngredientsTab);

            //Unlocks at start ^-^
            //You don't have to put this here i think but i do it here.
            KnownTechHandler.UnlockOnStart(HeavyModularHullPiece.Info.TechType);

            // register to the game
            HeavyModularHullPiecePrefab.Register();
            return Info.TechType;
        }
    }
}