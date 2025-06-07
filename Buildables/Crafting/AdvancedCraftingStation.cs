using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nautilus.Assets.Gadgets;
using static CraftData;
using UnityEngine;
using Valve.VR;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Utility;
using System.Reflection;
using System.IO;
using HarmonyLib;
using Nautilus.Assets;
using Nautilus.Crafting;
using Nautilus.Handlers;
using RoyalCommonalities;
using RoyalCommonalities.Items.Materials.Natural;
using RoyalCommonalities.WorldObjects.Precursor.Materials.Deposit;
using System.Runtime.CompilerServices;

namespace RoyalCommonalities.Buildables.Crafting
{
    internal class AdvancedCraftingStation
    {
        private const string ClassID = "RoyalFabricatorClassID";
        private const string DisplayName = "Advanced Crafting Station";
        private const string Description = "A Crafting Station capable of constructing advanced components.";
        internal static CraftTree.Type TreeType = default;

        private static Texture2D texture;
        private static Texture2D spriteTexture;


        internal static void CreateAndRegister()
        {
            LoadImageFiles();

            var Info = Nautilus.Assets.PrefabInfo.WithTechType(ClassID, DisplayName, Description, "English", false)
                .WithIcon(Plugin.Bundle.LoadAsset<Sprite>("ACStation"));

            var prefab = new Nautilus.Assets.CustomPrefab(Info);

            if (GetBuilderIndex(TechType.Fabricator, out var group, out var category, out _))
            {
                var scanGadget = prefab.SetPdaGroupCategoryAfter(group, category, TechType.Workbench)
                .WithAnalysisTech(spriteTexture == null ? null : Sprite.Create(spriteTexture, new Rect(0f, 0f, spriteTexture.width, spriteTexture.height), new Vector2(0.5f, 0.5f)), null, null);
                scanGadget.RequiredForUnlock = TechType.Constructor;
            }

            var fabGadget = prefab.CreateFabricator(out var treeType);
            TreeType = treeType;

            var RoyalfabTemplate = new Nautilus.Assets.PrefabTemplates.FabricatorTemplate(Info, TreeType)
            {

                ModifyPrefab = ModifyGameObject,
                FabricatorModel = FabricatorTemplate.Model.Workbench,
                //here u dictate where you want it to be placable.
                ConstructableFlags = Nautilus.Utility.ConstructableFlags.Ground | Nautilus.Utility.ConstructableFlags.Base | Nautilus.Utility.ConstructableFlags.Submarine
                | Nautilus.Utility.ConstructableFlags.Inside
            };

            prefab.SetGameObject(RoyalfabTemplate);

            Nautilus.Handlers.CraftDataHandler.SetRecipeData(Info.TechType, GetBlueprintRecipe());
            prefab.Register();

        }


        private static void LoadImageFiles()
        {
            //u can technicly do it through assetbundle not requiering all this stuff just using texture = Plugin.Bundle.LoadAsset<Texture2D>("ACStationTexture") in the if.
            //but idonno i haven't tested it so i don't guarantee it wuld work

            string executingLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //this is in wich folder is ur texture.
            string folderPath = Path.Combine(executingLocation, "assets");

            if (texture == null)
            {
                //here u give it name of ur textures file
                string fileLocation = Path.Combine(folderPath, "ACStationTexture.png");
                texture = ImageUtils.LoadTextureFromFile(fileLocation);
            }
        }

       
        public static void ModifyGameObject(GameObject gObj)
        {
            if (texture != null)
            {
                // Set the custom texture (u don't change anything here)
                SkinnedMeshRenderer skinnedMeshRenderer = gObj.GetComponentInChildren<SkinnedMeshRenderer>();
                skinnedMeshRenderer.material.mainTexture = texture;
            }

            // Change size
            Vector3 scale = gObj.transform.localScale;
            const float factor = 1.25f;
            //if you want you culd technicly add another factor and change the vector3 to have it in some stuff but doing it proportionaly with one factor will look better.
            gObj.transform.localScale = new Vector3(scale.x * factor, scale.y * factor, scale.z * factor);
        }

        private static Nautilus.Crafting.RecipeData GetBlueprintRecipe()
        {
            return new Nautilus.Crafting.RecipeData
            {
                //idonno what craft amount wuld change in case of a buildable but just in case don't play with it.
                craftAmount = 1,
                Ingredients =
                {
                    new CraftData.Ingredient(TechType.Titanium, 3),
                    new CraftData.Ingredient(Platinum.Info.TechType, 2),
                    new CraftData.Ingredient(TechType.AdvancedWiringKit, 2),
                    new CraftData.Ingredient(TechType.ComputerChip, 1),
                    new CraftData.Ingredient(TechType.JeweledDiskPiece, 1)
                }
            };
        }
    }


        

        

      
}