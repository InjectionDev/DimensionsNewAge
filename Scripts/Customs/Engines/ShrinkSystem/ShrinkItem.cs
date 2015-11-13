using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class ShrinkItem : Item
	{
		private Mobile m_BondOwner;

		public Mobile BondOwner
		{
			get{ return m_BondOwner; }
			set{ m_BondOwner = value; }
		}
		private BaseCreature m_link;
		private bool m_toDeletePet;

		public ShrinkItem( Serial serial ) : base( serial )
		{	}

		public ShrinkItem(BaseCreature c)
		{
			m_toDeletePet=true;
			m_link=c;
			if ( !(c.Body==400 || c.Body==401) )
			{			
				Hue=c.Hue;
			}
			Name = c.Name + " [shrunk]";
			ItemID=ShrinkTable.Lookup( c );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			if( from.InRange( this.GetWorldLocation(), 2 ) == false )
			{
				from.SendLocalizedMessage( 500486 );	//That is too far away.
				return;
			}
			else if ( m_link == null || m_link.Deleted )
			{
				from.SendMessage( "O Pet foi perdido para sempre..." );
				return;
				//Basically just an "In case something happened"  Thing so Server don't crash.
			}
			else if( !from.CheckAlive() )
			{
				from.SendMessage( "Voce nao pode fazer isto morto." );	//You cannot do that while dead!
			}
			else if ( from.Followers + m_link.ControlSlots > from.FollowersMax )
			{
				from.SendMessage( "Voce ja tem muitos followers para ficar com esta criatura." );
				return;
			}
			else
			{
				bool alreadyOwned = m_link.Owners.Contains( from );
				if (!alreadyOwned){m_link.Owners.Add( from );}

				m_link.SetControlMaster( from );
				m_toDeletePet=false;

				m_link.Location=from.Location;

				m_link.Map=from.Map;

				m_link.ControlTarget = from;

				m_link.ControlOrder = OrderType.Follow;
				
				if ( m_link.Summoned )
					m_link.SummonMaster = from;

				//m_link.Loyalty=PetLoyalty.WonderfullyHappy;

				if ( ShrinkConfig.RetainSelfBondStatus )
				{
					if ( m_link.IsBonded &&(( ShrinkConfig.RetainSelfBondStatus && from == BondOwner) || ( ShrinkConfig.TransferBondingStatus )))
						m_link.IsBonded = true;
					else
						m_link.IsBonded = false;
				}

				this.Delete();
			}



		}
		public override void OnDelete()
		{
			//Psuedo Code:  IF todeltepet=true Delete  the mobile m_link.  Then delete this item regardless

			try 
			{
				if ( m_toDeletePet )
				{
					if ( m_link != null )
						m_link.Delete();
				}
			} 
			catch
			{
				Console.Write( "Error with Shrunken pet: {0} is being Deleted.  ItemID of:  {1}.", this.Name, this.ItemID );
				//Another "In case Shit hits fan"  thing.
			}

			base.OnDelete();
		}
		#region Serialization
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( m_BondOwner );

			writer.Write( m_link );
			writer.Write( m_toDeletePet );
			
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_BondOwner = reader.ReadMobile();
					goto case 0;
				}
				case 0:
				{
					m_link = (BaseCreature)reader.ReadMobile();
					m_toDeletePet = reader.ReadBool();
					break;
				}

			}
			
			if ( m_link != null )
				m_link.IsStabled = true; 

		}
		#endregion
	}
}