using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a balron corpse" )]
	public class Balron : BaseCreature
	{

		[Constructable]
		public Balron () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
            Name = "Balron"; // NameList.RandomName("balron");
			Body = 40;
			BaseSoundID = 357;

			SetStr( 900, 1000 );
			SetDex( 150, 200 );
			SetInt( 150, 200 );

			SetHits( 600, 750 );

			SetDamage( 20, 25 );

			SetDamageType( ResistanceType.Physical, 100 );
			//SetDamageType( ResistanceType.Fire, 25 );
			//SetDamageType( ResistanceType.Energy, 25 );

            SetResistance(ResistanceType.Physical, 35, 45);
			SetResistance( ResistanceType.Fire, 60, 80 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 25.1, 50.0 );
            SetSkill(SkillName.EvalInt, 90.0, 90.0);
            SetSkill(SkillName.Magery, 90.0, 90.0);
			SetSkill( SkillName.Meditation, 25.1, 50.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
            SetSkill(SkillName.Tactics, 90.0, 90.0);
            SetSkill(SkillName.Wrestling, 90.0, 90.0);

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 50;

            PackItem(new Longsword());

            //Timer.DelayCall(TimeSpan.FromSeconds(3.0), new TimerCallback(SpawnGuardians));
		}


        private void SpawnGuardians()
        {
            Mobile lordArcher = new LordArcher();
            lordArcher.MoveToWorld(new Point3D(this.Location.X + 1, this.Location.Y, this.Location.Z), this.Map);

            Mobile lordWarrior = new LordWarrior();
            lordWarrior.MoveToWorld(new Point3D(this.Location.X, this.Location.Y + 1, this.Location.Z), this.Map);

            Mobile lordVeteran;
            if (Utility.RandomBool())
                lordVeteran = new LordVeteranArcher();
            else
                lordVeteran = new LordVeteranWarrior();
            lordVeteran.MoveToWorld(new Point3D(this.Location.X + 1, this.Location.Y + 1, this.Location.Z), this.Map);
        }

		public override void GenerateLoot()
		{
            AddLoot(LootPack.LootPackBalron, 1);
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 1; } }

		public Balron( Serial serial ) : base( serial )
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