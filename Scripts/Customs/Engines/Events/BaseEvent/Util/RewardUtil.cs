using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Factions;
using Server.Commands;
using System.Collections.Generic;

using Server.Mobiles;
using Server.Regions;
using Server.Gumps;
using Server.Items;

namespace DimensionsNewAge.Scripts.Customs.Engines
{
    public static class RewardUtil
    {

        [Description("Mytheril Plate Part")]
        public static Type[] MytherilPlateTypes { get { return m_MytherilPlateTypes; } }
        private static Type[] m_MytherilPlateTypes = new Type[]
			{
				typeof( PlateArmsMytheril ),
                typeof( PlateChestMytheril ),
                typeof( PlateGlovesMytheril ),
				typeof( PlateGorgetMytheril ),
                typeof( PlateCloseHelmMytheril ),
                typeof( PlateLegsMytheril ),
				typeof( HeaterShieldMytheril )
			};

        [Description("BlackRock Plate Part")]
        public static Type[] BlackRockPlateTypes { get { return m_BlackRockPlateTypes; } }
        private static Type[] m_BlackRockPlateTypes = new Type[]
			{
				typeof( PlateArmsBlackRock ),
                typeof( PlateChestBlackRock ),
                typeof( PlateGlovesBlackRock ),
				typeof( PlateGorgetBlackRock ),
                typeof( PlateCloseHelmBlackRock ),
                typeof( PlateLegsBlackRock ),
				typeof( HeaterShieldBlackRock )
			};

        [Description("Mytheril Weapon")]
        public static Type[] MytherilWeaponTypes { get { return m_MytherilWeaponTypes; } }
        private static Type[] m_MytherilWeaponTypes = new Type[]
			{
				typeof( BardicheMytheril ),
                typeof( BowMytheril ),
                typeof( CrossbowMytheril ),
				typeof( HalberdMytheril ),
                typeof( HeavyCrossbowMytheril ),
                typeof( KryssMytheril ),
                typeof( LargeBattleAxeMytheril ),
                typeof( WarMaceMytheril ),
                typeof( ShortSpearMytheril ),
                typeof( SpearMytheril ),
                typeof( SwordMytheril ),
                typeof( WarAxeMytheril ),
                typeof( WarForkMytheril )
			};

        [Description("BlackRock Weapon")]
        public static Type[] BlackRockWeaponTypes { get { return m_BlackRockWeaponTypes; } }
        private static Type[] m_BlackRockWeaponTypes = new Type[]
			{
				typeof( BardicheBlackRock ),
                typeof( BowBlackRock ),
                typeof( CrossbowBlackRock ),
				typeof( HalberdBlackRock ),
                typeof( HeavyCrossbowBlackRock ),
                typeof( KryssBlackRock ),
                typeof( LargeBattleAxeBlackRock ),
                typeof( WarMaceBlackRock ),
                typeof( ShortSpearBlackRock ),
                typeof( SpearBlackRock ),
                typeof( SwordBlackRock ),
                typeof( WarAxeBlackRock ),
                typeof( WarForkBlackRock )
			};

        [Description("Regular Mount")]
        public static Type[] RegularMountTypes { get { return m_RegularMountTypes; } }
        private static Type[] m_RegularMountTypes = new Type[]
			{
				typeof( Mustang ),
                typeof( Orn ),
                typeof( Oclock ),
				typeof( Zostrich )
			};

        [Description("Rare Mount")]
        public static Type[] RareMountTypes { get { return m_RareMountTypes; } }
        private static Type[] m_RareMountTypes = new Type[]
			{
                typeof( OrnRare ),
                typeof( OclockRare ),
				typeof( ZostrichRare )
			};

