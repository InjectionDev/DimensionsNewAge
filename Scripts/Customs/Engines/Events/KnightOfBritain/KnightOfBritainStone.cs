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
    public class KnightOfBritainStone : Item
    {
        private Rectangle2D KnightOfBritainArea;

        [Constructable]
        public KnightOfBritainStone() : base(0x3001)
        {
            Name = "Knight Of Britain Stone";
            Movable = false;
            Hue = 1974;

            this.InitializeEvent();
        }

        private void InitializeEvent()
        {
            Point2D eventArenaPointA = new Point2D(0, 0);
            Point2D eventArenaPointB = new Point2D(0, 0);
            this.KnightOfBritainArea = new Rectangle2D(eventArenaPointA, eventArenaPointB);

        }

        public KnightOfBritainStone(Serial serial)
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

            this.InitializeEvent();
        }

    }
}
