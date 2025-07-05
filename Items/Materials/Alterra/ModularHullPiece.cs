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
    public class ModularHullPiece
    {
        //TechType
        public static PrefabInfo Info { get; private set; }

        public static TechType Register()
        {
            Info = PrefabInfo
                // techtype | display name | description
                .WithTechType("modularhullpeace", "Modular Hull Piece", "Modular hull peace, made for the pourpose of building large vehicle hulls.")
                //icon
                .WithIcon(Plugin.Bundle.LoadAsset<Sprite>("ModularHullPieceIco"))
                //size in inventory
                .WithSizeInInventory(new Vector2int(2, 2));
            var ModularHullPiecePrefab = new CustomPrefab(Info);

            var recipe = new RecipeData(
                new Ingredient(TechType.TitaniumIngot, 2),
                new Ingredient(TechType.Titanium, 3),
                new Ingredient(TechType.Lithium, 2),
                new Ingredient(TechType.CopperWire, 2)
                );

            ModularHullPiecePrefab.SetRecipe(recipe)
                .WithFabricatorType(AdvancedCraftingStation.TreeType)
                .WithStepsToFabricatorTab(CraftTreeHandler.rootRCVehicleIngredientsTab)
                .WithCraftingTime(8f)
                ;

            ModularHullPiecePrefab.SetGameObject(GetAssetBundlePrefab());

            //Unlocks at start ^-^
            //You don't have to put this here i think but i do it here.
            KnownTechHandler.UnlockOnStart(ModularHullPiece.Info.TechType);

            // register to the game
            ModularHullPiecePrefab.Register();
            return Info.TechType;
        }
        private static GameObject GetAssetBundlePrefab()
        {
            GameObject ModularObj = Plugin.Bundle.LoadAsset<GameObject>("ModularHullPieceModel");

            PrefabUtils.AddBasicComponents(ModularObj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Medium);

            MaterialUtils.ApplySNShaders(ModularObj);
            ModularObj.AddComponent<WorldForces>();
            ModularObj.AddComponent<Pickupable>();
            ModularObj.AddComponent<SkyApplier>();
            PrefabUtils.AddWorldForces(ModularObj, 1f, 1f, 1f, false);

            return ModularObj;
        }
    }
}