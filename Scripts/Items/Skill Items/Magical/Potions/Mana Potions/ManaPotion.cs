using System;
using Server;

namespace Server.Items
{
	public class ManaPotion : BaseManaPotion
	{
        public override int MinMana { get { return 30; } }
        public override int MaxMana { get { return 35; } } 
		public override double Delay{ get{ return (Core.AOS ? 9.0 : 11.0); } }

		[Constructable]
        public ManaPotion()
            : base(PotionEffect.Mana)
		{
            Name = "Mana Potion";
		}

        public ManaPotion(Serial serial)
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