using System;
using Server;
using Server.Commands;
using Server.Mobiles;
using Server.Regions;
using Server.Gumps;
using System.Collections;
using System.Collections.Generic;


namespace Server.Items
{
    public class PvPStone : Item
    {
        #region Private Variables

        private List<Mobile> m_BroadcastList;
        private List<Mobile> m_CurrentDuelers;
        private static List<Mobile> m_DuelList;
        private int m_TotalParticipants;
        private static TimeSpan m_EventRate;
        private static DateTime m_LastEvent;
        private static DateTime m_LastReset;

        private Point3D m_WallMidPoint;
        private int m_WallExtendWidth;
        private bool m_WallNorthToSouth;
        private List<Item> m_WallList;
        private int m_WallHue;
        private int m_WallID;

        private int m_CoinsPerRound;
        private int m_CoinsWinner;

        private static Point3D m_StartLocation;
        private static Point3D m_DuelLocation1;
        private static Point3D m_DuelLocation2;
        private static Point3D m_LostLocation;
        private static Map m_MapLocation;

        private Rectangle2D m_DuelingArea;
        private Rectangle2D m_StageingArea;
        private Rectangle2D m_ViewingArea;

        private PvPRegion PvPRegion;
        private PvPStagingRegion SpectatorRegion;
        private PvPStagingRegion ParticipateRegion;

        //How many times the join message will be broadcasted
        private const int BroadCastMaxTicks = 4;
        private const int BroadCastTickDelay = 60; //seconds
        private int m_CurrentBroadCastTicks;
        private static BroadcastTimer BCastTimer;
        private int m_CountDown;
        private int m_BroadcastHue;

        private static bool m_Running;
        private static bool m_AcceptingPlayers;

        private int m_CurrentRound;
        private InternalTimer m_RestartTimer;
        private bool m_TimerEnabled;

        private int m_MinimumDuelers;
        #endregion

        #region Get/Set

        [CommandProperty(AccessLevel.Developer)]
        public int MinDuelers
        {
            get { return m_MinimumDuelers; }
            set { m_MinimumDuelers = value; }
        }
        [CommandProperty(AccessLevel.Developer)]
        public DateTime LastReset
        {
            get { return m_LastReset; }
            set { m_LastReset = value; }
        }
        [CommandProperty(AccessLevel.Developer)]
        public bool TimerEnabled
        {
            get { return m_TimerEnabled; }
            set 
            { 
                m_TimerEnabled = value;
                if (value)
                    m_RestartTimer.Start();
            }
        }
        public List<Mobile> CurrentDuelers
        {
            get { return m_CurrentDuelers; }
        }

