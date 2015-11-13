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
    public class SurvivorStone : Item
    {
        

        // Survivor Global Controle
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

        private static bool isSurvivorRunning;
        private static bool m_IsSurvivorRunning
        {
            get
            {
                return isSurvivorRunning;
            }
            set
            {
                isSurvivorRunning = value;
                SingletonEvent.Instance.IsEventRunning = value;
            }
        }


        // Survivor Event Controle
        private static int m_CountDownToStartEvent { get; set; }
        private static int m_CountDownToStartFight { get; set; }

        //Survivor Players
        private static List<Mobile> m_SurvivorPlayerList;
        private static List<Mobile> m_SurvivorParticipantPlayerList;
        private static Dictionary<int, int> m_SurvivorPlayerLivesList;
        private static Dictionary<Mobile, Point3D> m_SurvivorPlayerHomeLocationList;
        private static Dictionary<Mobile, Map> m_SurvivorPlayerHomeLocationMapList;

        // Estatistica
        private BaseEventStatistics eventStatistics;

        // Survivor Region
        private static SurvivorRegion m_SurvivorRegion;

        // Arena
        public string ArenaName { get; set; }
        
        public Point3D ArenaAreaAPoint { get; set; }
        public Point3D ArenaAreaBPoint { get; set; }
        public Rectangle2D ArenaArea
        {
            get
            {
                if (this.ArenaAreaAPoint.X == 0 && this.ArenaAreaAPoint.Y == 0)
                    return new Rectangle2D();
                if (this.ArenaAreaBPoint.X == 0 && this.ArenaAreaBPoint.Y == 0)
                    return new Rectangle2D();

                return new Rectangle2D(new Point2D(this.ArenaAreaAPoint.X, this.ArenaAreaAPoint.Y), new Point2D(this.ArenaAreaBPoint.X + 1, this.ArenaAreaBPoint.Y + 1));

            }
        }

        public Point3D ArenaRespawnPoint { get; set; }
        public Point3D ArenaRespawnAPoint { get; set; }
        public Point3D ArenaRespawnBPoint { get; set; }

        private const int qtInitialLives = 2;
        private const int qtMaxLives = 2;

        [Constructable]
        public SurvivorStone() : base(0xEDC)
        {
            Name = "Survivor Stone";
            Movable = false;
            Hue = 1967;

            this.InitializeSurvivorStone();
        }


        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel >= AccessLevel.GameMaster)
                from.SendGump(new SurvivorGump(from, this));
        }


        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (!string.IsNullOrEmpty(this.ArenaName))
                list.Add(1060661, "Arena\t{0}", this.ArenaName);

            list.Add(1060658, "Running Now\t{0}", m_IsSurvivorRunning.ToString());
            list.Add(1060659, "Aceitando Players\t{0}", m_IsAcceptingPlayers.ToString());
            list.Add(1060660, "Participantes\t{0}", m_SurvivorPlayerList.Count);
            //list.Add(1060661, "Proximo Evento\t{0}", string.Format("em {0} horas e {1} minutos.", eventTimeSpan.Hours, eventTimeSpan.Minutes));
        }

        public SurvivorStone(Serial serial)
            : base(serial)
		{
		}

        public bool IsArenaConfigValid()
        {
            if (string.IsNullOrEmpty(this.ArenaName))
                return false;

            if (this.ArenaArea.Height == 0)
                return false;

            if (this.ArenaRespawnPoint.X == 0 && this.ArenaRespawnPoint.Y == 0)
                return false;

            if (this.ArenaRespawnAPoint.X == 0 && this.ArenaRespawnAPoint.Y == 0)
                return false;

            if (this.ArenaRespawnBPoint.X == 0 && this.ArenaRespawnBPoint.Y == 0)
                return false;

            if (this.ArenaArea.Contains(this.ArenaRespawnPoint) == false)
                return false;

            if (this.ArenaArea.Contains(this.ArenaRespawnAPoint) == false)
                return false;

            if (this.ArenaArea.Contains(this.ArenaRespawnBPoint) == false)
                return false;

            return true;
        }

        private void InitializeSurvivorStone()
        {
            //Logger.LogMessage("InitializeSurvivorStone start", "Survivor");

            m_SurvivorPlayerList = new List<Mobile>();
            m_SurvivorParticipantPlayerList = new List<Mobile>();
            m_SurvivorPlayerLivesList = new Dictionary<int, int>();
            m_SurvivorPlayerHomeLocationList = new Dictionary<Mobile, Point3D>();
            m_SurvivorPlayerHomeLocationMapList = new Dictionary<Mobile, Map>();

            SingletonEvent.Instance.CurrentEventLocation = this.ArenaRespawnPoint;
            SingletonEvent.Instance.CurrentEventMap = this.Map;

            this.ForceRegionClean();

            //Logger.LogMessage("InitializeSurvivorStone finish", "Survivor");
        }


        public void AnnounceAndStartSurvivor(Mobile caller)
        {

            if (this.IsArenaConfigValid() == false)
            {
                caller.SendMessage("Arena nao esta corretamente configurada. Solicitacao ignorada.");
                return;
            }

            if (SingletonEvent.Instance.IsEventRunning)
            {
                if (caller != null)
                    caller.SendMessage("Um evento ja esta em andamento. Solicitacao ignorada.");
                return;
            }

            if (caller == null)
            {
                m_IsAutomaticEvent = true;
                SingletonEvent.Instance.AllowLooting = false;
                SingletonEvent.Instance.BlockBow = true;
                SingletonEvent.Instance.BlockPots = true;
                SingletonEvent.Instance.BlockSpells = true;
                SingletonEvent.Instance.HasAntiCamperMode = true;
                SingletonEvent.Instance.HasAntiPanelaMode = true;
                SingletonEvent.Instance.HasBadMacroerMode = false;
                SingletonEvent.Instance.IsTeamMode = false;
            }
            else
            {
                m_IsAutomaticEvent = false;
                SingletonEvent.Instance.AllowLooting = false;
                SingletonEvent.Instance.BlockBow = true;
                SingletonEvent.Instance.BlockPots = true;
                SingletonEvent.Instance.BlockSpells = true;
                SingletonEvent.Instance.HasAntiCamperMode = true;
                SingletonEvent.Instance.HasAntiPanelaMode = true;
                SingletonEvent.Instance.HasBadMacroerMode = false;
                SingletonEvent.Instance.IsTeamMode = false;
            }

            this.InitializeSurvivorRegion();

            m_SurvivorPlayerList.Clear();
            m_SurvivorParticipantPlayerList.Clear();
            m_SurvivorPlayerLivesList.Clear();
            m_SurvivorPlayerHomeLocationList.Clear();
            m_SurvivorPlayerHomeLocationMapList.Clear();

            this.eventStatistics = new BaseEventStatistics();

            m_IsSurvivorRunning = true;
            m_IsAcceptingPlayers = true;
            SingletonEvent.Instance.CurrentEventStone = this;
            SingletonEvent.Instance.CurrentEventType = EnumEventBase.EnumEventType.Survivor;

            m_CountDownToStartEvent = 5; // CountDown de 5 minutos para inicio do evento
            m_CountDownToStartFight = 10; // CountDown de 10 segundos para inicio da luta

            Logger.LogMessage("Evento Survivor Iniciado!", "Survivor");
            World.Broadcast(0x35, true, "Evento Survivor Iniciado!");
            World.Broadcast(0x35, true, "Digite .entrar para participar!");

            foreach (Mobile mobile in World.Mobiles.Values)
            {
                if (mobile is PlayerMobile)
                    mobile.SendGump(new SurvivorInviteGump(mobile, this));
            }

            Timer.DelayCall(TimeSpan.FromMinutes(4.0), new TimerCallback(BcastTimeLeftToStartEvent));
            Timer.DelayCall(TimeSpan.FromMinutes(3.0), new TimerCallback(BcastTimeLeftToStartEvent));
            Timer.DelayCall(TimeSpan.FromMinutes(2.0), new TimerCallback(BcastTimeLeftToStartEvent));
            Timer.DelayCall(TimeSpan.FromMinutes(1.0), new TimerCallback(BcastTimeLeftToStartEvent));

            Timer.DelayCall(TimeSpan.FromMinutes(5.0), new TimerCallback(StartSurvivor));
        }

        public void BcastTimeLeftToStartEvent()
        {
            m_CountDownToStartEvent -= 1;
            World.Broadcast(0x35, true, string.Format("Survivor Iniciando em {0} minutos...", m_CountDownToStartEvent));
            World.Broadcast(0x35, true, "Digite .entrar para participar!");
        }

        public void BcastTimeLeftToStartFight()
        {
            m_CountDownToStartFight -= 1;
            this.BroadcastToPlayersList(string.Format("Inicio em {0} segundos...", m_CountDownToStartFight));
        }

        private void StartSurvivor()
        {

            if (m_SurvivorPlayerList.Count < 2)
            {
                Logger.LogMessage("Survivor cancelado por falta de jogadores.", "Survivor");
                World.Broadcast(0x35, true, "Survivor cancelado por falta de jogadores.");
                m_IsAcceptingPlayers = false;
                m_IsSurvivorRunning = false;
                return;
            }
            else
            {
                World.Save();

                m_IsAcceptingPlayers = false;
                World.Broadcast(0x35, true, "Inscricoes encerradas para Survivor!");
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

            Timer.DelayCall(TimeSpan.FromMinutes(25.0), new TimerCallback(FinishSurvivorEvent));
        }


        private void PreparePlayersToFight()
        {
            foreach (Mobile mobile in m_SurvivorPlayerList)
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
                m_SurvivorPlayerHomeLocationList.Add(mobile, mobile.Location);
                m_SurvivorPlayerHomeLocationMapList.Add(mobile, mobile.Map);

                // Incognito - Remove Backpack
                player.SetIncognitoToEvent();
                player.DropAllItensToBackpack();
                player.Backpack.Visible = false;

                // Seta Time e Teleport
                this.MovePlayerToSpot(player);

                m_SurvivorPlayerLivesList.Add(player.Serial, qtInitialLives);
                player.SendMessage(string.Format("Voce recebeu {0} vidas iniciais!", qtInitialLives));
            }
        }

        private int currentTeam = 1;
        private void MovePlayerToSpot(PlayerMobile player)
        {
            player.UndressItem(player, Server.Layer.OuterTorso);
            player.UndressItem(player, Server.Layer.Shoes);

            if (SingletonEvent.Instance.IsTeamMode)
            {
                player.TeamID = currentTeam;

                if (currentTeam == 1)
                {
                    player.FixedParticles(0x3709, 1, 30, 9965, 5, 7, EffectLayer.Waist);
                    player.EquipItem(new TeamARobe());
                    player.EquipItem(new TeamAShoes());
                    player.MoveToWorld(this.ArenaRespawnAPoint, this.Map);
                    player.FixedParticles(0x3709, 1, 30, 9965, 5, 7, EffectLayer.Waist);
                    currentTeam = 2;
                }
                else
                {
                    player.FixedParticles(0x3709, 1, 30, 9965, 5, 7, EffectLayer.Waist);
                    player.EquipItem(new TeamBRobe());
                    player.EquipItem(new TeamBShoes());
                    player.MoveToWorld(this.ArenaRespawnBPoint, this.Map);
                    player.FixedParticles(0x3709, 1, 30, 9965, 5, 7, EffectLayer.Waist);
                    currentTeam = 1;
                }
            }
            else
            {
                player.TeamID = 0;

                player.FixedParticles(0x3709, 1, 30, 9965, 5, 7, EffectLayer.Waist);
                player.EquipItem(new TeamARobe() { Name = "Dimens Event" });
                player.EquipItem(new TeamAShoes() { Name = "Dimens Event" });
                player.MoveToWorld(this.ArenaRespawnPoint, this.Map);
                player.FixedParticles(0x3709, 1, 30, 9965, 5, 7, EffectLayer.Waist);
            }
        }

        private void ReleasePlayers()
        {
            foreach (Mobile mobile in m_SurvivorPlayerList)
            {
                Mobile player = World.FindMobile(mobile.Serial);

                player.Blessed = false;
                player.Paralyzed = false;
                player.Hidden = false;
                player.Criminal = true;

                player.FixedParticles(0x3709, 1, 30, 9965, 5, 7, EffectLayer.Waist);
            }

            World.Broadcast(0x35, true, string.Format("Survivor iniciado com {0} participantes!", m_SurvivorPlayerList.Count));
            World.Broadcast(0x35, true, string.Format("Que venca o melhor!"));
        }

        private void BroadcastToPlayersList(string pMessage)
        {
            foreach (Mobile mobile in m_SurvivorPlayerList)
            {
                Mobile player = World.FindMobile(mobile.Serial);

                if (player != null)
                    player.SendMessage(0x35, pMessage);
            }
        }

        public void FinishSurvivorEvent()
        {
            FinishSurvivorEvent(null);
        }

        public void FinishSurvivorEvent(Mobile caller)
        {
            if (m_IsSurvivorRunning)
            {

                if (m_SurvivorPlayerList.Count > 0)
                {
                    Logger.LogMessage("Survivor encerrado por limite de tempo.", "Survivor");
                    this.BroadcastToPlayersList("Survivor encerrado por limite de tempo.");
                    this.BroadcastToPlayersList("Não houve vencedores neste evento.");

                    foreach (Mobile player in m_SurvivorPlayerList)
                    {
                        this.SendPlayerBackHome(player);
                    }
                }

                m_SurvivorPlayerList.Clear();
                m_SurvivorParticipantPlayerList.Clear();
                m_SurvivorPlayerLivesList.Clear();
                m_SurvivorPlayerHomeLocationList.Clear();
                m_SurvivorPlayerHomeLocationMapList.Clear();

                m_IsSurvivorRunning = false;
                m_IsAcceptingPlayers = false;

                if (caller != null)
                    caller.SendMessage("Survivor foi finalizado. salvando...");

                World.Save();
            }
            
            this.ForceRegionClean();
            Logger.LogMessage("Survivor Encerrado.", "Survivor");
        }

        private void ForceRegionClean()
        {
            // Remove possiveis Mobiles da area, em caso de crash com save durante o evento o comando de GM [finishSurvivor resolve

            if (m_SurvivorRegion == null)
                this.InitializeSurvivorRegion();

            foreach (Mobile mobile in m_SurvivorRegion.GetMobiles())
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

                        Logger.LogMessage("ForceRegionClean removeu o player " + mobile.Name, "Survivor");
                    }
                }
            }

            m_SurvivorRegion.Unregister();
        }

        public void SetPlayerDeath(int serial)
        {
            PlayerMobile player = World.FindMobile(serial) as PlayerMobile;

            Logger.LogMessage("Survivor: SetPlayerDeath " + player.Serial + "-" + player.RawName, "Survivor");

            if (player != null)
            {
                
                // Refresh Killed
                bool wasPlayerEliminated;
                int qtLives;
                if (m_SurvivorPlayerLivesList.TryGetValue(player.Serial, out qtLives) && qtLives > 0)
                {
                    wasPlayerEliminated = false;

                    player.Hits = player.HitsMax;
                    player.Mana = player.ManaMax;
                    player.Stam = player.StamMax;

                    player.MoveToWorld(this.ArenaRespawnPoint, this.Map);
                    player.FixedParticles(0x3709, 1, 30, 9965, 5, 7, EffectLayer.Waist);

                    qtLives--;
                    player.SendMessage(0x35, string.Format("Voce foi morto ! ATENCAO, {0} vidas restantes !", qtLives));

                    m_SurvivorPlayerLivesList.Remove(player.Serial);
                    m_SurvivorPlayerLivesList.Add(player.Serial, qtLives);
                }
                else
                {
                    wasPlayerEliminated = true;

                    if (player.LastKiller != null && player.LastKiller is PlayerMobile)
                        this.eventStatistics.InsertKill((PlayerMobile)player.LastKiller, player);

                    m_SurvivorPlayerList.Remove(player);
                    m_SurvivorPlayerLivesList.Remove(player.Serial);

                    player.SendMessage(0x35, "Voce foi eliminado do Survivor!");

                    if (m_SurvivorPlayerList.Count > 1)
                    {
                        if (player.LastKiller != null && player.LastKiller is PlayerMobile)
                        {
                            World.Broadcast(0x35, true, string.Format("{0} foi eliminado do Survivor por {1}!", player.RawName, player.LastKiller));
                        }
                        else
                        {
                            World.Broadcast(0x35, true, string.Format("{0} foi eliminado do Survivor!", player.RawName));
                        }
                    }

                    if (SingletonEvent.Instance.IsTeamMode == false)
                        this.RewardParticipant(player);
                    else
                        player.SendGump(new AlertGump(player, "Voce foi eliminado do evento. Aguarde o fim para receber o premio.", "Obrigado pela participação!"));

                    this.SendPlayerBackHome(player);
                }


                // Refresh Killer
                Mobile killer = player.LastKiller;

                if (killer != null)
                {
                    killer.Hits = killer.HitsMax;
                    killer.Mana = killer.ManaMax;
                    killer.Stam = killer.StamMax;

                    if (wasPlayerEliminated)
                        killer.SendMessage(string.Format("Voce eliminou {0} do jogo ! Seu status foi revigorado !", player.RawName));
                    else
                        killer.SendMessage(string.Format("Voce matou {0} ! Seu status foi revigorado !", player.RawName));
                        
                }

                // Refresh Evento
                this.CheckSurvivorEventState();
            }
            else
            {
                Logger.LogMessage("Survivor: SetPlayerDeath nao localizado", "Survivor");
            }
        }

        private void CheckSurvivorEventState()
        {
            if (SingletonEvent.Instance.IsTeamMode)
                this.CheckSurvivorEventStateTeamMode();
            else
                this.CheckSurvivorEventStateIndividualMode();
        }

        private void CheckSurvivorEventStateIndividualMode()
        {
            if (m_SurvivorPlayerList.Count == 1)
            {
                this.SendPlayerBackHome(m_SurvivorPlayerList[0]);

                Logger.LogMessage(string.Format("Vencedor: {0} ({1})", m_SurvivorPlayerList[0].RawName, m_SurvivorPlayerList[0].Serial), "Survivor");

                World.Broadcast(0x35, true, string.Format("Survivor Acabou !!!"));
                World.Broadcast(0x35, true, string.Format("'{0}' foi o grande vencedor deste Survivor ! Parabens !!", m_SurvivorPlayerList[0].RawName));
                World.Broadcast(0x35, true, string.Format("RESULTADO:"));

                foreach (PlayerKills playerKills in eventStatistics.PlayerKillsList)
                {
                    foreach (Mobile mobileKilled in playerKills.playerKillList)
                    {
                        World.Broadcast(0x35, true, string.Format("{0} eliminou {1}", (playerKills.player == null ? "Algo" : playerKills.player.RawName), mobileKilled.RawName));
                    }
                }

                this.RewardWinner(m_SurvivorPlayerList[0]);

                m_SurvivorPlayerList.Remove(m_SurvivorPlayerList[0]);

                this.FinishSurvivorEvent();
            }
            else if (m_SurvivorPlayerList.Count > 1)
            {
                this.BroadcastToPlayersList(string.Format("{0} jogadores restantes", m_SurvivorPlayerList.Count));
            }
            else
            {
                Logger.LogMessage("Survivor: Encerrado Player Count 0", "Survivor");
                this.FinishSurvivorEvent();
            }
        }

        private void CheckSurvivorEventStateTeamMode()
        {
            List<int> teamList = new List<int>();

            foreach (Mobile mobile in m_SurvivorPlayerList)
            { 
                if (teamList.Contains(((PlayerMobile)mobile).TeamID) == false)
                    teamList.Add(((PlayerMobile)mobile).TeamID);
            }

            if (teamList.Count == 1)
            {
                int teamIDWinner = ((PlayerMobile)m_SurvivorPlayerList[0]).TeamID;
                string teamWinner = (teamIDWinner == 1) ? "Time Verde foi Vencedor !" : "Time Branco foi Vencedor !";

                //Logger.LogMessage(string.Format("Vencedor: {0} ({1})", m_SurvivorPlayerList[0].RawName, m_SurvivorPlayerList[0].Serial), "Survivor");

                World.Broadcast(0x35, true, string.Format("Survivor Acabou !!!" + teamWinner));
                //World.Broadcast(0x35, true, string.Format("'{0}' foi o grande vencedor deste Survivor ! Parabens !!", m_SurvivorPlayerList[0].RawName));
                World.Broadcast(0x35, true, string.Format("RESULTADO:"));

                foreach (PlayerKills playerKills in eventStatistics.PlayerKillsList)
                {
                    foreach (Mobile mobileKilled in playerKills.playerKillList)
                    {
                        World.Broadcast(0x35, true, string.Format("{0} eliminou {1}", (playerKills.player == null ? "Algo" : playerKills.player.RawName), mobileKilled.RawName));
                    }
                }

                foreach (Mobile player in m_SurvivorPlayerList)
                    this.SendPlayerBackHome(player);
                
                this.RewardTeamPlayers(teamIDWinner);

                this.FinishSurvivorEvent();
            }
            else if (teamList.Count > 1)
            {
                this.BroadcastToPlayersList(string.Format("{0} jogadores restantes", m_SurvivorPlayerList.Count));
            }
            else
            {
                Logger.LogMessage("Survivor: Encerrado Player Count 0", "Survivor");
                this.FinishSurvivorEvent();
            }
        }

        private void RewardTeamPlayers(int pTeamIDWinner)
        {
            foreach (Mobile mobile in m_SurvivorParticipantPlayerList)
            {
                PlayerMobile player = mobile as PlayerMobile;

                if (player.TeamID == pTeamIDWinner)
                    this.RewardWinner(player);
                else
                    this.RewardParticipant(player);
            }
        }


        public void AutoStartEvent()
        {
            this.AnnounceAndStartSurvivor(null);
        }

        public void EnterEvent(Mobile mobile)
        {
            if (mobile.AccessLevel >= AccessLevel.GameMaster)
            {
                SurvivorGump SurvivorGump = new SurvivorGump(mobile, this);
                mobile.SendGump(SurvivorGump);
                return;
            }

            if (!m_IsAcceptingPlayers)
            {
                mobile.SendMessage(0x35, "Survivor não está aceitando inscricoes neste momento.");
                return;
            }

            if (m_SurvivorPlayerList.Contains(mobile))
            {
                mobile.SendMessage(0x35, "Voce já está inscrito para o evento.");
                return;
            }

            m_SurvivorPlayerList.Add(mobile);
            m_SurvivorParticipantPlayerList.Add(mobile);

            // Salva a localizacao no inicio do evento e nao na inscricao
            //m_SurvivorPlayerHomeLocationList.Add(mobile, mobile.Location);
            //m_SurvivorPlayerHomeLocationMapList.Add(mobile, mobile.Map);

            mobile.SendMessage(0x35, "Voce se inscreveu para o Survivor com sucesso!");
            mobile.SendMessage(0x35, "Aguarde o inicio do evento.. Boa Sorte!");
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version

            writer.Write(ArenaName);
            writer.Write(ArenaRespawnPoint);
            writer.Write(ArenaRespawnAPoint);
            writer.Write(ArenaRespawnBPoint);
            writer.Write(ArenaAreaAPoint);
            writer.Write(ArenaAreaBPoint);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            this.InitializeSurvivorStone();

            switch (version)
            {
                case 0:
                    {
                        ArenaName = reader.ReadString();
                        ArenaRespawnPoint = reader.ReadPoint3D();
                        ArenaRespawnAPoint = reader.ReadPoint3D();
                        ArenaRespawnBPoint = reader.ReadPoint3D();
                        ArenaAreaAPoint = reader.ReadPoint3D();
                        ArenaAreaBPoint = reader.ReadPoint3D();
                        break;
                    }
            }

            Hue = 1967;
        }

        private void InitializeSurvivorRegion()
        {
            m_SurvivorRegion = new SurvivorRegion(this, "Survivor Region", this.Map, new Rectangle2D[] { ArenaArea });
            m_SurvivorRegion.Unregister();
            m_SurvivorRegion.Register();
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
            if (m_SurvivorPlayerHomeLocationList.ContainsKey(mobile) &&
                m_SurvivorPlayerHomeLocationMapList.ContainsKey(mobile))
            {
                this.CleanPlayer(mobile);

                mobile.Backpack.Visible = true;

                Item item = mobile.FindItemOnLayer(Server.Layer.OuterTorso);
                if (item != null && (item is TeamARobe || item is TeamBRobe))
                    item.Delete();

                item = mobile.FindItemOnLayer(Server.Layer.Shoes);
                if (item != null && (item is TeamAShoes || item is TeamBShoes))
                    item.Delete();

                ((PlayerMobile)mobile).RestoreStatusBackup();

                Point3D playerHome = m_SurvivorPlayerHomeLocationList[mobile];
                Map playerHomeMap = m_SurvivorPlayerHomeLocationMapList[mobile];

                mobile.MoveToWorld(playerHome, playerHomeMap);
                mobile.FixedParticles(0x3709, 1, 30, 9965, 5, 7, EffectLayer.Waist);
            }
            else
            {
                Logger.LogMessage("Survivor: SendPlayerBackHome not found. " + m_SurvivorPlayerHomeLocationList.Count, "Survivor");
            }
        }

        private void RewardParticipant(Mobile player)
        {
            string messageGump = "Obrigado por participar deste evento! Um Premio de Participacao foi depositado em seu banco!";

            player.BankBox.DropItem(new Gold(1000));
            player.SendMessage(0x35, "Um Premio de Participacao foi depositado em seu banco!");

            if (this.eventStatistics.GetPlayerKillList((PlayerMobile)player).Count > 0)
            {
                int qtExtraGold = 5000 * this.eventStatistics.GetPlayerKillList((PlayerMobile)player).Count;

                player.BankBox.DropItem(new Gold(qtExtraGold));
                player.SendMessage(0x35, string.Format("Voce recebeu um premio extra de {0} Golds, por eliminar {1} inimigos.", qtExtraGold, this.eventStatistics.GetPlayerKillList((PlayerMobile)player).Count));

                messageGump += string.Format("<BR>Voce recebeu um premio extra de {0} Golds, por eliminar {1} inimigos.", qtExtraGold, this.eventStatistics.GetPlayerKillList((PlayerMobile)player).Count);
            }

            player.SendGump(new AlertGump(player, messageGump, "Obrigado pela participação!"));
        }

        private void RewardWinner(Mobile player)
        {
            string messageGump = "O Premio de Vencedor foi depositado em seu banco!";
            Bag bagReward = new Bag();

            if (SingletonEvent.Instance.IsAutomaticEvent || SingletonEvent.Instance.CurrentEventRewardList.Count == 0)
            {
                bagReward.Hue = Utility.RandomYellowHue();
                bagReward.Name = "Reward Bag";

                if (SingletonEvent.Instance.IsTeamMode)
                {
                    Item item;
                    if (Utility.RandomBool())
                        item = RewardUtil.CreateRewardInstance(RewardUtil.BlackRockWeaponTypes) as Item;
                    else
                        item = RewardUtil.CreateRewardInstance(RewardUtil.BlackRockPlateTypes) as Item;

                    bagReward.DropItem(item);
                }
                else
                {
                    IMount mount = RewardUtil.CreateRewardInstance(RewardUtil.RegularMountTypes) as IMount;
                    ShrinkItem shrunkenPet = new ShrinkItem((BaseCreature)mount);
                    bagReward.DropItem(shrunkenPet);
                }

                bagReward.DropItem(new Gold(10000));

                player.BankBox.DropItem(bagReward);
            }
            else
            {
                RewardUtil.SendRewardToPlayer(player);
            }

            player.SendMessage(0x35, "O Premio de Vencedor foi depositado em seu banco!");

            if (this.eventStatistics.GetPlayerKillList((PlayerMobile)player).Count > 0)
            {
                int qtExtraGold = 5000 * this.eventStatistics.GetPlayerKillList((PlayerMobile)player).Count;

                bagReward.DropItem(new Gold(qtExtraGold));
                player.SendMessage(0x35, string.Format("Voce recebeu um premio extra de {0} Golds, por eliminar {1} inimigos.", qtExtraGold, this.eventStatistics.GetPlayerKillList((PlayerMobile)player).Count));

                messageGump += string.Format("<BR>Voce recebeu um premio extra de {0} Golds, por eliminar {1} inimigos.", qtExtraGold, this.eventStatistics.GetPlayerKillList((PlayerMobile)player).Count);
            }

            player.SendGump(new AlertGump(player, messageGump, "PARABÉNS!"));
        }
    }
}