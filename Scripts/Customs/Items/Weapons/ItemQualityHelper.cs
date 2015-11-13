using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Network;
using Server.Items;

namespace Server.Items
{
    public static class ItemQualityHelper
    {

        public static int GetArmorByItemQuality(DamageTypeEnum.ArmorDefenceType pArmorDefenceType, DamageTypeEnum.ArmorType pArmorType, CraftResource pCraftResource)
        {
            double percent = 1.0;


            switch (pCraftResource)
            {

                case CraftResource.Iron:
                    percent = 1.0;
                    break;
                case CraftResource.Rusty:
                    percent = 1.25;
                    break;
                case CraftResource.OldCopper:
                    percent = 1.30;
                    break;
                case CraftResource.DullCopper:
                    percent = 1.35;
                    break;
                case CraftResource.Ruby:
                    percent = 1.40;
                    break;
                case CraftResource.CyclopsLeather:
                case CraftResource.Copper:
                    percent = 1.45;
                    break;
                case CraftResource.Bronze:
                    percent = 1.50;
                    break;
                case CraftResource.ShadowIron:
                    percent = 1.55;
                    break;
                case CraftResource.GargoyleLeather:
                case CraftResource.Silver:
                    percent = 1.60;
                    break;
                case CraftResource.Mercury:
                    percent = 1.65;
                    break;
                case CraftResource.TerathanLeather:
                case CraftResource.Rose:
                    percent = 1.70;
                    break;
                case CraftResource.Gold:
                    percent = 1.75;
                    break;
                case CraftResource.OldEndurium:
                case CraftResource.GoldStone:
                case CraftResource.MaxMytheril:
                case CraftResource.Magma:
                case CraftResource.DaemonLeather:
                case CraftResource.Agapite:
                    percent = 1.80;
                    break;
                case CraftResource.Verite:
                    percent = 1.85;
                    break;
                case CraftResource.Plutonio:
                    percent = 1.90;
                    break;
                case CraftResource.DragonLeather:
                case CraftResource.BloodRock:
                    percent = 1.95;
                    break;
                case CraftResource.Valorite:
                    percent = 2.00;
                    break;
                case CraftResource.BlackRock:
                    percent = 2.05;
                    break;
                case CraftResource.ZZLeather:
                case CraftResource.Mytheril:
                    percent = 2.10;
                    break;
                case CraftResource.DragonGreenLeather:
                case CraftResource.Aqua:
                    percent = 2.15;
                    break;
                case CraftResource.Endurium:
                    percent = 2.25;
                    break;

                default:
                    Console.WriteLine("GetWeaponDamageByItemQuality pItemTypeBase em default");
                    percent = 1.0;
                    break;
            }


            switch (pArmorDefenceType)
            {



                case DamageTypeEnum.ArmorDefenceType.PhysicalResistance:
                    switch (pArmorType)
                    {
                        case DamageTypeEnum.ArmorType.PlateChest:
                            return Convert.ToInt32(5 * percent);
                        case DamageTypeEnum.ArmorType.PlateArms:
                            return Convert.ToInt32(5 * percent);
                        case DamageTypeEnum.ArmorType.PlateCloseHelm:
                            return Convert.ToInt32(3 * percent);
                        case DamageTypeEnum.ArmorType.PlateGloves:
                            return Convert.ToInt32(5 * percent);
                        case DamageTypeEnum.ArmorType.PlateGorget:
                            return Convert.ToInt32(5 * percent);
                        case DamageTypeEnum.ArmorType.PlateLegs:
                            return Convert.ToInt32(5 * percent);
                        case DamageTypeEnum.ArmorType.HeaterShield:
                            return Convert.ToInt32(8 * percent);

                        case DamageTypeEnum.ArmorType.StuddedCyclop:
                            return Convert.ToInt32(2 * percent);
                        case DamageTypeEnum.ArmorType.StuddedGargoyle:
                            return Convert.ToInt32(2 * percent);
                        case DamageTypeEnum.ArmorType.StuddedTerathan:
                            return Convert.ToInt32(2 * percent);
                        case DamageTypeEnum.ArmorType.StuddedDaemon:
                            return Convert.ToInt32(2 * percent);
                        case DamageTypeEnum.ArmorType.StuddedDragon:
                            return Convert.ToInt32(2 * percent);
                        case DamageTypeEnum.ArmorType.StuddedZZ:
                            return Convert.ToInt32(2 * percent);
                        case DamageTypeEnum.ArmorType.StuddedDragonGreen:
                            return Convert.ToInt32(2 * percent);

                        default:
                            Console.WriteLine("GetWeaponDamageByItemQuality pItemTypeBase em default");
                            return 0;
                    }


                case DamageTypeEnum.ArmorDefenceType.ArmorBase:
                    return 10;
                case DamageTypeEnum.ArmorDefenceType.BaseColdResistance:
                case DamageTypeEnum.ArmorDefenceType.BaseEnergyResistance:
                case DamageTypeEnum.ArmorDefenceType.BaseFireResistance:
                case DamageTypeEnum.ArmorDefenceType.BasePoisonResistance:
                    return 0;

                case DamageTypeEnum.ArmorDefenceType.InitMinHits:
                    {
                        if (pArmorType == DamageTypeEnum.ArmorType.StuddedCyclop || pArmorType == DamageTypeEnum.ArmorType.StuddedGargoyle || pArmorType == DamageTypeEnum.ArmorType.StuddedTerathan ||
                            pArmorType == DamageTypeEnum.ArmorType.StuddedDaemon || pArmorType == DamageTypeEnum.ArmorType.StuddedDragon || pArmorType == DamageTypeEnum.ArmorType.StuddedZZ ||
                            pArmorType == DamageTypeEnum.ArmorType.StuddedDragonGreen)
                            return 35;
                        else
                            return 50;
                    }

                case DamageTypeEnum.ArmorDefenceType.InitMaxHits:
                    {
                        if (pArmorType == DamageTypeEnum.ArmorType.StuddedCyclop || pArmorType == DamageTypeEnum.ArmorType.StuddedGargoyle || pArmorType == DamageTypeEnum.ArmorType.StuddedTerathan ||
                            pArmorType == DamageTypeEnum.ArmorType.StuddedDaemon || pArmorType == DamageTypeEnum.ArmorType.StuddedDragon || pArmorType == DamageTypeEnum.ArmorType.StuddedZZ ||
                            pArmorType == DamageTypeEnum.ArmorType.StuddedDragonGreen)
                            return 45;
                        else
                            return 65;
                    }

                default:
                    Console.WriteLine("GetWeaponDamageByItemQuality pItemTypeBase em default " + pArmorDefenceType);
                    return 0;
            }


        }


