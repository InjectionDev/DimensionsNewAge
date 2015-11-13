using System;
using Server;

namespace Server.Items
{
	public class LavaExplosionPotion : BaseExplosionPotion
	{
		public override int MinDamage { get { return Core.AOS ? 22 : 17; } }
		public override int MaxDamage { get { return Core.AOS ? 42 : 32; } }

		[Constructable]
		public LavaExplosionPotion() : base( PotionEffect.ExplosionGreater )
		{
		}

        public LavaExplosionPotion(Serial serial)
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