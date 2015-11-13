using System; 
using Server; 
using Server.Items;
using System.Collections.Generic;

namespace Server.Items 
{ 
	public class BagOfMagicWeapons : Bag 
	{ 
		[Constructable] 
		public BagOfMagicWeapons() : this( 1 ) 
		{ 
		} 

		[Constructable] 
		public BagOfMagicWeapons( int amount ) 
		{
            this.Name = "Bag Of Magic Weapons";
            this.Hue = DimensionsNewAge.Scripts.HueItemConst.HueMagicColorRandom;

            this.DropItem(new AdvancedPoisonBow());
            this.DropItem(new ElvenBow());
            this.DropItem(new FireBow());
            this.DropItem(new PoisonBow());
            this.DropItem(new RayBow());
		}

        public BagOfMagicWeapons(Serial serial)
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
