using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Nautilus.Crafting;
using Nautilus.Handlers;
using Nautilus.Utility;
using Nautilus.FMod;
using Nautilus.Extensions;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets;
using static PerformanceConsoleCommands;
using static CraftData;
using System.Collections;
using System.Reflection;
using Unity.Burst.Intrinsics;
using UnityEngine;
using HarmonyLib;
using RoyalCommonalities;
using RoyalCommonalities.MonoBehaviours;
using RoyalCommonalities.Buildables.Crafting;
using RoyalCommonalities.Items.Materials;
using static LootDistributionData;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using JetBrains.Annotations;
using static TechStringCache;
using Story;
using RoyalCommonalities.Items.Materials.Natural;
using Mono.Cecil;
using System.ComponentModel;
using System.CodeDom;
using UnityEngine.AddressableAssets;
using BepInEx.Logging;
using BepInEx;
using UnityEngine.PlayerLoop;

namespace RoyalCommonalities.WorldObjects.Materials.Outcrop
{
    class PlatinumOutcrop
    {
        //TechType
        public static PrefabInfo Info { get; private set; }
        public static void Register()
        {
            Info = PrefabInfo.WithTechType("platinumchunk", "Platinum Outcrop", "Wonder how this will work").WithIcon(Plugin.Bundle.LoadAsset<Sprite>("PlatinumChunkIco"));
            var RoyalRockPrefab = new CustomPrefab(Info);

            
            RoyalRockPrefab.SetGameObject(GetPrefabAsync);

            RoyalRockPrefab.SetSpawns(
                    new BiomeData
                    {
                        biome = BiomeType.Mountains_Sand,
                        count = 1,
                        probability = 0.35f
                    }
                );
            

            RoyalRockPrefab.Register();
        }
        /*
        public static List<BreakableResource.RandomPrefab> ResourceList = new()
        {
            new() { prefabReference = new AssetReferenceGameObject(Platinum.Info.PrefabFileName).ForceValid(), prefabTechType = Platinum.Info.TechType, chance = 0.3f },
            new() { prefabReference = new AssetReferenceGameObject("WorldEntities/Natural/diamond.prefab").ForceValid(), prefabTechType = TechType.Diamond, chance = 0.13f },
            new() { prefabReference = new AssetReferenceGameObject("WorldEntities/Natural/lithium.prefab").ForceValid(), prefabTechType = TechType.Lithium, chance = 0.4f }
        };
        */

        private static IEnumerator GetPrefabAsync(IOut<GameObject> gameObject)
        {
            var ResourceList = new List<BreakableResource.RandomPrefab>
                {
                    new() { prefabReference = new AssetReferenceGameObject(Platinum.Info.ClassID), prefabTechType = Platinum.Info.TechType, chance = 0.3f },
                    new() { prefabReference = new AssetReferenceGameObject("WorldEntities/Natural/diamond.prefab"), prefabTechType = TechType.Diamond, chance = 0.13f },
                    new() { prefabReference = new AssetReferenceGameObject("WorldEntities/Natural/lithium.prefab"), prefabTechType = TechType.Lithium, chance = 0.26f }
                };
            var prefab = Plugin.Bundle.LoadAsset<GameObject>("PlatinumChunk");

            var pId = prefab.EnsureComponent<PrefabIdentifier>();
            pId.ClassId = Info.ClassID;

            prefab.EnsureComponent<EntityTag>().slotType = EntitySlot.Type.Small;
            prefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;
            prefab.EnsureComponent<SkyApplier>().renderers = prefab.GetComponentsInChildren<Renderer>();
            PrefabUtils.AddResourceTracker(prefab, PlatinumOutcrop.Info.TechType);
            prefab.EnsureComponent<VFXSurface>().surfaceType = VFXSurfaceTypes.glass;

            var task = CraftData.GetPrefabForTechTypeAsync(TechType.SandstoneChunk);
            yield return task;

            var fx = task.GetResult().GetComponent<BreakableResource>().breakFX;
            var breakableResource = prefab.EnsureComponent<BreakableResource>();
            breakableResource.breakFX = fx;
            
            breakableResource.hitsToBreak = 3;
            var entropy = Player.mainObject.GetComponent<PlayerEntropy>();

            entropy.randomizers.Add(new PlayerEntropy.TechEntropy()
            {
                // Each possible resource that can be output from an outcrop needs it's own FairRandomizer component to be added to the player.
                entropy = Player.mainObject.AddComponent<FairRandomizer>(),
                techType = Platinum.Info.TechType
            });

            breakableResource.defaultPrefabReference = ResourceList.Last().prefabReference;
            breakableResource.defaultPrefabTechType = ResourceList.Last().prefabTechType;
            breakableResource.prefabList = ResourceList;

            //breakableResource.BreakIntoResources();

            prefab.AddComponent<GenericHandTarget>();
            prefab.AddComponent<DoRandomShitCuzNoUnityEditorAndMonoSucksAndDoesntHavePersistantDelegatesOnRuntime>();

            MaterialUtils.ApplySNShaders(prefab, 8f, 8f, 0.4f);

            gameObject.Set(prefab);
        }
    }
}