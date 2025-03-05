using Nautilus.Assets.Gadgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoyalCommonalities.Items.Materials;
using UnityEngine;
using RoyalCommonalities;
using static ReaperLeviathan;
using RoyalCommonalities.Buildables.Crafting;

namespace RoyalCommonalities
{
    internal static class CraftTreeHandler
    {

        //roots, no idea what they do but they make it work so they are important.
        internal static readonly string[] rootRCPrecursorTab = { "RCPR" };
        internal static readonly string[] rootRCVehicleIngredientsTab = { "RCVI" };

        internal static void AddFabricatorMenus()
        {
            //Gets icon for the tabs, you can't put it in the thing for some reason ;-;
            var RCVI_Icon = Plugin.Bundle.LoadAsset<Sprite>("EnzymeIcon");
            var RCPR_Icon = Plugin.Bundle.LoadAsset<Sprite>("PrecursorItemsIcon");

            //Adds tab ^-^
            Nautilus.Handlers.CraftTreeHandler.AddTabNode(AdvancedCraftingStation.TreeType, rootRCPrecursorTab[0], "Precursor Materials", RCPR_Icon);
            Nautilus.Handlers.CraftTreeHandler.AddTabNode(AdvancedCraftingStation.TreeType, rootRCVehicleIngredientsTab[0], "Vehicle Materials", RCVI_Icon);

            //this is how you do it for fabricator:
            //Nautilus.Handlers.CraftTreeHandler.AddTabNode(CraftTree.Type.Fabricator, rootRCVehicleIngredientsTab[0], "Vehicle Materials", RCVI_Icon);

            //just in case this how you do with in-game icon:
            //Nautilus.Handlers.CraftTreeHandler.AddTabNode(CraftTree.Type.Fabricator, rootRCPrecursorTab[0], "Precursor Materials", SpriteManager.Get(TechType.PrecursorIonCrystal));
        }

    }
}