        public static int GetWeaponDamageByItemQuality(DamageTypeEnum.DamageWeaponType pDamageWeaponType, DamageTypeEnum.DamageType pDamageType, CraftResource pCraftResource)
        {
            double percent = 1.0;

            switch (pCraftResource)
            {

                case CraftResource.Iron:
                    percent = 1.0;
                    break;
                case CraftResource.Rusty:
                    percent = 1.05;
                    break;
                case CraftResource.OldCopper:
                    percent = 1.10;
                    break;
                case CraftResource.DullCopper:
                    percent = 1.15;
                    break;
                case CraftResource.Ruby:
                    percent = 1.20;
                    break;
                case CraftResource.Copper:
                    percent = 1.25;
                    break;
                case CraftResource.Bronze:
                    percent = 1.30;
                    break;
                case CraftResource.ShadowIron:
                    percent = 1.35;
                    break;
                case CraftResource.Silver:
                    percent = 1.40;
                    break;
                case CraftResource.Mercury:
                    percent = 1.45;
                    break;
                case CraftResource.Rose:
                    percent = 1.50;
                    break;
                case CraftResource.Gold:
                    percent = 1.55;
                    break;
                case CraftResource.Agapite:
                    percent = 1.60;
                    break;
                case CraftResource.Verite:
                    percent = 1.65;
                    break;
                case CraftResource.Plutonio:
                    percent = 1.70;
                    break;
                case CraftResource.BloodRock:
                    percent = 1.75;
                    break;
                case CraftResource.Valorite:
                    percent = 1.80;
                    break;
                case CraftResource.BlackRock:
                    percent = 1.85;
                    break;
                case CraftResource.Mytheril:
                    percent = 1.90;
                    break;
                case CraftResource.Aqua:
                    percent = 1.95;
                    break;
                case CraftResource.Endurium:
                    percent = 2.0;
                    break;
                case CraftResource.OldEndurium:
                    percent = 2.05;
                    break;
                case CraftResource.GoldStone:
                    percent = 2.10;
                    break;
                case CraftResource.MaxMytheril:
                    percent = 2.15;
                    break;
                case CraftResource.Magma:
                    percent = 2.20;
                    break;

                case CraftResource.None:
                    break;

                default:
                    Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                    percent = 1.0;
                    break;
            }


            switch (pDamageWeaponType)
            {

                case DamageTypeEnum.DamageWeaponType.VikingSword:
                    switch (pDamageType)
                    {

                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(15 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(17 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(100 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }

                case DamageTypeEnum.DamageWeaponType.WarMace:
                    switch (pDamageType)
                    {
                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(16 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(17 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(110 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }

                case DamageTypeEnum.DamageWeaponType.Kryss:
                    switch (pDamageType)
                    {
                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(10 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(12 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(90 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }

                case DamageTypeEnum.DamageWeaponType.Bow:
                    switch (pDamageType)
                    {

                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(15 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(19 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(60 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }

                case DamageTypeEnum.DamageWeaponType.WarAxe:
                    switch (pDamageType)
                    {

                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(14 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(15 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(80 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }


                case DamageTypeEnum.DamageWeaponType.Bardiche:
                    switch (pDamageType)
                    {
                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(17 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(18 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(100 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }

                case DamageTypeEnum.DamageWeaponType.Crossbow:
                    switch (pDamageType)
                    {
                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(18 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(22 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(80 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }


                case DamageTypeEnum.DamageWeaponType.HeavyCrossbow:
                    switch (pDamageType)
                    {
                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(20 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(24 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(100 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }

                case DamageTypeEnum.DamageWeaponType.Halberd:
                    switch (pDamageType)
                    {
                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(18 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(19 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(80 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }

                case DamageTypeEnum.DamageWeaponType.LargeBattleAxe:
                    switch (pDamageType)
                    {
                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(16 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(17 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(70 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }

                case DamageTypeEnum.DamageWeaponType.ShortSpear:
                    switch (pDamageType)
                    {
                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(10 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(13 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(70 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }

                case DamageTypeEnum.DamageWeaponType.Spear:
                    switch (pDamageType)
                    {
                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(13 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(15 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(80 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }

                case DamageTypeEnum.DamageWeaponType.WarFork:
                    switch (pDamageType)
                    {
                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(12 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(13 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(110 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }

                case DamageTypeEnum.DamageWeaponType.AdvancedPoisonBow:
                    percent = 1.75; // BloodRock
                    switch (pDamageType)
                    {
                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(15 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(19 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(60 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }

                case DamageTypeEnum.DamageWeaponType.ElvenBow:
                    percent = 1.60; // Agapite
                    switch (pDamageType)
                    {
                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(15 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(19 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(60 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }

                case DamageTypeEnum.DamageWeaponType.FireBow:
                    percent = 1.90; // Mytheril
                    switch (pDamageType)
                    {
                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(15 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(19 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(60 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }

                case DamageTypeEnum.DamageWeaponType.PoisonBow:
                    percent = 1.40; // Silver
                    switch (pDamageType)
                    {
                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(15 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(19 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(60 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }

                case DamageTypeEnum.DamageWeaponType.RayBow:
                    percent = 1.40; // Silver
                    switch (pDamageType)
                    {
                        case DamageTypeEnum.DamageType.AosMinDamage:
                            return Convert.ToInt32(15 * percent);
                        case DamageTypeEnum.DamageType.AosMaxDamage:
                            return Convert.ToInt32(19 * percent);
                        case DamageTypeEnum.DamageType.InitMinHits:
                            return Convert.ToInt32(31 * percent);
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            return Convert.ToInt32(60 * percent);
                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 0;
                    }

                default:
                    switch (pDamageType)
                    {
                        case DamageTypeEnum.DamageType.InitMinHits:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 50;
                        case DamageTypeEnum.DamageType.InitMaxHits:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            return 60;

                        default:
                            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                            break;
                    }

                    Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
                    break;
            }

            Console.WriteLine("GetDamageByItemQuality pItemTypeBase em default " + pDamageWeaponType);
            return 0;

        }


    }


    public class DamageTypeEnum
    {
        public enum DamageType
        {
            OldMinDamage,
            OldMaxDamage,
            AosMinDamage,
            AosMaxDamage,
            InitMinHits,
            InitMaxHits
        }

        public enum DamageWeaponType
        {
            VikingSword,
            WarMace,
            Kryss,
            Bow,
            WarAxe,
            Bardiche,
            Crossbow,
            HeavyCrossbow,
            Halberd,
            LargeBattleAxe,
            ShortSpear,
            Spear,
            WarFork,
            PoisonBow,
            ElvenBow,
            RayBow,
            AdvancedPoisonBow,
            FireBow
        }

        public enum ArmorDefenceType
        {
            PhysicalResistance,
            BaseFireResistance,
            BaseColdResistance,
            BasePoisonResistance,
            BaseEnergyResistance,
            ArmorBase,
            InitMinHits,
            InitMaxHits
        }



        public enum ArmorType
        {
            PlateChest,
            PlateArms,
            PlateLegs,
            PlateCloseHelm,
            PlateGorget,
            PlateGloves,
            HeaterShield,
            StuddedCyclop,
            StuddedGargoyle,
            StuddedTerathan,
            StuddedDaemon,
            StuddedDragon,
            StuddedZZ,
            StuddedDragonGreen
        }

    }


}
