using System;
using Server;
using Server.Items;
using Server.Commands;

namespace Server.Items
{
    public class BagBetaTest : Bag
    {


        [Constructable]
        public BagBetaTest()
        {
            Hue = 780;
            Name = "Bag BetaTeste";

            Bag bagGold = new Bag();
            bagGold.DropItem(new Gold(50000));
            bagGold.DropItem(new Gold(50000));
            bagGold.DropItem(new Gold(50000));
            bagGold.DropItem(new Gold(50000));
            bagGold.DropItem(new Gold(50000));
            bagGold.DropItem(new Gold(50000));
            bagGold.DropItem(new Gold(50000));
            bagGold.DropItem(new Gold(50000));
            bagGold.DropItem(new Gold(50000));
            bagGold.DropItem(new Gold(50000));
            this.DropItem(bagGold);

            this.DropItem(new BagOfOres(250));
            this.DropItem(new Server.Multis.Deeds.CastleDeed());
        }


        public BagBetaTest(Serial serial)
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
