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

//IMPORTANT! DON'T ADD "Drillable" TO THE NAMESPACE OR ELSE IT WILL MESS WITH THE CODE!!!
namespace RoyalCommonalities.WorldObjects.Materials.Deposits.Precursor
{
    class DrillableIonite
    {
        //TechType
        public static PrefabInfo Info { get; private set; }
        public static void Register()
        {
            //i don't think you have to give it an icon for how it works rn but just put it in case.
            //"Wonder how till will work" didn't show up from me testing so maybe it never does. so it can stay as is
            Info = PrefabInfo.WithTechType("drillableionite", "Drillable Ionite", "Wonder how this will work").WithIcon(Plugin.Bundle.LoadAsset<Sprite>("Ionite"));
            var DrillableIonitePrefab = new CustomPrefab(Info);

            PDAHandler.AddEncyclopediaEntry(
                key: "Ionite",
                path: "DownloadedData/Precursor/Scan",
                title: "Ionite",
                desc: "This green mineral substance has no entry on the periodic table, but resembles the signature of ion cube. It has and an unprecedented ability to transmit or store large amounts of ionic energy within it.\nBy the way it is found it apears that this substance over time crystalises and pushes itself out of any alloy it is in. if not maintained correcly.\nIts apearance in the structures tho suggest that this phenomena has been deemed as 'neseseey evil'.\n\n- It is able to efficiently and instantly transport large amount of ion energy\n- Slowly Re-crystalises over time if not maintained.\n- Likely used for out of sight energy transportation.\n\nAssessment: Potencialy valuable building material."
,

                image: null,
                popupImage: null,
                unlockSound: PDAHandler.UnlockBasic
            );
            PDAHandler.AddCustomScannerEntry(
                key: DrillableIonite.Info.TechType,
                scanTime: 6f,
                destroyAfterScan: false,
                encyclopediaKey: "Ionite"
            );


            //you call that lil IEnumerator below here
            DrillableIonitePrefab.SetGameObject(GetPrefabAsync);

            //LOOK HERE
            //here you set your spawns. so in this case drillable ionite this is temporary. itll use coordinat spawns later. so refair to Drillable platinum code for normal drillables.
            //drillable platinum has stuff more explained.

            DrillableIonitePrefab.SetSpawns(
                    new BiomeData { 
                        biome = BiomeType.InactiveLavaZone_CastleChamber_Floor, 
                        count = 1, 
                        probability = 0.14f
                    },
                    new BiomeData
                    {
                        biome = BiomeType.PrisonAquarium_CaveFloor,
                        count = 1,
                        probability = 0.1f
                    },
                    new BiomeData { 
                        biome = BiomeType.LostRiverJunction_Ground, 
                        count = 1, 
                        probability = 0.03f
                    }
                );
            ;
            
            // register to the game
            DrillableIonitePrefab.Register();
        }
        private static IEnumerator GetPrefabAsync(IOut<GameObject> gameObject)
        {
            //This wuld be your model, donno if it works with game prefab clones couse i haven't tested it
            /* my hierarhy for the prefab looks like:

                DrillableThingy
                    modelRoot
                        chunk_01
                        chunk_02
                        chunk_03
                        chunk_04
                        chunk_05

            Each chunk has a mesh collider set to Convex. (you drill chunks individualy)
            also donno if its required but seeing code below it might be important to make the numbers 0something not just something

            i haven't tested if you can do more than 5 chunks with this code
            */
            var prefab = Plugin.Bundle.LoadAsset<GameObject>("DrillableIonite2");

            var pId = prefab.EnsureComponent<PrefabIdentifier>();
            pId.ClassId = Info.ClassID;


            //entity slot is weard but for this small works
            prefab.EnsureComponent<EntityTag>().slotType = EntitySlot.Type.Small;
            //this sets how far away a thingy renders. near is good
            prefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;
            prefab.EnsureComponent<SkyApplier>().renderers = prefab.GetComponentsInChildren<Renderer>();
            //tracker thing i beleave sets like lil icon for the thing. since this is platinum it gets ionite icon
            PrefabUtils.AddResourceTracker(prefab, Ionite.Info.TechType);
            prefab.EnsureComponent<VFXSurface>().surfaceType = VFXSurfaceTypes.glass;

            var task = CraftData.GetPrefabForTechTypeAsync(TechType.DrillableGold);
            yield return task;

            // this is the important stuff
            var fx = task.GetResult().GetComponent<Drillable>().breakFX;
            var drillable = prefab.EnsureComponent<Drillable>();
            drillable.breakFX = fx;
            drillable.breakAllFX = fx;
            //this u set what you want the drillable to give. for ionite is ionite. max resources to spawn is how much it gives from 1 to ur number. in case of ionite it gives 1 or 2 ionite.
            //i don't know what chance does.
            drillable.resources = new[] { new Drillable.ResourceType { chance = 1f, techType = Ionite.Info.TechType } };
            drillable.maxResourcesToSpawn = 2;
            //here is why 0 in name might be important. donno if its acualy what happens i don't acualy code. this drillable code i got from Metious
            drillable.modelRoot = prefab.transform.GetChild(0).gameObject;

            prefab.AddComponent<GenericHandTarget>();
            prefab.AddComponent<DoRandomShitCuzNoUnityEditorAndMonoSucksAndDoesntHavePersistantDelegatesOnRuntime>();

            MaterialUtils.ApplySNShaders(prefab, 8f, 8f, 0.4f);

            gameObject.Set(prefab);
        }
    }
}