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
    public class SurvivorRegion : BaseRegion
    {
        SurvivorStone SurvivorStone;



        public SurvivorRegion(SurvivorStone pSurvivorStone, string name, Map map, Rectangle2D[] area)
            : base(name, map, 50, area)
        {
            this.SurvivorStone = pSurvivorStone;

        }

        public override bool AllowSpawn()
        {
            return false;
        }

        public override bool AllowHarmful(Mobile from, Mobile target)
        {
            return true;
        }

        public override bool AllowBeneficial(Mobile from, Mobile target)
        {
            return true;
        }

        public override TimeSpan GetLogoutDelay(Mobile m)
        {
            if (m.AccessLevel == AccessLevel.Player)
                return TimeSpan.FromMinutes(5.0);

            return base.GetLogoutDelay(m);
        }

        public override bool OnBeginSpellCast(Mobile from, ISpell s)
        {
            from.SendMessage("Voce nao pode usar Magias neste evento.");
            return false;
        }

        public override bool OnDecay(Item item)
        {
            return false;
        }

        public override bool AcceptsSpawnsFrom(Region region)
        {
            return false;
        }

        public override bool OnDamage(Mobile m, ref int Damage)
        {
            if (m is PlayerMobile)
            {
                PlayerMobile player = m as PlayerMobile;
                if (SingletonEvent.Instance.IsTeamMode && player.TeamID > 0)
                {
                    Mobile damager = player.FindMostRecentDamager(false);
                    if (damager is PlayerMobile && ((PlayerMobile)damager).TeamID == player.TeamID)
                    {
                        Damage = 0;
                        damager.SendMessage("Voce nao pode atacar alguem do seu time!");
                        return false;
                    }
                }
            }

            return base.OnDamage(m, ref Damage);
        }

        public override void OnSpeech(SpeechEventArgs args)
        {

            if (SingletonEvent.Instance.HasAntiPanelaMode)
            {
                foreach (Mobile mobile in this.GetMobiles())
                {
                    if (mobile == args.Mobile)
                        continue;
                    else
                        mobile.Say(args.Speech);
                }
            }
            else
                base.OnSpeech(args);
        }


        public override bool OnSkillUse(Mobile m, int Skill)
        {
            m.SendMessage("Voce nao pode usar Skills neste evento.");
            return false;
        }


        public override void OnRegister()
        {
            base.OnRegister();
        }

        public override void OnEnter(Mobile m)
        {
            base.OnEnter(m);
        }


        public override void OnUnregister()
        {
            base.OnUnregister();
        }

        public override bool OnBeforeDeath(Mobile m)
        {
            if (m is PlayerMobile)
            {
                Console.WriteLine("Survivor: OnBeforeDeath " + m.Serial + "-" + m.Name);
                this.SurvivorStone.SetPlayerDeath(m.Serial);
                return false;
            }

            return base.OnBeforeDeath(m);
        }

        public override bool OnDoubleClick(Mobile m, object o)
        {
            ////no explosion potions
            //if (o is BaseExplosionPotion)
            //    return false;
            ////no cure pots either
            //if (o is BaseCurePotion)
            //    return false;
            ////no conflagration either
            //if (o is BaseConflagrationPotion)
            //    return false;
            ////can use any other potion
            //if (o is BasePotion)
            //    return true;
            ////can use bandage
            //if (o is Bandage)
            //    return true;
            ////can use fukiya
            //if (o is Fukiya)
            //    return true;
            ////can use Ninja Belt
            //if (o is LeatherNinjaBelt)
            //    return true;

            return true; // allow all
        }

        public override bool AllowHousing(Mobile from, Point3D p)
        {
            return false;
        }

    }
}
