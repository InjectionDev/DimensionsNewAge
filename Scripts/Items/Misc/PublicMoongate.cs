using System;
using System.Collections.Generic;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Spells;

namespace Server.Items
{



    public class PublicMoongateControl
    {

		public static void Initialize()
		{
			CommandSystem.Register( "MoonGen", AccessLevel.Administrator, new CommandEventHandler( MoonGen_OnCommand ) );
		}

		[Usage( "MoonGen" )]
		[Description( "Generates public moongates. Removes all old moongates." )]
		public static void MoonGen_OnCommand( CommandEventArgs e )
		{
			DeleteAll();

			int count = 0;

			count += MoonGen( PMList.Trammel );
			count += MoonGen( PMList.Felucca );
			count += MoonGen( PMList.Ilshenar );
			count += MoonGen( PMList.Malas );
			count += MoonGen( PMList.Tokuno );

			World.Broadcast( 0x35, true, "{0} moongates generated.", count );
		}

		private static void DeleteAll()
		{
			List<Item> list = new List<Item>();

			foreach ( Item item in World.Items.Values )
			{
                if (item is Moongate && ((Moongate)item).GateToStarRoom)
					list.Add( item );
			}

			foreach ( Item item in list )
				item.Delete();

			if ( list.Count > 0 )
				World.Broadcast( 0x35, true, "{0} moongates removed.", list.Count );
		}

		private static int MoonGen( PMList list )
		{
			foreach ( PMEntry entry in list.Entries )
			{
                Moongate moon = new Moongate();
                moon.Location = entry.Location;
                moon.Map = list.Map;
                moon.GateToStarRoom = true;

				if ( entry.Number == 1060642 ) // Umbra
                    moon.Hue = 0x497;
			}

			return list.Entries.Length;
		}
	}

	public class PMEntry
	{
		private Point3D m_Location;
		private int m_Number;
        public string CityName;

		public Point3D Location
		{
			get
			{
				return m_Location;
			}
		}

		public int Number
		{
			get
			{
				return m_Number;
			}
		}

		public PMEntry( Point3D loc, int number, string cityName )
		{
			m_Location = loc;
			m_Number = number;
            CityName = cityName;
		}

        
	}

	public class PMList
	{
		private int m_Number, m_SelNumber;
		private Map m_Map;
		private PMEntry[] m_Entries;

		public int Number
		{
			get
			{
				return m_Number;
			}
		}

		public int SelNumber
		{
			get
			{
				return m_SelNumber;
			}
		}

		public Map Map
		{
			get
			{
				return m_Map;
			}
		}

		public PMEntry[] Entries
		{
			get
			{
				return m_Entries;
			}
		}

		public PMList( int number, int selNumber, Map map, PMEntry[] entries )
		{
			m_Number = number;
			m_SelNumber = selNumber;
			m_Map = map;
			m_Entries = entries;
		}

		public static readonly PMList Trammel =
			new PMList( 1012000, 1012012, Map.Trammel, new PMEntry[]
				{
                    //new PMEntry( new Point3D( 4467, 1283, 5 ), 1012003, "Moonglow" ), // Moonglow
                    //new PMEntry( new Point3D( 1336, 1997, 5 ), 1012004, "Britain" ), // Britain
                    //new PMEntry( new Point3D( 1499, 3771, 5 ), 1012005, "Jhelom" ), // Jhelom
                    //new PMEntry( new Point3D(  771,  752, 5 ), 1012006, "Yew" ), // Yew
                    //new PMEntry( new Point3D( 2701,  692, 5 ), 1012007, "Minoc" ), // Minoc
                    //new PMEntry( new Point3D( 1828, 2948,-20), 1012008, "Trinsic" ), // Trinsic
                    //new PMEntry( new Point3D(  643, 2067, 5 ), 1012009, "Skara Brae" ), // Skara Brae
                    //new PMEntry( new Point3D( 3563, 2139, 34), 1012010, "Magincia" ) // Magincia
					//new PMEntry( new Point3D( 3450, 2677, 25), 1078098, "New Haven" )  // New Haven
				} );

