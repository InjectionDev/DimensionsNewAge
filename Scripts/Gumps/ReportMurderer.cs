using System;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server.Mobiles;

namespace Server.Gumps
{
	public class ReportMurdererGump : Gump
	{
		private int m_Idx;
		private List<Mobile> m_Killers;
		private Mobile m_Victum;

        public new void AddHtml(int x, int y, int weight, int height, string text, bool background, bool scrollbar)
        {
            base.AddHtml(x, y, weight, height, "<body><BASEFONT COLOR=#E6E8FA>" + text + "</body>", background, scrollbar);
        }

		public static void Initialize()
		{
			EventSink.PlayerDeath += new PlayerDeathEventHandler( EventSink_PlayerDeath );
		}

		public static void EventSink_PlayerDeath( PlayerDeathEventArgs e )
		{
			Mobile m = e.Mobile;

			List<Mobile> killers = new List<Mobile>();
			List<Mobile> toGive  = new List<Mobile>();

			foreach ( AggressorInfo ai in m.Aggressors )
			{
				if ( ai.Attacker.Player && ai.CanReportMurder && !ai.Reported )
				{
					if (!Core.SE || !((PlayerMobile)m).RecentlyReported.Contains(ai.Attacker))
					{
						killers.Add(ai.Attacker);
						ai.Reported = true;
						ai.CanReportMurder = false;
					}
				}
				if ( ai.Attacker.Player && (DateTime.Now - ai.LastCombatTime) < TimeSpan.FromSeconds( 30.0 ) && !toGive.Contains( ai.Attacker ) )
					toGive.Add( ai.Attacker );
			}

			foreach ( AggressorInfo ai in m.Aggressed )
			{
				if ( ai.Defender.Player && (DateTime.Now - ai.LastCombatTime) < TimeSpan.FromSeconds( 30.0 ) && !toGive.Contains( ai.Defender ) )
					toGive.Add( ai.Defender );
			}

			foreach ( Mobile g in toGive )
			{
				int n = Notoriety.Compute( g, m );

				int theirKarma = m.Karma, ourKarma = g.Karma;
				bool innocent = ( n == Notoriety.Innocent );
				bool criminal = ( n == Notoriety.Criminal || n == Notoriety.Murderer );

				int fameAward = m.Fame / 200;
				int karmaAward = 0;

				if ( innocent )
					karmaAward = ( ourKarma > -2500 ? -850 : -110 - (m.Karma / 100) );
				else if ( criminal )
					karmaAward = 50;

				Titles.AwardFame( g, fameAward, false );
				Titles.AwardKarma( g, karmaAward, true );
			}

			if ( m is PlayerMobile && ((PlayerMobile)m).NpcGuild == NpcGuild.ThievesGuild )
				return;

			if ( killers.Count > 0 )
				new GumpTimer( m, killers ).Start();
		}

		private class GumpTimer : Timer
		{
			private Mobile m_Victim;
			private List<Mobile> m_Killers;

			public GumpTimer( Mobile victim, List<Mobile> killers ) : base( TimeSpan.FromSeconds( 4.0 ) )
			{
				m_Victim = victim;
				m_Killers = killers;
			}

			protected override void OnTick()
			{
				m_Victim.SendGump( new ReportMurdererGump( m_Victim, m_Killers ) );
			}
		}

		public ReportMurdererGump( Mobile victum, List<Mobile> killers ) : this( victum, killers, 0 )
		{
		}

		private ReportMurdererGump( Mobile victum, List<Mobile> killers, int idx ) : base( 0, 0 )
		{
			m_Killers = killers;
			m_Victum = victum;
			m_Idx = idx;
			BuildGump();
		}

		private void BuildGump()
		{
            AddBackground(265, 205, 320, 290, 9270);
			Closable = false;
			Resizable = false;

			AddPage( 0 );

			AddImageTiled( 225, 175, 50, 45, 0xCE );   //Top left corner
			AddImageTiled( 267, 175, 315, 44, 0xC9 );  //Top bar
			AddImageTiled( 582, 175, 43, 45, 0xCF );   //Top right corner
			AddImageTiled( 225, 219, 44, 270, 0xCA );  //Left side
			AddImageTiled( 582, 219, 44, 270, 0xCB );  //Right side
			AddImageTiled( 225, 489, 44, 43, 0xCC );   //Lower left corner
			AddImageTiled( 267, 489, 315, 43, 0xE9 );  //Lower Bar
			AddImageTiled( 582, 489, 43, 43, 0xCD );   //Lower right corner

			AddPage( 1 );

			AddHtml( 260, 234, 300, 140, ((Mobile)m_Killers[m_Idx]).Name, false, false ); // Player's Name
			AddHtml( 260, 254, 300, 140, "Voce gostaria de denunciar ", false, false ); // Would you like to report...

			AddButton( 260, 300, 0xFA5, 0xFA7, 1, GumpButtonType.Reply, 0 );
            AddHtml(300, 300, 300, 50, "Sim", false, false); // Yes

			AddButton( 360, 300, 0xFA5, 0xFA7, 2, GumpButtonType.Reply, 0 );
            AddHtml(400, 300, 300, 50, "Não", false, false); // No
		}

		public static void ReportedListExpiry_Callback( object state )
		{
			object[] states = (object[])state;

			PlayerMobile from = (PlayerMobile)states[0];
			Mobile killer = (Mobile)states[1];

			if (from.RecentlyReported.Contains(killer))
			{
				from.RecentlyReported.Remove(killer);
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch ( info.ButtonID )
			{
				case 1:
				{
					Mobile killer = m_Killers[m_Idx];
					if ( killer != null && !killer.Deleted )
					{
						killer.Kills++;
						killer.ShortTermMurders++;

						if (Core.SE)
						{
							((PlayerMobile)from).RecentlyReported.Add(killer);
							Timer.DelayCall(TimeSpan.FromMinutes(10), new TimerStateCallback(ReportedListExpiry_Callback), new object[] { from, killer });
						}

						if (killer is PlayerMobile)
						{
							PlayerMobile pk = (PlayerMobile)killer;
							pk.ResetKillTime();
							pk.SendMessage("Voce foi denunciado por este assassinato!");//You have been reported for murder!

							if (pk.Kills == 5)
							{
                                pk.SendMessage("Agora voce é considerado um Assassino!");//You are now known as a murderer!
							}
							else if (SkillHandlers.Stealing.SuspendOnMurder && pk.Kills == 1 && pk.NpcGuild == NpcGuild.ThievesGuild)
							{
                                pk.SendMessage("Voce nao virou assassino foi esta na guilda dos ladroes."); // You have been suspended by the Thieves Guild.
							}
						}
					}
					break;
				}
				case 2:
				{
					break;
				}
			}

			m_Idx++;
			if ( m_Idx < m_Killers.Count )
				from.SendGump( new ReportMurdererGump( from, m_Killers, m_Idx ) );
		}
	}
}