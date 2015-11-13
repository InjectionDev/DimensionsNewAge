using System;
using Server;
using Server.Items;
using Server.Multis;
using Server.Multis.Deeds;
using Server.Network;
using Server.Mobiles;

namespace Server.Gumps
{
	public class HouseDemolishGump : Gump
	{
		private Mobile m_Mobile;
		private BaseHouse m_House;

        public new void AddHtml(int x, int y, int weight, int height, string text, bool background, bool scrollbar)
        {
            base.AddHtml(x, y, weight, height, "<body><BASEFONT COLOR=#E6E8FA>" + text + "</body>", background, scrollbar);
        }

		public HouseDemolishGump( Mobile mobile, BaseHouse house ) : base( 110, 100 )
		{
			m_Mobile = mobile;
			m_House = house;

			mobile.CloseGump( typeof( HouseDemolishGump ) );

			Closable = false;

			AddPage( 0 );

            AddBackground(0, 0, 420, 280, 9270);

			AddImageTiled( 10, 10, 400, 20, 2624 );
			//AddAlphaRegion( 10, 10, 400, 20 );

            AddHtml(10, 10, 400, 20, "<CENTER>ATENÇÃO</CENTER>", false, false); // <CENTER>WARNING</CENTER>

			AddImageTiled( 10, 40, 400, 200, 2624 );
			//AddAlphaRegion( 10, 40, 400, 200 );

            AddHtml(10, 40, 400, 200, "Você está prestes a demolir sua casa! <br>Qualquer item que esteja dentro dela ficará solto no mundo <br>e qualquer um poderá pegalo.", false, true); /* You are about to demolish your house.
																				* You will be refunded the house's value directly to your bank box.
																				* All items in the house will remain behind and can be freely picked up by anyone.
																				* Once the house is demolished, anyone can attempt to place a new house on the vacant land.
																				* This action will not un-condemn any other houses on your account, nor will it end your 7-day waiting period (if it applies to you).
																				* Are you sure you wish to continue?
																				*/

			AddImageTiled( 10, 250, 400, 20, 2624 );
			//AddAlphaRegion( 10, 250, 400, 20 );

			AddButton( 10, 250, 4005, 4007, 1, GumpButtonType.Reply, 0 );
            AddHtml(40, 250, 170, 20, "Confirmar", false, false); // OKAY

			AddButton( 210, 250, 4005, 4007, 0, GumpButtonType.Reply, 0 );
            AddHtml(240, 250, 170, 20, "Cancelar", false, false); // CANCEL
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( info.ButtonID == 1 && !m_House.Deleted )
			{
				if ( m_House.IsOwner( m_Mobile ) )
				{
					if ( m_House.MovingCrate != null || m_House.InternalizedVendors.Count > 0 )
					{
						return;
					}
					else if( !Guilds.Guild.NewGuildSystem && m_House.FindGuildstone() != null )
					{
						m_Mobile.SendMessage( "Voce nao pode fazer isto com uma GuildStone dentro." ); // You cannot redeed a house with a guildstone inside.
						return;
					}
					/*else if ( m_House.PlayerVendors.Count > 0 )
					{
						m_Mobile.SendLocalizedMessage( 503236 ); // You need to collect your vendor's belongings before moving.
						return;
					}*/
					else if ( m_House.HasRentedVendors && m_House.VendorInventories.Count > 0 )
					{
                        m_Mobile.SendMessage("Voce nao pode fazer isto com vendedores na casa."); // You cannot do that that while you still have contract vendors or unclaimed contract vendor inventory in your house.
						return;
					}
					else if ( m_House.HasRentedVendors )
					{
                        m_Mobile.SendMessage("Voce nao pode fazer isto com vendedores na casa."); // You cannot do that that while you still have contract vendors in your house.
						return;
					}
					else if ( m_House.VendorInventories.Count > 0 )
					{
                        m_Mobile.SendMessage("Voce nao pode fazer isto com vendedores na casa."); // You cannot do that that while you still have unclaimed contract vendor inventory in your house.
						return;
					}


					if ( m_Mobile.AccessLevel >= AccessLevel.GameMaster )
					{
						//m_Mobile.SendMessage( "You do not get a refund for your house as you are not a player" );
						m_House.RemoveKeys(m_Mobile);
						m_House.Delete();
					}
					else
					{
						Item toGive = null;

						if ( m_House.IsAosRules )
						{
							if ( m_House.Price > 0 )
								toGive = new BankCheck( m_House.Price );
							else
								toGive = m_House.GetDeed();
						}
						else
						{
							toGive = m_House.GetDeed();

							if ( toGive == null && m_House.Price > 0 )
								toGive = new BankCheck( m_House.Price );
						}

						if ( toGive != null )
						{
							BankBox box = m_Mobile.BankBox;

							if ( box.TryDropItem( m_Mobile, toGive, false ) )
							{
								if ( toGive is BankCheck )
									m_Mobile.SendMessage( string.Format("Voce recebeu {0} de gold pela transacao. O valor foi depositado em seu banco.", ( (BankCheck)toGive ).Worth.ToString() )); // ~1_AMOUNT~ gold has been deposited into your bank box.

								m_House.RemoveKeys( m_Mobile );
								m_House.Delete();
							}
							else
							{
								toGive.Delete();
                                m_Mobile.SendMessage("Seu banco está cheio!"); // Your bank box is full.
							}
						}
						else
						{
							m_Mobile.SendMessage( "Voce nao pode fazer isto .." );
						}
					}
				}
				else
				{
                    m_Mobile.SendMessage("Apenas o dono da casa pode fazer isto"); // Only the house owner may do this.
				}
			}
		}
	}
}