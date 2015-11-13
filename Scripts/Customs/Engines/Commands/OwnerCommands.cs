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
    public class OwnerCommands
    {

        public static void Initialize()
        {

            CommandSystem.Register("setminrespawn", AccessLevel.Owner, new CommandEventHandler(SetMinRespawn_OnCommand));


        }

        public static void SetMinRespawn_OnCommand(CommandEventArgs e)
        {
            foreach (Item item in World.Items.Values)
            {
                if (item is PremiumSpawner)
                {
                    if (((PremiumSpawner)item).MinDelay == TimeSpan.FromMinutes(5) && ((PremiumSpawner)item).MaxDelay == TimeSpan.FromMinutes(10))
                    {
                        ((PremiumSpawner)item).MinDelay = TimeSpan.FromMinutes(8);
                        ((PremiumSpawner)item).MaxDelay = TimeSpan.FromMinutes(12);
                    }
                }
            }
        }

    }
}