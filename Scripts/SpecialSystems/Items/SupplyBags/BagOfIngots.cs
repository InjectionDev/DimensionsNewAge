using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfIngots : Bag 
	{ 
		[Constructable] 
		public BagOfIngots() : this( 5000 ) 
		{ 
		} 

		[Constructable] 
		public BagOfIngots( int amount ) 
		{ 
			DropItem( new IronIngot   ( amount ) ); 
			DropItem( new RustyIngot   ( amount ) ); 
			DropItem( new OldCopperIngot   ( amount ) ); 
			DropItem( new DullCopperIngot   ( amount ) ); 
			DropItem( new RubyIngot   ( amount ) ); 
			DropItem( new CopperIngot   ( amount ) ); 
			DropItem( new BronzeIngot   ( amount ) ); 
			DropItem( new ShadowIronIngot   ( amount ) ); 
			DropItem( new SilverIngot   ( amount ) );
            DropItem( new MercuryIngot   ( amount ) );
            DropItem( new RoseIngot   ( amount ) );
            DropItem( new GoldIngot   ( amount ) );
            DropItem( new AgapiteIngot   ( amount ) );
            DropItem( new VeriteIngot   ( amount ) );
            DropItem( new PlutoniumIngot   ( amount ) );
            DropItem( new BloodRockIngot   ( amount ) );
            DropItem( new ValoriteIngot   ( amount ) );
            DropItem( new BlackRockIngot   ( amount ) );
            DropItem( new MytherilIngot   ( amount ) );
            DropItem( new AquaIngot   ( amount ) );

            DropItem( new Tongs() );
			DropItem( new TinkerTools() );
            DropItem( new SmithHammer());
		}

        public BagOfIngots(Serial serial)
            : base(serial) 
		{ 
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