		public static readonly PMList Felucca =
			new PMList( 1012001, 1012013, Map.Felucca, new PMEntry[]
				{
                    new PMEntry( new Point3D( 4467, 1283, 5 ), 1012003, "Moonglow" ), 
                    new PMEntry( new Point3D( 1336, 1997, 5 ), 1012004, "Britain" ),
                    new PMEntry( new Point3D( 1499, 3771, 5 ), 1012005, "Jhelom" ), 
                    new PMEntry( new Point3D(  771,  752, 5 ), 1012006, "Yew"  ), 
                    new PMEntry( new Point3D( 2701,  692, 5 ), 1012007, "Minoc"  ), 
                    new PMEntry( new Point3D( 1828, 2948,-20), 1012008, "Trinsic" ), 
                    new PMEntry( new Point3D(  643, 2067, 5 ), 1012009, "Skara Brae"  ),
                    new PMEntry( new Point3D( 3563, 2139, 34), 1012010, "Magincia" ), 
                    new PMEntry( new Point3D( 2711, 2234, 0 ), 1019001, "Buccaneers Den" ), 

                    new PMEntry( new Point3D( 181, 150, 5 ), 1012003, "Excalabria" ),
                    new PMEntry( new Point3D( 3987,410, 5 ), 1012003, "Asgard" ),
                    new PMEntry( new Point3D( 4674,567, 5 ), 1012003, "Spital" ),
                    new PMEntry( new Point3D( 3104,1003, 5 ), 1012003, "Horgau" ),
                    new PMEntry( new Point3D( 4521,1940, 5 ), 1012003, "Worclaw" ),
                    new PMEntry( new Point3D( 4885,3031, 5 ), 1012003, "Meer" ),
                    new PMEntry( new Point3D( 3830,3249, 5 ), 1012003, "Tenebrae" ),
                    new PMEntry( new Point3D( 3629,4022, 5 ), 1012003, "Tsarkon" ),
                    new PMEntry( new Point3D( 816,3611, 5 ), 1012003, "Spandau" ),
                    new PMEntry( new Point3D( 391,2603, 5 ), 1012003, "Stow" ),
                    new PMEntry( new Point3D( 3013,828, 2 ), 1012003, "Vesper" ),
                    new PMEntry( new Point3D( 3803,1279, 10 ), 1012003, "Mujelm" ),
                    new PMEntry( new Point3D( 2218,1116, 24 ), 1012003, "Cove" ),
                    new PMEntry( new Point3D( 3650,2653, 5 ), 1012003, "Occlo" ),
                    new PMEntry( new Point3D( 3220,3109, 5 ), 1012003, "Danube" ),
                    new PMEntry( new Point3D( 2791,3563, 5 ), 1012003, "SerpentsHold" )

				} );

		public static readonly PMList Ilshenar =
			new PMList( 1012002, 1012014, Map.Ilshenar, new PMEntry[]
				{
					new PMEntry( new Point3D( 1215,  467, -13 ), 1012015, "Compassion" ), // Compassion
					new PMEntry( new Point3D(  722, 1366, -60 ), 1012016, "Honesty" ), // Honesty
					new PMEntry( new Point3D(  744,  724, -28 ), 1012017, "Honor" ), // Honor
					new PMEntry( new Point3D(  281, 1016,   0 ), 1012018, "Humility" ), // Humility
					new PMEntry( new Point3D(  987, 1011, -32 ), 1012019, "Justice" ), // Justice
					new PMEntry( new Point3D( 1174, 1286, -30 ), 1012020, "Sacrifice" ), // Sacrifice
					new PMEntry( new Point3D( 1532, 1340, - 3 ), 1012021, "Spirituality" ), // Spirituality
					new PMEntry( new Point3D(  528,  216, -45 ), 1012022, "Valor" ), // Valor
					new PMEntry( new Point3D( 1721,  218,  96 ), 1019000, "Chaos" )  // Chaos
				} );

