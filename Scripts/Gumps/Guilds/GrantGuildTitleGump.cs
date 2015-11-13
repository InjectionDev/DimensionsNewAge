using System;
using Server;
using Server.Guilds;
using Server.Network;

namespace Server.Gumps
{
	public class GrantGuildTitleGump : GuildMobileListGump
	{
		public GrantGuildTitleGump( Mobile from, Guild guild ) : base( from, guild, true, guild.Members )
		{
		}

        public new void AddHtml(int x, int y, int weight, int height, string text, bool background, bool scrollbar)
        {
            base.AddHtml(x, y, weight, height, "<body><BASEFONT COLOR=#E6E8FA>" + text + "</body>", background, scrollbar);
        }

		protected override void Design()
		{
            AddHtml(20, 10, 400, 35, "Definir um Título para outro membro", false, false); // Grant a title to another member.

			AddButton( 20, 400, 4005, 4007, 1, GumpButtonType.Reply, 0 );
            AddHtml(55, 400, 245, 30, "Digitar Título", false, false); // I dub thee...

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
						Mobile m = (Mobile)m_List[index];

						if ( m != null && !m.Deleted )
						{
							m_Mobile.SendMessage( "Digite o novo Titulo (max 20 caracteres)" ); // New title (20 characters max):
							m_Mobile.Prompt = new GuildTitlePrompt( m_Mobile, m, m_Guild );
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