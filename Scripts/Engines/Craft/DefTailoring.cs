using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefTailoring : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Tailoring; }
		}

		public override int GumpTitleNumber
		{
			get { return 1044005; } // <CENTER>TAILORING MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefTailoring();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

        private DefTailoring()
            : base(3, 3, 1.20)// base( 1, 1, 4.5 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		public static bool IsNonColorable(Type type)
		{
			for (int i = 0; i < m_TailorNonColorables.Length; ++i)
			{
				if (m_TailorNonColorables[i] == type)
				{
					return true;
				}
			}

			return false;
		}

		private static Type[] m_TailorNonColorables = new Type[]
			{
				typeof( OrcHelm )
			};

		private static Type[] m_TailorColorables = new Type[]
			{
				typeof( GozaMatEastDeed ), typeof( GozaMatSouthDeed ),
				typeof( SquareGozaMatEastDeed ), typeof( SquareGozaMatSouthDeed ),
				typeof( BrocadeGozaMatEastDeed ), typeof( BrocadeGozaMatSouthDeed ),
				typeof( BrocadeSquareGozaMatEastDeed ), typeof( BrocadeSquareGozaMatSouthDeed )
			};

		public override bool RetainsColorFrom( CraftItem item, Type type )
		{
			if ( type != typeof( Cloth ) && type != typeof( UncutCloth ) )
				return false;

			type = item.ItemType;

			bool contains = false;

			for ( int i = 0; !contains && i < m_TailorColorables.Length; ++i )
				contains = ( m_TailorColorables[i] == type );

			return contains;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x248 );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{
				if ( quality == 0 )
					return 502785; // You were barely able to make this item.  It's quality is below average.
				else if ( makersMark && quality == 2 )
					return 1044156; // You create an exceptional quality item and affix your maker's mark.
				else if ( quality == 2 )
					return 1044155; // You create an exceptional quality item.
				else				
					return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

			#region Hats
			AddCraft( typeof( SkullCap ), "Chapeus", 1025444, 0.0, 25.0, typeof( Cloth ), 1044286, 2, 1044287 );
            AddCraft(typeof(Bandana), "Chapeus", 1025440, 0.0, 25.0, typeof(Cloth), 1044286, 2, 1044287);
            AddCraft(typeof(FloppyHat), "Chapeus", 1025907, 6.2, 31.2, typeof(Cloth), 1044286, 11, 1044287);
            AddCraft(typeof(Cap), "Chapeus", 1025909, 6.2, 31.2, typeof(Cloth), 1044286, 11, 1044287);
            AddCraft(typeof(WideBrimHat), "Chapeus", 1025908, 6.2, 31.2, typeof(Cloth), 1044286, 12, 1044287);
            AddCraft(typeof(StrawHat), "Chapeus", 1025911, 6.2, 31.2, typeof(Cloth), 1044286, 10, 1044287);
            AddCraft(typeof(TallStrawHat), "Chapeus", 1025910, 6.7, 31.7, typeof(Cloth), 1044286, 13, 1044287);
            AddCraft(typeof(WizardsHat), "Chapeus", 1025912, 7.2, 32.2, typeof(Cloth), 1044286, 15, 1044287);
            AddCraft(typeof(Bonnet), "Chapeus", 1025913, 6.2, 31.2, typeof(Cloth), 1044286, 11, 1044287);
            AddCraft(typeof(FeatheredHat), "Chapeus", 1025914, 6.2, 31.2, typeof(Cloth), 1044286, 12, 1044287);
            AddCraft(typeof(TricorneHat), "Chapeus", 1025915, 6.2, 31.2, typeof(Cloth), 1044286, 12, 1044287);
            AddCraft(typeof(JesterHat), "Chapeus", 1025916, 7.2, 32.2, typeof(Cloth), 1044286, 15, 1044287);

            //if ( Core.AOS )
            //    AddCraft( typeof( FlowerGarland ), 1011375, 1028965, 10.0, 35.0, typeof( Cloth ), 1044286, 5, 1044287 );

            //if( Core.SE )
            //{
            //    index = AddCraft( typeof( ClothNinjaHood ), 1011375, 1030202, 80.0, 105.0, typeof( Cloth ), 1044286, 13, 1044287 );
            //    SetNeededExpansion( index, Expansion.SE );

            //    index = AddCraft( typeof( Kasa ), 1011375, 1030211, 60.0, 85.0, typeof( Cloth ), 1044286, 12, 1044287 );	
            //    SetNeededExpansion( index, Expansion.SE );
            //}
			#endregion

			#region Shirts
			AddCraft( typeof( Doublet ), "Camisas", 1028059, 0, 25.0, typeof( Cloth ), 1044286, 8, 1044287 );
            AddCraft(typeof(Shirt), "Camisas", 1025399, 20.7, 45.7, typeof(Cloth), 1044286, 8, 1044287);
            AddCraft(typeof(FancyShirt), "Camisas", 1027933, 24.8, 49.8, typeof(Cloth), 1044286, 8, 1044287);
            AddCraft(typeof(Tunic), "Camisas", 1028097, 00.0, 25.0, typeof(Cloth), 1044286, 12, 1044287);
            AddCraft(typeof(Surcoat), "Camisas", 1028189, 8.2, 33.2, typeof(Cloth), 1044286, 14, 1044287);
            AddCraft(typeof(PlainDress), "Camisas", 1027937, 12.4, 37.4, typeof(Cloth), 1044286, 10, 1044287);
            AddCraft(typeof(FancyDress), "Camisas", 1027935, 33.1, 58.1, typeof(Cloth), 1044286, 12, 1044287);
            AddCraft(typeof(Cloak), "Camisas", 1025397, 41.4, 66.4, typeof(Cloth), 1044286, 14, 1044287);
            AddCraft(typeof(Robe), "Camisas", 1027939, 53.9, 78.9, typeof(Cloth), 1044286, 16, 1044287);
            AddCraft(typeof(JesterSuit), "Camisas", 1028095, 8.2, 33.2, typeof(Cloth), 1044286, 24, 1044287);



			if ( Core.AOS )
			{
                //AddCraft( typeof( FurCape ), 1015269, 1028969, 35.0, 60.0, typeof( Cloth ), 1044286, 13, 1044287 );
                //AddCraft( typeof( GildedDress ), 1015269, 1028973, 37.5, 62.5, typeof( Cloth ), 1044286, 16, 1044287 );
                //AddCraft( typeof( FormalShirt ), 1015269, 1028975, 26.0, 51.0, typeof( Cloth ), 1044286, 16, 1044287 );
			}

			if( Core.SE )
			{
                //index = AddCraft( typeof( ClothNinjaJacket ), 1015269, 1030207, 75.0, 100.0, typeof( Cloth ), 1044286, 12, 1044287 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( Kamishimo ), 1015269, 1030212, 75.0, 100.0, typeof( Cloth ), 1044286, 15, 1044287 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( HakamaShita ), 1015269, 1030215, 40.0, 65.0, typeof( Cloth ), 1044286, 14, 1044287 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( MaleKimono ), 1015269, 1030189, 50.0, 75.0, typeof( Cloth ), 1044286, 16, 1044287 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( FemaleKimono ), 1015269, 1030190, 50.0, 75.0, typeof( Cloth ), 1044286, 16, 1044287 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( JinBaori ), 1015269, 1030220, 30.0, 55.0, typeof( Cloth ), 1044286, 12, 1044287 );
                //SetNeededExpansion( index, Expansion.SE );
			}

			#endregion

			#region Pants
			AddCraft( typeof( ShortPants ), "Calcas", 1025422, 24.8, 49.8, typeof( Cloth ), 1044286, 6, 1044287 );
            AddCraft(typeof(LongPants), "Calcas", 1025433, 24.8, 49.8, typeof(Cloth), 1044286, 8, 1044287);
            AddCraft(typeof(Kilt), "Calcas", 1025431, 20.7, 45.7, typeof(Cloth), 1044286, 8, 1044287);
            AddCraft(typeof(Skirt), "Calcas", 1025398, 29.0, 54.0, typeof(Cloth), 1044286, 10, 1044287);

            //if ( Core.AOS )
            //    AddCraft( typeof( FurSarong ), 1015279, 1028971, 35.0, 60.0, typeof( Cloth ), 1044286, 12, 1044287 );

            //if( Core.SE )
            //{
            //    index = AddCraft( typeof( Hakama ), 1015279, 1030213, 50.0, 75.0, typeof( Cloth ), 1044286, 16, 1044287 );
            //    SetNeededExpansion( index, Expansion.SE );
            //    index = AddCraft( typeof( TattsukeHakama ), 1015279, 1030214, 50.0, 75.0, typeof( Cloth ), 1044286, 16, 1044287 );
            //    SetNeededExpansion( index, Expansion.SE );
            //}

			#endregion

			#region Misc

            AddCraft(typeof(BodySash), "Camisas", 1025441, 4.1, 29.1, typeof(Cloth), 1044286, 4, 1044287);
            AddCraft(typeof(HalfApron), "Camisas", 1025435, 20.7, 45.7, typeof(Cloth), 1044286, 6, 1044287);
            AddCraft(typeof(FullApron), "Camisas", 1025437, 29.0, 54.0, typeof(Cloth), 1044286, 10, 1044287);

            //if( Core.SE )
            //{
            //    index = AddCraft( typeof( Obi ), 1015283, 1030219, 20.0, 45.0, typeof( Cloth ), 1044286, 6, 1044287 );
            //    SetNeededExpansion( index, Expansion.SE );
            //}

            //if( Core.ML )
            //{
            //    index = AddCraft( typeof( ElvenQuiver ), 1015283, 1032657, 65.0, 115.0, typeof( Leather ), 1044462, 28, 1044463 );
            //    AddRecipe( index, 501 );
            //    SetNeededExpansion( index, Expansion.ML );

            //    index = AddCraft( typeof( QuiverOfFire ), 1015283, 1073109, 65.0, 115.0, typeof( Leather ), 1044462, 28, 1044463 );
            //    AddRes( index, typeof( FireRuby ), 1032695, 15, 1042081 );
            //    AddRecipe( index, 502 );
            //    SetNeededExpansion( index, Expansion.ML );

            //    index = AddCraft( typeof( QuiverOfIce ), 1015283, 1073110, 65.0, 115.0, typeof( Leather ), 1044462, 28, 1044463 );
            //    AddRes( index, typeof( WhitePearl ), 1032694, 15, 1042081 );
            //    AddRecipe( index, 503 );
            //    SetNeededExpansion( index, Expansion.ML );

            //    index = AddCraft( typeof( QuiverOfBlight ), 1015283, 1073111, 65.0, 115.0, typeof( Leather ), 1044462, 28, 1044463 );
            //    AddRes( index, typeof( Blight ), 1032675, 10, 1042081 );
            //    AddRecipe( index, 504 );
            //    SetNeededExpansion( index, Expansion.ML );

            //    index = AddCraft( typeof( QuiverOfLightning ), 1015283, 1073112, 65.0, 115.0, typeof( Leather ), 1044462, 28, 1044463 );
            //    AddRes( index, typeof( Corruption ), 1032676, 10, 1042081 );
            //    AddRecipe( index, 505 );
            //    SetNeededExpansion( index, Expansion.ML );
            //}

			//AddCraft( typeof( OilCloth ), 1015283, 1041498, 74.6, 99.6, typeof( Cloth ), 1044286, 1, 1044287 );

            //if( Core.SE )
            //{
            //    index = AddCraft( typeof( GozaMatEastDeed ), 1015283, 1030404, 55.0, 80.0, typeof( Cloth ), 1044286, 25, 1044287 );
            //    SetNeededExpansion( index, Expansion.SE );
            //    index = AddCraft( typeof( GozaMatSouthDeed ), 1015283, 1030405, 55.0, 80.0, typeof( Cloth ), 1044286, 25, 1044287 );
            //    SetNeededExpansion( index, Expansion.SE );
            //    index = AddCraft( typeof( SquareGozaMatEastDeed ), 1015283, 1030407, 55.0, 80.0, typeof( Cloth ), 1044286, 25, 1044287 );
            //    SetNeededExpansion( index, Expansion.SE );
            //    index = AddCraft( typeof( SquareGozaMatSouthDeed ), 1015283, 1030406, 55.0, 80.0, typeof( Cloth ), 1044286, 25, 1044287 );
            //    SetNeededExpansion( index, Expansion.SE );
            //    index = AddCraft( typeof( BrocadeGozaMatEastDeed ), 1015283, 1030408, 55.0, 80.0, typeof( Cloth ), 1044286, 25, 1044287 );
            //    SetNeededExpansion( index, Expansion.SE );
            //    index = AddCraft( typeof( BrocadeGozaMatSouthDeed ), 1015283, 1030409, 55.0, 80.0, typeof( Cloth ), 1044286, 25, 1044287 );
            //    SetNeededExpansion( index, Expansion.SE );
            //    index = AddCraft( typeof( BrocadeSquareGozaMatEastDeed ), 1015283, 1030411, 55.0, 80.0, typeof( Cloth ), 1044286, 25, 1044287 );
            //    SetNeededExpansion( index, Expansion.SE );
            //    index = AddCraft( typeof( BrocadeSquareGozaMatSouthDeed ), 1015283, 1030410, 55.0, 80.0, typeof( Cloth ), 1044286, 25, 1044287 );
            //    SetNeededExpansion( index, Expansion.SE );
            //}

			#endregion

			#region Footwear
            //if ( Core.AOS )
            //    AddCraft( typeof( FurBoots ), 1015288, 1028967, 50.0, 75.0, typeof( Cloth ), 1044286, 12, 1044287 );

            //if( Core.SE )
            //{
            //    index = AddCraft( typeof( NinjaTabi ), 1015288, 1030210, 70.0, 95.0, typeof( Cloth ), 1044286, 10, 1044287 );
            //    SetNeededExpansion( index, Expansion.SE );
            //    index = AddCraft( typeof( SamuraiTabi ), 1015288, 1030209, 20.0, 45.0, typeof( Cloth ), 1044286, 6, 1044287 );
            //    SetNeededExpansion( index, Expansion.SE );
            //}

			AddCraft( typeof( Sandals ), "Calcados", 1025901, 12.4, 37.4, typeof( Leather ), 1044462, 4, 1044463 );
            AddCraft(typeof(Shoes), "Calcados", 1025904, 16.5, 41.5, typeof(Leather), 1044462, 6, 1044463);
            AddCraft(typeof(Boots), "Calcados", 1025899, 33.1, 58.1, typeof(Leather), 1044462, 8, 1044463);
            AddCraft(typeof(ThighBoots), "Calcados", 1025906, 41.4, 66.4, typeof(Leather), 1044462, 10, 1044463);
			#endregion

			#region Leather Armor

			AddCraft( typeof( LeatherGorget ), "Armadura Couro", 1025063, 53.9, 78.9, typeof( Leather ), 1044462, 4, 1044463 );
            AddCraft(typeof(LeatherCap), "Armadura Couro", 1027609, 6.2, 31.2, typeof(Leather), 1044462, 2, 1044463);
            AddCraft(typeof(LeatherGloves), "Armadura Couro", 1025062, 51.8, 76.8, typeof(Leather), 1044462, 3, 1044463);
            AddCraft(typeof(LeatherArms), "Armadura Couro", 1025061, 53.9, 78.9, typeof(Leather), 1044462, 4, 1044463);
            AddCraft(typeof(LeatherLegs), "Armadura Couro", 1025067, 66.3, 91.3, typeof(Leather), 1044462, 10, 1044463);
            AddCraft(typeof(LeatherChest), "Armadura Couro", 1025068, 70.5, 95.5, typeof(Leather), 1044462, 12, 1044463);

			if( Core.SE )
			{
                //index = AddCraft( typeof( LeatherJingasa ), 1015293, 1030177, 45.0, 70.0, typeof( Leather ), 1044462, 4, 1044463 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( LeatherMempo ), 1015293, 1030181, 80.0, 105.0, typeof( Leather ), 1044462, 8, 1044463 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( LeatherDo ), 1015293, 1030182, 75.0, 100.0, typeof( Leather ), 1044462, 12, 1044463 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( LeatherHiroSode ), 1015293, 1030185, 55.0, 80.0, typeof( Leather ), 1044462, 5, 1044463 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( LeatherSuneate ), 1015293, 1030193, 68.0, 93.0, typeof( Leather ), 1044462, 12, 1044463 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( LeatherHaidate ), 1015293, 1030197, 68.0, 93.0, typeof( Leather ), 1044462, 12, 1044463 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( LeatherNinjaPants ), 1015293, 1030204, 80.0, 105.0, typeof( Leather ), 1044462, 13, 1044463 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( LeatherNinjaJacket ), 1015293, 1030206, 85.0, 110.0, typeof( Leather ), 1044462, 13, 1044463 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( LeatherNinjaBelt ), 1015293, 1030203, 50.0, 75.0, typeof( Leather ), 1044462, 5, 1044463 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( LeatherNinjaMitts ), 1015293, 1030205, 65.0, 90.0, typeof( Leather ), 1044462, 12, 1044463 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( LeatherNinjaHood ), 1015293, 1030201, 90.0, 115.0, typeof( Leather ), 1044462, 14, 1044463 );
                //SetNeededExpansion( index, Expansion.SE );
			}

			#endregion

			#region Studded Armor
            AddCraft(typeof(StuddedGorget), "Armadura Couro Reforcado", 1025078, 78.8, 103.8, typeof(Leather), 1044462, 6, 1044463);
            AddCraft(typeof(StuddedGloves), "Armadura Couro Reforcado", 1025077, 82.9, 107.9, typeof(Leather), 1044462, 8, 1044463);
            AddCraft(typeof(StuddedArms), "Armadura Couro Reforcado", 1025076, 87.1, 112.1, typeof(Leather), 1044462, 10, 1044463);
            AddCraft(typeof(StuddedLegs), "Armadura Couro Reforcado", 1025082, 91.2, 116.2, typeof(Leather), 1044462, 12, 1044463);
            AddCraft(typeof(StuddedChest), "Armadura Couro Reforcado", 1025083, 94.0, 119.0, typeof(Leather), 1044462, 14, 1044463);

			if( Core.SE )
			{
                //index = AddCraft( typeof( StuddedMempo ), 1015300, 1030216, 80.0, 105.0, typeof( Leather ), 1044462, 8, 1044463 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( StuddedDo ), 1015300, 1030183, 95.0, 120.0, typeof( Leather ), 1044462, 14, 1044463 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( StuddedHiroSode ), 1015300, 1030186, 85.0, 110.0, typeof( Leather ), 1044462, 8, 1044463 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( StuddedSuneate ), 1015300, 1030194, 92.0, 117.0, typeof( Leather ), 1044462, 14, 1044463 );
                //SetNeededExpansion( index, Expansion.SE );
                //index = AddCraft( typeof( StuddedHaidate ), 1015300, 1030198, 92.0, 117.0, typeof( Leather ), 1044462, 14, 1044463 );
                //SetNeededExpansion( index, Expansion.SE );
			}

			#endregion

			#region Female Armor
            //AddCraft( typeof( LeatherShorts ), 1015306, 1027168, 62.2, 87.2, typeof( Leather ), 1044462, 8, 1044463 );
            //AddCraft( typeof( LeatherSkirt ), 1015306, 1027176, 58.0, 83.0, typeof( Leather ), 1044462, 6, 1044463 );
            //AddCraft( typeof( LeatherBustierArms ), 1015306, 1027178, 58.0, 83.0, typeof( Leather ), 1044462, 6, 1044463 );
            //AddCraft( typeof( StuddedBustierArms ), 1015306, 1027180, 82.9, 107.9, typeof( Leather ), 1044462, 8, 1044463 );
            //AddCraft( typeof( FemaleLeatherChest ), 1015306, 1027174, 62.2, 87.2, typeof( Leather ), 1044462, 8, 1044463 );
            //AddCraft( typeof( FemaleStuddedChest ), 1015306, 1027170, 87.1, 112.1, typeof( Leather ), 1044462, 10, 1044463 );
			#endregion

            #region Bone Armor
            //index = AddCraft( typeof( BoneHelm ), 1049149, 1025206, 85.0, 110.0, typeof( Leather ), 1044462, 4, 1044463 );
            //AddRes( index, typeof( Bone ), 1049064, 2, 1049063 );
			
            //index = AddCraft( typeof( BoneGloves ), 1049149, 1025205, 89.0, 114.0, typeof( Leather ), 1044462, 6, 1044463 );
            //AddRes( index, typeof( Bone ), 1049064, 2, 1049063 );

            //index = AddCraft( typeof( BoneArms ), 1049149, 1025203, 92.0, 117.0, typeof( Leather ), 1044462, 8, 1044463 );
            //AddRes( index, typeof( Bone ), 1049064, 4, 1049063 );

            //index = AddCraft( typeof( BoneLegs ), 1049149, 1025202, 95.0, 120.0, typeof( Leather ), 1044462, 10, 1044463 );
            //AddRes( index, typeof( Bone ), 1049064, 6, 1049063 );
		
            //index = AddCraft( typeof( BoneChest ), 1049149, 1025199, 96.0, 121.0, typeof( Leather ), 1044462, 12, 1044463 );
            //AddRes( index, typeof( Bone ), 1049064, 10, 1049063 );

            //index = AddCraft(typeof(OrcHelm), 1049149, 1027947, 90.0, 115.0, typeof(Leather), 1044462, 6, 1044463);
            //AddRes(index, typeof(Bone), 1049064, 4, 1049063);
            #endregion

            // Tailor roupas magicas

            // 501913 - You don't have the proper reagents.
            // 1002127 - Reagents

            index = AddCraft(typeof(ThighBootsOfCamouflage), "Roupa Magica", "Boots Of Camouflage", 90.0, 110.0, typeof(Cloth), 1044286, 20, 1044287);
            AddRes(index, typeof(SpidersSilk), 1002127, 17, 501913);
            AddSkill(index, SkillName.Magery, 90, 100);

            index = AddCraft(typeof(FishingGloves), "Roupa Magica", "Fishing Gloves", 50.0, 110.0, typeof(Cloth), 1044286, 7, 1044287);
            AddRes(index, typeof(SpidersSilk), 1002127, 3, 501913);
            AddSkill(index, SkillName.Magery, 60, 100);

            index = AddCraft(typeof(MeditHat), "Roupa Magica", "Meditation Hat", 70.0, 110.0, typeof(Cloth), 1044286, 7, 1044287);
            AddRes(index, typeof(Bloodmoss), 1002127, 7, 501913);
            AddSkill(index, SkillName.Magery, 60, 100);

            index = AddCraft(typeof(MeditHat), "Roupa Magica", "Fancy Dress Of Bless", 80.0, 110.0, typeof(Cloth), 1044286, 16, 1044287);
            AddRes(index, typeof(SpidersSilk), 1002127, 14, 501913);
            AddSkill(index, SkillName.Magery, 75, 100);

            index = AddCraft(typeof(MeditHat), "Roupa Magica", "Robe Of Bless", 70.0, 110.0, typeof(Cloth), 1044286, 16, 1044287);
            AddRes(index, typeof(Bloodmoss), 1002127, 14, 501913);
            AddSkill(index, SkillName.Magery, 75, 100);

            index = AddCraft(typeof(BodySashOfStrenght), "Roupa Magica", "Body Sash Of Strenght", 70.0, 110.0, typeof(Cloth), 1044286, 4, 1044287);
            AddRes(index, typeof(Nightshade), 1002127, 8, 501913);
            AddSkill(index, SkillName.Magery, 65, 100);

            index = AddCraft(typeof(CloakOfIllusion), "Roupa Magica", "Cloak Of Illusion", 95.0, 110.0, typeof(Cloth), 1044286, 14, 1044287);
            AddRes(index, typeof(BlackPearl), 1002127, 14, 501913);
            AddSkill(index, SkillName.Magery, 785, 100);

            index = AddCraft(typeof(ShortPantsOfBrain), "Roupa Magica", "Pants Of Brain", 75.0, 110.0, typeof(Cloth), 1044286, 8, 1044287);
            AddRes(index, typeof(Bloodmoss), 1002127, 8, 501913);
            AddSkill(index, SkillName.Magery, 70, 100);

            index = AddCraft(typeof(ShirtOfDexterity), "Roupa Magica", "Shirt Of Dexterity", 75.0, 110.0, typeof(Cloth), 1044286, 8, 1044287);
            AddRes(index, typeof(SulfurousAsh), 1002127, 6, 501913);
            AddSkill(index, SkillName.Magery, 60, 100);





            AddCraft(typeof(StuddedChestCyclop), "Couro Especial", "Cyclop Studded Chest", 65.0, 100.0, typeof(CyclopsLeather), 1044462, 12, 1044463);
            AddCraft(typeof(StuddedArmsCyclop), "Couro Especial", "Cyclop Studded Arms", 65.0, 100.0, typeof(CyclopsLeather), 1044462, 4, 1044463);
            AddCraft(typeof(StuddedGlovesCyclop), "Couro Especial", "Cyclop Studded Gloves", 65.0, 100.0, typeof(CyclopsLeather), 1044462, 3, 1044463);
            AddCraft(typeof(StuddedGorgetCyclop), "Couro Especial", "Cyclop Studded Gorget", 65.0, 100.0, typeof(CyclopsLeather), 1044462, 4, 1044463);
            AddCraft(typeof(StuddedLegsCyclop), "Couro Especial", "Cyclop Studded Legs", 65.0, 100.0, typeof(CyclopsLeather), 1044462, 10, 1044463);
            AddCraft(typeof(LeatherCapCyclop), "Couro Especial", "Cyclop Studded Cap", 65.0, 100.0, typeof(CyclopsLeather), 1044462, 2, 1044463);

            AddCraft(typeof(StuddedChestGargoyle), "Couro Especial", "Gargoyle Studded Chest", 75.0, 100.0, typeof(GargoyleLeather), 1044462, 12, 1044463);
            AddCraft(typeof(StuddedArmsGargoyle), "Couro Especial", "Gargoyle Studded Arms", 75.0, 100.0, typeof(GargoyleLeather), 1044462, 4, 1044463);
            AddCraft(typeof(StuddedGlovesGargoyle), "Couro Especial", "Gargoyle Studded Gloves", 75.0, 100.0, typeof(GargoyleLeather), 1044462, 3, 1044463);
            AddCraft(typeof(StuddedGorgetGargoyle), "Couro Especial", "Gargoyle Studded Gorget", 75.0, 100.0, typeof(GargoyleLeather), 1044462, 4, 1044463);
            AddCraft(typeof(StuddedLegsGargoyle), "Couro Especial", "Gargoyle Studded Legs", 75.0, 100.0, typeof(GargoyleLeather), 1044462, 10, 1044463);
            AddCraft(typeof(LeatherCapGargoyle), "Couro Especial", "Gargoyle Studded Cap", 75.0, 100.0, typeof(GargoyleLeather), 1044462, 2, 1044463);

            AddCraft(typeof(StuddedChestTerathan), "Couro Especial", "Terathan Studded Chest", 80.0, 100.0, typeof(TerathanLeather), 1044462, 12, 1044463);
            AddCraft(typeof(StuddedArmsTerathan), "Couro Especial", "Terathan Studded Arms", 80.0, 100.0, typeof(TerathanLeather), 1044462, 4, 1044463);
            AddCraft(typeof(StuddedGlovesTerathan), "Couro Especial", "Terathan Studded Gloves", 80.0, 100.0, typeof(TerathanLeather), 1044462, 3, 1044463);
            AddCraft(typeof(StuddedGorgetTerathan), "Couro Especial", "Terathan Studded Gorget", 80.0, 100.0, typeof(TerathanLeather), 1044462, 4, 1044463);
            AddCraft(typeof(StuddedLegsTerathan), "Couro Especial", "Terathan Studded Legs", 80.0, 100.0, typeof(TerathanLeather), 1044462, 10, 1044463);
            AddCraft(typeof(LeatherCapTerathan), "Couro Especial", "Terathan Studded Cap", 80.0, 100.0, typeof(TerathanLeather), 1044462, 2, 1044463);

            AddCraft(typeof(StuddedChestDaemon), "Couro Especial", "Daemon Studded Chest", 86.0, 120.0, typeof(DaemonLeather), 1044462, 12, 1044463);
            AddCraft(typeof(StuddedArmsDaemon), "Couro Especial", "Daemon Studded Arms", 86.0, 120.0, typeof(DaemonLeather), 1044462, 4, 1044463);
            AddCraft(typeof(StuddedGlovesDaemon), "Couro Especial", "Daemon Studded Gloves", 86.0, 120.0, typeof(DaemonLeather), 1044462, 3, 1044463);
            AddCraft(typeof(StuddedGorgetDaemon), "Couro Especial", "Daemon Studded Gorget", 86.0, 120.0, typeof(DaemonLeather), 1044462, 4, 1044463);
            AddCraft(typeof(StuddedLegsDaemon), "Couro Especial", "Daemon Studded Legs", 86.0, 120.0, typeof(DaemonLeather), 1044462, 10, 1044463);
            AddCraft(typeof(LeatherCapDaemon), "Couro Especial", "Daemon Studded Cap", 86.0, 120.0, typeof(DaemonLeather), 1044462, 2, 1044463);

            AddCraft(typeof(StuddedChestDragon), "Couro Especial", "Dragon Studded Chest", 92.0, 120.0, typeof(DragonLeather), 1044462, 12, 1044463);
            AddCraft(typeof(StuddedArmsDragon), "Couro Especial", "Dragon Studded Arms", 92.0, 120.0, typeof(DragonLeather), 1044462, 4, 1044463);
            AddCraft(typeof(StuddedGlovesDragon), "Couro Especial", "Dragon Studded Gloves", 92.0, 120.0, typeof(DragonLeather), 1044462, 3, 1044463);
            AddCraft(typeof(StuddedGorgetDragon), "Couro Especial", "Dragon Studded Gorget", 92.0, 120.0, typeof(DragonLeather), 1044462, 4, 1044463);
            AddCraft(typeof(StuddedLegsDragon), "Couro Especial", "Dragon Studded Legs", 92.0, 120.0, typeof(DragonLeather), 1044462, 10, 1044463);
            AddCraft(typeof(LeatherCapDragon), "Couro Especial", "Dragon Studded Cap", 92.0, 120.0, typeof(DragonLeather), 1044462, 2, 1044463);

            AddCraft(typeof(StuddedChestZZ), "Couro Especial", "ZZ Studded Chest", 95.0, 120.0, typeof(ZZLeather), 1044462, 12, 1044463);
            AddCraft(typeof(StuddedArmsZZ), "Couro Especial", "ZZ Studded Arms", 95.0, 120.0, typeof(ZZLeather), 1044462, 4, 1044463);
            AddCraft(typeof(StuddedGlovesZZ), "Couro Especial", "ZZ Studded Gloves", 95.0, 120.0, typeof(ZZLeather), 1044462, 3, 1044463);
            AddCraft(typeof(StuddedGorgetZZ), "Couro Especial", "ZZ Studded Gorget", 95.0, 120.0, typeof(ZZLeather), 1044462, 4, 1044463);
            AddCraft(typeof(StuddedLegsZZ), "Couro Especial", "ZZ Studded Legs", 95.0, 120.0, typeof(ZZLeather), 1044462, 10, 1044463);
            AddCraft(typeof(LeatherCapZZ), "Couro Especial", "ZZ Studded Cap", 95.0, 120.0, typeof(ZZLeather), 1044462, 2, 1044463);

            AddCraft(typeof(StuddedChestDragonGreen), "Couro Especial", "DragonGreen Studded Chest", 100.0, 120.0, typeof(DragonGreenLeather), 1044462, 12, 1044463);
            AddCraft(typeof(StuddedArmsDragonGreen), "Couro Especial", "DragonGreen Studded Arms", 100.0, 120.0, typeof(DragonGreenLeather), 1044462, 4, 1044463);
            AddCraft(typeof(StuddedGlovesDragonGreen), "Couro Especial", "DragonGreen Studded Gloves", 100.0, 120.0, typeof(DragonGreenLeather), 1044462, 3, 1044463);
            AddCraft(typeof(StuddedGorgetDragonGreen), "Couro Especial", "DragonGreen Studded Gorget", 100.0, 120.0, typeof(DragonGreenLeather), 1044462, 4, 1044463);
            AddCraft(typeof(StuddedLegsDragonGreen), "Couro Especial", "DragonGreen Studded Legs", 100.0, 120.0, typeof(DragonGreenLeather), 1044462, 10, 1044463);
            AddCraft(typeof(LeatherCapDragonGreen), "Couro Especial", "DragonGreen Studded Cap", 100.0, 120.0, typeof(DragonGreenLeather), 1044462, 2, 1044463);





			// Set the overridable material
			SetSubRes( typeof( Leather ), 1049150 );

			// Add every material you want the player to be able to choose from
			// This will override the overridable material
			AddSubRes( typeof( Leather ),		1049150, 00.0, 1044462, 1049311 );
			AddSubRes( typeof( SpinedLeather ),	1049151, 65.0, 1044462, 1049311 );
			AddSubRes( typeof( HornedLeather ),	1049152, 80.0, 1044462, 1049311 );
			AddSubRes( typeof( BarbedLeather ),	1049153, 99.0, 1044462, 1049311 );

			MarkOption = true;
            Repair = true; // Core.AOS;
            CanEnhance = false; // Core.AOS;
		}
	}
}