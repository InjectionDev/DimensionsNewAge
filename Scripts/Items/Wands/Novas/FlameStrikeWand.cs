using System;
using Server;
using Server.Targeting;

namespace Server.Items
{
    public class FlameStrikeWand : BaseWand
    {
        [Constructable]
        public FlameStrikeWand()
            : base(WandEffect.FlameStrike, 3, 10)
        {
        }

        public FlameStrikeWand(Serial serial)
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

        public override void OnWandUse(Mobile from)
        {
            Cast(new Server.Spells.Seventh.FlameStrikeSpell(from, this));
        }
    }
}