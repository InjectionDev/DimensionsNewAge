using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DimensionsNewAge.Scripts.Customs.Engines;
using Server.Mobiles;
using Server.Regions;

namespace Server.Items
{
    public class RespawnControllerStone : Item
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public int QtRespawnMobileRare1 { get; set; }
        [CommandProperty(AccessLevel.GameMaster)]
        public int QtRespawnMobileRare2 { get; set; }
        [CommandProperty(AccessLevel.GameMaster)]
        public int QtRespawnMobileRare3 { get; set; }
        [CommandProperty(AccessLevel.GameMaster)]
        public int QtRespawnMobileRare4 { get; set; }
        [CommandProperty(AccessLevel.GameMaster)]
        public int QtRespawnMobileRare5 { get; set; }

        public System.Collections.ArrayList MobileListRare1 { get; set; }
        public System.Collections.ArrayList MobileListRare2 { get; set; }
        public System.Collections.ArrayList MobileListRare3 { get; set; }
        public System.Collections.ArrayList MobileListRare4 { get; set; }
        public System.Collections.ArrayList MobileListRare5 { get; set; }

        [Constructable]
        public RespawnControllerStone()
            : base(0xEDC)
        {
            Name = "RespawnController Stone";
            Movable = false;
            Hue = 1969;

            QtRespawnMobileRare1 = 10;
            QtRespawnMobileRare2 = 8;
            QtRespawnMobileRare3 = 0;
            QtRespawnMobileRare4 = 0;
            QtRespawnMobileRare5 = 0;

            MobileListRare1 = new System.Collections.ArrayList();
            MobileListRare2 = new System.Collections.ArrayList();
            MobileListRare3 = new System.Collections.ArrayList();
            MobileListRare4 = new System.Collections.ArrayList();
            MobileListRare5 = new System.Collections.ArrayList();

            this.InitializeRespawnController();
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel >= AccessLevel.GameMaster)
            { 
                foreach(Mobile mobile in MobileListRare1)
                    from.SendMessage(string.Format("Rare1 -> {0} -> {1}/{2} {3}", mobile.Name, mobile.Location.X, mobile.Location.Y, mobile.Map.Name));
                foreach(Mobile mobile in MobileListRare2)
                    from.SendMessage(string.Format("Rare2 -> {0} -> {1}/{2} {3}", mobile.Name, mobile.Location.X, mobile.Location.Y, mobile.Map.Name));
                foreach(Mobile mobile in MobileListRare3)
                    from.SendMessage(string.Format("Rare3 -> {0} -> {1}/{2} {3}", mobile.Name, mobile.Location.X, mobile.Location.Y, mobile.Map.Name));
                foreach(Mobile mobile in MobileListRare4)
                    from.SendMessage(string.Format("Rare4 -> {0} -> {1}/{2} {3}", mobile.Name, mobile.Location.X, mobile.Location.Y, mobile.Map.Name));
                foreach(Mobile mobile in MobileListRare5)
                    from.SendMessage(string.Format("Rare5 -> {0} -> {1}/{2} {3}", mobile.Name, mobile.Location.X, mobile.Location.Y, mobile.Map.Name));
            }
        }

        private void InitializeRespawnController()
        {
            Timer smash = new RespawnControllerTimer(this);
            smash.Start();
        }

        public RespawnControllerStone(Serial serial)
            : base(serial)
		{
		}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version

            writer.Write(QtRespawnMobileRare1);
            writer.Write(QtRespawnMobileRare2);
            writer.Write(QtRespawnMobileRare3);
            writer.Write(QtRespawnMobileRare4);
            writer.Write(QtRespawnMobileRare5);
            writer.WriteMobileList(MobileListRare1);
            writer.WriteMobileList(MobileListRare2);
            writer.WriteMobileList(MobileListRare3);
            writer.WriteMobileList(MobileListRare4);
            writer.WriteMobileList(MobileListRare5);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        QtRespawnMobileRare1 = reader.ReadInt();
                        QtRespawnMobileRare2 = reader.ReadInt();
                        QtRespawnMobileRare3 = reader.ReadInt();
                        QtRespawnMobileRare4 = reader.ReadInt();
                        QtRespawnMobileRare5 = reader.ReadInt();
                        MobileListRare1 = reader.ReadMobileList();
                        MobileListRare2 = reader.ReadMobileList();
                        MobileListRare3 = reader.ReadMobileList();
                        MobileListRare4 = reader.ReadMobileList();
                        MobileListRare5 = reader.ReadMobileList();
                        break;
                    }
            }

            this.InitializeRespawnController();
        }
    }

    public class RespawnControllerTimer : Timer
    {

        private RespawnControllerStone m_ControllerStone;

        private static List<Item> respawnList;

        public RespawnControllerTimer(RespawnControllerStone Controller)
            : base(TimeSpan.Zero, TimeSpan.FromSeconds(30))
        {
            m_ControllerStone = Controller;

            // Possiveis locais para respawn
            respawnList = new List<Item>();
            foreach (Item item in World.Items.Values)
            {
                if (item is PremiumSpawner)
                    respawnList.Add(item);
            }

            Logger.LogRespawnControllerMessage(string.Format("PremiumSpawner Count: " + respawnList.Count));
        }

        protected override void OnTick()
        {
            ValidateMobs();
            RefreshRespawn();

            AnnounceLocation();
        }

        private static DateTime dtLastAnnounce;
        private void AnnounceLocation()
        {
            if (dtLastAnnounce == null || DateTime.Now > dtLastAnnounce.AddMinutes(30))
            {
                Mobile mobileAnnounce = m_ControllerStone.MobileListRare1[new Random().Next(m_ControllerStone.MobileListRare1.Count)] as Mobile;
                Region regionAnnounce = Region.Find(mobileAnnounce.Location, mobileAnnounce.Map);

                if (regionAnnounce != null && !string.IsNullOrEmpty(regionAnnounce.Name))
                {
                    Logger.LogRespawnControllerMessage(string.Format("Rumores se espalham sobre uma criatura estranha nas proximidades de {0}... ({1}) {2}/{3} {4}", regionAnnounce.Name, mobileAnnounce.Serial, mobileAnnounce.Location.X, mobileAnnounce.Location.Y, mobileAnnounce.Map));
                    World.Broadcast(1428, true, string.Format("Rumores se espalham sobre uma criatura estranha nas proximidades de {0}...", regionAnnounce.Name));
                    dtLastAnnounce = DateTime.Now;
                }
                else
                {
                    AnnounceLocation();
                }
            }
        }

        private void RefreshRespawn()
        {
            int qtMobilesToCreateRare1 = m_ControllerStone.QtRespawnMobileRare1 - m_ControllerStone.MobileListRare1.Count;
            int qtMobilesToCreateRare2 = m_ControllerStone.QtRespawnMobileRare2 - m_ControllerStone.MobileListRare2.Count;
            int qtMobilesToCreateRare3 = m_ControllerStone.QtRespawnMobileRare3 - m_ControllerStone.MobileListRare3.Count;
            int qtMobilesToCreateRare4 = m_ControllerStone.QtRespawnMobileRare4 - m_ControllerStone.MobileListRare4.Count;
            int qtMobilesToCreateRare5 = m_ControllerStone.QtRespawnMobileRare5 - m_ControllerStone.MobileListRare5.Count;

            Point3D locationRandom = new Point3D();
            Map mapRandom = Map.Felucca;

            for (int i = 1; i <= qtMobilesToCreateRare1; i++)
            {
                GetNewRandomLocation(ref locationRandom, ref mapRandom);
                Mobile mobile = RewardUtil.CreateRewardInstance(RewardUtil.RegularMountTypes) as Mobile;
                mobile.MoveToWorld(locationRandom, mapRandom);

                Logger.LogRespawnControllerMessage(string.Format("New MobileRare1 -> {0}/{1} {2} -> {3} ({4})", locationRandom.X, locationRandom.Y, mapRandom.Name, mobile.Name, mobile.Serial));
                m_ControllerStone.MobileListRare1.Add(mobile);
            }

            for (int i = 1; i <= qtMobilesToCreateRare2; i++)
            {
                GetNewRandomLocation(ref locationRandom, ref mapRandom);
                Mobile mobile = RewardUtil.CreateRewardInstance(typeof(Balron)) as Mobile;
                mobile.MoveToWorld(locationRandom, mapRandom);

                Logger.LogRespawnControllerMessage(string.Format("New MobileRare2 -> {0}/{1} {2} -> {3} ({4})", locationRandom.X, locationRandom.Y, mapRandom.Name, mobile.Name, mobile.Serial));
                m_ControllerStone.MobileListRare2.Add(mobile);
            }

            for (int i = 1; i <= qtMobilesToCreateRare3; i++)
            {
                GetNewRandomLocation(ref locationRandom, ref mapRandom);
                Mobile mobile = RewardUtil.CreateRewardInstance(typeof(RidableLlama)) as Mobile;
                mobile.MoveToWorld(locationRandom, mapRandom);

                Logger.LogRespawnControllerMessage(string.Format("New MobileRare3 -> {0}/{1} {2} -> {3} ({4})", locationRandom.X, locationRandom.Y, mapRandom.Name, mobile.Name, mobile.Serial));
                m_ControllerStone.MobileListRare3.Add(mobile);
            }

            for (int i = 1; i <= qtMobilesToCreateRare4; i++)
            {
                GetNewRandomLocation(ref locationRandom, ref mapRandom);
                Mobile mobile = RewardUtil.CreateRewardInstance(typeof(Ridgeback)) as Mobile;
                mobile.MoveToWorld(locationRandom, mapRandom);

                Logger.LogRespawnControllerMessage(string.Format("New MobileRare4 -> {0}/{1} {2} -> {3} ({4})", locationRandom.X, locationRandom.Y, mapRandom.Name, mobile.Name, mobile.Serial));
                m_ControllerStone.MobileListRare4.Add(mobile);
            }

            for (int i = 1; i <= qtMobilesToCreateRare5; i++)
            {
                GetNewRandomLocation(ref locationRandom, ref mapRandom);
                Mobile mobile = RewardUtil.CreateRewardInstance(typeof(Nightmare)) as Mobile;
                mobile.MoveToWorld(locationRandom, mapRandom);

                Logger.LogRespawnControllerMessage(string.Format("New MobileRare5 -> {0}/{1} {2} -> {3} ({4})", locationRandom.X, locationRandom.Y, mapRandom.Name, mobile.Name, mobile.Serial));
                m_ControllerStone.MobileListRare5.Add(mobile);
            }
        }

        private object objLock = new object();
        private void GetNewRandomLocation(ref Point3D pLocation, ref Map mMap)
        {
            lock (objLock)
            {
                Item item = respawnList[new Random().Next(respawnList.Count)];
                pLocation = item.Location;
                mMap = item.Map;
				
				System.Threading.Thread.Sleep(200);

                // Mapas habilitados
                if (mMap != Map.Felucca && mMap != Map.Ilshenar && mMap != Map.Malas)
                {
                    Logger.LogRespawnControllerMessage(string.Format("Map -> {0} -> Novo", mMap.Name));
                    GetNewRandomLocation(ref pLocation, ref mMap);
                    return;
                }

                Region region = Region.Find(pLocation, mMap);
                if (region != null && region.AllowSpawn() == false)
                {
                    Logger.LogRespawnControllerMessage(string.Format("Region -> {0} !AllowSpawn -> Novo", region.Name));
                    GetNewRandomLocation(ref pLocation, ref mMap);
                    return;
                }

                if (region != null && region is GuardedRegion)
                {
                    Logger.LogRespawnControllerMessage(string.Format("Region -> {0} GuardedRegion -> Novo", region.Name));
                    GetNewRandomLocation(ref pLocation, ref mMap);
                    return;
                }
                
            }
        }

        private void ValidateMobs()
        {
            List<Mobile> mobileListToRemove = new List<Mobile>();

            foreach (Mobile mobile in m_ControllerStone.MobileListRare1)
                if (mobile == null || mobile.Deleted || mobile.Alive == false || (mobile is BaseCreature && ((BaseCreature)mobile).Controlled))
                    mobileListToRemove.Add(mobile);

            foreach (Mobile mobile in m_ControllerStone.MobileListRare2)
                if (mobile == null || mobile.Deleted || mobile.Alive == false || (mobile is BaseCreature && ((BaseCreature)mobile).Controlled))
                    mobileListToRemove.Add(mobile);

            foreach (Mobile mobile in m_ControllerStone.MobileListRare3)
                if (mobile == null || mobile.Deleted || mobile.Alive == false || (mobile is BaseCreature && ((BaseCreature)mobile).Controlled))
                    mobileListToRemove.Add(mobile);

            foreach (Mobile mobile in m_ControllerStone.MobileListRare4)
                if (mobile == null || mobile.Deleted || mobile.Alive == false || (mobile is BaseCreature && ((BaseCreature)mobile).Controlled))
                    mobileListToRemove.Add(mobile);

            foreach (Mobile mobile in m_ControllerStone.MobileListRare5)
                if (mobile == null || mobile.Deleted || mobile.Alive == false || (mobile is BaseCreature && ((BaseCreature)mobile).Controlled))
                    mobileListToRemove.Add(mobile);

            foreach (Mobile mobile in mobileListToRemove)
            {
                if (m_ControllerStone.MobileListRare1.Contains(mobile))
                    m_ControllerStone.MobileListRare1.Remove(mobile);
                if (m_ControllerStone.MobileListRare2.Contains(mobile))
                    m_ControllerStone.MobileListRare2.Remove(mobile);
                if (m_ControllerStone.MobileListRare3.Contains(mobile))
                    m_ControllerStone.MobileListRare3.Remove(mobile);
                if (m_ControllerStone.MobileListRare4.Contains(mobile))
                    m_ControllerStone.MobileListRare4.Remove(mobile);
                if (m_ControllerStone.MobileListRare5.Contains(mobile))
                    m_ControllerStone.MobileListRare5.Remove(mobile);
            }
        }
    }
}
