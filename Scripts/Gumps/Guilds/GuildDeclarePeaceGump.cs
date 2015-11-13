using System;
using Server;
using Server.Guilds;
using Server.Network;

namespace Server.Gumps
{
	public class GuildDeclarePeaceGump : GuildListGump
	{
		public GuildDeclarePeaceGump( Mobile from, Guild guild ) : base( from, guild, true, guild.Enemies )
		{
		}

        public new void AddHtml(int x, int y, int weight, int height, string text, bool background, bool scrollbar)
        {
            base.AddHtml(x, y, weight, height, "<body><BASEFONT COLOR=#E6E8FA>" + text + "</body>", background, scrollbar);
        }

		protected override void Design()
		{
            AddHtml(20, 10, 400, 35, "Selecionar Guilda que você deseja acordar Paz", false, false); // Select the guild you wish to declare peace with.

			AddButton( 20, 400, 4005, 4007, 1, GumpButtonType.Reply, 0 );
            AddHtml(55, 400, 245, 30, "Enviar Acordo de Paz", false, false); // Send the olive branch.

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
							m_Guild.RemoveEnemy( g );
							m_Guild.GuildMessage( 1018018, true, "{0} ({1})", g.Name, g.Abbreviation ); // Guild Message: You are now at peace with this guild:

							GuildGump.EnsureClosed( m_Mobile );

							if ( m_Guild.Enemies.Count > 0 )
								m_Mobile.SendGump( new GuildDeclarePeaceGump( m_Mobile, m_Guild ) );
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