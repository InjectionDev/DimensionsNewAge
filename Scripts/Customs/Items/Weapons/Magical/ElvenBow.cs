using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0x13B2, 0x13B1)]
    public class ElvenBow : BaseRanged
    {
        public override int EffectID { get { return 0xF42; } }
        public override Type AmmoType { get { return typeof(Arrow); } }
        public override Item Ammo { get { return new Arrow(); } }

        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ParalyzingBlow; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.MortalStrike; } }

        public override int AosStrengthReq { get { return 30; } }
        public override int AosSpeed { get { return 25; } }
        public override float MlSpeed { get { return 4.25f; } }

        public override int OldStrengthReq { get { return 20; } }
        public override int OldSpeed { get { return 20; } }

        public override int DefMaxRange { get { return 10; } }

        public override int AosMinDamage { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.ElvenBow, DamageTypeEnum.DamageType.AosMinDamage, CraftResource.None); } }
        public override int AosMaxDamage { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.ElvenBow, DamageTypeEnum.DamageType.AosMaxDamage, CraftResource.None); } }
        public override int InitMinHits { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.ElvenBow, DamageTypeEnum.DamageType.InitMinHits, CraftResource.None); } }
        public override int InitMaxHits { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.ElvenBow, DamageTypeEnum.DamageType.InitMaxHits, CraftResource.None); } }


        public override WeaponAnimation DefAnimation { get { return WeaponAnimation.ShootBow; } }

        [Constructable]
        public ElvenBow()
            : base(0x13B2)
        {
            Weight = 6.0;
            Layer = Layer.TwoHanded;
            Hue = DimensionsNewAge.Scripts.HueItemConst.HueElvenBow;
            Name = "Elven Bow";
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Tactics].Base += 15;
                from.Skills[SkillName.Archery].Base += 10;
                from.SendAsciiMessage(0x44, "You Feel Yourself Faster using Ray Bow!");
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Tactics].Base -= 15;
                from.Skills[SkillName.Archery].Base -= 10;
                from.SendAsciiMessage(0x44, "You Unequip Ray Bow!");
            }

            base.OnRemoved(parent);
        }

        public ElvenBow(Serial serial)
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