using System;
using System.Collections;

namespace Server.Items
{
	public enum CraftResource
	{
		None = 0,
		Iron = 1,
        Rusty,
        OldCopper,
		DullCopper,
        Ruby,
        Copper,
        Bronze,
		ShadowIron,
        Silver,
        Mercury,
        Rose,
		Gold,
		Agapite,
		Verite,
        Plutonio,
        BloodRock,
        Valorite,
        BlackRock,
        Mytheril,
        Aqua,
        Endurium,
        OldEndurium,
        GoldStone,
        MaxMytheril,
        Magma,

		RegularLeather = 101,
		SpinedLeather,
		HornedLeather,
		BarbedLeather,
        CyclopsLeather,
        GargoyleLeather,
        TerathanLeather,
        DaemonLeather,
        DragonLeather,
        ZZLeather,
        DragonGreenLeather,

		RedScales = 201,
		YellowScales,
		BlackScales,
		GreenScales,
		WhiteScales,
		BlueScales,

		RegularWood = 301,
		OakWood,
		AshWood,
		YewWood,
		Heartwood,
		Bloodwood,
		Frostwood,
        FineWood,
        PoisonWood,
        FireWood
	}

	public enum CraftResourceType
	{
		None,
		Metal,
		Leather,
		Scales,
		Wood
	}

	public class CraftAttributeInfo
	{
		private int m_WeaponFireDamage;
		private int m_WeaponColdDamage;
		private int m_WeaponPoisonDamage;
		private int m_WeaponEnergyDamage;
		private int m_WeaponChaosDamage;
		private int m_WeaponDirectDamage;
		private int m_WeaponDurability;
		private int m_WeaponLuck;
		private int m_WeaponGoldIncrease;
		private int m_WeaponLowerRequirements;

		private int m_ArmorPhysicalResist;
		private int m_ArmorFireResist;
		private int m_ArmorColdResist;
		private int m_ArmorPoisonResist;
		private int m_ArmorEnergyResist;
		private int m_ArmorDurability;
		private int m_ArmorLuck;
		private int m_ArmorGoldIncrease;
		private int m_ArmorLowerRequirements;

		private int m_RunicMinAttributes;
		private int m_RunicMaxAttributes;
		private int m_RunicMinIntensity;
		private int m_RunicMaxIntensity;

		public int WeaponFireDamage{ get{ return m_WeaponFireDamage; } set{ m_WeaponFireDamage = value; } }
		public int WeaponColdDamage{ get{ return m_WeaponColdDamage; } set{ m_WeaponColdDamage = value; } }
		public int WeaponPoisonDamage{ get{ return m_WeaponPoisonDamage; } set{ m_WeaponPoisonDamage = value; } }
		public int WeaponEnergyDamage{ get{ return m_WeaponEnergyDamage; } set{ m_WeaponEnergyDamage = value; } }
		public int WeaponChaosDamage{ get{ return m_WeaponChaosDamage; } set{ m_WeaponChaosDamage = value; } }
		public int WeaponDirectDamage{ get{ return m_WeaponDirectDamage; } set{ m_WeaponDirectDamage = value; } }
		public int WeaponDurability{ get{ return m_WeaponDurability; } set{ m_WeaponDurability = value; } }
		public int WeaponLuck{ get{ return m_WeaponLuck; } set{ m_WeaponLuck = value; } }
		public int WeaponGoldIncrease{ get{ return m_WeaponGoldIncrease; } set{ m_WeaponGoldIncrease = value; } }
		public int WeaponLowerRequirements{ get{ return m_WeaponLowerRequirements; } set{ m_WeaponLowerRequirements = value; } }

		public int ArmorPhysicalResist{ get{ return m_ArmorPhysicalResist; } set{ m_ArmorPhysicalResist = value; } }
		public int ArmorFireResist{ get{ return m_ArmorFireResist; } set{ m_ArmorFireResist = value; } }
		public int ArmorColdResist{ get{ return m_ArmorColdResist; } set{ m_ArmorColdResist = value; } }
		public int ArmorPoisonResist{ get{ return m_ArmorPoisonResist; } set{ m_ArmorPoisonResist = value; } }
		public int ArmorEnergyResist{ get{ return m_ArmorEnergyResist; } set{ m_ArmorEnergyResist = value; } }
		public int ArmorDurability{ get{ return m_ArmorDurability; } set{ m_ArmorDurability = value; } }
		public int ArmorLuck{ get{ return m_ArmorLuck; } set{ m_ArmorLuck = value; } }
		public int ArmorGoldIncrease{ get{ return m_ArmorGoldIncrease; } set{ m_ArmorGoldIncrease = value; } }
		public int ArmorLowerRequirements{ get{ return m_ArmorLowerRequirements; } set{ m_ArmorLowerRequirements = value; } }

		public int RunicMinAttributes{ get{ return m_RunicMinAttributes; } set{ m_RunicMinAttributes = value; } }
		public int RunicMaxAttributes{ get{ return m_RunicMaxAttributes; } set{ m_RunicMaxAttributes = value; } }
		public int RunicMinIntensity{ get{ return m_RunicMinIntensity; } set{ m_RunicMinIntensity = value; } }
		public int RunicMaxIntensity{ get{ return m_RunicMaxIntensity; } set{ m_RunicMaxIntensity = value; } }

		public CraftAttributeInfo()
		{
		}

		public static readonly CraftAttributeInfo Blank;
        public static readonly CraftAttributeInfo Rusty, OldCopper, DullCopper, Ruby, Copper, Bronze, ShadowIron, Silver, Mercury, Rose, Gold, Agapite, Verite, Plutonio, BloodRock, Valorite, BlackRock, Mytheril, Aqua, Endurium, OldEndurium, GoldStone, MaxMytheril, Magma;
		public static readonly CraftAttributeInfo Spined, Horned, Barbed, Cyclops, Gargoyle, Terathan, Daemon, Dragon, ZZ, DragonGreen;
		public static readonly CraftAttributeInfo RedScales, YellowScales, BlackScales, GreenScales, WhiteScales, BlueScales;
		public static readonly CraftAttributeInfo OakWood, AshWood, YewWood, Heartwood, Bloodwood, Frostwood, FineWood, PoisonWood, FireWood;

