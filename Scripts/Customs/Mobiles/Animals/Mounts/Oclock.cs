using System;
using Server.Mobiles;

namespace Server.Mobiles
{
    [CorpseName("an oclock corpse")]
	public class Oclock : BaseMount
	{
		[Constructable]
		public Oclock() : this( "Oclock" )
		{
		}

		[Constructable]
		public Oclock( string name ) : base( name, 0xD2, 0x3EA3, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			BaseSoundID = 0x270;

			SetStr( 94, 170 );
			SetDex( 56, 75 );
			SetInt( 6, 10 );

			SetHits( 71, 88 );
			SetMana( 0 );

			SetDamage( 5, 11 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Fire, 5, 15 );

			SetSkill( SkillName.MagicResist, 25.1, 30.0 );
			SetSkill( SkillName.Tactics, 25.3, 40.0 );
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

        public Oclock(Serial serial)
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

    [CorpseName("an rare oclock corpse")]
    public class OclockRare : BaseMount
    {
        [Constructable]
        public OclockRare()
            : this("Oclock Rare")
        {
        }

        [Constructable]
        public OclockRare(string name)
            : base(name, 0xD2, 0x3EA3, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            BaseSoundID = 0x270;

            SetStr(94, 170);
            SetDex(56, 75);
            SetInt(6, 10);

            SetHits(71, 88);
            SetMana(0);

            SetDamage(5, 11);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 15, 20);
            SetResistance(ResistanceType.Fire, 5, 15);

            SetSkill(SkillName.MagicResist, 25.1, 30.0);
            SetSkill(SkillName.Tactics, 25.3, 40.0);
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
                    Name = "Black Oclock";
                    break;
                case 0x1b6:
                    Name = "Crimson Oclock";
                    break;
                case 0x31c:
                    Name = "SkyGray Oclock";
                    break;
                case 0x158:
                    Name = "Wimmimate Oclock";
                    break;
                case 0x033:
                    Name = "Pamamino Oclock";
                    break;
                case 0x263:
                    Name = "Sky Oclock";
                    break;
                case 0x279:
                    Name = "Redroan Oclock";
                    break;
                case 0x1bb:
                    Name = "Roan Oclock";
                    break;
                case 0x3e7:
                    Name = "Grey Oclock";
                    break;

                default:
                    Name = "Oclock";
                    break;
            }
        }

        public override int Meat { get { return 3; } }
        public override FoodType FavoriteFood { get { return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }
        public override PackInstinct PackInstinct { get { return PackInstinct.Ostard; } }

        public OclockRare(Serial serial)
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

    [CorpseName("an exotic oclock corpse")]
    public class OclockExotic : BaseMount
    {
        [Constructable]
        public OclockExotic()
            : this("Oclock Exotic")
        {
        }

        [Constructable]
        public OclockExotic(string name)
            : base(name, 0xD2, 0x3EA3, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            BaseSoundID = 0x270;

            SetStr(94, 170);
            SetDex(56, 75);
            SetInt(6, 10);

            SetHits(71, 88);
            SetMana(0);

            SetDamage(5, 11);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 15, 20);
            SetResistance(ResistanceType.Fire, 5, 15);

            SetSkill(SkillName.MagicResist, 25.1, 30.0);
            SetSkill(SkillName.Tactics, 25.3, 40.0);
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

        public OclockExotic(Serial serial)
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