using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

using DimensionsNewAge.Scripts.Customs.Engines;

namespace Server.Items
{

    public class TheHuntGump : Gump
    {
        Mobile caller;

        private static TheHuntStone theHuntStone;


        public TheHuntGump(Mobile from, TheHuntStone pTheHuntStone)
            : this()
        {
            caller = from;

            theHuntStone = pTheHuntStone;

        }

        public TheHuntGump()
            : base(200, 100)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddBackground(177, 79, 423, 440, 9270);
            AddLabel(303, 129, 42, @"D I M E N S I O N S");
            AddLabel(347, 149, 141, @"New Age");
            AddImageTiled(109, 20, 198, 181, 50992);
            AddImageTiled(398, 19, 198, 181, 50993);
            AddImage(126, 73, 10400);
            AddLabel(207, 206, 37, @"The Hunt");
            AddLabel(256, 240, 545, @"Ir Invisivel para o evento.");
            AddLabel(256, 270, 545, @"Ir como Juiz para o evento.");
            AddLabel(256, 300, 545, @"Definir Premio");
            AddLabel(256, 330, 545, @"Definir Configurações do Evento");
            //AddLabel(256, 360, 545, @"Definir Modos"); // reservado
            AddLabel(256, 400, 545, @"Anunciar o Evento");
            AddButton(215, 240, 1154, 1153, 100, GumpButtonType.Reply, 0);
            AddButton(215, 270, 1154, 1153, 101, GumpButtonType.Reply, 0);
            AddButton(215, 300, 1154, 1153, 5, GumpButtonType.Reply, 0);
            AddButton(215, 330, 1154, 1153, 6, GumpButtonType.Reply, 0);

            AddButton(215, 400, 1154, 1153, 1, GumpButtonType.Reply, 0);
            //AddButton(215, 360, 1154, 1153, 0, GumpButtonType.Reply, 0); // reservado
            AddLabel(256, 430, 545, @"Cancelar o Evento");
            AddButton(215, 430, 1154, 1153, 2, GumpButtonType.Reply, 0);
			
        }


        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {

                

                case 100: // Ir Invisivel para o evento
                    {
                        from.Hidden = true;
                        BaseEventHelper.GoEvent(from, EnumEventBase.EnumEventType.TheHunt);
                        from.SendGump(this);
                        break;
                    }
                case 101: // Ir como Juiz para o evento
                    {

                        BaseEventHelper.GoEvent(from, EnumEventBase.EnumEventType.TheHunt);
                        from.SendGump(this);
                        break;
                    }
                case 1:
                    {
                        theHuntStone.AnnounceAndStartTheHunt(from);
                        from.SendGump(this);
                        break;

                    }
                case 2:
                    {
                        theHuntStone.FinishTheHuntEvent(from);
                        from.SendGump(this);
                        break;

                    }

                case 5:
                    {
                        from.SendGump(this);
                        from.SendGump(new EventRewardGump(from));
                        break;

                    }
                case 6:
                    {
                        from.SendGump(this);
                        from.SendGump(new EventModeGump(from));
                        break;

                    }

                case 0:
                default:
                    break;
            }
        }

    }
}