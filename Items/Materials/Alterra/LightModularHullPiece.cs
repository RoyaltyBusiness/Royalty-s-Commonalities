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
    public class LightModularHullPiece
    {
        //TechType
        public static PrefabInfo Info { get; private set; }

        public static TechType Register()
        {
            Info = PrefabInfo.WithTechType("lightmodularhullpeace", "Light Modular Hull Piece", "Modular hull peace, made for the pourpose of building hulls for light vehicles.").WithIcon(SpriteManager.Get(TechType.CyclopsHullBlueprint));
            var LightModularHullPiecePrefab = new CustomPrefab(Info);



            // The model of our coal will use the same one as Nickel's. (edited to Titanium)
            var LightModularHullPieceObj = new CloneTemplate(Info, TechType.CyclopsHullFragment);
            LightModularHullPiecePrefab.SetGameObject(LightModularHullPieceObj);

            var recipe = new RecipeData(
                new Ingredient(TechType.TitaniumIngot, 1),
                new Ingredient(TechType.Lithium, 3),
                new Ingredient(TechType.CopperWire, 2)
                );

            LightModularHullPiecePrefab.SetRecipe(recipe)
                .WithFabricatorType(AdvancedCraftingStation.TreeType)
                .WithStepsToFabricatorTab(CraftTreeHandler.rootRCVehicleIngredientsTab);

            //Unlocks at start ^-^
            //You don't have to put this here i think but i do it here.
            KnownTechHandler.UnlockOnStart(LightModularHullPiece.Info.TechType);

            // register to the game
            LightModularHullPiecePrefab.Register();
            return Info.TechType;
        }
    }
}