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
    public class TheHuntStone : Item
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

        private static bool isTheHuntRunning;
        private static bool m_IsTheHuntRunning
        {
            get
            {
                return isTheHuntRunning;
            }
            set
            {
                isTheHuntRunning = value;
                SingletonEvent.Instance.IsEventRunning = value;
            }
        }


        // TheHunt Event Controle
        private static int m_CountDownToStartEvent { get; set; }
        private static int m_CountDownToStartFight { get; set; }

        //TheHunt Players
        private static List<Mobile> m_TheHuntPlayerList;
        private static List<Point3D> m_PlayerSpotList;
        private static Dictionary<Mobile, Point3D> m_TheHuntPlayerHomeLocationList;
        private static Dictionary<Mobile, Map> m_TheHuntPlayerHomeLocationMapList;
        private BaseEventStatistics eventStatistics;

        // TheHunt Region
        private static TheHuntRegion m_TheHuntRegion;

        [Constructable]
        public TheHuntStone() : base(0xEDC)
        {
            if (Check())
            {
                World.Broadcast(0x35, true, "Ja existe uma TheHuntStone no mundo !");
                Delete();
                return;
            }

            Name = "TheHunt Stone";
            Movable = false;
            Hue = 0x480;

            this.InitializeTheHuntStone();
        }

        private bool Check()
        {
            foreach (Item item in World.Items.Values)
            {
                if (item is TheHuntStone && !item.Deleted && item != this)
                    return true;
            }

            return false;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel >= AccessLevel.GameMaster)
                from.SendGump(new TheHuntGump(from, this));
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add(1060658, "Running Now\t{0}", m_IsTheHuntRunning.ToString());
            list.Add(1060659, "Aceitando Players\t{0}", m_IsAcceptingPlayers.ToString());
            list.Add(1060660, "Participantes\t{0}", m_TheHuntPlayerList.Count);
            list.Add(1060661, "Proximo Evento\t{0}", string.Format("em {0} horas e {1} minutos.", eventTimeSpan.Hours, eventTimeSpan.Minutes));
        }

        public TheHuntStone(Serial serial)
            : base(serial)
		{
		}

        private void InitializeTheHuntStone()
        {
            //Logger.LogMessage("InitializeTheHuntStone start", "TheHunt");

            m_PlayerSpotList = new List<Point3D>();
            m_TheHuntPlayerList = new List<Mobile>();
            m_TheHuntPlayerHomeLocationList = new Dictionary<Mobile, Point3D>();
            m_TheHuntPlayerHomeLocationMapList = new Dictionary<Mobile, Map>();

            SingletonEvent.Instance.CurrentEventLocation = new Point3D(47, 836, -28);
            SingletonEvent.Instance.CurrentEventMap = Map.Ilshenar;

            this.ForceRegionClean();

            // Timer para o evento automatico
            // Evento Diario as 21:00
            this.ScheduleTheHunt();

            CommandSystem.Register("thehunt", AccessLevel.Player, new CommandEventHandler(CmdTheHunt_OnCommand));
            CommandSystem.Register("startthehunt", AccessLevel.GameMaster, new CommandEventHandler(CmdStartTheHunt_OnCommand));
            CommandSystem.Register("finishthehunt", AccessLevel.GameMaster, new CommandEventHandler(CmdFinishTheHunt_OnCommand));
            CommandSystem.Register("thehuntstone", AccessLevel.GameMaster, new CommandEventHandler(CmdTheHuntStone_OnCommand));
            CommandSystem.Register("gothehunt", AccessLevel.GameMaster, new CommandEventHandler(CmdGoTheHunt_OnCommand));

            //Logger.LogMessage("InitializeTheHuntStone finish", "TheHunt");
        }

        TimeSpan eventTimeSpan;
        private void ScheduleTheHunt()
        {
            
            DateTime dtCurrent = DateTime.Now;
            DateTime dtEvent = new DateTime(dtCurrent.Year, dtCurrent.Month, dtCurrent.Day, 21, 00, 0);

            if (dtCurrent > dtEvent) // se ja rodou no dia atual
                dtEvent = dtEvent.AddDays(1);

            this.eventTimeSpan = dtEvent - dtCurrent;

            Logger.LogMessage(string.Format("Evento Agendado para daqui a {0} horas e {1} minutos. ({2})", eventTimeSpan.Hours, eventTimeSpan.Minutes, DateTime.Now.Add(eventTimeSpan).ToString()), "TheHunt");
            
            // Timer para iniciar o evento
            Timer.DelayCall(eventTimeSpan, new TimerCallback(AutoStartEvent));

            // Timer para agendar o proximo após o termino deste
            Timer.DelayCall(eventTimeSpan.Add(TimeSpan.FromHours(1)), new TimerCallback(ScheduleTheHunt));
        }

        public void AnnounceAndStartTheHunt(Mobile caller)
        {
            
            if (SingletonEvent.Instance.IsEventRunning)
            {
                if (caller != null)
                    caller.SendMessage("Um evento ja esta em andamento. Solicitacao ignorada.");

                return;
            }

            this.InitializeTheHuntRegion();

            if (caller == null)
            {
                m_IsAutomaticEvent = true;
                SingletonEvent.Instance.AllowLooting = false;
                SingletonEvent.Instance.BlockBow = false;
                SingletonEvent.Instance.BlockPots = false;
                SingletonEvent.Instance.BlockSpells = false;
                SingletonEvent.Instance.HasAntiCamperMode = true;
                SingletonEvent.Instance.HasAntiPanelaMode = true;
                SingletonEvent.Instance.HasBadMacroerMode = false;
            }
            else
            {
                m_IsAutomaticEvent = false;
            }

            InitializePlayerSpotList();
            m_TheHuntPlayerList.Clear();
            m_TheHuntPlayerHomeLocationList.Clear();
            m_TheHuntPlayerHomeLocationMapList.Clear();

            this.eventStatistics = new BaseEventStatistics();

            m_IsTheHuntRunning = true;
            m_IsAcceptingPlayers = true;
            SingletonEvent.Instance.CurrentEventStone = this;
            SingletonEvent.Instance.CurrentEventType = EnumEventBase.EnumEventType.TheHunt;

            m_CountDownToStartEvent = 5; // CountDown de 5 minutos para inicio do evento
            m_CountDownToStartFight = 10; // CountDown de 10 segundos para inicio da luta

            Logger.LogMessage("Evento TheHunt Iniciado!", "TheHunt");
            World.Broadcast(0x35, true, "Evento The Hunt Iniciado!");
            World.Broadcast(0x35, true, "Digite .entrar para participar!");

            foreach (Mobile mobile in World.Mobiles.Values)
            {
                if (mobile is PlayerMobile)
                    mobile.SendGump(new TheHuntInviteGump(mobile, this));
            }

            Timer.DelayCall(TimeSpan.FromMinutes(4.0), new TimerCallback(BcastTimeLeftToStartEvent));
            Timer.DelayCall(TimeSpan.FromMinutes(3.0), new TimerCallback(BcastTimeLeftToStartEvent));
            Timer.DelayCall(TimeSpan.FromMinutes(2.0), new TimerCallback(BcastTimeLeftToStartEvent));
            Timer.DelayCall(TimeSpan.FromMinutes(1.0), new TimerCallback(BcastTimeLeftToStartEvent));

            Timer.DelayCall(TimeSpan.FromMinutes(5.0), new TimerCallback(StartTheHunt));
        }

        public void BcastTimeLeftToStartEvent()
        {
            m_CountDownToStartEvent -= 1;
            World.Broadcast(0x35, true, string.Format("The Hunt Iniciando em {0} minutos...", m_CountDownToStartEvent));
            World.Broadcast(0x35, true, "Digite .entrar para participar!");
        }

        public void BcastTimeLeftToStartFight()
        {
            m_CountDownToStartFight -= 1;
            this.BroadcastToPlayersList(string.Format("Inicio em {0} segundos...", m_CountDownToStartFight));
        }

        private void StartTheHunt()
        {

            if (m_TheHuntPlayerList.Count < 2)
            {
                Logger.LogMessage("TheHunt cancelado por falta de jogadores.", "TheHunt");
                World.Broadcast(0x35, true, "TheHunt cancelado por falta de jogadores.");
                m_IsAcceptingPlayers = false;
                m_IsTheHuntRunning = false;
                return;
            }
            else
            {
                World.Save();

                m_IsAcceptingPlayers = false;
                World.Broadcast(0x35, true, "Inscricoes encerradas para TheHunt!");
            }

            this.PreparePlayersToFight();

            Timer.DelayCall(TimeSpan.FromSeconds(30.0), new TimerCallback(ReleasePlayers));

            Timer.DelayCall(TimeSpan.FromSeconds(11.0), new TimerCallback(BcastTimeLeftToStartFight));
            Timer.DelayCall(TimeSpan.FromSeconds(12.0), new TimerCallback(BcastTimeLeftToStartFight));
            Timer.DelayCall(TimeSpan.FromSeconds(13.0), new TimerCallback(BcastTimeLeftToStartFight));
            Timer.DelayCall(TimeSpan.FromSeconds(14.0), new TimerCallback(BcastTimeLeftToStartFight));
            Timer.DelayCall(TimeSpan.FromSeconds(15.0), new TimerCallback(BcastTimeLeftToStartFight));
            Timer.DelayCall(TimeSpan.FromSeconds(16.0), new TimerCallback(BcastTimeLeftToStartFight));
            Timer.DelayCall(TimeSpan.FromSeconds(17.0), new TimerCallback(BcastTimeLeftToStartFight));
            Timer.DelayCall(TimeSpan.FromSeconds(18.0), new TimerCallback(BcastTimeLeftToStartFight));
            Timer.DelayCall(TimeSpan.FromSeconds(29.0), new TimerCallback(BcastTimeLeftToStartFight));

            Timer.DelayCall(TimeSpan.FromMinutes(25.0), new TimerCallback(FinishTheHuntEvent));
        }


        private void PreparePlayersToFight()
        {
            foreach (Mobile mobile in m_TheHuntPlayerList)
            {
                PlayerMobile player = World.FindMobile(mobile.Serial) as PlayerMobile;

                this.CleanPlayer(player);

                player.Blessed = true;
                player.Paralyzed = true;
                player.Hidden = true;

                if (player.Alive == false)
                    player.Resurrect();

                IMount mount = player.Mount;
                if (mount != null)
                {
                    mount.Rider = null;
                    ShrinkItem shrunkenPet = new ShrinkItem((BaseCreature)mount);
                    player.Backpack.DropItem(shrunkenPet);
                    mobile.SendMessage("Este evento nao permite entrar com animais.");
                    mobile.SendMessage("Ele foi guardado e seu banco.");
                }

                // Salva posicao antes de puxar para o local do evento
                m_TheHuntPlayerHomeLocationList.Add(mobile, mobile.Location);
                m_TheHuntPlayerHomeLocationMapList.Add(mobile, mobile.Map);

                Point3D point3D = m_PlayerSpotList[new Random().Next(m_PlayerSpotList.Count)];
                m_PlayerSpotList.Remove(point3D);

                player.FixedParticles(0x3709, 1, 30, 9965, 5, 7, EffectLayer.Waist);
                player.MoveToWorld(point3D, Map.Ilshenar);
                player.FixedParticles(0x3709, 1, 30, 9965, 5, 7, EffectLayer.Waist);

            }
        }

        private void ReleasePlayers()
        {
            foreach (Mobile mobile in m_TheHuntPlayerList)
            {
                Mobile player = World.FindMobile(mobile.Serial);

                player.Blessed = false;
                player.Paralyzed = false;
                player.Hidden = false;
                player.Criminal = true;

                player.FixedParticles(0x3709, 1, 30, 9965, 5, 7, EffectLayer.Waist);
            }

            World.Broadcast(0x35, true, string.Format("TheHunt iniciado com {0} participantes!", m_TheHuntPlayerList.Count));
            World.Broadcast(0x35, true, string.Format("Que venca o melhor!"));
        }

        private void BroadcastToPlayersList(string pMessage)
        {
            foreach (Mobile mobile in m_TheHuntPlayerList)
            {
                Mobile player = World.FindMobile(mobile.Serial);

                if (player != null)
                    player.SendMessage(0x35, pMessage);
            }
        }

        private void InitializePlayerSpotList()
        {
            m_PlayerSpotList.Clear();
            m_PlayerSpotList.Add(new Point3D(10, 897, -30));
            m_PlayerSpotList.Add(new Point3D(45, 836, -29));
            m_PlayerSpotList.Add(new Point3D(126, 903, -28));
            m_PlayerSpotList.Add(new Point3D(147, 952, -29));
            m_PlayerSpotList.Add(new Point3D(99, 1158, -23)); // Red Spot
            m_PlayerSpotList.Add(new Point3D(13, 1177, -27));
            m_PlayerSpotList.Add(new Point3D(45, 1078, -29));
            m_PlayerSpotList.Add(new Point3D(27, 1013, -29));
            m_PlayerSpotList.Add(new Point3D(18, 950, -28));
            m_PlayerSpotList.Add(new Point3D(91, 1046, -28));
            m_PlayerSpotList.Add(new Point3D(48, 909, -29));

        }

        public void FinishTheHuntEvent()
        {
            FinishTheHuntEvent(null);
        }

        public void FinishTheHuntEvent(Mobile caller)
        {
            if (m_IsTheHuntRunning)
            {

                if (m_TheHuntPlayerList.Count > 0)
                {
                    Logger.LogMessage("TheHunt encerrado por limite de tempo.", "TheHunt");
                    this.BroadcastToPlayersList("TheHunt encerrado por limite de tempo.");
                    this.BroadcastToPlayersList("Não houve vencedores neste evento.");

                    foreach (Mobile player in m_TheHuntPlayerList)
                    {
                        this.SendPlayerBackHome(player);
                    }
                }

                m_TheHuntPlayerList.Clear();
                m_PlayerSpotList.Clear();
                m_TheHuntPlayerHomeLocationList.Clear();
                m_TheHuntPlayerHomeLocationMapList.Clear();

                m_IsTheHuntRunning = false;
                m_IsAcceptingPlayers = false;

                if (caller != null)
                    caller.SendMessage("TheHunt foi finalizado. salvando...");

                World.Save();
            }
            
            this.ForceRegionClean();
            Logger.LogMessage("TheHunt Encerrado.", "TheHunt");
        }

        private void ForceRegionClean()
        {
            // Remove possiveis Mobiles da area, em caso de crash com save durante o evento o comando de GM [finishTheHunt resolve

            if (m_TheHuntRegion == null)
                this.InitializeTheHuntRegion();

            foreach (Mobile mobile in m_TheHuntRegion.GetMobiles())
            {
                if (mobile != null)
                {
                    if (mobile is PlayerMobile)
                    {
                        if (mobile.AccessLevel >= AccessLevel.GameMaster)
                            continue;

                        // Caso tenha caido o server durante o evento, vai remover o player quando deserializar a stone
                        this.CleanPlayer(mobile);
                        mobile.MoveToWorld(new Point3D(1377, 1749, 0), Map.Felucca); // Lado fora ponte brit. TODO

                        Logger.LogMessage("ForceRegionClean removeu o player " + mobile.Name, "TheHunt");
                    }
                }
            }

            m_TheHuntRegion.Unregister();
        }

        public void SetPlayerDeath(int serial)
        {
            PlayerMobile player = World.FindMobile(serial) as PlayerMobile;

            Logger.LogMessage("TheHunt: SetPlayerDeath " + player.Serial + "-" + player.Name, "TheHunt");

            if (player != null)
            {
                this.eventStatistics.InsertKill((PlayerMobile)player.LastKiller, player);

                m_TheHuntPlayerList.Remove(player);

                player.SendMessage(0x35, "Voce foi eliminado do TheHunt!");

                if (m_TheHuntPlayerList.Count > 1)
                {
                    if (player.LastKiller != null && player.LastKiller is PlayerMobile)
                    {
                        World.Broadcast(0x35, true, string.Format("{0} foi eliminado do TheHunt por {1}!", player.Name, player.LastKiller));
                    }
                    else
                    {
                        World.Broadcast(0x35, true, string.Format("{0} foi eliminado do TheHunt!", player.Name));
                    }
                }

                this.RewardParticipant(player);
                this.SendPlayerBackHome(player);

                this.CheckTheHuntEventState();
            }
            else
            {
                Logger.LogMessage("TheHunt: SetPlayerDeath nao localizado", "TheHunt");
            }
        }

        private void CheckTheHuntEventState()
        {
            if (m_TheHuntPlayerList.Count == 1)
            {
                Logger.LogMessage(string.Format("Vencedor: {0} ({1})", m_TheHuntPlayerList[0].Name, m_TheHuntPlayerList[0].Serial), "TheHunt");
               
                World.Broadcast(0x35, true, string.Format("The Hunt Acabou !!!"));
                World.Broadcast(0x35, true, string.Format("'{0}' foi o grande vencedor deste The Hunt ! Parabens !!", m_TheHuntPlayerList[0].Name));
                World.Broadcast(0x35, true, string.Format("RESULTADO:"));

                foreach (PlayerKills playerKills in eventStatistics.PlayerKillsList)
                {
                    foreach (Mobile mobileKilled in playerKills.playerKillList)
                    {
                        World.Broadcast(0x35, true, string.Format("{0} eliminou {1}", (playerKills.player == null ? "Desconhecido" : playerKills.player.Name), mobileKilled.Name));
                    }
                }

                this.RewardWinner(m_TheHuntPlayerList[0]);
                this.SendPlayerBackHome(m_TheHuntPlayerList[0]);

                m_TheHuntPlayerList.Remove(m_TheHuntPlayerList[0]);
                this.FinishTheHuntEvent();
            }
            else if (m_TheHuntPlayerList.Count > 1)
            {
                this.BroadcastToPlayersList(string.Format("{0} jogadores restantes", m_TheHuntPlayerList.Count));
            }
            else
            {
                Logger.LogMessage("TheHunt: Encerrado Player Count 0", "TheHunt");
                this.FinishTheHuntEvent();
            }
        }

        public void CmdFinishTheHunt_OnCommand(CommandEventArgs e)
        {
            this.FinishTheHuntEvent();
        }

        public void CmdStartTheHunt_OnCommand(CommandEventArgs e)
        {
            this.AnnounceAndStartTheHunt(e.Mobile);
        }

        public void CmdTheHuntStone_OnCommand(CommandEventArgs e)
        {
            e.Mobile.MoveToWorld(this.Location, this.Map);
        }

        public void CmdGoTheHunt_OnCommand(CommandEventArgs e)
        {
            //e.Mobile.MoveToWorld(new Point3D(47, 836, -28), Map.Ilshenar);
            e.Mobile.MoveToWorld(this.Location, this.Map);
        }
        
        public void CmdTheHunt_OnCommand(CommandEventArgs e)
        {
            this.EnterEvent(e.Mobile);
        }

        public void AutoStartEvent()
        {
            this.AnnounceAndStartTheHunt(null);
        }

        public void EnterEvent(Mobile mobile)
        {
            if (mobile.AccessLevel >= AccessLevel.GameMaster)
            {
                TheHuntGump TheHuntGump = new TheHuntGump(mobile, this);
                mobile.SendGump(TheHuntGump);
                return;
            }

            if (!m_IsAcceptingPlayers)
            {
                mobile.SendMessage(0x35, "TheHunt não está aceitando inscricoes neste momento.");
                return;
            }

            if (m_TheHuntPlayerList.Contains(mobile))
            {
                mobile.SendMessage(0x35, "Voce já está inscrito para o evento.");
                return;
            }

            if (m_TheHuntPlayerList.Count >= m_PlayerSpotList.Count)
            {
                mobile.SendMessage(0x35, string.Format("TheHunt já atingiu o limite máximo de {0} participantes.", m_PlayerSpotList.Count));
                return;
            }

            m_TheHuntPlayerList.Add(mobile);

            // Salva a localizacao no inicio do evento e nao na inscricao
            //m_TheHuntPlayerHomeLocationList.Add(mobile, mobile.Location);
            //m_TheHuntPlayerHomeLocationMapList.Add(mobile, mobile.Map);

            mobile.SendMessage(0x35, "Voce se inscreveu para o TheHunt com sucesso!");
            mobile.SendMessage(0x35, "Aguarde o inicio do evento.. Boa Sorte!");
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

            this.InitializeTheHuntStone();
        }

        private void InitializeTheHuntRegion()
        {
            Rectangle2D rectangle2DRegion = new Rectangle2D(0, 800, 192, 400);

            m_TheHuntRegion = new TheHuntRegion(this, "TheHunt Region", Map.Ilshenar, new Rectangle2D[] { rectangle2DRegion });
            m_TheHuntRegion.Unregister();
            m_TheHuntRegion.Register();
        }

        private void CleanPlayer(Mobile pm)
        {
            pm.CurePoison(pm);
            pm.Hits = pm.HitsMax;
            pm.Mana = pm.ManaMax;
            pm.Stam = pm.StamMax;

            Targeting.Target.Cancel(pm);
            pm.MagicDamageAbsorb = 0;
            pm.MeleeDamageAbsorb = 0;

            Spells.Second.ProtectionSpell.Registry.Remove(pm);
            Spells.Fourth.CurseSpell.RemoveEffect(pm);
            Server.Spells.Necromancy.CorpseSkinSpell.RemoveCurse(pm);
            Server.Spells.Necromancy.BloodOathSpell.RemoveCurse(pm);
            Server.Spells.Necromancy.StrangleSpell.RemoveCurse(pm);

            pm.Blessed = false;
            pm.Paralyzed = false;
            pm.Hidden = false;

            DefensiveSpell.Nullify(pm);
            pm.Combatant = null;
            pm.Warmode = false;
            pm.Criminal = false;
            pm.Blessed = false;
            pm.Aggressed.Clear();
            pm.Aggressors.Clear();
            pm.Delta(MobileDelta.Noto);
            pm.InvalidateProperties();
        }

        private void SendPlayerBackHome(Mobile mobile)
        {
            if (m_TheHuntPlayerHomeLocationList.ContainsKey(mobile) &&
                m_TheHuntPlayerHomeLocationMapList.ContainsKey(mobile))
            {
                this.CleanPlayer(mobile);

                Point3D playerHome = m_TheHuntPlayerHomeLocationList[mobile];
                Map playerHomeMap = m_TheHuntPlayerHomeLocationMapList[mobile];

                mobile.MoveToWorld(playerHome, playerHomeMap);
                mobile.FixedParticles(0x3709, 1, 30, 9965, 5, 7, EffectLayer.Waist);
            }
            else
            {
                Logger.LogMessage("TheHunt: SendPlayerBackHome not found. " + m_TheHuntPlayerHomeLocationList.Count, "TheHunt");
            }
        }

        private void RewardParticipant(Mobile player)
        {
            player.BankBox.DropItem(new Gold(1000));
            player.SendMessage(0x35, "Um Premio de Participacao foi depositado em seu banco!");

            if (this.eventStatistics.GetPlayerKillList((PlayerMobile)player).Count > 0)
            {
                int qtExtraGold = 5000 * this.eventStatistics.GetPlayerKillList((PlayerMobile)player).Count;

                player.BankBox.DropItem(new Gold(qtExtraGold));
                player.SendMessage(0x35, string.Format("Voce recebeu um premio extra de {0} Golds, por eliminar {1} inimigos.", qtExtraGold, this.eventStatistics.GetPlayerKillList((PlayerMobile)player).Count));
            }

            player.SendGump(new AlertGump(player, "Obrigado por participar deste evento! Um Premio de Participacao foi depositado em seu banco!", "Obrigado!"));
        }

        private void RewardWinner(Mobile player)
        {
            if (SingletonEvent.Instance.IsAutomaticEvent || SingletonEvent.Instance.CurrentEventRewardList.Count == 0)
            {
                IMount mount = new Mustang();
                if (mount != null)
                    mount.Rider = player;

                player.BankBox.DropItem(new Gold(10000));
            }
            else
            {
                RewardUtil.SendRewardToPlayer(player);
            }

            player.SendMessage(0x35, "O Premio de Vencedor foi depositado em seu banco!");

            if (this.eventStatistics.GetPlayerKillList((PlayerMobile)player).Count > 0)
            {
                int qtExtraGold = 5000 * this.eventStatistics.GetPlayerKillList((PlayerMobile)player).Count;

                player.BankBox.DropItem(new Gold(qtExtraGold));
                player.SendMessage(0x35, string.Format("Voce recebeu um premio extra de {0} Golds, por eliminar {1} inimigos.", qtExtraGold, this.eventStatistics.GetPlayerKillList((PlayerMobile)player).Count));
            }

            player.SendGump(new AlertGump(player, "O Premio de Vencedor foi depositado em seu banco!", "PARABÉNS!"));
        }
    }
}