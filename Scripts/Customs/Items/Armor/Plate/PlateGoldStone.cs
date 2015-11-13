using System;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0x1415, 0x1416)]
    public class PlateChestGoldStone : BaseArmor
    {

        public override int AosStrReq { get { return 90; } }
        public override int OldStrReq { get { return 60; } }

        public override int AosDexBonus { get { return -8; } }

        public override int PhysicalResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.PhysicalResistance, DamageTypeEnum.ArmorType.PlateChest, CraftResource.GoldStone); } }
        public override int BaseColdResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseColdResistance, DamageTypeEnum.ArmorType.PlateChest, CraftResource.GoldStone); } }
        public override int BaseEnergyResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseEnergyResistance, DamageTypeEnum.ArmorType.PlateChest, CraftResource.GoldStone); } }
        public override int BaseFireResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseFireResistance, DamageTypeEnum.ArmorType.PlateChest, CraftResource.GoldStone); } }
        public override int BasePoisonResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BasePoisonResistance, DamageTypeEnum.ArmorType.PlateChest, CraftResource.GoldStone); } }
        public override int ArmorBase { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.ArmorBase, DamageTypeEnum.ArmorType.PlateChest, CraftResource.GoldStone); } }
        public override int InitMinHits { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.InitMinHits, DamageTypeEnum.ArmorType.PlateChest, CraftResource.GoldStone); } }
        public override int InitMaxHits { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.InitMaxHits, DamageTypeEnum.ArmorType.PlateChest, CraftResource.GoldStone); } }



        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

        [Constructable]
        public PlateChestGoldStone()
            : base(0x1415)
        {
            Weight = 10.0;
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueGoldStone;
            Name = "GoldStone Platemail Chest";
        }

        public PlateChestGoldStone(Serial serial)
            : base(serial)
        {
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Tactics].Base += 3;
                from.Str += 3;
                from.SendAsciiMessage(0x44, "You Feel Yourself Stronger using GoldStone Plate!");
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Tactics].Base -= 3;
                from.Str -= 3;
            }

            base.OnRemoved(parent);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }


    [FlipableAttribute(0x1410, 0x1417)]
    public class PlateArmsGoldStone : BaseArmor
    {

        public override int AosStrReq { get { return 80; } }
        public override int OldStrReq { get { return 40; } }

        public override int AosDexBonus { get { return -2; } }


        public override int PhysicalResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.PhysicalResistance, DamageTypeEnum.ArmorType.PlateArms, CraftResource.GoldStone); } }
        public override int BaseColdResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseColdResistance, DamageTypeEnum.ArmorType.PlateArms, CraftResource.GoldStone); } }
        public override int BaseEnergyResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseEnergyResistance, DamageTypeEnum.ArmorType.PlateArms, CraftResource.GoldStone); } }
        public override int BaseFireResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseFireResistance, DamageTypeEnum.ArmorType.PlateArms, CraftResource.GoldStone); } }
        public override int BasePoisonResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BasePoisonResistance, DamageTypeEnum.ArmorType.PlateArms, CraftResource.GoldStone); } }
        public override int ArmorBase { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.ArmorBase, DamageTypeEnum.ArmorType.PlateArms, CraftResource.GoldStone); } }
        public override int InitMinHits { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.InitMinHits, DamageTypeEnum.ArmorType.PlateArms, CraftResource.GoldStone); } }
        public override int InitMaxHits { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.InitMaxHits, DamageTypeEnum.ArmorType.PlateArms, CraftResource.GoldStone); } }


        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

        [Constructable]
        public PlateArmsGoldStone()
            : base(0x1410)
        {
            Weight = 5.0;
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueGoldStone;
            Name = "GoldStone Platemail Arms";
        }

        public PlateArmsGoldStone(Serial serial)
            : base(serial)
        {
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Tactics].Base += 3;
                from.Str += 3;
                from.SendAsciiMessage(0x44, "You Feel Yourself Stronger using GoldStone Plate!");
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Tactics].Base -= 3;
                from.Str -= 3;
            }

            base.OnRemoved(parent);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            if (Weight == 1.0)
                Weight = 5.0;
        }
    }


    [FlipableAttribute(0x1411, 0x141a)]
    public class PlateLegsGoldStone : BaseArmor
    {
        public override int AosStrReq { get { return 90; } }

        public override int OldStrReq { get { return 60; } }
        public override int AosDexBonus { get { return -6; } }


        public override int PhysicalResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.PhysicalResistance, DamageTypeEnum.ArmorType.PlateLegs, CraftResource.GoldStone); } }
        public override int BaseColdResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseColdResistance, DamageTypeEnum.ArmorType.PlateLegs, CraftResource.GoldStone); } }
        public override int BaseEnergyResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseEnergyResistance, DamageTypeEnum.ArmorType.PlateLegs, CraftResource.GoldStone); } }
        public override int BaseFireResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseFireResistance, DamageTypeEnum.ArmorType.PlateLegs, CraftResource.GoldStone); } }
        public override int BasePoisonResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BasePoisonResistance, DamageTypeEnum.ArmorType.PlateLegs, CraftResource.GoldStone); } }
        public override int ArmorBase { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.ArmorBase, DamageTypeEnum.ArmorType.PlateLegs, CraftResource.GoldStone); } }
        public override int InitMinHits { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.InitMinHits, DamageTypeEnum.ArmorType.PlateLegs, CraftResource.GoldStone); } }
        public override int InitMaxHits { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.InitMaxHits, DamageTypeEnum.ArmorType.PlateLegs, CraftResource.GoldStone); } }



        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

        [Constructable]
        public PlateLegsGoldStone()
            : base(0x1411)
        {
            Weight = 7.0;
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueGoldStone;
            Name = "GoldStone Platemail Legs";
        }

        public PlateLegsGoldStone(Serial serial)
            : base(serial)
        {
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Tactics].Base += 3;
                from.Str += 3;
                from.SendAsciiMessage(0x44, "You Feel Yourself Stronger using GoldStone Plate!");
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Tactics].Base -= 3;
                from.Str -= 3;
            }

            base.OnRemoved(parent);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }


    public class PlateCloseHelmGoldStone : BaseArmor
    {

        public override int AosStrReq { get { return 55; } }
        public override int OldStrReq { get { return 40; } }


        public override int PhysicalResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.PhysicalResistance, DamageTypeEnum.ArmorType.PlateCloseHelm, CraftResource.GoldStone); } }
        public override int BaseColdResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseColdResistance, DamageTypeEnum.ArmorType.PlateCloseHelm, CraftResource.GoldStone); } }
        public override int BaseEnergyResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseEnergyResistance, DamageTypeEnum.ArmorType.PlateCloseHelm, CraftResource.GoldStone); } }
        public override int BaseFireResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseFireResistance, DamageTypeEnum.ArmorType.PlateCloseHelm, CraftResource.GoldStone); } }
        public override int BasePoisonResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BasePoisonResistance, DamageTypeEnum.ArmorType.PlateCloseHelm, CraftResource.GoldStone); } }
        public override int ArmorBase { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.ArmorBase, DamageTypeEnum.ArmorType.PlateCloseHelm, CraftResource.GoldStone); } }
        public override int InitMinHits { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.InitMinHits, DamageTypeEnum.ArmorType.PlateCloseHelm, CraftResource.GoldStone); } }
        public override int InitMaxHits { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.InitMaxHits, DamageTypeEnum.ArmorType.PlateCloseHelm, CraftResource.GoldStone); } }


        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

        [Constructable]
        public PlateCloseHelmGoldStone()
            : base(0x1408)
        {
            Weight = 5.0;
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueGoldStone;
            Name = "GoldStone Platemail Helm";
        }

        public PlateCloseHelmGoldStone(Serial serial)
            : base(serial)
        {
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Tactics].Base += 3;
                from.Str += 3;
                from.SendAsciiMessage(0x44, "You Feel Yourself Stronger using GoldStone Plate!");
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Tactics].Base -= 3;
                from.Str -= 3;
            }

            base.OnRemoved(parent);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class PlateGorgetGoldStone : BaseArmor
    {

        public override int AosStrReq { get { return 45; } }
        public override int OldStrReq { get { return 30; } }

        public override int AosDexBonus { get { return -1; } }


        public override int PhysicalResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.PhysicalResistance, DamageTypeEnum.ArmorType.PlateGorget, CraftResource.GoldStone); } }
        public override int BaseColdResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseColdResistance, DamageTypeEnum.ArmorType.PlateGorget, CraftResource.GoldStone); } }
        public override int BaseEnergyResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseEnergyResistance, DamageTypeEnum.ArmorType.PlateGorget, CraftResource.GoldStone); } }
        public override int BaseFireResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseFireResistance, DamageTypeEnum.ArmorType.PlateGorget, CraftResource.GoldStone); } }
        public override int BasePoisonResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BasePoisonResistance, DamageTypeEnum.ArmorType.PlateGorget, CraftResource.GoldStone); } }
        public override int ArmorBase { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.ArmorBase, DamageTypeEnum.ArmorType.PlateGorget, CraftResource.GoldStone); } }
        public override int InitMinHits { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.InitMinHits, DamageTypeEnum.ArmorType.PlateGorget, CraftResource.GoldStone); } }
        public override int InitMaxHits { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.InitMaxHits, DamageTypeEnum.ArmorType.PlateGorget, CraftResource.GoldStone); } }


        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

        [Constructable]
        public PlateGorgetGoldStone()
            : base(0x1413)
        {
            Weight = 2.0;
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueGoldStone;
            Name = "GoldStone Platemail Gorget";
        }

        public PlateGorgetGoldStone(Serial serial)
            : base(serial)
        {
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Tactics].Base += 3;
                from.Str += 3;
                from.SendAsciiMessage(0x44, "You Feel Yourself Stronger using GoldStone Plate!");
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Tactics].Base -= 3;
                from.Str -= 3;
            }

            base.OnRemoved(parent);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    [FlipableAttribute(0x1414, 0x1418)]
    public class PlateGlovesGoldStone : BaseArmor
    {
        public override int AosStrReq { get { return 70; } }
        public override int OldStrReq { get { return 30; } }

        public override int AosDexBonus { get { return -2; } }

        public override int PhysicalResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.PhysicalResistance, DamageTypeEnum.ArmorType.PlateGloves, CraftResource.GoldStone); } }
        public override int BaseColdResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseColdResistance, DamageTypeEnum.ArmorType.PlateGloves, CraftResource.GoldStone); } }
        public override int BaseEnergyResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseEnergyResistance, DamageTypeEnum.ArmorType.PlateGloves, CraftResource.GoldStone); } }
        public override int BaseFireResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseFireResistance, DamageTypeEnum.ArmorType.PlateGloves, CraftResource.GoldStone); } }
        public override int BasePoisonResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BasePoisonResistance, DamageTypeEnum.ArmorType.PlateGloves, CraftResource.GoldStone); } }
        public override int ArmorBase { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.ArmorBase, DamageTypeEnum.ArmorType.PlateGloves, CraftResource.GoldStone); } }
        public override int InitMinHits { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.InitMinHits, DamageTypeEnum.ArmorType.PlateGloves, CraftResource.GoldStone); } }
        public override int InitMaxHits { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.InitMaxHits, DamageTypeEnum.ArmorType.PlateGloves, CraftResource.GoldStone); } }


        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

        [Constructable]
        public PlateGlovesGoldStone()
            : base(0x1414)
        {
            Weight = 2.0;
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueGoldStone;
            Name = "GoldStone Platemail Gloves";
        }

        public PlateGlovesGoldStone(Serial serial)
            : base(serial)
        {
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Tactics].Base += 3;
                from.Str += 3;
                from.SendAsciiMessage(0x44, "You Feel Yourself Stronger using GoldStone Plate!");
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Tactics].Base -= 3;
                from.Str -= 3;
            }

            base.OnRemoved(parent);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

        }
    }

}