		public static readonly PMList Malas =
			new PMList( 1060643, 1062039, Map.Malas, new PMEntry[]
				{
					new PMEntry( new Point3D( 1015,  527, -65 ), 1060641, "Luna" ), // Luna
					new PMEntry( new Point3D( 1997, 1386, -85 ), 1060642, "Umbra" )  // Umbra
				} );

		public static readonly PMList Tokuno =
			new PMList( 1063258, 1063415, Map.Tokuno, new PMEntry[]
				{
                    //new PMEntry( new Point3D( 1169,  998, 41 ), 1063412 ), // Isamu-Jima
                    //new PMEntry( new Point3D(  802, 1204, 25 ), 1063413 ), // Makoto-Jima
                    //new PMEntry( new Point3D(  270,  628, 15 ), 1063414 )  // Homare-Jima
				} );

		public static readonly PMList[] UORLists		= new PMList[] { Felucca };
		public static readonly PMList[] UORListsYoung	= new PMList[] { Felucca };
		public static readonly PMList[] LBRLists		= new PMList[] { Felucca, Ilshenar };
		public static readonly PMList[] LBRListsYoung	= new PMList[] { Felucca, Ilshenar };
		public static readonly PMList[] AOSLists		= new PMList[] { Felucca, Ilshenar, Malas };
		public static readonly PMList[] AOSListsYoung	= new PMList[] { Felucca, Ilshenar, Malas };
		public static readonly PMList[] SELists			= new PMList[] { Felucca, Ilshenar, Malas, Tokuno };
		public static readonly PMList[] SEListsYoung	= new PMList[] { Felucca, Ilshenar, Malas, Tokuno };
		public static readonly PMList[] RedLists		= new PMList[] { Felucca };
		public static readonly PMList[] SigilLists		= new PMList[] { Felucca };
	}



    //public class MoongateGump : Gump
    //{
    //    private Mobile m_Mobile;
    //    private Item m_Moongate;
    //    private PMList[] m_Lists;

    //    public new void AddHtml(int x, int y, int weight, int height, string text, bool background, bool scrollbar)
    //    {
    //        base.AddHtml(x, y, weight, height, "<body><BASEFONT COLOR=#E6E8FA>" + text + "</body>", background, scrollbar);
    //    }

    //    public MoongateGump( Mobile mobile, Item moongate ) : base( 100, 100 )
    //    {
    //        m_Mobile = mobile;
    //        m_Moongate = moongate;

    //        PMList[] checkLists;

    //        if ( mobile.Player )
    //        {
    //            if ( Factions.Sigil.ExistsOn( mobile ) )
    //            {
    //                checkLists = PMList.SigilLists;
    //            }
    //            else if ( mobile.Kills >= 5 )
    //            {
    //                checkLists = PMList.RedLists;
    //            }
    //            else
    //            {
    //                ClientFlags flags = mobile.NetState == null ? ClientFlags.None : mobile.NetState.Flags;
    //                bool young = mobile is PlayerMobile ? ((PlayerMobile)mobile).Young : false;

    //                if ( Core.SE && (flags & ClientFlags.Tokuno) != 0 )
    //                    checkLists = young ? PMList.SEListsYoung : PMList.SELists;
    //                else if ( Core.AOS && (flags & ClientFlags.Malas) != 0 )
    //                    checkLists = young ? PMList.AOSListsYoung : PMList.AOSLists;
    //                else if ( (flags & ClientFlags.Ilshenar) != 0 )
    //                    checkLists = young ? PMList.LBRListsYoung : PMList.LBRLists;
    //                else
    //                    checkLists = young ? PMList.UORListsYoung : PMList.UORLists;
    //            }
    //        }
    //        else
    //        {
    //            checkLists = PMList.SELists;
    //        }

