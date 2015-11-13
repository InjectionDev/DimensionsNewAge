using System;
using System.Collections;
using System.IO;
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

namespace DimensionsNewAge.Scripts.Customs.Engines.SphereImport
{
    public class SphereImport
    {

        private static string filePath = @"C:\SphereSave\";

        private static string sphereAccName = string.Empty;
        private static string sphereCharName = string.Empty;
        private static string runuoAccName = string.Empty;
        private static string runuoCharName = string.Empty;

        private static string charID = string.Empty;
        private static string bankboxID = string.Empty;
        private static string backpackID = string.Empty;

        private static Mobile caller;
        private static List<string> objectStringList = new List<string>();
        private static List<SphereItemClass> sphereItensListToAcc = new List<SphereItemClass>();
        private static List<string> sphereConianerList = new List<string>();
        private static List<string> sphereMultiList = new List<string>();

        public static void Initialize()
        {
            //CommandSystem.Register("sphereimportitens", AccessLevel.Owner, new CommandEventHandler(SphereImport_OnCommand));

            CommandSystem.Register("sphereimport", AccessLevel.Administrator, new CommandEventHandler(SphereImportGump_OnCommand));

        }

        private static void SphereImportGump_OnCommand(CommandEventArgs e)
        {
            //e.Mobile.SendMessage("Entre com ACC do Sphere");
            //e.Mobile.Prompt = new GenericTextPromp(e.Mobile as PlayerMobile);


            e.Mobile.SendGump(new SphereImportGump(e.Mobile));
        }



        public static void SphereImportItem(Mobile pCaller, string pSphereAcc, string pSphereChar, string pRunUOAcc, string pRunUOChar, bool pImportSkill, bool pImportItensToPlayer, bool pImportItensToStaff)
        {

            caller = pCaller;
            sphereAccName = pSphereAcc;
            sphereCharName = pSphereChar;
            runuoAccName = pRunUOAcc;
            runuoCharName = pRunUOChar;


            Logger.LogSphereImport(string.Format("Procurando ACC RunUO:{0} CHAR:{1}", runuoAccName, runuoCharName), sphereCharName);
            bool existRunuoAcc = false;
            foreach (var mobile in World.Mobiles.Values)
            {
                if (mobile is PlayerMobile && ((PlayerMobile)mobile).RawName == runuoCharName && ((PlayerMobile)mobile).Account.Username == runuoAccName)
                {
                    existRunuoAcc = true;
                    break;
                }
            }

            if (existRunuoAcc == false)
            {
                Logger.LogSphereImport(string.Format("ACC RunUO Nao existe", sphereAccName, sphereCharName), sphereCharName);
                return;
            }

            Logger.LogSphereImport(string.Format("Procurando ACC:{0} CHAR:{1}", sphereAccName, sphereCharName), sphereCharName);
            FindAccount(pImportSkill);

            if (string.IsNullOrEmpty(charID))
            {
                Logger.LogSphereImport("ACC ID Nao localizado!", sphereCharName);
                return;
            }

            Logger.LogSphereImport(string.Format("Procurando Bankbox e Backpack"), sphereCharName);
            FindBankBox();

            if (string.IsNullOrEmpty(backpackID))
            {
                Logger.LogSphereImport("BACKPACK ID Nao localizado!", sphereCharName);
                return;
            }
            if (string.IsNullOrEmpty(bankboxID))
            {
                Logger.LogSphereImport("BANKBOX ID Nao localizado!", sphereCharName);
                return;
            }


            sphereMultiList.Clear();
            sphereConianerList.Clear();
            sphereItensListToAcc.Clear();

            Logger.LogSphereImport(string.Format("Procurando Multis"), sphereCharName);
            FindMuilti();

            Logger.LogSphereImport(string.Format("Procurando Containers"), sphereCharName);
            FindContainers();

            Logger.LogSphereImport(string.Format("Procurando Itens"), sphereCharName);
            FindItens();

            if (pImportItensToPlayer)
                CreateItens(true);

            if (pImportItensToStaff)
                CreateItens(false);
        }

