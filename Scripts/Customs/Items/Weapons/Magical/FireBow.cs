using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0x13B2, 0x13B1)]
    public class FireBow : BaseRanged
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


        public override int AosMinDamage { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.FireBow, DamageTypeEnum.DamageType.AosMinDamage, CraftResource.None); } }
        public override int AosMaxDamage { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.FireBow, DamageTypeEnum.DamageType.AosMaxDamage, CraftResource.None); } }
        public override int InitMinHits { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.FireBow, DamageTypeEnum.DamageType.InitMinHits, CraftResource.None); } }
        public override int InitMaxHits { get { return ItemQualityHelper.GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType.FireBow, DamageTypeEnum.DamageType.InitMaxHits, CraftResource.None); } }


        public override WeaponAnimation DefAnimation { get { return WeaponAnimation.ShootBow; } }

        public override int FireResistance { get { return 10; } }

        [Constructable]
        public FireBow()
            : base(0x13B2)
        {
            Weight = 6.0;
            Layer = Layer.TwoHanded;
            Hue = DimensionsNewAge.Scripts.HueItemConst.HueFireBow;
            Name = "Fire Bow";
        }

        public override void OnHit(Mobile attacker, Mobile defender, double damageBonus)
        {
            if (defender.Mana >= 5)
            {
                defender.Mana -= 5;
                defender.SendAsciiMessage(0x44, "You Feel Yourself Decentralized and lost some Mana!");
            }

            base.OnHit(attacker, defender, damageBonus);
        }

        public FireBow(Serial serial)
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