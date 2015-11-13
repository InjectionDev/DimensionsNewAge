using System;

namespace Server.Items
{

    public class BraceletAlchemy : BaseBracelet
    {
        [Constructable]
        public BraceletAlchemy()
            : base(0x1086)
        {
            Weight = 0.1;
            Name = "Bracelet Of Alchemy";
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Alchemy].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Alchemy].Base -= 5;
            }

            base.OnRemoved(parent);
        }

        public BraceletAlchemy(Serial serial)
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

    public class BraceletBlacksmithing : BaseBracelet
    {
        [Constructable]
        public BraceletBlacksmithing()
            : base(0x1086)
        {
            Weight = 0.1;
            Name = "Bracelet Of Blacksmithing";
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Blacksmith].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Blacksmith].Base -= 5;
            }

            base.OnRemoved(parent);
        }

        public BraceletBlacksmithing(Serial serial)
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

    public class BraceletMining : BaseBracelet
    {
        [Constructable]
        public BraceletMining()
            : base(0x1086)
        {
            Weight = 0.1;
            Name = "Bracelet Of Mining";
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Mining].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Mining].Base -= 5;
            }

            base.OnRemoved(parent);
        }

        public BraceletMining(Serial serial)
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

    public class BraceletBowcraft : BaseBracelet
    {
        [Constructable]
        public BraceletBowcraft()
            : base(0x1086)
        {
            Weight = 0.1;
            Name = "Bracelet Of Bowcraft";
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Fletching].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Fletching].Base -= 5;
            }

            base.OnRemoved(parent);
        }

        public BraceletBowcraft(Serial serial)
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

    public class BraceletTaming : BaseBracelet
    {
        [Constructable]
        public BraceletTaming()
            : base(0x1086)
        {
            Weight = 0.1;
            Name = "Bracelet Of Taming";
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.AnimalTaming].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.AnimalTaming].Base -= 5;
            }

            base.OnRemoved(parent);
        }

        public BraceletTaming(Serial serial)
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

    public class BraceletLumberjacking : BaseBracelet
    {
        [Constructable]
        public BraceletLumberjacking()
            : base(0x1086)
        {
            Weight = 0.1;
            Name = "Bracelet Of Lumberjacking";
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Lumberjacking].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Lumberjacking].Base -= 5;
            }

            base.OnRemoved(parent);
        }

        public BraceletLumberjacking(Serial serial)
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

    public class BraceletSnooping : BaseBracelet
    {
        [Constructable]
        public BraceletSnooping()
            : base(0x1086)
        {
            Weight = 0.1;
            Name = "Bracelet Of Snooping";
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Snooping].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Snooping].Base -= 5;
            }

            base.OnRemoved(parent);
        }

        public BraceletSnooping(Serial serial)
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

    public class BraceletTailoring : BaseBracelet
    {
        [Constructable]
        public BraceletTailoring()
            : base(0x1086)
        {
            Weight = 0.1;
            Name = "Bracelet Of Tailoring";
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Tailoring].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Tailoring].Base -= 5;
            }

            base.OnRemoved(parent);
        }

        public BraceletTailoring(Serial serial)
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

    public class BraceletPoisoning : BaseBracelet
    {
        [Constructable]
        public BraceletPoisoning()
            : base(0x1086)
        {
            Weight = 0.1;
            Name = "Bracelet Of Poisoning";
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Poisoning].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Poisoning].Base -= 5;
            }

            base.OnRemoved(parent);
        }

        public BraceletPoisoning(Serial serial)
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

    public class BraceletStealing : BaseBracelet
    {
        [Constructable]
        public BraceletStealing()
            : base(0x1086)
        {
            Weight = 0.1;
            Name = "Bracelet Of Stealing";
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Stealing].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Stealing].Base -= 5;
            }

            base.OnRemoved(parent);
        }

        public BraceletStealing(Serial serial)
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
