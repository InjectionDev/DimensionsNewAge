using System;
using Server;

namespace Server.Items
{
    public class TeamARobe : BaseOuterTorso
    {

        [Constructable]
        public TeamARobe()
            : base(0x2684)
        {
            Name = "Time Verde";
            Movable = false;
            Hue = 1962; // Verde
            StrRequirement = 0;
        }

        public TeamARobe(Serial serial)
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

    public class TeamBRobe : BaseOuterTorso
    {

        [Constructable]
        public TeamBRobe()
            : base(0x2684)
        {
            Name = "Time Branco";
            Movable = false;
            Hue = 1974; // branco
            StrRequirement = 0;
        }

        public TeamBRobe(Serial serial)
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


    public class TeamAShoes : BaseShoes
    {
        [Constructable]
        public TeamAShoes()
            : base(0x170F)
        {
            Name = "Time Verde";
            Movable = false;
            Hue = 1962; // Verde
            StrRequirement = 0;
        }

        public TeamAShoes(Serial serial)
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

    public class TeamBShoes : BaseShoes
    {
        [Constructable]
        public TeamBShoes()
            : base(0x170F)
        {
            Name = "Time Branco";
            Movable = false;
            Hue = 1974; // branco
            StrRequirement = 0;
        }

        public TeamBShoes(Serial serial)
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
