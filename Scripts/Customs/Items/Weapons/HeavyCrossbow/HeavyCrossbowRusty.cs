using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13FD, 0x13FC )]
	public class HeavyCrossbowRusty : BaseRanged
	{
		public override int EffectID{ get{ return 0x1BFE; } }
		public override Type AmmoType{ get{ return typeof( Bolt ); } }
		public override Item Ammo{ get{ return new Bolt(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.MovingShot; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Dismount; } }

		public override int AosStrengthReq{ get{ return 80; } }
		public override int AosSpeed{ get{ return 22; } }
		public override float MlSpeed{ get{ return 5.00f; } }

		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldSpeed{ get{ return 10; } }


        public override int AosMinDamage { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.HeavyCrossbow, DamageTypeEnum.DamageType.AosMinDamage, CraftResource.Rusty); } }
        public override int AosMaxDamage { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.HeavyCrossbow, DamageTypeEnum.DamageType.AosMaxDamage, CraftResource.Rusty); } }
        public override int InitMinHits { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.HeavyCrossbow, DamageTypeEnum.DamageType.InitMinHits, CraftResource.Rusty); } }
        public override int InitMaxHits { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.HeavyCrossbow, DamageTypeEnum.DamageType.InitMaxHits, CraftResource.Rusty); } }
  


		public override int DefMaxRange{ get{ return 8; } }


		[Constructable]
		public HeavyCrossbowRusty() : base( 0x13FD )
		{
			Weight = 9.0;
			Layer = Layer.TwoHanded;
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueRusty;
            Name = "Rusty HeavyCrossbow";
		}

        public HeavyCrossbowRusty(Serial serial)
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