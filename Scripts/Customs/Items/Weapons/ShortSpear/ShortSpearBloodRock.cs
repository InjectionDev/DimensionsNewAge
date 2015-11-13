using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1403, 0x1402 )]
    public class ShortSpearBloodRock : BaseSpear
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ShadowStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosStrengthReq{ get{ return 40; } }
		public override int AosSpeed{ get{ return 55; } }
		public override float MlSpeed{ get{ return 2.00f; } }

		public override int OldStrengthReq{ get{ return 15; } }
		public override int OldSpeed{ get{ return 50; } }

        public override int AosMinDamage { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.ShortSpear, DamageTypeEnum.DamageType.AosMinDamage, CraftResource.BloodRock); } }
        public override int AosMaxDamage { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.ShortSpear, DamageTypeEnum.DamageType.AosMaxDamage, CraftResource.BloodRock); } }
        public override int InitMinHits { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.ShortSpear, DamageTypeEnum.DamageType.InitMinHits, CraftResource.BloodRock); } }
        public override int InitMaxHits { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.ShortSpear, DamageTypeEnum.DamageType.InitMaxHits, CraftResource.BloodRock); } }

        public override SkillName DefSkill { get { return SkillName.Fencing; } } // novo como fenc
        public override WeaponType DefType { get { return WeaponType.Piercing; } } // novo como fenc
        public override WeaponAnimation DefAnimation { get { return WeaponAnimation.Pierce1H; } }

		[Constructable]
        public ShortSpearBloodRock()
            : base(0x1403)
		{
			Weight = 4.0;
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBloodRock;
            Name = "BloodRock Short Spear";
		}

		public ShortSpearBloodRock( Serial serial ) : base( serial )
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