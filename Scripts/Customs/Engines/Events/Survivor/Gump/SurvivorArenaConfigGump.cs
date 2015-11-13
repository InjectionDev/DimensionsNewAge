using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

using Server.Items;
using System.ComponentModel;
using DimensionsNewAge.Scripts.Customs.Engines.Events;
using Server.Targeting;

namespace DimensionsNewAge.Scripts.Customs.Engines
{

    public class SurvivorArenaConfigGump : Gump
    {
        private Mobile caller;
        private static SurvivorStone survivorStone;

        public SurvivorArenaConfigGump(Mobile from, SurvivorStone pSurvivorStone)
            : this()
        {
            caller = from;
            survivorStone = pSurvivorStone;

            this.InitializeGump();
        }

        public SurvivorArenaConfigGump()
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
            AddBackground(178, 8, 423, 538, 9270);
            AddLabel(303, 60, 42, @"D I M E N S I O N S");
            AddLabel(347, 80, 141, @"New Age");
            AddImageTiled(109, -49, 198, 181, 50992);
            AddImageTiled(398, -50, 198, 181, 50993);
            AddImage(126, 4, 10400);
            AddLabel(207, 137, 37, @"Configuração Arena");

            AddButton(205, 165, 2117, 2118, 1, GumpButtonType.Reply, 0);
            AddLabel(225, 162, 545, @"Alterar Nome da Arena");
            
            AddLabel(233, 181, string.IsNullOrEmpty(survivorStone.ArenaName) ? 252 : 167, string.IsNullOrEmpty(survivorStone.ArenaName) ? "Sem Nome" : "Arena: " + survivorStone.ArenaName);


            //AddButton(205, 215, 2117, 2118, 2, GumpButtonType.Reply, 0);
            AddLabel(205, 212, 545, @"Definir Area da Arena (2 cliques, para formar o retangulo)");

            AddLabel(233, 231, (survivorStone.ArenaAreaAPoint.X == 0 && survivorStone.ArenaAreaAPoint.Y == 0) ? 252 : 167, @"Clique ponto A");
            AddButton(214, 234, 2117, 2118, 21, GumpButtonType.Reply, 0);
            AddLabel(233, 251, (survivorStone.ArenaAreaBPoint.X == 0 && survivorStone.ArenaAreaBPoint.Y == 0) ? 252 : 167, @"Clique ponto B");
            AddButton(214, 254, 2117, 2118, 22, GumpButtonType.Reply, 0);

            if (survivorStone.ArenaAreaAPoint.X == 0 && survivorStone.ArenaAreaAPoint.Y == 0)
            {
                AddLabel(233, 271, 252, "Area não definida");
            }
            else if (survivorStone.ArenaAreaBPoint.X == 0 && survivorStone.ArenaAreaBPoint.Y == 0)
            {
                AddLabel(233, 271, 252, "Area não definida");
            }
            else
            {
                AddLabel(233, 271, (survivorStone.ArenaArea.Height == 0) ? 252 : 167, (survivorStone.ArenaArea.Height == 0) ? "Area não definida" : string.Format("Area definida. {0} tiles", survivorStone.ArenaArea.Height + survivorStone.ArenaArea.Width));
            }


            AddButton(205, 304, 2117, 2118, 3, GumpButtonType.Reply, 0);
            AddLabel(225, 301, 545, @"Definir Spawn Central");
            AddLabel(233, 320, Convert.ToInt32(this.ArenaRespawnPointDesc.Split('|')[0]), this.ArenaRespawnPointDesc.Split('|')[1]);

            AddButton(205, 354, 2117, 2118, 4, GumpButtonType.Reply, 0);
            AddLabel(225, 351, 545, @"Definir Spawn Time A");
            AddLabel(233, 370, Convert.ToInt32(this.ArenaRespawnAPointDesc.Split('|')[0]), this.ArenaRespawnAPointDesc.Split('|')[1]);

