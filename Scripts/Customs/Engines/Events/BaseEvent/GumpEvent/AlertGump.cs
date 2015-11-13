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

    public class AlertGump : Gump
    {
        private Mobile caller;

        private string m_text = string.Empty;
        private string m_title = "AVISO";

        public AlertGump(Mobile from, string text, string title)
            : this()
        {
            this.caller = from;

            this.m_text = "<body><BASEFONT COLOR=#800000>" + text + "</body>";

            if (!string.IsNullOrEmpty(title))
                this.m_title = title;

            this.InitializeGump();
        }

        private AlertGump()
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
            AddBackground(70, 30, 314, 253, 9270);
            AddLabel(162, 57, 42, this.m_title);
            AddHtml(92, 99, 271, 134, this.m_text, true, true);
            AddImage(97, 47, 9004);
            AddImage(23, 14, 10400);
            AddButton(94, 241, 247, 248, 0, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            caller = state.Mobile;

            switch (info.ButtonID)
            {
                case 0:
                default:
                    break;
            }
        }

    }
}






