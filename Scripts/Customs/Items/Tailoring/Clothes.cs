using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Items
{
    [Flipable]
    public class ThighBootsOfCamouflage : BaseShoes
    {

        //SKILLMAKE=TAILORING 101.0, MAGERY 90.0, t_sewing_kit
        //RESOURCES=20 i_CLOTH, 17 i_reag_spider_silk

        [Constructable]
        public ThighBootsOfCamouflage()
            : base(0x1711)
        {
            Weight = 3.0;
            Name = "Boots Of Camouflage";
            Hue = 030;
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Hiding].Base += 5;
                from.Skills[SkillName.Stealth].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Hiding].Base -= 5;
                from.Skills[SkillName.Stealth].Base -= 5;
            }

            base.OnRemoved(parent);
        }

        public ThighBootsOfCamouflage(Serial serial)
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

    [FlipableAttribute(0x13d5, 0x13dd)]
    public class FishingGloves : BaseArmor
    {
        //SKILLMAKE=TAILORING 50.0, MAGERY 40.0, t_sewing_kit
        //RESOURCES=7 i_CLOTH, 3 i_reag_spider_silk

        public override int BasePhysicalResistance { get { return 2; } }

        public override int InitMinHits { get { return 35; } }
        public override int InitMaxHits { get { return 45; } }

        public override int AosStrReq { get { return 25; } }
        public override int OldStrReq { get { return 25; } }

        public override int ArmorBase { get { return 16; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Studded; } }
        public override CraftResource DefaultResource { get { return CraftResource.RegularLeather; } }

        public override ArmorMeditationAllowance DefMedAllowance { get { return ArmorMeditationAllowance.Half; } }

        [Constructable]
        public FishingGloves()
            : base(0x13D5)
        {
            Weight = 1.0;
            Name = "Fishing Gloves";
            Hue = 060;
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Fishing].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Fishing].Base -= 5;
            }

            base.OnRemoved(parent);
        }

        public FishingGloves(Serial serial)
            : base(serial)
        {
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

    public class MeditHat : BaseHat
    {

        //RESOURCES=7 i_CLOTH, 7 i_reag_blood_moss
        //SKILLMAKE=TAILORING 70.0, MAGERY 60.0, t_sewing_kit

        public override int BasePhysicalResistance { get { return 0; } }

        public override int InitMinHits { get { return 20; } }
        public override int InitMaxHits { get { return 30; } }

        [Constructable]
        public MeditHat()
            : base(0x1718)
        {
            Weight = 1.0;
            Name = "Meditation Hat";
            Hue = 02;
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Meditation].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Meditation].Base -= 5;
            }

            base.OnRemoved(parent);
        }

        public MeditHat(Serial serial)
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

    [Flipable(0x1F00, 0x1EFF)]
    public class FancyDressOfBless : BaseOuterTorso
    {

        [Constructable]
        public FancyDressOfBless()
            : base(0x1F00)
        {
            Weight = 3.0;
            Name = "Fancy Dress Of Bless";
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Str += 3;
                from.Dex += 3;
                from.Int += 3;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Str -= 3;
                from.Dex -= 3;
                from.Int -= 3;
            }

            base.OnRemoved(parent);
        }

        public FancyDressOfBless(Serial serial)
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

    [Flipable]
    public class RobeOfBless : BaseOuterTorso
    {


        [Constructable]
        public RobeOfBless()
            : base(0x1F03)
        {
            Weight = 3.0;
            Name = "Robe Of Bless";
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Str += 3;
                from.Dex += 3;
                from.Int += 3;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Str -= 3;
                from.Dex -= 3;
                from.Int -= 3;
            }

            base.OnRemoved(parent);
        }

        public RobeOfBless(Serial serial)
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

    [Flipable(0x1541, 0x1542)]
    public class BodySashOfStrenght : BaseMiddleTorso
    {

        [Constructable]
        public BodySashOfStrenght()
            : base(0x1541)
        {
            Weight = 1.0;
            Name = "Body Sash Of Strenght";
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Str += 3;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Str -= 3;
            }

            base.OnRemoved(parent);
        }

        public BodySashOfStrenght(Serial serial)
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

    [Flipable]
    public class CloakOfIllusion : BaseCloak
    {

        [Constructable]
        public CloakOfIllusion()
            : base(0x1515)
        {
            Weight = 5.0;
            Name = "Cloak Of Illusion";
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Magery].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Magery].Base -= 5;
            }

            base.OnRemoved(parent);
        }

        public CloakOfIllusion(Serial serial)
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

    [FlipableAttribute(0x152e, 0x152f)]
    public class ShortPantsOfBrain : BasePants
    {

        [Constructable]
        public ShortPantsOfBrain()
            : base(0x152E)
        {
            Weight = 2.0;
            Name = "Pants Of Brain";
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Int += 3;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Int -= 3;
            }

            base.OnRemoved(parent);
        }

        public ShortPantsOfBrain(Serial serial)
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

    [FlipableAttribute(0x1517, 0x1518)]
    public class ShirtOfDexterity : BaseShirt
    {
        [Constructable]
        public ShirtOfDexterity()
            : base(0x1517)
        {
            Weight = 1.0;
            Name = "Shirt Of Dexterity";
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Dex += 3;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Dex -= 3;
            }

            base.OnRemoved(parent);
        }

        public ShirtOfDexterity(Serial serial)
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

            if (Weight == 2.0)
                Weight = 1.0;
        }
    }
}