            AddButton(205, 404, 2117, 2118, 5, GumpButtonType.Reply, 0);
            AddLabel(225, 401, 545, @"Definir Spawn Time B");
            AddLabel(233, 420, Convert.ToInt32(this.ArenaRespawnBPointDesc.Split('|')[0]), this.ArenaRespawnBPointDesc.Split('|')[1]);

            AddButton(208, 495, 247, 248, 0, GumpButtonType.Reply, 0);
        }

        private string ArenaRespawnPointDesc
        {
            get { 
                
                if (survivorStone.ArenaRespawnPoint.X == 0 && survivorStone.ArenaRespawnPoint.Y == 0)
                    return "252|Spawn não definido";

                if (survivorStone.ArenaArea.Contains(survivorStone.ArenaRespawnPoint) == false)
                    return "252|Spawn fora da Arena";

                return string.Format("167|Spawn definido. {0}/{1}/{2}", survivorStone.ArenaRespawnPoint.X, survivorStone.ArenaRespawnPoint.Y, survivorStone.ArenaRespawnPoint.Z);
            }
        }

        private string ArenaRespawnAPointDesc
        {
            get { 
                
                if (survivorStone.ArenaRespawnAPoint.X == 0 && survivorStone.ArenaRespawnAPoint.Y == 0)
                    return "252|Spawn não definido";

                if (survivorStone.ArenaArea.Contains(survivorStone.ArenaRespawnAPoint) == false)
                    return "252|Spawn fora da Arena";

                return string.Format("167|Spawn definido. {0}/{1}/{2}", survivorStone.ArenaRespawnAPoint.X, survivorStone.ArenaRespawnAPoint.Y, survivorStone.ArenaRespawnAPoint.Z);
            }
        }

        private string ArenaRespawnBPointDesc
        {
            get { 
                
                if (survivorStone.ArenaRespawnBPoint.X == 0 && survivorStone.ArenaRespawnBPoint.Y == 0)
                    return "252|Spawn não definido";

                if (survivorStone.ArenaArea.Contains(survivorStone.ArenaRespawnBPoint) == false)
                    return "252|Spawn fora da Arena";

                return string.Format("167|Spawn definido. {0}/{1}/{2}", survivorStone.ArenaRespawnBPoint.X, survivorStone.ArenaRespawnBPoint.Y, survivorStone.ArenaRespawnBPoint.Z);
            }
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {

                case 1: 
                    {
                        caller.Prompt = new SetArenaNamePrompt(survivorStone);
                        caller.SendMessage("Digite o nome da arena.");
                        break;
                    }
                case 2:
                    {
                        caller.SendMessage("Forme o retangulo da arena com 2 cliques.");
                        BoundingBoxPicker.Begin(caller, new BoundingBoxCallback(SetBoundingBoxArea_Callback), survivorStone);
                        break;
                    }
                case 21:
                    {
                        from.BeginTarget(-1, true, TargetFlags.None, new TargetStateCallback(EndArenaAreaAPoint), survivorStone);
                        caller.SendMessage("Clique na Primeira Posicao para formar a Area da Arena.");
                        break;
                    }
                case 22:
                    {
                        from.BeginTarget(-1, true, TargetFlags.None, new TargetStateCallback(EndArenaAreaBPoint), survivorStone);
                        caller.SendMessage("Clique na Segunda Posicao para formar a Area da Arena.");
                        break;
                    }
                case 3:
                    {
                        from.BeginTarget(-1, true, TargetFlags.None, new TargetStateCallback(EndArenaRespawnPointTarget), survivorStone);
                        caller.SendMessage("Clique na Posicao Central da Arena.");
                        break;
                    }
                case 4:
                    {
                        from.BeginTarget(-1, true, TargetFlags.None, new TargetStateCallback(EndArenaRespawnPointATarget), survivorStone);
                        caller.SendMessage("Clique na Posicao Time A da Arena.");
                        break;
                    }
                case 5:
                    {
                        from.BeginTarget(-1, true, TargetFlags.None, new TargetStateCallback(EndArenaRespawnPointBTarget), survivorStone);
                        caller.SendMessage("Clique na Posicao Time A da Arena.");
                        break;
                    }


                case 0:
                default:
                    from.SendGump(new SurvivorGump(from, survivorStone));
                    break;
            }
        }

