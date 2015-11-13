using System; 
using Server; 
using Server.Items;
using System.Collections.Generic;

namespace Server.Items 
{ 
	public class BagOfMagicJewels : Bag 
	{ 
		[Constructable] 
		public BagOfMagicJewels() : this( 1 ) 
		{ 
		} 

		[Constructable] 
		public BagOfMagicJewels( int amount ) 
		{
            this.Name = "Bag Of Jewels";
            this.Hue = DimensionsNewAge.Scripts.HueItemConst.HueMagicColorRandom;

            Bag bagBracelet = new Bag();
            bagBracelet.DropItem(new BraceletAlchemy());
            bagBracelet.DropItem(new BraceletBlacksmithing());
            bagBracelet.DropItem(new BraceletMining());
            bagBracelet.DropItem(new BraceletBowcraft());
            bagBracelet.DropItem(new BraceletTaming());
            bagBracelet.DropItem(new BraceletLumberjacking());
            bagBracelet.DropItem(new BraceletSnooping());
            bagBracelet.DropItem(new BraceletTailoring());
            bagBracelet.DropItem(new BraceletPoisoning());
            bagBracelet.DropItem(new BraceletStealing());
            this.DropItem(bagBracelet);

            Bag bagNecklace = new Bag();
            bagNecklace.DropItem(new NecklaceFencing());
            bagNecklace.DropItem(new NecklaceMacefighting());
            bagNecklace.DropItem(new NecklaceSwordsmanship());
            bagNecklace.DropItem(new NecklaceArchery());
            bagNecklace.DropItem(new NecklaceTactics());
            bagNecklace.DropItem(new NecklaceWrestling());
            this.DropItem(bagNecklace);

            Bag bagRing = new Bag();
            bagRing.DropItem(new RingDexterity());
            bagRing.DropItem(new RingStrenght());
            bagRing.DropItem(new RingInteligence());
            bagRing.DropItem(new RingPower());
            this.DropItem(bagRing);

		}

        public BagOfMagicJewels(Serial serial)
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
