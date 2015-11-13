using System;
using Server;
using Server.Targeting;

namespace Server.Items
{
	[Flipable( 0x14E8, 0x14E7 )]
	public class HitchingPost : AddonComponent
	{
		#region Constructors
		[Constructable]
		public HitchingPost() : this( 0x14E7 )
		{
		}

		[Constructable]
		public HitchingPost( int itemID ) : base( itemID )
		{
		}
		
		public HitchingPost( Serial serial ) : base( serial )
		{
		}
		#endregion

		public override void OnDoubleClick( Mobile from )
		{
			if( from.InRange( this.GetWorldLocation(), 2 ) == false )
			{
				from.SendLocalizedMessage( 500486 );	//That is too far away.
			}
			else
			{
				from.Target=new HitchingPostTarget( this );
				from.SendMessage( "What do you wish to shrink?" );
			}
		}

		private class HitchingPostTarget : Target
		{
			private HitchingPost m_Post;

			public HitchingPostTarget( Item i ) : base( 3, false, TargetFlags.None )
			{
				m_Post=(HitchingPost)i;
			}
			
			protected override void OnTarget( Mobile from, object targ )
			{
				if ( !(m_Post.Deleted) )
				{
					ShrinkFunctions.Shrink( from, targ );
				}

				return;
			}
		}
        

		#region Serialization
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
		#endregion
	}


	public class HitchingPostEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new HitchingPostEastDeed(); } }

		[Constructable]
		public HitchingPostEastAddon()
		{
			AddComponent( new HitchingPost( 0x14E7 ), 0, 0, 0);
		}

		public HitchingPostEastAddon( Serial serial ) : base( serial )
		{
		}

		#region Serialization
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
		#endregion
	}

	public class HitchingPostEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new HitchingPostEastAddon(); } }

		[Constructable]
		public HitchingPostEastDeed()
		{
			Name="Hitching Post (east)";
		}

		public HitchingPostEastDeed( Serial serial ) : base( serial )
		{
		}

		#region Serialization
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
		#endregion
	}

	
	public class HitchingPostSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new HitchingPostSouthDeed(); } }

		[Constructable]
		public HitchingPostSouthAddon()
		{
			AddComponent( new HitchingPost( 0x14E8 ), 0, 0, 0);
		}

		public HitchingPostSouthAddon( Serial serial ) : base( serial )
		{
		}

		#region Serialization
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
		#endregion
	}

	public class HitchingPostSouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new HitchingPostSouthAddon(); } }

		[Constructable]
		public HitchingPostSouthDeed()
		{
			Name="Hitching Post (south)";
		}

		public HitchingPostSouthDeed( Serial serial ) : base( serial )
		{
		}

		#region Serialization
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
		#endregion
	}

}
