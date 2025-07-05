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

namespace RoyalCommonalities.Items.Materials.Natural
{
    class Platinum
    {
        //TechType
        public static PrefabInfo Info { get; private set; }

        public static void Register()
        {
            Info = PrefabInfo.WithTechType("royalplatinum", "Platinum", "Stable and dense high-grade noble metal").WithIcon((Plugin.Bundle.LoadAsset<Sprite>("PlatinumIco")));
            var PlatinumPrefab = new CustomPrefab(Info);

            PlatinumPrefab.SetGameObject(GetAssetBundlePrefab());

            // register to the game
            PlatinumPrefab.Register();

        }
        private static GameObject GetAssetBundlePrefab()
        {
            GameObject PlatObj = Plugin.Bundle.LoadAsset<GameObject>("Platinum");

            PrefabUtils.AddBasicComponents(PlatObj, Info.ClassID, Info.TechType, LargeWorldEntity.CellLevel.Medium);

            MaterialUtils.ApplySNShaders(PlatObj);
            PlatObj.AddComponent<WorldForces>();
            PlatObj.AddComponent<Pickupable>();
            PlatObj.AddComponent<SkyApplier>();
            PrefabUtils.AddWorldForces(PlatObj, 1f, 1f, 1f, false);

            return PlatObj;
        }
    }
}