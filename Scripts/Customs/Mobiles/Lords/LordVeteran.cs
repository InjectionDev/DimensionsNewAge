using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a lord veteran corpse")]
    public class LordVeteranWarrior : BaseCreature
    {
        [Constructable]
        public LordVeteranWarrior()
            : base(AIType.AI_Melee, FightMode.Closest, 12, 1, 0.2, 0.4)
        {
            Name = "Lord Veteran Warrior";
            Body = 0x190;
            BaseSoundID = 0x0;

            SetStr(300, 350);
            SetDex(170, 200);
            SetInt(80, 95);

            SetHits(450, 500);

            SetDamage(35, 40);

            SetDamageType(ResistanceType.Physical, 100);
            //SetDamageType( ResistanceType.Fire, 25 );
            //SetDamageType( ResistanceType.Cold, 25 );
            //SetDamageType( ResistanceType.Energy, 25 );

            SetResistance(ResistanceType.Physical, 25, 35);
            SetResistance(ResistanceType.Fire, 50, 60);
            SetResistance(ResistanceType.Cold, 50, 60);
            SetResistance(ResistanceType.Poison, 50, 60);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.MagicResist, 80.0, 90.0);
            SetSkill(SkillName.Tactics, 80.0, 90.0);
            SetSkill(SkillName.Swords, 80.0, 90.0);
            SetSkill(SkillName.Fencing, 80.0, 90.0);
            SetSkill(SkillName.Macing, 80.0, 90.0);
            SetSkill(SkillName.Wrestling, 80.0, 90.0);

            Fame = 4500;
            Karma = -4500;

            VirtualArmor = 40;

            AddItem(new PlateArmsCopper());
            AddItem(new PlateChestCopper());
            AddItem(new PlateCloseHelmCopper());
            AddItem(new PlateGlovesCopper());
            AddItem(new PlateGorgetCopper());
            AddItem(new PlateLegsCopper());
            EquipItem(new ChaosShield() { LootType = LootType.Blessed });

            switch (Utility.Random(3))
            {
                case 0: EquipItem(new KryssCopper() { LootType = LootType.Blessed }); break;
                case 1: EquipItem(new SwordCopper() { LootType = LootType.Blessed }); break;
                case 2: EquipItem(new WarMaceCopper() { LootType = LootType.Blessed }); break;
            }

            EquipItem(new HalfApron(37));
            EquipItem(new BodySash(37));
            EquipItem(new Cloak(37));

            Mobile mount = new Horse();
            mount.Hue = Utility.RandomRedHue();

            ((IMount)mount).Rider = this;
        }

        public override bool OnBeforeDeath()
        {
            IMount mount = this.Mount;

            if (mount != null)
                mount.Rider = null;

            if (mount is Mobile)
                ((Mobile)mount).Kill();

            return base.OnBeforeDeath();
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosRich);
        }

        public override bool AutoDispel { get { return false; } }
        public override bool BleedImmune { get { return true; } }
        public override int TreasureMapLevel { get { return 2; } }


        public LordVeteranWarrior(Serial serial)
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

    [CorpseName("a lord veteran corpse")]
    public class LordVeteranArcher : BaseCreature
    {
        [Constructable]
        public LordVeteranArcher()
            : base(AIType.AI_Archer, FightMode.Closest, 12, 1, 0.2, 0.4)
        {
            Name = "Lord Veteran Archer";
            Body = 0x190;
            BaseSoundID = 0x0;

            SetStr(300, 350);
            SetDex(170, 200);
            SetInt(80, 95);

            SetHits(450, 500);

            SetDamage(35, 40);

            SetDamageType(ResistanceType.Physical, 100);
            //SetDamageType( ResistanceType.Fire, 25 );
            //SetDamageType( ResistanceType.Cold, 25 );
            //SetDamageType( ResistanceType.Energy, 25 );

            SetResistance(ResistanceType.Physical, 25, 35);
            SetResistance(ResistanceType.Fire, 50, 60);
            SetResistance(ResistanceType.Cold, 50, 60);
            SetResistance(ResistanceType.Poison, 50, 60);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.MagicResist, 60.0, 100.0);
            SetSkill(SkillName.Tactics, 90.0, 100.0);
            SetSkill(SkillName.Archery, 80.0, 100.0);
            SetSkill(SkillName.Wrestling, 80.0, 100.0);

            Fame = 4500;
            Karma = -4500;

            VirtualArmor = 40;

            AddItem(new PlateArmsCopper());
            AddItem(new PlateChestCopper());
            AddItem(new PlateCloseHelmCopper());
            AddItem(new PlateGlovesCopper());
            AddItem(new PlateGorgetCopper());
            AddItem(new PlateLegsCopper());

            EquipItem(new BowCopper() { LootType = LootType.Blessed });

            EquipItem(new HalfApron(37));
            EquipItem(new BodySash(37));
            EquipItem(new Cloak(37));

            Mobile mount = new Horse();
            mount.Hue = Utility.RandomRedHue();


            ((IMount)mount).Rider = this;
        }

        public override bool OnBeforeDeath()
        {
            IMount mount = this.Mount;

            if (mount != null)
                mount.Rider = null;

            if (mount is Mobile)
                ((Mobile)mount).Kill();

            return base.OnBeforeDeath();
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosRich);
        }

        public override bool AutoDispel { get { return false; } }
        public override bool BleedImmune { get { return true; } }
        public override int TreasureMapLevel { get { return 2; } }


        public LordVeteranArcher(Serial serial)
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