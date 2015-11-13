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
    public class KnightOfBritainSign : Item
    {

        [Constructable]
        public KnightOfBritainSign()
            : base(0x0C03)
        {
            Name = "Knight Of Britain";
            Movable = false;
            Hue = 1974;
            
        }

        public KnightOfBritainSign(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.SendGump(new KnightOfBritainGump(from));

            base.OnDoubleClick(from);
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
