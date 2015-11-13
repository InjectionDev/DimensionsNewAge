using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfOres : Bag 
	{ 
		[Constructable] 
		public BagOfOres() : this( 5000 ) 
		{ 
		} 

		[Constructable] 
		public BagOfOres( int amount ) 
		{
            DropItem(new IronOre(amount));
            //DropItem(new RustyOre(amount));
            //DropItem(new OldCopperOre(amount));
            //DropItem(new DullCopperOre(amount));
            //DropItem(new RubyOre(amount));
            //DropItem(new CopperOre(amount));
            //DropItem(new BronzeOre(amount));
            //DropItem(new ShadowIronOre(amount));
            DropItem(new SilverOre(amount));
            DropItem(new MercuryOre(amount));
            DropItem(new RoseOre(amount));
            DropItem(new GoldOre(amount));
            DropItem(new AgapiteOre(amount));
            DropItem(new VeriteOre(amount));
            DropItem(new PlutoniumOre(amount));
            DropItem(new BloodRockOre(amount));
            //DropItem(new ValoriteOre(amount));
            //DropItem(new BlackRockOre(amount));
            //DropItem(new MytherilOre(amount));
            //DropItem(new AquaOre(amount));
		}

        public BagOfOres(Serial serial)
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