        public void EndArenaRespawnPointTarget(Mobile from, object target, object pSurvivorStone)
        {
            IPoint3D p = target as IPoint3D;

            if (p == null)
                return;

            ((SurvivorStone)pSurvivorStone).ArenaRespawnPoint = new Point3D(p);
            from.SendGump(new SurvivorArenaConfigGump(from, (SurvivorStone)pSurvivorStone));
        }

        public void EndArenaRespawnPointATarget(Mobile from, object target, object pSurvivorStone)
        {
            IPoint3D p = target as IPoint3D;

            if (p == null)
                return;

            ((SurvivorStone)pSurvivorStone).ArenaRespawnAPoint = new Point3D(p);
            from.SendGump(new SurvivorArenaConfigGump(from, (SurvivorStone)pSurvivorStone));
        }

        public void EndArenaRespawnPointBTarget(Mobile from, object target, object pSurvivorStone)
        {
            IPoint3D p = target as IPoint3D;

            if (p == null)
                return;

            ((SurvivorStone)pSurvivorStone).ArenaRespawnBPoint = new Point3D(p);
            from.SendGump(new SurvivorArenaConfigGump(from, (SurvivorStone)pSurvivorStone));
        }

        private static void SetBoundingBoxArea_Callback(Mobile mob, Map map, Point3D start, Point3D end, object pSurvivorStone)
		{
            //((SurvivorStone)pSurvivorStone).ArenaArea = new Rectangle2D(new Point2D(start.X, start.Y), new Point2D(end.X + 1, end.Y + 1));
            //mob.SendMessage("Area da Arena Alterado.");

            //mob.SendGump(new SurvivorArenaConfigGump(mob, (SurvivorStone)pSurvivorStone));
		}

        public void EndArenaAreaAPoint(Mobile from, object target, object pSurvivorStone)
        {
            IPoint3D p = target as IPoint3D;

            if (p == null)
                return;

            ((SurvivorStone)pSurvivorStone).ArenaAreaAPoint = new Point3D(p);
            from.SendGump(new SurvivorArenaConfigGump(from, (SurvivorStone)pSurvivorStone));
        }

        public void EndArenaAreaBPoint(Mobile from, object target, object pSurvivorStone)
        {
            IPoint3D p = target as IPoint3D;

            if (p == null)
                return;

            ((SurvivorStone)pSurvivorStone).ArenaAreaBPoint = new Point3D(p);
            from.SendGump(new SurvivorArenaConfigGump(from, (SurvivorStone)pSurvivorStone));
        }
    }

    public class InternalTarget : Target
    {

        private Mobile m_Owner;

        public InternalTarget(Mobile from)
            : base(100, true, TargetFlags.None)
        {
            m_Owner = from;
        }

        protected override void OnTarget(Mobile from, object o)
        {
            IPoint3D p = o as IPoint3D;


        }

    }


    public class SetArenaNamePrompt : Server.Prompts.Prompt
    {
        private SurvivorStone survivorStone;

        public SetArenaNamePrompt(SurvivorStone pSurvivorStone)
        {
            survivorStone = pSurvivorStone;
        }

        public override void OnResponse(Mobile from, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                survivorStone.ArenaName = string.Empty;
                from.SendMessage("Nome Invalido.");
            }
            else
            {
                survivorStone.ArenaName = text.Trim();
                from.SendMessage("Nome da Arena Alterado.");
            }

            from.SendGump(new SurvivorArenaConfigGump(from, survivorStone));
        }
    }
}