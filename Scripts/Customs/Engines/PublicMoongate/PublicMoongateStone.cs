using System;
using Server;
using Server.Commands;
using Server.Mobiles;
using Server.Regions;
using Server.Gumps;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


using DimensionsNewAge.Scripts.Customs.Engines;

namespace Server.Items
{
    public class PublicMoongateStone : Item
    {


        [Constructable]
        public PublicMoongateStone()
            : base(0xEDC)
        {
            Name = "Star Room Stone";
            Movable = false;
            Hue = 1557;
        }


        public override void OnDoubleClick(Mobile from)
        {
            from.SendGump(new PublicMoongateGump(from));
        }


        public PublicMoongateStone(Serial serial)
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