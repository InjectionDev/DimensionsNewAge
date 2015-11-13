using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a lord corpse")]
    public class LordWarrior : BaseCreature
    {
        [Constructable]
        public LordWarrior()
            : base(AIType.AI_Melee, FightMode.Closest, 12, 1, 0.2, 0.4)
        {
            Name = "Lord Warrior";
            Body = 0x190;
            BaseSoundID = 0x0;

            SetStr(250, 300);
            SetDex(150, 170);
            SetInt(71, 92);

            SetHits(300, 400);

            SetDamage(30, 35);

            SetDamageType(ResistanceType.Physical, 100);
            //SetDamageType( ResistanceType.Fire, 25 );
            //SetDamageType( ResistanceType.Cold, 25 );
            //SetDamageType( ResistanceType.Energy, 25 );

            SetResistance(ResistanceType.Physical, 25, 35);
            SetResistance(ResistanceType.Fire, 50, 60);
            SetResistance(ResistanceType.Cold, 50, 60);
            SetResistance(ResistanceType.Poison, 50, 60);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.MagicResist, 60.0, 80.0);
            SetSkill(SkillName.Tactics, 60.0, 80.0);
            SetSkill(SkillName.Swords, 60.0, 80.0);
            SetSkill(SkillName.Fencing, 60.0, 80.0);
            SetSkill(SkillName.Macing, 60.0, 80.0);
            SetSkill(SkillName.Wrestling, 60.0, 80.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 30;

            AddItem(new PlateArmsIron());
            AddItem(new PlateChestIron());
            AddItem(new PlateCloseHelmIron());
            AddItem(new PlateGlovesIron());
            AddItem(new PlateGorgetIron());
            AddItem(new PlateLegsIron());

            switch (Utility.Random(4))
            {
                case 0: EquipItem(new KryssIron() { LootType = LootType.Blessed }); break;
                case 1: EquipItem(new SwordIron() { LootType = LootType.Blessed }); break;
                case 2: EquipItem(new BardicheIron() { LootType = LootType.Blessed }); break;
                case 3: EquipItem(new WarMaceIron() { LootType = LootType.Blessed }); break;
            }

            EquipItem(new HalfApron(37));
            EquipItem(new BodySash(37));
            EquipItem(new Cloak(37));
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosRich);
        }

        public override bool AutoDispel { get { return false; } }
        public override bool BleedImmune { get { return true; } }
        public override int TreasureMapLevel { get { return 2; } }


        public LordWarrior(Serial serial)
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

    [CorpseName("a lord corpse")]
    public class LordArcher : BaseCreature
    {
        [Constructable]
        public LordArcher()
            : base(AIType.AI_Archer, FightMode.Closest, 12, 1, 0.2, 0.4)
        {
            Name = "Lord Archer";
            Body = 0x190;
            BaseSoundID = 0x0;

            SetStr(250, 300);
            SetDex(200, 220);
            SetInt(71, 92);

            SetHits(300, 400);

            SetDamage(30, 35);

            SetDamageType(ResistanceType.Physical, 100);
            //SetDamageType( ResistanceType.Fire, 25 );
            //SetDamageType( ResistanceType.Cold, 25 );
            //SetDamageType( ResistanceType.Energy, 25 );

            SetResistance(ResistanceType.Physical, 25, 35);
            SetResistance(ResistanceType.Fire, 50, 60);
            SetResistance(ResistanceType.Cold, 50, 60);
            SetResistance(ResistanceType.Poison, 50, 60);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.MagicResist, 50.1, 95.0);
            SetSkill(SkillName.Tactics, 60.0, 100.0);
            SetSkill(SkillName.Archery, 60.0, 90.0);
            SetSkill(SkillName.Wrestling, 60.0, 90.0);

            Fame = 3500;
            Karma = -3500;

            VirtualArmor = 30;

            AddItem(new PlateArmsIron());
            AddItem(new PlateChestIron());
            AddItem(new PlateCloseHelmIron());
            AddItem(new PlateGlovesIron());
            AddItem(new PlateGorgetIron());
            AddItem(new PlateLegsIron());

            EquipItem(new BowIron() { LootType = LootType.Blessed });

            EquipItem(new HalfApron(37));
            EquipItem(new BodySash(37));
            EquipItem(new Cloak(37));
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosRich);
        }

        public override bool AutoDispel { get { return false; } }
        public override bool BleedImmune { get { return true; } }
        public override int TreasureMapLevel { get { return 2; } }


        public LordArcher(Serial serial)
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