using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Factions;
using Server.Commands;
using System.Collections.Generic;

using Server.Mobiles;
using Server.Regions;
using Server.Gumps;
using DimensionsNewAge.Scripts.Customs.Engines;
using Server.Items;

namespace DimensionsNewAge.Scripts.Customs.Engines
{
    public class PlayerCommands
    {

        public static void Initialize()
        {

            CommandSystem.Register("drinkcure", AccessLevel.Player, new CommandEventHandler(EquipWeapon_OnCommand));
            CommandSystem.Register("drinkrefresh", AccessLevel.Player, new CommandEventHandler(EquipWeapon_OnCommand));
            CommandSystem.Register("drinkmana", AccessLevel.Player, new CommandEventHandler(EquipWeapon_OnCommand));
            CommandSystem.Register("drinkheal", AccessLevel.Player, new CommandEventHandler(EquipWeapon_OnCommand));



        }

        public static void DrinkHeal_OnCommand(CommandEventArgs e)
        {
            Item item = new Item();
            item.Consume();
        }

        public static void EquipWeapon_OnCommand(CommandEventArgs e)
        {
            Item item = new Item();
            item.Consume();
        }

    }
}