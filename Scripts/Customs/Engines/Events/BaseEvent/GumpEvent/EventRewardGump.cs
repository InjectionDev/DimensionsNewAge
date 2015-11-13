using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

using Server.Items;
using System.ComponentModel;

namespace DimensionsNewAge.Scripts.Customs.Engines
{

    public class EventRewardGump : Gump
    {
        private Mobile caller;

        private string htmlRewardList;

        public EventRewardGump(Mobile from)
            : this()
        {
            caller = from;

        }

        public EventRewardGump()
            : base(200,100)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddBackground(177, 10, 423, 581, 9270);
            AddLabel(303, 60, 42, @"D I M E N S I O N S");
            AddLabel(347, 80, 141, @"New Age");
            AddImageTiled(109, -49, 198, 181, 50992);
            AddImageTiled(398, -50, 198, 181, 50993);
            AddImage(126, 4, 10400);
            AddLabel(207, 137, 37, @"Premio Vencedor");
            AddLabel(207, 323, 37, string.Format(@"Itens Selecionados (total {0})", SingletonEvent.Instance.CurrentEventRewardList.Count));
            AddHtml(202, 353, 373, 135, this.GetRewardListHtml(), (bool)true, (bool)true);
            //AddLabel(207, 506, 37, @"Confirmar ?");
            AddButton(209, 534, 247, 248, 0, GumpButtonType.Reply, 0);
            //AddButton(341, 534, 241, 242, 0, GumpButtonType.Reply, 0);


            AddLabel(232, 165, 545, @"5k Gold");
            AddButton(210, 165, 2117, 2118, 1, GumpButtonType.Reply, 0);
            AddLabel(232, 185, 545, @"2 Lings Myth/Aqua");
            AddButton(210, 185, 2117, 2118, 2, GumpButtonType.Reply, 0);
            AddLabel(232, 205, 545, @"2 Lings OldEnd/GoldStone");
            AddButton(210, 205, 2117, 2118, 3, GumpButtonType.Reply, 0);
            AddLabel(232, 225, 545, @"Mustang/Orn/Oolock");
            AddButton(210, 225, 2117, 2118, 4, GumpButtonType.Reply, 0);
            AddLabel(232, 245, 545, @"Orn/Oclock Color");
            AddButton(210, 245, 2117, 2118, 5, GumpButtonType.Reply, 0);
            AddLabel(232, 265, 545, @"Arma Mytheril");
            AddButton(210, 265, 2117, 2118, 6, GumpButtonType.Reply, 0);
            AddLabel(232, 285, 545, @"Plate Mytheril");
            AddButton(210, 285, 2117, 2118, 7, GumpButtonType.Reply, 0);

            AddLabel(415, 165, 545, @"Magic Dye Tub");
            AddButton(393, 165, 2117, 2118, 8, GumpButtonType.Reply, 0);
            AddLabel(415, 185, 545, @"Bracelete Magico");
            AddButton(393, 185, 2117, 2118, 9, GumpButtonType.Reply, 0);
            AddLabel(415, 205, 545, @"Necklace Magico");
            AddButton(393, 205, 2117, 2118, 10, GumpButtonType.Reply, 0);
            AddLabel(415, 225, 545, @"Ring Magico");
            AddButton(393, 225, 2117, 2118, 11, GumpButtonType.Reply, 0);

            ////////AddLabel(415, 205, 545, @"Plate Magma/OldEnd");
            ////////AddButton(393, 205, 2117, 2118, 10, GumpButtonType.Reply, 0);
            ////////AddLabel(415, 225, 545, @"Fire/Poison Bow");
            ////////AddButton(393, 225, 2117, 2118, 11, GumpButtonType.Reply, 0);

            AddLabel(415, 245, 545, @"Poison Bow");
            AddButton(393, 245, 2117, 2118, 12, GumpButtonType.Reply, 0);
            AddLabel(415, 265, 545, @"Elven Bow");
            AddButton(393, 265, 2117, 2118, 13, GumpButtonType.Reply, 0);
            AddLabel(415, 285, 545, @"Ray Bow");
            AddButton(393, 285, 2117, 2118, 14, GumpButtonType.Reply, 0);
            AddLabel(415, 305, 545, @"Fire/Adv Poison Bow");
            AddButton(393, 305, 2117, 2118, 15, GumpButtonType.Reply, 0);

            AddLabel(442, 515, 37, @"Remover Ultimo");
            AddButton(446, 542, 2463, 2464, 99, GumpButtonType.Reply, 0);
        }

