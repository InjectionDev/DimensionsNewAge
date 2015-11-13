using System;

namespace Server.Items
{
	public class MagicDyeTub : DyeTub
	{
        public int QtDyes { get; set; }
        public int QtMaxDyes { get; set; }

		[Constructable]
		public MagicDyeTub()
		{
            this.QtMaxDyes = 2;
            this.QtDyes = 0;
            

            Hue = DyedHue = DimensionsNewAge.Scripts.HueItemConst.HuesDyeTubColorRandom;
            Name = string.Format("Magic Dying Tub ({0} cargas)", QtMaxDyes - QtDyes);
		}


        public MagicDyeTub(Serial serial)
            : base(serial)
		{
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
}