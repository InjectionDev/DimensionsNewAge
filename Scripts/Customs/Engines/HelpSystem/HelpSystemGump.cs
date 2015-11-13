using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Menus;
using Server.Menus.Questions;
using Server.Accounting;
using Server.Multis;
using Server.Mobiles;
using Server.Items;

using Server.Engines.Help;

using DimensionsNewAge.Scripts.Customs.Engines;

namespace Server.Items
{

    public class HelpSystemGump : Gump
    {
        Mobile caller;


        public HelpSystemGump(Mobile from)
            : this()
        {
            caller = from;

            this.InitializeGump();
        }


        public HelpSystemGump()
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
            AddBackground(75, 10, 690, 464, 9270);
            AddLabel(472, 60, 42, @"D I M E N S I O N S");
            AddLabel(516, 80, 141, @"New Age");
            AddImageTiled(256, -31, 198, 181, 50992);
            AddImageTiled(553, -37, 198, 181, 50993);
            AddImage(27, -3, 10400);
            AddLabel(415, 220, 545, @"Estou com dúvidas ou problemas ingame.");
            AddLabel(415, 290, 545, @"Visite nosso Site");
            AddLabel(415, 190, 545, @"Estou preso no cenário, morto ou perdido.");
            AddLabel(376, 161, 37, @"Help System");
            AddImage(104, 35, 1800);
            AddImage(128, 72, 60987, 1959);
            AddLabel(415, 250, 545, @"Reportar um Bug");
            AddButton(379, 190, 1154, 1155, 1, GumpButtonType.Reply, 0);
            AddButton(379, 1220, 1154, 1155, 0, GumpButtonType.Reply, 0);
            AddButton(379, 220, 1154, 1155, 2, GumpButtonType.Reply, 0);
            AddButton(379, 250, 1154, 1155, 3, GumpButtonType.Reply, 0);
            AddButton(379, 290, 1154, 1155, 4, GumpButtonType.Reply, 0);
            AddBackground(546, 357, 206, 104, 9350);
            AddLabel(555, 366, 42, @"Dimensions New Age (2013)");
            AddLabel(565, 386, 141, @"Desenvolvido por:");
            AddLabel(575, 403, 141, @"Daniel Montenegro");
            AddLabel(565, 421, 141, @"Administrado por:");
            AddLabel(575, 438, 141, @"Magus Shadow");
            
            
            
        }

        public static bool CheckCombat(Mobile m)
        {
            for (int i = 0; i < m.Aggressed.Count; ++i)
            {
                AggressorInfo info = m.Aggressed[i];

                if (DateTime.Now - info.LastCombatTime < TimeSpan.FromSeconds(30.0))
                    return true;
            }

            return false;
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            PageType type = (PageType)(-1);

            switch (info.ButtonID)
            {

               

                case 1:
                    {
                        //BaseHouse house = BaseHouse.FindHouseAt(from);

                        //if (house != null && house.IsAosRules)
                        //{
                        //    from.Location = house.BanLocation;
                        //}
                        //else if (from.Region.IsPartOf(typeof(Server.Regions.Jail)))
                        //{
                        //    from.SendLocalizedMessage(1041530, "", 0x35); // You'll need a better jailbreak plan then that!
                        //}
                        //else if (Factions.Sigil.ExistsOn(from))
                        //{
                        //    from.SendLocalizedMessage(1061632); // You can't do that while carrying the sigil.
                        //}
                        //else if (from.CanUseStuckMenu() && from.Region.CanUseStuckMenu(from) && !CheckCombat(from) && !from.Frozen && !from.Criminal && (Core.AOS || from.Kills < 5))
                        //{
                        //    StuckMenu menu = new StuckMenu(from, from, true);

                        //    menu.BeginClose();

                        //    from.SendGump(menu);
                        //}
                        //else
                        //{
                        //    type = PageType.Stuck;
                        //}

                        if (from.Region.IsPartOf(typeof(Server.Regions.Jail)))
                        {
                            from.SendLocalizedMessage(1041530, "", 0x35); // You'll need a better jailbreak plan then that!
                            return;
                        }

                        from.SendMessage("Voce sera Teleportado para StarRoom em aproximadamente 2 minutos.");

                        new TeleportTimer(from, TimeSpan.FromMinutes(2.5)).Start();

                        break;

                    }
                case 2:
                    type = PageType.Question;
                    break;
                case 3:
                    type = PageType.Bug;
                    break;
                case 4:
                    caller.LaunchBrowser(@"http://www.dimensnewage.com.br");
                    break;

                case 0:
                default:
                    break;
            }

            if (type != (PageType)(-1) && PageQueue.CheckAllowedToPage(caller))
                caller.SendGump(new PagePromptGump(caller, type));
        }



    }

    internal class TeleportTimer : Timer
    {
        private Mobile m_Mobile;
        private DateTime m_End;

        public TeleportTimer(Mobile mobile, TimeSpan delay)
            : base(TimeSpan.Zero, TimeSpan.FromSeconds(1.0))
        {
            Priority = TimerPriority.TwoFiftyMS;

            m_Mobile = mobile;
            m_End = DateTime.Now + delay;
        }

        protected override void OnTick()
        {
            if (DateTime.Now < m_End)
            {
                m_Mobile.Frozen = true;
            }
            else
            {
                m_Mobile.Frozen = false;
                Stop();

                if (Factions.Sigil.ExistsOn(m_Mobile))
                {
                    m_Mobile.SendLocalizedMessage(1061632); // You can't do that while carrying the sigil.
                    return;
                }

                if (m_Mobile.Alive == false)
                    m_Mobile.Resurrect();

                Point3D dest = new Point3D(5140, 1761, 5);
                Map destMap = Map.Trammel;

                Mobiles.BaseCreature.TeleportPets(m_Mobile, dest, destMap);
                m_Mobile.MoveToWorld(dest, destMap);
            }
        }
    }
}