        [Description("Magic Bracelet")]
        public static Type[] MagicBraceletType { get { return m_MagicBraceletType; } }
        private static Type[] m_MagicBraceletType = new Type[]
			{
                typeof( BraceletAlchemy ),
                typeof( BraceletBlacksmithing ),
				typeof( BraceletMining ),
                typeof( BraceletBowcraft ),
                typeof( BraceletTaming ),
                typeof( BraceletLumberjacking ),
                typeof( BraceletSnooping ),
                typeof( BraceletTailoring ),
                typeof( BraceletPoisoning ),
                typeof( BraceletStealing )
			};

        [Description("Magic Necklace")]
        public static Type[] MagicNecklaceType { get { return m_MagicNecklaceType; } }
        private static Type[] m_MagicNecklaceType = new Type[]
			{
                typeof( NecklaceFencing ),
                typeof( NecklaceMacefighting ),
				typeof( NecklaceSwordsmanship ),
                typeof( NecklaceArchery ),
                typeof( NecklaceTactics ),
                typeof( NecklaceWrestling )
			};

        [Description("Magic Ring")]
        public static Type[] MagicRingType { get { return m_MagicRingType; } }
        private static Type[] m_MagicRingType = new Type[]
			{
                typeof( RingDexterity ),
                typeof( RingStrenght ),
				typeof( RingInteligence ),
                typeof( RingPower )
			};

        public static void SendRewardToPlayer(Mobile player)
        {
            Bag bagReward = new Bag();
            bagReward.Hue = Utility.RandomYellowHue();
            bagReward.Name = "Reward Bag";

            foreach (RewardItem rewardItem in SingletonEvent.Instance.CurrentEventRewardList)
            {

                object item = CreateRewardInstance(rewardItem.RewardTypeList);

                // Ajusta Item

                if (item is Gold)
                {
                    ((Gold)item).Amount = 5000;
                }
                else 
                    if (item is BaseOre || item is BaseIngot)
                    {
                        ((Item)item).Amount = 2;
                    }

                // Send Item

                if (item is Item)
                {
                    bagReward.DropItem((Item)item);
                }
                else 
                    if (item is BaseCreature)
                    {
                        ShrinkItem shrunkenPet = new ShrinkItem((BaseCreature)item);
                        bagReward.DropItem(shrunkenPet);
                    }
            }

            player.SendMessage("Uma Bag de Recompensa foi depositada em seu Banco!");
            player.BankBox.DropItem(bagReward);
        }


        public static object CreateRewardInstance(Type type)
        {
            try
            {
                return Activator.CreateInstance(type);
            }
            catch
            {
                Logger.LogMessage("CreateRewardInstance catch null " + type.Name, "RewardUtil");
                return null;
            }
        }

        public static object CreateRewardInstance(Type[] types)
        {
            if (types.Length > 0)
                return CreateRewardInstance(types, Utility.Random(types.Length));

            Logger.LogMessage("CreateRewardInstance null", "RewardUtil");
            return null;
        }

        public static object CreateRewardInstance(Type[] types, int index)
        {
            if (index >= 0 && index < types.Length)
                return CreateRewardInstance(types[index]);

            Logger.LogMessage("CreateRewardInstance null index: " + index, "RewardUtil");
            return null;
        }

        public static string GetRewardHtmlString()
        {

            string htmlRewardList = string.Empty;

            int qtGold = 0;

            if (SingletonEvent.Instance.IsAutomaticEvent || SingletonEvent.Instance.CurrentEventRewardList.Count == 0)
            {
                htmlRewardList += "10000 Gold<BR>";
                htmlRewardList += "Mustang<BR>";
            }
            else
            {
                foreach (RewardItem rewardItem in SingletonEvent.Instance.CurrentEventRewardList)
                {
                    if (rewardItem.RewardDescription == "Gold")
                    {
                        qtGold += 5000;
                    }
                    else
                    {
                        htmlRewardList += rewardItem.RewardDescription;
                        htmlRewardList += "<BR>";
                    }
                }

                if (qtGold > 0)
                {
                    htmlRewardList += string.Format("{0} Gold", qtGold);
                    htmlRewardList += "<BR>";
                }
            }

            return htmlRewardList;
        }

    }
}
