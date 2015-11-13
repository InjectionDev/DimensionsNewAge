using System;

namespace Server.Items
{

	public class NecklaceFencing : BaseNecklace
	{
		[Constructable]
		public NecklaceFencing() : base( 0x1088 )
		{
			Weight = 0.1;
            Name = "Necklace Of Fencing";
		}

        public NecklaceFencing(Serial serial)
            : base(serial)
		{
		}

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Fencing].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Fencing].Base -= 5;
            }

            base.OnRemoved(parent);
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

    public class NecklaceMacefighting : BaseNecklace
    {
        [Constructable]
        public NecklaceMacefighting()
            : base(0x1088)
        {
            Weight = 0.1;
            Name = "Necklace Of Macefighting";
        }

        public NecklaceMacefighting(Serial serial)
            : base(serial)
        {
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Macing].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Macing].Base -= 5;
            }

            base.OnRemoved(parent);
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

    public class NecklaceSwordsmanship : BaseNecklace
    {
        [Constructable]
        public NecklaceSwordsmanship()
            : base(0x1088)
        {
            Weight = 0.1;
            Name = "Necklace Of Swordsmanship";
        }

        public NecklaceSwordsmanship(Serial serial)
            : base(serial)
        {
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Swords].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Swords].Base -= 5;
            }

            base.OnRemoved(parent);
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

    public class NecklaceArchery : BaseNecklace
    {
        [Constructable]
        public NecklaceArchery()
            : base(0x1088)
        {
            Weight = 0.1;
            Name = "Necklace Of Archery";
        }

        public NecklaceArchery(Serial serial)
            : base(serial)
        {
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Archery].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Archery].Base -= 5;
            }

            base.OnRemoved(parent);
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

    public class NecklaceTactics : BaseNecklace
    {
        [Constructable]
        public NecklaceTactics()
            : base(0x1088)
        {
            Weight = 0.1;
            Name = "Necklace Of Tactics";
        }

        public NecklaceTactics(Serial serial)
            : base(serial)
        {
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Tactics].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Tactics].Base -= 5;
            }

            base.OnRemoved(parent);
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

    public class NecklaceWrestling : BaseNecklace
    {
        [Constructable]
        public NecklaceWrestling()
            : base(0x1088)
        {
            Weight = 0.1;
            Name = "Necklace Of Wrestling";
        }

        public NecklaceWrestling(Serial serial)
            : base(serial)
        {
        }

        public override bool OnEquip(Mobile from)
        {
            if (base.OnEquip(from))
            {
                from.Skills[SkillName.Wrestling].Base += 5;
                return true;
            }
            return false;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile from = parent as Mobile;
                from.Skills[SkillName.Wrestling].Base -= 5;
            }

            base.OnRemoved(parent);
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