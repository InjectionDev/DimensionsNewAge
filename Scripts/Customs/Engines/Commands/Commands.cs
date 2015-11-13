using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Factions;
using Server.Commands;
using System.Collections.Generic;

using Server.Mobiles;
using Server.Regions;
using Server.Gumps;
using DimensionsNewAge.Scripts.Customs.Engines;
using Server.Items;

namespace DimensionsNewAge.Scripts.Customs.Engines
{
    public class Commands
    {
        
        public static void Initialize()
        {


            CommandSystem.Register("goga", AccessLevel.GameMaster, new CommandEventHandler(GoGreenAcres_OnCommand));
            CommandSystem.Register("gogreenacres", AccessLevel.GameMaster, new CommandEventHandler(GoGreenAcres_OnCommand));

            CommandSystem.Register("goga2", AccessLevel.GameMaster, new CommandEventHandler(GoGreenAcres2_OnCommand));

            CommandSystem.Register("core", AccessLevel.GameMaster, new CommandEventHandler(Core_OnCommand));
            CommandSystem.Register("itensnull", AccessLevel.GameMaster, new CommandEventHandler(ItensNull_OnCommand));

            CommandSystem.Register("gosr", AccessLevel.GameMaster, new CommandEventHandler(GoStarRoom_OnCommand));
            CommandSystem.Register("gostarroom", AccessLevel.GameMaster, new CommandEventHandler(GoStarRoom_OnCommand));

            CommandSystem.Register("gotype", AccessLevel.GameMaster, new CommandEventHandler(GoType_OnCommand));
            CommandSystem.Register("walkdoor", AccessLevel.GameMaster, new CommandEventHandler(WalkDoor_OnCommand));
            CommandSystem.Register("gobrit", AccessLevel.GameMaster, new CommandEventHandler(GoBritain_OnCommand));

            CommandSystem.Register("entrar", AccessLevel.Player, new CommandEventHandler(JoinEvent_OnCommand));
            CommandSystem.Register("evento", AccessLevel.Player, new CommandEventHandler(JoinEvent_OnCommand));

            CommandSystem.Register("betatest", AccessLevel.Owner, new CommandEventHandler(BetaTeste_OnCommand));

            CommandSystem.Register("password", AccessLevel.Player, new CommandEventHandler(Password_OnCommand));

            CommandSystem.Register("online", AccessLevel.Player, new CommandEventHandler(Online_OnCommand));
            
        }

        public static void Online_OnCommand(CommandEventArgs e)
        {
            int userCount = NetState.Instances.Count;

            e.Mobile.SendMessage(365, string.Format("Neste momento existem {0} usuarios online.", userCount >= 2 ? userCount + 1 : userCount)); 
        }

        public static void Password_OnCommand(CommandEventArgs e)
        {
            if (e.Arguments.Length == 0)
            {
                e.Mobile.SendMessage("Voce nao informou a senha!");
                return;
            }
            else if (e.Arguments.Length > 1)
            {
                e.Mobile.SendMessage("Nao pode ter espaco na senha!");
                return;
            }
            else
            {
                e.Mobile.Account.SetPassword(e.Arguments[0]);
                e.Mobile.SendMessage("Senha alterada com Sucesso!");
                e.Mobile.SendMessage("Nova Senha: " + e.Arguments[0]);
                return;
            }
        }   

        public static void GoBritain_OnCommand(CommandEventArgs e)
        {
            e.Mobile.MoveToWorld(new Point3D(1419, 1699, 0), Map.Felucca);
        }

        public static void WalkDoor_OnCommand(CommandEventArgs e)
        {
            e.Mobile.BodyValue = 987;
        }

        public static void BetaTeste_OnCommand(CommandEventArgs e)
        {
            BagBetaTest bag = new BagBetaTest();
            e.Mobile.BankBox.DropItem(bag);

            for (int i = 0; i < e.Mobile.Skills.Length; ++i)
                if (e.Mobile.Skills[i].Base < 95)
                    e.Mobile.Skills[i].Base = 95;


            e.Mobile.SendGump(new DimensionsNewAge.Scripts.Customs.Engines.AlertGump(e.Mobile, "Obrigado por participar do Beta Teste, Dimensions New Age !<BR>Uma bag de recursos foi colocada em seu banco.", "DIMENS BETA TESTE"));
        }

        public static void JoinEvent_OnCommand(CommandEventArgs e)
        {
            if (SingletonEvent.Instance.IsEventRunning)
            {
                SendEventInvite(e.Mobile);
            }
            else
            {
                e.Mobile.SendMessage("Nenhum evento em andamento.");
            }
        }

        public static void SendEventInvite(Mobile mobile)
        {
            if (SingletonEvent.Instance.IsEventRunning)
            {
                switch (SingletonEvent.Instance.CurrentEventType)
                {
                    case EnumEventBase.EnumEventType.Survivor:
                        {
                            mobile.SendGump(new SurvivorInviteGump(mobile, (SurvivorStone)SingletonEvent.Instance.CurrentEventStone));
                            break;
                        }
                    case EnumEventBase.EnumEventType.TheHunt:
                        {
                            mobile.SendGump(new TheHuntInviteGump(mobile, (TheHuntStone)SingletonEvent.Instance.CurrentEventStone));
                            break;
                        }
                    default:

                        break;
                }
            }
        }

        public static void GoType_OnCommand(CommandEventArgs e)
        {
            if (e.Arguments.Length != 1)
            {
                e.Mobile.SendMessage("Argumentos invalidos, informe o Type");
                return;
            }

            string nmType = e.Arguments[0];
            foreach (Item item in World.Items.Values)
            {
                if (item.GetType().Name == nmType)
                {
                    e.Mobile.MoveToWorld(item.Location, item.Map);
                    return;
                }
            }

            e.Mobile.SendMessage("Nenhum item localizado com Type " + nmType);
        }

        public static void GoStarRoom_OnCommand(CommandEventArgs e)
        {
            Point3D starRoomPoint3D = new Point3D(5140, 1761, 5);
            e.Mobile.MoveToWorld(starRoomPoint3D, Map.Trammel);
        }


        public static void GoGreenAcres_OnCommand(CommandEventArgs e)
        {
            e.Mobile.MoveToWorld(new Point3D(5445, 1153, 0), Map.Felucca);
        }

        public static void GoGreenAcres2_OnCommand(CommandEventArgs e)
        {
            e.Mobile.MoveToWorld(new Point3D(5447, 1714, 0), Map.Felucca);
        }

        public static void Core_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage(string.Format("CORE.SE:{0}, CORE.AOS:{1}", Core.SE, Core.AOS));
        }

        public static void ItensNull_OnCommand(CommandEventArgs e)
        {
            string itensNull = string.Empty;
            foreach (Item item in World.Items.Values)
            {
                if (item.Location.X <= 0 || item.Location.Y <= 0)
                    itensNull += item.Name + ", ";
            }

            e.Mobile.SendMessage(string.Format("Itens Null: {0}", itensNull));
        }
    }
}