        private string GetRewardListHtml()
        {
            string htmlRewardList = string.Empty;

            int qtGold = 0;

            foreach (RewardItem rewardItem in SingletonEvent.Instance.CurrentEventRewardList)
            {

                if (rewardItem.RewardDescription == "Gold")
                {
                    qtGold += 5000;
                }
                else
                {
                    htmlRewardList += rewardItem.RewardDescription;
                    htmlRewardList += "<BR>";
                }

            }

            string returnHtmlRewardList = "<body><BASEFONT COLOR=#800000>";

            if (qtGold > 0)
            {
                returnHtmlRewardList += string.Format("{0} Gold", qtGold);
                returnHtmlRewardList += "<BR>";
            }

            returnHtmlRewardList += htmlRewardList;
            returnHtmlRewardList += "</body>";

            return returnHtmlRewardList;
        }


        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {

                case 1: // 5k Gold
                    {
                        RewardItem rewardItem = new RewardItem("Gold", new Type[] { typeof(Gold) });
                        SingletonEvent.Instance.CurrentEventRewardList.Add(rewardItem);
                        from.SendGump(new EventRewardGump(from));
                        break;
                    }
                case 2: // 2 Lings Myth/Aqua
                    {
                        RewardItem rewardItem = new RewardItem("Lings tipo Myth/Aqua/etc", new Type[] { typeof(MytherilIngot), typeof(AquaIngot) });
                        SingletonEvent.Instance.CurrentEventRewardList.Add(rewardItem);
                        from.SendGump(new EventRewardGump(from));
                        break;
                    }
                case 3: // 2 Lings OldEnd/GoldStone
                    {
                        RewardItem rewardItem = new RewardItem("Lings tipo OldEnd/GoldStone/etc", new Type[] { typeof(EnduriumIngot), typeof(OldEnduriumIngot), typeof(GoldStoneIngot) });
                        SingletonEvent.Instance.CurrentEventRewardList.Add(rewardItem);
                        from.SendGump(new EventRewardGump(from));
                        break;
                    }
                case 4: // Mustang/Orn/Oolock
                    {
                        RewardItem rewardItem = new RewardItem("Mount tipo Mustang/Orn/Oolock/Zostrich", RewardUtil.RegularMountTypes);
                        SingletonEvent.Instance.CurrentEventRewardList.Add(rewardItem);
                        from.SendGump(new EventRewardGump(from));
                        break;
                    }
                case 5: // Orn/Oclock Color
                    {
                        RewardItem rewardItem = new RewardItem("Mount tipo Orn/Oclock Color", RewardUtil.RareMountTypes);
                        SingletonEvent.Instance.CurrentEventRewardList.Add(rewardItem);
                        from.SendGump(new EventRewardGump(from));
                        break;
                    }
                case 6: // Arma Mytheril
                    {
                        RewardItem rewardItem = new RewardItem("Arma tipo Mytheril", RewardUtil.MytherilWeaponTypes);
                        SingletonEvent.Instance.CurrentEventRewardList.Add(rewardItem);
                        from.SendGump(new EventRewardGump(from));
                        break;
                    }
                case 7: // Plate Mytheril
                    {
                        RewardItem rewardItem = new RewardItem("Plate tipo Mytheril (peça)", RewardUtil.MytherilPlateTypes);
                        SingletonEvent.Instance.CurrentEventRewardList.Add(rewardItem);
                        from.SendGump(new EventRewardGump(from));
                        break;
                    }
                case 8: //Magic Dye Tub
                    {
                        RewardItem rewardItem = new RewardItem("Magic Dye Tub", new Type[] { typeof(MagicDyeTub) });
                        SingletonEvent.Instance.CurrentEventRewardList.Add(rewardItem);
                        from.SendGump(new EventRewardGump(from));
                        break;
                    }
                case 9: //Magic Bracelet
                    {
                        RewardItem rewardItem = new RewardItem("Magic Bracelet", RewardUtil.MagicBraceletType);
                        SingletonEvent.Instance.CurrentEventRewardList.Add(rewardItem);
                        from.SendGump(new EventRewardGump(from));
                        break;
                    }
                case 10: //Magic Necklace
                    {
                        RewardItem rewardItem = new RewardItem("Magic Necklace", RewardUtil.MagicNecklaceType);
                        SingletonEvent.Instance.CurrentEventRewardList.Add(rewardItem);
                        from.SendGump(new EventRewardGump(from));
                        break;
                    }
                case 11: //Magic Ring
                    {
                        RewardItem rewardItem = new RewardItem("Magic Ring", RewardUtil.MagicRingType);
                        SingletonEvent.Instance.CurrentEventRewardList.Add(rewardItem);
                        from.SendGump(new EventRewardGump(from));
                        break;
                    }
                case 12: //Poison Bow
                    {
                        RewardItem rewardItem = new RewardItem("Poison Bow", new Type[] { typeof(PoisonBow) });
                        SingletonEvent.Instance.CurrentEventRewardList.Add(rewardItem);
                        from.SendGump(new EventRewardGump(from));
                        break;
                    }
                case 13: //Elven Bow
                    {
                        RewardItem rewardItem = new RewardItem("Elven Bow", new Type[] { typeof(ElvenBow) });
                        SingletonEvent.Instance.CurrentEventRewardList.Add(rewardItem);
                        from.SendGump(new EventRewardGump(from));
                        break;
                    }
                case 14: //Ray Bow
                    {
                        RewardItem rewardItem = new RewardItem("Ray Bow", new Type[] { typeof(RayBow) });
                        SingletonEvent.Instance.CurrentEventRewardList.Add(rewardItem);
                        from.SendGump(new EventRewardGump(from));
                        break;
                    }
                case 15: //Fire/Adv Poison Bow
                    {
                        RewardItem rewardItem = new RewardItem("Bow tipo Fire/AdvPoison", new Type[] { typeof(FireBow), typeof(AdvancedPoisonBow) });
                        SingletonEvent.Instance.CurrentEventRewardList.Add(rewardItem);
                        from.SendGump(new EventRewardGump(from));
                        break;
                    }
                case 99: // Remover ultimo item
                    {
                        if (SingletonEvent.Instance.CurrentEventRewardList.Count > 0)
                            SingletonEvent.Instance.CurrentEventRewardList.RemoveAt(SingletonEvent.Instance.CurrentEventRewardList.Count-1);
                        from.SendGump(new EventRewardGump(from));
                        break;
                    }
                    

                case 0:
                default:
                    break;
            }
        }

    }
}