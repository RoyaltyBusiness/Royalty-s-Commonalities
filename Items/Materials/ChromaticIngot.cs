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

namespace RoyalCommonalities.Items.Materials
{
    public class ChromaticIngot
    {
        //TechType
        public static PrefabInfo Info { get; private set; }

        public static TechType Register()
        {
            Info = PrefabInfo.WithTechType("chromaticingot", "Chromatic Ingot", "Ingot simulating properties of alien materials").WithIcon(Plugin.Bundle.LoadAsset<Sprite>("ChromaticIngot"));
            var chromeingotPrefab = new CustomPrefab(Info);



        // The model of our coal will use the same one as Nickel's. (edited to Titanium)
        var chromeingotObj = new CloneTemplate(Info, TechType.TitaniumIngot);
            chromeingotPrefab.SetGameObject(chromeingotObj);

            var recipe = new RecipeData(
                new Ingredient(TechType.TitaniumIngot),
                new Ingredient(TechType.Copper, 2),
                new Ingredient(TechType.Gold, 2),
                new Ingredient(TechType.PrecursorIonCrystal, 2)
                );

            chromeingotPrefab.SetRecipe(recipe)
                .WithFabricatorType(CraftTree.Type.Fabricator)
                .WithStepsToFabricatorTab(CraftTreeHandler.rootRCPrecursorTab);

            //Unlocks at start ^-^
            //You don't have to put this here i think but i do it here.
            KnownTechHandler.UnlockOnStart(ChromaticIngot.Info.TechType);

            // register to the game
            chromeingotPrefab.Register();
            return Info.TechType;
        }
    }
}