        public List<Mobile> DuelList
        {
            get { return m_DuelList; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public int GoldPerRound
        {
            get { return m_CoinsPerRound; }
            set { m_CoinsPerRound = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public int GoldWinner
        {
            get { return m_CoinsWinner; }
            set { m_CoinsWinner = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public int BroadCastHue
        {
            get { return m_BroadcastHue; }
            set { m_BroadcastHue = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public int WallHUe
        {
            get { return m_WallHue; }
            set { m_WallHue = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public int WallID
        {
            get { return m_WallID; }
            set { m_WallID = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public bool WallNorthToSouth
        {
            get { return m_WallNorthToSouth; }
            set { m_WallNorthToSouth = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public int WallExtendWidth
        {
            get { return m_WallExtendWidth; }
            set { m_WallExtendWidth = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public Point3D WallMidPoint
        {
            get { return m_WallMidPoint; }
            set { m_WallMidPoint = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public int CurrentRound
        {
            get { return m_CurrentRound; }
            set { m_CurrentRound = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public Map MapLocation
        {
            get { return m_MapLocation; }
            set { m_MapLocation = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public static bool Running
        {
            get { return m_Running; }
            set { m_Running = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public static bool AcceptingPlayers
        {
            get { return m_AcceptingPlayers; }
            set { m_AcceptingPlayers = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public static DateTime LastEvent
        {
            get { return m_LastEvent; }
            set { m_LastEvent = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public TimeSpan EventRate
        {
            get { return m_EventRate; }
            set { m_EventRate = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public static Point3D StartLocation
        {
            get { return m_StartLocation; }
            set { m_StartLocation = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public static Point3D DuelLocation1
        {
            get { return m_DuelLocation1; }
            set { m_DuelLocation1 = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public static Point3D DuelLocation2
        {
            get { return m_DuelLocation2; }
            set { m_DuelLocation2 = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public static Point3D LostLocation
        {
            get { return m_LostLocation; }
            set { m_LostLocation = value; }
        }

        [CommandProperty(AccessLevel.Developer)]
        public Rectangle2D DuelingArea
        {
            get { return m_DuelingArea; }
            set { m_DuelingArea = value; UpdateRegions(true); }
        }

        [CommandProperty(AccessLevel.Developer)]
        public Rectangle2D StageingArea
        {
            get { return m_StageingArea;  }
            set { m_StageingArea = value; UpdateRegions(true); }
        }

        [CommandProperty(AccessLevel.Developer)]
        public Rectangle2D ViewingArea
        {
            get { return m_ViewingArea; }
            set { m_ViewingArea = value; UpdateRegions(true); }
        }

        [CommandProperty(AccessLevel.Developer)]
        public bool KickStart
        {
            get { return false; }
            set
            {
                if (value && !m_Running)
                    StartPvP();
            }
        }

        #endregion

        #region Constructor

        [Constructable]
        public PvPStone() : base(0xEDC)
        {
            Name = "PvP Stone Event Stone";
            Movable = false;
            Visible = true;
            m_EventRate = TimeSpan.FromHours(6.0);
            m_StartLocation = new Point3D(0, 0, 0);
            m_LostLocation = new Point3D(0, 0, 0);
            m_DuelLocation1 = new Point3D(0, 0, 0);
            m_DuelLocation2 = new Point3D(0, 0, 0);
            m_LastEvent = DateTime.Now;
            m_DuelingArea = new Rectangle2D(0, 0, 0, 0);
            m_StageingArea = new Rectangle2D(0, 0, 0, 0);
            m_ViewingArea = new Rectangle2D(0, 0, 0, 0);
            m_CurrentBroadCastTicks = 0;
            m_Running = false;
            m_AcceptingPlayers = false;
            m_MapLocation = Map.Felucca;
            m_CurrentRound = 0;
            m_TotalParticipants = 0;
            m_DuelList = new List<Mobile>();
            m_CurrentDuelers = new List<Mobile>();
            m_BroadcastHue = 269;
            m_BroadcastList = new List<Mobile>();
            m_WallMidPoint = new Point3D(0,0,0);
            m_WallExtendWidth = 3;
            m_WallNorthToSouth = true;
            m_WallList = new List<Item>();
            m_WallID = 0x0081;
            m_WallHue = 0;
            m_CountDown = 0;
            m_CoinsPerRound = 2;
            m_CoinsWinner = 10;
            m_RestartTimer = new InternalTimer(this, TimeSpan.FromSeconds(1.0));
            m_TimerEnabled = false;
            m_LastReset = DateTime.Now;
            Rectangle2D[] m_Rects = new Rectangle2D[] { m_DuelingArea };
            PvPRegion = new PvPRegion(this, "The Pit", this.Map, m_Rects);
            m_Rects = new Rectangle2D[] { m_ViewingArea };
            SpectatorRegion = new PvPStagingRegion(this, "Spectators", this.Map, m_Rects);
            m_Rects = new Rectangle2D[] { m_StageingArea };
            ParticipateRegion = new PvPStagingRegion(this, "Gladiators", this.Map, m_Rects);
            UpdateRegions(false);
            m_MinimumDuelers = 4;
        }

        #endregion

        #region Misc Methods

        /// <summary>
        /// Double click the PvP Stone to open up the properties window for it. (Game Master+)
        /// </summary>
        /// <param name="from"></param>
        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel > AccessLevel.GameMaster)
                from.SendGump(new PropertiesGump(from, this));
        }

        /// <summary>
        /// Display any needed data here for the stone properties.
        /// </summary>
        /// <param name="list"></param>
        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add(1060658, "Participants\t{0}", m_TotalParticipants.ToString());
        }

        #endregion

        #region WallCode

        /// <summary>
        /// Drop a wall out in front of the dueling players, Also Initialize the countdown and wall removal
        /// </summary>
        public void DropWall()
        {
            m_CountDown = 10;
            if (m_WallNorthToSouth)
            {
                for (int index = m_WallMidPoint.Y - m_WallExtendWidth; index <= m_WallMidPoint.Y + m_WallExtendWidth; index++)
                {
                    StonePaversDark block = new StonePaversDark();
                    block.ItemID = m_WallID; //generic wall
                    block.Hue = m_WallHue;
                    block.MoveToWorld(new Point3D(m_WallMidPoint.X, index,m_WallMidPoint.Z), m_MapLocation);
                    m_WallList.Add(block);
                }
            }
            else
            {
                for (int index = m_WallMidPoint.X - m_WallExtendWidth; index <= m_WallMidPoint.X + m_WallExtendWidth; index++)
                {
                    StonePaversDark block = new StonePaversDark();
                    block.ItemID = m_WallID; //generic wall
                    block.Hue = m_WallHue;
                    block.MoveToWorld(new Point3D(index, m_WallMidPoint.Y, m_WallMidPoint.Z), m_MapLocation);
                    m_WallList.Add(block);
                }
            }
            Timer.DelayCall(TimeSpan.FromSeconds(9.0), new TimerCallback(RemoveWall_OnTick));
            BcastTimeLeft();
            Timer.DelayCall(TimeSpan.FromSeconds(2.0), new TimerCallback(BcastTimeLeft));
            Timer.DelayCall(TimeSpan.FromSeconds(4.0), new TimerCallback(BcastTimeLeft));
            Timer.DelayCall(TimeSpan.FromSeconds(6.0), new TimerCallback(BcastTimeLeft));
            Timer.DelayCall(TimeSpan.FromSeconds(8.0), new TimerCallback(BcastTimeLeft));
        }

        /// <summary>
        /// Tell the duelers how much time is left till they initiate their fight.
        /// </summary>
        public void BcastTimeLeft()
        {
            if (m_CurrentDuelers[0] != null && m_CurrentDuelers[1] != null)
            {
                m_CountDown -= 2;
                if (m_CountDown > 0)
                {
                    ((Mobile)m_CurrentDuelers[0]).SendMessage(m_CountDown.ToString() + " seconds...");
                    ((Mobile)m_CurrentDuelers[1]).SendMessage(m_CountDown.ToString() + " seconds...");
                }
                else
                {
                    ((Mobile)m_CurrentDuelers[0]).SendMessage(" FIGHT...");
                    ((Mobile)m_CurrentDuelers[1]).SendMessage(" FIGHT...");
                }
            }
        }

        /// <summary>
        /// Remove the walls that where placed, and unlock the duelers so they can fight.
        /// </summary>
        public void RemoveWall_OnTick()
        {
            for (int index = 0; index < m_WallList.Count; index++)
                ((Item)m_WallList[index]).Delete();

            m_CountDown = 10;
            UnLockDuelers();
        }

        #endregion

        #region Command Events [pvp

        /// <summary>
        /// When the PvP Event is active, players have access to join it.
        /// </summary>
        /// <param name="e">Command Event Args</param>
        public static void JoinEventCommandEvent(CommandEventArgs e)
        {
            if (!e.Mobile.Alive)
            {
                e.Mobile.SendMessage("you can not enter a pvp event if your dead.");
                return;
            }
            if (Running)
            {
                if (AcceptingPlayers)
                {
                    e.Mobile.MoveToWorld(StartLocation, m_MapLocation);
                    e.Mobile.Hidden = false;
                }
                else
                {
                    e.Mobile.SendMessage("the pvp event is not accepting anymore players");
                }
            }
            else
            {
                e.Mobile.SendMessage("The pvp event is not running");
            }
        }


        /// <summary>
        /// When the Server boots up, add the [pvp command to the list.
        /// </summary>
        public static void Initialize()
        {
            CommandSystem.Register("JoinPvP", AccessLevel.Player, new CommandEventHandler(JoinEventCommandEvent));
        }

        #endregion

        #region serial/deserialize

        public PvPStone( Serial serial ) : base( serial )
		{
		}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)8); // version
            //version 8
            writer.Write((int)m_MinimumDuelers);
            writer.Write((DateTime)m_LastReset);
            //version 7
            writer.Write((bool)m_TimerEnabled);
            //version 6
            writer.Write((int)m_CoinsPerRound);
            writer.Write((int)m_CoinsWinner);
            //version 5
            writer.Write((int)m_BroadcastHue);
            //version 4
            writer.Write((int)m_WallHue);
            writer.Write((int)m_WallID);
            //version 3
            writer.Write((Point3D)m_WallMidPoint);
            writer.Write((int)m_WallExtendWidth);
            writer.Write((bool)m_WallNorthToSouth);
            //version 2
            writer.Write((int)m_CurrentRound);
            writer.Write((int)m_TotalParticipants);
            //version 1
            writer.Write((bool)m_Running);
            writer.Write((bool)m_AcceptingPlayers);
            writer.Write((Map)m_MapLocation);
            //version 0
            writer.Write((TimeSpan)m_EventRate);
            writer.Write((Point3D)m_StartLocation);
            writer.Write((Point3D)m_LostLocation);
            writer.Write((Point3D)m_DuelLocation1);
            writer.Write((Point3D)m_DuelLocation2);
            writer.Write((DateTime)m_LastEvent);
            writer.Write((Rectangle2D)m_DuelingArea);
            writer.Write((Rectangle2D)m_StageingArea);
            writer.Write((Rectangle2D)m_ViewingArea);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 8:
                    {
                        m_MinimumDuelers = (int)reader.ReadInt();
                        m_LastReset = (DateTime)reader.ReadDateTime();
                        goto case 7;
                    }
                case 7:
                    {
                        m_TimerEnabled = (bool)reader.ReadBool();
                        goto case 6;
                    }
                case 6:
                    {
                        m_CoinsPerRound = (int)reader.ReadInt();
                        m_CoinsWinner = (int)reader.ReadInt();
                        goto case 5;
                    }
                case 5:
                    {
                        m_BroadcastHue = (int)reader.ReadInt();
                        goto case 4;
                    }
                case 4:
                    {
                        m_WallHue = (int)reader.ReadInt();
                        m_WallID = (int)reader.ReadInt();
                        goto case 3;
                    }
                case 3:
                    {
                        m_WallMidPoint = (Point3D)reader.ReadPoint3D();
                        m_WallExtendWidth = (int)reader.ReadInt(); ;
                        m_WallNorthToSouth = (bool)reader.ReadBool();
                        goto case 2;
                    }
                case 2:
                    {
                        m_CurrentRound = (int)reader.ReadInt();
                        m_TotalParticipants = (int)reader.ReadInt();
                        goto case 1;
                    }
                case 1:
                    {
                        m_Running = (bool)reader.ReadBool();
                        m_AcceptingPlayers = (bool)reader.ReadBool();
                        m_MapLocation = (Map)reader.ReadMap();
                        goto case 0;
                    }
                case 0:
                    {
                        m_EventRate = (TimeSpan)reader.ReadTimeSpan();
                        m_StartLocation = (Point3D)reader.ReadPoint3D();
                        m_LostLocation = (Point3D)reader.ReadPoint3D();
                        m_DuelLocation1 = (Point3D)reader.ReadPoint3D();
                        m_DuelLocation2 = (Point3D)reader.ReadPoint3D();
                        m_LastEvent = (DateTime)reader.ReadDateTime();

                        m_DuelingArea = (Rectangle2D)reader.ReadRect2D();
                        m_StageingArea = (Rectangle2D)reader.ReadRect2D();
                        m_ViewingArea = (Rectangle2D)reader.ReadRect2D();
                        break;
                    }
            }
            if (version == 7)
                m_LastReset = DateTime.Now;

            if (version == 5)
            {
                m_CoinsPerRound = 2;
                m_CoinsWinner = 10;
            }

            if (version == 4)
                m_BroadcastHue = 269;

            if (version == 3)
            {
                m_WallHue = 0;
                m_WallID = 0x0081;
            }

            if (m_DuelList == null)
                m_DuelList = new List<Mobile>();
            if (m_CurrentDuelers == null)
                m_CurrentDuelers = new List<Mobile>();
            if (m_WallList == null)
                m_WallList = new List<Item>();
            if (m_BroadcastList == null)
                m_BroadcastList = new List<Mobile>();
            m_CountDown = 0;
            UpdateRegions(false);

            m_RestartTimer = new InternalTimer(this, TimeSpan.FromSeconds(1.0));
            
            if(m_TimerEnabled)
                m_RestartTimer.Start();
        }

        #endregion

        #region PvP System

        /// <summary>
        /// Starts the PvP Event
        /// </summary>
        public void StartPvP()
        {
            if (!Running)
            {
                Running = true;
                AcceptingPlayers = true;
                LastEvent = DateTime.Now;
                BCastTimer = new BroadcastTimer(this, TimeSpan.FromSeconds(BroadCastTickDelay), TimeSpan.FromSeconds(BroadCastTickDelay));
                BCastTimer.Start();
                string text = "A Player Vs Player Event is starting up in 5 minutes. Type [joinpvp To Join.";
                World.Broadcast(m_BroadcastHue, true, String.Format("{0}", text));
            }
        }

        /// <summary>
        /// Remove all ill effects from a player.
        /// </summary>
        /// <param name="pm"></param>
        public void CleanPlayer(Mobile pm)
        {
            //Cure Poison
            pm.CurePoison(pm);
            //Reset Hit Points
            pm.Hits = pm.HitsMax;
            //Rest Mana
            pm.Mana = pm.ManaMax;
            //Reset Stam
            pm.Stam = pm.StamMax;
            //Cancel any targeting
            Targeting.Target.Cancel(pm);
            //remove abosorption for magic
            pm.MagicDamageAbsorb = 0;
            //remove absorption for melee
            pm.MeleeDamageAbsorb = 0;
            //clear protection spell
            Spells.Second.ProtectionSpell.Registry.Remove(pm);
            //clear curse effect
            Spells.Fourth.CurseSpell.RemoveEffect(pm);
            //clear corpseskin
            Server.Spells.Necromancy.CorpseSkinSpell.RemoveCurse(pm);
            //clear blodd oath
            Server.Spells.Necromancy.BloodOathSpell.RemoveCurse(pm);
            //clear evil omen
            //Server.Spells.Necromancy.EvilOmenSpell.RemoveCurse(pm);
            //remove strangle
            Server.Spells.Necromancy.StrangleSpell.RemoveCurse(pm);
            //clear Paralyzed
            pm.Paralyzed = false;
            //clear defensive spell
            DefensiveSpell.Nullify(pm);
            //remove any combatant
            pm.Combatant = null;
            //remove war mode
            pm.Warmode = false;
            //remove criminal
            pm.Criminal = false;
            //clear agressed list
            pm.Aggressed.Clear();
            //clear agressor list
            pm.Aggressors.Clear();
            //clear delta notoriety
            pm.Delta(MobileDelta.Noto);
            //invalidate any properties due to the previous changes
            pm.InvalidateProperties();
        }

        /// <summary>
        /// Get Event Ready to start fresh again.
        /// </summary>
        public void InitializeEvent()
        {
            //load players into dueling system
            if (ParticipateRegion != null)
            {
                foreach (Mobile pm in ParticipateRegion.GetPlayers())
                {
                    if (pm is PlayerMobile && pm.AccessLevel == AccessLevel.Player)
                    {
                        CleanPlayer((PlayerMobile)pm);
                        m_DuelList.Add(pm);
                    }
                }
            }

            //if there are not at least 4 players (default), close duel and kick all the players.
            if (m_DuelList.Count < m_MinimumDuelers)
            {
                //tell the players there was not enough players for the pvp event
                string text = "There was not enough players for this PVP Event.";
                World.Broadcast(m_BroadcastHue, true, String.Format("{0}", text));

                //clear participant region over to the viewing region with gate
                if (ParticipateRegion != null)
                {
                    foreach (Mobile pm in m_DuelList)
                    {
                        pm.MoveToWorld(m_LostLocation, m_MapLocation);
                    }
                }

                //clear the duellist
                m_DuelList.Clear();

                //close the event
                CloseEvent();

                return;
            }

            //everything is fine, start the tourney
            m_DuelList.Clear();
            StartNewRound();
        }

        /// <summary>
        /// award coins to the player
        /// </summary>
        /// <param name="m">player to give coins too</param>
        /// <param name="tourneywinner">is this the tourney winner</param>
        public void GiveGold(Mobile m, bool tourneywinner)
        {
            //set up coin amount based on round and if they are the tourney winner
            int total = tourneywinner ? m_CoinsPerRound * m_CurrentRound + m_CoinsWinner : m_CoinsPerRound * m_CurrentRound;
            
            //create a floating set of coins
            BankCheck check = new BankCheck( total );

            //if the players pack is full, delete the floating coins
            if (!m.AddToBackpack(check))
            {
                check.Delete();
            }
        }

        private Mobile m_Winner;
        private Mobile m_Loser;

        /// <summary>
        /// This event fires from the PvPRegion when a player is about to die.
        /// </summary>
        /// <param name="winner">the winner of the match</param>
        /// <param name="loser">the looser of the match</param>
        public void MatchEnd(Mobile winner, Mobile loser)
        {
            m_Winner = winner;
            m_Loser = loser;
            //clean players
            CleanPlayer(winner);
            //clean players
            CleanPlayer(loser);
            //broadcast message to others
            BroadCastMessage(winner.Name + " has defeated " + loser.Name + " in this match");
            //move back to duel location 1
            winner.MoveToWorld(m_DuelLocation1, m_MapLocation);
            //move back to duel location 2
            loser.MoveToWorld(m_DuelLocation2, m_MapLocation);
            LockDuelers();
            Timer.DelayCall(TimeSpan.FromSeconds(3.0), new TimerCallback(DelayMatchEnd));
        }

        public void DelayMatchEnd()
        {
            UnLockDuelers();
            //clear dueler list
            m_CurrentDuelers.Clear();
            //move winner to start location
            m_Winner.MoveToWorld(m_StartLocation, m_MapLocation);
            //move loser to viewing area
            m_Loser.MoveToWorld(m_LostLocation, m_MapLocation);
            //award looser with pandora coins
            GiveGold(m_Loser, false);
            //award the pvp rating points
            //AwardPoints(m_Winner, m_Loser);

            //if there are more then 1 player in the participate region start the next match
            if (ParticipateRegion.GetPlayers().Count > 1)
            {
                m_DuelList.Remove(m_Winner);
                m_DuelList.Remove(m_Loser);

                StartNextMatch();
            }
            //else there are no more players, award the winner.
            else
            {
                string text = m_Winner.Name + " has won the pvp tournament";
                World.Broadcast(m_BroadcastHue, true, String.Format("{0}", text));
                //award winner with extra points.
                m_Winner.SendMessage(55, "you recieve an additional " + m_TotalParticipants.ToString() + " points to your raiting");
                //award coins
                GiveGold(m_Winner, true);
                //reset current round
                m_CurrentRound = 0;
                //close the event
                CloseEvent();
                //move winner to spectator area.
                m_Winner.MoveToWorld(m_LostLocation, m_MapLocation);
            }        
        }

        /// <summary>
        /// Start the next match
        /// </summary>
        public void StartNextMatch()
        {
            //if there are not enough duelers for a match start a new round.
            if (m_DuelList.Count > 1)
                LoadPlayers();
            else
            {
                m_DuelList.Clear();
                StartNewRound();
            }
        }

        /// <summary>
        /// Send message to people in the 2 regions
        /// </summary>
        /// <param name="message">a string message to send</param>
        public void BroadCastMessage(string message)
        {
            m_BroadcastList.Clear();

            //grab players in ParticipateRegion
            if (ParticipateRegion != null)
            {
                foreach (Mobile pm in ParticipateRegion.GetPlayers())
                {
                    if (pm is PlayerMobile)
                    {
                        CleanPlayer((PlayerMobile)pm);
                        m_BroadcastList.Add(pm);
                    }
                }
            }

            //grab players in SpectatorRegion
            if (SpectatorRegion != null)
            {
                foreach (Mobile pm in SpectatorRegion.GetPlayers())
                {
                    if (pm is PlayerMobile)
                    {
                        CleanPlayer((PlayerMobile)pm);
                        m_BroadcastList.Add(pm);
                    }
                }
            }

            //Send themt he message
            foreach (Mobile m in m_BroadcastList)
            {
                if (m != null)
                    m.SendMessage(55, message);
            }
        }

        /// <summary>
        /// Lock the duelers so they cant move/fight yet
        /// </summary>
        public void LockDuelers()
        {
            if (m_CurrentDuelers[0] != null && m_CurrentDuelers[1] != null)
            {
                //Freeze and Bless dueler one
                ((Mobile)m_CurrentDuelers[0]).Blessed = true;
                ((Mobile)m_CurrentDuelers[0]).Frozen = true;

                //Freeze and Bless duler two
                ((Mobile)m_CurrentDuelers[1]).Blessed = true;
                ((Mobile)m_CurrentDuelers[1]).Frozen = true;
            }
        }

        /// <summary>
        /// Unlock the Duelers so they can move and fight.
        /// </summary>
        public void UnLockDuelers()
        {
            if (m_CurrentDuelers[0] != null && m_CurrentDuelers[1] != null)
            {
                //Unfreeze and Unbless dueler one
                ((Mobile)m_CurrentDuelers[0]).Blessed = false;
                ((Mobile)m_CurrentDuelers[0]).Frozen = false;

                //Unfreeze and Unbless duler two
                ((Mobile)m_CurrentDuelers[1]).Blessed = false;
                ((Mobile)m_CurrentDuelers[1]).Frozen = false;
            }
        }

        /// <summary>
        /// Start a new round of combat between the participants of the pvp event.
        /// </summary>
        public void StartNewRound()
        {
            //update current round
            m_CurrentRound++;

            //load players into dueling system
            if (ParticipateRegion != null)
            {
                foreach (Mobile pm in ParticipateRegion.GetPlayers())
                {
                    if (pm is PlayerMobile && pm.AccessLevel == AccessLevel.Player)
                    {
                        CleanPlayer((PlayerMobile)pm);
                        m_DuelList.Add(pm);
                    }
                }
            }

            //tell players what round it is
            BroadCastMessage("Starting Round #" + m_CurrentRound.ToString());

            //if this is the first round, add up the total players participating.
            if (m_CurrentRound == 1)
            {
                m_TotalParticipants = m_DuelList.Count;
            }

            //load players into the duel pit.
            LoadPlayers();
        }

        /// <summary>
        /// Load all the players into a duel match.
        /// </summary>
        public void LoadPlayers()
        {
            if (m_DuelList.Count > 1)
            {

                //Set up a random generator
                Random rand = new Random(DateTime.Now.Millisecond);

                //Grab a random player, set them up, toss them into the fray
                int duelerone = rand.Next(m_DuelList.Count);
                Mobile m1 = m_DuelList[duelerone] as Mobile;
                m_DuelList.Remove(m1);

                //Grab a random player, set them up, toss them into the fray
                int duelertwo = rand.Next(m_DuelList.Count);
                Mobile m2 = m_DuelList[duelertwo] as Mobile;
                m_DuelList.Remove(m2);

                //Move Duelers
                m1.MoveToWorld(m_DuelLocation1, m_MapLocation);
                m2.MoveToWorld(m_DuelLocation2, m_MapLocation);

                //Add the two duelers to the duelers list
                m_CurrentDuelers.Add(m2);
                m_CurrentDuelers.Add(m1);

                //Lock them in place
                LockDuelers();

                //Make them face each other
                SetFacing(m1,m2);

                //Drop the wall down between the duelers
                DropWall();
                
                //broadcast next matchup to viewers
                BroadCastMessage("Next Match: " + m1.Name + " vs " + m2.Name);
            }
            else
            {
                m_DuelList.Clear();
                StartNewRound();
            }
        }

        /// <summary>
        /// Make sure the players are facing each other before the duel.
        /// </summary>
        /// <param name="m1">dueler 1</param>
        /// <param name="m2">dueler 2</param>
        public void SetFacing(Mobile m1, Mobile m2)
        {
            if (m_WallNorthToSouth)
            {
                if (m1.Location.X < m2.Location.X)
                {
                    m1.Direction = Direction.East;
                    m2.Direction = Direction.West;
                }
                else
                {
                    m1.Direction = Direction.West;
                    m2.Direction = Direction.East;
                }
            }
            else
            {
                if (m1.Location.Y < m2.Location.Y)
                {
                    m1.Direction = Direction.South;
                    m2.Direction = Direction.North;
                }
                else
                {
                    m1.Direction = Direction.North;
                    m2.Direction = Direction.South;
                }
            }
        }

        /// <summary>
        /// Close the event down here.
        /// </summary>
        public void CloseEvent()
        {
            //clear the duel list (or else they will be in system on next duel)
            m_DuelList.Clear();
            //clear the broadcast list
            m_BroadcastList.Clear();
            //clear the CurrentDuelers list.
            m_CurrentDuelers.Clear();
            //clear the wall list
            m_WallList.Clear();
            //reset the duel round
            m_CurrentRound = 0;
            //shut event off
            m_Running = false;
            //clear total particpants.
            m_TotalParticipants = 0;
            //clear broadcastticks
            m_CurrentBroadCastTicks = 0;
            //close command to join

            if (m_RestartTimer != null)
            {
                m_AcceptingPlayers = false; m_RestartTimer = new InternalTimer(this, TimeSpan.FromSeconds(1.0));
                m_RestartTimer.Start();
            }
        }

        #endregion

        #region Broadcasting System

        /// <summary>
        /// Send a broadcast worldwide for players to join
        /// </summary>
        public virtual void DoBcast()
        {
            m_CurrentBroadCastTicks++;
            if (m_CurrentBroadCastTicks <= BroadCastMaxTicks)
            {
                int minutesleft = BroadCastMaxTicks - m_CurrentBroadCastTicks + 1;
                string text = "A Player Vs Player Event is starting up in " + minutesleft.ToString() + " minutes.  Type [joinpvp To Join.";
                World.Broadcast(m_BroadcastHue, true, String.Format("{0}", text));
            }
            else
            {
                m_CurrentBroadCastTicks = 0;
                BCastTimer.Stop();
                m_AcceptingPlayers = false;
                InitializeEvent();
            }
        }
        /// <summary>
        /// Timer Class for Broad Casting to players every 15 seconds.
        /// </summary>
        private class BroadcastTimer : Timer
        {
            private PvPStone m_stone;
            public BroadcastTimer(PvPStone stone, TimeSpan span1, TimeSpan span2) : base(span1, span2)
            {
                Priority = TimerPriority.OneSecond;
                m_stone = stone;
            }

            protected override void OnTick()
            {
                m_stone.DoBcast();
            }
        }

        #endregion

        #region AutoTimer

        private void RestartTick()
        {
            if (m_TimerEnabled)
            {
                if (m_LastEvent + m_EventRate <= DateTime.Now)
                {
                    KickStart = true;
                    m_LastEvent = DateTime.Now;
                }

                if (m_RestartTimer != null)
                    m_RestartTimer.Stop();

                if (!m_Running && m_RestartTimer != null)
                {
                    m_RestartTimer = new InternalTimer(this, TimeSpan.FromSeconds(1.0));
                    m_RestartTimer.Start();
                }
            }
        }

        public override void OnDelete()
        {
            base.OnDelete();

            if (m_RestartTimer != null)
                m_RestartTimer.Stop();
        }

        private class InternalTimer : Timer
        {
            private PvPStone m_stone;

            public InternalTimer(PvPStone spawner, TimeSpan delay) : base(delay)
            {
                m_stone = spawner;
            }

            protected override void OnTick()
            {
                if (m_stone != null)
                    if (!m_stone.Deleted)
                        m_stone.RestartTick();
            }
        }

        #endregion

        #region Region Management


        /// <summary>
        /// Update the regions so everything works as it should.
        /// </summary>
        /// <param name="Unregister"></param>
        public void UpdateRegions(bool Unregister)
        {
            if (Unregister)
            {
                PvPRegion.Unregister();
                SpectatorRegion.Unregister();
                ParticipateRegion.Unregister();
            }
            Rectangle2D[] m_Rects = new Rectangle2D[] { m_DuelingArea };
            PvPRegion = new PvPRegion(this, "The Pit", this.Map, m_Rects);
            m_Rects = new Rectangle2D[] { m_ViewingArea };
            SpectatorRegion = new PvPStagingRegion(this, "Spectators", this.Map, m_Rects);
            m_Rects = new Rectangle2D[] { m_StageingArea };
            ParticipateRegion = new PvPStagingRegion(this, "Gladiators", this.Map, m_Rects);

            PvPRegion.Register();
            SpectatorRegion.Register();
            ParticipateRegion.Register();
        }

        #endregion
    }
}
