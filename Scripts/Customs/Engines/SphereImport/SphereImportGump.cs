using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

using DimensionsNewAge.Scripts.Customs.Engines;
using System.Collections;

namespace DimensionsNewAge.Scripts.Customs.Engines.SphereImport
{

    public class SphereImportGump : Gump
    {
        Mobile caller;

        string textSphereAcc;
        string textSphereChar;
        string textRunUOAcc;
        string textRunUOChar;

        bool importSkills;
        bool importItensToPlayer;
        bool importItensToStaff;

        public SphereImportGump(Mobile from)
            : this()
        {
            caller = from;
            this.InitializeGump();
        }

        public SphereImportGump()
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
            AddBackground(16, 15, 668, 499, 9270);
            AddLabel(281, 43, 42, @"D I M E N S I O N S");
            AddLabel(308, 63, 141, @"New Age");
            AddImageTiled(-18, -44, 198, 181, 50992);
            AddImageTiled(441, -45, 198, 181, 50993);
            AddLabel(47, 200, 545, @"ACC Name Sphere");
            AddLabel(47, 267, 545, @"Char Name Sphere");
            //AddLabel(195, 149, 37, @"DIFERENCIAR LETRAS MINUSCULAS E MAIUSCULAS");
            AddLabel(251, 121, 141, @"S P H E R E    I M P O R T");
            AddLabel(368, 200, 545, @"ACC Name RunUO");
            AddLabel(368, 267, 545, @"Char Name RunUO");
            AddLabel(76, 333, 545, @"Importar Skills");
            AddCheck(48, 333, 210, 211, false, 20);
            AddCheck(48, 356, 210, 211, false, 21);
            AddCheck(48, 379, 210, 211, false, 22);
            AddLabel(76, 356, 545, @"Importar Itens -> Banco do Player");
            AddButton(53, 451, 247, 248, 1, GumpButtonType.Reply, 0);
            AddButton(163, 451, 241, 242, 0, GumpButtonType.Reply, 0);
            AddBackground(438, 421, 229, 76, 9350);
            AddLabel(451, 430, 42, @"Sphere to RunUO - Import System");
            AddLabel(461, 450, 141, @"Desenvolvido por:");
            AddLabel(471, 467, 141, @"Daniel Montenegro");
            AddBackground(44, 221, 272, 26, 9350);
            AddBackground(44, 287, 272, 26, 9350);
            AddBackground(365, 221, 272, 26, 9350);
            AddBackground(365, 287, 272, 26, 9350);
            AddTextEntry(52, 223, 250, 20, 0, 10, @"");
            AddTextEntry(52, 290, 250, 20, 0, 11, @"");
            AddTextEntry(374, 223, 250, 20, 0, 12, @"");
            AddTextEntry(374, 290, 250, 20, 0, 13, @"");
            AddLabel(76, 379, 545, @"Importar Itens -> Bag do Staff");

        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            this.caller = sender.Mobile;

            switch (info.ButtonID)
            {


                case 1:
                    {
                        TextRelay entrySphereAcc = info.GetTextEntry(10);
                        this.textSphereAcc = (entrySphereAcc == null ? "" : entrySphereAcc.Text.Trim());

                        TextRelay entrySphereChar = info.GetTextEntry(11);
                        this.textSphereChar = (entrySphereChar == null ? "" : entrySphereChar.Text.Trim());

                        TextRelay entryRunUOAcc = info.GetTextEntry(12);
                        this.textRunUOAcc = (entryRunUOAcc == null ? "" : entryRunUOAcc.Text.Trim());

                        TextRelay entryRunUOChar = info.GetTextEntry(13);
                        this.textRunUOChar = (entryRunUOChar == null ? "" : entryRunUOChar.Text.Trim());

                        ArrayList Selections = new ArrayList(info.Switches);

                        if (Selections.Contains(20))
                            this.importSkills = true;
                        else
                            this.importSkills = false;

                        if (Selections.Contains(21))
                            this.importItensToPlayer = true;
                        else
                            this.importItensToPlayer = false;

                        if (Selections.Contains(22))
                            this.importItensToStaff = true;
                        else
                            this.importItensToStaff = false;

                        StartImport();

                        break;
                    }


                case 0:
                default:
                    caller.SendMessage("Importacao CANCELADA.");
                    break;
            }
        }

        private void StartImport()
        {
            if (string.IsNullOrEmpty(this.textSphereAcc) || string.IsNullOrEmpty(this.textSphereChar) || string.IsNullOrEmpty(this.textRunUOAcc) || string.IsNullOrEmpty(this.textRunUOChar))
            {
                this.caller.SendMessage("Dados invalidos para importacao");
                return;
            }

            Logger.LogSphereImport(string.Format("Procurando ACC RunUO:{0} CHAR:{1}", this.textRunUOAcc, this.textRunUOChar), this.textSphereChar);
            bool existRunuoAcc = false;
            foreach (var mobile in World.Mobiles.Values)
            {
                if (mobile is PlayerMobile && ((PlayerMobile)mobile).RawName == this.textRunUOChar && ((PlayerMobile)mobile).Account.Username == this.textRunUOAcc)
                {
                    existRunuoAcc = true;
                    break;
                }
            }

            if (existRunuoAcc == false)
            {
                this.caller.SendMessage("Conta ou Char nao existe no RunUO");
                return;
            }

            SphereImport.SphereImportItem(this.caller, this.textSphereAcc, this.textSphereChar, this.textRunUOAcc, this.textRunUOChar, this.importSkills, this.importItensToPlayer, this.importItensToStaff);

        }

    }
}