using System;
using System.Collections.Generic;
using System.Net;
using Server.Misc;


namespace Server.Commands
{
    public class ConnectionMonitor : Timer
    {

        private static IPAddress m_PublicAddress;
        private static IPAddress new_PublicAddress;

        private static bool IsActive = false;

        public static void Initialize()
        {
            if (IsActive)
            {
                m_PublicAddress = ServerList.FindPublicAddress();

                new ConnectionMonitor().Start();
            }
        }

        public ConnectionMonitor()
            : base(TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(1.0))
		{
            Logger.LogMessage(string.Format("Monitoring IP changes... current: {0}", (m_PublicAddress == null ? "No Connection" : m_PublicAddress.ToString())), "ConnectionMonitor");
            Console.WriteLine("Monitoring IP changes...");
		}

        protected override void OnTick()
        {
            new_PublicAddress = ServerList.FindPublicAddress();

            if (new_PublicAddress == null)
            {
                Logger.LogMessage(string.Format("No Connection!"), "ConnectionMonitor");
                m_PublicAddress = null;
            }
            else if (m_PublicAddress == null && new_PublicAddress != null)
            {
                Logger.LogMessage(string.Format("Find New Connection!"), "ConnectionMonitor");
                HasIpChanged();
            }
            else if (!m_PublicAddress.Equals(new_PublicAddress))
            {
                HasIpChanged();
            }
        }

        private static void HasIpChanged()
        {
            Logger.LogMessage(string.Format("IP Changed! '{0}' -> '{1}'", (m_PublicAddress == null ? "No Connection" : m_PublicAddress.ToString()), new_PublicAddress), "ConnectionMonitor");
            Console.WriteLine(string.Format("IP Changed! '{0}' -> '{1}'", (m_PublicAddress == null ? "No Connection" : m_PublicAddress.ToString()), new_PublicAddress));
            World.Broadcast(0x35, true, "Reiniciando Servidor...");
            //AutoSave.Save();
            //Core.Kill(true);
        }

    }
}

