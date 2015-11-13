using System;
using Server;
using Server.Commands;
using Server.Mobiles;
using Server.Regions;
using Server.Gumps;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


using DimensionsNewAge.Scripts.Customs.Engines;

namespace Server.Items
{
    public class SurvivorScheduleStone : Item
    {


        // TheHunt Global Controle
        private static bool isAutomaticEvent;
        private static bool m_IsAutomaticEvent
        {
            get
            {
                return isAutomaticEvent;
            }
            set
            {
                isAutomaticEvent = value;
                SingletonEvent.Instance.IsAutomaticEvent = value;
            }
        }

        private static bool isAcceptingPlayers;
        private static bool m_IsAcceptingPlayers
        {
            get
            {
                return isAcceptingPlayers;
            }
            set
            {
                isAcceptingPlayers = value;
                SingletonEvent.Instance.IsAcceptingPlayers = value;
            }
        }


        [Constructable]
        public SurvivorScheduleStone()
            : base(0xEDC)
        {
            if (Check())
            {
                World.Broadcast(0x35, true, "Ja existe uma Survivor ScheduleStone no mundo !");
                Delete();
                return;
            }

            Name = "Survivor ScheduleStone";
            Movable = false;
            Hue = 0x7ad;

            InitializeSurvivorScheduleStone();
            ScheduleSurvivor();
        }

        private void InitializeSurvivorScheduleStone()
        {
            CommandSystem.Register("Survivor", AccessLevel.Player, new CommandEventHandler(CmdSurvivor_OnCommand));
            //CommandSystem.Register("startSurvivor", AccessLevel.GameMaster, new CommandEventHandler(CmdStartSurvivor_OnCommand));
            CommandSystem.Register("finishSurvivor", AccessLevel.GameMaster, new CommandEventHandler(CmdFinishSurvivor_OnCommand));
            CommandSystem.Register("Survivorstone", AccessLevel.GameMaster, new CommandEventHandler(CmdSurvivorStone_OnCommand));
            CommandSystem.Register("goSurvivor", AccessLevel.GameMaster, new CommandEventHandler(CmdGoSurvivor_OnCommand));
        }

        private bool Check()
        {
            foreach (Item item in World.Items.Values)
            {
                if (item is SurvivorScheduleStone && !item.Deleted && item != this)
                    return true;
            }

            return false;
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add(1060661, "Proximo Evento\t{0}", string.Format("em {0} horas e {1} minutos.", eventTimeSpan.Hours, eventTimeSpan.Minutes));
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.SendMessage("Agendamento Automatico Survivor");

            base.OnDoubleClick(from);
        }

        public SurvivorScheduleStone(Serial serial)
            : base(serial)
        {

        }

        TimeSpan eventTimeSpan;
        private void ScheduleSurvivor()
        {
            DateTime dtCurrent = DateTime.Now;
            DateTime dtEvent = new DateTime(dtCurrent.Year, dtCurrent.Month, dtCurrent.Day, 22, 00, 0);

            if (dtCurrent > dtEvent) // se ja rodou no dia atual
                dtEvent = dtEvent.AddDays(1);

            this.eventTimeSpan = dtEvent - dtCurrent;

            Logger.LogMessage(string.Format("Evento Agendado para daqui a {0} horas e {1} minutos. ({2})", eventTimeSpan.Hours, eventTimeSpan.Minutes, DateTime.Now.Add(eventTimeSpan).ToString()), "Survivor");

            // Timer para iniciar o evento
            Timer.DelayCall(eventTimeSpan, new TimerCallback(AutoStartEvent));

            // Timer para agendar o proximo após o termino deste
            Timer.DelayCall(eventTimeSpan.Add(TimeSpan.FromHours(1)), new TimerCallback(ScheduleSurvivor));
        }

        private static SurvivorStone currentSurvivorStone;
        public void AutoStartEvent()
        {
            // Sorteia alguma SurvivorStone pelo mundo
            List<SurvivorStone> survivorStoneList = new List<SurvivorStone>();
            foreach (Item item in World.Items.Values)
            {
                if (item is SurvivorStone && ((SurvivorStone)item).IsArenaConfigValid())
                {
                    survivorStoneList.Add((SurvivorStone)item);
                }
            }

            if (survivorStoneList.Count > 0)
            {
                currentSurvivorStone = survivorStoneList[new Random().Next(survivorStoneList.Count)];
                currentSurvivorStone.AnnounceAndStartSurvivor(null);
            }
            else
            {
                Logger.LogMessage("Survivor Automatico Cancelado. Nenhuma Arena Configurada.", "Survivor");
            }
        }

        public void CmdFinishSurvivor_OnCommand(CommandEventArgs e)
        {
            if (currentSurvivorStone != null && SingletonEvent.Instance.IsEventRunning)
                currentSurvivorStone.FinishSurvivorEvent();
        }

        public void CmdSurvivorStone_OnCommand(CommandEventArgs e)
        {
            if (currentSurvivorStone != null && SingletonEvent.Instance.IsEventRunning)
                e.Mobile.MoveToWorld(currentSurvivorStone.Location, currentSurvivorStone.Map);
        }

        public void CmdGoSurvivor_OnCommand(CommandEventArgs e)
        {
            if (currentSurvivorStone != null && SingletonEvent.Instance.IsEventRunning)
                e.Mobile.MoveToWorld(currentSurvivorStone.ArenaRespawnPoint, currentSurvivorStone.Map);
            else
                e.Mobile.MoveToWorld(this.Location, this.Map);
        }

        private int survivorStoneIndex = 0;
        public void CmdSurvivor_OnCommand(CommandEventArgs e)
        {
            if (e.Mobile.AccessLevel >= AccessLevel.GameMaster)
            {
                List<SurvivorStone> survivorStoneList = new List<SurvivorStone>();
                foreach (Item item in World.Items.Values)
                    if (item is SurvivorStone)
                        survivorStoneList.Add((SurvivorStone)item);

                if (survivorStoneList.Count > 0)
                {
                    if (survivorStoneIndex >= survivorStoneList.Count)
                        survivorStoneIndex = 0;

                    e.Mobile.MoveToWorld(survivorStoneList[survivorStoneIndex].Location, survivorStoneList[survivorStoneIndex].Map);
                    survivorStoneIndex++;
                }
                else
                {
                    e.Mobile.SendMessage("Nenhuma SurvivorStone localizada.");
                }

                return;
            }


            if (currentSurvivorStone != null && SingletonEvent.Instance.IsEventRunning)
                currentSurvivorStone.EnterEvent(e.Mobile);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            this.InitializeSurvivorScheduleStone();
            this.ScheduleSurvivor();
        }

    }
}