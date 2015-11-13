using System;
using System.Xml;
using Server;
using Server.Mobiles;
using Server.Gumps;
using Server.Accounting;
using Server.Misc;
using Server.Items;
using Server.Spells;
using Server.Spells.Necromancy;

namespace Server.Regions
{
    public class PvPStagingRegion : BaseRegion
    {
        PvPStone m_PvPStone;

        public PvPStagingRegion(PvPStone pvpstone, string name, Map map, Rectangle2D[] area) : base(name, map, 50, area)
        {
            m_PvPStone = pvpstone;
        }

        public override bool AllowHousing(Mobile from, Point3D p)
        {
            return false;
        }

        public override void OnEnter(Mobile m)
        {
            if (m is PlayerMobile)
            {
                IMount mount = m.Mount;
                if (mount != null)
                    mount.Rider = null;
            }
            m.CloseGump(typeof(SummonFamiliarGump));
        }

        public override bool OnBeginSpellCast(Mobile from, ISpell s)
        {
            return false;
        }

        public override bool OnSkillUse(Mobile m, int Skill)
        {
            //Can Spirit Speak
            if (Skill == 32)
                return true;
            //Can use Poisoning
            if (Skill == 30)
                return true;

            //no other skills can be used
            return false;
        }

        public override bool OnBeforeDeath(Mobile m)
        {
            //players cant die
            if (m is PlayerMobile)
            {
                return false;
            }
            return base.OnBeforeDeath(m);
        }

        public override bool OnDoubleClick(Mobile m, object o)
        {
            if (m.AccessLevel > AccessLevel.Player)
                return true;

            if (o is BaseTool)
                return true;
            //Allow Displaying of paperdoll
            if (o is PlayerMobile && m is PlayerMobile)
            {
                ((PlayerMobile)o).DisplayPaperdollTo(m);
            }
            //Allow opening of own backpack
            if (o is Backpack)
            {
                Backpack pack = o as Backpack;
                if (pack.Parent == m)
                    return true;
                else
                    return false;
            }
            //cash bankchecks
            if (o is BankCheck)
                return true;
            
            //can open any container
            if (o is BaseContainer)
                return true;

            //all other items are a NO
            return false;
        }
    }
}
