using System;
using Server;
using Server.Guilds;
using Server.Network;

namespace Server.Gumps
{
	public class DeclareFealtyGump : GuildMobileListGump
	{
		public DeclareFealtyGump( Mobile from, Guild guild ) : base( from, guild, true, guild.Members )
		{
		}

        public new void AddHtml(int x, int y, int weight, int height, string text, bool background, bool scrollbar)
        {
            base.AddHtml(x, y, weight, height, "<body><BASEFONT COLOR=#E6E8FA>" + text + "</body>", background, scrollbar);
        }

		protected override void Design()
		{
            AddHtml(20, 10, 400, 35, "Declarar Lealdade", false, false); // Declare your fealty

			AddButton( 20, 400, 4005, 4007, 1, GumpButtonType.Reply, 0 );
            AddHtml(55, 400, 250, 35, "Selecionei meu novo comandante", false, false); // I have selected my new lord.

			AddButton( 300, 400, 4005, 4007, 0, GumpButtonType.Reply, 0 );
            AddHtml(335, 400, 100, 35, "Cancelar", false, false); // CANCEL
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( GuildGump.BadMember( m_Mobile, m_Guild ) )
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
							state.Mobile.GuildFealty = m;
						}
					}
				}
			}

			GuildGump.EnsureClosed( m_Mobile );
			m_Mobile.SendGump( new GuildGump( m_Mobile, m_Guild ) );
		}
	}
}
