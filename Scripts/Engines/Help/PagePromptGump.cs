using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Engines.Help
{
	public class PagePromptGump : Gump
	{
		private Mobile m_From;
		private PageType m_Type;

        public new void AddHtml(int x, int y, int weight, int height, string text, bool background, bool scrollbar)
        {
            base.AddHtml(x, y, weight, height, "<body><BASEFONT COLOR=#E6E8FA>" + text + "</body>", background, scrollbar);
        }

		public PagePromptGump( Mobile from, PageType type ) : base( 0, 0 )
		{
			m_From = from;
			m_Type = type;

			from.CloseGump( typeof( PagePromptGump ) );

			AddBackground( 50, 50, 540, 350, 2600 );

			AddPage( 0 );

            AddHtml(264, 80, 200, 24, "Solicitação de Suporte", false, false); // Enter Description
            AddHtml(120, 108, 420, 48, "Por favor, digite um resumo do seu problema (max 200 caracteres)", false, false); // Please enter a brief description (up to 200 characters) of your problem:

			AddBackground( 100, 148, 440, 200, 3500 );
			AddTextEntry( 120, 168, 400, 200, 1153, 0, "" );

			AddButton( 175, 355, 2074, 2075, 1, GumpButtonType.Reply, 0 ); // Okay
			AddButton( 405, 355, 2073, 2072, 0, GumpButtonType.Reply, 0 ); // Cancel
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( info.ButtonID == 0 )
			{
				m_From.SendLocalizedMessage( 501235, "", 0x35 ); // Help request aborted.
			}
			else
			{
				TextRelay entry = info.GetTextEntry( 0 );
				string text = ( entry == null ? "" : entry.Text.Trim() );

				if ( text.Length == 0 )
				{
					m_From.SendMessage( 0x35, "Voce precisa digitar uma descricao." );
					m_From.SendGump( new PagePromptGump( m_From, m_Type ) );
				}
				else
				{
					m_From.SendMessage( "Assim que possivel um Staff ira lhe atender. Fique atento ao seu Journal." ); /* The next available Counselor/Game Master will respond as soon as possible.
																	  * Please check your Journal for messages every few minutes.
																	  */

					PageQueue.Enqueue( new PageEntry( m_From, text, m_Type ) );
				}
			}
		}
	}
}
