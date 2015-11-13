using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0x1407, 0x1406)]
    public class WarMaceShadow : BaseBashing
    {
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.CrushingBlow; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.BleedAttack; } }

        public override int AosStrengthReq { get { return 80; } }
        public override int AosSpeed { get { return 26; } }
        public override float MlSpeed { get { return 4.00f; } }

        public override int OldStrengthReq { get { return 30; } }
        public override int OldSpeed { get { return 32; } }


        public override int AosMinDamage { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.WarMace, DamageTypeEnum.DamageType.AosMinDamage, CraftResource.ShadowIron); } }
        public override int AosMaxDamage { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.WarMace, DamageTypeEnum.DamageType.AosMaxDamage, CraftResource.ShadowIron); } }
        public override int InitMinHits { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.WarMace, DamageTypeEnum.DamageType.InitMinHits, CraftResource.ShadowIron); } }
        public override int InitMaxHits { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.WarMace, DamageTypeEnum.DamageType.InitMaxHits, CraftResource.ShadowIron); } }


        [Constructable]
        public WarMaceShadow()
            : base(0x1407)
        {
            Weight = 17.0;
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueShadow;
            Name = "Shadow War Mace";
        }

        public WarMaceShadow(Serial serial)
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