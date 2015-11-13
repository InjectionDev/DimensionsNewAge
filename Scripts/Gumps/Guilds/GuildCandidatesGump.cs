using System;
using Server;
using Server.Guilds;
using Server.Network;

namespace Server.Gumps
{
	public class GuildCandidatesGump : GuildMobileListGump
	{
		public GuildCandidatesGump( Mobile from, Guild guild ) : base( from, guild, false, guild.Candidates )
		{
		}

        public new void AddHtml(int x, int y, int weight, int height, string text, bool background, bool scrollbar)
        {
            base.AddHtml(x, y, weight, height, "<body><BASEFONT COLOR=#E6E8FA>" + text + "</body>", background, scrollbar);
        }

		protected override void Design()
		{
            AddHtml(20, 10, 500, 35, "<center> Candidatos para Guilda </center>", false, false); // <center> Candidates </center>

			AddButton( 20, 400, 4005, 4007, 1, GumpButtonType.Reply, 0 );
            AddHtml(55, 400, 300, 35, "Retornar", false, false); // Return to the main menu.
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( GuildGump.BadMember( m_Mobile, m_Guild ) )
				return;

			if ( info.ButtonID == 1 )
			{
				GuildGump.EnsureClosed( m_Mobile );
				m_Mobile.SendGump( new GuildGump( m_Mobile, m_Guild ) );
			}
		}
	}
}