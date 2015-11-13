using System;
using System.Collections;
using Server;
using Server.Guilds;
using Server.Network;
using System.Collections.Generic;

namespace Server.Gumps
{
	public class GuildWarAdminGump : Gump
	{
		private Mobile m_Mobile;
		private Guild m_Guild;

        public new void AddHtml(int x, int y, int weight, int height, string text, bool background, bool scrollbar)
        {
            base.AddHtml(x, y, weight, height, "<body><BASEFONT COLOR=#E6E8FA>" + text + "</body>", background, scrollbar);
        }

		public GuildWarAdminGump( Mobile from, Guild guild ) : base( 20, 30 )
		{
			m_Mobile = from;
			m_Guild = guild;

			Dragable = false;

			AddPage( 0 );
            AddBackground(0, 0, 550, 440, 9270);
			//AddBackground( 10, 10, 530, 420, 9270 );

            AddHtml(20, 10, 510, 35, "<center>GUILD WAR</center>", false, false); // <center>WAR FUNCTIONS</center>

			AddButton( 20, 40, 4005, 4007, 1, GumpButtonType.Reply, 0 );
            AddHtml(55, 40, 400, 30, "Pesquisar Guilda para Declarar Guerra", false, false); // Declare war through guild name search.

			int count = 0;

			if ( guild.Enemies.Count > 0 )
			{
				AddButton( 20, 160 + (count * 30), 4005, 4007, 2, GumpButtonType.Reply, 0 );
                AddHtml(55, 160 + (count++ * 30), 400, 30, "Declarar Paz", false, false); // Declare peace.
			}
			else
			{
                AddHtml(20, 160 + (count++ * 30), 400, 30, "Sem Guerras no momento", false, false); // No current wars
			}

			if ( guild.WarInvitations.Count > 0 )
			{
				AddButton( 20, 160 + (count * 30), 4005, 4007, 3, GumpButtonType.Reply, 0 );
                AddHtml(55, 160 + (count++ * 30), 400, 30, "Aceitar solicitações de Guerra", false, false); // Accept war invitations.

				AddButton( 20, 160 + (count * 30), 4005, 4007, 4, GumpButtonType.Reply, 0 );
                AddHtml(55, 160 + (count++ * 30), 400, 30, "Recusar solicitações de Guerra", false, false); // Reject war invitations.
			}
			else
			{
                AddHtml(20, 160 + (count++ * 30), 400, 30, "Sem convites de Guerra", false, false); // No current invitations received for war.
			}

			if ( guild.WarDeclarations.Count > 0 )
			{
				AddButton( 20, 160 + (count * 30), 4005, 4007, 5, GumpButtonType.Reply, 0 );
                AddHtml(55, 160 + (count++ * 30), 400, 30, "Aceitar Guerra", false, false); // Rescind your war declarations.
			}
			else
			{
                AddHtml(20, 160 + (count++ * 30), 400, 30, "Sem Guerras Declaradas", false, false); // No current war declarations
			}

			AddButton( 20, 400, 4005, 4007, 6, GumpButtonType.Reply, 0 );
            AddHtml(55, 400, 400, 35, "Retornar", false, false); // Return to the previous menu.
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( GuildGump.BadLeader( m_Mobile, m_Guild ) )
				return;

			switch ( info.ButtonID )
			{
				case 1: // Declare war
				{
                    //m_Mobile.SendMessage("Pesquisar Nome da Guilda para Declarar Guerra"); // Declare war through search - Enter Guild Name:  
                    //m_Mobile.Prompt = new GuildDeclareWarPrompt( m_Mobile, m_Guild );

                    
                    List<Guild> guilds = Utility.CastConvertList<BaseGuild, Guild>(Guild.Search(""));

                    GuildGump.EnsureClosed(m_Mobile);

                    if (guilds.Count > 0)
                    {
                        m_Mobile.SendGump(new GuildDeclareWarGump(m_Mobile, m_Guild, guilds));
                    }
                    else
                    {
                        m_Mobile.SendGump(new GuildWarAdminGump(m_Mobile, m_Guild));
                        m_Mobile.SendLocalizedMessage(1018003); // No guilds found matching - try another name in the search
                    }

					break;
				}
				case 2: // Declare peace
				{
					GuildGump.EnsureClosed( m_Mobile );
					m_Mobile.SendGump( new GuildDeclarePeaceGump( m_Mobile, m_Guild ) );

					break;
				}
				case 3: // Accept war
				{
					GuildGump.EnsureClosed( m_Mobile );
					m_Mobile.SendGump( new GuildAcceptWarGump( m_Mobile, m_Guild ) );

					break;
				}
				case 4: // Reject war
				{
					GuildGump.EnsureClosed( m_Mobile );
					m_Mobile.SendGump( new GuildRejectWarGump( m_Mobile, m_Guild ) );

					break;
				}
				case 5: // Rescind declarations
				{
					GuildGump.EnsureClosed( m_Mobile );
					m_Mobile.SendGump( new GuildRescindDeclarationGump( m_Mobile, m_Guild ) );

					break;
				}
				case 6: // Return
				{
					GuildGump.EnsureClosed( m_Mobile );
					m_Mobile.SendGump( new GuildmasterGump( m_Mobile, m_Guild ) );

					break;
				}
			}
		}
	}
}