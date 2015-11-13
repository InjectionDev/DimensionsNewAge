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

namespace Server.Regions
{
    public class PvPRegion : BaseRegion
    {
        PvPStone m_PvPStone;

        public PvPRegion(PvPStone pvpstone, string name, Map map, Rectangle2D[] area) : base(name, map, 50, area)
        {
            m_PvPStone = pvpstone;
        }

        public override bool AllowHousing(Mobile from, Point3D p)
        {
            return false;
        }

        public override TimeSpan GetLogoutDelay(Mobile m)
        {
            if (m.AccessLevel == AccessLevel.Player)
                return TimeSpan.FromMinutes(5.0);

            return base.GetLogoutDelay(m);
        }

        public override void OnEnter(Mobile m)
        {
            if (m is PlayerMobile)
            {
				//Dismount players entering.
                IMount mount = m.Mount;
                if (mount != null)
                    mount.Rider = null;

                ((PlayerMobile)m).AutoStablePets();
            }
        }

        public override bool OnBeginSpellCast(Mobile from, ISpell s)
        {
			// This is a list of spells that players can not use in the pvp region area.
			// Feel free to remove or add any spells to fit your servers needs.
            if (
                s is MagicTrapSpell || s is RemoveTrapSpell || s is MagicLockSpell || s is TelekinesisSpell ||
                s is TeleportSpell || s is UnlockSpell || s is WallOfStoneSpell || s is RecallSpell ||
                s is BladeSpiritsSpell || s is DispelFieldSpell || s is IncognitoSpell || s is PoisonFieldSpell ||
                s is SummonCreatureSpell || s is DispelSpell || s is InvisibilitySpell || s is MarkSpell ||
                s is ParalyzeFieldSpell || s is RevealSpell || s is EnergyFieldSpell || s is ChainLightningSpell ||
                s is GateTravelSpell || s is MassDispelSpell || s is MeteorSwarmSpell || s is PolymorphSpell ||
                s is AirElementalSpell || s is EarthElementalSpell || s is EarthquakeSpell || s is EnergyVortexSpell ||
                s is FireElementalSpell || s is ResurrectionSpell || s is SummonDaemonSpell || s is WaterElementalSpell ||
                s is DispelEvilSpell || s is NobleSacrificeSpell || s is EnemyOfOneSpell || s is AnimateDeadSpell ||
                s is ExorcismSpell || s is SummonFamiliarSpell || s is VengefulSpiritSpell )
                return false;

            return true;
        }

        public override bool OnSkillUse(Mobile m, int Skill)
        {
            //Can Spirit Speak
            if (Skill == 32)
                return true;
            //Can use Meditation
            if (Skill == 46)
                return true;

            //no other skills can be used
            return false;
        }

        public override bool OnBeforeDeath(Mobile m)
        {
            if (m is PlayerMobile)
            {
				//Set up match results with the PvP Stone
                if (m_PvPStone.CurrentDuelers[0] == m)
                    m_PvPStone.MatchEnd(m_PvPStone.CurrentDuelers[1], m);
                else
                    m_PvPStone.MatchEnd(m_PvPStone.CurrentDuelers[0], m);

                return false;
            }
            return base.OnBeforeDeath(m);
        }

        public override bool OnDoubleClick(Mobile m, object o)
        {
            //no explosion potions
            if (o is BaseExplosionPotion)
                return false;
            //no cure pots either
            if (o is BaseCurePotion)
                return false;
            //no conflagration either
            if (o is BaseConflagrationPotion)
                return false;
            //can use any other potion
            if (o is BasePotion)
                return true;
            //can use bandage
            if (o is Bandage)
                return true;
            //can use fukiya
            if (o is Fukiya)
                return true;
            //can use Ninja Belt
            if (o is LeatherNinjaBelt)
                return true;

            //nothing else can be used
            return false;
        }
    }
}
