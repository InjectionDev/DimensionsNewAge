using System;
using Server.Network;

namespace Server.Misc
{
	public class LoginStats
	{
		public static void Initialize()
		{
			// Register our event handler
			EventSink.Login += new LoginEventHandler( EventSink_Login );
		}

		private static void EventSink_Login( LoginEventArgs args )
		{
			int userCount = NetState.Instances.Count;
			int itemCount = World.Items.Count;
			int mobileCount = World.Mobiles.Count;

			Mobile m = args.Mobile;

            m.SkillsCap = 55000;

            for (int i = 0; i < m.Skills.Length; ++i)
            {
                m.Skills[i].Cap = 1000;

                if (m.Skills[i].Base > 100)
                    m.Skills[i].Base = 100;
            }

            //m.SendMessage( "Welcome, {0}! There {1} currently {2} user{3} online, with {4} item{5} and {6} mobile{7} in the world.",
            //    args.Mobile.Name,
            //    userCount == 1 ? "is" : "are",
            //    userCount, userCount == 1 ? "" : "s",
            //    itemCount, itemCount == 1 ? "" : "s",
            //    mobileCount, mobileCount == 1 ? "" : "s" );

            m.SendMessage(365, string.Format("Bem-vindo ao DimensNewAge {0}.", args.Mobile.Name));
            m.SendMessage(365, string.Format("Neste momento existem {0} usuarios online.", userCount >= 2 ? userCount + 1 : userCount)); 
            m.SendMessage(365, string.Format("Temos '{0}' Itens e '{1}' Mobs pelo mundo.", itemCount, mobileCount)); 
		}
	}
}