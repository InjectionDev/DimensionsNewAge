using System;
using Server;
using Server.Multis;
using Server.Network;
using Server.Guilds;

namespace Server.Gumps
{
	public interface ISecurable
	{
		SecureLevel Level{ get; set; }
	}

	public class SetSecureLevelGump : Gump
	{
		private ISecurable m_Info;

        public new void AddHtml(int x, int y, int weight, int height, string text, bool background, bool scrollbar)
        {
            base.AddHtml(x, y, weight, height, "<body><BASEFONT COLOR=#E6E8FA>" + text + "</body>", background, scrollbar);
        }

		public SetSecureLevelGump( Mobile owner, ISecurable info, BaseHouse house ) : base( 50, 50 )
		{
			m_Info = info;

			AddPage( 0 );

			int offset = ( Guild.NewGuildSystem )? 20 : 0;

            AddBackground(0, 0, 220, 160 + offset, 9270);

            //AddImageTiled( 10, 10, 200, 20, 5124 );
            //AddImageTiled( 10, 40, 200, 20, 5124 );
            //AddImageTiled( 10, 70, 200, 80 + offset, 5124 );

			//AddAlphaRegion( 10, 10, 200, 140 );

            AddHtml(10, 10, 200, 20, "<CENTER>ACESSOS</CENTER>",false, false); // <CENTER>SET ACCESS</CENTER>
            AddHtml(10, 40, 100, 20, "Dono:", false, false); // Owner:

			AddLabel( 110, 40, 1152, owner == null ? "" : owner.Name );

			AddButton( 10, 70, GetFirstID( SecureLevel.Owner ), 4007, 1, GumpButtonType.Reply, 0 );
            AddHtml(45, 70, 150, 20, "Apenas o Dono", false, false); // Owner Only

			AddButton( 10, 90, GetFirstID( SecureLevel.CoOwners ), 4007, 2, GumpButtonType.Reply, 0 );
            AddHtml(45, 90, 150, 20, "Sócios", false, false); // Co-Owners

			AddButton( 10, 110, GetFirstID( SecureLevel.Friends ), 4007, 3, GumpButtonType.Reply, 0 );
            AddHtml(45, 110, 150, 20, "Amigos", false, false); // Friends

			Mobile houseOwner = house.Owner;
			if( Guild.NewGuildSystem && house != null && houseOwner != null && houseOwner.Guild != null && ((Guild)houseOwner.Guild).Leader == houseOwner )	//Only the actual House owner AND guild master can set guild secures
			{
				AddButton( 10, 130, GetFirstID( SecureLevel.Guild ), 4007, 5, GumpButtonType.Reply, 0 );
                AddHtml(45, 130, 150, 20, "Membros da Guilda", false, false); // Guild Members
			}

			AddButton( 10, 130 + offset, GetFirstID( SecureLevel.Anyone ), 4007, 4, GumpButtonType.Reply, 0 );
            AddHtml(45, 130 + offset, 150, 20, "Qualquer pessoa", false, false); // Anyone
		}

		public int GetColor( SecureLevel level )
		{
			return ( m_Info.Level == level ) ? 0x7F18 : 0x7FFF;
		}

		public int GetFirstID( SecureLevel level )
		{
			return ( m_Info.Level == level ) ? 4006 : 4005;
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			SecureLevel level = m_Info.Level;

			switch ( info.ButtonID )
			{
				case 1: level = SecureLevel.Owner; break;
				case 2: level = SecureLevel.CoOwners; break;
				case 3: level = SecureLevel.Friends; break;
				case 4: level = SecureLevel.Anyone; break;
				case 5: level = SecureLevel.Guild; break;
			}

			if ( m_Info.Level == level )
			{
				state.Mobile.SendMessage( "Level de acesso nao foi alterado." ); // Access level unchanged.
			}
			else
			{
                m_Info.Level = level;
                state.Mobile.SendMessage("Novo level de acesso definido."); // New access level set.
			}
		}
	}
}