using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0x1401, 0x1400)]
    public class KryssRusty : BaseSword
    {
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ArmorIgnore; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.InfectiousStrike; } }

        public override int AosStrengthReq { get { return 10; } }
        public override int AosSpeed { get { return 53; } }
        public override float MlSpeed { get { return 2.00f; } }

        public override int OldStrengthReq { get { return 10; } }
        public override int OldSpeed { get { return 53; } }

        public override int DefHitSound { get { return 0x23C; } }
        public override int DefMissSound { get { return 0x238; } }

        public override int AosMinDamage { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.Kryss, DamageTypeEnum.DamageType.AosMinDamage, CraftResource.Rusty); } }
        public override int AosMaxDamage { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.Kryss, DamageTypeEnum.DamageType.AosMaxDamage, CraftResource.Rusty); } }
        public override int InitMinHits { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.Kryss, DamageTypeEnum.DamageType.InitMinHits, CraftResource.Rusty); } }
        public override int InitMaxHits { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.Kryss, DamageTypeEnum.DamageType.InitMaxHits, CraftResource.Rusty); } }


        public override SkillName DefSkill { get { return SkillName.Fencing; } }
        public override WeaponType DefType { get { return WeaponType.Piercing; } }
        public override WeaponAnimation DefAnimation { get { return WeaponAnimation.Pierce1H; } }

        [Constructable]
        public KryssRusty()
            : base(0x1401)
        {
            Weight = 2.0;
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueRusty;
            Name = "Rusty Kryss";
        }

        public KryssRusty(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}