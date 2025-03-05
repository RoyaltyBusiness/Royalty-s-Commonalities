using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nautilus.Assets.Gadgets;
using static CraftData;
using UnityEngine;

namespace RoyalCommonalities.Buildables.Crafting
{
    internal class AdvancedCraftingStation
    {
        private const string ClassID = "RoyalFabricatorClassID";
        private const string DisplayName = "Advanced Crafting Station";
        private const string Description = "A Crafting Station capable of constructing advanced components.";
        internal static CraftTree.Type TreeType = default;
        internal static void CreateAndRegister()
        {
            var Info = Nautilus.Assets.PrefabInfo.WithTechType(ClassID, DisplayName, Description, "English", false)
                .WithIcon(SpriteManager.Get(TechType.Workbench));

            var prefab = new Nautilus.Assets.CustomPrefab(Info);

            if (GetBuilderIndex(TechType.Fabricator, out var group, out var category, out _))
            {
                var scanGadget = prefab.SetPdaGroupCategoryAfter(group, category, TechType.Fabricator);
                scanGadget.RequiredForUnlock = TechType.Constructor;
            }

            var fabGadget = prefab.CreateFabricator(out var treeType);
            TreeType = treeType;

            var RoyalfabTemplate = new Nautilus.Assets.PrefabTemplates.FabricatorTemplate(Info, TreeType)
            {
                
                FabricatorModel = Nautilus.Assets.PrefabTemplates.FabricatorTemplate.Model.Workbench,
                ConstructableFlags = Nautilus.Utility.ConstructableFlags.Ground | Nautilus.Utility.ConstructableFlags.Base | Nautilus.Utility.ConstructableFlags.Submarine
                | Nautilus.Utility.ConstructableFlags.Inside
            };

            prefab.SetGameObject(RoyalfabTemplate);

            Nautilus.Handlers.CraftDataHandler.SetRecipeData(Info.TechType, GetBlueprintRecipe());
            prefab.Register();
        }
        private static Nautilus.Crafting.RecipeData GetBlueprintRecipe()
        {
            return new Nautilus.Crafting.RecipeData
            {
                craftAmount = 1,
                Ingredients =
                {
                    new CraftData.Ingredient(TechType.Titanium, 4),
                    new CraftData.Ingredient(TechType.AdvancedWiringKit, 2),
                    new CraftData.Ingredient(TechType.ComputerChip, 2),
                }
            };
        }
    }
}