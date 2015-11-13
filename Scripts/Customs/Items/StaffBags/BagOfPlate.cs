using System; 
using Server; 
using Server.Items;
using System.Collections.Generic;

namespace Server.Items 
{ 
	public class BagOfPlate : Bag 
	{ 
		[Constructable] 
		public BagOfPlate() : this( 1 ) 
		{ 
		} 

		[Constructable] 
		public BagOfPlate( int amount ) 
		{
            this.Name = "Bag Of Plates";
            this.Hue = DimensionsNewAge.Scripts.HueItemConst.HueMagicColorRandom;

            Bag bagPlateRusty = new Bag();
            bagPlateRusty.DropItem(new PlateChestRusty());
            bagPlateRusty.DropItem(new PlateArmsRusty());
            bagPlateRusty.DropItem(new PlateLegsRusty());
            bagPlateRusty.DropItem(new PlateCloseHelmRusty());
            bagPlateRusty.DropItem(new PlateGorgetRusty());
            bagPlateRusty.DropItem(new PlateGlovesRusty());
            bagPlateRusty.DropItem(new SwordRusty());
            bagPlateRusty.DropItem(new WarMaceRusty());
            bagPlateRusty.DropItem(new KryssRusty());
            bagPlateRusty.DropItem(new WarMaceRusty());
            bagPlateRusty.DropItem(new HeaterShieldRusty());
            bagPlateRusty.DropItem(new BowRusty());
            bagPlateRusty.Hue = DimensionsNewAge.Scripts.HueOreConst.HueRusty;
            this.DropItem(bagPlateRusty);

            Bag bagPlateOldCopper = new Bag();
            bagPlateOldCopper.DropItem(new PlateChestOldCopper());
            bagPlateOldCopper.DropItem(new PlateArmsOldCopper());
            bagPlateOldCopper.DropItem(new PlateLegsOldCopper());
            bagPlateOldCopper.DropItem(new PlateCloseHelmOldCopper());
            bagPlateOldCopper.DropItem(new PlateGorgetOldCopper());
            bagPlateOldCopper.DropItem(new PlateGlovesOldCopper());
            bagPlateOldCopper.DropItem(new SwordOldCopper());
            bagPlateOldCopper.DropItem(new WarMaceOldCopper());
            bagPlateOldCopper.DropItem(new KryssOldCopper());
            bagPlateOldCopper.DropItem(new WarMaceOldCopper());
            bagPlateOldCopper.DropItem(new HeaterShieldOldCopper());
            bagPlateOldCopper.DropItem(new BowOldCopper());
            bagPlateOldCopper.Hue = DimensionsNewAge.Scripts.HueOreConst.HueOldCopper;
            this.DropItem(bagPlateOldCopper);

            Bag bagPlateDullCopper = new Bag();
            bagPlateDullCopper.DropItem(new PlateChestDullCopper());
            bagPlateDullCopper.DropItem(new PlateArmsDullCopper());
            bagPlateDullCopper.DropItem(new PlateLegsDullCopper());
            bagPlateDullCopper.DropItem(new PlateCloseHelmDullCopper());
            bagPlateDullCopper.DropItem(new PlateGorgetDullCopper());
            bagPlateDullCopper.DropItem(new PlateGlovesDullCopper());
            bagPlateDullCopper.DropItem(new SwordDullCopper());
            bagPlateDullCopper.DropItem(new WarMaceDullCopper());
            bagPlateDullCopper.DropItem(new KryssDullCopper());
            bagPlateDullCopper.DropItem(new WarMaceDullCopper());
            bagPlateDullCopper.DropItem(new HeaterShieldDullCopper());
            bagPlateDullCopper.DropItem(new BowDullCopper());
            bagPlateDullCopper.Hue = DimensionsNewAge.Scripts.HueOreConst.HueDullCopper;
            this.DropItem(bagPlateDullCopper);

            Bag bagPlateRuby = new Bag();
            bagPlateRuby.DropItem(new PlateChestRuby());
            bagPlateRuby.DropItem(new PlateArmsRuby());
            bagPlateRuby.DropItem(new PlateLegsRuby());
            bagPlateRuby.DropItem(new PlateCloseHelmRuby());
            bagPlateRuby.DropItem(new PlateGorgetRuby());
            bagPlateRuby.DropItem(new PlateGlovesRuby());
            bagPlateRuby.DropItem(new SwordRuby());
            bagPlateRuby.DropItem(new WarMaceRuby());
            bagPlateRuby.DropItem(new KryssRuby());
            bagPlateRuby.DropItem(new WarMaceRuby());
            bagPlateRuby.DropItem(new HeaterShieldRuby());
            bagPlateRuby.DropItem(new BowRuby());
            bagPlateRuby.Hue = DimensionsNewAge.Scripts.HueOreConst.HueRuby;
            this.DropItem(bagPlateRuby);

            Bag bagPlateCopper = new Bag();
            bagPlateCopper.DropItem(new PlateChestCopper());
            bagPlateCopper.DropItem(new PlateArmsCopper());
            bagPlateCopper.DropItem(new PlateLegsCopper());
            bagPlateCopper.DropItem(new PlateCloseHelmCopper());
            bagPlateCopper.DropItem(new PlateGorgetCopper());
            bagPlateCopper.DropItem(new PlateGlovesCopper());
            bagPlateCopper.DropItem(new SwordCopper());
            bagPlateCopper.DropItem(new WarMaceCopper());
            bagPlateCopper.DropItem(new KryssCopper());
            bagPlateCopper.DropItem(new WarMaceCopper());
            bagPlateCopper.DropItem(new HeaterShieldCopper());
            bagPlateCopper.DropItem(new BowCopper());
            bagPlateCopper.Hue = DimensionsNewAge.Scripts.HueOreConst.HueCopper;
            this.DropItem(bagPlateCopper);

            Bag bagPlateBronze = new Bag();
            bagPlateBronze.DropItem(new PlateChestBronze());
            bagPlateBronze.DropItem(new PlateArmsBronze());
            bagPlateBronze.DropItem(new PlateLegsBronze());
            bagPlateBronze.DropItem(new PlateCloseHelmBronze());
            bagPlateBronze.DropItem(new PlateGorgetBronze());
            bagPlateBronze.DropItem(new PlateGlovesBronze());
            bagPlateBronze.DropItem(new SwordBronze());
            bagPlateBronze.DropItem(new WarMaceBronze());
            bagPlateBronze.DropItem(new KryssBronze());
            bagPlateBronze.DropItem(new WarMaceBronze());
            bagPlateBronze.DropItem(new HeaterShieldBronze());
            bagPlateBronze.DropItem(new BowBronze());
            bagPlateBronze.Hue = DimensionsNewAge.Scripts.HueOreConst.HueBronze;
            this.DropItem(bagPlateBronze);

            Bag bagPlateShadowIron = new Bag();
            bagPlateShadowIron.DropItem(new PlateChestShadow());
            bagPlateShadowIron.DropItem(new PlateArmsShadow());
            bagPlateShadowIron.DropItem(new PlateLegsShadow());
            bagPlateShadowIron.DropItem(new PlateCloseHelmShadow());
            bagPlateShadowIron.DropItem(new PlateGorgetShadow());
            bagPlateShadowIron.DropItem(new PlateGlovesShadow());
            bagPlateShadowIron.DropItem(new SwordShadow());
            bagPlateShadowIron.DropItem(new WarMaceShadow());
            bagPlateShadowIron.DropItem(new KryssShadow());
            bagPlateShadowIron.DropItem(new WarMaceShadow());
            bagPlateShadowIron.DropItem(new HeaterShieldShadow());
            bagPlateShadowIron.DropItem(new BowShadow());
            bagPlateShadowIron.Hue = DimensionsNewAge.Scripts.HueOreConst.HueShadow;
            this.DropItem(bagPlateShadowIron);

            Bag bagPlateSilver = new Bag();
            bagPlateSilver.DropItem(new PlateChestSilver());
            bagPlateSilver.DropItem(new PlateArmsSilver());
            bagPlateSilver.DropItem(new PlateLegsSilver());
            bagPlateSilver.DropItem(new PlateCloseHelmSilver());
            bagPlateSilver.DropItem(new PlateGorgetSilver());
            bagPlateSilver.DropItem(new PlateGlovesSilver());
            bagPlateSilver.DropItem(new SwordSilver());
            bagPlateSilver.DropItem(new WarMaceSilver());
            bagPlateSilver.DropItem(new KryssSilver());
            bagPlateSilver.DropItem(new WarMaceSilver());
            bagPlateSilver.DropItem(new HeaterShieldSilver());
            bagPlateSilver.DropItem(new BowSilver());
            bagPlateSilver.Hue = DimensionsNewAge.Scripts.HueOreConst.HueSilver;
            this.DropItem(bagPlateSilver);

            Bag bagPlateMercury = new Bag();
            bagPlateMercury.DropItem(new PlateChestMercury());
            bagPlateMercury.DropItem(new PlateArmsMercury());
            bagPlateMercury.DropItem(new PlateLegsMercury());
            bagPlateMercury.DropItem(new PlateCloseHelmMercury());
            bagPlateMercury.DropItem(new PlateGorgetMercury());
            bagPlateMercury.DropItem(new PlateGlovesMercury());
            bagPlateMercury.DropItem(new SwordMercury());
            bagPlateMercury.DropItem(new WarMaceMercury());
            bagPlateMercury.DropItem(new KryssMercury());
            bagPlateMercury.DropItem(new WarMaceMercury());
            bagPlateMercury.DropItem(new HeaterShieldMercury());
            bagPlateMercury.DropItem(new BowMercury());
            bagPlateMercury.Hue = DimensionsNewAge.Scripts.HueOreConst.HueMercury;
            this.DropItem(bagPlateMercury);

            Bag bagPlateRose = new Bag();
            bagPlateRose.DropItem(new PlateChestRose());
            bagPlateRose.DropItem(new PlateArmsRose());
            bagPlateRose.DropItem(new PlateLegsRose());
            bagPlateRose.DropItem(new PlateCloseHelmRose());
            bagPlateRose.DropItem(new PlateGorgetRose());
            bagPlateRose.DropItem(new PlateGlovesRose());
            bagPlateRose.DropItem(new SwordRose());
            bagPlateRose.DropItem(new WarMaceRose());
            bagPlateRose.DropItem(new KryssRose());
            bagPlateRose.DropItem(new WarMaceRose());
            bagPlateRose.DropItem(new HeaterShieldRose());
            bagPlateRose.DropItem(new BowRose());
            bagPlateRose.Hue = DimensionsNewAge.Scripts.HueOreConst.HueRose;
            this.DropItem(bagPlateRose);

            Bag bagPlateGold = new Bag();
            bagPlateGold.DropItem(new PlateChestGold());
            bagPlateGold.DropItem(new PlateArmsGold());
            bagPlateGold.DropItem(new PlateLegsGold());
            bagPlateGold.DropItem(new PlateCloseHelmGold());
            bagPlateGold.DropItem(new PlateGorgetGold());
            bagPlateGold.DropItem(new PlateGlovesGold());
            bagPlateGold.DropItem(new SwordGold());
            bagPlateGold.DropItem(new WarMaceGold());
            bagPlateGold.DropItem(new KryssGold());
            bagPlateGold.DropItem(new WarMaceGold());
            bagPlateGold.DropItem(new HeaterShieldGold());
            bagPlateGold.DropItem(new BowGold());
            bagPlateGold.Hue = DimensionsNewAge.Scripts.HueOreConst.HueGold;
            this.DropItem(bagPlateGold);

            Bag bagPlateAgapite = new Bag();
            bagPlateAgapite.DropItem(new PlateChestAgapite());
            bagPlateAgapite.DropItem(new PlateArmsAgapite());
            bagPlateAgapite.DropItem(new PlateLegsAgapite());
            bagPlateAgapite.DropItem(new PlateCloseHelmAgapite());
            bagPlateAgapite.DropItem(new PlateGorgetAgapite());
            bagPlateAgapite.DropItem(new PlateGlovesAgapite());
            bagPlateAgapite.DropItem(new SwordAgapite());
            bagPlateAgapite.DropItem(new WarMaceAgapite());
            bagPlateAgapite.DropItem(new KryssAgapite());
            bagPlateAgapite.DropItem(new WarMaceAgapite());
            bagPlateAgapite.DropItem(new HeaterShieldAgapite());
            bagPlateAgapite.DropItem(new BowAgapite());
            bagPlateAgapite.Hue = DimensionsNewAge.Scripts.HueOreConst.HueAgapite;
            this.DropItem(bagPlateAgapite);

            Bag bagPlateVerite = new Bag();
            bagPlateVerite.DropItem(new PlateChestVerite());
            bagPlateVerite.DropItem(new PlateArmsVerite());
            bagPlateVerite.DropItem(new PlateLegsVerite());
            bagPlateVerite.DropItem(new PlateCloseHelmVerite());
            bagPlateVerite.DropItem(new PlateGorgetVerite());
            bagPlateVerite.DropItem(new PlateGlovesVerite());
            bagPlateVerite.DropItem(new SwordVerite());
            bagPlateVerite.DropItem(new WarMaceVerite());
            bagPlateVerite.DropItem(new KryssVerite());
            bagPlateVerite.DropItem(new WarMaceVerite());
            bagPlateVerite.DropItem(new HeaterShieldVerite());
            bagPlateVerite.DropItem(new BowVerite());
            bagPlateVerite.Hue = DimensionsNewAge.Scripts.HueOreConst.HueVerite;
            this.DropItem(bagPlateVerite);

            Bag bagPlatePlutonio = new Bag();
            bagPlatePlutonio.DropItem(new PlateChestPlutonio());
            bagPlatePlutonio.DropItem(new PlateArmsPlutonio());
            bagPlatePlutonio.DropItem(new PlateLegsPlutonio());
            bagPlatePlutonio.DropItem(new PlateCloseHelmPlutonio());
            bagPlatePlutonio.DropItem(new PlateGorgetPlutonio());
            bagPlatePlutonio.DropItem(new PlateGlovesPlutonio());
            bagPlatePlutonio.DropItem(new SwordPlutonio());
            bagPlatePlutonio.DropItem(new WarMacePlutonio());
            bagPlatePlutonio.DropItem(new KryssPlutonio());
            bagPlatePlutonio.DropItem(new WarMacePlutonio());
            bagPlatePlutonio.DropItem(new HeaterShieldPlutonio());
            bagPlatePlutonio.DropItem(new BowPlutonio());
            bagPlatePlutonio.Hue = DimensionsNewAge.Scripts.HueOreConst.HuePlutonio;
            this.DropItem(bagPlatePlutonio);

            Bag bagPlateBloodRock = new Bag();
            bagPlateBloodRock.DropItem(new PlateChestBloodRock());
            bagPlateBloodRock.DropItem(new PlateArmsBloodRock());
            bagPlateBloodRock.DropItem(new PlateLegsBloodRock());
            bagPlateBloodRock.DropItem(new PlateCloseHelmBloodRock());
            bagPlateBloodRock.DropItem(new PlateGorgetBloodRock());
            bagPlateBloodRock.DropItem(new PlateGlovesBloodRock());
            bagPlateBloodRock.DropItem(new SwordBloodRock());
            bagPlateBloodRock.DropItem(new WarMaceBloodRock());
            bagPlateBloodRock.DropItem(new KryssBloodRock());
            bagPlateBloodRock.DropItem(new WarMaceBloodRock());
            bagPlateBloodRock.DropItem(new HeaterShieldBloodRock());
            bagPlateBloodRock.DropItem(new BowBloodRock());
            bagPlateBloodRock.Hue = DimensionsNewAge.Scripts.HueOreConst.HueBloodRock;
            this.DropItem(bagPlateBloodRock);

            Bag bagPlateValorite = new Bag();
            bagPlateValorite.DropItem(new PlateChestValorite());
            bagPlateValorite.DropItem(new PlateArmsValorite());
            bagPlateValorite.DropItem(new PlateLegsValorite());
            bagPlateValorite.DropItem(new PlateCloseHelmValorite());
            bagPlateValorite.DropItem(new PlateGorgetValorite());
            bagPlateValorite.DropItem(new PlateGlovesValorite());
            bagPlateValorite.DropItem(new SwordValorite());
            bagPlateValorite.DropItem(new WarMaceValorite());
            bagPlateValorite.DropItem(new KryssValorite());
            bagPlateValorite.DropItem(new WarMaceValorite());
            bagPlateValorite.DropItem(new HeaterShieldValorite());
            bagPlateValorite.DropItem(new BowValorite());
            bagPlateValorite.Hue = DimensionsNewAge.Scripts.HueOreConst.HueValorite;
            this.DropItem(bagPlateValorite);

            Bag bagPlateBlackRock = new Bag();
            bagPlateBlackRock.DropItem(new PlateChestBlackRock());
            bagPlateBlackRock.DropItem(new PlateArmsBlackRock());
            bagPlateBlackRock.DropItem(new PlateLegsBlackRock());
            bagPlateBlackRock.DropItem(new PlateCloseHelmBlackRock());
            bagPlateBlackRock.DropItem(new PlateGorgetBlackRock());
            bagPlateBlackRock.DropItem(new PlateGlovesBlackRock());
            bagPlateBlackRock.DropItem(new SwordBlackRock());
            bagPlateBlackRock.DropItem(new WarMaceBlackRock());
            bagPlateBlackRock.DropItem(new KryssBlackRock());
            bagPlateBlackRock.DropItem(new WarMaceBlackRock());
            bagPlateBlackRock.DropItem(new HeaterShieldBlackRock());
            bagPlateBlackRock.DropItem(new BowBlackRock());
            bagPlateBlackRock.Hue = DimensionsNewAge.Scripts.HueOreConst.HueBlackRock;
            this.DropItem(bagPlateBlackRock);

            Bag bagPlateMytheril = new Bag();
            bagPlateMytheril.DropItem(new PlateChestMytheril());
            bagPlateMytheril.DropItem(new PlateArmsMytheril());
            bagPlateMytheril.DropItem(new PlateLegsMytheril());
            bagPlateMytheril.DropItem(new PlateCloseHelmMytheril());
            bagPlateMytheril.DropItem(new PlateGorgetMytheril());
            bagPlateMytheril.DropItem(new PlateGlovesMytheril());
            bagPlateMytheril.DropItem(new SwordMytheril());
            bagPlateMytheril.DropItem(new WarMaceMytheril());
            bagPlateMytheril.DropItem(new KryssMytheril());
            bagPlateMytheril.DropItem(new WarMaceMytheril());
            bagPlateMytheril.DropItem(new HeaterShieldMytheril());
            bagPlateMytheril.DropItem(new BowMytheril());
            bagPlateMytheril.Hue = DimensionsNewAge.Scripts.HueOreConst.HueMytheril;
            this.DropItem(bagPlateMytheril);

            Bag bagPlateAqua = new Bag();
            bagPlateAqua.DropItem(new PlateChestAqua());
            bagPlateAqua.DropItem(new PlateArmsAqua());
            bagPlateAqua.DropItem(new PlateLegsAqua());
            bagPlateAqua.DropItem(new PlateCloseHelmAqua());
            bagPlateAqua.DropItem(new PlateGorgetAqua());
            bagPlateAqua.DropItem(new PlateGlovesAqua());
            bagPlateAqua.DropItem(new SwordAqua());
            bagPlateAqua.DropItem(new WarMaceAqua());
            bagPlateAqua.DropItem(new KryssAqua());
            bagPlateAqua.DropItem(new WarMaceAqua());
            bagPlateAqua.DropItem(new HeaterShieldAqua());
            bagPlateAqua.DropItem(new BowAqua());
            bagPlateAqua.Hue = DimensionsNewAge.Scripts.HueOreConst.HueAqua;
            this.DropItem(bagPlateAqua);

		}

        public BagOfPlate(Serial serial)
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
} 
