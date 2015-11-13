using System;
using System.Collections.Generic;

namespace Server.Commands
{
    public class HearAll
    {
        private static List<Mobile> m_HearAll = new List<Mobile>();

        public static void Initialize()
        {
            CommandSystem.Register("hearall", AccessLevel.GameMaster, new CommandEventHandler(HearAll_OnCommand));
            EventSink.Speech += new SpeechEventHandler(OnSpeech);
            EventSink.Login += new LoginEventHandler(EventSink_Login);
            EventSink.Logout += new LogoutEventHandler(EventSink_Logout);
        }

        static void EventSink_Logout(LogoutEventArgs e)
        {
            Logger.LogMessage("Logout: " + e.Mobile.RawName, "Logins");

            foreach (Mobile mobile in m_HearAll)
            {
                mobile.SendMessage("Logout: " + e.Mobile.RawName);
                
            }
        }

        static void EventSink_Login(LoginEventArgs e)
        {
            Logger.LogMessage("Login: " + e.Mobile.RawName, "Logins");

            foreach (Mobile mobile in m_HearAll)
            {
                mobile.SendMessage("Login: " + e.Mobile.RawName);
                
            }
        }

        public static void OnSpeech(SpeechEventArgs e)
        {
            if (m_HearAll.Count > 0)
            {
                string msg = String.Format("({0}): {1}", e.Mobile.RawName, e.Speech);
                foreach (Mobile mobile in m_HearAll)
                {
                    mobile.SendMessage(msg);
                }
            }
        }

        public static void HearAll_OnCommand(CommandEventArgs e)
        {
            if (m_HearAll.Contains(e.Mobile))
            {
                m_HearAll.Remove(e.Mobile);
                e.Mobile.SendMessage("HearAll OFF.");
            }
            else
            {
                m_HearAll.Add(e.Mobile);
                e.Mobile.SendMessage("HearAll ON.");
            }
        }
    }
}

