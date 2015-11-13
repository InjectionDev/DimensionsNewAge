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
    public class TheHuntRegion : BaseRegion
    {
        TheHuntStone TheHuntStone;

        private List<Item> m_WallList;
        private int m_WallID = 0x0081;
        private int m_WallHue = 0;

        private List<Mobile> playerMobileBackupList;

        public TheHuntRegion(TheHuntStone pTheHuntStone, string name, Map map, Rectangle2D[] area)
            : base(name, map, 50, area)
        {
            this.TheHuntStone = pTheHuntStone;

            this.playerMobileBackupList = new List<Mobile>();
        }

        public override TimeSpan GetLogoutDelay(Mobile m)
        {
            if (m.AccessLevel == AccessLevel.Player)
                return TimeSpan.FromMinutes(5.0);

            return base.GetLogoutDelay(m);
        }

        public override bool OnBeginSpellCast(Mobile from, ISpell s)
        {
            if (s is GateTravelSpell || s is RecallSpell || s is MarkSpell || s is InvisibilitySpell)
                return false;

            return true;
        }

        public override void OnSpeech(SpeechEventArgs args)
        {
            if (SingletonEvent.Instance.HasAntiPanelaMode)
                this.GetMobiles()[new Random().Next(this.GetMobileCount())].Say(args.Speech);
            else
                base.OnSpeech(args);
        }

        public override bool AllowSpawn()
        {
            return false;
        }

        public override bool OnSkillUse(Mobile m, int Skill)
        {
            if (Skill == (int)SkillName.Hiding)
                return false;
            if (Skill == (int)SkillName.Stealing)
                return false;
            if (Skill == (int)SkillName.AnimalTaming)
                return false;
            if (Skill == (int)SkillName.Stealth)
                return false;

            return true;
        }


        public override void OnRegister()
        {
            base.OnRegister();

            this.playerMobileBackupList.Clear();

            this.m_WallList = new List<Item>();

            List<Point3D> positionList = new List<Point3D>();
            positionList.Add(new Point3D(8, 873, -29));
            positionList.Add(new Point3D(9, 873, -29));
            positionList.Add(new Point3D(10, 873, -29));
            positionList.Add(new Point3D(11, 873, -29));
            positionList.Add(new Point3D(12, 873, -29));
            positionList.Add(new Point3D(13, 873, -29));
            positionList.Add(new Point3D(14, 873, -29));

            positionList.Add(new Point3D(17, 1197, -6));
            positionList.Add(new Point3D(18, 1197, -6));
            positionList.Add(new Point3D(19, 1197, -6));
            positionList.Add(new Point3D(20, 1197, -6));
            positionList.Add(new Point3D(21, 1197, -6));
            positionList.Add(new Point3D(22, 1197, -6));

            foreach (Point3D point3D in positionList)
            {
                StonePaversDark block = new StonePaversDark();
                block.ItemID = this.m_WallID;
                block.Hue = this.m_WallHue;
                block.MoveToWorld(point3D, Map.Ilshenar);
                this.m_WallList.Add(block);
            }
        }

        public override void OnEnter(Mobile m)
        {
            base.OnEnter(m);

            //if (m is PlayerMobile && m.AccessLevel == AccessLevel.Player)
            //{
            //    Mobile player = m as Mobile;
            //    this.playerMobileBackupList.Add(player);

            //    player.Name = "Cacador";
            //    player.Karma = 0;
            //    player.Fame = 0;
            //    player.HairHue = 0;
            //    player.Hue = 0;
            //    player.BodyValue = 0x190;
            //}
        }


        public override void OnUnregister()
        {
            base.OnUnregister();

            foreach (PlayerMobile playerBackup in this.playerMobileBackupList)
            {
                PlayerMobile m = World.FindMobile(playerBackup.Serial) as PlayerMobile;

                m.Name = playerBackup.Name;
                m.Karma = playerBackup.Karma;
                m.Fame = playerBackup.Fame;
                m.HairHue = playerBackup.HairHue;
                m.Hue = playerBackup.Hue;
                m.BodyValue = playerBackup.BodyValue;
            }
            this.playerMobileBackupList.Clear();

            

            foreach (Item item in this.m_WallList)
            {
                item.Delete();
            }
        }

        public override bool OnBeforeDeath(Mobile m)
        {
            if (m is PlayerMobile)
            {
                Console.WriteLine("TheHunt: OnBeforeDeath " + m.Serial + "-" + m.Name);
                this.TheHuntStone.SetPlayerDeath(m.Serial);
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