    //        m_Lists = new PMList[checkLists.Length];

    //        for ( int i = 0; i < m_Lists.Length; ++i )
    //            m_Lists[i] = checkLists[i];

    //        for ( int i = 0; i < m_Lists.Length; ++i )
    //        {
    //            if ( m_Lists[i].Map == mobile.Map )
    //            {
    //                PMList temp = m_Lists[i];

    //                m_Lists[i] = m_Lists[0];
    //                m_Lists[0] = temp;

    //                break;
    //            }
    //        }

    //        AddPage( 0 );

    //        AddBackground(0, 0, 380, 280, 9270);

    //        AddButton( 10, 210, 4005, 4007, 1, GumpButtonType.Reply, 0 );
    //        AddHtml( 45, 210, 140, 25, "Confirmar", false, false ); // OKAY

    //        AddButton( 10, 235, 4005, 4007, 0, GumpButtonType.Reply, 0 );
    //        AddHtml( 45, 235, 140, 25, "Cancelar", false, false ); // CANCEL

    //        AddHtml( 10, 6, 200, 20, "Escolha seu destino", false, false ); // Pick your destination:

    //        AddButton(10, 130, 2117, 2118, 100, GumpButtonType.Reply, 0);
    //        AddHtml(30, 130, 140, 25, "Star Room", false, false); // Star Room

    //        for ( int i = 0; i < checkLists.Length; ++i )
    //        {
    //            AddButton( 10, 35 + (i * 25), 2117, 2118, 0, GumpButtonType.Page, Array.IndexOf( m_Lists, checkLists[i] ) + 1 );
    //            AddHtml( 30, 35 + (i * 25), 150, 20, checkLists[i].Map.Name, false, false );
    //        }

    //        for ( int i = 0; i < m_Lists.Length; ++i )
    //            RenderPage( i, Array.IndexOf( checkLists, m_Lists[i] ) );
    //    }

    //    private void RenderPage( int index, int offset )
    //    {
    //        PMList list = m_Lists[index];

    //        AddPage( index + 1 );

    //        AddButton( 10, 35 + (offset * 25), 2117, 2118, 0, GumpButtonType.Page, index + 1 );
    //        AddHtmlLocalized( 30, 35 + (offset * 25), 150, 20, list.SelNumber, false, false );

    //        PMEntry[] entries = list.Entries;

    //        for ( int i = 0; i < entries.Length; ++i )
    //        {
    //            AddRadio( 200, 35 + (i * 25), 210, 211, false, (index * 100) + i );
    //            AddHtml( 225, 35 + (i * 25), 150, 20, entries[i].CityName, false, false );
    //        }
    //    }

    //    public override void OnResponse( NetState state, RelayInfo info )
    //    {
    //        if ( info.ButtonID == 0 ) // Cancel
    //            return;
    //        else if ( m_Mobile.Deleted || m_Moongate.Deleted || m_Mobile.Map == null )
    //            return;
    //        else if (info.ButtonID == 100)
    //        {
    //            if (!m_Mobile.InRange(m_Moongate.GetWorldLocation(), 1) || m_Mobile.Map != m_Moongate.Map)
    //            {
    //                m_Mobile.SendMessage("Voce esta muito longe do Moongate"); // You are too far away to use the gate.
    //            }
    //            else if (m_Mobile.Criminal)
    //            {
    //                m_Mobile.SendMessage("Voce esta Criminal, nao pode escapar"); // Thou'rt a criminal and cannot escape so easily.
    //            }
    //            else if (SpellHelper.CheckCombat(m_Mobile))
    //            {
    //                m_Mobile.SendMessage("Voce esta em Batalha, nao pode escapar"); // Wouldst thou flee during the heat of battle??
    //            }
    //            else if (m_Mobile.Spell != null)
    //            {
    //                m_Mobile.SendMessage("Voce esta ocupado para fazer isto"); // You are too busy to do that at the moment.
    //            }
    //            else
    //            {
    //                Point3D starRoomPoint3D = new Point3D(5140, 1761, 5);
    //                BaseCreature.TeleportPets(m_Mobile, starRoomPoint3D, Map.Trammel);

