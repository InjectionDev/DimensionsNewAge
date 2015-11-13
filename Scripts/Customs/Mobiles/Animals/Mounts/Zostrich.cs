using System;
using Server.Mobiles;

namespace Server.Mobiles
{
    [CorpseName("an zostrich corpse")]
	public class Zostrich : BaseMount
	{
		[Constructable]
		public Zostrich() : this( "Zostrich" )
		{
		}

		[Constructable]
		public Zostrich( string name ) : base( name, 0xDA, 0x3EA4, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
            if (Utility.RandomDouble() < 0.2)
			    Hue = Utility.RandomHairHue() | 0x8000;

			BaseSoundID = 0x275;

			SetStr( 94, 170 );
			SetDex( 96, 115 );
			SetInt( 6, 10 );

			SetHits( 71, 110 );
			SetMana( 0 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 30 );
			SetResistance( ResistanceType.Fire, 10, 15 );
			SetResistance( ResistanceType.Poison, 20, 25 );
			SetResistance( ResistanceType.Energy, 20, 25 );

			SetSkill( SkillName.MagicResist, 75.1, 80.0 );
			SetSkill( SkillName.Tactics, 79.3, 94.0 );
			SetSkill( SkillName.Wrestling, 79.3, 94.0 );

			Fame = 1500;
			Karma = -1500;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 85.0;
		}

		public override int Meat{ get{ return 3; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish | FoodType.Eggs | FoodType.FruitsAndVegies; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Ostard; } }

        public Zostrich(Serial serial)
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

    [CorpseName("an rare zostrich corpse")]
    public class ZostrichRare : BaseMount
    {
        [Constructable]
        public ZostrichRare()
            : this("Zostrich Rare")
        {
        }

        [Constructable]
        public ZostrichRare(string name)
            : base(name, 0xDA, 0x3EA4, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            BaseSoundID = 0x275;

            SetStr(94, 170);
            SetDex(96, 115);
            SetInt(6, 10);

            SetHits(71, 110);
            SetMana(0);

            SetDamage(11, 17);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 25, 30);
            SetResistance(ResistanceType.Fire, 10, 15);
            SetResistance(ResistanceType.Poison, 20, 25);
            SetResistance(ResistanceType.Energy, 20, 25);

            SetSkill(SkillName.MagicResist, 75.1, 80.0);
            SetSkill(SkillName.Tactics, 79.3, 94.0);
            SetSkill(SkillName.Wrestling, 79.3, 94.0);

            Fame = 1500;
            Karma = -1500;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 100.0;

            Hue = DimensionsNewAge.Scripts.HueItemConst.HueMustangColorRandom;

            switch (Hue)
            {
                case 0x455:
                    Name = "Black Zostrich";
                    break;
                case 0x1b6:
                    Name = "Crimson Zostrich";
                    break;
                case 0x31c:
                    Name = "SkyGray Zostrich";
                    break;
                case 0x158:
                    Name = "Wimmimate Zostrich";
                    break;
                case 0x033:
                    Name = "Pamamino Zostrich";
                    break;
                case 0x263:
                    Name = "Sky Zostrich";
                    break;
                case 0x279:
                    Name = "Redroan Zostrich";
                    break;
                case 0x1bb:
                    Name = "Roan Zostrich";
                    break;
                case 0x3e7:
                    Name = "Grey Zostrich";
                    break;

                default:
                    Name = "Zostrich";
                    break;
            }
        }

        public override int Meat { get { return 3; } }
        public override FoodType FavoriteFood { get { return FoodType.Meat | FoodType.Fish | FoodType.Eggs | FoodType.FruitsAndVegies; } }
        public override PackInstinct PackInstinct { get { return PackInstinct.Ostard; } }

        public ZostrichRare(Serial serial)
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

    [CorpseName("an exotic zostrich corpse")]
    public class ZostrichExotic : BaseMount
    {
        [Constructable]
        public ZostrichExotic()
            : this("Zostrich Exotic")
        {
        }

        [Constructable]
        public ZostrichExotic(string name)
            : base(name, 0xDA, 0x3EA4, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            BaseSoundID = 0x275;

            SetStr(94, 170);
            SetDex(96, 115);
            SetInt(6, 10);

            SetHits(71, 110);
            SetMana(0);

            SetDamage(11, 17);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 25, 30);
            SetResistance(ResistanceType.Fire, 10, 15);
            SetResistance(ResistanceType.Poison, 20, 25);
            SetResistance(ResistanceType.Energy, 20, 25);

            SetSkill(SkillName.MagicResist, 75.1, 80.0);
            SetSkill(SkillName.Tactics, 79.3, 94.0);
            SetSkill(SkillName.Wrestling, 79.3, 94.0);

            Fame = 1500;
            Karma = -1500;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 105.0;

            Hue = DimensionsNewAge.Scripts.HueItemConst.HueMagicColorRandom;
        }

        public override int Meat { get { return 3; } }
        public override FoodType FavoriteFood { get { return FoodType.Meat | FoodType.Fish | FoodType.Eggs | FoodType.FruitsAndVegies; } }
        public override PackInstinct PackInstinct { get { return PackInstinct.Ostard; } }

        public ZostrichExotic(Serial serial)
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