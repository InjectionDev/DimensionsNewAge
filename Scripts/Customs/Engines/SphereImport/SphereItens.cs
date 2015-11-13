using System;
using System.Collections.Generic;
using System.Text;

using Server.Items;

namespace DimensionsNewAge.Scripts.Customs.Engines.SphereImport
{
    public static class SphereItens
    {
        public static List<string> SphereCommomContainersList
        {
            get
            {
                List<string> sphereCommomContainersList = new List<string>();
                sphereCommomContainersList.Add("i_metal_chest");
                sphereCommomContainersList.Add("i_bag");
                sphereCommomContainersList.Add("i_backpack");
                sphereCommomContainersList.Add("i_box_wood");
                sphereCommomContainersList.Add("i_chest_wooden_brass");
                sphereCommomContainersList.Add("i_chest_wooden");
                sphereCommomContainersList.Add("i_chest_metal_brass");
                sphereCommomContainersList.Add("i_chest_metal");
                sphereCommomContainersList.Add("i_bankbox");
                sphereCommomContainersList.Add("i_box_brass");
                sphereCommomContainersList.Add("i_cofre");
                sphereCommomContainersList.Add("i_pouch");

                return sphereCommomContainersList;
            }
        }


        public static List<SphereItemClass> SphereItensList
        {
            get 
            {
                List<SphereItemClass> sphereItensList = new List<SphereItemClass>();

                sphereItensList.Add(new SphereItemClass("i_gold", typeof(Server.Items.Gold)));

                sphereItensList.Add(new SphereItemClass("i_ore_iron", typeof(Server.Items.IronOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_rusty", typeof(Server.Items.RustyOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_old_copper", typeof(Server.Items.OldCopperOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_dull_copper", typeof(Server.Items.DullCopperOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_ruby", typeof(Server.Items.RubyOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_copper", typeof(Server.Items.CopperOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_bronze", typeof(Server.Items.BronzeOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_shadow", typeof(Server.Items.ShadowIronOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_silver", typeof(Server.Items.SilverOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_mercury", typeof(Server.Items.MercuryOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_rose", typeof(Server.Items.RoseOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_gold", typeof(Server.Items.GoldOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_agapite", typeof(Server.Items.AgapiteOre)))    ;
                sphereItensList.Add(new SphereItemClass("i_ore_verite", typeof(Server.Items.VeriteOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_plutonium", typeof(Server.Items.PlutoniumOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_bloodrock", typeof(Server.Items.BloodRockOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_valorite", typeof(Server.Items.ValoriteOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_blackrock", typeof(Server.Items.BlackRockOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_mytheril", typeof(Server.Items.MytherilOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_aqua", typeof(Server.Items.AquaOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_endurium", typeof(Server.Items.EnduriumOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_old_endurium", typeof(Server.Items.OldEnduriumOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_goldstone", typeof(Server.Items.GoldStoneOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_max_mytheril", typeof(Server.Items.MaxMytherilOre)));
                sphereItensList.Add(new SphereItemClass("i_ore_magma", typeof(Server.Items.MagmaOre)));

                sphereItensList.Add(new SphereItemClass("i_ingot_iron", typeof(Server.Items.IronIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_rusty", typeof(Server.Items.RustyIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_old_copper", typeof(Server.Items.OldCopperIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_dull_copper", typeof(Server.Items.DullCopperIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_ruby", typeof(Server.Items.RubyIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_copper", typeof(Server.Items.CopperIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_bronze", typeof(Server.Items.BronzeIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_shadow", typeof(Server.Items.ShadowIronIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_silver", typeof(Server.Items.SilverIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_mercury", typeof(Server.Items.MercuryIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_rose", typeof(Server.Items.RoseIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_gold", typeof(Server.Items.GoldIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_agapite", typeof(Server.Items.AgapiteIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_verite", typeof(Server.Items.VeriteIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_plutonium", typeof(Server.Items.PlutoniumIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_bloodrock", typeof(Server.Items.BloodRockIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_valorite", typeof(Server.Items.ValoriteIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_blackrock", typeof(Server.Items.BlackRockIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_mytheril", typeof(Server.Items.MytherilIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_aqua", typeof(Server.Items.AquaIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_endurium", typeof(Server.Items.EnduriumIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_oldend", typeof(Server.Items.OldEnduriumIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_goldstone", typeof(Server.Items.GoldStoneIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_maxmyt", typeof(Server.Items.MaxMytherilIngot)));
                sphereItensList.Add(new SphereItemClass("i_ingot_magma", typeof(Server.Items.MagmaIngot)));


                



                #region Plate Color
                sphereItensList.Add(new SphereItemClass("i_old_copper_platemail_leggings", typeof(Server.Items.PlateLegsOldCopper)));
                sphereItensList.Add(new SphereItemClass("i_old_copper_platemail_arms", typeof(Server.Items.PlateArmsOldCopper)));
                sphereItensList.Add(new SphereItemClass("i_old_copper_platemail_gorget", typeof(Server.Items.PlateGorgetOldCopper)));
                sphereItensList.Add(new SphereItemClass("i_old_copper_platemail_gauntlets", typeof(Server.Items.PlateGlovesOldCopper)));
                sphereItensList.Add(new SphereItemClass("i_old_copper_plate_helm", typeof(Server.Items.PlateCloseHelmOldCopper)));
                sphereItensList.Add(new SphereItemClass("i_old_copper_platemail", typeof(Server.Items.PlateChestOldCopper)));
                sphereItensList.Add(new SphereItemClass("i_old_copper_heater_shield", typeof(Server.Items.HeaterShieldOldCopper)));
                sphereItensList.Add(new SphereItemClass("i_old_copper_closed_helm", typeof(Server.Items.PlateCloseHelmOldCopper)));

                sphereItensList.Add(new SphereItemClass("i_Dull_Copper_platemail_leggings", typeof(Server.Items.PlateLegsDullCopper)));
                sphereItensList.Add(new SphereItemClass("i_Dull_Copper_platemail_arms", typeof(Server.Items.PlateArmsDullCopper)));
                sphereItensList.Add(new SphereItemClass("i_Dull_Copper_platemail_gorget", typeof(Server.Items.PlateGorgetDullCopper)));
                sphereItensList.Add(new SphereItemClass("i_Dull_Copper_platemail_gauntlets", typeof(Server.Items.PlateGlovesDullCopper)));
                sphereItensList.Add(new SphereItemClass("i_Dull_Copper_plate_helm", typeof(Server.Items.PlateCloseHelmDullCopper)));
                sphereItensList.Add(new SphereItemClass("i_Dull_Copper_platemail", typeof(Server.Items.PlateChestDullCopper)));
                sphereItensList.Add(new SphereItemClass("i_Dull_Copper_heater_shield", typeof(Server.Items.HeaterShieldDullCopper)));
                sphereItensList.Add(new SphereItemClass("i_Dull_Copper_closed_helm", typeof(Server.Items.PlateCloseHelmDullCopper)));

                sphereItensList.Add(new SphereItemClass("i_Ruby_platemail_leggings", typeof(Server.Items.PlateLegsRuby)));
                sphereItensList.Add(new SphereItemClass("i_Ruby_platemail_arms", typeof(Server.Items.PlateArmsRuby)));
                sphereItensList.Add(new SphereItemClass("i_Ruby_platemail_gorget", typeof(Server.Items.PlateGorgetRuby)));
                sphereItensList.Add(new SphereItemClass("i_Ruby_platemail_gauntlets", typeof(Server.Items.PlateGlovesRuby)));
                sphereItensList.Add(new SphereItemClass("i_Ruby_plate_helm", typeof(Server.Items.PlateCloseHelmRuby)));
                sphereItensList.Add(new SphereItemClass("i_Ruby_platemail", typeof(Server.Items.PlateChestRuby)));
                sphereItensList.Add(new SphereItemClass("i_Ruby_heater_shield", typeof(Server.Items.HeaterShieldRuby)));
                sphereItensList.Add(new SphereItemClass("i_Ruby_closed_helm", typeof(Server.Items.PlateCloseHelmRuby)));

                sphereItensList.Add(new SphereItemClass("i_Copper_platemail_leggings", typeof(Server.Items.PlateLegsCopper)));
                sphereItensList.Add(new SphereItemClass("i_Copper_platemail_arms", typeof(Server.Items.PlateArmsCopper)));
                sphereItensList.Add(new SphereItemClass("i_Copper_platemail_gorget", typeof(Server.Items.PlateGorgetCopper)));
                sphereItensList.Add(new SphereItemClass("i_Copper_platemail_gauntlets", typeof(Server.Items.PlateGlovesCopper)));
                sphereItensList.Add(new SphereItemClass("i_Copper_plate_helm", typeof(Server.Items.PlateCloseHelmCopper)));
                sphereItensList.Add(new SphereItemClass("i_Copper_platemail", typeof(Server.Items.PlateChestCopper)));
                sphereItensList.Add(new SphereItemClass("i_Copper_heater_shield", typeof(Server.Items.HeaterShieldCopper)));
                sphereItensList.Add(new SphereItemClass("i_Copper_closed_helm", typeof(Server.Items.PlateCloseHelmCopper)));

                sphereItensList.Add(new SphereItemClass("i_Bronze_platemail_leggings", typeof(Server.Items.PlateLegsBronze)));
                sphereItensList.Add(new SphereItemClass("i_Bronze_platemail_arms", typeof(Server.Items.PlateArmsBronze)));
                sphereItensList.Add(new SphereItemClass("i_Bronze_platemail_gorget", typeof(Server.Items.PlateGorgetBronze)));
                sphereItensList.Add(new SphereItemClass("i_Bronze_platemail_gauntlets", typeof(Server.Items.PlateGlovesBronze)));
                sphereItensList.Add(new SphereItemClass("i_Bronze_plate_helm", typeof(Server.Items.PlateCloseHelmBronze)));
                sphereItensList.Add(new SphereItemClass("i_Bronze_platemail", typeof(Server.Items.PlateChestBronze)));
                sphereItensList.Add(new SphereItemClass("i_Bronze_heater_shield", typeof(Server.Items.HeaterShieldBronze)));
                sphereItensList.Add(new SphereItemClass("i_Bronze_closed_helm", typeof(Server.Items.PlateCloseHelmBronze)));

                sphereItensList.Add(new SphereItemClass("i_Shadow_platemail_leggings", typeof(Server.Items.PlateLegsShadow)));
                sphereItensList.Add(new SphereItemClass("i_Shadow_platemail_arms", typeof(Server.Items.PlateArmsShadow)));
                sphereItensList.Add(new SphereItemClass("i_Shadow_platemail_gorget", typeof(Server.Items.PlateGorgetShadow)));
                sphereItensList.Add(new SphereItemClass("i_Shadow_platemail_gauntlets", typeof(Server.Items.PlateGlovesShadow)));
                sphereItensList.Add(new SphereItemClass("i_Shadow_plate_helm", typeof(Server.Items.PlateCloseHelmShadow)));
                sphereItensList.Add(new SphereItemClass("i_Shadow_platemail", typeof(Server.Items.PlateChestShadow)));
                sphereItensList.Add(new SphereItemClass("i_Shadow_heater_shield", typeof(Server.Items.HeaterShieldShadow)));
                sphereItensList.Add(new SphereItemClass("i_Shadow__closed_helm", typeof(Server.Items.PlateCloseHelmShadow)));

                sphereItensList.Add(new SphereItemClass("i_Silver_platemail_leggings", typeof(Server.Items.PlateLegsSilver)));
                sphereItensList.Add(new SphereItemClass("i_Silver_platemail_arms", typeof(Server.Items.PlateArmsSilver)));
                sphereItensList.Add(new SphereItemClass("i_Silver_platemail_gorget", typeof(Server.Items.PlateGorgetSilver)));
                sphereItensList.Add(new SphereItemClass("i_Silver_platemail_gauntlets", typeof(Server.Items.PlateGlovesSilver)));
                sphereItensList.Add(new SphereItemClass("i_Silver_plate_helm", typeof(Server.Items.PlateCloseHelmSilver)));
                sphereItensList.Add(new SphereItemClass("i_Silver_platemail", typeof(Server.Items.PlateChestSilver)));
                sphereItensList.Add(new SphereItemClass("i_Silver_heater_shield", typeof(Server.Items.HeaterShieldSilver)));
                sphereItensList.Add(new SphereItemClass("i_Silver_closed_helm", typeof(Server.Items.PlateCloseHelmSilver)));

                sphereItensList.Add(new SphereItemClass("i_Mercury_platemail_leggings", typeof(Server.Items.PlateLegsMercury)));
                sphereItensList.Add(new SphereItemClass("i_Mercury_platemail_arms", typeof(Server.Items.PlateArmsMercury)));
                sphereItensList.Add(new SphereItemClass("i_Mercury_platemail_gorget", typeof(Server.Items.PlateGorgetMercury)));
                sphereItensList.Add(new SphereItemClass("i_Mercury_platemail_gauntlets", typeof(Server.Items.PlateGlovesMercury)));
                sphereItensList.Add(new SphereItemClass("i_Mercury_plate_helm", typeof(Server.Items.PlateCloseHelmMercury)));
                sphereItensList.Add(new SphereItemClass("i_Mercury_platemail", typeof(Server.Items.PlateChestMercury)));
                sphereItensList.Add(new SphereItemClass("i_Mercury_heater_shield", typeof(Server.Items.HeaterShieldMercury)));
                sphereItensList.Add(new SphereItemClass("i_Mercury_closed_helm", typeof(Server.Items.PlateCloseHelmMercury)));

                sphereItensList.Add(new SphereItemClass("i_Rose_platemail_leggings", typeof(Server.Items.PlateLegsRose)));
                sphereItensList.Add(new SphereItemClass("i_Rose_platemail_arms", typeof(Server.Items.PlateArmsRose)));
                sphereItensList.Add(new SphereItemClass("i_Rose_platemail_gorget", typeof(Server.Items.PlateGorgetRose)));
                sphereItensList.Add(new SphereItemClass("i_Rose_platemail_gauntlets", typeof(Server.Items.PlateGlovesRose)));
                sphereItensList.Add(new SphereItemClass("i_Rose_plate_helm", typeof(Server.Items.PlateCloseHelmRose)));
                sphereItensList.Add(new SphereItemClass("i_Rose_platemail", typeof(Server.Items.PlateChestRose)));
                sphereItensList.Add(new SphereItemClass("i_Rose_heater_shield", typeof(Server.Items.HeaterShieldRose)));
                sphereItensList.Add(new SphereItemClass("i_Rose_closed_helm", typeof(Server.Items.PlateCloseHelmRose)));

                sphereItensList.Add(new SphereItemClass("i_Gold_platemail_leggings", typeof(Server.Items.PlateLegsGold)));
                sphereItensList.Add(new SphereItemClass("i_Gold_platemail_arms", typeof(Server.Items.PlateArmsGold)));
                sphereItensList.Add(new SphereItemClass("i_Gold_platemail_gorget", typeof(Server.Items.PlateGorgetGold)));
                sphereItensList.Add(new SphereItemClass("i_Gold_platemail_gauntlets", typeof(Server.Items.PlateGlovesGold)));
                sphereItensList.Add(new SphereItemClass("i_Gold_plate_helm", typeof(Server.Items.PlateCloseHelmGold)));
                sphereItensList.Add(new SphereItemClass("i_Gold_platemail", typeof(Server.Items.PlateChestGold)));
                sphereItensList.Add(new SphereItemClass("i_Gold_heater_shield", typeof(Server.Items.HeaterShieldGold)));
                sphereItensList.Add(new SphereItemClass("i_Gold_closed_helm", typeof(Server.Items.PlateCloseHelmGold)));

                sphereItensList.Add(new SphereItemClass("i_Agapite_platemail_leggings", typeof(Server.Items.PlateLegsAgapite)));
                sphereItensList.Add(new SphereItemClass("i_Agapite_platemail_arms", typeof(Server.Items.PlateArmsAgapite)));
                sphereItensList.Add(new SphereItemClass("i_Agapite_platemail_gorget", typeof(Server.Items.PlateGorgetAgapite)));
                sphereItensList.Add(new SphereItemClass("i_Agapite_platemail_gauntlets", typeof(Server.Items.PlateGlovesAgapite)));
                sphereItensList.Add(new SphereItemClass("i_Agapite_plate_helm", typeof(Server.Items.PlateCloseHelmAgapite)));
                sphereItensList.Add(new SphereItemClass("i_Agapite_platemail", typeof(Server.Items.PlateChestAgapite)));
                sphereItensList.Add(new SphereItemClass("i_Agapite_heater_shield", typeof(Server.Items.HeaterShieldAgapite)));
                sphereItensList.Add(new SphereItemClass("i_agapite_closed_helm", typeof(Server.Items.PlateCloseHelmAgapite)));

                sphereItensList.Add(new SphereItemClass("i_Verite_platemail_leggings", typeof(Server.Items.PlateLegsVerite)));
                sphereItensList.Add(new SphereItemClass("i_Verite_platemail_arms", typeof(Server.Items.PlateArmsVerite)));
                sphereItensList.Add(new SphereItemClass("i_Verite_platemail_gorget", typeof(Server.Items.PlateGorgetVerite)));
                sphereItensList.Add(new SphereItemClass("i_Verite_platemail_gauntlets", typeof(Server.Items.PlateGlovesVerite)));
                sphereItensList.Add(new SphereItemClass("i_Verite_plate_helm", typeof(Server.Items.PlateCloseHelmVerite)));
                sphereItensList.Add(new SphereItemClass("i_Verite_platemail", typeof(Server.Items.PlateChestVerite)));
                sphereItensList.Add(new SphereItemClass("i_Verite_heater_shield", typeof(Server.Items.HeaterShieldVerite)));
                sphereItensList.Add(new SphereItemClass("i_Verite_closed_helm", typeof(Server.Items.PlateCloseHelmVerite)));

                sphereItensList.Add(new SphereItemClass("i_plutonium_platemail_leggings", typeof(Server.Items.PlateLegsPlutonio)));
                sphereItensList.Add(new SphereItemClass("i_plutonium_platemail_arms", typeof(Server.Items.PlateArmsPlutonio)));
                sphereItensList.Add(new SphereItemClass("i_plutonium_platemail_gorget", typeof(Server.Items.PlateGorgetPlutonio)));
                sphereItensList.Add(new SphereItemClass("i_plutonium_platemail_gauntlets", typeof(Server.Items.PlateGlovesPlutonio)));
                sphereItensList.Add(new SphereItemClass("i_plutonium_plate_helm", typeof(Server.Items.PlateCloseHelmPlutonio)));
                sphereItensList.Add(new SphereItemClass("i_plutonium_platemail", typeof(Server.Items.PlateChestPlutonio)));
                sphereItensList.Add(new SphereItemClass("i_plutonium_heater_shield", typeof(Server.Items.HeaterShieldPlutonio)));
                sphereItensList.Add(new SphereItemClass("i_plutonium_closed_helm", typeof(Server.Items.PlateCloseHelmPlutonio)));

                sphereItensList.Add(new SphereItemClass("i_bloodrock_platemail_leggings", typeof(Server.Items.PlateLegsBloodRock)));
                sphereItensList.Add(new SphereItemClass("i_bloodrock_platemail_arms", typeof(Server.Items.PlateArmsBloodRock)));
                sphereItensList.Add(new SphereItemClass("i_bloodrock_platemail_gorget", typeof(Server.Items.PlateGorgetBloodRock)));
                sphereItensList.Add(new SphereItemClass("i_bloodrock_platemail_gauntlets", typeof(Server.Items.PlateGlovesBloodRock)));
                sphereItensList.Add(new SphereItemClass("i_bloodrock_plate_helm", typeof(Server.Items.PlateCloseHelmBloodRock)));
                sphereItensList.Add(new SphereItemClass("i_bloodrock_platemail", typeof(Server.Items.PlateChestBloodRock)));
                sphereItensList.Add(new SphereItemClass("i_bloodrock_heater_shield", typeof(Server.Items.HeaterShieldBloodRock)));
                sphereItensList.Add(new SphereItemClass("i_bloodrock_closed_helm", typeof(Server.Items.PlateCloseHelmBloodRock)));

                sphereItensList.Add(new SphereItemClass("i_Valorite_platemail_leggings", typeof(Server.Items.PlateLegsValorite)));
                sphereItensList.Add(new SphereItemClass("i_Valorite_platemail_arms", typeof(Server.Items.PlateArmsValorite)));
                sphereItensList.Add(new SphereItemClass("i_Valorite_platemail_gorget", typeof(Server.Items.PlateGorgetValorite)));
                sphereItensList.Add(new SphereItemClass("i_Valorite_platemail_gauntlets", typeof(Server.Items.PlateGlovesValorite)));
                sphereItensList.Add(new SphereItemClass("i_Valorite_plate_helm", typeof(Server.Items.PlateCloseHelmValorite)));
                sphereItensList.Add(new SphereItemClass("i_Valorite_platemail", typeof(Server.Items.PlateChestValorite)));
                sphereItensList.Add(new SphereItemClass("i_Valorite_heater_shield", typeof(Server.Items.HeaterShieldValorite)));
                sphereItensList.Add(new SphereItemClass("i_Valorite_closed_helm", typeof(Server.Items.PlateCloseHelmValorite)));

                sphereItensList.Add(new SphereItemClass("i_BlackRock_platemail_leggings", typeof(Server.Items.PlateLegsBlackRock)));
                sphereItensList.Add(new SphereItemClass("i_BlackRock_platemail_arms", typeof(Server.Items.PlateArmsBlackRock)));
                sphereItensList.Add(new SphereItemClass("i_BlackRock_platemail_gorget", typeof(Server.Items.PlateGorgetBlackRock)));
                sphereItensList.Add(new SphereItemClass("i_BlackRock_platemail_gauntlets", typeof(Server.Items.PlateGlovesBlackRock)));
                sphereItensList.Add(new SphereItemClass("i_BlackRock_plate_helm", typeof(Server.Items.PlateCloseHelmBlackRock)));
                sphereItensList.Add(new SphereItemClass("i_BlackRock_platemail", typeof(Server.Items.PlateChestBlackRock)));
                sphereItensList.Add(new SphereItemClass("i_BlackRock_heater_shield", typeof(Server.Items.HeaterShieldBlackRock)));
                sphereItensList.Add(new SphereItemClass("i_BlackRock_closed_helm", typeof(Server.Items.PlateCloseHelmBlackRock)));

                sphereItensList.Add(new SphereItemClass("i_Mytheril_platemail_leggings", typeof(Server.Items.PlateLegsMytheril)));
                sphereItensList.Add(new SphereItemClass("i_Mytheril_platemail_arms", typeof(Server.Items.PlateArmsMytheril)));
                sphereItensList.Add(new SphereItemClass("i_Mytheril_platemail_gorget", typeof(Server.Items.PlateGorgetMytheril)));
                sphereItensList.Add(new SphereItemClass("i_Mytheril_platemail_gauntlets", typeof(Server.Items.PlateGlovesMytheril)));
                sphereItensList.Add(new SphereItemClass("i_Mytheril_plate_helm", typeof(Server.Items.PlateCloseHelmMytheril)));
                sphereItensList.Add(new SphereItemClass("i_Mytheril_platemail", typeof(Server.Items.PlateChestMytheril)));
                sphereItensList.Add(new SphereItemClass("i_Mytheril_heater_shield", typeof(Server.Items.HeaterShieldMytheril)));
                sphereItensList.Add(new SphereItemClass("i_Mytheril_closed_helm", typeof(Server.Items.PlateCloseHelmMytheril)));

                sphereItensList.Add(new SphereItemClass("i_Aqua_platemail_leggings", typeof(Server.Items.PlateLegsAqua)));
                sphereItensList.Add(new SphereItemClass("i_Aqua_platemail_arms", typeof(Server.Items.PlateArmsAqua)));
                sphereItensList.Add(new SphereItemClass("i_Aqua_platemail_gorget", typeof(Server.Items.PlateGorgetAqua)));
                sphereItensList.Add(new SphereItemClass("i_Aqua_platemail_gauntlets", typeof(Server.Items.PlateGlovesAqua)));
                sphereItensList.Add(new SphereItemClass("i_Aqua_plate_helm", typeof(Server.Items.PlateCloseHelmAqua)));
                sphereItensList.Add(new SphereItemClass("i_Aqua_platemail", typeof(Server.Items.PlateChestAqua)));
                sphereItensList.Add(new SphereItemClass("i_Aqua_heater_shield", typeof(Server.Items.HeaterShieldAqua)));
                sphereItensList.Add(new SphereItemClass("i_aqua_closed_helm", typeof(Server.Items.PlateCloseHelmAqua)));

                sphereItensList.Add(new SphereItemClass("i_Endurium_platemail_leggings", typeof(Server.Items.PlateLegsEndurium)));
                sphereItensList.Add(new SphereItemClass("i_Endurium_platemail_arms", typeof(Server.Items.PlateArmsEndurium)));
                sphereItensList.Add(new SphereItemClass("i_Endurium_platemail_gorget", typeof(Server.Items.PlateGorgetEndurium)));
                sphereItensList.Add(new SphereItemClass("i_Endurium_platemail_gauntlets", typeof(Server.Items.PlateGlovesEndurium)));
                sphereItensList.Add(new SphereItemClass("i_Endurium_plate_helm", typeof(Server.Items.PlateCloseHelmEndurium)));
                sphereItensList.Add(new SphereItemClass("i_Endurium_platemail", typeof(Server.Items.PlateChestEndurium)));
                sphereItensList.Add(new SphereItemClass("i_Endurium_heater_shield", typeof(Server.Items.HeaterShieldEndurium)));
                sphereItensList.Add(new SphereItemClass("i_Endurium_closed_helm", typeof(Server.Items.PlateCloseHelmEndurium)));

                sphereItensList.Add(new SphereItemClass("i_Old_Endurium_platemail_leggings", typeof(Server.Items.PlateLegsOldEndurium)));
                sphereItensList.Add(new SphereItemClass("i_Old_Endurium_platemail_arms", typeof(Server.Items.PlateArmsOldEndurium)));
                sphereItensList.Add(new SphereItemClass("i_Old_Endurium_platemail_gorget", typeof(Server.Items.PlateGorgetOldEndurium)));
                sphereItensList.Add(new SphereItemClass("i_Old_Endurium_platemail_gauntlets", typeof(Server.Items.PlateGlovesOldEndurium)));
                sphereItensList.Add(new SphereItemClass("i_Old_Endurium_plate_helm", typeof(Server.Items.PlateCloseHelmOldEndurium)));
                sphereItensList.Add(new SphereItemClass("i_Old_Endurium_platemail", typeof(Server.Items.PlateChestOldEndurium)));
                sphereItensList.Add(new SphereItemClass("i_OldEndurium_heater_shield", typeof(Server.Items.HeaterShieldOldEndurium)));
                sphereItensList.Add(new SphereItemClass("i_OldEndurium_closed_helm", typeof(Server.Items.PlateCloseHelmOldEndurium)));

                sphereItensList.Add(new SphereItemClass("i_GoldStone_platemail_leggings", typeof(Server.Items.PlateLegsGoldStone)));
                sphereItensList.Add(new SphereItemClass("i_GoldStone_platemail_arms", typeof(Server.Items.PlateArmsGoldStone)));
                sphereItensList.Add(new SphereItemClass("i_GoldStone_platemail_gorget", typeof(Server.Items.PlateGorgetGoldStone)));
                sphereItensList.Add(new SphereItemClass("i_GoldStone_platemail_gauntlets", typeof(Server.Items.PlateGlovesGoldStone)));
                sphereItensList.Add(new SphereItemClass("i_GoldStone_plate_helm", typeof(Server.Items.PlateCloseHelmGoldStone)));
                sphereItensList.Add(new SphereItemClass("i_GoldStone_platemail", typeof(Server.Items.PlateChestGoldStone)));
                sphereItensList.Add(new SphereItemClass("i_GoldStone_heater_shield", typeof(Server.Items.HeaterShieldGoldStone)));
                sphereItensList.Add(new SphereItemClass("i_GoldStone_closed_helm", typeof(Server.Items.PlateCloseHelmGoldStone)));

                sphereItensList.Add(new SphereItemClass("i_MaxMytheril_platemail_leggings", typeof(Server.Items.PlateLegsMaxMytheril)));
                sphereItensList.Add(new SphereItemClass("i_MaxMytheril_platemail_arms", typeof(Server.Items.PlateArmsMaxMytheril)));
                sphereItensList.Add(new SphereItemClass("i_MaxMytheril_platemail_gorget", typeof(Server.Items.PlateGorgetMaxMytheril)));
                sphereItensList.Add(new SphereItemClass("i_MaxMytheril_platemail_gauntlets", typeof(Server.Items.PlateGlovesMaxMytheril)));
                sphereItensList.Add(new SphereItemClass("i_MaxMytheril_plate_helm", typeof(Server.Items.PlateCloseHelmMaxMytheril)));
                sphereItensList.Add(new SphereItemClass("i_MaxMytheril_platemail", typeof(Server.Items.PlateChestMaxMytheril)));
                sphereItensList.Add(new SphereItemClass("i_MaxMytheril_heater_shield", typeof(Server.Items.HeaterShieldMaxMytheril)));
                sphereItensList.Add(new SphereItemClass("i_MaxMytheril_closed_helm", typeof(Server.Items.PlateCloseHelmMaxMytheril)));

                sphereItensList.Add(new SphereItemClass("i_magma_platemail_leggings", typeof(Server.Items.PlateLegsMagma)));
                sphereItensList.Add(new SphereItemClass("i_magma_platemail_arms", typeof(Server.Items.PlateArmsMagma)));
                sphereItensList.Add(new SphereItemClass("i_magma_platemail_gorget", typeof(Server.Items.PlateGorgetMagma)));
                sphereItensList.Add(new SphereItemClass("i_magma_platemail_gauntlets", typeof(Server.Items.PlateGlovesMagma)));
                sphereItensList.Add(new SphereItemClass("i_magma_plate_helm", typeof(Server.Items.PlateCloseHelmMagma)));
                sphereItensList.Add(new SphereItemClass("i_magma_platemail", typeof(Server.Items.PlateChestMagma)));
                sphereItensList.Add(new SphereItemClass("i_magma_heater_shield", typeof(Server.Items.HeaterShieldMagma)));
                sphereItensList.Add(new SphereItemClass("i_magma_closed_helm", typeof(Server.Items.PlateCloseHelmMagma)));



                

                #endregion

                #region Arma Color

                sphereItensList.Add(new SphereItemClass("i_kryss_ocopper", typeof(Server.Items.KryssOldCopper)));
                sphereItensList.Add(new SphereItemClass("i_bow_ocopper", typeof(Server.Items.BowOldCopper)));
                sphereItensList.Add(new SphereItemClass("i_sword_ocopper", typeof(Server.Items.SwordOldCopper)));
                sphereItensList.Add(new SphereItemClass("i_mace_ocopper", typeof(Server.Items.WarMaceOldCopper)));

                sphereItensList.Add(new SphereItemClass("i_kryss_dcopper", typeof(Server.Items.KryssDullCopper)));
                sphereItensList.Add(new SphereItemClass("i_bow_dcopper", typeof(Server.Items.BowDullCopper)));
                sphereItensList.Add(new SphereItemClass("i_sword_dcopper", typeof(Server.Items.SwordDullCopper)));
                sphereItensList.Add(new SphereItemClass("i_mace_dcopper", typeof(Server.Items.WarMaceDullCopper)));

                sphereItensList.Add(new SphereItemClass("i_kryss_ruby", typeof(Server.Items.KryssRuby)));
                sphereItensList.Add(new SphereItemClass("i_bow_ruby", typeof(Server.Items.BowRuby)));
                sphereItensList.Add(new SphereItemClass("i_sword_ruby", typeof(Server.Items.SwordRuby)));
                sphereItensList.Add(new SphereItemClass("i_mace_ruby", typeof(Server.Items.WarMaceRuby)));

                sphereItensList.Add(new SphereItemClass("i_kryss_copper", typeof(Server.Items.KryssCopper)));
                sphereItensList.Add(new SphereItemClass("i_bow_copper", typeof(Server.Items.BowCopper)));
                sphereItensList.Add(new SphereItemClass("i_sword_copper", typeof(Server.Items.SwordCopper)));
                sphereItensList.Add(new SphereItemClass("i_mace_copper", typeof(Server.Items.WarMaceCopper)));

                sphereItensList.Add(new SphereItemClass("i_kryss_bronze", typeof(Server.Items.KryssBronze)));
                sphereItensList.Add(new SphereItemClass("i_bow_bronze", typeof(Server.Items.BowBronze)));
                sphereItensList.Add(new SphereItemClass("i_sword_bronze", typeof(Server.Items.SwordBronze)));
                sphereItensList.Add(new SphereItemClass("i_mace_bronze", typeof(Server.Items.WarMaceBronze)));

                sphereItensList.Add(new SphereItemClass("i_kryss_shadow", typeof(Server.Items.KryssShadow)));
                sphereItensList.Add(new SphereItemClass("i_bow_shadow", typeof(Server.Items.BowShadow)));
                sphereItensList.Add(new SphereItemClass("i_sword_shadow", typeof(Server.Items.SwordShadow)));
                sphereItensList.Add(new SphereItemClass("i_mace_shadow", typeof(Server.Items.WarMaceShadow)));

                sphereItensList.Add(new SphereItemClass("i_kryss_silv", typeof(Server.Items.KryssSilver)));
                sphereItensList.Add(new SphereItemClass("i_bow_silv", typeof(Server.Items.BowSilver)));
                sphereItensList.Add(new SphereItemClass("i_sword_silv", typeof(Server.Items.SwordSilver)));
                sphereItensList.Add(new SphereItemClass("i_mace_silv", typeof(Server.Items.WarMaceSilver)));

                sphereItensList.Add(new SphereItemClass("i_kryss_mercury", typeof(Server.Items.KryssMercury)));
                sphereItensList.Add(new SphereItemClass("i_bow_mercury", typeof(Server.Items.BowMercury)));
                sphereItensList.Add(new SphereItemClass("i_sword_mercury", typeof(Server.Items.SwordMercury)));
                sphereItensList.Add(new SphereItemClass("i_mace_mercury", typeof(Server.Items.WarMaceMercury)));

                sphereItensList.Add(new SphereItemClass("i_kryss_rose", typeof(Server.Items.KryssRose)));
                sphereItensList.Add(new SphereItemClass("i_bow_rose", typeof(Server.Items.BowRose)));
                sphereItensList.Add(new SphereItemClass("i_sword_rose", typeof(Server.Items.SwordRose)));
                sphereItensList.Add(new SphereItemClass("i_mace_rose", typeof(Server.Items.WarMaceRose)));

                sphereItensList.Add(new SphereItemClass("i_kryss_gold", typeof(Server.Items.KryssGold)));
                sphereItensList.Add(new SphereItemClass("i_bow_gold", typeof(Server.Items.BowGold)));
                sphereItensList.Add(new SphereItemClass("i_sword_gold", typeof(Server.Items.SwordGold)));
                sphereItensList.Add(new SphereItemClass("i_mace_gold", typeof(Server.Items.WarMaceGold)));

                sphereItensList.Add(new SphereItemClass("i_kryss_aga", typeof(Server.Items.KryssAgapite)));
                sphereItensList.Add(new SphereItemClass("i_bow_aga", typeof(Server.Items.BowAgapite)));
                sphereItensList.Add(new SphereItemClass("i_sword_aga", typeof(Server.Items.SwordAgapite)));
                sphereItensList.Add(new SphereItemClass("i_mace_aga", typeof(Server.Items.WarMaceAgapite)));

                sphereItensList.Add(new SphereItemClass("i_kryss_ver", typeof(Server.Items.KryssVerite)));
                sphereItensList.Add(new SphereItemClass("i_bow_ver", typeof(Server.Items.BowVerite)));
                sphereItensList.Add(new SphereItemClass("i_sword_ver", typeof(Server.Items.SwordVerite)));
                sphereItensList.Add(new SphereItemClass("i_mace_ver", typeof(Server.Items.WarMaceVerite)));

                sphereItensList.Add(new SphereItemClass("i_kryss_plutonium", typeof(Server.Items.KryssPlutonio)));
                sphereItensList.Add(new SphereItemClass("i_bow_plutonium", typeof(Server.Items.BowPlutonio)));
                sphereItensList.Add(new SphereItemClass("i_sword_plutonium", typeof(Server.Items.SwordPlutonio)));
                sphereItensList.Add(new SphereItemClass("i_mace_plutonium", typeof(Server.Items.WarMacePlutonio)));

                sphereItensList.Add(new SphereItemClass("i_kryss_br", typeof(Server.Items.KryssBloodRock)));
                sphereItensList.Add(new SphereItemClass("i_bow_br", typeof(Server.Items.BowBloodRock)));
                sphereItensList.Add(new SphereItemClass("i_sword_br", typeof(Server.Items.SwordBloodRock)));
                sphereItensList.Add(new SphereItemClass("i_mace_br", typeof(Server.Items.WarMaceBloodRock)));

                sphereItensList.Add(new SphereItemClass("i_kryss_val", typeof(Server.Items.KryssValorite)));
                sphereItensList.Add(new SphereItemClass("i_bow_val", typeof(Server.Items.BowValorite)));
                sphereItensList.Add(new SphereItemClass("i_sword_val", typeof(Server.Items.SwordValorite)));
                sphereItensList.Add(new SphereItemClass("i_mace_val", typeof(Server.Items.WarMaceValorite)));

                sphereItensList.Add(new SphereItemClass("i_kryss_blr", typeof(Server.Items.KryssBlackRock)));
                sphereItensList.Add(new SphereItemClass("i_bow_blr", typeof(Server.Items.BowBlackRock)));
                sphereItensList.Add(new SphereItemClass("i_sword_blr", typeof(Server.Items.SwordBlackRock)));
                sphereItensList.Add(new SphereItemClass("i_mace_blr", typeof(Server.Items.WarMaceBlackRock)));

                sphereItensList.Add(new SphereItemClass("i_kryss_myt", typeof(Server.Items.KryssMytheril)));
                sphereItensList.Add(new SphereItemClass("i_bow_myt", typeof(Server.Items.BowMytheril)));
                sphereItensList.Add(new SphereItemClass("i_sword_myt", typeof(Server.Items.SwordMytheril)));
                sphereItensList.Add(new SphereItemClass("i_mace_myt", typeof(Server.Items.WarMaceMytheril)));

                sphereItensList.Add(new SphereItemClass("i_kryss_aqua", typeof(Server.Items.KryssAqua)));
                sphereItensList.Add(new SphereItemClass("i_bow_aqua", typeof(Server.Items.BowAqua)));
                sphereItensList.Add(new SphereItemClass("i_sword_aqua", typeof(Server.Items.SwordAqua)));
                sphereItensList.Add(new SphereItemClass("i_mace_aqua", typeof(Server.Items.WarMaceAqua)));

                #endregion


                sphereItensList.Add(new SphereItemClass("i_ray_bow", typeof(Server.Items.RayBow)));
                sphereItensList.Add(new SphereItemClass("i_poison_bow", typeof(Server.Items.PoisonBow)));
                sphereItensList.Add(new SphereItemClass("i_adv_poison_bow", typeof(Server.Items.AdvancedPoisonBow)));
                sphereItensList.Add(new SphereItemClass("i_fire_bow", typeof(Server.Items.FireBow)));
                sphereItensList.Add(new SphereItemClass("i_Bow_Elven", typeof(Server.Items.ElvenBow)));
                

                //tailor
                sphereItensList.Add(new SphereItemClass("i_boots_camouflage", typeof(Server.Items.ThighBootsOfCamouflage)));
                sphereItensList.Add(new SphereItemClass("i_fishing_gloves", typeof(Server.Items.FishingGloves)));
                sphereItensList.Add(new SphereItemClass("i_medit_hat", typeof(Server.Items.MeditHat)));
                sphereItensList.Add(new SphereItemClass("I_FANCY_BLESS", typeof(Server.Items.FancyDressOfBless)));
                sphereItensList.Add(new SphereItemClass("I_ROBE_BLESS", typeof(Server.Items.RobeOfBless)));
                sphereItensList.Add(new SphereItemClass("I_SASH_STR", typeof(Server.Items.BodySashOfStrenght)));
                sphereItensList.Add(new SphereItemClass("I_ILLUS_CLOAK", typeof(Server.Items.CloakOfIllusion)));
                sphereItensList.Add(new SphereItemClass("I_PANTS_BRAIN", typeof(Server.Items.ShortPantsOfBrain)));
                sphereItensList.Add(new SphereItemClass("I_SHIRT_DEX", typeof(Server.Items.ShirtOfDexterity)));

                //tinker
                sphereItensList.Add(new SphereItemClass("I_BRACER_ALCH", typeof(Server.Items.BraceletAlchemy)));
                sphereItensList.Add(new SphereItemClass("I_BRACER_BS", typeof(Server.Items.BraceletBlacksmithing)));
                sphereItensList.Add(new SphereItemClass("I_BRACER_MINE", typeof(Server.Items.BraceletMining)));
                sphereItensList.Add(new SphereItemClass("I_BRACER_BOWC", typeof(Server.Items.BraceletBowcraft)));
                sphereItensList.Add(new SphereItemClass("I_BRACER_TAME", typeof(Server.Items.BraceletTaming)));
                sphereItensList.Add(new SphereItemClass("I_BRACER_LJ", typeof(Server.Items.BraceletLumberjacking)));
                sphereItensList.Add(new SphereItemClass("I_BRACER_SNOOP", typeof(Server.Items.BraceletSnooping)));
                sphereItensList.Add(new SphereItemClass("I_BRACER_TAIL", typeof(Server.Items.BraceletTailoring)));
                sphereItensList.Add(new SphereItemClass("I_BRACER_POIS", typeof(Server.Items.BraceletPoisoning)));
                sphereItensList.Add(new SphereItemClass("I_BRACER_STEAL", typeof(Server.Items.BraceletStealing)));
                sphereItensList.Add(new SphereItemClass("I_RING_DEX", typeof(Server.Items.RingDexterity)));
                sphereItensList.Add(new SphereItemClass("I_RING_STR", typeof(Server.Items.RingStrenght)));
                sphereItensList.Add(new SphereItemClass("I_RING_INT", typeof(Server.Items.RingInteligence)));
                sphereItensList.Add(new SphereItemClass("I_RING_POWER", typeof(Server.Items.RingPower)));
                sphereItensList.Add(new SphereItemClass("I_NECK_FENCE", typeof(Server.Items.NecklaceFencing)));
                sphereItensList.Add(new SphereItemClass("I_NECK_MACE", typeof(Server.Items.NecklaceMacefighting)));
                sphereItensList.Add(new SphereItemClass("I_NECK_SWORD", typeof(Server.Items.NecklaceSwordsmanship)));
                sphereItensList.Add(new SphereItemClass("I_NECK_ARCH", typeof(Server.Items.NecklaceArchery)));
                sphereItensList.Add(new SphereItemClass("I_NECK_TAC", typeof(Server.Items.NecklaceTactics)));
                sphereItensList.Add(new SphereItemClass("I_NECK_WRES", typeof(Server.Items.NecklaceWrestling)));

                // Wands
                sphereItensList.Add(new SphereItemClass("i_wand_chain_lightning", typeof(Server.Items.ChainLightningWand)));
                sphereItensList.Add(new SphereItemClass("i_wand_cure", typeof(Server.Items.CureWand)));
                sphereItensList.Add(new SphereItemClass("i_wand_energy_vortex", typeof(Server.Items.EnergyVortexWand)));
                sphereItensList.Add(new SphereItemClass("i_wand_explosion", typeof(Server.Items.ExplosionWand)));
                sphereItensList.Add(new SphereItemClass("i_wand_fire_field", typeof(Server.Items.FireFieldWand)));
                sphereItensList.Add(new SphereItemClass("i_wand_flamestrike", typeof(Server.Items.FlameStrikeWand)));
                sphereItensList.Add(new SphereItemClass("i_wand_paralyze_field", typeof(Server.Items.ParalyzeFieldWand)));
                sphereItensList.Add(new SphereItemClass("i_wand_paralyze", typeof(Server.Items.ParalyzeWand)));
                sphereItensList.Add(new SphereItemClass("i_wand_poison_field", typeof(Server.Items.PoisonFieldWand)));
                sphereItensList.Add(new SphereItemClass("i_wand_poison", typeof(Server.Items.PoisonWand)));
                sphereItensList.Add(new SphereItemClass("i_wand_resurrection", typeof(Server.Items.ResurrectionWand)));

                //sphereItensList.Add(new SphereItemClass("i_wand_incognito", typeof(Server.Items.ClumsyWand)));
                //sphereItensList.Add(new SphereItemClass("i_wand_incognito", typeof(Server.Items.FeebleWand)));
                sphereItensList.Add(new SphereItemClass("i_wand_fireball", typeof(Server.Items.FireballWand)));
                sphereItensList.Add(new SphereItemClass("i_wand_greater_heal", typeof(Server.Items.GreaterHealWand)));
                sphereItensList.Add(new SphereItemClass("i_wand_harm", typeof(Server.Items.HarmWand)));
                sphereItensList.Add(new SphereItemClass("i_wand_heal", typeof(Server.Items.HealWand)));
                //sphereItensList.Add(new SphereItemClass("i_wand_incognito", typeof(Server.Items.IDWand)));
                sphereItensList.Add(new SphereItemClass("i_wand_lightning", typeof(Server.Items.LightningWand)));
                sphereItensList.Add(new SphereItemClass("i_wand_magic_arrow", typeof(Server.Items.MagicArrowWand)));
                sphereItensList.Add(new SphereItemClass("i_wand_mana_drain", typeof(Server.Items.ManaDrainWand)));
                //sphereItensList.Add(new SphereItemClass("i_wand_incognito", typeof(Server.Items.WeaknessWand)));



                // Misc
                sphereItensList.Add(new SphereItemClass("i_arrow", typeof(Server.Items.Arrow)));
                sphereItensList.Add(new SphereItemClass("i_xbolt", typeof(Server.Items.Bolt)));
                sphereItensList.Add(new SphereItemClass("i_bandage", typeof(Server.Items.Bandage)));
                sphereItensList.Add(new SphereItemClass("i_cape", typeof(Server.Items.Cloak)));
                sphereItensList.Add(new SphereItemClass("i_robe", typeof(Server.Items.Robe)));
                sphereItensList.Add(new SphereItemClass("i_forge", typeof(SmallForgeDeed)));
                sphereItensList.Add(new SphereItemClass("i_anvil", typeof(AnvilEastDeed)));
                sphereItensList.Add(new SphereItemClass("i_dress_full", typeof(Server.Items.FancyDress)));
                sphereItensList.Add(new SphereItemClass("i_dress_plain", typeof(Server.Items.PlainDress)));
                sphereItensList.Add(new SphereItemClass("i_pants_short", typeof(Server.Items.ShortPants)));
                sphereItensList.Add(new SphereItemClass("i_shirt_plain", typeof(Server.Items.Shirt)));

                sphereItensList.Add(new SphereItemClass("i_cofre", typeof(Server.Items.Cofre)));
                sphereItensList.Add(new SphereItemClass("i_chest_metal", typeof(Server.Items.MetalChest)));
                sphereItensList.Add(new SphereItemClass("i_chest_metal_brass", typeof(Server.Items.MetalChest)));
                sphereItensList.Add(new SphereItemClass("i_chest_wooden_brass", typeof(Server.Items.WoodenChest)));
                sphereItensList.Add(new SphereItemClass("i_chest_wooden", typeof(Server.Items.WoodenChest)));

                sphereItensList.Add(new SphereItemClass("i_mapa_tesouro", typeof(Server.Items.TreasureMap)));
                
                
                

                // Magic
                sphereItensList.Add(new SphereItemClass("i_dye_tub", typeof(Server.Items.MagicDyeTub)));
                
                // Hide
                sphereItensList.Add(new SphereItemClass("i_dragon_hide", typeof(Server.Items.DragonHides)));
                sphereItensList.Add(new SphereItemClass("i_zz_hide", typeof(Server.Items.ZZHides)));
                sphereItensList.Add(new SphereItemClass("i_daemon_hide", typeof(Server.Items.DaemonHides)));
                sphereItensList.Add(new SphereItemClass("i_cloth", typeof(Server.Items.Cloth)));
                sphereItensList.Add(new SphereItemClass("i_hide", typeof(Server.Items.Hides)));
                sphereItensList.Add(new SphereItemClass("i_cyclops_hide", typeof(Server.Items.CyclopsHides)));
                sphereItensList.Add(new SphereItemClass("i_terathan_hide", typeof(Server.Items.TerathanHides)));
                
                    

                //Logs
                sphereItensList.Add(new SphereItemClass("i_log", typeof(Server.Items.Log)));
                sphereItensList.Add(new SphereItemClass("i_log_fine", typeof(Server.Items.FineLog)));
                sphereItensList.Add(new SphereItemClass("i_log_poison", typeof(Server.Items.PoisonLog)));
                sphereItensList.Add(new SphereItemClass("i_log_fire", typeof(Server.Items.FireLog)));

                


                // Regs
                sphereItensList.Add(new SphereItemClass("i_reag_black_pearl", typeof(Server.Items.BlackPearl)));
                sphereItensList.Add(new SphereItemClass("i_reag_blood_moss", typeof(Server.Items.Bloodmoss)));
                sphereItensList.Add(new SphereItemClass("i_reag_garlic", typeof(Server.Items.Garlic)));
                sphereItensList.Add(new SphereItemClass("i_reag_ginseng", typeof(Server.Items.Ginseng)));
                sphereItensList.Add(new SphereItemClass("i_reag_mandrake_root", typeof(Server.Items.MandrakeRoot)));
                sphereItensList.Add(new SphereItemClass("i_reag_nightshade", typeof(Server.Items.Nightshade)));
                sphereItensList.Add(new SphereItemClass("i_reag_sulfur_ash", typeof(Server.Items.SulfurousAsh)));
                sphereItensList.Add(new SphereItemClass("i_reag_spider_silk", typeof(Server.Items.SpidersSilk)));

                // Deed
                sphereItensList.Add(new SphereItemClass("i_deed_tower", typeof(Server.Multis.Deeds.TowerDeed)));
                sphereItensList.Add(new SphereItemClass("i_deed_keep", typeof(Server.Multis.Deeds.KeepDeed)));


                // Potion
                sphereItensList.Add(new SphereItemClass("i_potion_shrink", typeof(Server.Items.ShrinkPotion)));
                sphereItensList.Add(new SphereItemClass("i_potion_manatotal", typeof(Server.Items.GreaterManaPotion)));
                sphereItensList.Add(new SphereItemClass("i_potion_heal", typeof(Server.Items.HealPotion)));
                sphereItensList.Add(new SphereItemClass("i_potion_cure", typeof(Server.Items.CurePotion)));
                sphereItensList.Add(new SphereItemClass("i_potion_refreshtotal", typeof(Server.Items.TotalRefreshPotion)));
                sphereItensList.Add(new SphereItemClass("i_potion_refresh", typeof(Server.Items.RefreshPotion)));
                sphereItensList.Add(new SphereItemClass("i_potion_agility", typeof(Server.Items.AgilityPotion)));
                sphereItensList.Add(new SphereItemClass("i_potion_explosion", typeof(Server.Items.ExplosionPotion)));
                sphereItensList.Add(new SphereItemClass("i_potion_healgreat", typeof(Server.Items.GreaterHealPotion)));
                sphereItensList.Add(new SphereItemClass("i_potion_poisondeadly", typeof(Server.Items.DeadlyPoisonPotion)));
                sphereItensList.Add(new SphereItemClass("i_potion_strengthgreat", typeof(Server.Items.GreaterStrengthPotion)));

                // Pets
                sphereItensList.Add(new SphereItemClass("i_pet_horse_brown_dk", typeof(Server.Mobiles.Horse)));
                sphereItensList.Add(new SphereItemClass("i_pet_ostard_forest", typeof(Server.Mobiles.Orn)));
                sphereItensList.Add(new SphereItemClass("i_pet_llama", typeof(Server.Mobiles.Llama)));
                sphereItensList.Add(new SphereItemClass("i_pet_horse_tan", typeof(Server.Mobiles.Horse)));
                sphereItensList.Add(new SphereItemClass("i_pet_horse_gray", typeof(Server.Mobiles.Horse)));
                sphereItensList.Add(new SphereItemClass("i_pet_ostard_zostrich", typeof(Server.Mobiles.Zostrich)));
                sphereItensList.Add(new SphereItemClass("i_mt_horse_brown_lt", typeof(Server.Mobiles.Horse)));
                    


                return sphereItensList;
                
            }
        }

    }

    public class SphereItemClass
    {
        
        public string sphereID { get; set; }
        public Type itemType { get; set; }
        public int qtAmount { get; set; }
        public int Hue { get; set; }
        public string sphereSerial { get; set; }

        public SphereItemClass(string pSphereID, Type pItemType)
        {
            sphereID = pSphereID;
            itemType = pItemType;
            qtAmount = 1;
            Hue = 1;
        }

        public SphereItemClass(string pSphereID, Type pItemType, int QtAmount, int pHue)
        {
            sphereID = pSphereID;
            itemType = pItemType;
            qtAmount = QtAmount;
            Hue = pHue;
        }

    }


}
