using System;
using Server;

namespace Server.Items
{
	public class GreaterManaPotion : BaseManaPotion
	{
        public override int MinMana { get { return 35; } }
        public override int MaxMana { get { return 45; } } 
		public override double Delay{ get{ return 12.0; } }

		[Constructable]
        public GreaterManaPotion()
            : base(PotionEffect.ManaGreater)
		{
            Name = "Greater Mana Potion";
		}

        public GreaterManaPotion(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}