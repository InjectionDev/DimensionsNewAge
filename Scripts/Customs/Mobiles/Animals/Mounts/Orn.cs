using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "an orn corpse" )]
	public class Orn : BaseMount
	{
		[Constructable]
		public Orn() : this( "Orn" )
		{
		}

		[Constructable]
		public Orn( string name ) : base( name, 0xDB, 0x3EA5, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
            if (Utility.RandomDouble() < 0.2)
			    Hue = Utility.RandomSlimeHue() | 0x8000;

			BaseSoundID = 0x270;

			SetStr( 94, 170 );
			SetDex( 56, 75 );
			SetInt( 6, 10 );

			SetHits( 71, 88 );
			SetMana( 0 );

			SetDamage( 8, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );

			SetSkill( SkillName.MagicResist, 27.1, 32.0 );
			SetSkill( SkillName.Tactics, 29.3, 44.0 );
			SetSkill( SkillName.Wrestling, 29.3, 44.0 );

			Fame = 450;
			Karma = 0;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 85.0;
		}

		public override int Meat{ get{ return 3; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Ostard; } }

        public Orn(Serial serial)
            : base(serial)
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

    [CorpseName("an rare orn corpse")]
    public class OrnRare : BaseMount
    {
        [Constructable]
        public OrnRare()
            : this("Orn Rare")
        {
        }

        [Constructable]
        public OrnRare(string name)
            : base(name, 0xDB, 0x3EA5, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            BaseSoundID = 0x270;

            SetStr(94, 170);
            SetDex(56, 75);
            SetInt(6, 10);

            SetHits(71, 88);
            SetMana(0);

            SetDamage(8, 14);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 15, 20);

            SetSkill(SkillName.MagicResist, 27.1, 32.0);
            SetSkill(SkillName.Tactics, 29.3, 44.0);
            SetSkill(SkillName.Wrestling, 29.3, 44.0);

            Fame = 450;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 100.0;

            Hue = DimensionsNewAge.Scripts.HueItemConst.HueMustangColorRandom;

            switch (Hue)
            {
                case 0x455:
                    Name = "Black Orn";
                    break;
                case 0x1b6:
                    Name = "Crimson Orn";
                    break;
                case 0x31c:
                    Name = "SkyGray Orn";
                    break;
                case 0x158:
                    Name = "Wimmimate Orn";
                    break;
                case 0x033:
                    Name = "Pamamino Orn";
                    break;
                case 0x263:
                    Name = "Sky Orn";
                    break;
                case 0x279:
                    Name = "Redroan Orn";
                    break;
                case 0x1bb:
                    Name = "Roan Orn";
                    break;
                case 0x3e7:
                    Name = "Grey Orn";
                    break;

                default:
                    Name = "Orn";
                    break;
            }
        }

        public override int Meat { get { return 3; } }
        public override FoodType FavoriteFood { get { return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }
        public override PackInstinct PackInstinct { get { return PackInstinct.Ostard; } }

        public OrnRare(Serial serial)
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

    [CorpseName("an exotic orn corpse")]
    public class OrnExotic : BaseMount
    {
        [Constructable]
        public OrnExotic()
            : this("Orn Exotic")
        {
        }

        [Constructable]
        public OrnExotic(string name)
            : base(name, 0xDB, 0x3EA5, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            BaseSoundID = 0x270;

            SetStr(94, 170);
            SetDex(56, 75);
            SetInt(6, 10);

            SetHits(71, 88);
            SetMana(0);

            SetDamage(8, 14);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 15, 20);

            SetSkill(SkillName.MagicResist, 27.1, 32.0);
            SetSkill(SkillName.Tactics, 29.3, 44.0);
            SetSkill(SkillName.Wrestling, 29.3, 44.0);

            Fame = 450;
            Karma = 0;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 105.0;

            Hue = DimensionsNewAge.Scripts.HueItemConst.HueMagicColorRandom;
        }

        public override int Meat { get { return 3; } }
        public override FoodType FavoriteFood { get { return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }
        public override PackInstinct PackInstinct { get { return PackInstinct.Ostard; } }

        public OrnExotic(Serial serial)
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
}