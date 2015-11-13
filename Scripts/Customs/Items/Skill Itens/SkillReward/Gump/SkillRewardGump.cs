using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using System.Collections.Generic;
using System.Linq;
using DimensionsNewAge.Scripts.Customs.Engines;

namespace Server.Items
{

    public class SkillRewardGump : Gump
    {
        static Mobile caller;
        static SkillRewardItem skillRewardItem;


        public SkillRewardGump(Mobile from, SkillRewardItem pSkillRewardItem)
            : this()
        {
            caller = from;
            skillRewardItem = pSkillRewardItem;

            this.InitializeGump();
        }

        public SkillRewardGump()
            : base(200, 100)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
        }

        private void InitializeGump()
        {


            AddPage(0);
            AddBackground(18, 10, 773, 566, 9390);
            AddImage(127, 58, 104);
            AddLabel(61, 347, 897, @"Pergaminho Sagrado de Arshen");
            AddBackground(467, 52, 289, 483, 9350);
            AddImage(484, 66, 5577);

            AddButton(676, 489, 1144, 1145, 0, GumpButtonType.Reply, 0);


            int btnPos = 125;
            int lblPos = 125;


            switch (skillRewardItem.SkillType)
            {
                case (int)SkillRewardItemType.Combat:
                    {
                        AddLabel(579, 85, 897, @"Habilidades de Guerra");
                        AddLabel(102, 409, 897, @"Pergaminho Veteranos de Guerra");
                        AddLabel(102, 434, 897, @"Pergaminho Nível " + skillRewardItem.SkillAmount);
                        AddLabel(526, lblPos += 25, 0, @"Archery");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Archery + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Fencing");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Fencing + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Mace Fighting");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Macing + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Parrying");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Parry + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Swordsmanship");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Swords + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Tactics");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Tactics + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Wrestling");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Wrestling + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Anatomy");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Anatomy + 1000, GumpButtonType.Reply, 0);
                        break;
                    }

                case (int)SkillRewardItemType.Work:
                    {
                        AddLabel(579, 85, 897, @"Habilidades de Trabalhadores");
                        AddLabel(102, 409, 897, @"Pergaminho dos Workers");
                        AddLabel(102, 434, 897, @"Pergaminho Nível " + skillRewardItem.SkillAmount);
                        AddLabel(526, lblPos += 25, 0, @"Alchemy");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Alchemy + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Blacksmith");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Blacksmith + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Bowcraft");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Fletching + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Carpentry");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Carpentry + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Cartography");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Cartography + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Cooking");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Cooking + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Inscription");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Inscribe + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Tailoring");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Tailoring + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Tinkering");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Tinkering + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Mining");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Mining + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Lumberjacking");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Lumberjacking + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Fishing");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Fishing + 1000, GumpButtonType.Reply, 0);
                        break;
                    }

                case (int)SkillRewardItemType.Bardo:
                    {
                        AddLabel(579, 85, 897, @"Habilidades de Bardo");
                        AddLabel(102, 409, 897, @"Pergaminho dos Bardos");
                        AddLabel(102, 434, 897, @"Pergaminho Nível " + skillRewardItem.SkillAmount);
                        AddLabel(526, lblPos += 25, 0, @"Discordance");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Discordance + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Musicianship");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Musicianship + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Peacemaking");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Peacemaking + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Provocation");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Provocation + 1000, GumpButtonType.Reply, 0);
                        break;
                    }

                case (int)SkillRewardItemType.Tammer:
                    {
                        AddLabel(579, 85, 897, @"Habilidades de Tammer");
                        AddLabel(102, 409, 897, @"Pergaminho dos Tammers");
                        AddLabel(102, 434, 897, @"Pergaminho Nível " + skillRewardItem.SkillAmount);
                        AddLabel(526, lblPos += 25, 0, @"Animal Taming");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.AnimalTaming + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Veterinary");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Veterinary + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"AnimalLore");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.AnimalLore + 1000, GumpButtonType.Reply, 0);
                        AddLabel(526, lblPos += 25, 0, @"Magery");
                        AddButton(487, btnPos += 25, 1153, 1154, (int)SkillName.Magery + 1000, GumpButtonType.Reply, 0);
                        break;
                    }

            }

        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            if (info.ButtonID >= 1000)
            {
                from.Skills[info.ButtonID - 1000].Base += skillRewardItem.SkillAmount;
                if (from.Skills[info.ButtonID - 1000].Base > 100)
                    from.Skills[info.ButtonID - 1000].Base = 100;

                Logger.LogMessage(string.Format("{0} -> Skill:{1}. SkillAmount:{2}", from.RawName, from.Skills[info.ButtonID - 1000].SkillName, skillRewardItem.SkillAmount), "SkillReward");
                from.SendAsciiMessage(0x44, "Voce leu o pergaminho e aprendeu algumas habilidades.");
                skillRewardItem.Delete();
            }

            switch (info.ButtonID)
            {


                case 100:
                    {
                        
                        break;
                    }
                case 101: 
                    {
                        break;

                    }
                case 1:
                    {

                        break;
                    }
                case 2:
                    {
                        break;

                    }

                case 0:
                default:
                    break;
            }
        }

    }
}