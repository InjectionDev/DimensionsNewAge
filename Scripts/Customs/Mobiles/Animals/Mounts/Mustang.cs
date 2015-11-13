using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a mustang corpse" )]
	public class Mustang : BaseMount
	{
		[Constructable]
		public Mustang() : this( "Mustang" )
		{
		}

		[Constructable]
        public Mustang(string name)
            : base(name, 0x74, 0x3EA7, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
		{
			BaseSoundID = 0xA8;

			SetStr( 100, 150 );
			SetDex( 50, 60 );
			SetInt( 50, 60 );

			SetHits( 150, 200 );

			SetDamage( 10, 16 );

			SetDamageType( ResistanceType.Physical, 100 );
			//SetDamageType( ResistanceType.Fire, 40 );
			//SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.EvalInt, 10.4, 50.0 );
			SetSkill( SkillName.Magery, 10.4, 50.0 );
			SetSkill( SkillName.MagicResist, 85.3, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 80.5, 92.5 );

			Fame = 500;
			Karma = 500;

			VirtualArmor = 60;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 90.0;

            Hue = DimensionsNewAge.Scripts.HueItemConst.HueMustangColorRandom;

            switch (Hue)
            {
                case 1109:
                    Name = "Black Mustang";
                    break;
                case 438:
                    Name = "Crimson Mustang";
                    break;
                case 796:
                    Name = "SkyGray Mustang";
                    break;
                case 344:
                    Name = "Wimmimate Mustang";
                    break;
                case 51:
                    Name = "Pamamino Mustang";
                    break;
                case 611:
                    Name = "Sky Mustang";
                    break;
                case 279:
                    Name = "Redroan Mustang";
                    break;
                case 443:
                    Name = "Roan Mustang";
                    break;
                case 999:
                    Name = "Grey Mustang";
                    break;

                default:
                    Name = "Mustang";
                    break;
            }

            BodyValue = 116;
            ItemID = 16039;

			PackItem( new SulfurousAsh( Utility.RandomMinMax( 3, 5 ) ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.LowScrolls );
			AddLoot( LootPack.Potions );
		}

		public override int GetAngerSound()
		{
			if ( !Controlled )
				return 0x16A;

			return base.GetAngerSound();
		}

		public override int Meat{ get{ return 5; } }
		public override int Hides{ get{ return 10; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return false; } }

        public Mustang(Serial serial)
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

    [CorpseName("a exotic mustang corpse")]
    public class MustangExotic : BaseMount
    {
        [Constructable]
        public MustangExotic()
            : this("Mustang Exotic")
        {
        }

        [Constructable]
        public MustangExotic(string name)
            : base(name, 0x74, 0x3EA7, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            BaseSoundID = 0xA8;

            SetStr(100, 150);
            SetDex(50, 60);
            SetInt(50, 60);

            SetHits(150, 200);

            SetDamage(10, 16);

            SetDamageType(ResistanceType.Physical, 100);
            //SetDamageType( ResistanceType.Fire, 40 );
            //SetDamageType( ResistanceType.Energy, 20 );

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 20, 30);

            SetSkill(SkillName.EvalInt, 10.4, 50.0);
            SetSkill(SkillName.Magery, 10.4, 50.0);
            SetSkill(SkillName.MagicResist, 85.3, 100.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 80.5, 92.5);

            Fame = 500;
            Karma = 500;

            VirtualArmor = 60;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 95;

            Hue = DimensionsNewAge.Scripts.HueItemConst.HueMagicColorRandom;

            BodyValue = 116;
            ItemID = 16039;


            PackItem(new SulfurousAsh(Utility.RandomMinMax(3, 5)));
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average);
            AddLoot(LootPack.LowScrolls);
            AddLoot(LootPack.Potions);
        }

        public override int GetAngerSound()
        {
            if (!Controlled)
                return 0x16A;

            return base.GetAngerSound();
        }

        public override int Meat { get { return 5; } }
        public override int Hides { get { return 10; } }
        public override HideType HideType { get { return HideType.Barbed; } }
        public override FoodType FavoriteFood { get { return FoodType.Meat; } }
        public override bool CanAngerOnTame { get { return false; } }

        public MustangExotic(Serial serial)
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