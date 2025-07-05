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

            // clone tamplete model, saved here just in case
            //var ioniteObj = new CloneTemplate(Info, TechType.Kyanite);
            //ionitePrefab.SetGameObject(ioniteObj);

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

            //this gets you custom model
            ionitePrefab.SetGameObject(GetAssetBundlePrefab());

            // register to the game
            ionitePrefab.Register();

        }
        private static GameObject GetAssetBundlePrefab()
        {
            //your icon and model can have the same name since when you load an asset you specify if you want a GameObject or Sprite, or even any other type.
            //here you put your model
            GameObject IonObj = Plugin.Bundle.LoadAsset<GameObject>("Ionite");

            PrefabUtils.AddBasicComponents(IonObj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Medium);

            MaterialUtils.ApplySNShaders(IonObj);
            IonObj.AddComponent<WorldForces>();
            IonObj.AddComponent<Pickupable>();
            IonObj.AddComponent<SkyApplier>();
            //obj / mass / underwater gravity / underwater drag / isKinematic
            PrefabUtils.AddWorldForces(IonObj, 1f, 1f, 1f, false);

            return IonObj;
        }
    }
}