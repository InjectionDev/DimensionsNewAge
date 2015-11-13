using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

using DimensionsNewAge.Scripts.Customs.Engines;

namespace Server.Items
{

    public class SurvivorInviteGump : Gump
    {
        Mobile caller;

        private static SurvivorStone SurvivorStone;


        public SurvivorInviteGump(Mobile from, SurvivorStone pSurvivorStone)
            : this()
        {
            caller = from;

            SurvivorStone = pSurvivorStone;


        }


        public SurvivorInviteGump()
            : base(200, 100)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddBackground(177, 10, 518, 581, 9270);
            AddLabel(362, 60, 42, @"D I M E N S I O N S");
            AddLabel(406, 80, 141, @"New Age");
            AddImageTiled(121, -49, 198, 181, 50992);
            AddImageTiled(490, -50, 198, 181, 50993);
            AddImage(126, 4, 10400);
            AddLabel(207, 137, 37, @"Survivor");
            AddHtml(202, 167, 470, 147, @"<body><BASEFONT COLOR=#800000>Survivor é uma luta individual entre jogadores desequipados, onde sairá apenas um  campeão. <BR>Ao eliminar um inimigo o jogador terá o seu Hits/Stamina recuperado. <BR>Os participantes têm três vidas, e quando matam um inimigo ganham um vida (tendo máximo 03). <BR>Na luta final não são concedidas novas vidas. <BR></body>", true, true);
            AddLabel(207, 323, 37, @"Regras");
            AddHtml(202, 353, 470, 135, htmlInfo, true, true);
            AddLabel(207, 500, 37, @"Deseja participar do evento ?");
            AddButton(209, 534, 247, 248, 1, GumpButtonType.Reply, 0);
            AddButton(341, 534, 241, 242, 2, GumpButtonType.Reply, 0);
			
        }

        private string htmlInfo
        {
            get
            {
                string infos = "<BASEFONT COLOR=#D98719>* INFORMAÇÕES *<BASEFONT COLOR=#800000> <BR>";
                infos += SingletonEvent.Instance.AllowLooting ? "Looting Permitido!<BR>" : "Sem Looting<BR>";
                infos += SingletonEvent.Instance.BlockSpells ? "Bloqueado uso de Magias<BR>" : "Liberado uso de Magias<BR>";
                infos += SingletonEvent.Instance.BlockPots ? "Bloqueado uso de Poções<BR>" : "Liberado uso de Poções<BR>";
                infos += SingletonEvent.Instance.BlockBow ? "Bloqueado uso de Arcos<BR>" : "Liberado uso de Arcos<BR>";
                infos += SingletonEvent.Instance.IsArmyMode ? "Army Mode<BR>" : "Equipamento próprio<BR>";
                if (SingletonEvent.Instance.HasBadMacroerMode)
                    infos += "Bad Macroer Mode ON<BR>";
                if (SingletonEvent.Instance.HasAntiPanelaMode)
                    infos += "Anti Panela Mode ON<BR>";
                if (SingletonEvent.Instance.HasAntiCamperMode)
                    infos += "Anti Camper Mode ON<BR>";

                string premio = "<BASEFONT COLOR=#D98719>* PREMIO *<BASEFONT COLOR=#800000> <BR>";
                premio += RewardUtil.GetRewardHtmlString();
                premio += "<BASEFONT COLOR=#D98719>* Extra *<BASEFONT COLOR=#800000> <BR>5000 Gold bonus por Morte <BR>Troféu para Vencedor";

                return "<body><BASEFONT COLOR=#800000>" + infos + premio + "</body>";
            }
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {

                


                case 100: // Ir Invisivel para o evento
                    {
                        from.Hidden = true;
                        BaseEventHelper.GoEvent(from, EnumEventBase.EnumEventType.Survivor);
                        from.SendGump(this);
                        break;
                    }
                case 101: // Ir como Juiz para o evento
                    {

                        BaseEventHelper.GoEvent(from, EnumEventBase.EnumEventType.Survivor);
                        from.SendGump(this);
                        break;
                    }
                case 1:
                    {
                        SurvivorStone.EnterEvent(from);
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