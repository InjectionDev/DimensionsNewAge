using System;
using Server;
using Server.Commands;
using Server.Mobiles;
using Server.Regions;
using Server.Gumps;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


namespace DimensionsNewAge.Scripts.Customs.Engines
{
    public class SingletonEvent
    {

        private static SingletonEvent singletonEvent;

        private List<RewardItem> currentEventRewardList;
        public List<RewardItem> CurrentEventRewardList
        {
            get
            {
                return this.currentEventRewardList;
            }
            set
            {
                this.currentEventRewardList = value;
            }
        }
            
        private Point3D currentEventLocation;
        public Point3D CurrentEventLocation
        {
            get
            {
                return this.currentEventLocation;
            }
            set
            {
                this.currentEventLocation = value;
            }
        }

        private Map currentEventMap;
        public Map CurrentEventMap
        {
            get
            {
                return this.currentEventMap;
            }
            set
            {
                this.currentEventMap = value;
            }
        }

        private Item currentEventStone;
        public Item CurrentEventStone
        {
            get
            {
                return this.currentEventStone;
            }
            set
            {
                this.currentEventStone = value;
            }
        }

        private EnumEventBase.EnumEventType currentEventType;
        public EnumEventBase.EnumEventType CurrentEventType
        {
            get
            {
                return this.currentEventType;
            }
            set
            {
                this.currentEventType = value;
            }
        }
        
        private bool isEventRunning;
        public bool IsEventRunning
        {
            get
            {
                return this.isEventRunning;
            }
            set
            {
                this.isEventRunning = value;
            }
        }

        private bool isAcceptingPlayers;
        public bool IsAcceptingPlayers
        {
            get
            {
                return this.isAcceptingPlayers;
            }
            set
            {
                this.isAcceptingPlayers = value;
            }
        }

        private bool isAutomaticEvent;
        public bool IsAutomaticEvent
        {
            get
            {
                return this.isAutomaticEvent;
            }
            set
            {
                this.isAutomaticEvent = value;
            }
        }

        private bool blockSpells;
        public bool BlockSpells
        {
            get
            {
                return this.blockSpells;
            }
            set
            {
                this.blockSpells = value;
            }
        }

        private bool blockPots;
        public bool BlockPots
        {
            get
            {
                return this.blockPots;
            }
            set
            {
                this.blockPots = value;
            }
        }

        private bool allowLooting;
        public bool AllowLooting
        {
            get
            {
                return this.allowLooting;
            }
            set
            {
                this.allowLooting = value;
            }
        }

        private bool isArmyMode;
        public bool IsArmyMode
        {
            get
            {
                return this.isArmyMode;
            }
            set
            {
                this.isArmyMode = value;
            }
        }

        private bool blockBow;
        public bool BlockBow
        {
            get
            {
                return this.blockBow;
            }
            set
            {
                this.blockBow = value;
            }
        }

        private bool hasBadMacroerMode;
        public bool HasBadMacroerMode
        {
            get
            {
                return this.hasBadMacroerMode;
            }
            set
            {
                this.hasBadMacroerMode = value;
            }
        }

        private bool hasAntiPanelaMode;
        public bool HasAntiPanelaMode
        {
            get
            {
                return this.hasAntiPanelaMode;
            }
            set
            {
                this.hasAntiPanelaMode = value;
            }
        }

        private bool hasAntiCamperMode;
        public bool HasAntiCamperMode
        {
            get
            {
                return this.hasAntiCamperMode;
            }
            set
            {
                this.hasAntiCamperMode = value;
            }
        }

        private bool isTeamMode;
        public bool IsTeamMode
        {
            get
            {
                return this.isTeamMode;
            }
            set
            {
                if (SingletonEvent.Instance.IsEventRunning == false)
                    this.isTeamMode = value;
            }
        }

        public static SingletonEvent Instance
        {
            get
            {
                if (singletonEvent == null)
                {
                    singletonEvent = new SingletonEvent();

                    singletonEvent.currentEventRewardList = new List<RewardItem>();
                    
                }

                return singletonEvent;
            }
        }

    }
}
