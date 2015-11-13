using System;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	public class ShrinkPotion : Item
	{

		public ShrinkPotion( Serial serial ) : base( serial )
		{
		}

		[Constructable]
		public ShrinkPotion() : base( 0xF04 )
		{
			Name="Shrink Potion";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;
			else if( from.InRange( this.GetWorldLocation(), 2 ) == false )
			{
				from.SendLocalizedMessage( 500486 );	//That is too far away.
				return;
			}

			Container pack = from.Backpack;

			if ( !(Parent == from || ( pack != null && Parent == pack )) ) //If not in pack.
			{
				from.SendLocalizedMessage( 1042001 );	//That must be in your pack to use it.
				return;
			}
			from.Target = new ShrinkPotionTarget( this );
			from.SendMessage( "Selecione o animal que deseja estabular." );
		}


		private class ShrinkPotionTarget : Target
		{
			private ShrinkPotion m_Potion;

			public ShrinkPotionTarget( Item i ) : base( 3, false, TargetFlags.None )
			{
				m_Potion=(ShrinkPotion)i;
			}
			
			protected override void OnTarget( Mobile from, object targ )
			{
				if ( !(m_Potion.Deleted) )
				{
					if ( ShrinkFunctions.Shrink( from, targ ) )
					{
						m_Potion.Delete();
					}
				}

				return;
			}
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		} 
	}
}
