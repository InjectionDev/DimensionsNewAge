using System;
using Server;
using Server.Mobiles;
using Server.Multis;

namespace Server.Items
{
	public class ContractOfEmployment : Item
	{

		[Constructable]
		public ContractOfEmployment() : base( 0x14F0 )
		{
			Weight = 1.0;
            Name = "Contrato com Vendedor";
			//LootType = LootType.Blessed;
		}

		public ContractOfEmployment( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendMessage( "O item precisa estar na sua bag" ); // That must be in your pack for you to use it.
			}
			else if ( from.AccessLevel >= AccessLevel.GameMaster )
			{
                from.SendMessage("Voce e GM e pode colocar o vendedor onde quiser."); // Your godly powers allow you to place this vendor whereever you wish.

				Mobile v = new PlayerVendor( from, BaseHouse.FindHouseAt( from ) );

				v.Direction = from.Direction & Direction.Mask;
				v.MoveToWorld( from.Location, from.Map );

				v.SayTo( from, "Ah! Como e bom voltar ao trabalho..." ); // Ah! it feels good to be working again.

				this.Delete();
			}
			else
			{
				BaseHouse house = BaseHouse.FindHouseAt( from );

				if ( house == null )
				{
                    from.SendMessage("Vendedores so podem ser colocados dentro de casa"); // Vendors can only be placed in houses.	
				}
				else if ( !BaseHouse.NewVendorSystem && !house.IsFriend( from ) )
				{
                    from.SendMessage("Apenas o dono, sócios e amigos podem colocar vendedores nesta casa"); // You must ask the owner of this building to name you a friend of the household in order to place a vendor here.
				}
				else if ( BaseHouse.NewVendorSystem && !house.IsOwner( from ) )
				{
                    from.SendMessage("Apenas o dono pode colocar vendedores diretamente."); // Only the house owner can directly place vendors.  Please ask the house owner to offer you a vendor contract so that you may place a vendor in this house.
				}
				else if ( !house.Public || !house.CanPlaceNewVendor() ) 
				{
                    from.SendMessage("Voce nao pode colocar este vendedor aqui. Verifique se a casa e publica e tem espaco suficiente."); // You cannot place this vendor or barkeep.  Make sure the house is public and has sufficient storage available.
				}
				else
				{
					bool vendor, contract;
					BaseHouse.IsThereVendor( from.Location, from.Map, out vendor, out contract );

					if ( vendor )
					{
                        from.SendMessage("Voce nao pode colocar um vendedor aqui"); // You cannot place a vendor or barkeep at this location.
					}
					else if ( contract )
					{
                        from.SendMessage("Voce nao pode colocar este vendedor aqui, verifique o contrato."); // You cannot place a vendor or barkeep on top of a rental contract!
					}
					else
					{
						Mobile v = new PlayerVendor( from, house );

						v.Direction = from.Direction & Direction.Mask;
						v.MoveToWorld( from.Location, from.Map );

						v.SayTo( from, "Ah! Como e bom voltar ao trabalho..."); // Ah! it feels good to be working again.

						this.Delete();
					}
				}
			}
		}
	}
}