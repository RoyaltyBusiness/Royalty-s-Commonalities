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
            Info = PrefabInfo
                // techtype | display name | description
                .WithTechType("lightmodularhullpeace", "Light Modular Hull Piece", "Modular hull peace, made for the pourpose of building hulls for light vehicles.")
                //icon
                .WithIcon(Plugin.Bundle.LoadAsset<Sprite>("LightModularHullPieceIco"))
                //size in inventory
                .WithSizeInInventory(new Vector2int(2, 2));

            var LightModularHullPiecePrefab = new CustomPrefab(Info);

            var recipe = new RecipeData(
                new Ingredient(TechType.TitaniumIngot, 1),
                new Ingredient(TechType.Lithium, 3),
                new Ingredient(TechType.CopperWire, 2)
                );

            LightModularHullPiecePrefab.SetRecipe(recipe)
                .WithFabricatorType(AdvancedCraftingStation.TreeType)
                .WithStepsToFabricatorTab(CraftTreeHandler.rootRCVehicleIngredientsTab)
                .WithCraftingTime(7f)
                ;

            //Unlocks at start ^-^
            //You don't have to put this here i think but i do it here.
            KnownTechHandler.UnlockOnStart(LightModularHullPiece.Info.TechType);

            LightModularHullPiecePrefab.SetGameObject(GetAssetBundlePrefab());

            // register to the game
            LightModularHullPiecePrefab.Register();
            return Info.TechType;
        }
        private static GameObject GetAssetBundlePrefab()
        {
            GameObject LightModularObj = Plugin.Bundle.LoadAsset<GameObject>("LightModularHullPieceModel");

            PrefabUtils.AddBasicComponents(LightModularObj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Medium);

            MaterialUtils.ApplySNShaders(LightModularObj);
            LightModularObj.AddComponent<WorldForces>();
            LightModularObj.AddComponent<Pickupable>();
            LightModularObj.AddComponent<SkyApplier>();
            PrefabUtils.AddWorldForces(LightModularObj, 1f, 1f, 1f, false);

            return LightModularObj;
        }
    }
}