		static CraftAttributeInfo()
		{
			Blank = new CraftAttributeInfo();

            CraftAttributeInfo rusty = Rusty = new CraftAttributeInfo();
            rusty.ArmorDurability = 50;
            rusty.ArmorLowerRequirements = 20;
            rusty.WeaponDurability = 100;
            rusty.WeaponLowerRequirements = 50;

            CraftAttributeInfo oldCopper = OldCopper = new CraftAttributeInfo();
            oldCopper.ArmorDurability = 50;
            oldCopper.ArmorLowerRequirements = 20;
            oldCopper.WeaponDurability = 100;
            oldCopper.WeaponLowerRequirements = 50;

			CraftAttributeInfo dullCopper = DullCopper = new CraftAttributeInfo();
			dullCopper.ArmorDurability = 50;
			dullCopper.ArmorLowerRequirements = 20;
			dullCopper.WeaponDurability = 100;
			dullCopper.WeaponLowerRequirements = 50;

            CraftAttributeInfo ruby = Ruby = new CraftAttributeInfo();
            ruby.ArmorDurability = 50;
            ruby.ArmorLowerRequirements = 20;
            ruby.WeaponDurability = 100;
            ruby.WeaponLowerRequirements = 50;

            CraftAttributeInfo copper = Copper = new CraftAttributeInfo();
            copper.ArmorDurability = 50;
            copper.ArmorLowerRequirements = 20;
            copper.WeaponDurability = 100;
            copper.WeaponLowerRequirements = 50;

            CraftAttributeInfo bronze = Bronze = new CraftAttributeInfo();
            bronze.ArmorDurability = 50;
            bronze.ArmorLowerRequirements = 20;
            bronze.WeaponDurability = 100;
            bronze.WeaponLowerRequirements = 50;

			CraftAttributeInfo shadowIron = ShadowIron = new CraftAttributeInfo();
            shadowIron.ArmorDurability = 50;
            shadowIron.ArmorLowerRequirements = 20;
            shadowIron.WeaponDurability = 100;
            shadowIron.WeaponLowerRequirements = 50;

            CraftAttributeInfo silver = Silver = new CraftAttributeInfo();
            silver.ArmorDurability = 50;
            silver.ArmorLowerRequirements = 20;
            silver.WeaponDurability = 100;
            silver.WeaponLowerRequirements = 50;

            CraftAttributeInfo mercury = Mercury = new CraftAttributeInfo();
            mercury.ArmorDurability = 50;
            mercury.ArmorLowerRequirements = 20;
            mercury.WeaponDurability = 100;
            mercury.WeaponLowerRequirements = 50;

            CraftAttributeInfo rose = Rose = new CraftAttributeInfo();
            rose.ArmorDurability = 50;
            rose.ArmorLowerRequirements = 20;
            rose.WeaponDurability = 100;
            rose.WeaponLowerRequirements = 50;

			CraftAttributeInfo golden = Gold = new CraftAttributeInfo();
            golden.ArmorDurability = 50;
            golden.ArmorLowerRequirements = 20;
            golden.WeaponDurability = 100;
            golden.WeaponLowerRequirements = 50;

			CraftAttributeInfo agapite = Agapite = new CraftAttributeInfo();
            agapite.ArmorDurability = 50;
            agapite.ArmorLowerRequirements = 20;
            agapite.WeaponDurability = 100;
            agapite.WeaponLowerRequirements = 50;

			CraftAttributeInfo verite = Verite = new CraftAttributeInfo();
            verite.ArmorDurability = 50;
            verite.ArmorLowerRequirements = 20;
            verite.WeaponDurability = 100;
            verite.WeaponLowerRequirements = 50;

            CraftAttributeInfo plutonio = Plutonio = new CraftAttributeInfo();
            plutonio.ArmorDurability = 50;
            plutonio.ArmorLowerRequirements = 20;
            plutonio.WeaponDurability = 100;
            plutonio.WeaponLowerRequirements = 50;

            CraftAttributeInfo bloodrock = BloodRock = new CraftAttributeInfo();
            bloodrock.ArmorDurability = 50;
            bloodrock.ArmorLowerRequirements = 20;
            bloodrock.WeaponDurability = 100;
            bloodrock.WeaponLowerRequirements = 50;

            CraftAttributeInfo valorite = Valorite = new CraftAttributeInfo();
            valorite.ArmorDurability = 50;
            valorite.ArmorLowerRequirements = 20;
            valorite.WeaponDurability = 100;
            valorite.WeaponLowerRequirements = 50;

            CraftAttributeInfo blackrock = BlackRock = new CraftAttributeInfo();
            blackrock.ArmorDurability = 50;
            blackrock.ArmorLowerRequirements = 20;
            blackrock.WeaponDurability = 100;
            blackrock.WeaponLowerRequirements = 50;

            CraftAttributeInfo mytheril = Mytheril = new CraftAttributeInfo();
            mytheril.ArmorDurability = 50;
            mytheril.ArmorLowerRequirements = 20;
            mytheril.WeaponDurability = 100;
            mytheril.WeaponLowerRequirements = 50;

            CraftAttributeInfo aqua = Aqua = new CraftAttributeInfo();
            aqua.ArmorDurability = 50;
            aqua.ArmorLowerRequirements = 20;
            aqua.WeaponDurability = 100;
            aqua.WeaponLowerRequirements = 50;

            //aqua.ArmorPhysicalResist = 4;
            //aqua.ArmorColdResist = 3;
            //aqua.ArmorPoisonResist = 3;
            //aqua.ArmorEnergyResist = 3;
            //aqua.ArmorDurability = 50;
            //aqua.WeaponFireDamage = 10;
            //aqua.WeaponColdDamage = 20;
            //aqua.WeaponPoisonDamage = 10;
            //aqua.WeaponEnergyDamage = 20;
            //aqua.RunicMinAttributes = 5;
            //aqua.RunicMaxAttributes = 5;
            //if (Core.ML)
            //{
            //    aqua.RunicMinIntensity = 85;
            //    aqua.RunicMaxIntensity = 100;
            //}
            //else
            //{
            //    aqua.RunicMinIntensity = 50;
            //    aqua.RunicMaxIntensity = 100;
            //}


			CraftAttributeInfo spined = Spined = new CraftAttributeInfo();
			spined.ArmorPhysicalResist = 5;
			spined.ArmorLuck = 40;
			spined.RunicMinAttributes = 1;
			spined.RunicMaxAttributes = 3;
			if ( Core.ML )
			{
				spined.RunicMinIntensity = 40;
				spined.RunicMaxIntensity = 100;
			}
			else
			{
				spined.RunicMinIntensity = 20;
				spined.RunicMaxIntensity = 40;
			}

			CraftAttributeInfo horned = Horned = new CraftAttributeInfo();
			horned.ArmorPhysicalResist = 2;
			horned.ArmorFireResist = 3;
			horned.ArmorColdResist = 2;
			horned.ArmorPoisonResist = 2;
			horned.ArmorEnergyResist = 2;
			horned.RunicMinAttributes = 3;
			horned.RunicMaxAttributes = 4;
			if ( Core.ML )
			{
				horned.RunicMinIntensity = 45;
				horned.RunicMaxIntensity = 100;
			}
			else
			{
				horned.RunicMinIntensity = 30;
				horned.RunicMaxIntensity = 70;
			}

			CraftAttributeInfo barbed = Barbed = new CraftAttributeInfo();
			barbed.ArmorPhysicalResist = 2;
			barbed.ArmorFireResist = 1;
			barbed.ArmorColdResist = 2;
			barbed.ArmorPoisonResist = 3;
			barbed.ArmorEnergyResist = 4;
			barbed.RunicMinAttributes = 4;
			barbed.RunicMaxAttributes = 5;
			if ( Core.ML )
			{
				barbed.RunicMinIntensity = 50;
				barbed.RunicMaxIntensity = 100;
			}
			else
			{
				barbed.RunicMinIntensity = 40;
				barbed.RunicMaxIntensity = 100;
			}


            CraftAttributeInfo cyclops = Cyclops = new CraftAttributeInfo();

            CraftAttributeInfo gargoyle = Gargoyle = new CraftAttributeInfo();

            CraftAttributeInfo terathan = Terathan = new CraftAttributeInfo();

            CraftAttributeInfo daemon = Daemon = new CraftAttributeInfo();

            CraftAttributeInfo dragon = Dragon = new CraftAttributeInfo();

            CraftAttributeInfo zz = ZZ = new CraftAttributeInfo();

            CraftAttributeInfo dragonGreen = DragonGreen = new CraftAttributeInfo();



			CraftAttributeInfo red = RedScales = new CraftAttributeInfo();

			red.ArmorFireResist = 10;
			red.ArmorColdResist = -3;

			CraftAttributeInfo yellow = YellowScales = new CraftAttributeInfo();

			yellow.ArmorPhysicalResist = -3;
			yellow.ArmorLuck = 20;

			CraftAttributeInfo black = BlackScales = new CraftAttributeInfo();

			black.ArmorPhysicalResist = 10;
			black.ArmorEnergyResist = -3;

			CraftAttributeInfo green = GreenScales = new CraftAttributeInfo();

			green.ArmorFireResist = -3;
			green.ArmorPoisonResist = 10;

			CraftAttributeInfo white = WhiteScales = new CraftAttributeInfo();

			white.ArmorPhysicalResist = -3;
			white.ArmorColdResist = 10;

			CraftAttributeInfo blue = BlueScales = new CraftAttributeInfo();

			blue.ArmorPoisonResist = -3;
			blue.ArmorEnergyResist = 10;

			//public static readonly CraftAttributeInfo OakWood, AshWood, YewWood, Heartwood, Bloodwood, Frostwood;

            CraftAttributeInfo oak = OakWood = new CraftAttributeInfo();

            CraftAttributeInfo ash = AshWood = new CraftAttributeInfo();

            CraftAttributeInfo yew = YewWood = new CraftAttributeInfo();

            CraftAttributeInfo heart = Heartwood = new CraftAttributeInfo();

            CraftAttributeInfo blood = Bloodwood = new CraftAttributeInfo();

            CraftAttributeInfo frost = Frostwood = new CraftAttributeInfo();

            CraftAttributeInfo fine = FineWood = new CraftAttributeInfo();

            CraftAttributeInfo poison = PoisonWood = new CraftAttributeInfo();

            CraftAttributeInfo fire = FireWood = new CraftAttributeInfo();
		}
	}

