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
using RoyalCommonalities.Items.Materials.Natural;

namespace RoyalCommonalities.Items.Materials
{
    public class HeavyModularHullPiece
    {
        //TechType
        public static PrefabInfo Info { get; private set; }

        public static TechType Register()
        {
            Info = PrefabInfo
                // techtype | display name | description
                .WithTechType("heavymodularhullpeace", "Heavy Modular Hull Piece", "Modular hull peace, made for the pourpose of building large armored vehicle hulls.")
                //icon
                .WithIcon(Plugin.Bundle.LoadAsset<Sprite>("HeavyModularHullPieceIco"))
                //size in inventory
                .WithSizeInInventory(new Vector2int(2, 2));
            var HeavyModularHullPiecePrefab = new CustomPrefab(Info);

            var recipe = new RecipeData(
                new Ingredient(TechType.TitaniumIngot, 4),
                new Ingredient(TechType.Lead, 5),
                new Ingredient(Platinum.Info.TechType, 3)
                );

            HeavyModularHullPiecePrefab.SetRecipe(recipe)
                .WithFabricatorType(AdvancedCraftingStation.TreeType)
                .WithStepsToFabricatorTab(CraftTreeHandler.rootRCVehicleIngredientsTab)
                .WithCraftingTime(9f)
                ;

            HeavyModularHullPiecePrefab.SetGameObject(GetAssetBundlePrefab());

            //Unlocks at start ^-^
            //You don't have to put this here i think but i do it here.
            KnownTechHandler.UnlockOnStart(HeavyModularHullPiece.Info.TechType);

            // register to the game
            HeavyModularHullPiecePrefab.Register();
            return Info.TechType;
        }
        private static GameObject GetAssetBundlePrefab()
        {
            GameObject HeavyModularObj = Plugin.Bundle.LoadAsset<GameObject>("HeavyModularHullPieceModel");

            PrefabUtils.AddBasicComponents(HeavyModularObj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Medium);

            MaterialUtils.ApplySNShaders(HeavyModularObj);
            HeavyModularObj.AddComponent<WorldForces>();
            HeavyModularObj.AddComponent<Pickupable>();
            HeavyModularObj.AddComponent<SkyApplier>();
            PrefabUtils.AddWorldForces(HeavyModularObj, 1f, 1f, 1f, false);

            return HeavyModularObj;
        }
    }
}