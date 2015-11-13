using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefBlacksmithy : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Blacksmith;	}
		}

		public override int GumpTitleNumber
		{
			get { return 1044002; } // <CENTER>BLACKSMITHY MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefBlacksmithy();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		private DefBlacksmithy() : base( 3, 3, 1.20 )// base( 1, 2, 1.7 )
		{
			/*
			
			base( MinCraftEffect, MaxCraftEffect, Delay )
			
			MinCraftEffect	: The minimum number of time the mobile will play the craft effect
			MaxCraftEffect	: The maximum number of time the mobile will play the craft effect
			Delay			: The delay between each craft effect
			
			Example: (3, 6, 1.7) would make the mobile do the PlayCraftEffect override
			function between 3 and 6 time, with a 1.7 second delay each time.
			
			*/ 
		}

		private static Type typeofAnvil = typeof( AnvilAttribute );
		private static Type typeofForge = typeof( ForgeAttribute );

        

		public static void CheckAnvilAndForge( Mobile from, int range, out bool anvil, out bool forge )
		{
			anvil = false;
			forge = false;

			Map map = from.Map;

			if ( map == null )
				return;

			IPooledEnumerable eable = map.GetItemsInRange( from.Location, range );

			foreach ( Item item in eable )
			{
				Type type = item.GetType();

				bool isAnvil = ( type.IsDefined( typeofAnvil, false ) || item.ItemID == 4015 || item.ItemID == 4016 || item.ItemID == 0x2DD5 || item.ItemID == 0x2DD6 );
				bool isForge = ( type.IsDefined( typeofForge, false ) || item.ItemID == 4017 || (item.ItemID >= 6522 && item.ItemID <= 6569) || item.ItemID == 0x2DD8 );

				if ( isAnvil || isForge )
				{
					if ( (from.Z + 16) < item.Z || (item.Z + 16) < from.Z || !from.InLOS( item ) )
						continue;

					anvil = anvil || isAnvil;
					forge = forge || isForge;

					if ( anvil && forge )
						break;
				}
			}

			eable.Free();

			for ( int x = -range; (!anvil || !forge) && x <= range; ++x )
			{
				for ( int y = -range; (!anvil || !forge) && y <= range; ++y )
				{
					StaticTile[] tiles = map.Tiles.GetStaticTiles( from.X+x, from.Y+y, true );

					for ( int i = 0; (!anvil || !forge) && i < tiles.Length; ++i )
					{
						int id = tiles[i].ID;

						bool isAnvil = ( id == 4015 || id == 4016 || id == 0x2DD5 || id == 0x2DD6 );
						bool isForge = ( id == 4017 || (id >= 6522 && id <= 6569) || id == 0x2DD8 );

						if ( isAnvil || isForge )
						{
							if ( (from.Z + 16) < tiles[i].Z || (tiles[i].Z + 16) < from.Z || !from.InLOS( new Point3D( from.X+x, from.Y+y, tiles[i].Z + (tiles[i].Height/2) + 1 ) ) )
								continue;

							anvil = anvil || isAnvil;
							forge = forge || isForge;
						}
					}
				}
			}
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckTool( tool, from ) )
				return 1048146; // If you have a tool equipped, you must use that tool.
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			bool anvil, forge;
			CheckAnvilAndForge( from, 2, out anvil, out forge );

			if ( anvil && forge )
				return 0;

			return 1044267; // You must be near an anvil and a forge to smith items.
		}

		public override void PlayCraftEffect( Mobile from )
		{
			// no animation, instant sound
			//if ( from.Body.Type == BodyType.Human && !from.Mounted )
			//	from.Animate( 9, 5, 1, true, false, 0 );
			//new InternalTimer( from ).Start();

			from.PlaySound( 0x2A );
		}

		// Delay to synchronize the sound with the hit on the anvil
		private class InternalTimer : Timer
		{
			private Mobile m_From;

			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( 0.7 ) )
			{
				m_From = from;
			}

			protected override void OnTick()
			{
				m_From.PlaySound( 0x2A );
			}
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
            if (toolBroken)
            {
                from.SendLocalizedMessage(1044038); // You have worn out your tool
            }

			if ( failed )
			{
				if ( lostMaterial )
                {
                    return 1044043; // You failed to create the item, and some of your materials are lost.
                }
				else
                {
					return 1044157; // You failed to create the item, but no materials were lost.
                }
			}
			else
			{
                if (quality == 0)
                {
                    return 502785; // You were barely able to make this item.  It's quality is below average.
                }
                else if (makersMark && quality == 2)
                {
                    return 1044156; // You create an exceptional quality item and affix your maker's mark.
                }
                else if (quality == 2)
                {
                    return 1044155; // You create an exceptional quality item.
                }
                else
                {
                    return 1044154; // You create the item.
                }
			}
		}

		public override void InitCraftList()
		{
			/*
			Synthax for a SIMPLE craft item
			AddCraft( ObjectType, Group, MinSkill, MaxSkill, ResourceType, Amount, Message )
			
			ObjectType		: The type of the object you want to add to the build list.
			Group			: The group in wich the object will be showed in the craft menu.
			MinSkill		: The minimum of skill value
			MaxSkill		: The maximum of skill value
			ResourceType	: The type of the resource the mobile need to create the item
			Amount			: The amount of the ResourceType it need to create the item
			Message			: String or Int for Localized.  The message that will be sent to the mobile, if the specified resource is missing.
			
			Synthax for a COMPLEXE craft item.  A complexe item is an item that need either more than
			only one skill, or more than only one resource.
			
			Coming soon....
			*/

			#region Ringmail
            //AddCraft( typeof( RingmailGloves ), 1011076, 1025099, 12.0, 62.0, typeof( IronIngot ), 1044036, 10, 1044037 );
            //AddCraft( typeof( RingmailLegs ), 1011076, 1025104, 19.4, 69.4, typeof( IronIngot ), 1044036, 16, 1044037 );
            //AddCraft( typeof( RingmailArms ), 1011076, 1025103, 16.9, 66.9, typeof( IronIngot ), 1044036, 14, 1044037 );
            //AddCraft( typeof( RingmailChest ), 1011076, 1025100, 21.9, 71.9, typeof( IronIngot ), 1044036, 18, 1044037 );
			#endregion

			#region Chainmail
            //AddCraft( typeof( ChainCoif ), 1011077, 1025051, 14.5, 64.5, typeof( IronIngot ), 1044036, 10, 1044037 );
            //AddCraft( typeof( ChainLegs ), 1011077, 1025054, 36.7, 86.7, typeof( IronIngot ), 1044036, 18, 1044037 );
            //AddCraft( typeof( ChainChest ), 1011077, 1025055, 39.1, 89.1, typeof( IronIngot ), 1044036, 20, 1044037 );
			#endregion

			//int index = -1;

			#region Platemail
            //AddCraft( typeof( PlateArms ), 1011078, 1025136, 66.3, 116.3, typeof( IronIngot ), 1044036, 18, 1044037 );
            //AddCraft( typeof( PlateGloves ), 1011078, 1025140, 58.9, 108.9, typeof( IronIngot ), 1044036, 12, 1044037 );
            //AddCraft( typeof( PlateGorget ), 1011078, 1025139, 56.4, 106.4, typeof( IronIngot ), 1044036, 10, 1044037 );
            //AddCraft( typeof( PlateLegs ), 1011078, 1025137, 68.8, 118.8, typeof( IronIngot ), 1044036, 20, 1044037 );
            //AddCraft( typeof( PlateChest ), 1011078, 1046431, 75.0, 125.0, typeof( IronIngot ), 1044036, 25, 1044037 );
            //AddCraft( typeof( FemalePlateChest ), 1011078, 1046430, 44.1, 94.1, typeof( IronIngot ), 1044036, 20, 1044037 );

            //if ( Core.AOS ) // exact pre-aos functionality unknown
            //    AddCraft( typeof( DragonBardingDeed ), 1011078, 1053012, 72.5, 122.5, typeof( IronIngot ), 1044036, 750, 1044037 );

            //if( Core.SE )
            //{
				
            //    index = AddCraft( typeof( PlateMempo ), 1011078, 1030180, 80.0, 130.0, typeof( IronIngot ), 1044036, 18, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );

            //    index = AddCraft( typeof( PlateDo ), 1011078, 1030184, 80.0, 130.0, typeof( IronIngot ), 1044036, 28, 1044037 ); //Double check skill
            //    SetNeededExpansion( index, Expansion.SE );

            //    index = AddCraft( typeof( PlateHiroSode ), 1011078, 1030187, 80.0, 130.0, typeof( IronIngot ), 1044036, 16, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );

            //    index = AddCraft( typeof( PlateSuneate ), 1011078, 1030195, 65.0, 115.0, typeof( IronIngot ), 1044036, 20, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );

            //    index = AddCraft( typeof( PlateHaidate ), 1011078, 1030200, 65.0, 115.0, typeof( IronIngot ), 1044036, 20, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );
				
            //}
			#endregion

			#region Helmets
            //AddCraft( typeof( Bascinet ), 1011079, 1025132, 8.3, 58.3, typeof( IronIngot ), 1044036, 15, 1044037 );
            //AddCraft( typeof( CloseHelm ), 1011079, 1025128, 37.9, 87.9, typeof( IronIngot ), 1044036, 15, 1044037 );
            //AddCraft( typeof( Helmet ), 1011079, 1025130, 37.9, 87.9, typeof( IronIngot ), 1044036, 15, 1044037 );
            //AddCraft( typeof( NorseHelm ), 1011079, 1025134, 37.9, 87.9, typeof( IronIngot ), 1044036, 15, 1044037 );
            //AddCraft( typeof( PlateHelm ), 1011079, 1025138, 62.6, 112.6, typeof( IronIngot ), 1044036, 15, 1044037 );
			
            //if( Core.SE )
            //{
            //    index = AddCraft( typeof( ChainHatsuburi ), 1011079, 1030175, 30.0, 80.0, typeof( IronIngot ), 1044036, 20, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );

            //    index = AddCraft( typeof( PlateHatsuburi ), 1011079, 1030176, 45.0, 95.0, typeof( IronIngot ), 1044036, 20, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );

            //    index = AddCraft( typeof( HeavyPlateJingasa ), 1011079, 1030178, 45.0, 95.0, typeof( IronIngot ), 1044036, 20, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );
				
            //    index = AddCraft( typeof( LightPlateJingasa ), 1011079, 1030188, 45.0, 95.0, typeof( IronIngot ), 1044036, 20, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );
				
            //    index = AddCraft( typeof( SmallPlateJingasa ), 1011079, 1030191, 45.0, 95.0, typeof( IronIngot ), 1044036, 20, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );

            //    index = AddCraft( typeof( DecorativePlateKabuto ), 1011079, 1030179, 90.0, 140.0, typeof( IronIngot ), 1044036, 25, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );
				
            //    index = AddCraft( typeof( PlateBattleKabuto ), 1011079, 1030192, 90.0, 140.0, typeof( IronIngot ), 1044036, 25, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );

            //    index = AddCraft( typeof( StandardPlateKabuto ), 1011079, 1030196, 90.0, 140.0, typeof( IronIngot ), 1044036, 25, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );

            //    if (Core.ML)
            //    {
            //        index = AddCraft(typeof(Circlet), 1011079, 1032645, 62.1, 112.1, typeof(IronIngot), 1044036, 6, 1044037);
            //        SetNeededExpansion(index, Expansion.ML);

            //        index = AddCraft(typeof(RoyalCirclet), 1011079, 1032646, 70.0, 120.0, typeof(IronIngot), 1044036, 6, 1044037);
            //        SetNeededExpansion(index, Expansion.ML);

            //        index = AddCraft(typeof(GemmedCirclet), 1011079, 1032647, 75.0, 125.0, typeof(IronIngot), 1044036, 6, 1044037);
            //        AddRes(index, typeof(Tourmaline), 1044237, 1, 1044240);
            //        AddRes(index, typeof(Amethyst), 1044236, 1, 1044240);
            //        AddRes(index, typeof(BlueDiamond), 1032696, 1, 1044240);
            //        SetNeededExpansion(index, Expansion.ML);
            //    }

            //}
			#endregion

			#region Shields
            //AddCraft( typeof( Buckler ), 1011080, 1027027, -25.0, 25.0, typeof( IronIngot ), 1044036, 10, 1044037 );
            //AddCraft( typeof( BronzeShield ), 1011080, 1027026, -15.2, 34.8, typeof( IronIngot ), 1044036, 12, 1044037 );
            //AddCraft( typeof( HeaterShield ), 1011080, 1027030, 24.3, 74.3, typeof( IronIngot ), 1044036, 18, 1044037 );
            //AddCraft( typeof( MetalShield ), 1011080, 1027035, -10.2, 39.8, typeof( IronIngot ), 1044036, 14, 1044037 );
            //AddCraft( typeof( MetalKiteShield ), 1011080, 1027028, 4.6, 54.6, typeof( IronIngot ), 1044036, 16, 1044037 );
            //AddCraft( typeof( WoodenKiteShield ), 1011080, 1027032, -15.2, 34.8, typeof( IronIngot ), 1044036, 8, 1044037 );

            //if ( Core.AOS )
            //{
            //    AddCraft( typeof( ChaosShield ), 1011080, 1027107, 85.0, 135.0, typeof( IronIngot ), 1044036, 25, 1044037 );
            //    AddCraft( typeof( OrderShield ), 1011080, 1027108, 85.0, 135.0, typeof( IronIngot ), 1044036, 25, 1044037 );
            //}
			#endregion

			#region Bladed



            //if ( Core.AOS )
            //    AddCraft( typeof( BoneHarvester ), 1011081, 1029915, 33.0, 83.0, typeof( IronIngot ), 1044036, 10, 1044037 );

			//AddCraft( typeof( Broadsword ), 1011081, 1023934, 35.4, 85.4, typeof( IronIngot ), 1044036, 10, 1044037 );

            //if ( Core.AOS )
            //    AddCraft( typeof( CrescentBlade ), 1011081, 1029921, 45.0, 95.0, typeof( IronIngot ), 1044036, 14, 1044037 );

            //AddCraft( typeof( Cutlass ), 1011081, 1025185, 24.3, 74.3, typeof( IronIngot ), 1044036, 8, 1044037 );
            //AddCraft( typeof( Dagger ), 1011081, 1023921, -0.4, 49.6, typeof( IronIngot ), 1044036, 3, 1044037 );
            //AddCraft( typeof( Katana ),1011081, 1025119, 44.1, 94.1, typeof( IronIngot ), 1044036, 8, 1044037 );
            //AddCraft( typeof( Kryss ), 1011081, 1025121, 36.7, 86.7, typeof( IronIngot ), 1044036, 8, 1044037 );
            //AddCraft( typeof( Longsword ), 1011081, 1023937, 28.0, 78.0, typeof( IronIngot ), 1044036, 12, 1044037 );
            //AddCraft( typeof( Scimitar ), 1011081, 1025046, 31.7, 81.7, typeof( IronIngot ), 1044036, 10, 1044037 );
            //AddCraft( typeof( VikingSword ), 1011081, 1025049, 24.3, 74.3, typeof( IronIngot ), 1044036, 14, 1044037 );


            AddCraft(typeof(SwordIron), "Swordsmanship", "Sword Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordRusty), "Swordsmanship", "Sword Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordOldCopper), "Swordsmanship", "Sword Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordDullCopper), "Swordsmanship", "Sword Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordRuby), "Swordsmanship", "Sword Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordCopper), "Swordsmanship", "Sword Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordBronze), "Swordsmanship", "Sword Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordShadow), "Swordsmanship", "Sword Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordSilver), "Swordsmanship", "Sword Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordMercury), "Swordsmanship", "Sword Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordRose), "Swordsmanship", "Sword Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordGold), "Swordsmanship", "Sword Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordAgapite), "Swordsmanship", "Sword Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordVerite), "Swordsmanship", "Sword Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordPlutonio), "Swordsmanship", "Sword Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordBloodRock), "Swordsmanship", "Sword BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordValorite), "Swordsmanship", "Sword Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordBlackRock), "Swordsmanship", "Sword BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordMytheril), "Swordsmanship", "Sword Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SwordAqua), "Swordsmanship", "Sword Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);

            AddCraft(typeof(BardicheIron), "Swordsmanship", "Bardiche Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheRusty), "Swordsmanship", "Bardiche Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheOldCopper), "Swordsmanship", "Bardiche Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheDullCopper), "Swordsmanship", "Bardiche Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheRuby), "Swordsmanship", "Bardiche Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheCopper), "Swordsmanship", "Bardiche Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheBronze), "Swordsmanship", "Bardiche Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheShadow), "Swordsmanship", "Bardiche Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheSilver), "Swordsmanship", "Bardiche Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheMercury), "Swordsmanship", "Bardiche Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheRose), "Swordsmanship", "Bardiche Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheGold), "Swordsmanship", "Bardiche Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheAgapite), "Swordsmanship", "Bardiche Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheVerite), "Swordsmanship", "Bardiche Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardichePlutonio), "Swordsmanship", "Bardiche Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheBloodRock), "Swordsmanship", "Bardiche BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheValorite), "Swordsmanship", "Bardiche Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheBlackRock), "Swordsmanship", "Bardiche BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheMytheril), "Swordsmanship", "Bardiche Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BardicheAqua), "Swordsmanship", "Bardiche Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);

            AddCraft(typeof(HalberdIron), "Swordsmanship", "Halberd Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdRusty), "Swordsmanship", "Halberd Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdOldCopper), "Swordsmanship", "Halberd Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdDullCopper), "Swordsmanship", "Halberd Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdRuby), "Swordsmanship", "Halberd Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdCopper), "Swordsmanship", "Halberd Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdBronze), "Swordsmanship", "Halberd Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdShadow), "Swordsmanship", "Halberd Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdSilver), "Swordsmanship", "Halberd Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdMercury), "Swordsmanship", "Halberd Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdRose), "Swordsmanship", "Halberd Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdGold), "Swordsmanship", "Halberd Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdAgapite), "Swordsmanship", "Halberd Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdVerite), "Swordsmanship", "Halberd Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdPlutonio), "Swordsmanship", "Halberd Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdBloodRock), "Swordsmanship", "Halberd BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdValorite), "Swordsmanship", "Halberd Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdBlackRock), "Swordsmanship", "Halberd BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdMytheril), "Swordsmanship", "Halberd Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HalberdAqua), "Swordsmanship", "Halberd Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);

            AddCraft(typeof(LargeBattleAxeIron), "Swordsmanship", "Battle Axe Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeRusty), "Swordsmanship", "Battle Axe Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeOldCopper), "Swordsmanship", "Battle Axe Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeDullCopper), "Swordsmanship", "Battle Axe Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeRuby), "Swordsmanship", "Battle Axe Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeCopper), "Swordsmanship", "Battle Axe Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeBronze), "Swordsmanship", "Battle Axe Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeShadow), "Swordsmanship", "Battle Axe Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeSilver), "Swordsmanship", "Battle Axe Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeMercury), "Swordsmanship", "Battle Axe Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeRose), "Swordsmanship", "Battle Axe Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeGold), "Swordsmanship", "Battle Axe Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeAgapite), "Swordsmanship", "Battle Axe Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeVerite), "Swordsmanship", "Battle Axe Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxePlutonio), "Swordsmanship", "Battle Axe Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeBloodRock), "Swordsmanship", "Battle Axe BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeValorite), "Swordsmanship", "Battle Axe Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeBlackRock), "Swordsmanship", "Battle Axe BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeMytheril), "Swordsmanship", "Battle Axe Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(LargeBattleAxeAqua), "Swordsmanship", "Battle Axe Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);

            AddCraft(typeof(KryssIron), "Fencing", "Kryss Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssRusty), "Fencing", "Kryss Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssOldCopper), "Fencing", "Kryss Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssDullCopper), "Fencing", "Kryss Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssRuby), "Fencing", "Kryss Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssCopper), "Fencing", "Kryss Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssBronze), "Fencing", "Kryss Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssShadow), "Fencing", "Kryss Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssSilver), "Fencing", "Kryss Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssMercury), "Fencing", "Kryss Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssRose), "Fencing", "Kryss Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssGold), "Fencing", "Kryss Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssAgapite), "Fencing", "Kryss Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssVerite), "Fencing", "Kryss Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssPlutonio), "Fencing", "Kryss Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssBloodRock), "Fencing", "Kryss BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssValorite), "Fencing", "Kryss Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssBlackRock), "Fencing", "Kryss BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssMytheril), "Fencing", "Kryss Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(KryssAqua), "Fencing", "Kryss Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);

            AddCraft(typeof(ShortSpearIron), "Fencing", "Short Spear Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearRusty), "Fencing", "Short Spear Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearOldCopper), "Fencing", "Short Spear Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearDullCopper), "Fencing", "Short Spear Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearRuby), "Fencing", "Short Spear Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearCopper), "Fencing", "Short Spear Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearBronze), "Fencing", "Short Spear Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearShadow), "Fencing", "Short Spear Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearSilver), "Fencing", "Short Spear Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearMercury), "Fencing", "Short Spear Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearRose), "Fencing", "Short Spear Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearGold), "Fencing", "Short Spear Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearAgapite), "Fencing", "Short Spear Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearVerite), "Fencing", "Short Spear Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearPlutonio), "Fencing", "Short Spear Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearBloodRock), "Fencing", "Short Spear BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearValorite), "Fencing", "Short Spear Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearBlackRock), "Fencing", "Short Spear BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearMytheril), "Fencing", "Short Spear Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(ShortSpearAqua), "Fencing", "Short Spear Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);

            AddCraft(typeof(SpearIron), "Fencing", "Spear Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearRusty), "Fencing", "Spear Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearOldCopper), "Fencing", "Spear Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearDullCopper), "Fencing", "Spear Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearRuby), "Fencing", "Spear Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearCopper), "Fencing", "Spear Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearBronze), "Fencing", "Spear Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearShadow), "Fencing", "Spear Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearSilver), "Fencing", "Spear Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearMercury), "Fencing", "Spear Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearRose), "Fencing", "Spear Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearGold), "Fencing", "Spear Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearAgapite), "Fencing", "Spear Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearVerite), "Fencing", "Spear Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearPlutonio), "Fencing", "Spear Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearBloodRock), "Fencing", "Spear BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearValorite), "Fencing", "Spear Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearBlackRock), "Fencing", "Spear BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearMytheril), "Fencing", "Spear Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(SpearAqua), "Fencing", "Spear Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);

            AddCraft(typeof(WarForkIron), "Fencing", "War Fork Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkRusty), "Fencing", "War Fork Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkOldCopper), "Fencing", "War Fork Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkDullCopper), "Fencing", "War Fork Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkRuby), "Fencing", "War Fork Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkCopper), "Fencing", "War Fork Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkBronze), "Fencing", "War Fork Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkShadow), "Fencing", "War Fork Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkSilver), "Fencing", "War Fork Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkMercury), "Fencing", "War Fork Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkRose), "Fencing", "War Fork Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkGold), "Fencing", "War Fork Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkAgapite), "Fencing", "War Fork Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkVerite), "Fencing", "War Fork Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkPlutonio), "Fencing", "War Fork Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkBloodRock), "Fencing", "War Fork BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkValorite), "Fencing", "War Fork Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkBlackRock), "Fencing", "War Fork BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkMytheril), "Fencing", "War Fork Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarForkAqua), "Fencing", "War Fork Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);

            AddCraft(typeof(WarMaceIron), "Mace Fighting", "WarMace Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceRusty), "Mace Fighting", "WarMace Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceOldCopper), "Mace Fighting", "WarMace Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceDullCopper), "Mace Fighting", "WarMace Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceRuby), "Mace Fighting", "WarMace Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceCopper), "Mace Fighting", "WarMace Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceBronze), "Mace Fighting", "WarMace Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceShadow), "Mace Fighting", "WarMace Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceSilver), "Mace Fighting", "WarMace Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceMercury), "Mace Fighting", "WarMace Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceRose), "Mace Fighting", "WarMace Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceGold), "Mace Fighting", "WarMace Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceAgapite), "Mace Fighting", "WarMace Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceVerite), "Mace Fighting", "WarMace Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMacePlutonio), "Mace Fighting", "WarMace Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceBloodRock), "Mace Fighting", "WarMace BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceValorite), "Mace Fighting", "WarMace Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceBlackRock), "Mace Fighting", "WarMace BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceMytheril), "Mace Fighting", "WarMace Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarMaceAqua), "Mace Fighting", "WarMace Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);

            AddCraft(typeof(WarAxeIron), "Mace Fighting", "WarAxe Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeRusty), "Mace Fighting", "WarAxe Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeOldCopper), "Mace Fighting", "WarAxe Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeDullCopper), "Mace Fighting", "WarAxe Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeRuby), "Mace Fighting", "WarAxe Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeCopper), "Mace Fighting", "WarAxe Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeBronze), "Mace Fighting", "WarAxe Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeShadow), "Mace Fighting", "WarAxe Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeSilver), "Mace Fighting", "WarAxe Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeMercury), "Mace Fighting", "WarAxe Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeRose), "Mace Fighting", "WarAxe Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeGold), "Mace Fighting", "WarAxe Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeAgapite), "Mace Fighting", "WarAxe Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeVerite), "Mace Fighting", "WarAxe Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxePlutonio), "Mace Fighting", "WarAxe Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeBloodRock), "Mace Fighting", "WarAxe BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeValorite), "Mace Fighting", "WarAxe Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeBlackRock), "Mace Fighting", "WarAxe BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeMytheril), "Mace Fighting", "WarAxe Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(WarAxeAqua), "Mace Fighting", "WarAxe Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);

            AddCraft(typeof(PlateChestIron), "Plate", "PlateChest Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestRusty), "Plate", "PlateChest Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestOldCopper), "Plate", "PlateChest Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestDullCopper), "Plate", "PlateChest Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestRuby), "Plate", "PlateChest Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestCopper), "Plate", "PlateChest Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestBronze), "Plate", "PlateChest Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestShadow), "Plate", "PlateChest Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestSilver), "Plate", "PlateChest Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestMercury), "Plate", "PlateChest Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestRose), "Plate", "PlateChest Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestGold), "Plate", "PlateChest Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestAgapite), "Plate", "PlateChest Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestVerite), "Plate", "PlateChest Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestPlutonio), "Plate", "PlateChest Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestBloodRock), "Plate", "PlateChest BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestValorite), "Plate", "PlateChest Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestBlackRock), "Plate", "PlateChest BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestMytheril), "Plate", "PlateChest Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestAqua), "Plate", "PlateChest Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestEndurium), "Plate Magica", "PlateChest Endurium", 100.0, 120.0, typeof(EnduriumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestOldEndurium), "Plate Magica", "PlateChest OldEndurium", 100.0, 120.0, typeof(OldEnduriumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestGoldStone), "Plate Magica", "PlateChest GoldStone", 100.0, 120.0, typeof(GoldStoneIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestMaxMytheril), "Plate Magica", "PlateChest MaxMytheril", 100.0, 120.0, typeof(MaxMytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateChestMagma), "Plate Magica", "PlateChest Magma", 100.0, 120.0, typeof(MagmaIngot), 1044036, 14, 1044037);

            AddCraft(typeof(PlateArmsIron), "Plate", "PlateArms Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsRusty), "Plate", "PlateArms Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsOldCopper), "Plate", "PlateArms Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsDullCopper), "Plate", "PlateArms Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsRuby), "Plate", "PlateArms Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsCopper), "Plate", "PlateArms Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsBronze), "Plate", "PlateArms Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsShadow), "Plate", "PlateArms Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsSilver), "Plate", "PlateArms Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsMercury), "Plate", "PlateArms Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsRose), "Plate", "PlateArms Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsGold), "Plate", "PlateArms Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsAgapite), "Plate", "PlateArms Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsVerite), "Plate", "PlateArms Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsPlutonio), "Plate", "PlateArms Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsBloodRock), "Plate", "PlateArms BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsValorite), "Plate", "PlateArms Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsBlackRock), "Plate", "PlateArms BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsMytheril), "Plate", "PlateArms Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsAqua), "Plate", "PlateArms Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsEndurium), "Plate Magica", "PlateArms Endurium", 100.0, 120.0, typeof(EnduriumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsOldEndurium), "Plate Magica", "PlateArms OldEndurium", 100.0, 120.0, typeof(OldEnduriumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsGoldStone), "Plate Magica", "PlateArms GoldStone", 100.0, 120.0, typeof(GoldStoneIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsMaxMytheril), "Plate Magica", "PlateArms MaxMytheril", 100.0, 120.0, typeof(MaxMytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateArmsMagma), "Plate Magica", "PlateArms Magma", 100.0, 120.0, typeof(MagmaIngot), 1044036, 14, 1044037);

            AddCraft(typeof(PlateLegsIron), "Plate", "PlateLegs Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsRusty), "Plate", "PlateLegs Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsOldCopper), "Plate", "PlateLegs Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsDullCopper), "Plate", "PlateLegs Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsRuby), "Plate", "PlateLegs Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsCopper), "Plate", "PlateLegs Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsBronze), "Plate", "PlateLegs Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsShadow), "Plate", "PlateLegs Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsSilver), "Plate", "PlateLegs Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsMercury), "Plate", "PlateLegs Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsRose), "Plate", "PlateLegs Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsGold), "Plate", "PlateLegs Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsAgapite), "Plate", "PlateLegs Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsVerite), "Plate", "PlateLegs Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsPlutonio), "Plate", "PlateLegs Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsBloodRock), "Plate", "PlateLegs BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsValorite), "Plate", "PlateLegs Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsBlackRock), "Plate", "PlateLegs BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsMytheril), "Plate", "PlateLegs Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsAqua), "Plate", "PlateLegs Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsEndurium), "Plate Magica", "PlateLegs Endurium", 100.0, 120.0, typeof(EnduriumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsOldEndurium), "Plate Magica", "PlateLegs OldEndurium", 100.0, 120.0, typeof(OldEnduriumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsGoldStone), "Plate Magica", "PlateLegs GoldStone", 100.0, 120.0, typeof(GoldStoneIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsMaxMytheril), "Plate Magica", "PlateLegs MaxMytheril", 100.0, 120.0, typeof(MaxMytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateLegsMagma), "Plate Magica", "PlateLegs Magma", 100.0, 120.0, typeof(MagmaIngot), 1044036, 14, 1044037);

            AddCraft(typeof(PlateCloseHelmIron), "Plate", "PlateCloseHelm Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmRusty), "Plate", "PlateCloseHelm Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmOldCopper), "Plate", "PlateCloseHelm Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmDullCopper), "Plate", "PlateCloseHelm Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmRuby), "Plate", "PlateCloseHelm Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmCopper), "Plate", "PlateCloseHelm Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmBronze), "Plate", "PlateCloseHelm Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmShadow), "Plate", "PlateCloseHelm Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmSilver), "Plate", "PlateCloseHelm Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmMercury), "Plate", "PlateCloseHelm Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmRose), "Plate", "PlateCloseHelm Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmGold), "Plate", "PlateCloseHelm Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmAgapite), "Plate", "PlateCloseHelm Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmVerite), "Plate", "PlateCloseHelm Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmPlutonio), "Plate", "PlateCloseHelm Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmBloodRock), "Plate", "PlateCloseHelm BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmValorite), "Plate", "PlateCloseHelm Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmBlackRock), "Plate", "PlateCloseHelm BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmMytheril), "Plate", "PlateCloseHelm Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmAqua), "Plate", "PlateCloseHelm Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmEndurium), "Plate Magica", "PlateCloseHelm Endurium", 100.0, 120.0, typeof(EnduriumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmOldEndurium), "Plate Magica", "PlateCloseHelm OldEndurium", 100.0, 120.0, typeof(OldEnduriumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmGoldStone), "Plate Magica", "PlateCloseHelm GoldStone", 100.0, 120.0, typeof(GoldStoneIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmMaxMytheril), "Plate Magica", "PlateCloseHelm MaxMytheril", 100.0, 120.0, typeof(MaxMytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateCloseHelmMagma), "Plate Magica", "PlateCloseHelm Magma", 100.0, 120.0, typeof(MagmaIngot), 1044036, 14, 1044037);

            AddCraft(typeof(PlateGorgetIron), "Plate", "PlateGorget Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetRusty), "Plate", "PlateGorget Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetOldCopper), "Plate", "PlateGorget Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetDullCopper), "Plate", "PlateGorget Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetRuby), "Plate", "PlateGorget Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetCopper), "Plate", "PlateGorget Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetBronze), "Plate", "PlateGorget Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetShadow), "Plate", "PlateGorget Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetSilver), "Plate", "PlateGorget Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetMercury), "Plate", "PlateGorget Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetRose), "Plate", "PlateGorget Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetGold), "Plate", "PlateGorget Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetAgapite), "Plate", "PlateGorget Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetVerite), "Plate", "PlateGorget Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetPlutonio), "Plate", "PlateGorget Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetBloodRock), "Plate", "PlateGorget BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetValorite), "Plate", "PlateGorget Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetBlackRock), "Plate", "PlateGorget BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetMytheril), "Plate", "PlateGorget Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetAqua), "Plate", "PlateGorget Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetEndurium), "Plate Magica", "PlateGorget Endurium", 100.0, 120.0, typeof(EnduriumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetOldEndurium), "Plate Magica", "PlateGorget OldEndurium", 100.0, 120.0, typeof(OldEnduriumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetGoldStone), "Plate Magica", "PlateGorget GoldStone", 100.0, 120.0, typeof(GoldStoneIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetMaxMytheril), "Plate Magica", "PlateGorget MaxMytheril", 100.0, 120.0, typeof(MaxMytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGorgetMagma), "Plate Magica", "PlateGorget Magma", 100.0, 120.0, typeof(MagmaIngot), 1044036, 14, 1044037);

            AddCraft(typeof(PlateGlovesIron), "Plate", "PlateGloves Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesRusty), "Plate", "PlateGloves Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesOldCopper), "Plate", "PlateGloves Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesDullCopper), "Plate", "PlateGloves Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesRuby), "Plate", "PlateGloves Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesCopper), "Plate", "PlateGloves Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesBronze), "Plate", "PlateGloves Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesShadow), "Plate", "PlateGloves Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesSilver), "Plate", "PlateGloves Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesMercury), "Plate", "PlateGloves Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesRose), "Plate", "PlateGloves Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesGold), "Plate", "PlateGloves Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesAgapite), "Plate", "PlateGloves Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesVerite), "Plate", "PlateGloves Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesPlutonio), "Plate", "PlateGloves Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesBloodRock), "Plate", "PlateGloves BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesValorite), "Plate", "PlateGloves Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesBlackRock), "Plate", "PlateGloves BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesMytheril), "Plate", "PlateGloves Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesAqua), "Plate", "PlateGloves Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesEndurium), "Plate Magica", "PlateGloves Endurium", 100.0, 120.0, typeof(EnduriumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesOldEndurium), "Plate Magica", "PlateGloves OldEndurium", 100.0, 120.0, typeof(OldEnduriumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesGoldStone), "Plate Magica", "PlateGloves GoldStone", 100.0, 120.0, typeof(GoldStoneIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesMaxMytheril), "Plate Magica", "PlateGloves MaxMytheril", 100.0, 120.0, typeof(MaxMytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(PlateGlovesMagma), "Plate Magica", "PlateGloves Magma", 100.0, 120.0, typeof(MagmaIngot), 1044036, 14, 1044037);


            //if( Core.SE )
            //{
            //    index = AddCraft( typeof( NoDachi ), 1011081, 1030221, 75.0, 125.0, typeof( IronIngot ), 1044036, 18, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );
            //    index = AddCraft( typeof( Wakizashi ), 1011081, 1030223, 50.0, 100.0, typeof( IronIngot ), 1044036, 8, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );
            //    index = AddCraft( typeof( Lajatang ), 1011081, 1030226, 80.0, 130.0, typeof( IronIngot ), 1044036, 25, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );
            //    index = AddCraft( typeof( Daisho ), 1011081, 1030228, 60.0, 110.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );
            //    index = AddCraft( typeof( Tekagi ), 1011081, 1030230, 55.0, 105.0, typeof( IronIngot ), 1044036, 12, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );
            //    index = AddCraft( typeof( Shuriken ), 1011081, 1030231, 45.0, 95.0, typeof( IronIngot ), 1044036, 5, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );
            //    index = AddCraft( typeof( Kama ), 1011081, 1030232, 40.0, 90.0, typeof( IronIngot ), 1044036, 14, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );
            //    index = AddCraft( typeof( Sai ), 1011081, 1030234, 50.0, 100.0, typeof( IronIngot ), 1044036, 12, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );

            //    if( Core.ML )
            //    {
            //        index = AddCraft( typeof( RadiantScimitar ), 1011081, 1031571, 75.0, 125.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( WarCleaver ), 1011081, 1031567, 70.0, 120.0, typeof( IronIngot ), 1044036, 18, 1044037 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( ElvenSpellblade ), 1011081, 1031564, 70.0, 120.0, typeof( IronIngot ), 1044036, 14, 1044037 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( AssassinSpike ), 1011081, 1031565, 70.0, 120.0, typeof( IronIngot ), 1044036, 9, 1044037 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( Leafblade ), 1011081, 1031566, 70.0, 120.0, typeof( IronIngot ), 1044036, 12, 1044037 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( RuneBlade ), 1011081, 1031570, 70.0, 120.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( ElvenMachete ), 1011081, 1031573, 70.0, 120.0, typeof( IronIngot ), 1044036, 14, 1044037 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( RuneCarvingKnife ), 1011081, 1072915, 70.0, 120.0, typeof( IronIngot ), 1044036, 9, 1044037 );
            //        AddRes( index, typeof( DreadHornMane ), 1032682, 1, 1053098 );
            //        AddRes( index, typeof( Putrefication ), 1032678, 10, 1053098 );
            //        AddRes( index, typeof( Muculent ), 1032680, 10, 1053098 );
            //        AddRecipe( index, 0 );
            //        ForceNonExceptional( index );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( ColdForgedBlade ), 1011081, 1072916, 70.0, 120.0, typeof( IronIngot ), 1044036, 18, 1044037 );
            //        AddRes( index, typeof( GrizzledBones ), 1032684, 1, 1053098 );
            //        AddRes( index, typeof( Taint ), 1032684, 10, 1053098 );
            //        AddRes( index, typeof( Blight ), 1032675, 10, 1053098 );
            //        AddRecipe( index, 1 );
            //        ForceNonExceptional( index );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( OverseerSunderedBlade ), 1011081, 1072920, 70.0, 120.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //        AddRes( index, typeof( GrizzledBones ), 1032684, 1, 1053098 );
            //        AddRes( index, typeof( Blight ), 1032675, 10, 1053098 );
            //        AddRes( index, typeof( Scourge ), 1032677, 10, 1053098 );
            //        AddRecipe( index, 2 );
            //        ForceNonExceptional( index );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( LuminousRuneBlade ), 1011081, 1072922, 70.0, 120.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //        AddRes( index, typeof( GrizzledBones ), 1032684, 1, 1053098 );
            //        AddRes( index, typeof( Corruption ), 1032676, 10, 1053098 );
            //        AddRes( index, typeof( Putrefication ), 1032678, 10, 1053098 );
            //        AddRecipe( index, 3 );
            //        ForceNonExceptional( index );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( TrueSpellblade ), 1011081, 1073513, 75.0, 125.0, typeof( IronIngot ), 1044036, 14, 1044037 );
            //        AddRes( index, typeof( BlueDiamond ), 1032696, 1, 1044240 );
            //        AddRecipe( index, 4 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( IcySpellblade ), 1011081, 1073514, 75.0, 125.0, typeof( IronIngot ), 1044036, 14, 1044037 );
            //        AddRes( index, typeof( Turquoise ), 1032691, 1, 1044240 );
            //        AddRecipe( index, 5 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( FierySpellblade ), 1011081, 1073515, 75.0, 125.0, typeof( IronIngot ), 1044036, 14, 1044037 );
            //        AddRes( index, typeof( FireRuby ), 1032695, 1, 1044240 );
            //        AddRecipe( index, 6 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( SpellbladeOfDefense ), 1011081, 1073516, 75.0, 125.0, typeof( IronIngot ), 1044036, 18, 1044037 );
            //        AddRes( index, typeof( WhitePearl ), 1032694, 1, 1044240 );
            //        AddRecipe( index, 7 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( TrueAssassinSpike ), 1011081, 1073517, 75.0, 125.0, typeof( IronIngot ), 1044036, 9, 1044037 );
            //        AddRes( index, typeof( DarkSapphire ), 1032690, 1, 1044240 );
            //        AddRecipe( index, 8 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( ChargedAssassinSpike ), 1011081, 1073518, 75.0, 125.0, typeof( IronIngot ), 1044036, 9, 1044037 );
            //        AddRes( index, typeof( EcruCitrine ), 1032693, 1, 1044240 );
            //        AddRecipe( index, 9 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( MagekillerAssassinSpike ), 1011081, 1073519, 75.0, 125.0, typeof( IronIngot ), 1044036, 9, 1044037 );
            //        AddRes( index, typeof( BrilliantAmber ), 1032697, 1, 1044240 );
            //        AddRecipe( index, 10 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( WoundingAssassinSpike ), 1011081, 1073520, 75.0, 125.0, typeof( IronIngot ), 1044036, 9, 1044037 );
            //        AddRes( index, typeof( PerfectEmerald ), 1032692, 1, 1044240 );
            //        AddRecipe( index, 11 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( TrueLeafblade ), 1011081, 1073521, 75.0, 125.0, typeof( IronIngot ), 1044036, 12, 1044037 );
            //        AddRes( index, typeof( BlueDiamond ), 1032696, 1, 1044240 );
            //        AddRecipe( index, 12 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( Luckblade ), 1011081, 1073522, 75.0, 125.0, typeof( IronIngot ), 1044036, 12, 1044037 );
            //        AddRes( index, typeof( WhitePearl ), 1032694, 1, 1044240 );
            //        AddRecipe( index, 13 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( MagekillerLeafblade ), 1011081, 1073523, 75.0, 125.0, typeof( IronIngot ), 1044036, 12, 1044037 );
            //        AddRes( index, typeof( FireRuby ), 1032695, 1, 1044240 );
            //        AddRecipe( index, 14 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( LeafbladeOfEase ), 1011081, 1073524, 75.0, 125.0, typeof( IronIngot ), 1044036, 12, 1044037 );
            //        AddRes( index, typeof( PerfectEmerald ), 1032692, 1, 1044240 );
            //        AddRecipe( index, 15 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( KnightsWarCleaver ), 1011081, 1073525, 75.0, 125.0, typeof( IronIngot ), 1044036, 18, 1044037 );
            //        AddRes( index, typeof( PerfectEmerald ), 1032692, 1, 1044240 );
            //        AddRecipe( index, 16 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( ButchersWarCleaver ), 1011081, 1073526, 75.0, 125.0, typeof( IronIngot ), 1044036, 18, 1044037 );
            //        AddRes( index, typeof( Turquoise ), 1032691, 1, 1044240 );
            //        AddRecipe( index, 17 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( SerratedWarCleaver ), 1011081, 1073527, 75.0, 125.0, typeof( IronIngot ), 1044036, 18, 1044037 );
            //        AddRes( index, typeof( EcruCitrine ), 1032693, 1, 1044240 );
            //        AddRecipe( index, 18 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( TrueWarCleaver ), 1011081, 1073528, 75.0, 125.0, typeof( IronIngot ), 1044036, 18, 1044037 );
            //        AddRes( index, typeof( BrilliantAmber ), 1032697, 1, 1044240 );
            //        AddRecipe( index, 19 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( AdventurersMachete ), 1011081, 1073533, 75.0, 125.0, typeof( IronIngot ), 1044036, 14, 1044037 );
            //        AddRes( index, typeof( WhitePearl ), 1032694, 1, 1044240 );
            //        AddRecipe( index, 20 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( OrcishMachete ), 1011081, 1073534, 75.0, 125.0, typeof( IronIngot ), 1044036, 14, 1044037 );
            //        AddRes( index, typeof( Scourge ), 1072136, 1, 1042081 );
            //        AddRecipe( index, 21 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( MacheteOfDefense ), 1011081, 1073535, 75.0, 125.0, typeof( IronIngot ), 1044036, 14, 1044037 );
            //        AddRes( index, typeof( BrilliantAmber ), 1032697, 1, 1044240 );
            //        AddRecipe( index, 22 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( DiseasedMachete ), 1011081, 1073536, 75.0, 125.0, typeof( IronIngot ), 1044036, 14, 1044037 );
            //        AddRes( index, typeof( Blight ), 1072134, 1, 1042081 );
            //        AddRecipe( index, 23 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( RuneSabre ), 1011081, 1073537, 75.0, 125.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //        AddRes( index, typeof( Turquoise ), 1032691, 1, 1044240 );
            //        AddRecipe( index, 24 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( MagesRuneBlade ), 1011081, 1073538, 75.0, 125.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //        AddRes( index, typeof( BlueDiamond ), 1032696, 1, 1044240 );
            //        AddRecipe( index, 25 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( RuneBladeOfKnowledge ), 1011081, 1073539, 75.0, 125.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //        AddRes( index, typeof( EcruCitrine ), 1032693, 1, 1044240 );
            //        AddRecipe( index, 26 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( CorruptedRuneBlade ), 1011081, 1073540, 75.0, 125.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //        AddRes( index, typeof( Corruption ), 1072135, 1, 1042081 );
            //        AddRecipe( index, 27 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( TrueRadiantScimitar ), 1011081, 1073541, 75.0, 125.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //        AddRes( index, typeof( BrilliantAmber ), 1032697, 1, 1044240 );
            //        AddRecipe( index, 28 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( DarkglowScimitar ), 1011081, 1073542, 75.0, 125.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //        AddRes( index, typeof( DarkSapphire ), 1032690, 1, 1044240 );
            //        AddRecipe( index, 29 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( IcyScimitar ), 1011081, 1073543, 75.0, 125.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //        AddRes( index, typeof( DarkSapphire ), 1032690, 1, 1044240 );
            //        AddRecipe( index, 30 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( TwinklingScimitar ), 1011081, 1073544, 75.0, 125.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //        AddRes( index, typeof( DarkSapphire ), 1032690, 1, 1044240 );
            //        AddRecipe( index, 31 );
            //        SetNeededExpansion( index, Expansion.ML );

            //        index = AddCraft( typeof( BoneMachete ), 1011081, 1020526, 75.0, 125.0, typeof( IronIngot ), 1044036, 20, 1044037 );
            //        AddRes( index, typeof( Bone ), 1049064, 6, 1049063 );
            //        AddRecipe( index, 32 );
            //        SetNeededExpansion( index, Expansion.ML );
            //    }
            //}
			#endregion

			#region Axes
            //AddCraft( typeof( Axe ), 1011082, 1023913, 34.2, 84.2, typeof( IronIngot ), 1044036, 14, 1044037 );
            //AddCraft( typeof( BattleAxe ), 1011082, 1023911, 30.5, 80.5, typeof( IronIngot ), 1044036, 14, 1044037 );
            //AddCraft( typeof( DoubleAxe ), 1011082, 1023915, 29.3, 79.3, typeof( IronIngot ), 1044036, 12, 1044037 );
            //AddCraft( typeof( ExecutionersAxe ), 1011082, 1023909, 34.2, 84.2, typeof( IronIngot ), 1044036, 14, 1044037 );
            //AddCraft( typeof( LargeBattleAxe ), 1011082, 1025115, 28.0, 78.0, typeof( IronIngot ), 1044036, 12, 1044037 );
            //AddCraft( typeof( TwoHandedAxe ), 1011082, 1025187, 33.0, 83.0, typeof( IronIngot ), 1044036, 16, 1044037 );
            //AddCraft( typeof( WarAxe ), 1011082, 1025040, 39.1, 89.1, typeof( IronIngot ), 1044036, 16, 1044037 );

            //if( Core.ML )
            //{
            //    index = AddCraft( typeof( OrnateAxe ), 1011082, 1031572, 70.0, 120.0, typeof( IronIngot ), 1044036, 18, 1044037 );
            //    SetNeededExpansion( index, Expansion.ML );

            //    index = AddCraft( typeof( GuardianAxe ), 1011082, 1073545, 75.0, 125.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //    AddRes( index, typeof( BlueDiamond ), 1032696, 1, 1044240 );
            //    AddRecipe( index, 33 );
            //    SetNeededExpansion( index, Expansion.ML );

            //    index = AddCraft( typeof( SingingAxe ), 1011082, 1073546, 75.0, 125.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //    AddRes( index, typeof( BrilliantAmber ), 1032697, 1, 1044240 );
            //    AddRecipe( index, 34 );
            //    SetNeededExpansion( index, Expansion.ML );

            //    index = AddCraft( typeof( ThunderingAxe ), 1011082, 1073547, 75.0, 125.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //    AddRes( index, typeof( EcruCitrine ), 1032693, 1, 1044240 );
            //    AddRecipe( index, 35 );
            //    SetNeededExpansion( index, Expansion.ML );

            //    index = AddCraft( typeof( HeavyOrnateAxe ), 1011082, 1073548, 75.0, 125.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //    AddRes( index, typeof( Turquoise ), 1032691, 1, 1044240 );
            //    AddRecipe( index, 36 );
            //    SetNeededExpansion( index, Expansion.ML );
			 
            //}
			#endregion

			#region Pole Arms

			//AddCraft( typeof( Bardiche ), 1011083, 1023917, 31.7, 81.7, typeof( IronIngot ), 1044036, 18, 1044037 );

            //if ( Core.AOS )
            //    AddCraft( typeof( BladedStaff ), 1011083, 1029917, 40.0, 90.0, typeof( IronIngot ), 1044036, 12, 1044037 );

            //if ( Core.AOS )
            //    AddCraft( typeof( DoubleBladedStaff ), 1011083, 1029919, 45.0, 95.0, typeof( IronIngot ), 1044036, 16, 1044037 );

			//AddCraft( typeof( Halberd ), 1011083, 1025183, 39.1, 89.1, typeof( IronIngot ), 1044036, 20, 1044037 );

            //if ( Core.AOS )
            //    AddCraft( typeof( Lance ), 1011083, 1029920, 48.0, 98.0, typeof( IronIngot ), 1044036, 20, 1044037 );

            //if ( Core.AOS )
            //    AddCraft( typeof( Pike ), 1011083, 1029918, 47.0, 97.0, typeof( IronIngot ), 1044036, 12, 1044037 );

			//AddCraft( typeof( ShortSpear ), 1011083, 1025123, 45.3, 95.3, typeof( IronIngot ), 1044036, 6, 1044037 );

            //if ( Core.AOS )
            //    AddCraft( typeof( Scythe ), 1011083, 1029914, 39.0, 89.0, typeof( IronIngot ), 1044036, 14, 1044037 );

			//AddCraft( typeof( Spear ), 1011083, 1023938, 49.0, 99.0, typeof( IronIngot ), 1044036, 12, 1044037 );
			//AddCraft( typeof( WarFork ), 1011083, 1025125, 42.9, 92.9, typeof( IronIngot ), 1044036, 12, 1044037 );

			// Not craftable (is this an AOS change ??)
			//AddCraft( typeof( Pitchfork ), 1011083, 1023720, 36.1, 86.1, typeof( IronIngot ), 1044036, 12, 1044037 );
			#endregion

			#region Bashing
            //AddCraft( typeof( HammerPick ), 1011084, 1025181, 34.2, 84.2, typeof( IronIngot ), 1044036, 16, 1044037 );
            //AddCraft( typeof( Mace ), 1011084, 1023932, 14.5, 64.5, typeof( IronIngot ), 1044036, 6, 1044037 );
            //AddCraft( typeof( Maul ), 1011084, 1025179, 19.4, 69.4, typeof( IronIngot ), 1044036, 10, 1044037 );

            //if ( Core.AOS )
            //    AddCraft( typeof( Scepter ), 1011084, 1029916, 21.4, 71.4, typeof( IronIngot ), 1044036, 10, 1044037 );

			//AddCraft( typeof( WarMace ), 1011084, 1025127, 28.0, 78.0, typeof( IronIngot ), 1044036, 14, 1044037 );
			//AddCraft( typeof( WarHammer ), 1011084, 1025177, 34.2, 84.2, typeof( IronIngot ), 1044036, 16, 1044037 );

            //if( Core.SE )
            //{
            //    index = AddCraft( typeof( Tessen ), 1011084, 1030222, 85.0, 135.0, typeof( IronIngot ), 1044036, 16, 1044037 );
            //    AddSkill( index, SkillName.Tailoring, 50.0, 55.0 );
            //    AddRes( index, typeof( Cloth ), 1044286, 10, 1044287 );
            //    SetNeededExpansion( index, Expansion.SE );
            //}

            //if( Core.ML )
            //{
            //    index = AddCraft( typeof( DiamondMace ), 1011084, 1073568, 70.0, 120.0, typeof( IronIngot ), 1044036, 20, 1044037 );
            //    SetNeededExpansion( index, Expansion.ML );

            //    index = AddCraft( typeof( ShardThrasher ), 1011084, 1072918, 70.0, 120.0, typeof( IronIngot ), 1044036, 20, 1044037 );
            //    AddRes( index, typeof( EyeOfTheTravesty ), 1073126, 1, 1042081 );
            //    AddRes( index, typeof( Muculent ), 1072139, 10, 1042081 );
            //    AddRes( index, typeof( Corruption ), 1072135, 10, 1042081 );
            //    AddRecipe( index, 37 );
            //    ForceNonExceptional( index );
            //    SetNeededExpansion( index, Expansion.ML );

            //    index = AddCraft( typeof( RubyMace ), 1011084, 1073529, 75.0, 125.0, typeof( IronIngot ), 1044036, 20, 1044037 );
            //    AddRes( index, typeof( FireRuby ), 1032695, 1, 1044240 );
            //    AddRecipe( index, 38 );
            //    SetNeededExpansion( index, Expansion.ML );

            //    index = AddCraft( typeof( EmeraldMace ), 1011084, 1073530, 75.0, 125.0, typeof( IronIngot ), 1044036, 20, 1044037 );
            //    AddRes( index, typeof( PerfectEmerald ), 1032692, 1, 1044240 );
            //    AddRecipe( index, 39 );
            //    SetNeededExpansion( index, Expansion.ML );

            //    index = AddCraft( typeof( SapphireMace ), 1011084, 1073531, 75.0, 125.0, typeof( IronIngot ), 1044036, 20, 1044037 );
            //    AddRes( index, typeof( DarkSapphire ), 1032690, 1, 1044240 );
            //    AddRecipe( index, 40 );
            //    SetNeededExpansion( index, Expansion.ML );

            //    index = AddCraft( typeof( SilverEtchedMace ), 1011084, 1073532, 75.0, 125.0, typeof( IronIngot ), 1044036, 20, 1044037 );
            //    AddRes( index, typeof( BlueDiamond ), 1032696, 1, 1044240 );
            //    AddRecipe( index, 41 );
            //    SetNeededExpansion( index, Expansion.ML );
            //}
			#endregion

            #region Dragon Scale Armor
            //index = AddCraft( typeof( DragonGloves ), 1053114, 1029795, 68.9, 118.9, typeof( RedScales ), 1060883, 16, 1060884 );
            //SetUseSubRes2( index, true );

            //index = AddCraft( typeof( DragonHelm ), 1053114, 1029797, 72.6, 122.6, typeof( RedScales ), 1060883, 20, 1060884 );
            //SetUseSubRes2( index, true );

            //index = AddCraft( typeof( DragonLegs ), 1053114, 1029799, 78.8, 128.8, typeof( RedScales ), 1060883, 28, 1060884 );
            //SetUseSubRes2( index, true );

            //index = AddCraft( typeof( DragonArms ), 1053114, 1029815, 76.3, 126.3, typeof( RedScales ), 1060883, 24, 1060884 );
            //SetUseSubRes2( index, true );

            //index = AddCraft( typeof( DragonChest ), 1053114, 1029793, 85.0, 135.0, typeof( RedScales ), 1060883, 36, 1060884 );
            //SetUseSubRes2( index, true );
            #endregion
			
			// Set the overridable material
			SetSubRes( typeof( IronIngot ), 1044022 );

			// Add every material you want the player to be able to choose from
			// This will override the overridable material
            //AddSubRes( typeof( IronIngot ),			1044022, 00.0, 1044036, 1044267 );
            //AddSubRes( typeof( DullCopperIngot ),	1044023, 65.0, 1044036, 1044268 );
            //AddSubRes( typeof( ShadowIronIngot ),	1044024, 70.0, 1044036, 1044268 );
            //AddSubRes( typeof( CopperIngot ),		1044025, 75.0, 1044036, 1044268 );
            //AddSubRes( typeof( BronzeIngot ),		1044026, 80.0, 1044036, 1044268 );
            //AddSubRes( typeof( GoldIngot ),			1044027, 85.0, 1044036, 1044268 );
            //AddSubRes( typeof( AgapiteIngot ),		1044028, 90.0, 1044036, 1044268 );
            //AddSubRes( typeof( VeriteIngot ),		1044029, 95.0, 1044036, 1044268 );
            //AddSubRes( typeof( ValoriteIngot ),		1044030, 99.0, 1044036, 1044268 );


            AddSubRes(typeof(IronIngot), "Iron", 100.0, null);
            AddSubRes(typeof(RustyIngot), "Rusty", 100.0, null);
            AddSubRes(typeof(OldCopperIngot), "OldCopper", 100.0, null);
            AddSubRes(typeof(DullCopperIngot), "DullCopper", 100.0, null);
            AddSubRes(typeof(RubyIngot), "Ruby", 100.0, null);
            AddSubRes(typeof(CopperIngot), "Copper", 100.0, null);
            AddSubRes(typeof(BronzeIngot), "Bronze", 100.0, null);
            AddSubRes(typeof(ShadowIronIngot), "Shadow", 100.0, null);
            AddSubRes(typeof(SilverIngot), "Silver", 100.0, null);
            AddSubRes(typeof(MercuryIngot), "Mercury", 100.0, null);
            AddSubRes(typeof(RoseIngot), "Rose", 100.0, null);
            AddSubRes(typeof(GoldIngot), "Gold", 100.0, null);
            AddSubRes(typeof(AgapiteIngot), "Agapite", 100.0, null);
            AddSubRes(typeof(VeriteIngot), "Verite", 100.0, null);
            AddSubRes(typeof(PlutoniumIngot), "Plutonio", 100.0, null);
            AddSubRes(typeof(BloodRockIngot), "BloodRock", 100.0, null);
            AddSubRes(typeof(ValoriteIngot), "Valorite", 100.0, null);
            AddSubRes(typeof(BlackRockIngot), "BlackRock", 100.0, null);
            AddSubRes(typeof(MytherilIngot), "Mytheril", 100.0, null);
            AddSubRes(typeof(AquaIngot), "Aqua", 100.0, null);



			SetSubRes2( typeof( RedScales ), 1060875 );

			AddSubRes2( typeof( RedScales ),		1060875, 0.0, 1053137, 1044268 );
			AddSubRes2( typeof( YellowScales ),		1060876, 0.0, 1053137, 1044268 );
			AddSubRes2( typeof( BlackScales ),		1060877, 0.0, 1053137, 1044268 );
			AddSubRes2( typeof( GreenScales ),		1060878, 0.0, 1053137, 1044268 );
			AddSubRes2( typeof( WhiteScales ),		1060879, 0.0, 1053137, 1044268 );
			AddSubRes2( typeof( BlueScales ),		1060880, 0.0, 1053137, 1044268 );

			Resmelt = true;
			Repair = true;
			MarkOption = true;
            CanEnhance = false; // Core.AOS;
		}
	}

	public class ForgeAttribute : Attribute
	{
		public ForgeAttribute()
		{
		}
	}

	public class AnvilAttribute : Attribute
	{
		public AnvilAttribute()
		{
		}
	}
}