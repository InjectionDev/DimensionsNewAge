using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfGranites : Bag 
	{ 
		[Constructable] 
		public BagOfGranites() : this( 5000 ) 
		{ 
		} 

		[Constructable] 
		public BagOfGranites( int amount ) 
		{
            DropItem(new Granite(amount));
            DropItem(new RustyGranite(amount));
            DropItem(new OldCopperGranite(amount));
            DropItem(new DullCopperGranite(amount));
            DropItem(new RubyGranite(amount));
            DropItem(new CopperGranite(amount));
            DropItem(new BronzeGranite(amount));
            DropItem(new ShadowIronGranite(amount));
            DropItem(new SilverGranite(amount));
            DropItem(new MercuryGranite(amount));
            DropItem(new RoseGranite(amount));
            DropItem(new GoldGranite(amount));
            DropItem(new AgapiteGranite(amount));
            DropItem(new VeriteGranite(amount));
            DropItem(new PlutonioGranite(amount));
            DropItem(new BloodRockGranite(amount));
            DropItem(new ValoriteGranite(amount));
            DropItem(new BlackRockGranite(amount));
            DropItem(new MytherilGranite(amount));
            DropItem(new AquaGranite(amount));
		}

        public BagOfGranites(Serial serial)
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