	public class CraftResourceInfo
	{
		private int m_Hue;
		private int m_Number;
		private string m_Name;
		private CraftAttributeInfo m_AttributeInfo;
		private CraftResource m_Resource;
		private Type[] m_ResourceTypes;

		public int Hue{ get{ return m_Hue; } }
		public int Number{ get{ return m_Number; } }
		public string Name{ get{ return m_Name; } }
		public CraftAttributeInfo AttributeInfo{ get{ return m_AttributeInfo; } }
		public CraftResource Resource{ get{ return m_Resource; } }
		public Type[] ResourceTypes{ get{ return m_ResourceTypes; } }

		public CraftResourceInfo( int hue, int number, string name, CraftAttributeInfo attributeInfo, CraftResource resource, params Type[] resourceTypes )
		{
			m_Hue = hue;
            if (number != -1)
			    m_Number = number;
			m_Name = name;
			m_AttributeInfo = attributeInfo;
			m_Resource = resource;
			m_ResourceTypes = resourceTypes;

			for ( int i = 0; i < resourceTypes.Length; ++i )
				CraftResources.RegisterType( resourceTypes[i], resource );
		}
	}

	public class CraftResources
	{
		private static CraftResourceInfo[] m_MetalInfo = new CraftResourceInfo[]
			{
                //new CraftResourceInfo( 0x000, 1053109, "Iron",			CraftAttributeInfo.Blank,		CraftResource.Iron,				typeof( IronIngot ),		typeof( IronOre ),			typeof( Granite ) ),
                //new CraftResourceInfo( 0x973, 1053108, "Dull Copper",	CraftAttributeInfo.DullCopper,	CraftResource.DullCopper,		typeof( DullCopperIngot ),	typeof( DullCopperOre ),	typeof( DullCopperGranite ) ),
                //new CraftResourceInfo( 0x966, 1053107, "Shadow Iron",	CraftAttributeInfo.ShadowIron,	CraftResource.ShadowIron,		typeof( ShadowIronIngot ),	typeof( ShadowIronOre ),	typeof( ShadowIronGranite ) ),
                //new CraftResourceInfo( 0x96D, 1053106, "Copper",		CraftAttributeInfo.Copper,		CraftResource.Copper,			typeof( CopperIngot ),		typeof( CopperOre ),		typeof( CopperGranite ) ),
                //new CraftResourceInfo( 0x972, 1053105, "Bronze",		CraftAttributeInfo.Bronze,		CraftResource.Bronze,			typeof( BronzeIngot ),		typeof( BronzeOre ),		typeof( BronzeGranite ) ),
                //new CraftResourceInfo( 0x8A5, 1053104, "Gold",			CraftAttributeInfo.Golden,		CraftResource.Gold,				typeof( GoldIngot ),		typeof( GoldOre ),			typeof( GoldGranite ) ),
                //new CraftResourceInfo( 0x979, 1053103, "Agapite",		CraftAttributeInfo.Agapite,		CraftResource.Agapite,			typeof( AgapiteIngot ),		typeof( AgapiteOre ),		typeof( AgapiteGranite ) ),
                //new CraftResourceInfo( 0x89F, 1053102, "Verite",		CraftAttributeInfo.Verite,		CraftResource.Verite,			typeof( VeriteIngot ),		typeof( VeriteOre ),		typeof( VeriteGranite ) ),
                //new CraftResourceInfo( 0x8AB, 1053101, "Valorite",		CraftAttributeInfo.Valorite,	CraftResource.Valorite,			typeof( ValoriteIngot ),	typeof( ValoriteOre ),		typeof( ValoriteGranite ) ),

                
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueIron,       -1,         "Iron",			CraftAttributeInfo.Blank,		CraftResource.Iron,				typeof( IronIngot ),		typeof( IronOre ),			typeof( Granite ) ),
				new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueRusty,      -1,         "Rusty",		CraftAttributeInfo.Rusty,		CraftResource.Rusty,			typeof( RustyIngot ),		typeof( RustyOre ),			typeof( RustyGranite ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueOldCopper,  -1,         "Old Copper",	CraftAttributeInfo.OldCopper,	CraftResource.OldCopper,		typeof( OldCopperIngot ),	typeof( OldCopperOre ),	    typeof( OldCopperGranite ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueDullCopper, -1,         "Dull Copper",	CraftAttributeInfo.DullCopper,	CraftResource.DullCopper,		typeof( DullCopperIngot ),	typeof( DullCopperOre ),	typeof( DullCopperGranite ) ),
				new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueRuby,       -1,         "Ruby",	        CraftAttributeInfo.Ruby,	    CraftResource.Ruby,		        typeof( RubyIngot ),	    typeof( RubyOre ),	        typeof( RubyGranite ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueCopper,     -1,         "Copper",		CraftAttributeInfo.Copper,		CraftResource.Copper,			typeof( CopperIngot ),		typeof( CopperOre ),		typeof( CopperGranite ) ),
				new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueBronze,     -1,         "Bronze",		CraftAttributeInfo.Bronze,		CraftResource.Bronze,			typeof( BronzeIngot ),		typeof( BronzeOre ),		typeof( BronzeGranite ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueShadow,     -1,         "Shadow",	    CraftAttributeInfo.ShadowIron,	CraftResource.ShadowIron,		typeof( ShadowIronIngot ),	typeof( ShadowIronOre ),	typeof( ShadowIronGranite ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueSilver,     -1,         "Silver",		CraftAttributeInfo.Silver,		CraftResource.Silver,			typeof( SilverIngot ),		typeof( SilverOre ),		typeof( SilverGranite ) ),
				new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueMercury,    -1,         "Mercury",		CraftAttributeInfo.Mercury,		CraftResource.Mercury,			typeof( MercuryIngot ),		typeof( MercuryOre ),		typeof( MercuryGranite ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueRose,       -1,         "Rose",	        CraftAttributeInfo.Rose,	    CraftResource.Rose,		        typeof( RoseIngot ),	    typeof( RoseOre ),	        typeof( RoseGranite ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueGold,       -1,         "Gold",			CraftAttributeInfo.Gold,		CraftResource.Gold,				typeof( GoldIngot ),		typeof( GoldOre ),			typeof( GoldGranite ) ),
				new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueAgapite,    -1,         "Agapite",		CraftAttributeInfo.Agapite,		CraftResource.Agapite,			typeof( AgapiteIngot ),		typeof( AgapiteOre ),		typeof( AgapiteGranite ) ),
				new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueVerite,     -1,         "Verite",		CraftAttributeInfo.Verite,		CraftResource.Verite,			typeof( VeriteIngot ),		typeof( VeriteOre ),		typeof( VeriteGranite ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HuePlutonio,   -1,         "Plutonio",		CraftAttributeInfo.Plutonio,	CraftResource.Plutonio,			typeof( PlutoniumIngot ),	typeof( PlutoniumOre ),		typeof( PlutonioGranite ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueBloodRock,  -1,         "BloodRock",	CraftAttributeInfo.BloodRock,	CraftResource.BloodRock,	    typeof( BloodRockIngot ),	typeof( BloodRockOre ),		typeof( BloodRockGranite ) ),
				new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueValorite,   -1,         "Valorite",		CraftAttributeInfo.Valorite,	CraftResource.Valorite,			typeof( ValoriteIngot ),	typeof( ValoriteOre ),		typeof( ValoriteGranite ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueBlackRock,  -1,         "BlackRock",	CraftAttributeInfo.BlackRock,	CraftResource.BlackRock,	    typeof( BlackRockIngot ),	typeof( BlackRockOre ),		typeof( BlackRockGranite ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueMytheril,   -1,         "Mytheril",	    CraftAttributeInfo.Mytheril,	CraftResource.Mytheril,		    typeof( MytherilIngot ),	typeof( MytherilOre ),		typeof( MytherilGranite ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueAqua,       -1,         "Aqua",	        CraftAttributeInfo.Aqua,	    CraftResource.Aqua,		        typeof( AquaIngot ),	    typeof( AquaOre ),		    typeof( AquaGranite ) ),

                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueEndurium,        -1,         "Endurium",	            CraftAttributeInfo.Endurium,	    CraftResource.Endurium,		        typeof( EnduriumIngot ),	    typeof( EnduriumOre ),		    typeof( EnduriumGranite ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueOldEndurium,     -1,         "OldEndurium",	        CraftAttributeInfo.OldEndurium,	    CraftResource.OldEndurium,		    typeof( OldEnduriumIngot ),	    typeof( OldEnduriumOre ),		typeof( OldEnduriumGranite ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueGoldStone,       -1,         "GoldStone",	        CraftAttributeInfo.GoldStone,	    CraftResource.GoldStone,		    typeof( GoldStoneIngot ),	    typeof( GoldStoneOre ),		    typeof( GoldStoneGranite ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueMaxMytheril,     -1,         "MaxMytheril",	        CraftAttributeInfo.MaxMytheril,	    CraftResource.MaxMytheril,		    typeof( MaxMytherilIngot ),	    typeof( MaxMytherilOre ),		typeof( MaxMytherilGranite ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueOreConst.HueMagma,           -1,         "Magma",	            CraftAttributeInfo.Magma,	        CraftResource.Magma,		        typeof( MagmaIngot ),	        typeof( MagmaOre ),		        typeof( MagmaGranite ) ),
			};

		private static CraftResourceInfo[] m_ScaleInfo = new CraftResourceInfo[]
			{
				new CraftResourceInfo( 0x66D, 1053129, "Red Scales",	CraftAttributeInfo.RedScales,		CraftResource.RedScales,		typeof( RedScales ) ),
				new CraftResourceInfo( 0x8A8, 1053130, "Yellow Scales",	CraftAttributeInfo.YellowScales,	CraftResource.YellowScales,		typeof( YellowScales ) ),
				new CraftResourceInfo( 0x455, 1053131, "Black Scales",	CraftAttributeInfo.BlackScales,		CraftResource.BlackScales,		typeof( BlackScales ) ),
				new CraftResourceInfo( 0x851, 1053132, "Green Scales",	CraftAttributeInfo.GreenScales,		CraftResource.GreenScales,		typeof( GreenScales ) ),
				new CraftResourceInfo( 0x8FD, 1053133, "White Scales",	CraftAttributeInfo.WhiteScales,		CraftResource.WhiteScales,		typeof( WhiteScales ) ),
				new CraftResourceInfo( 0x8B0, 1053134, "Blue Scales",	CraftAttributeInfo.BlueScales,		CraftResource.BlueScales,		typeof( BlueScales ) )
			};

		private static CraftResourceInfo[] m_LeatherInfo = new CraftResourceInfo[]
			{
				new CraftResourceInfo( 0x000, 1049353, "Normal",		CraftAttributeInfo.Blank,		CraftResource.RegularLeather,	typeof( Leather ),			typeof( Hides ) ),
                //new CraftResourceInfo( 0x283, 1049354, "Spined",		CraftAttributeInfo.Spined,		CraftResource.SpinedLeather,	typeof( SpinedLeather ),	typeof( SpinedHides ) ),
                //new CraftResourceInfo( 0x227, 1049355, "Horned",		CraftAttributeInfo.Horned,		CraftResource.HornedLeather,	typeof( HornedLeather ),	typeof( HornedHides ) ),
                //new CraftResourceInfo( 0x1C1, 1049356, "Barbed",		CraftAttributeInfo.Barbed,		CraftResource.BarbedLeather,	typeof( BarbedLeather ),	typeof( BarbedHides ) )

                new CraftResourceInfo( DimensionsNewAge.Scripts.HueHideConst.HueHideCyclops,        -1, "Cyclops",		CraftAttributeInfo.Cyclops,		CraftResource.CyclopsLeather,	    typeof( CyclopsLeather ),	    typeof( CyclopsHides ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueHideConst.HueHideGargoyle,       -1, "Gargoyle",		CraftAttributeInfo.Gargoyle,	CraftResource.GargoyleLeather,	    typeof( GargoyleLeather ),	    typeof( GargoyleHides ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueHideConst.HueHideTerathan,       -1, "Terathan",		CraftAttributeInfo.Terathan,	CraftResource.TerathanLeather,	    typeof( TerathanLeather ),	    typeof( TerathanHides ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueHideConst.HueHideDaemon,         -1, "Daemon",		CraftAttributeInfo.Daemon,		CraftResource.DaemonLeather,	    typeof( DaemonLeather ),	    typeof( DaemonHides ) ),
				new CraftResourceInfo( DimensionsNewAge.Scripts.HueHideConst.HueHideDragon,         -1, "Dragon",		CraftAttributeInfo.Dragon,		CraftResource.DragonLeather,	    typeof( DragonLeather ),	    typeof( DragonHides ) ),
				new CraftResourceInfo( DimensionsNewAge.Scripts.HueHideConst.HueHideZZ,             -1, "ZZ",		    CraftAttributeInfo.ZZ,		    CraftResource.ZZLeather,	        typeof( ZZLeather ),	        typeof( ZZHides ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueHideConst.HueHideDragonGreen,    -1, "Dragon Green",	CraftAttributeInfo.DragonGreen,	CraftResource.DragonGreenLeather,   typeof( DragonGreenLeather ),	typeof( DragonGreenHides ) )

			};

		private static CraftResourceInfo[] m_AOSLeatherInfo = new CraftResourceInfo[]
			{
				new CraftResourceInfo( 0x000, 1049353, "Normal",		CraftAttributeInfo.Blank,		CraftResource.RegularLeather,	typeof( Leather ),			typeof( Hides ) ),
                //new CraftResourceInfo( 0x8AC, 1049354, "Spined",		CraftAttributeInfo.Spined,		CraftResource.SpinedLeather,	typeof( SpinedLeather ),	typeof( SpinedHides ) ),
                //new CraftResourceInfo( 0x845, 1049355, "Horned",		CraftAttributeInfo.Horned,		CraftResource.HornedLeather,	typeof( HornedLeather ),	typeof( HornedHides ) ),
                //new CraftResourceInfo( 0x851, 1049356, "Barbed",		CraftAttributeInfo.Barbed,		CraftResource.BarbedLeather,	typeof( BarbedLeather ),	typeof( BarbedHides ) ),

                new CraftResourceInfo( DimensionsNewAge.Scripts.HueHideConst.HueHideCyclops,        -1, "Cyclops",		CraftAttributeInfo.Cyclops,		CraftResource.CyclopsLeather,	    typeof( CyclopsLeather ),	    typeof( CyclopsHides ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueHideConst.HueHideGargoyle,       -1, "Gargoyle",		CraftAttributeInfo.Gargoyle,	CraftResource.GargoyleLeather,	    typeof( GargoyleLeather ),	    typeof( GargoyleHides ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueHideConst.HueHideTerathan,       -1, "Terathan",		CraftAttributeInfo.Terathan,	CraftResource.TerathanLeather,	    typeof( TerathanLeather ),	    typeof( TerathanHides ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueHideConst.HueHideDaemon,         -1, "Daemon",		CraftAttributeInfo.Daemon,		CraftResource.DaemonLeather,	    typeof( DaemonLeather ),	    typeof( DaemonHides ) ),
				new CraftResourceInfo( DimensionsNewAge.Scripts.HueHideConst.HueHideDragon,         -1, "Dragon",		CraftAttributeInfo.Dragon,		CraftResource.DragonLeather,	    typeof( DragonLeather ),	    typeof( DragonHides ) ),
				new CraftResourceInfo( DimensionsNewAge.Scripts.HueHideConst.HueHideZZ,             -1, "ZZ",		    CraftAttributeInfo.ZZ,		    CraftResource.ZZLeather,	        typeof( ZZLeather ),	        typeof( ZZHides ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueHideConst.HueHideDragonGreen,    -1, "Dragon Green",	CraftAttributeInfo.DragonGreen,	CraftResource.DragonGreenLeather,   typeof( DragonGreenLeather ),	typeof( DragonGreenHides ) )

			};

		private static CraftResourceInfo[] m_WoodInfo = new CraftResourceInfo[]
			{
				new CraftResourceInfo( DimensionsNewAge.Scripts.HueWoodConst.HueNormalWood, 1011542, "Normal",		CraftAttributeInfo.Blank,		CraftResource.RegularWood,	typeof( Log ),			typeof( Board ) ),
                //new CraftResourceInfo( 0x7DA, 1072533, "Oak",			CraftAttributeInfo.OakWood,		CraftResource.OakWood,		typeof( OakLog ),		typeof( OakBoard ) ),
                //new CraftResourceInfo( 0x4A7, 1072534, "Ash",			CraftAttributeInfo.AshWood,		CraftResource.AshWood,		typeof( AshLog ),		typeof( AshBoard ) ),
                //new CraftResourceInfo( 0x4A8, 1072535, "Yew",			CraftAttributeInfo.YewWood,		CraftResource.YewWood,		typeof( YewLog ),		typeof( YewBoard ) ),
                //new CraftResourceInfo( 0x4A9, 1072536, "Heartwood",		CraftAttributeInfo.Heartwood,	CraftResource.Heartwood,	typeof( HeartwoodLog ),	typeof( HeartwoodBoard ) ),
                //new CraftResourceInfo( 0x4AA, 1072538, "Bloodwood",		CraftAttributeInfo.Bloodwood,	CraftResource.Bloodwood,	typeof( BloodwoodLog ),	typeof( BloodwoodBoard ) ),
                //new CraftResourceInfo( 0x47F, 1072539, "Frostwood",		CraftAttributeInfo.Frostwood,	CraftResource.Frostwood,	typeof( FrostwoodLog ),	typeof( FrostwoodBoard ) )

                new CraftResourceInfo( DimensionsNewAge.Scripts.HueWoodConst.HueFineWood, -1, "Fine",			CraftAttributeInfo.FineWood,		CraftResource.FineWood,		typeof( OakLog ),		typeof( OakBoard ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueWoodConst.HuePoisonWood, -1, "Poison",			CraftAttributeInfo.PoisonWood,		CraftResource.PoisonWood,		typeof( OakLog ),		typeof( OakBoard ) ),
                new CraftResourceInfo( DimensionsNewAge.Scripts.HueWoodConst.HueFireWood, -1, "Fire",			CraftAttributeInfo.FireWood,		CraftResource.FireWood,		typeof( OakLog ),		typeof( OakBoard ) )
			};

		/// <summary>
		/// Returns true if '<paramref name="resource"/>' is None, Iron, RegularLeather or RegularWood. False if otherwise.
		/// </summary>
		public static bool IsStandard( CraftResource resource )
		{
			return ( resource == CraftResource.None || resource == CraftResource.Iron || resource == CraftResource.RegularLeather || resource == CraftResource.RegularWood );
		}

		private static Hashtable m_TypeTable;

		/// <summary>
		/// Registers that '<paramref name="resourceType"/>' uses '<paramref name="resource"/>' so that it can later be queried by <see cref="CraftResources.GetFromType"/>
		/// </summary>
		public static void RegisterType( Type resourceType, CraftResource resource )
		{
			if ( m_TypeTable == null )
				m_TypeTable = new Hashtable();

			m_TypeTable[resourceType] = resource;
		}

		/// <summary>
		/// Returns the <see cref="CraftResource"/> value for which '<paramref name="resourceType"/>' uses -or- CraftResource.None if an unregistered type was specified.
		/// </summary>
		public static CraftResource GetFromType( Type resourceType )
		{
			if ( m_TypeTable == null )
				return CraftResource.None;

			object obj = m_TypeTable[resourceType];

			if ( !(obj is CraftResource) )
				return CraftResource.None;

			return (CraftResource)obj;
		}

		/// <summary>
		/// Returns a <see cref="CraftResourceInfo"/> instance describing '<paramref name="resource"/>' -or- null if an invalid resource was specified.
		/// </summary>
		public static CraftResourceInfo GetInfo( CraftResource resource )
		{
			CraftResourceInfo[] list = null;

			switch ( GetType( resource ) )
			{
				case CraftResourceType.Metal: list = m_MetalInfo; break;
				case CraftResourceType.Leather: list = Core.AOS ? m_AOSLeatherInfo : m_LeatherInfo; break;
				case CraftResourceType.Scales: list = m_ScaleInfo; break;
				case CraftResourceType.Wood: list = m_WoodInfo; break;
			}

			if ( list != null )
			{
				int index = GetIndex( resource );

				if ( index >= 0 && index < list.Length )
					return list[index];
			}

			return null;
		}

		/// <summary>
		/// Returns a <see cref="CraftResourceType"/> value indiciating the type of '<paramref name="resource"/>'.
		/// </summary>
		public static CraftResourceType GetType( CraftResource resource )
		{
			if ( resource >= CraftResource.Iron && resource <= CraftResource.Magma )
				return CraftResourceType.Metal;

			if ( resource >= CraftResource.RegularLeather && resource <= CraftResource.DragonGreenLeather )
				return CraftResourceType.Leather;

			if ( resource >= CraftResource.RedScales && resource <= CraftResource.BlueScales )
				return CraftResourceType.Scales;

			if ( resource >= CraftResource.RegularWood && resource <= CraftResource.Frostwood )
				return CraftResourceType.Wood;

			return CraftResourceType.None;
		}

		/// <summary>
		/// Returns the first <see cref="CraftResource"/> in the series of resources for which '<paramref name="resource"/>' belongs.
		/// </summary>
		public static CraftResource GetStart( CraftResource resource )
		{
			switch ( GetType( resource ) )
			{
				case CraftResourceType.Metal: return CraftResource.Iron;
				case CraftResourceType.Leather: return CraftResource.RegularLeather;
				case CraftResourceType.Scales: return CraftResource.RedScales;
				case CraftResourceType.Wood: return CraftResource.RegularWood;
			}

			return CraftResource.None;
		}

		/// <summary>
		/// Returns the index of '<paramref name="resource"/>' in the seriest of resources for which it belongs.
		/// </summary>
		public static int GetIndex( CraftResource resource )
		{
			CraftResource start = GetStart( resource );

			if ( start == CraftResource.None )
				return 0;

			return (int)(resource - start);
		}

		/// <summary>
		/// Returns the <see cref="CraftResourceInfo.Number"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
		/// </summary>
		public static int GetLocalizationNumber( CraftResource resource )
		{
			CraftResourceInfo info = GetInfo( resource );

			return ( info == null ? 0 : info.Number );
		}

		/// <summary>
		/// Returns the <see cref="CraftResourceInfo.Hue"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
		/// </summary>
		public static int GetHue( CraftResource resource )
		{
			CraftResourceInfo info = GetInfo( resource );

			return ( info == null ? 0 : info.Hue );
		}

		/// <summary>
		/// Returns the <see cref="CraftResourceInfo.Name"/> property of '<paramref name="resource"/>' -or- an empty string if the resource specified was invalid.
		/// </summary>
		public static string GetName( CraftResource resource )
		{
			CraftResourceInfo info = GetInfo( resource );

			return ( info == null ? String.Empty : info.Name );
		}

		/// <summary>
		/// Returns the <see cref="CraftResource"/> value which represents '<paramref name="info"/>' -or- CraftResource.None if unable to convert.
		/// </summary>
		public static CraftResource GetFromOreInfo( OreInfo info )
		{
			if ( info.Name.IndexOf( "Spined" ) >= 0 )
				return CraftResource.SpinedLeather;
			else if ( info.Name.IndexOf( "Horned" ) >= 0 )
				return CraftResource.HornedLeather;
			else if ( info.Name.IndexOf( "Barbed" ) >= 0 )
				return CraftResource.BarbedLeather;
			else if ( info.Name.IndexOf( "Leather" ) >= 0 )
				return CraftResource.RegularLeather;

            if (info.Level == 0)
                return CraftResource.Iron;
            else if (info.Level == 1)
                return CraftResource.Rusty;
            else if (info.Level == 2)
                return CraftResource.OldCopper;
            else if (info.Level == 3)
                return CraftResource.DullCopper;
            else if (info.Level == 4)
                return CraftResource.Ruby;
            else if (info.Level == 5)
                return CraftResource.Copper;
            else if (info.Level == 6)
                return CraftResource.Bronze;
            else if (info.Level == 7)
                return CraftResource.ShadowIron;
            else if (info.Level == 8)
                return CraftResource.Silver;
            else if (info.Level == 9)
                return CraftResource.Mercury;
            else if (info.Level == 10)
                return CraftResource.Rose;
            else if (info.Level == 11)
                return CraftResource.Gold;
            else if (info.Level == 12)
                return CraftResource.Agapite;
            else if (info.Level == 13)
                return CraftResource.Verite;
            else if (info.Level == 14)
                return CraftResource.Plutonio;
            else if (info.Level == 15)
                return CraftResource.BloodRock;
            else if (info.Level == 16)
                return CraftResource.Valorite;
            else if (info.Level == 17)
                return CraftResource.BlackRock;
            else if (info.Level == 18)
                return CraftResource.Mytheril;
            else if (info.Level == 19)
                return CraftResource.Aqua;

			return CraftResource.None;
		}

		/// <summary>
		/// Returns the <see cref="CraftResource"/> value which represents '<paramref name="info"/>', using '<paramref name="material"/>' to help resolve leather OreInfo instances.
		/// </summary>
		public static CraftResource GetFromOreInfo( OreInfo info, ArmorMaterialType material )
		{
			if ( material == ArmorMaterialType.Studded || material == ArmorMaterialType.Leather || material == ArmorMaterialType.Spined ||
				material == ArmorMaterialType.Horned || material == ArmorMaterialType.Barbed )
			{
				if ( info.Level == 0 )
					return CraftResource.RegularLeather;
				else if ( info.Level == 1 )
					return CraftResource.SpinedLeather;
				else if ( info.Level == 2 )
					return CraftResource.HornedLeather;
				else if ( info.Level == 3 )
					return CraftResource.BarbedLeather;

				return CraftResource.None;
			}

			return GetFromOreInfo( info );
		}
	}

	// NOTE: This class is only for compatability with very old RunUO versions.
	// No changes to it should be required for custom resources.
	public class OreInfo
	{
        public static readonly OreInfo Iron         = new OreInfo(0, DimensionsNewAge.Scripts.HueOreConst.HueIron, "Iron");
        public static readonly OreInfo Rusty        = new OreInfo(1, DimensionsNewAge.Scripts.HueOreConst.HueRusty, "Rusty");
        public static readonly OreInfo OldCopper    = new OreInfo(2, DimensionsNewAge.Scripts.HueOreConst.HueOldCopper, "Old Copper");
        public static readonly OreInfo DullCopper   = new OreInfo(3, DimensionsNewAge.Scripts.HueOreConst.HueDullCopper, "Dull Copper");
        public static readonly OreInfo Ruby         = new OreInfo(4, DimensionsNewAge.Scripts.HueOreConst.HueOldCopper, "Ruby");
        public static readonly OreInfo Copper       = new OreInfo(5, DimensionsNewAge.Scripts.HueOreConst.HueCopper, "Copper");
        public static readonly OreInfo Bronze       = new OreInfo(6, DimensionsNewAge.Scripts.HueOreConst.HueBronze, "Bronze");
        public static readonly OreInfo ShadowIron   = new OreInfo(7, DimensionsNewAge.Scripts.HueOreConst.HueShadow, "Shadow Iron");
        public static readonly OreInfo Silver       = new OreInfo(8, DimensionsNewAge.Scripts.HueOreConst.HueSilver, "Silver");
        public static readonly OreInfo Mercury      = new OreInfo(9, DimensionsNewAge.Scripts.HueOreConst.HueMercury, "Mercury");
        public static readonly OreInfo Rose         = new OreInfo(10, DimensionsNewAge.Scripts.HueOreConst.HueRose, "Rose");
        public static readonly OreInfo Gold         = new OreInfo(11, DimensionsNewAge.Scripts.HueOreConst.HueGold, "Gold");
        public static readonly OreInfo Agapite      = new OreInfo(12, DimensionsNewAge.Scripts.HueOreConst.HueAgapite, "Agapite");
        public static readonly OreInfo Verite       = new OreInfo(13, DimensionsNewAge.Scripts.HueOreConst.HueVerite, "Verite");
        public static readonly OreInfo Plutonio     = new OreInfo(14, DimensionsNewAge.Scripts.HueOreConst.HuePlutonio, "Plutonio");
        public static readonly OreInfo BloodRock    = new OreInfo(15, DimensionsNewAge.Scripts.HueOreConst.HueBloodRock, "BloodRock");
        public static readonly OreInfo Valorite     = new OreInfo(16, DimensionsNewAge.Scripts.HueOreConst.HueValorite, "Valorite");
        public static readonly OreInfo BlackRock    = new OreInfo(17, DimensionsNewAge.Scripts.HueOreConst.HueBlackRock, "BlackRock");
        public static readonly OreInfo Mytheril     = new OreInfo(18, DimensionsNewAge.Scripts.HueOreConst.HueMytheril, "Mytheril");
        public static readonly OreInfo Aqua         = new OreInfo(19, DimensionsNewAge.Scripts.HueOreConst.HueAqua, "Aqua");
        public static readonly OreInfo Endurium     = new OreInfo(20, DimensionsNewAge.Scripts.HueOreConst.HueEndurium, "Endurium");
        public static readonly OreInfo OldEndurium  = new OreInfo(21, DimensionsNewAge.Scripts.HueOreConst.HueOldEndurium, "Old Endurium");
        public static readonly OreInfo GoldStone    = new OreInfo(22, DimensionsNewAge.Scripts.HueOreConst.HueGoldStone, "Gold Stone");
        public static readonly OreInfo MaxMytheril  = new OreInfo(23, DimensionsNewAge.Scripts.HueOreConst.HueMaxMytheril, "Max Mytheril");
        public static readonly OreInfo Magma        = new OreInfo(24, DimensionsNewAge.Scripts.HueOreConst.HueMagma, "Magma");

		private int m_Level;
		private int m_Hue;
		private string m_Name;

		public OreInfo( int level, int hue, string name )
		{
			m_Level = level;
			m_Hue = hue;
			m_Name = name;
		}

		public int Level
		{
			get
			{
				return m_Level;
			}
		}

		public int Hue
		{
			get
			{
				return m_Hue;
			}
		}

		public string Name
		{
			get
			{
				return m_Name;
			}
		}
	}
}