using System;
using Server;

namespace Server.Items
{
	public class LesserManaPotion : BaseManaPotion
	{
        public override int MinMana { get { return 20; } }
        public override int MaxMana { get { return 25; } } 
		public override double Delay{ get{ return (Core.AOS ? 4.0 : 12.0); } }

		[Constructable]
        public LesserManaPotion()
            : base(PotionEffect.ManaLesser)
		{
            Name = "Lesser Mana Potion";
		}

        public LesserManaPotion(Serial serial)
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