        private static void FindMuilti()
        {
            string[] files = Directory.GetFiles(filePath);

            Logger.LogSphereImport(string.Format("Buscando Multi em {0} arquivos", files.Length), sphereCharName);

            foreach (string file in files)
            {

                try
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        String line;

                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.StartsWith("[") && line.EndsWith("]"))
                            {
                                if (objectStringList.Count > 0)
                                {
                                    FindMultiInList();

                                    objectStringList.Clear();
                                }
                            }

                            objectStringList.Add(line);
                        }
                    }

                }
                catch (Exception e)
                {
                    Logger.LogSphereImport("FindMuilti: Erro ao ler arquivo. " + file + " - " + e.Message, sphereCharName);
                }
            }
        }

        private static void FindMultiInList()
        {
            bool isMulti = false;
            bool isCorrectMulti = false;
            foreach (string line in objectStringList)
            {
                if (line.Contains("[WI i_multi_"))
                {
                    isMulti = true;
                    break;
                }
            }

            if (isMulti == false)
                return;


            foreach (string line in objectStringList)
            {
                if (line.StartsWith("MORE1="))
                {
                    if (line.Replace("MORE1=", "").Trim() == charID.Trim())
                    {
                        isCorrectMulti = true;
                        break;
                    }
                }
            }

            if (isCorrectMulti == false)
                return;

            foreach (string line in objectStringList)
            {
                if (line.StartsWith("SERIAL="))
                {
                    sphereMultiList.Add(line.Replace("SERIAL=", ""));
                    Logger.LogSphereImport(string.Format("Multi localizado! Serial: {0}", line.Replace("SERIAL=", "")), sphereCharName);
                    return;
                }
            }

        }

        private static void CreateItens(bool pImportItensToPlayer)
        {
            
            Bag bagItens = new Bag();
            bagItens.Name = "Itens de " + sphereCharName;
            bagItens.Hue = 1153;

            Bag bagGold = new Bag();
            Bag bagOres = new Bag();
            Bag bagIngots = new Bag();
            Bag bagArmor = new Bag();
            Bag bagWeapon = new Bag();
            Bag bagMount = new Bag();
            Bag bagOther = new Bag();
            Bag bagPotion = new Bag();
            Bag bagRegs = new Bag();
            Bag bagCloth = new Bag();
            Bag bagContainer = new Bag();
            Bag bagMaps = new Bag();

            Bag bagPlateOutras = new Bag();
            bagPlateOutras.Hue = DimensionsNewAge.Scripts.HueOreConst.HueIron;
            bagArmor.DropItem(bagPlateOutras);

            Bag bagPlateRusty = new Bag();
            bagPlateRusty.Hue = DimensionsNewAge.Scripts.HueOreConst.HueRusty;
            bagArmor.DropItem(bagPlateRusty);

            Bag bagPlateOldCopper = new Bag();
            bagArmor.DropItem(bagPlateOldCopper);
            bagPlateOldCopper.Hue = DimensionsNewAge.Scripts.HueOreConst.HueOldCopper;

            Bag bagPlateDullCopper = new Bag();
            bagArmor.DropItem(bagPlateDullCopper);
            bagPlateDullCopper.Hue = DimensionsNewAge.Scripts.HueOreConst.HueDullCopper;

            Bag bagPlateRuby = new Bag();
            bagPlateRuby.Hue = DimensionsNewAge.Scripts.HueOreConst.HueRuby;
            bagArmor.DropItem(bagPlateRuby);

            Bag bagPlateCopper = new Bag();
            bagPlateCopper.Hue = DimensionsNewAge.Scripts.HueOreConst.HueCopper;
            bagArmor.DropItem(bagPlateCopper);

            Bag bagPlateBronze = new Bag();
            bagPlateBronze.Hue = DimensionsNewAge.Scripts.HueOreConst.HueBronze;
            bagArmor.DropItem(bagPlateBronze);

            Bag bagPlateShadowIron = new Bag();
            bagPlateShadowIron.Hue = DimensionsNewAge.Scripts.HueOreConst.HueShadow;
            bagArmor.DropItem(bagPlateShadowIron);

            Bag bagPlateSilver = new Bag();
            bagPlateSilver.Hue = DimensionsNewAge.Scripts.HueOreConst.HueSilver;
            bagArmor.DropItem(bagPlateSilver);

            Bag bagPlateMercury = new Bag();
            bagPlateMercury.Hue = DimensionsNewAge.Scripts.HueOreConst.HueMercury;
            bagArmor.DropItem(bagPlateMercury);

            Bag bagPlateRose = new Bag();
            bagPlateRose.Hue = DimensionsNewAge.Scripts.HueOreConst.HueRose;
            bagArmor.DropItem(bagPlateRose);

            Bag bagPlateGold = new Bag();
            bagPlateGold.Hue = DimensionsNewAge.Scripts.HueOreConst.HueGold;
            bagArmor.DropItem(bagPlateGold);

            Bag bagPlateAgapite = new Bag();
            bagPlateAgapite.Hue = DimensionsNewAge.Scripts.HueOreConst.HueAgapite;
            bagArmor.DropItem(bagPlateAgapite);

            Bag bagPlateVerite = new Bag();
            bagPlateVerite.Hue = DimensionsNewAge.Scripts.HueOreConst.HueVerite;
            bagArmor.DropItem(bagPlateVerite);

            Bag bagPlatePlutonio = new Bag();
            bagPlatePlutonio.Hue = DimensionsNewAge.Scripts.HueOreConst.HuePlutonio;
            bagArmor.DropItem(bagPlatePlutonio);

            Bag bagPlateBloodRock = new Bag();
            bagPlateBloodRock.Hue = DimensionsNewAge.Scripts.HueOreConst.HueBloodRock;
            bagArmor.DropItem(bagPlateBloodRock);

            Bag bagPlateValorite = new Bag();
            bagPlateValorite.Hue = DimensionsNewAge.Scripts.HueOreConst.HueValorite;
            bagArmor.DropItem(bagPlateValorite);

            Bag bagPlateBlackRock = new Bag();
            bagPlateBlackRock.Hue = DimensionsNewAge.Scripts.HueOreConst.HueBlackRock;
            bagArmor.DropItem(bagPlateBlackRock);

            Bag bagPlateMytheril = new Bag();
            bagPlateMytheril.Hue = DimensionsNewAge.Scripts.HueOreConst.HueAqua;
            bagArmor.DropItem(bagPlateMytheril);

            Bag bagPlateAqua = new Bag();
            bagPlateAqua.Hue = DimensionsNewAge.Scripts.HueOreConst.HueAqua;
            bagArmor.DropItem(bagPlateAqua);


            bagItens.DropItem(bagOres);
            bagItens.DropItem(bagIngots);
            bagItens.DropItem(bagArmor);
            bagItens.DropItem(bagWeapon);
            bagItens.DropItem(bagMount);
            bagItens.DropItem(bagOther);
            bagItens.DropItem(bagGold);
            bagItens.DropItem(bagPotion);
            bagItens.DropItem(bagRegs);
            bagItens.DropItem(bagCloth);
            bagItens.DropItem(bagContainer);
            bagItens.DropItem(bagMaps);

            Spellbook book = new Spellbook();
            book.Content = ulong.MaxValue;
            bagItens.DropItem(book);
            bagItens.DropItem(new Runebook());
            bagItens.DropItem(new Runebook());


            if (pImportItensToPlayer)
            {
                foreach (Mobile mobile in World.Mobiles.Values)
                {
                    if (mobile is PlayerMobile && mobile.Account.Username == runuoAccName && mobile.Name == runuoCharName)
                    {
                        mobile.BankBox.DropItem(bagItens);
                        break;
                    }
                }
            }
            else
            {
                caller.Backpack.DropItem(bagItens);
            }

            foreach (SphereItemClass sphereItem in sphereItensListToAcc)
            {

                try
                {
                    object item;

                    if (sphereItem.itemType == typeof(TreasureMap))
                        item = new TreasureMap(new Random().Next(1, 3), Map.Felucca);
                    else
                        item = RewardUtil.CreateRewardInstance(sphereItem.itemType);


                    if (item is Item)
                    {
                        if (((Item)item).Stackable)
                        {
                            ((Item)item).Amount = sphereItem.qtAmount;
                        }
                    }

                    if (item is BaseOre)
                    {
                        bagOres.DropItem((Item)item);
                    }
                    else if (item is Gold)
                    {
                        bagGold.DropItem((Item)item);
                    }
                    else if (item is BaseIngot)
                    {
                        bagIngots.DropItem((Item)item);
                    }
                    else if (item is BaseWeapon)
                    {
                        bagWeapon.DropItem((Item)item);
                    }
                    else if (item is BaseReagent)
                    {
                        bagRegs.DropItem((Item)item);
                    }
                    else if (item is LockableContainer)
                    { 
                        bagContainer.DropItem((Item)item);
                    }
                    else if (item is TreasureMap)
                    {
                        bagMaps.DropItem((Item)item);
                    }
                    else if (item is BaseArmor)
                    {


                        bagPlateOutras.DropItem((Item)item);


                        if (item is PlateChestRusty
                            || item is PlateArmsRusty
                            || item is PlateLegsRusty
                            || item is PlateCloseHelmRusty
                            || item is PlateGorgetRusty
                            || item is PlateGlovesRusty
                            || item is SwordRusty
                            || item is WarMaceRusty
                            || item is KryssRusty
                            || item is WarMaceRusty
                            || item is HeaterShieldRusty
                            || item is BowRusty)
                        {
                            bagPlateRusty.DropItem((Item)item);
                        }


                        if (item is PlateChestOldCopper
                            || item is PlateArmsOldCopper
                            || item is PlateLegsOldCopper
                            || item is PlateCloseHelmOldCopper
                            || item is PlateGorgetOldCopper
                            || item is PlateGlovesOldCopper
                            || item is SwordOldCopper
                            || item is WarMaceOldCopper
                            || item is KryssOldCopper
                            || item is WarMaceOldCopper
                            || item is HeaterShieldOldCopper
                            || item is BowOldCopper)
                        {
                            bagPlateOldCopper.DropItem((Item)item);
                        }

                        if (item is PlateChestDullCopper
                            || item is PlateArmsDullCopper
                            || item is PlateLegsDullCopper
                            || item is PlateCloseHelmDullCopper
                            || item is PlateGorgetDullCopper
                            || item is PlateGlovesDullCopper
                            || item is SwordDullCopper
                            || item is WarMaceDullCopper
                            || item is KryssDullCopper
                            || item is WarMaceDullCopper
                            || item is HeaterShieldDullCopper
                            || item is BowDullCopper)
                        {
                            bagPlateDullCopper.DropItem((Item)item);
                        }


                        if (item is PlateChestRuby
                            || item is PlateArmsRuby
                            || item is PlateLegsRuby
                            || item is PlateCloseHelmRuby
                            || item is PlateGorgetRuby
                            || item is PlateGlovesRuby
                            || item is SwordRuby
                            || item is WarMaceRuby
                            || item is KryssRuby
                            || item is WarMaceRuby
                            || item is HeaterShieldRuby
                            || item is BowRuby)
                        {
                            bagPlateRuby.DropItem((Item)item);
                        }

                        if (item is PlateChestCopper
                            || item is PlateArmsCopper
                            || item is PlateLegsCopper
                            || item is PlateCloseHelmCopper
                            || item is PlateGorgetCopper
                            || item is PlateGlovesCopper
                            || item is SwordCopper
                            || item is WarMaceCopper
                            || item is KryssCopper
                            || item is WarMaceCopper
                            || item is HeaterShieldCopper
                            || item is BowCopper)
                        {
                            bagPlateCopper.DropItem((Item)item);
                        }

                        if (item is PlateChestBronze
                            || item is PlateArmsBronze
                            || item is PlateLegsBronze
                            || item is PlateCloseHelmBronze
                            || item is PlateGorgetBronze
                            || item is PlateGlovesBronze
                            || item is SwordBronze
                            || item is WarMaceBronze
                            || item is KryssBronze
                            || item is WarMaceBronze
                            || item is HeaterShieldBronze
                            || item is BowBronze)
                        {
                            bagPlateBronze.DropItem((Item)item);
                        }


                        if (item is PlateChestShadow
                            || item is PlateArmsShadow
                            || item is PlateLegsShadow
                            || item is PlateCloseHelmShadow
                            || item is PlateGorgetShadow
                            || item is PlateGlovesShadow
                            || item is SwordShadow
                            || item is WarMaceShadow
                            || item is KryssShadow
                            || item is WarMaceShadow
                            || item is HeaterShieldShadow
                            || item is BowShadow)
                        {
                            bagPlateShadowIron.DropItem((Item)item);
                        }


                        if (item is PlateChestSilver
                            || item is PlateArmsSilver
                            || item is PlateLegsSilver
                            || item is PlateCloseHelmSilver
                            || item is PlateGorgetSilver
                            || item is PlateGlovesSilver
                            || item is SwordSilver
                            || item is WarMaceSilver
                            || item is KryssSilver
                            || item is WarMaceSilver
                            || item is HeaterShieldSilver
                            || item is BowSilver)
                        {
                            bagPlateSilver.DropItem((Item)item);
                        }


                        if (item is PlateChestMercury
                            || item is PlateArmsMercury
                            || item is PlateLegsMercury
                            || item is PlateCloseHelmMercury
                            || item is PlateGorgetMercury
                            || item is PlateGlovesMercury
                            || item is SwordMercury
                            || item is WarMaceMercury
                            || item is KryssMercury
                            || item is WarMaceMercury
                            || item is HeaterShieldMercury
                            || item is BowMercury)
                        {
                            bagPlateMercury.DropItem((Item)item);
                        }


                        if (item is PlateChestRose
                            || item is PlateArmsRose
                            || item is PlateLegsRose
                            || item is PlateCloseHelmRose
                            || item is PlateGorgetRose
                            || item is PlateGlovesRose
                            || item is SwordRose
                            || item is WarMaceRose
                            || item is KryssRose
                            || item is WarMaceRose
                            || item is HeaterShieldRose
                            || item is BowRose)
                        {
                            bagPlateRose.DropItem((Item)item);
                        }


                        if (item is PlateChestGold
                            || item is PlateArmsGold
                            || item is PlateLegsGold
                            || item is PlateCloseHelmGold
                            || item is PlateGorgetGold
                            || item is PlateGlovesGold
                            || item is SwordGold
                            || item is WarMaceGold
                            || item is KryssGold
                            || item is WarMaceGold
                            || item is HeaterShieldGold
                            || item is BowGold)
                        {
                            bagPlateGold.DropItem((Item)item);
                        }


                        if (item is PlateChestAgapite
                            || item is PlateArmsAgapite
                            || item is PlateLegsAgapite
                            || item is PlateCloseHelmAgapite
                            || item is PlateGorgetAgapite
                            || item is PlateGlovesAgapite
                            || item is SwordAgapite
                            || item is WarMaceAgapite
                            || item is KryssAgapite
                            || item is WarMaceAgapite
                            || item is HeaterShieldAgapite
                            || item is BowAgapite)
                        {
                            bagPlateAgapite.DropItem((Item)item);
                        }


                        if (item is PlateChestVerite
                            || item is PlateArmsVerite
                            || item is PlateLegsVerite
                            || item is PlateCloseHelmVerite
                            || item is PlateGorgetVerite
                            || item is PlateGlovesVerite
                            || item is SwordVerite
                            || item is WarMaceVerite
                            || item is KryssVerite
                            || item is WarMaceVerite
                            || item is HeaterShieldVerite
                            || item is BowVerite)
                        {
                            bagPlateVerite.DropItem((Item)item);
                        }


                        if (item is PlateChestPlutonio
                            || item is PlateArmsPlutonio
                            || item is PlateLegsPlutonio
                            || item is PlateCloseHelmPlutonio
                            || item is PlateGorgetPlutonio
                            || item is PlateGlovesPlutonio
                            || item is SwordPlutonio
                            || item is WarMacePlutonio
                            || item is KryssPlutonio
                            || item is WarMacePlutonio
                            || item is HeaterShieldPlutonio
                            || item is BowPlutonio)
                        {
                            bagPlatePlutonio.DropItem((Item)item);
                        }


                        if (item is PlateChestBloodRock
                            || item is PlateArmsBloodRock
                            || item is PlateLegsBloodRock
                            || item is PlateCloseHelmBloodRock
                            || item is PlateGorgetBloodRock
                            || item is PlateGlovesBloodRock
                            || item is SwordBloodRock
                            || item is WarMaceBloodRock
                            || item is KryssBloodRock
                            || item is WarMaceBloodRock
                            || item is HeaterShieldBloodRock
                            || item is BowBloodRock)
                        {
                            bagPlateBloodRock.DropItem((Item)item);
                        }




                        if (item is PlateChestValorite
                            || item is PlateArmsValorite
                            || item is PlateLegsValorite
                            || item is PlateCloseHelmValorite
                            || item is PlateGorgetValorite
                            || item is PlateGlovesValorite
                            || item is SwordValorite
                            || item is WarMaceValorite
                            || item is KryssValorite
                            || item is WarMaceValorite
                            || item is HeaterShieldValorite
                            || item is BowValorite)
                        {
                            bagPlateValorite.DropItem((Item)item);
                        }


                        if (item is PlateChestBlackRock
                            || item is PlateArmsBlackRock
                            || item is PlateLegsBlackRock
                            || item is PlateCloseHelmBlackRock
                            || item is PlateGorgetBlackRock
                            || item is PlateGlovesBlackRock
                            || item is SwordBlackRock
                            || item is WarMaceBlackRock
                            || item is KryssBlackRock
                            || item is WarMaceBlackRock
                            || item is HeaterShieldBlackRock
                            || item is BowBlackRock)
                        {
                            bagPlateBlackRock.DropItem((Item)item);
                        }


                        if (item is PlateChestMytheril
                            || item is PlateArmsMytheril
                            || item is PlateLegsMytheril
                            || item is PlateCloseHelmMytheril
                            || item is PlateGorgetMytheril
                            || item is PlateGlovesMytheril
                            || item is SwordMytheril
                            || item is WarMaceMytheril
                            || item is KryssMytheril
                            || item is WarMaceMytheril
                            || item is HeaterShieldMytheril
                            || item is BowMytheril)
                        {
                            bagPlateMytheril.DropItem((Item)item);
                        }


                        if (item is PlateChestAqua
                            || item is PlateArmsAqua
                            || item is PlateLegsAqua
                            || item is PlateCloseHelmAqua
                            || item is PlateGorgetAqua
                            || item is PlateGlovesAqua
                            || item is SwordAqua
                            || item is WarMaceAqua
                            || item is KryssAqua
                            || item is WarMaceAqua
                            || item is HeaterShieldAqua
                            || item is BowAqua)
                        {
                            bagPlateAqua.DropItem((Item)item);
                        }


                    }
                    else if (item is BaseClothing)
                    {
                        bagCloth.DropItem((Item)item);
                    }
                    else if (item is BasePotion)
                    {
                        //for (int i = 0; i <= sphereItem.qtAmount - 1; i++)
                        //{
                        //    object itemPotion = RewardUtil.CreateRewardInstance(sphereItem.itemType);
                        //    bagPotion.DropItem((Item)itemPotion);
                        //}

                        bagPotion.DropItem((Item)item);
                    }
                    else if (item is BaseCreature)
                    {
                        if (sphereItem.Hue != 1)
                        {
                            ((BaseCreature)item).Hue = sphereItem.Hue;
                        }

                        ShrinkItem shrunkenPet = new ShrinkItem((BaseCreature)item);
                        bagMount.DropItem(shrunkenPet);
                    }
                    else
                    {
                        bagOther.DropItem((Item)item);
                    }


                    if (item is Server.Mobiles.Horse && sphereItem.Hue != 1)
                    {
                        ((Server.Mobiles.Horse)item).Hue = DimensionsNewAge.Scripts.HueItemConst.GetNewHueBySphereHue(sphereItem.Hue);
                        ((Server.Mobiles.Horse)item).Name = "Wild Horse";
                    }

                    if (item is Server.Items.MagicDyeTub && sphereItem.Hue != 1)
                        ((Server.Items.MagicDyeTub)item).DyedHue = DimensionsNewAge.Scripts.HueItemConst.GetNewHueBySphereHue(sphereItem.Hue);

                    if (item is Server.Items.BaseClothing && sphereItem.Hue != 1)
                        ((Server.Items.BaseClothing)item).Hue = DimensionsNewAge.Scripts.HueItemConst.GetNewHueBySphereHue(sphereItem.Hue);

                }
                catch (Exception ex)
                {
                    Logger.LogSphereImport(string.Format("CreateItens ERRO " + ex.Message), sphereCharName);
                }
            }

            if (bagOres.Items.Count == 0)
                bagOres.Delete();
            if (bagIngots.Items.Count == 0)
                bagIngots.Delete();
            if (bagArmor.Items.Count == 0)
                bagArmor.Delete();
            if (bagWeapon.Items.Count == 0)
                bagWeapon.Delete();
            if (bagMount.Items.Count == 0)
                bagMount.Delete();
            if (bagOther.Items.Count == 0)
                bagOther.Delete();
            if (bagGold.Items.Count == 0)
                bagGold.Delete();
            if (bagPotion.Items.Count == 0)
                bagPotion.Delete();
            if (bagRegs.Items.Count == 0)
                bagRegs.Delete();
            if (bagCloth.Items.Count == 0)
                bagCloth.Delete();
            if (bagContainer.Items.Count == 0)
                bagContainer.Delete();
            if (bagMaps.Items.Count == 0)
                bagMaps.Delete();



            if (bagPlateOutras.Items.Count == 0)
                bagPlateOutras.Delete();
            if (bagPlateRusty.Items.Count == 0)
                bagPlateRusty.Delete();
            if (bagPlateOldCopper.Items.Count == 0)
                bagPlateOldCopper.Delete();
            if (bagPlateDullCopper.Items.Count == 0)
                bagPlateDullCopper.Delete();
            if (bagPlateRuby.Items.Count == 0)
                bagPlateRuby.Delete();
            if (bagPlateCopper.Items.Count == 0)
                bagPlateCopper.Delete();
            if (bagPlateBronze.Items.Count == 0)
                bagPlateBronze.Delete();
            if (bagPlateShadowIron.Items.Count == 0)
                bagPlateShadowIron.Delete();
            if (bagPlateSilver.Items.Count == 0)
                bagPlateSilver.Delete();
            if (bagPlateMercury.Items.Count == 0)
                bagPlateMercury.Delete();
            if (bagPlateRose.Items.Count == 0)
                bagPlateRose.Delete();
            if (bagPlateGold.Items.Count == 0)
                bagPlateGold.Delete();
            if (bagPlateAgapite.Items.Count == 0)
                bagPlateAgapite.Delete();
            if (bagPlateVerite.Items.Count == 0)
                bagPlateVerite.Delete();
            if (bagPlatePlutonio.Items.Count == 0)
                bagPlatePlutonio.Delete();
            if (bagPlateBloodRock.Items.Count == 0)
                bagPlateBloodRock.Delete();
            if (bagPlateValorite.Items.Count == 0)
                bagPlateValorite.Delete();
            if (bagPlateBlackRock.Items.Count == 0)
                bagPlateBlackRock.Delete();
            if (bagPlateMytheril.Items.Count == 0)
                bagPlateMytheril.Delete();
            if (bagPlateAqua.Items.Count == 0)
                bagPlateAqua.Delete();
        }



        private static bool FindBankInList()
        {
            bool isBank = false;
            bool isCorrectBank = false;
            foreach (string line in objectStringList)
            {
                if (line.Contains("i_bankbox"))
                {
                    isBank = true;
                    break;
                }
            }

            if (isBank == false)
                return false;


            foreach (string line in objectStringList)
            {
                if (line.StartsWith("CONT="))
                {
                    if (line.Replace("CONT=", "").Trim() == charID.Trim())
                    {
                        isCorrectBank = true;
                        break;
                    }
                }
            }

            if (isCorrectBank == false)
                return false;

            foreach (string line in objectStringList)
            {
                if (line.StartsWith("SERIAL="))
                {
                    bankboxID = line.Replace("SERIAL=", "");
                    Logger.LogSphereImport(string.Format("BankBox localizada! Serial: {0}", bankboxID), sphereCharName);
                    return true;
                }
            }

            return false;
        }


        private static bool FindBackPackInList()
        {
            bool isBackPack = false;
            bool isCorrectBackPack = false;
            foreach (string line in objectStringList)
            {
                if (line.Contains("i_backpack"))
                {
                    isBackPack = true;
                    break;
                }
            }

            if (isBackPack == false)
                return false;


            foreach (string line in objectStringList)
            {
                if (line.StartsWith("CONT="))
                {
                    if (line.Replace("CONT=", "").Trim() == charID.Trim())
                    {
                        isCorrectBackPack = true;
                        break;
                    }
                }
            }

            if (isCorrectBackPack == false)
                return false;

            foreach (string line in objectStringList)
            {
                if (line.StartsWith("SERIAL="))
                {
                    backpackID = line.Replace("SERIAL=", "");
                    Logger.LogSphereImport(string.Format("BackPack localizada! Serial: {0}", backpackID), sphereCharName);
                    return true;
                }
            }

            return false;
        }


        private static void FindContainerInList()
        {
            bool isContainer = false;
            bool isCorrectContainer = false;
            SphereItemClass currentSphereItem = null;

            string debug = "1";

            try
            {

                foreach (string line in objectStringList)
                {
                    if (line.StartsWith("[WI "))
                    {
                        string container = line.ToLower().Replace("[wi", "").Replace("]", "").Trim();
                        if (SphereItens.SphereCommomContainersList.Contains(container))
                        {
                            isContainer = true;
                            break;
                        }
                    }
                }
                debug = "2";
                if (isContainer == false)
                    return;

                foreach (string line in objectStringList)
                {

                    if (line.StartsWith("CONT="))
                    {
                        if (line.Replace("CONT=", "").Trim() == charID.Trim() ||
                            line.Replace("CONT=", "").Trim() == backpackID.Trim() ||
                            line.Replace("CONT=", "").Trim() == bankboxID.Trim() ||
                            sphereConianerList.Contains(line.Replace("CONT=", "").Trim()))
                        {
                            isCorrectContainer = true;
                            break;
                        }
                    }

                    // Multi
                    if (line.StartsWith("LINK="))
                    {
                        if (sphereMultiList.Contains(  line.Replace("LINK=", "").Trim()))
                        {
                            isCorrectContainer = true;
                            break;
                        }
                    }
                }
                debug = "3";
                if (isCorrectContainer == false)
                    return;

                foreach (string line in objectStringList)
                {
                    if (line.StartsWith("SERIAL=") && sphereConianerList.Contains(line.Replace("SERIAL=", "").Trim()) == false)
                    {
                        hasFindNewContainer = true;
                        sphereConianerList.Add(line.Replace("SERIAL=", "").Trim());
                        Logger.LogSphereImport(string.Format("Containers localizado Serial:{0}", line.Replace("SERIAL=", "")), sphereCharName);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogSphereImport("FindContainerInList: Erro ao ler arquivo. - " + e.Message, sphereCharName);
            }
        }



        private static void FindItensInList()
        {
            bool isItem = false;
            bool isCorrectItem = false;
            SphereItemClass currentSphereItem = null;

            string debug = "";

            try
            {
                foreach (string line in objectStringList)
                {
                    debug = line;
                    if (line.StartsWith("[WI "))
                    {
                        isItem = true;

                        if (line.Contains("i_memory"))
                            isItem = false;

                        


                        break;
                    }
                }

                if (isItem == false)
                    return;

                foreach (string line in objectStringList)
                {
                    debug = line;
                    if (line.StartsWith("CONT="))
                    {
                        if (line.Replace("CONT=", "").Trim() == charID.Trim() ||
                            line.Replace("CONT=", "").Trim() == backpackID.Trim() ||
                            line.Replace("CONT=", "").Trim() == bankboxID.Trim() ||
                            sphereConianerList.Contains(line.Replace("CONT=", "").Trim()) ||
                            sphereMultiList.Contains(line.Replace("CONT=", "").Trim()))
                        {
                            isCorrectItem = true;
                            break;
                        }
                    }


                    // Multi
                    if (line.StartsWith("LINK="))
                    {
                        if (sphereMultiList.Contains(line.Replace("LINK=", "").Trim()))
                        {
                            isCorrectItem = true;
                            break;
                        }
                    }
                }

                foreach (string line in objectStringList)
                {
                    debug = line;
                    if (line.StartsWith("CONT="))
                    {
                        if (line.Replace("CONT=", "").Trim() == charID.Trim() ||
                            line.Replace("CONT=", "").Trim() == backpackID.Trim() ||
                            line.Replace("CONT=", "").Trim() == bankboxID.Trim() ||
                            sphereConianerList.Contains(line.Replace("CONT=", "").Trim()))
                        {
                            isCorrectItem = true;
                            break;
                        }
                    }
                }

                if (isCorrectItem == false)
                    return;

                foreach (string line in objectStringList)
                {
                    if (line.StartsWith("[WI "))
                    {
                        debug = line;
                        foreach (SphereItemClass sphereItem in SphereItens.SphereItensList)
                        {
                            if (line.ToLower().Contains(sphereItem.sphereID.ToLower()))
                            {
                                currentSphereItem = sphereItem;
                                break;
                            }
                        }

                        if (currentSphereItem == null)
                        {
                            Logger.LogSphereImport(string.Format("NAO MAPEADO -> Item:{0}.", line.Replace("[WI", "").Replace("]", "").Trim()), sphereCharName);
                            return;
                        }
                    }
                }

                foreach (string line in objectStringList)
                {
                    debug = line;
                    if (line.StartsWith("AMOUNT="))
                    {
                        currentSphereItem.qtAmount = Convert.ToInt32(line.Replace("AMOUNT=", "").Trim());
                    }
                }

                foreach (string line in objectStringList)
                {
                    debug = line;
                    if (line.StartsWith("COLOR="))
                    {
                        currentSphereItem.Hue = int.Parse(line.Replace("COLOR=", "").Trim(), System.Globalization.NumberStyles.HexNumber);
                    }
                }

                foreach (string line in objectStringList)
                {
                    debug = line;
                    if (line.StartsWith("SERIAL="))
                    {
                        currentSphereItem.sphereSerial = line.Replace("SERIAL=", "").Trim();
                    }
                }

                if (currentSphereItem.sphereID == "i_bandage" && currentSphereItem.qtAmount == 1)
                {
                    currentSphereItem = null;
                    return;
                }

                if (currentSphereItem != null)
                    Logger.LogSphereImport(string.Format("Item:{0}. Qtd:{1}. Type:{2}. Hue:{3} ({4})", currentSphereItem.sphereID, currentSphereItem.qtAmount, currentSphereItem.itemType.Name, currentSphereItem.Hue, currentSphereItem.sphereSerial), sphereCharName);

                sphereItensListToAcc.Add(currentSphereItem);
            }
            catch (Exception e)
            {
                Logger.LogSphereImport("FindItensInList: Erro ao ler arquivo. - " + debug + " - " + e.Message, sphereCharName);
            }
        }

        private static bool hasFindNewContainer = false;
        private static void FindContainers()
        {
            string[] files = Directory.GetFiles(filePath);

            string debug = "1";

            foreach (string file in files)
            {

                try
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        String line;

                        while ((line = sr.ReadLine()) != null)
                        {

                            debug = line;
                            if (line.StartsWith("[") && line.EndsWith("]"))
                            {
                                if (objectStringList.Count > 0)
                                {
                                    FindContainerInList();

                                    objectStringList.Clear();
                                }
                            }

                            objectStringList.Add(line);
                        }
                    }

                }
                catch (Exception e)
                {
                    Logger.LogSphereImport("FindContainers: Erro ao ler arquivo. " + debug + " - " + e.Message, sphereCharName);
                }
            }

            if (hasFindNewContainer)
            {
                hasFindNewContainer = false;
                FindContainers();
            }

        }


        private static void FindItens()
        {
            string[] files = Directory.GetFiles(filePath);

            foreach (string file in files)
            {

                try
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        String line;

                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.StartsWith("[") && line.EndsWith("]"))
                            {
                                if (objectStringList.Count > 0)
                                {
                                    FindItensInList();

                                    objectStringList.Clear();
                                }
                            }

                            objectStringList.Add(line);
                        }
                    }

                }
                catch (Exception e)
                {
                    Logger.LogSphereImport("FindItens: Erro ao ler arquivo. " + file + " - " + e.Message, sphereCharName);
                }
            }

        }


        private static void FindBankBox()
        {
            string[] files = Directory.GetFiles(filePath);

            Logger.LogSphereImport(string.Format("Buscando BankBox em {0} arquivos", files.Length), sphereCharName);

            foreach (string file in files)
            {

                try
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        String line;

                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.StartsWith("[") && line.EndsWith("]"))
                            {
                                if (objectStringList.Count > 0)
                                {
                                    FindBankInList();
                                    FindBackPackInList();

                                    objectStringList.Clear();
                                }
                            }

                            objectStringList.Add(line);
                        }
                    }

                }
                catch (Exception e)
                {
                    Logger.LogSphereImport("FindBankBox: Erro ao ler arquivo. " + file + " - " + e.Message, sphereCharName);
                }
            }

        }


        private static void FindAccount(bool pImportSkills)
        {

            charID = null;
            string file = filePath + "spherechars.scp";

            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    String line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.StartsWith("[") && line.EndsWith("]"))
                        {
                            if (objectStringList.Count > 0)
                            {
                                if (FindAccountInList())
                                {
                                    if (pImportSkills)
                                        SetSkill();

                                    objectStringList.Clear();
                                    return;
                                }

                                objectStringList.Clear();
                            }
                        }

                        objectStringList.Add(line);
                    }
                }

                if (charID == null)
                {
                    Logger.LogSphereImport("FindAccount: NAO LOCALIZOU CONTA. ", sphereCharName);
                    return;
                }

            }
            catch (Exception e)
            {
                Logger.LogSphereImport("FindAccount: Erro ao ler arquivo. " + file + " - " + e.Message, sphereCharName);
            }


        }

        private static void SetSkill()
        {
            Logger.LogSphereImport("SetSkill " + sphereCharName, sphereCharName);

            Mobile destMobile = null;
            foreach (Mobile mobile in World.Mobiles.Values)
            {
                if (mobile is PlayerMobile && mobile.Account.Username == runuoAccName && mobile.Name == runuoCharName)
                {
                    destMobile = mobile;
                    break;
                }
            }

            if (destMobile == null)
            {
                Logger.LogSphereImport("SetSkill Conta Inexistente", sphereCharName);
                return;
            }

            for (int i = 0; i < destMobile.Skills.Length; ++i)
                destMobile.Skills[i].Base = 0;

            foreach (string line in objectStringList)
            {
                //Logger.LogSphereImport("SetSkill -> " + line, charName);

                if (line.StartsWith("OSTR=") && destMobile.Str < Convert.ToInt32(line.Replace("OSTR=", "")))
                {
                    Logger.LogSphereImport("Str -> " + line.Replace("OSTR=", ""), sphereCharName);
                    destMobile.Str = Convert.ToInt32(line.Replace("OSTR=", ""));
                }
                if (line.StartsWith("OINT=") && destMobile.Int < Convert.ToInt32(line.Replace("OINT=", "")))
                {
                    Logger.LogSphereImport("Int -> " + line.Replace("OINT=", ""), sphereCharName);
                    destMobile.Int = Convert.ToInt32(line.Replace("OINT=", ""));
                }
                if (line.StartsWith("ODEX=") && destMobile.Dex < Convert.ToInt32(line.Replace("ODEX=", "")))
                                    {
                    Logger.LogSphereImport("Dex -> " + line.Replace("ODEX=", ""), sphereCharName);
                    destMobile.Dex = Convert.ToInt32(line.Replace("ODEX=", ""));
                                    }


                if (line.StartsWith("Alchemy=") && destMobile.Skills.Alchemy.Base < Convert.ToInt32(line.Replace("Alchemy=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Alchemy -> " + line.Replace("Alchemy=", ""), sphereCharName);
                                        destMobile.Skills.Alchemy.Base = Convert.ToInt32(line.Replace("Alchemy=", "")) / 10;
                }
                if (line.StartsWith("Anatomy=") && destMobile.Skills.Anatomy.Base < Convert.ToInt32(line.Replace("Anatomy=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Anatomy -> " + line.Replace("Anatomy=", ""), sphereCharName);
                                        destMobile.Skills.Anatomy.Base = Convert.ToInt32(line.Replace("Anatomy=", "")) / 10;
                }
                if (line.StartsWith("AnimalLore=") && destMobile.Skills.AnimalLore.Base < Convert.ToInt32(line.Replace("AnimalLore=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("AnimalLore -> " + line.Replace("AnimalLore=", ""), sphereCharName);
                                        destMobile.Skills.AnimalLore.Base = Convert.ToInt32(line.Replace("AnimalLore=", "")) / 10;
                }
                if (line.StartsWith("ItemID=") && destMobile.Skills.ItemID.Base < Convert.ToInt32(line.Replace("ItemID=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("ItemID -> " + line.Replace("ItemID=", ""), sphereCharName);
                                        destMobile.Skills.ItemID.Base = Convert.ToInt32(line.Replace("ItemID=", "")) / 10;
                }
                if (line.StartsWith("ArmsLore=") && destMobile.Skills.ArmsLore.Base < Convert.ToInt32(line.Replace("ArmsLore=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("ArmsLore -> " + line.Replace("ArmsLore=", ""), sphereCharName);
                                        destMobile.Skills.ArmsLore.Base = Convert.ToInt32(line.Replace("ArmsLore=", "")) / 10;
                }
                if (line.StartsWith("Parrying=") && destMobile.Skills.Parry.Base < Convert.ToInt32(line.Replace("Parrying=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Parry -> " + line.Replace("Parrying=", ""), sphereCharName);
                                        destMobile.Skills.Parry.Base = Convert.ToInt32(line.Replace("Parrying=", "")) / 10;
                }
                if (line.StartsWith("Begging=") && destMobile.Skills.Begging.Base < Convert.ToInt32(line.Replace("Begging=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Begging -> " + line.Replace("Begging=", ""), sphereCharName);
                                        destMobile.Skills.Begging.Base = Convert.ToInt32(line.Replace("Begging=", "")) / 10;
                }
                if (line.StartsWith("Blacksmithing=") && destMobile.Skills.Blacksmith.Base < Convert.ToInt32(line.Replace("Blacksmithing=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Blacksmith -> " + line.Replace("Blacksmithing=", ""), sphereCharName);
                                        destMobile.Skills.Blacksmith.Base = Convert.ToInt32(line.Replace("Blacksmithing=", "")) / 10;
                }
                if (line.StartsWith("Bowcraft=") && destMobile.Skills.Fletching.Base < Convert.ToInt32(line.Replace("Bowcraft=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Fletching/Bowcraft -> " + line.Replace("Bowcraft=", ""), sphereCharName);
                                        destMobile.Skills.Fletching.Base = Convert.ToInt32(line.Replace("Bowcraft=", "")) / 10;
                }
                if (line.StartsWith("Peacemaking=") && destMobile.Skills.Peacemaking.Base < Convert.ToInt32(line.Replace("Peacemaking=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Peacemaking -> " + line.Replace("Peacemaking=", ""), sphereCharName);
                                        destMobile.Skills.Peacemaking.Base = Convert.ToInt32(line.Replace("Peacemaking=", "")) / 10;
                }
                if (line.StartsWith("Camping=") && destMobile.Skills.Camping.Base < Convert.ToInt32(line.Replace("Camping=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Camping -> " + line.Replace("Camping=", ""), sphereCharName);
                                        destMobile.Skills.Camping.Base = Convert.ToInt32(line.Replace("Camping=", "")) / 10;
                }
                if (line.StartsWith("Carpentry=") && destMobile.Skills.Carpentry.Base < Convert.ToInt32(line.Replace("Carpentry=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Carpentry -> " + line.Replace("Carpentry=", ""), sphereCharName);
                                        destMobile.Skills.Carpentry.Base = Convert.ToInt32(line.Replace("Carpentry=", "")) / 10;
                }
                if (line.StartsWith("Cartography=") && destMobile.Skills.Cartography.Base < Convert.ToInt32(line.Replace("Cartography=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Cartography -> " + line.Replace("Cartography=", ""), sphereCharName);
                                        destMobile.Skills.Cartography.Base = Convert.ToInt32(line.Replace("Cartography=", "")) / 10;
                }
                if (line.StartsWith("Cooking=") && destMobile.Skills.Cooking.Base < Convert.ToInt32(line.Replace("Cooking=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Cooking -> " + line.Replace("Cooking=", ""), sphereCharName);
                                        destMobile.Skills.Cooking.Base = Convert.ToInt32(line.Replace("Cooking=", "")) / 10;
                }
                if (line.StartsWith("DetectingHidden=") && destMobile.Skills.DetectHidden.Base < Convert.ToInt32(line.Replace("DetectingHidden=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("DetectHidden -> " + line.Replace("DetectingHidden=", ""), sphereCharName);
                                        destMobile.Skills.DetectHidden.Base = Convert.ToInt32(line.Replace("DetectingHidden=", "")) / 10;
                }
                if (line.StartsWith("Enticement=") && destMobile.Skills.Discordance.Base < Convert.ToInt32(line.Replace("Enticement=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Discordance -> " + line.Replace("Enticement=", ""), sphereCharName);
                                        destMobile.Skills.Discordance.Base = Convert.ToInt32(line.Replace("Enticement=", "")) / 10;
                }
                if (line.StartsWith("EvaluatingIntel=") && destMobile.Skills.EvalInt.Base < Convert.ToInt32(line.Replace("EvaluatingIntel=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("EvalInt -> " + line.Replace("EvaluatingIntel=", ""), sphereCharName);
                                        destMobile.Skills.EvalInt.Base = Convert.ToInt32(line.Replace("EvaluatingIntel=", "")) / 10;
                }
                if (line.StartsWith("Healing=") && destMobile.Skills.Healing.Base < Convert.ToInt32(line.Replace("Healing=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Healing -> " + line.Replace("Healing=", ""), sphereCharName);
                                        destMobile.Skills.Healing.Base = Convert.ToInt32(line.Replace("Healing=", "")) / 10;
                }
                if (line.StartsWith("Fishing=") && destMobile.Skills.Fishing.Base < Convert.ToInt32(line.Replace("Fishing=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Fishing -> " + line.Replace("Fishing=", ""), sphereCharName);
                                        destMobile.Skills.Fishing.Base = Convert.ToInt32(line.Replace("Fishing=", "")) / 10;
                }
                if (line.StartsWith("Forensics=") && destMobile.Skills.Forensics.Base < Convert.ToInt32(line.Replace("Forensics=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Forensics -> " + line.Replace("Forensics=", ""), sphereCharName);
                                        destMobile.Skills.Forensics.Base = Convert.ToInt32(line.Replace("Forensics=", "")) / 10;
                }
                if (line.StartsWith("Herding=") && destMobile.Skills.Herding.Base < Convert.ToInt32(line.Replace("Herding=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Herding -> " + line.Replace("Herding=", ""), sphereCharName);
                                        destMobile.Skills.Herding.Base = Convert.ToInt32(line.Replace("Herding=", "")) / 10;
                }
                if (line.StartsWith("Hiding=") && destMobile.Skills.Hiding.Base < Convert.ToInt32(line.Replace("Hiding=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Hiding -> " + line.Replace("Hiding=", ""), sphereCharName);
                                        destMobile.Skills.Hiding.Base = Convert.ToInt32(line.Replace("Hiding=", "")) / 10;
                }
                if (line.StartsWith("Provocation=") && destMobile.Skills.Provocation.Base < Convert.ToInt32(line.Replace("Provocation=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Provocation -> " + line.Replace("Provocation=", ""), sphereCharName);
                                        destMobile.Skills.Provocation.Base = Convert.ToInt32(line.Replace("Provocation=", "")) / 10;
                }
                if (line.StartsWith("Inscription=") && destMobile.Skills.Inscribe.Base < Convert.ToInt32(line.Replace("Inscription=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Inscribe -> " + line.Replace("Inscription=", ""), sphereCharName);
                                        destMobile.Skills.Inscribe.Base = Convert.ToInt32(line.Replace("Inscription=", "")) / 10;
                }
                if (line.StartsWith("LockPicking=") && destMobile.Skills.Lockpicking.Base < Convert.ToInt32(line.Replace("LockPicking=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Lockpicking -> " + line.Replace("LockPicking=", ""), sphereCharName);
                                        destMobile.Skills.Lockpicking.Base = Convert.ToInt32(line.Replace("LockPicking=", "")) / 10;
                }
                if (line.StartsWith("Magery=") && destMobile.Skills.Magery.Base < Convert.ToInt32(line.Replace("Magery=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Magery -> " + line.Replace("Magery=", ""), sphereCharName);
                                        destMobile.Skills.Magery.Base = Convert.ToInt32(line.Replace("Magery=", "")) / 10;
                }
                if (line.StartsWith("MagicResistance=") && destMobile.Skills.MagicResist.Base < Convert.ToInt32(line.Replace("MagicResistance=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("MagicResist -> " + line.Replace("MagicResistance=", ""), sphereCharName);
                                        destMobile.Skills.MagicResist.Base = Convert.ToInt32(line.Replace("MagicResistance=", "")) / 10;
                }
                if (line.StartsWith("Tactics=") && destMobile.Skills.Tactics.Base < Convert.ToInt32(line.Replace("Tactics=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Tactics -> " + line.Replace("Tactics=", ""), sphereCharName);
                                        destMobile.Skills.Tactics.Base = Convert.ToInt32(line.Replace("Tactics=", "")) / 10;
                }
                if (line.StartsWith("Snooping=") && destMobile.Skills.Snooping.Base < Convert.ToInt32(line.Replace("Snooping=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Snooping -> " + line.Replace("Snooping=", ""), sphereCharName);
                                        destMobile.Skills.Snooping.Base = Convert.ToInt32(line.Replace("Snooping=", "")) / 10;
                }
                if (line.StartsWith("Musicianship=") && destMobile.Skills.Musicianship.Base < Convert.ToInt32(line.Replace("Musicianship=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Musicianship -> " + line.Replace("Musicianship=", ""), sphereCharName);
                                        destMobile.Skills.Musicianship.Base = Convert.ToInt32(line.Replace("Musicianship=", "")) / 10;
                }
                if (line.StartsWith("Poisoning=") && destMobile.Skills.Poisoning.Base < Convert.ToInt32(line.Replace("Poisoning=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Poisoning -> " + line.Replace("Poisoning=", ""), sphereCharName);
                                        destMobile.Skills.Poisoning.Base = Convert.ToInt32(line.Replace("Poisoning=", "")) / 10;
                }
                if (line.StartsWith("Archery=") && destMobile.Skills.Archery.Base < Convert.ToInt32(line.Replace("Archery=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Archery -> " + line.Replace("Archery=", ""), sphereCharName);
                                        destMobile.Skills.Archery.Base = Convert.ToInt32(line.Replace("Archery=", "")) / 10;
                }
                if (line.StartsWith("SpiritSpeak=") && destMobile.Skills.SpiritSpeak.Base < Convert.ToInt32(line.Replace("SpiritSpeak=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("SpiritSpeak -> " + line.Replace("SpiritSpeak=", ""), sphereCharName);
                                        destMobile.Skills.SpiritSpeak.Base = Convert.ToInt32(line.Replace("SpiritSpeak=", "")) / 10;
                }
                if (line.StartsWith("Stealing=") && destMobile.Skills.Stealing.Base < Convert.ToInt32(line.Replace("Stealing=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Stealing -> " + line.Replace("Stealing=", ""), sphereCharName);
                                        destMobile.Skills.Stealing.Base = Convert.ToInt32(line.Replace("Stealing=", "")) / 10;
                }
                if (line.StartsWith("Tailoring=") && destMobile.Skills.Tailoring.Base < Convert.ToInt32(line.Replace("Tailoring=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Tailoring -> " + line.Replace("Tailoring=", ""), sphereCharName);
                                        destMobile.Skills.Tailoring.Base = Convert.ToInt32(line.Replace("Tailoring=", "")) / 10;
                }
                if (line.StartsWith("Taming=") && destMobile.Skills.AnimalTaming.Base < Convert.ToInt32(line.Replace("Taming=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("AnimalTaming -> " + line.Replace("Taming=", ""), sphereCharName);
                                        destMobile.Skills.AnimalTaming.Base = Convert.ToInt32(line.Replace("Taming=", "")) / 10;
                }
                if (line.StartsWith("TasteID=") && destMobile.Skills.TasteID.Base < Convert.ToInt32(line.Replace("TasteID=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("TasteID -> " + line.Replace("TasteID=", ""), sphereCharName);
                                        destMobile.Skills.TasteID.Base = Convert.ToInt32(line.Replace("TasteID=", "")) / 10;
                }
                if (line.StartsWith("Tinkering=") && destMobile.Skills.Tinkering.Base < Convert.ToInt32(line.Replace("Tinkering=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Tinkering -> " + line.Replace("Tinkering=", ""), sphereCharName);
                                        destMobile.Skills.Tinkering.Base = Convert.ToInt32(line.Replace("Tinkering=", "")) / 10;
                }
                if (line.StartsWith("Tracking=") && destMobile.Skills.Tracking.Base < Convert.ToInt32(line.Replace("Tracking=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Tracking -> " + line.Replace("Tracking=", ""), sphereCharName);
                                        destMobile.Skills.Tracking.Base = Convert.ToInt32(line.Replace("Tracking=", "")) / 10;
                }
                if (line.StartsWith("Veterinary=") && destMobile.Skills.Veterinary.Base < Convert.ToInt32(line.Replace("Veterinary=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Veterinary -> " + line.Replace("Veterinary=", ""), sphereCharName);
                                        destMobile.Skills.Veterinary.Base = Convert.ToInt32(line.Replace("Veterinary=", "")) / 10;
                }
                if (line.StartsWith("Swordsmanship=") && destMobile.Skills.Swords.Base < Convert.ToInt32(line.Replace("Swordsmanship=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Swords -> " + line.Replace("Swordsmanship=", ""), sphereCharName);
                                        destMobile.Skills.Swords.Base = Convert.ToInt32(line.Replace("Swordsmanship=", "")) / 10;
                }
                if (line.StartsWith("Macefighting=") && destMobile.Skills.Macing.Base < Convert.ToInt32(line.Replace("Macefighting=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Macing -> " + line.Replace("Macefighting=", ""), sphereCharName);
                                        destMobile.Skills.Macing.Base = Convert.ToInt32(line.Replace("Macefighting=", "")) / 10;
                }
                if (line.StartsWith("Fencing=") && destMobile.Skills.Fencing.Base < Convert.ToInt32(line.Replace("Fencing=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Fencing -> " + line.Replace("Fencing=", ""), sphereCharName);
                                        destMobile.Skills.Fencing.Base = Convert.ToInt32(line.Replace("Fencing=", "")) / 10;
                }
                if (line.StartsWith("Wrestling=") && destMobile.Skills.Wrestling.Base < Convert.ToInt32(line.Replace("Wrestling=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Wrestling -> " + line.Replace("Wrestling=", ""), sphereCharName);
                                        destMobile.Skills.Wrestling.Base = Convert.ToInt32(line.Replace("Wrestling=", "")) / 10;
                }
                if (line.StartsWith("Lumberjacking=") && destMobile.Skills.Lumberjacking.Base < Convert.ToInt32(line.Replace("Lumberjacking=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Lumberjacking -> " + line.Replace("Lumberjacking=", ""), sphereCharName);
                                        destMobile.Skills.Lumberjacking.Base = Convert.ToInt32(line.Replace("Lumberjacking=", "")) / 10;
                }
                if (line.StartsWith("Mining=") && destMobile.Skills.Mining.Base < Convert.ToInt32(line.Replace("Mining=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Mining -> " + line.Replace("Mining=", ""), sphereCharName);
                                        destMobile.Skills.Mining.Base = Convert.ToInt32(line.Replace("Mining=", "")) / 10;
                }
                if (line.StartsWith("Meditation=") && destMobile.Skills.Meditation.Base < Convert.ToInt32(line.Replace("Meditation=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Meditation -> " + line.Replace("Meditation=", ""), sphereCharName);
                                        destMobile.Skills.Meditation.Base = Convert.ToInt32(line.Replace("Meditation=", "")) / 10;
                }
                if (line.StartsWith("Stealth=") && destMobile.Skills.Stealth.Base < Convert.ToInt32(line.Replace("Stealth=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("Stealth -> " + line.Replace("Stealth=", ""), sphereCharName);
                                        destMobile.Skills.Stealth.Base = Convert.ToInt32(line.Replace("Stealth=", "")) / 10;
                }
                if (line.StartsWith("RemoveTrap=") && destMobile.Skills.RemoveTrap.Base < Convert.ToInt32(line.Replace("RemoveTrap=", "")) / 10)
                                    {
                                        Logger.LogSphereImport("RemoveTrap -> " + line.Replace("RemoveTrap=", ""), sphereCharName);
                                        destMobile.Skills.RemoveTrap.Base = Convert.ToInt32(line.Replace("RemoveTrap=", "")) / 10;
                }
               
            }

            for (int i = 0; i < destMobile.Skills.Length; ++i)
                if (destMobile.Skills[i].Base > 100)
                    destMobile.Skills[i].Base = 100;
        }


        private static bool FindAccountInList()
        {
            bool isAccount = false;
            bool isCorrectAccount = false;
            bool isCorrectChar = false;

            foreach (string line in objectStringList)
            {
                if (line.StartsWith("[WC"))
                {
                    isAccount = true;
                    break;
                }
            }

            if (isAccount == false)
                return false;


            foreach (string line in objectStringList)
            {
                if (line.StartsWith("ACCOUNT="))
                {
                    if (line.Replace("ACCOUNT=", "").Trim() == sphereAccName.Trim())
                    {
                        isCorrectAccount = true;
                        break;
                    }
                }
            }

            if (isCorrectAccount == false)
                return false;

            foreach (string line in objectStringList)
            {
                if (line.StartsWith("NAME="))
                {
                    if (line.Replace("NAME=", "").Trim() == sphereCharName.Trim())
                    {
                        isCorrectChar = true;
                        break;
                    }
                }
            }

            if (isCorrectChar == false)
                return false;

            foreach (string line in objectStringList)
            {
                if (line.StartsWith("SERIAL="))
                {
                    charID = line.Replace("SERIAL=", "");
                    Logger.LogSphereImport(string.Format("Char '{0}' localizado! Serial: {1}", sphereAccName, charID), sphereCharName);
                    break;
                }
            }

            //SetSkill(objectStringList);

            if (string.IsNullOrEmpty(charID))
                return false;
            else
                return true;

        }




    }
}
