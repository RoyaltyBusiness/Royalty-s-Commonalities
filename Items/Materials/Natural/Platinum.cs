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
            Info = PrefabInfo.WithTechType("royalplatinum", "Platinum", "Rare material").WithIcon(SpriteManager.Get(TechType.Magnesium));
            var PlatinumPrefab = new CustomPrefab(Info);

            // The model
            var PlatinumObj = new CloneTemplate(Info, TechType.Magnesium);
            PlatinumPrefab.SetGameObject(PlatinumObj);

            //Unlocks at start ^-^
            //You don't have to put this here i think but i do it here.
            KnownTechHandler.UnlockOnStart(Platinum.Info.TechType);

            // register to the game
            PlatinumPrefab.Register();

        }
    }
}