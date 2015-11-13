using System;
using System.Xml;
using Server;
using Server.Mobiles;
using Server.Gumps;
using Server.Accounting;
using Server.Misc;
using Server.Items;
using Server.Spells.Chivalry;
using Server.Spells.Necromancy;
using Server.Spells.Spellweaving;
using Server.Spells.First;
using Server.Spells.Second;
using Server.Spells.Third;
using Server.Spells.Fourth;
using Server.Spells.Fifth;
using Server.Spells.Sixth;
using Server.Spells.Seventh;
using Server.Spells.Eighth;
using System.Collections.Generic;
using DimensionsNewAge.Scripts.Customs.Engines;

namespace Server.Regions
{
    public class KnightOfBritainRegion : BaseRegion
    {

        public KnightOfBritainRegion(XmlElement xml, Map map, Region parent)
            : base(xml, map, parent)
        {

        }

        public override bool OnDamage(Mobile m, ref int Damage)
        {
            if (m is PlayerMobile)
            {
                PlayerMobile player = m as PlayerMobile;
                Mobile damager = player.FindMostRecentDamager(false);

                if (damager != null && damager is PlayerMobile)
                {
                    ((PlayerMobile)damager).KnightOfBritainPoints += Damage / 2;

                    ((PlayerMobile)damager).SendMessage(1259, string.Format("KnightOfBritain -> +{0} dano em ", Damage) + player.RawName);
                }
            }

            return base.OnDamage(m, ref Damage);
        }


        public override bool OnBeforeDeath(Mobile m)
        {
            if (m is PlayerMobile)
            {
                PlayerMobile player = m as PlayerMobile;
                Mobile damager = player.FindMostRecentDamager(false);

                if (damager != null && damager is PlayerMobile)
                {
                    ((PlayerMobile)damager).KnightOfBritainKills++;

                    ((PlayerMobile)damager).SendMessage(1259, "KnightOfBritain -> +30 pela morte de " + player.RawName);
                }
            }

            return base.OnBeforeDeath(m);
        }


    }
}