    //                m_Mobile.Combatant = null;
    //                m_Mobile.Warmode = false;
    //                m_Mobile.Hidden = true;

    //                m_Mobile.MoveToWorld(starRoomPoint3D, Map.Trammel);

    //                Effects.PlaySound(starRoomPoint3D, Map.Trammel, 0x1FE);
    //            }

    //            return;
    //        }

    //        int[] switches = info.Switches;

    //        if ( switches.Length == 0 )
    //            return;

    //        int switchID = switches[0];
    //        int listIndex = switchID / 100;
    //        int listEntry = switchID % 100;

    //        if ( listIndex < 0 || listIndex >= m_Lists.Length )
    //            return;

    //        PMList list = m_Lists[listIndex];

    //        if ( listEntry < 0 || listEntry >= list.Entries.Length )
    //            return;

    //        PMEntry entry = list.Entries[listEntry];

    //        if ( !m_Mobile.InRange( m_Moongate.GetWorldLocation(), 1 ) || m_Mobile.Map != m_Moongate.Map )
    //        {
    //            m_Mobile.SendMessage( "Voce esta muito longe do Moongate" ); // You are too far away to use the gate.
    //        }
    //        else if ( m_Mobile.Player && m_Mobile.Kills >= 5 && list.Map != Map.Felucca )
    //        {
    //            m_Mobile.SendMessage("Voce nao pode ir para este local"); // You are not allowed to travel there.
    //        }
    //        else if ( Factions.Sigil.ExistsOn( m_Mobile ) && list.Map != Factions.Faction.Facet )
    //        {
    //            m_Mobile.SendMessage("Voce nao pode ir para este local"); // You are not allowed to travel there.
    //        }
    //        else if ( m_Mobile.Criminal )
    //        {
    //            m_Mobile.SendMessage("Voce esta Criminal, nao pode escapar"); // Thou'rt a criminal and cannot escape so easily.
    //        }
    //        else if ( SpellHelper.CheckCombat( m_Mobile ) )
    //        {
    //            m_Mobile.SendMessage("Voce esta em Batalha, nao pode escapar"); // Wouldst thou flee during the heat of battle??
    //        }
    //        else if ( m_Mobile.Spell != null )
    //        {
    //            m_Mobile.SendMessage("Voce esta ocupado para fazer isto"); // You are too busy to do that at the moment.
    //        }
    //        else if ( m_Mobile.Map == list.Map && m_Mobile.InRange( entry.Location, 1 ) )
    //        {
    //            m_Mobile.SendMessage("Voce ja esta no local"); // You are already there.
    //        }
    //        else
    //        {
    //            if (info.ButtonID == 100)
    //            {
    //                BaseCreature.TeleportPets(m_Mobile, entry.Location, list.Map);

    //                m_Mobile.Combatant = null;
    //                m_Mobile.Warmode = false;
    //                //m_Mobile.Hidden = true;

    //                m_Mobile.MoveToWorld(new Point3D(5140, 1761, 5), Map.Trammel);

    //                Effects.PlaySound(entry.Location, list.Map, 0x1FE);
    //            }
    //            else
    //            {
    //                BaseCreature.TeleportPets(m_Mobile, entry.Location, list.Map);

    //                m_Mobile.Combatant = null;
    //                m_Mobile.Warmode = false;
    //                //m_Mobile.Hidden = true;

    //                m_Mobile.MoveToWorld(entry.Location, list.Map);

    //                Effects.PlaySound(entry.Location, list.Map, 0x1FE);
    //            }
    //        }
    //    }
    //}
}