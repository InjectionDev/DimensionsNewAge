using System;
using System.Collections;
using Server;
using Server.Guilds;
using Server.Network;
using System.Collections.Generic;

namespace Server.Gumps
{
	public class GuildWarGump : Gump
	{
		private Mobile m_Mobile;
		private Guild m_Guild;

        public new void AddHtml(int x, int y, int weight, int height, string text, bool background, bool scrollbar)
        {
            base.AddHtml(x, y, weight, height, "<body><BASEFONT COLOR=#E6E8FA>" + text + "</body>", background, scrollbar);
        }

		public GuildWarGump( Mobile from, Guild guild ) : base( 20, 30 )
		{
			m_Mobile = from;
			m_Guild = guild;

			Dragable = false;

			AddPage( 0 );
            AddBackground(0, 0, 550, 440, 9270);
			//AddBackground( 10, 10, 530, 420, 9270 );

            AddHtml(20, 10, 500, 35, "<center>GUILD WAR</center>", false, false); // <center>WARFARE STATUS</center>

			AddButton( 20, 400, 4005, 4007, 1, GumpButtonType.Reply, 0 );
            AddHtml(55, 400, 300, 35, "Retornar", false, false); // Return to the main menu.

			AddPage( 1 );

			AddButton( 375, 375, 5224, 5224, 0, GumpButtonType.Page, 2 );
            AddHtml(410, 373, 100, 25, "Próxima página", false, false); // Next page

            AddHtml(20, 45, 400, 20, "Guilda em Guerra com:", false, false); // We are at war with:

			List<Guild> enemies = guild.Enemies;

			if ( enemies.Count == 0 )
			{
                AddHtml(20, 65, 400, 20, "Sem Guerras no momento.", false, false); // No current wars
			}
			else
			{
				for ( int i = 0; i < enemies.Count; ++i )
				{
					Guild g = enemies[i];

					AddHtml( 20, 65 + (i * 20), 300, 20, g.Name, false, false );
				}
			}

			AddPage( 2 );

			AddButton( 375, 375, 5224, 5224, 0, GumpButtonType.Page, 3 );
            AddHtml(410, 373, 100, 25, "Próxima página", false, false); // Next page

			AddButton( 30, 375, 5223, 5223, 0, GumpButtonType.Page, 1 );
            AddHtml(65, 373, 150, 25, "Página anterior", false, false); // Previous page

            AddHtml(20, 45, 400, 20, "Guildas com Guerra Declarada", false, false); // Guilds that we have declared war on: 

			List<Guild> declared = guild.WarDeclarations;

			if ( declared.Count == 0 )
			{
                AddHtml(20, 65, 400, 20, "Nenhum convite de Guerra.", false, false); // No current invitations received for war.
			}
			else
			{
				for ( int i = 0; i < declared.Count; ++i )
				{
					Guild g = (Guild)declared[i];

					AddHtml( 20, 65 + (i * 20), 300, 20, g.Name, false, false );
				}
			}

			AddPage( 3 );

			AddButton( 30, 375, 5223, 5223, 0, GumpButtonType.Page, 2 );
            AddHtml(65, 373, 150, 25, "Página anterior", false, false); // Previous page

            AddHtml(20, 45, 400, 20, "Guildas com Guerra Declarada", false, false); // Guilds that have declared war on us: 

			List<Guild> invites = guild.WarInvitations;

			if ( invites.Count == 0 )
			{
                AddHtml(20, 65, 400, 20, "Sem Guerras no momento.", false, false); // No current war declarations
			}
			else
			{
				for ( int i = 0; i < invites.Count; ++i )
				{
					Guild g = invites[i];

					AddHtml( 20, 65 + (i * 20), 300, 20, g.Name, false, false );
				}
			}
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