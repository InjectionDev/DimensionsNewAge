using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

using Server.Items;
using System.ComponentModel;
using System.Collections;

namespace DimensionsNewAge.Scripts.Customs.Engines
{

    public class EventModeGump : Gump
    {
        private Mobile caller;

        public EventModeGump(Mobile from)
            : this()
        {
            caller = from;

        }

        private EventModeGump()
            : base(200, 100)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddBackground(177, 9, 423, 459, 9270);
            AddLabel(303, 60, 42, @"D I M E N S I O N S");
            AddLabel(347, 80, 141, @"New Age");
            AddImageTiled(109, -49, 198, 181, 50992);
            AddImageTiled(398, -50, 198, 181, 50993);
            AddImage(126, 4, 10400);
            AddLabel(207, 137, 37, @"Configurações do Evento");
            AddLabel(232, 166, 545, @"Bloquear Magias");
            AddCheck(205, 166, 210, 211, SingletonEvent.Instance.BlockSpells, 100);
            AddLabel(232, 188, 545, @"Bloquear uso de Pots");
            AddCheck(205, 188, 210, 211, SingletonEvent.Instance.BlockPots, 101);
            AddLabel(232, 210, 545, @"Permitir Looting");
            AddCheck(205, 210, 210, 211, SingletonEvent.Instance.AllowLooting, 102);
            AddLabel(232, 232, 545, @"Army Mode");
            AddCheck(205, 232, 210, 211, SingletonEvent.Instance.IsArmyMode, 103);
            AddLabel(232, 254, 545, @"Bloquear Bow");
            AddCheck(205, 254, 210, 211, SingletonEvent.Instance.BlockBow, 104);
            AddLabel(232, 298, 545, @"Ativar Bad Macroer");
            AddCheck(205, 298, 210, 211, SingletonEvent.Instance.HasBadMacroerMode, 105);
            //AddLabel(232, 285, 545, @""); // livre
            AddLabel(232, 320, 545, @"Ativar Anti Panela");
            AddCheck(205, 320, 210, 211, SingletonEvent.Instance.HasAntiPanelaMode, 106);
            AddLabel(232, 342, 545, @"Ativar Anti Camper");
            AddCheck(205, 342, 210, 211, SingletonEvent.Instance.HasAntiCamperMode, 107);

            AddLabel(232, 384, 545, @"Modo Time");
            AddCheck(205, 384, 210, 211, SingletonEvent.Instance.IsTeamMode, 108);

            AddButton(205, 419, 247, 248, 1, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            caller = state.Mobile;

            switch (info.ButtonID)
            {
                case 0: 
                    {
                        return;
                    }

                default:
                    {
                        if (info.ButtonID == 1)
                        {
                            ArrayList Selections = new ArrayList(info.Switches);

                            if (Selections.Contains(100))
                                SingletonEvent.Instance.BlockSpells = true;
                            else
                                SingletonEvent.Instance.BlockSpells = false;

                            if (Selections.Contains(101))
                                SingletonEvent.Instance.BlockPots = true;
                            else
                                SingletonEvent.Instance.BlockPots = false;

                            if (Selections.Contains(102))
                                SingletonEvent.Instance.AllowLooting = true;
                            else
                                SingletonEvent.Instance.AllowLooting = false;

                            if (Selections.Contains(103))
                                SingletonEvent.Instance.IsArmyMode = true;
                            else
                                SingletonEvent.Instance.IsArmyMode = false;

                            if (Selections.Contains(104))
                                SingletonEvent.Instance.BlockBow = true;
                            else
                                SingletonEvent.Instance.BlockBow = false;

                            if (Selections.Contains(105))
                                SingletonEvent.Instance.HasBadMacroerMode = true;
                            else
                                SingletonEvent.Instance.HasBadMacroerMode = false;

                            if (Selections.Contains(106))
                                SingletonEvent.Instance.HasAntiPanelaMode = true;
                            else
                                SingletonEvent.Instance.HasAntiPanelaMode = false;

                            if (Selections.Contains(107))
                                SingletonEvent.Instance.HasAntiCamperMode = true;
                            else
                                SingletonEvent.Instance.HasAntiCamperMode = false;


                            if (Selections.Contains(108))
                            {
                                if (SingletonEvent.Instance.IsTeamMode != true && SingletonEvent.Instance.IsEventRunning)
                                    caller.SendMessage("Voce nao pode alterar o Modo Time com um evento em andamento.");
                                else
                                    SingletonEvent.Instance.IsTeamMode = true;
                            }
                            else
                            {
                                if (SingletonEvent.Instance.IsTeamMode != false && SingletonEvent.Instance.IsEventRunning)
                                    caller.SendMessage("Voce nao pode alterar o Modo Time com um evento em andamento.");
                                else
                                    SingletonEvent.Instance.IsTeamMode = false;
                            }

                        }
                    }
                    break;
            }
        }
    
    }
}






