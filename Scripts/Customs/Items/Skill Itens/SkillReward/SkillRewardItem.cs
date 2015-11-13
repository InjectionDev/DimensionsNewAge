using System;

namespace Server.Items
{
    public class SkillRewardItem : Item
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public int SkillType { get; set; }
        [CommandProperty(AccessLevel.GameMaster)]
        public int SkillAmount { get; set; }

        [Constructable]
        public SkillRewardItem()
            : this(1)
        {
        }

        [Constructable]
        public SkillRewardItem(int amount, int skillType, int skillAmount)
            : base(0x2258)
        {
            Hue = 789;
            Name = "Pergaminho de Arshen";
            SkillType = skillType;
            SkillAmount = skillAmount;
        }

        [Constructable]
        public SkillRewardItem(int amount)
            : base(0x2258)
        {
            Hue = 789;
            Name = "Pergaminho de Arshen";
            SkillType = (int)SkillRewardItemType.Combat;
            SkillAmount = 3;

        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            switch (SkillType)
            { 
                case (int)SkillRewardItemType.Combat:
                    list.Add(1060661, "Pergaminho\t{0}", "Guerreiro");
                    break;
                case (int)SkillRewardItemType.Bardo:
                    list.Add(1060661, "Pergaminho\t{0}", "Bardo");
                    break;
                case (int)SkillRewardItemType.Tammer:
                    list.Add(1060661, "Pergaminho\t{0}", "Tammer");
                    break;
                case (int)SkillRewardItemType.Work:
                    list.Add(1060661, "Pergaminho\t{0}", "Trabalhador");
                    break;


            }

            list.Add(1060658, "Nivel\t{0}", this.SkillAmount);
            
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.SendGump(new SkillRewardGump(from, this));

            base.OnDoubleClick(from);
        }

        public SkillRewardItem(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version

            writer.Write(SkillType);
            writer.Write(SkillAmount);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        SkillType = reader.ReadInt();
                        SkillAmount = reader.ReadInt();
                        break;
                    }
            }
        }
    }

    public enum SkillRewardItemType
    {
        Combat = 1,
        Work = 2,
        Bardo = 3,
        Tammer = 4
    }
}