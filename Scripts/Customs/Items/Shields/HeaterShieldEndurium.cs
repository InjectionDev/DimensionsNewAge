using System;
using Server;

namespace Server.Items
{
    public class HeaterShieldEndurium : BaseShield
    {

        public override int AosStrReq { get { return 90; } }

        public override int PhysicalResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.PhysicalResistance, DamageTypeEnum.ArmorType.HeaterShield, CraftResource.Endurium); } }
        public override int BaseColdResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseColdResistance, DamageTypeEnum.ArmorType.HeaterShield, CraftResource.Endurium); } }
        public override int BaseEnergyResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseEnergyResistance, DamageTypeEnum.ArmorType.HeaterShield, CraftResource.Endurium); } }
        public override int BaseFireResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BaseFireResistance, DamageTypeEnum.ArmorType.HeaterShield, CraftResource.Endurium); } }
        public override int BasePoisonResistance { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.BasePoisonResistance, DamageTypeEnum.ArmorType.HeaterShield, CraftResource.Endurium); } }
        public override int ArmorBase { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.ArmorBase, DamageTypeEnum.ArmorType.HeaterShield, CraftResource.Endurium); } }
        public override int InitMinHits { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.InitMinHits, DamageTypeEnum.ArmorType.HeaterShield, CraftResource.Endurium); } }
        public override int InitMaxHits { get { return ItemQualityHelper.GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType.InitMaxHits, DamageTypeEnum.ArmorType.HeaterShield, CraftResource.Endurium); } }


        [Constructable]
        public HeaterShieldEndurium()
            : base(0x1B76)
        {
            Weight = 8.0;
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueEndurium;
            Name = "Endurium Heater Shield";
        }

        public HeaterShieldEndurium(Serial serial)
            : base(serial)
        {
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);//version
        }
    }
}
