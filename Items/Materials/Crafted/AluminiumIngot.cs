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

namespace RoyalCommonalities.Items.Materials.Crafted
{
    class AluminiumIngot
    {
        //TechType
        public static PrefabInfo Info { get; private set; }

        public static void Register()
        {
            Info = PrefabInfo.WithTechType("RoyalAluminiumIngot", "Aluminium Ingot", "Ingot composed out of aliminium").WithIcon(Plugin.Bundle.LoadAsset<Sprite>("AluminiumIngotIco"));
            var AluminiumIngotPrefab = new CustomPrefab(Info);

            var AluminiumIngotObj = new CloneTemplate(Info, TechType.TitaniumIngot);
            AluminiumIngotPrefab.SetGameObject(AluminiumIngotObj);

            var recipe = new RecipeData(
                new Ingredient(Aluminium.Info.TechType, 6)
                )
            { craftAmount = 1 };
            AluminiumIngotPrefab.SetRecipe(recipe)

                .WithFabricatorType(CraftTree.Type.Fabricator)
                .WithStepsToFabricatorTab("Resources", "BasicMaterials")
                .WithCraftingTime(4f)
                ;

            AluminiumIngotPrefab.SetGameObject(GetAssetBundlePrefab());

            //Unlocks at start ^-^
            //You don't have to put this here i think but i do it here.
            //ScanningGadget.requiredForUnlock(Ionite.Info.TechType);
            KnownTechHandler.UnlockOnStart(AluminiumIngot.Info.TechType);

            // register to the game
            AluminiumIngotPrefab.Register();

        }

        private static GameObject GetAssetBundlePrefab()
        {
            GameObject Obj = Plugin.Bundle.LoadAsset<GameObject>("AluminiumIngotModel");

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
        