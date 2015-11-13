using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13FD, 0x13FC )]
	public class HeavyCrossbowSilver : BaseRanged
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


        public override int AosMinDamage { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.HeavyCrossbow, DamageTypeEnum.DamageType.AosMinDamage, CraftResource.Silver); } }
        public override int AosMaxDamage { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.HeavyCrossbow, DamageTypeEnum.DamageType.AosMaxDamage, CraftResource.Silver); } }
        public override int InitMinHits { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.HeavyCrossbow, DamageTypeEnum.DamageType.InitMinHits, CraftResource.Silver); } }
        public override int InitMaxHits { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.HeavyCrossbow, DamageTypeEnum.DamageType.InitMaxHits, CraftResource.Silver); } }
  


		public override int DefMaxRange{ get{ return 8; } }


		[Constructable]
		public HeavyCrossbowSilver() : base( 0x13FD )
		{
			Weight = 9.0;
			Layer = Layer.TwoHanded;
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueSilver;
            Name = "Silver HeavyCrossbow";
		}

        public HeavyCrossbowSilver(Serial serial)
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