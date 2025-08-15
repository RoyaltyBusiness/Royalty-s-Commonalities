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
    class Aluminium
    {
        //TechType
        public static PrefabInfo Info { get; private set; }

        public static void Register()
        {
            Info = PrefabInfo.WithTechType("royalaluminium", "Aluminium", "A strong yet lightweight metal usualy used in making Airplanes").WithIcon(Plugin.Bundle.LoadAsset<Sprite>("AluminiumIco"));
            var AluminiumPrefab = new CustomPrefab(Info);

            var recipe = new RecipeData(
                new Ingredient(TechType.AluminumOxide, 2)
                )
            { craftAmount = 3 };


            AluminiumPrefab.SetRecipe(recipe)
                .WithFabricatorType(AdvancedCraftingStation.TreeType)
                .WithStepsToFabricatorTab(CraftTreeHandler.rootRCConvertTab)
                .WithCraftingTime(2f)
                ;

            AluminiumPrefab.SetGameObject(GetAssetBundlePrefab());

            //Unlocks at start ^-^
            //You don't have to put this here i think but i do it here.
            KnownTechHandler.UnlockOnStart(Aluminium.Info.TechType);

            // register to the game
            AluminiumPrefab.Register();
        }

        private static GameObject GetAssetBundlePrefab()
        {
            GameObject Obj = Plugin.Bundle.LoadAsset<GameObject>("AluminiumModel");

            PrefabUtils.AddBasicComponents(Obj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Medium);

            MaterialUtils.ApplySNShaders(Obj);
            Obj.AddComponent<WorldForces>();
            Obj.AddComponent<Pickupable>();
            Obj.AddComponent<SkyApplier>();
            PrefabUtils.AddWorldForces(Obj, 1f, 1f, 1f, false);

            return Obj;
        }
    }
}