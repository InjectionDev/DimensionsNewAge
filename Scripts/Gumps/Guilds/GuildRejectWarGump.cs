using System;
using Server;
using Server.Guilds;
using Server.Network;

namespace Server.Gumps
{
	public class GuildRejectWarGump : GuildListGump
	{
		public GuildRejectWarGump( Mobile from, Guild guild ) : base( from, guild, true, guild.WarInvitations )
		{
		}

        public new void AddHtml(int x, int y, int weight, int height, string text, bool background, bool scrollbar)
        {
            base.AddHtml(x, y, weight, height, "<body><BASEFONT COLOR=#E6E8FA>" + text + "</body>", background, scrollbar);
        }

		protected override void Design()
		{
            AddHtml(20, 10, 400, 35, "Selecione a Guilda que deseja recusar os convites de guerra", false, false); // Select the guild to reject their invitations: 

			AddButton( 20, 400, 4005, 4007, 1, GumpButtonType.Reply, 0 );
            AddHtml(55, 400, 245, 30, "Recusar convites de guerra", false, false);  // Reject war invitations.

			AddButton( 300, 400, 4005, 4007, 2, GumpButtonType.Reply, 0 );
            AddHtml(335, 400, 100, 35, "Cancelar", false, false); // CANCEL
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( GuildGump.BadLeader( m_Mobile, m_Guild ) )
				return;

			if ( info.ButtonID == 1 )
			{
				int[] switches = info.Switches;

				if ( switches.Length > 0 )
				{
					int index = switches[0];

					if ( index >= 0 && index < m_List.Count )
					{
						Guild g = (Guild)m_List[index];

						if ( g != null )
						{
							m_Guild.WarInvitations.Remove( g );
							g.WarDeclarations.Remove( m_Guild );

							GuildGump.EnsureClosed( m_Mobile );

							if ( m_Guild.WarInvitations.Count > 0 )
								m_Mobile.SendGump( new GuildRejectWarGump( m_Mobile, m_Guild ) );
							else
								m_Mobile.SendGump( new GuildmasterGump( m_Mobile, m_Guild ) );
						}
					}
				}
			}
			else if ( info.ButtonID == 2 )
			{
				GuildGump.EnsureClosed( m_Mobile );
				m_Mobile.SendGump( new GuildmasterGump( m_Mobile, m_Guild ) );
			}
		}
	}
}