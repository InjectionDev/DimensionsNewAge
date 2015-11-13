using System;
using System.Collections;
using Server;
using Server.Guilds;
using Server.Network;
using Server.Factions;

namespace Server.Gumps
{
	public class GuildChangeTypeGump : Gump
	{
		private Mobile m_Mobile;
		private Guild m_Guild;

        public new void AddHtml(int x, int y, int weight, int height, string text, bool background, bool scrollbar)
        {
            base.AddHtml(x, y, weight, height, "<body><BASEFONT COLOR=#E6E8FA>" + text + "</body>", background, scrollbar);
        }

		public GuildChangeTypeGump( Mobile from, Guild guild ) : base( 20, 30 )
		{
			m_Mobile = from;
			m_Guild = guild;

			Dragable = true;

			AddPage( 0 );
            AddBackground(0, 0, 550, 400, 9270);
			//AddBackground( 10, 10, 530, 380, 3000 );

            AddHtml(20, 15, 510, 30, "<center>Mudar Alinhamento da Guilda</center>", false, false); // <center>Change Guild Type Menu</center>

            AddHtml(50, 50, 450, 30, "Selecione o Alinhamento da Guilda", false, false); // Please select the type of guild you would like to change to

			AddButton( 20, 100, 4005, 4007, 1, GumpButtonType.Reply, 0 );
            AddHtml(85, 100, 300, 30, "Neutro", false, false); // Standard guild

			AddButton( 20, 150, 4005, 4007, 2, GumpButtonType.Reply, 0 );
			AddItem( 50, 143, 7109 );
            AddHtml(85, 150, 300, 300, "Order Guild", false, false); // Order guild

			AddButton( 20, 200, 4005, 4007, 3, GumpButtonType.Reply, 0 );
			AddItem( 45, 200, 7107 );
            AddHtml(85, 200, 300, 300, "Chaos Guild", false, false); // Chaos guild

			AddButton( 300, 360, 4005, 4007, 4, GumpButtonType.Reply, 0 );
            AddHtml(335, 360, 150, 30, "Cancelar", false, false); // CANCEL
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( GuildGump.BadLeader( m_Mobile, m_Guild ) )
				return;

			PlayerState pl = PlayerState.Find( m_Mobile );

			if ( pl != null )
			{
                m_Mobile.SendMessage("Voce nao pode mudar de Alinhamento enquanto esta em uma Faccao"); // You cannot change guild types while in a Faction!
			}
			else if ( m_Guild.TypeLastChange.AddDays( 7 ) > DateTime.Now )
			{
				m_Mobile.SendMessage( "Seu Alinhamento sera alterado em dentro de uma semana" ); // Your guild type will be changed in one week.
			}
			else
			{
				GuildType newType;

				switch ( info.ButtonID )
				{
					default: return; // Close
					case 1: newType = GuildType.Regular; break;
					case 2: newType = GuildType.Order;   break;
					case 3: newType = GuildType.Chaos;   break;
				}

				if ( m_Guild.Type == newType )
					return;

				m_Guild.Type = newType;
				m_Guild.GuildMessage( 1018022, true, newType.ToString() ); // Guild Message: Your guild type has changed:
			}

			GuildGump.EnsureClosed( m_Mobile );
			m_Mobile.SendGump( new GuildmasterGump( m_Mobile, m_Guild ) );
		}
	}
}
