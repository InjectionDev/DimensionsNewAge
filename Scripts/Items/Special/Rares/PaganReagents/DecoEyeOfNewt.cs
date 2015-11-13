using System;

namespace Server.Items
{
    public class DecoEyeOfNewt : BaseReagent
	{



        [Constructable]
        public DecoEyeOfNewt()
            : this(1)
		{
		}

		[Constructable]
		public DecoEyeOfNewt( int amount ) : base( 0xF87, amount )
		{
		}

		public DecoEyeOfNewt( Serial serial ) : base( serial )
		{
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
}
