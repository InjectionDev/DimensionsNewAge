using System;

namespace Server.Items
{

	public class RingDexterity : BaseRing
	{
		[Constructable]
        public RingDexterity() : base(0x108a)
		{
			Weight = 0.1;
            Name = "Ring Of Dexterity";
		}

		public RingDexterity( Serial serial ) : base( serial )
		{
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

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); 
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

    public class RingStrenght : BaseRing
    {
        [Constructable]
        public RingStrenght()
            : base(0x108a)
        {
            Weight = 0.1;
            Name = "Ring Of Strenght";
        }

        public RingStrenght(Serial serial)
            : base(serial)
        {
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

    public class RingInteligence : BaseRing
    {
        [Constructable]
        public RingInteligence()
            : base(0x108a)
        {
            Weight = 0.1;
            Name = "Ring Of Inteligence";
        }

        public RingInteligence(Serial serial)
            : base(serial)
        {
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

    public class RingPower : BaseRing
    {
        [Constructable]
        public RingPower()
            : base(0x108a)
        {
            Weight = 0.1;
            Name = "Ring Of Power";
        }

        public RingPower(Serial serial)
            : base(serial)
        {
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
