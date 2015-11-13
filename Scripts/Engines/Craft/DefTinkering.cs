using System;
using Server;
using Server.Items;
using Server.Factions;
using Server.Targeting;

namespace Server.Engines.Craft
{
	public class DefTinkering : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Tinkering; }
		}

		public override int GumpTitleNumber
		{
			get { return 1044007; } // <CENTER>TINKERING MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefTinkering();

				return m_CraftSystem;
			}
		}

        private DefTinkering()
            : base(3, 3, 1.20)// base( 1, 1, 3.0 )
		{
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			if ( item.NameNumber == 1044258 || item.NameNumber == 1046445 ) // potion keg and faction trap removal kit
				return 0.5; // 50%

			return 0.0; // 0%
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.
			else if ( itemType != null && ( itemType.IsSubclassOf( typeof( BaseFactionTrapDeed ) ) || itemType == typeof( FactionTrapRemovalKit ) ) && Faction.Find( from ) == null )
				return 1044573; // You have to be in a faction to do that.

			return 0;
		}

		private static Type[] m_TinkerColorables = new Type[]
			{
				typeof( ForkLeft ), typeof( ForkRight ),
				typeof( SpoonLeft ), typeof( SpoonRight ),
				typeof( KnifeLeft ), typeof( KnifeRight ),
				typeof( Plate ),
				typeof( Goblet ), typeof( PewterMug ),
				typeof( KeyRing ),
				typeof( Candelabra ), typeof( Scales ),
				typeof( Key ), typeof( Globe ),
				typeof( Spyglass ), typeof( Lantern ),
				typeof( HeatingStand )
			};

		public override bool RetainsColorFrom( CraftItem item, Type type )
		{
			if ( !type.IsSubclassOf( typeof( BaseIngot ) ) )
				return false;

			type = item.ItemType;

			bool contains = false;

			for ( int i = 0; !contains && i < m_TinkerColorables.Length; ++i )
				contains = ( m_TinkerColorables[i] == type );

			return contains;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			// no sound
			//from.PlaySound( 0x241 );
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

		public override bool ConsumeOnFailure( Mobile from, Type resourceType, CraftItem craftItem )
		{
			if ( resourceType == typeof( Silver ) )
				return false;

			return base.ConsumeOnFailure( from, resourceType, craftItem );
		}

		public void AddJewelrySet( GemType gemType, Type itemType )
		{
			int offset = (int)gemType - 1;

			int index = AddCraft( typeof( GoldRing ), 1044049, 1044176 + offset, 40.0, 90.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );

			index = AddCraft( typeof( SilverBeadNecklace ), 1044049, 1044185 + offset, 40.0, 90.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );

			index = AddCraft( typeof( GoldNecklace ), 1044049, 1044194 + offset, 40.0, 90.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );

			index = AddCraft( typeof( GoldEarrings ), 1044049, 1044203 + offset, 40.0, 90.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );

			index = AddCraft( typeof( GoldBeadNecklace ), 1044049, 1044212 + offset, 40.0, 90.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );

			index = AddCraft( typeof( GoldBracelet ), 1044049, 1044221 + offset, 40.0, 90.0, typeof( IronIngot ), 1044036, 2, 1044037 );
			AddRes( index, itemType, 1044231 + offset, 1, 1044240 );
		}

		public override void InitCraftList()
		{
			int index = -1;

			#region Wooden Items
			AddCraft( typeof( JointingPlane ), "Itens Madeira", 1024144, 0.0, 50.0, typeof( Log ), 1044041, 4, 1044351 );
            AddCraft(typeof(MouldingPlane), "Itens Madeira", 1024140, 0.0, 50.0, typeof(Log), 1044041, 4, 1044351);
            AddCraft(typeof(SmoothingPlane), "Itens Madeira", 1024146, 0.0, 50.0, typeof(Log), 1044041, 4, 1044351);
            AddCraft(typeof(ClockFrame), "Itens Madeira", 1024173, 0.0, 50.0, typeof(Log), 1044041, 6, 1044351);
            AddCraft(typeof(Axle), "Itens Madeira", 1024187, -25.0, 25.0, typeof(Log), 1044041, 2, 1044351);
            AddCraft(typeof(RollingPin), "Itens Madeira", 1024163, 0.0, 50.0, typeof(Log), 1044041, 5, 1044351);

            //if( Core.SE )
            //{
            //    index = AddCraft( typeof( Nunchaku ), 1044042, 1030158, 70.0, 120.0, typeof( IronIngot ), 1044036, 3, 1044037 );
            //    AddRes( index, typeof( Log ), 1044041, 8, 1044351 );
            //    SetNeededExpansion( index, Expansion.SE );
            //}
			#endregion

			#region Tools
			AddCraft( typeof( Scissors ), "Ferramentas", 1023998, 5.0, 55.0, typeof( IronIngot ), 1044036, 2, 1044037 );
            AddCraft(typeof(MortarPestle), "Ferramentas", 1023739, 20.0, 70.0, typeof(IronIngot), 1044036, 3, 1044037);
            AddCraft(typeof(Scorp), "Ferramentas", 1024327, 30.0, 80.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(TinkerTools), "Ferramentas", 1044164, 10.0, 60.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(Hatchet), "Ferramentas", 1023907, 30.0, 80.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(DrawKnife), "Ferramentas", 1024324, 30.0, 80.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(SewingKit), "Ferramentas", 1023997, 10.0, 70.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(Saw), "Ferramentas", 1024148, 30.0, 80.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(DovetailSaw), "Ferramentas", 1024136, 30.0, 80.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(Froe), "Ferramentas", 1024325, 30.0, 80.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(Shovel), "Ferramentas", 1023898, 40.0, 90.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(Hammer), "Ferramentas", 1024138, 30.0, 80.0, typeof(IronIngot), 1044036, 1, 1044037);
            AddCraft(typeof(Tongs), "Ferramentas", 1024028, 35.0, 85.0, typeof(IronIngot), 1044036, 1, 1044037);
            AddCraft(typeof(SmithHammer), "Ferramentas", 1025091, 40.0, 90.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(SledgeHammer), "Ferramentas", 1024021, 40.0, 90.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(Inshave), "Ferramentas", 1024326, 30.0, 80.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(Pickaxe), "Ferramentas", 1023718, 40.0, 90.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(Lockpick), "Ferramentas", 1025371, 45.0, 95.0, typeof(IronIngot), 1044036, 1, 1044037);
            AddCraft(typeof(Skillet), "Ferramentas", 1044567, 30.0, 80.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(FlourSifter), "Ferramentas", 1024158, 50.0, 100.0, typeof(IronIngot), 1044036, 3, 1044037);
            AddCraft(typeof(FletcherTools), "Ferramentas", 1044166, 35.0, 85.0, typeof(IronIngot), 1044036, 3, 1044037);
            AddCraft(typeof(MapmakersPen), "Ferramentas", 1044167, 25.0, 75.0, typeof(IronIngot), 1044036, 1, 1044037);
            AddCraft(typeof(ScribesPen), "Ferramentas", 1044168, 25.0, 75.0, typeof(IronIngot), 1044036, 1, 1044037);
			#endregion

			#region Parts
			AddCraft( typeof( Gears ), "Pecas", 1024179, 5.0, 55.0, typeof( IronIngot ), 1044036, 2, 1044037 );
            AddCraft(typeof(ClockParts), "Pecas", 1024175, 25.0, 75.0, typeof(IronIngot), 1044036, 1, 1044037);
            AddCraft(typeof(BarrelTap), "Pecas", 1024100, 35.0, 85.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(Springs), "Pecas", 1024189, 5.0, 55.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(SextantParts), "Pecas", 1024185, 30.0, 80.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(BarrelHoops), "Pecas", 1024321, -15.0, 35.0, typeof(IronIngot), 1044036, 5, 1044037);
            AddCraft(typeof(Hinge), "Pecas", 1024181, 5.0, 55.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(BolaBall), "Pecas", 1023699, 45.0, 95.0, typeof(IronIngot), 1044036, 10, 1044037);
			
            //if ( Core.ML )
            //{
            //    index = AddCraft( typeof( JeweledFiligree ), 1044047, 1072894, 70.0, 110.0, typeof( IronIngot ), 1044036, 2, 1044037 );
            //    AddRes( index, typeof( StarSapphire ), 1044231, 1, 1044253 );
            //    AddRes( index, typeof( Ruby ), 1044234, 1, 1044253 );
            //    SetNeededExpansion( index, Expansion.ML );
            //}
			
			#endregion

			#region Utensils
			AddCraft( typeof( ButcherKnife ), "Utensilios", 1025110, 25.0, 75.0, typeof( IronIngot ), 1044036, 2, 1044037 );
            AddCraft(typeof(SpoonLeft), "Utensilios", 1044158, 0.0, 50.0, typeof(IronIngot), 1044036, 1, 1044037);
            AddCraft(typeof(SpoonRight), "Utensilios", 1044159, 0.0, 50.0, typeof(IronIngot), 1044036, 1, 1044037);
            AddCraft(typeof(Plate), "Utensilios", 1022519, 0.0, 50.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(ForkLeft), "Utensilios", 1044160, 0.0, 50.0, typeof(IronIngot), 1044036, 1, 1044037);
            AddCraft(typeof(ForkRight), "Utensilios", 1044161, 0.0, 50.0, typeof(IronIngot), 1044036, 1, 1044037);
            AddCraft(typeof(Cleaver), "Utensilios", 1023778, 20.0, 70.0, typeof(IronIngot), 1044036, 3, 1044037);
            AddCraft(typeof(KnifeLeft), "Utensilios", 1044162, 0.0, 50.0, typeof(IronIngot), 1044036, 1, 1044037);
            AddCraft(typeof(KnifeRight), "Utensilios", 1044163, 0.0, 50.0, typeof(IronIngot), 1044036, 1, 1044037);
            AddCraft(typeof(Goblet), "Utensilios", 1022458, 10.0, 60.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(PewterMug), "Utensilios", 1024097, 10.0, 60.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(SkinningKnife), "Utensilios", 1023781, 25.0, 75.0, typeof(IronIngot), 1044036, 2, 1044037);
			#endregion

			#region Misc
			AddCraft( typeof( KeyRing ), "Variados", 1024113, 10.0, 60.0, typeof( IronIngot ), 1044036, 2, 1044037 );
            AddCraft(typeof(Candelabra), "Variados", 1022599, 55.0, 105.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(Scales), "Variados", 1026225, 60.0, 110.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(Key), "Variados", 1024112, 20.0, 70.0, typeof(IronIngot), 1044036, 3, 1044037);
            AddCraft(typeof(Globe), "Variados", 1024167, 55.0, 105.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(Spyglass), "Variados", 1025365, 60.0, 110.0, typeof(IronIngot), 1044036, 4, 1044037);
            AddCraft(typeof(Lantern), "Variados", 1022597, 30.0, 80.0, typeof(IronIngot), 1044036, 2, 1044037);
            AddCraft(typeof(HeatingStand), "Variados", 1026217, 60.0, 110.0, typeof(IronIngot), 1044036, 4, 1044037);

            //if ( Core.SE )
            //{
            //    index = AddCraft( typeof( ShojiLantern ), 1044050, 1029404, 65.0, 115.0, typeof( IronIngot ), 1044036, 10, 1044037 );
            //    AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
            //    SetNeededExpansion( index, Expansion.SE );

            //    index = AddCraft( typeof( PaperLantern ), 1044050, 1029406, 65.0, 115.0, typeof( IronIngot ), 1044036, 10, 1044037 );
            //    AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
            //    SetNeededExpansion( index, Expansion.SE );

            //    index = AddCraft( typeof( RoundPaperLantern ), 1044050, 1029418, 65.0, 115.0, typeof( IronIngot ), 1044036, 10, 1044037 );
            //    AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
            //    SetNeededExpansion( index, Expansion.SE );

            //    index = AddCraft( typeof( WindChimes ), 1044050, 1030290, 80.0, 130.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );

            //    index = AddCraft( typeof( FancyWindChimes ), 1044050, 1030291, 80.0, 130.0, typeof( IronIngot ), 1044036, 15, 1044037 );
            //    SetNeededExpansion( index, Expansion.SE );

            //}
			#endregion

			#region Jewelry
			AddJewelrySet( GemType.StarSapphire, typeof( StarSapphire ) );
			AddJewelrySet( GemType.Emerald, typeof( Emerald ) );
			AddJewelrySet( GemType.Sapphire, typeof( Sapphire ) );
			AddJewelrySet( GemType.Ruby, typeof( Ruby ) );
			AddJewelrySet( GemType.Citrine, typeof( Citrine ) );
			AddJewelrySet( GemType.Amethyst, typeof( Amethyst ) );
			AddJewelrySet( GemType.Tourmaline, typeof( Tourmaline ) );
			AddJewelrySet( GemType.Amber, typeof( Amber ) );
			AddJewelrySet( GemType.Diamond, typeof( Diamond ) );
			#endregion

			#region Multi-Component Items
			index = AddCraft( typeof( AxleGears ), 1044051, 1024177, 0.0, 0.0, typeof( Axle ), 1044169, 1, 1044253 );
			AddRes( index, typeof( Gears ), 1044254, 1, 1044253 );

			index = AddCraft( typeof( ClockParts ), 1044051, 1024175, 0.0, 0.0, typeof( AxleGears ), 1044170, 1, 1044253 );
			AddRes( index, typeof( Springs ), 1044171, 1, 1044253 );

			index = AddCraft( typeof( SextantParts ), 1044051, 1024185, 0.0, 0.0, typeof( AxleGears ), 1044170, 1, 1044253 );
			AddRes( index, typeof( Hinge ), 1044172, 1, 1044253 );

			index = AddCraft( typeof( ClockRight ), 1044051, 1044257, 0.0, 0.0, typeof( ClockFrame ), 1044174, 1, 1044253 );
			AddRes( index, typeof( ClockParts ), 1044173, 1, 1044253 );

			index = AddCraft( typeof( ClockLeft ), 1044051, 1044256, 0.0, 0.0, typeof( ClockFrame ), 1044174, 1, 1044253 );
			AddRes( index, typeof( ClockParts ), 1044173, 1, 1044253 );

			AddCraft( typeof( Sextant ), 1044051, 1024183, 0.0, 0.0, typeof( SextantParts ), 1044175, 1, 1044253 );

			index = AddCraft( typeof( Bola ), 1044051, 1046441, 60.0, 80.0, typeof( BolaBall ), 1046440, 4, 1042613 );
			AddRes( index, typeof( Leather ), 1044462, 3, 1044463 );

			index = AddCraft( typeof( PotionKeg ), 1044051, 1044258, 75.0, 100.0, typeof( Keg ), 1044255, 1, 1044253 );
			AddRes( index, typeof( Bottle ), 1044250, 10, 1044253 );
			AddRes( index, typeof( BarrelLid ), 1044251, 1, 1044253 );
			AddRes( index, typeof( BarrelTap ), 1044252, 1, 1044253 );
			
			
			#endregion

			#region Traps
			// Dart Trap
			index = AddCraft( typeof( DartTrapCraft ), "Armadilhas", 1024396, 30.0, 80.0, typeof( IronIngot ), 1044036, 1, 1044037 );
			AddRes( index, typeof( Bolt ), 1044570, 1, 1044253 );

			// Poison Trap
            index = AddCraft(typeof(PoisonTrapCraft), "Armadilhas", 1044593, 30.0, 80.0, typeof(IronIngot), 1044036, 1, 1044037);
			AddRes( index, typeof( BasePoisonPotion ), 1044571, 1, 1044253 );

			// Explosion Trap
            index = AddCraft(typeof(ExplosionTrapCraft), "Armadilhas", 1044597, 55.0, 105.0, typeof(IronIngot), 1044036, 1, 1044037);
			AddRes( index, typeof( BaseExplosionPotion ), 1044569, 1, 1044253 );

            //// Faction Gas Trap
            //index = AddCraft(typeof(FactionGasTrapDeed), "Armadilhas", 1044598, 65.0, 115.0, typeof(Silver), 1044572, Core.AOS ? 250 : 1000, 1044253);
            //AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );
            //AddRes( index, typeof( BasePoisonPotion ), 1044571, 1, 1044253 );

            //// Faction explosion Trap
            //index = AddCraft(typeof(FactionExplosionTrapDeed), "Armadilhas", 1044599, 65.0, 115.0, typeof(Silver), 1044572, Core.AOS ? 250 : 1000, 1044253);
            //AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );
            //AddRes( index, typeof( BaseExplosionPotion ), 1044569, 1, 1044253 );

            //// Faction Saw Trap
            //index = AddCraft(typeof(FactionSawTrapDeed), "Armadilhas", 1044600, 65.0, 115.0, typeof(Silver), 1044572, Core.AOS ? 250 : 1000, 1044253);
            //AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );
            //AddRes( index, typeof( Gears ), 1044254, 1, 1044253 );

            //// Faction Spike Trap			
            //index = AddCraft(typeof(FactionSpikeTrapDeed), "Armadilhas", 1044601, 65.0, 115.0, typeof(Silver), 1044572, Core.AOS ? 250 : 1000, 1044253);
            //AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );
            //AddRes( index, typeof( Springs ), 1044171, 1, 1044253 );

            //// Faction trap removal kit
            //index = AddCraft(typeof(FactionTrapRemovalKit), "Armadilhas", 1046445, 90.0, 115.0, typeof(Silver), 1044572, 500, 1044253);
            //AddRes( index, typeof( IronIngot ), 1044036, 10, 1044037 );
			#endregion

            // 1011172 - Jewelry
            // 502778 - That's not the proper material.

            index = AddCraft(typeof(BraceletAlchemy), "Joias Magicas", "Bracelet Of Alchemy", 60.0, 100.0, typeof(GoldIngot), 1027145, 10, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Magery, 68, 100);

            index = AddCraft(typeof(BraceletBlacksmithing), "Joias Magicas", "Bracelet Of Blacksmithing", 100.0, 100.0, typeof(GoldIngot), 1027145, 10, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Blacksmith, 100, 100);
            AddSkill(index, SkillName.Magery, 78, 100);
            AddSkill(index, SkillName.ArmsLore, 100, 100);

            index = AddCraft(typeof(BraceletMining), "Joias Magicas", "Bracelet Of Mining", 99.0, 100.0, typeof(GoldIngot), 1027145, 10, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Mining, 99, 100);
            AddSkill(index, SkillName.Magery, 58, 100);

            index = AddCraft(typeof(BraceletBowcraft), "Joias Magicas", "Bracelet Of Bowcraft", 91.0, 100.0, typeof(GoldIngot), 1027145, 10, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Fletching, 50, 100);
            AddSkill(index, SkillName.Magery, 77, 100);

            index = AddCraft(typeof(BraceletTaming), "Joias Magicas", "Bracelet Of Taming", 76.0, 100.0, typeof(GoldIngot), 1027145, 10, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.AnimalLore, 100, 100);
            AddSkill(index, SkillName.Magery, 64, 100);

            index = AddCraft(typeof(BraceletLumberjacking), "Joias Magicas", "Bracelet Of Lumberjacking", 72.0, 100.0, typeof(GoldIngot), 1027145, 10, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Lumberjacking, 50, 100);
            AddSkill(index, SkillName.Magery, 56, 100);

            index = AddCraft(typeof(BraceletSnooping), "Joias Magicas", "Bracelet Of Snooping", 71.0, 100.0, typeof(GoldIngot), 1027145, 10, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Snooping, 50, 100);
            AddSkill(index, SkillName.Magery, 61, 100);

            index = AddCraft(typeof(BraceletTailoring), "Joias Magicas", "Bracelet Of Tailoring", 89.0, 100.0, typeof(GoldIngot), 1027145, 10, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Tailoring, 50, 100);
            AddSkill(index, SkillName.Magery, 76, 100);

            index = AddCraft(typeof(BraceletPoisoning), "Joias Magicas", "Bracelet Of Poisoning", 68.0, 100.0, typeof(GoldIngot), 1027145, 10, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Poisoning, 50, 100);
            AddSkill(index, SkillName.Magery, 57, 100);

            index = AddCraft(typeof(BraceletStealing), "Joias Magicas", "Bracelet Of Stealing", 72.0, 100.0, typeof(GoldIngot), 1027145, 10, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Stealing, 50, 100);
            AddSkill(index, SkillName.Magery, 62, 100);


            index = AddCraft(typeof(RingDexterity), "Joias Magicas", "Ring Of Dexterity", 77.0, 100.0, typeof(GoldIngot), 1027145, 5, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Magery, 65, 100);

            index = AddCraft(typeof(RingStrenght), "Joias Magicas", "Ring Of Strenght", 78.0, 100.0, typeof(GoldIngot), 1027145, 5, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Magery, 66, 100);

            index = AddCraft(typeof(RingInteligence), "Joias Magicas", "Ring Of Inteligence", 79.0, 100.0, typeof(GoldIngot), 1027145, 5, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Magery, 67, 100);

            index = AddCraft(typeof(RingPower), "Joias Magicas", "Ring Of Power", 100.0, 100.0, typeof(GoldIngot), 1027145, 5, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Magery, 80, 100);


            index = AddCraft(typeof(NecklaceFencing), "Joias Magicas", "Necklace Of Fencing", 81.0, 100.0, typeof(GoldIngot), 1027145, 10, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Magery, 69, 100);
            AddSkill(index, SkillName.Fencing, 50, 100);

            index = AddCraft(typeof(NecklaceMacefighting), "Joias Magicas", "Necklace Of Macefighting", 82.0, 100.0, typeof(GoldIngot), 1027145, 10, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Magery, 70, 100);
            AddSkill(index, SkillName.Macing, 50, 100);

            index = AddCraft(typeof(NecklaceSwordsmanship), "Joias Magicas", "Necklace Of Swordsmanship", 83.0, 100.0, typeof(GoldIngot), 1027145, 10, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Magery, 71, 100);
            AddSkill(index, SkillName.Swords, 50, 100);

            index = AddCraft(typeof(NecklaceArchery), "Joias Magicas", "Necklace Of Archery", 84.0, 100.0, typeof(GoldIngot), 1027145, 10, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Magery, 72, 100);
            AddSkill(index, SkillName.Archery, 50, 100);

            index = AddCraft(typeof(NecklaceTactics), "Joias Magicas", "Necklace Of Tactics", 85.0, 100.0, typeof(GoldIngot), 1027145, 10, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Magery, 73, 100);
            AddSkill(index, SkillName.Tactics, 50, 100);

            index = AddCraft(typeof(NecklaceWrestling), "Joias Magicas", "Necklace Of Wrestling", 86.0, 100.0, typeof(GoldIngot), 1027145, 10, 1044037);
            AddRes(index, typeof(Diamond), 1011172, 2, 502778);
            AddRes(index, typeof(Ruby), 1011172, 2, 502778);
            AddRes(index, typeof(Sapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Citrine), 1011172, 2, 502778);
            AddRes(index, typeof(Amber), 1011172, 2, 502778);
            AddRes(index, typeof(StarSapphire), 1011172, 2, 502778);
            AddRes(index, typeof(Amethyst), 1011172, 2, 502778);
            AddRes(index, typeof(Tourmaline), 1011172, 2, 502778);
            AddSkill(index, SkillName.Magery, 74, 100);
            AddSkill(index, SkillName.Wrestling, 50, 100);



			// Set the overridable material
			SetSubRes( typeof( IronIngot ), 1044022 );


            //SetSubRes(typeof(Diamond), 1011172);
            //SetSubRes(typeof(Ruby), 1011172);
            //SetSubRes(typeof(Sapphire), 1011172);
            //SetSubRes(typeof(Citrine), 1011172);
            //SetSubRes(typeof(Amber), 1011172);
            //SetSubRes(typeof(StarSapphire), 1011172);
            //SetSubRes(typeof(Amethyst), 1011172);
            //SetSubRes(typeof(Tourmaline), 1011172);

			// Add every material you want the player to be able to choose from
			// This will override the overridable material
			AddSubRes( typeof( IronIngot ),			1044022, 00.0, 1044036, 1044267 );
			AddSubRes( typeof( DullCopperIngot ),	1044023, 65.0, 1044036, 1044268 );
			AddSubRes( typeof( ShadowIronIngot ),	1044024, 70.0, 1044036, 1044268 );
			AddSubRes( typeof( CopperIngot ),		1044025, 75.0, 1044036, 1044268 );
			AddSubRes( typeof( BronzeIngot ),		1044026, 80.0, 1044036, 1044268 );
			AddSubRes( typeof( GoldIngot ),			1044027, 85.0, 1044036, 1044268 );
			AddSubRes( typeof( AgapiteIngot ),		1044028, 90.0, 1044036, 1044268 );
			AddSubRes( typeof( VeriteIngot ),		1044029, 95.0, 1044036, 1044268 );
			AddSubRes( typeof( ValoriteIngot ),		1044030, 99.0, 1044036, 1044268 );

			MarkOption = true;
			Repair = true;
            CanEnhance = false;// Core.AOS;
		}
	}

	public abstract class TrapCraft : CustomCraft
	{
		private LockableContainer m_Container;

		public LockableContainer Container{ get{ return m_Container; } }

		public abstract TrapType TrapType{ get; }

		public TrapCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality ) : base( from, craftItem, craftSystem, typeRes, tool, quality )
		{
		}

		private int Verify( LockableContainer container )
		{
			if ( container == null || container.KeyValue == 0 )
				return 1005638; // You can only trap lockable chests.
			if ( From.Map != container.Map || !From.InRange( container.GetWorldLocation(), 2 ) )
				return 500446; // That is too far away.
			if ( !container.Movable )
				return 502944; // You cannot trap this item because it is locked down.
			if ( !container.IsAccessibleTo( From ) )
				return 502946; // That belongs to someone else.
			if ( container.Locked )
				return 502943; // You can only trap an unlocked object.
			if ( container.TrapType != TrapType.None )
				return 502945; // You can only place one trap on an object at a time.

			return 0;
		}

		private bool Acquire( object target, out int message )
		{
			LockableContainer container = target as LockableContainer;

			message = Verify( container );

			if ( message > 0 )
			{
				return false;
			}
			else
			{
				m_Container = container;
				return true;
			}
		}

		public override void EndCraftAction()
		{
			From.SendLocalizedMessage( 502921 ); // What would you like to set a trap on?
			From.Target = new ContainerTarget( this );
		}

		private class ContainerTarget : Target
		{
			private TrapCraft m_TrapCraft;

			public ContainerTarget( TrapCraft trapCraft ) : base( -1, false, TargetFlags.None )
			{
				m_TrapCraft = trapCraft;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				int message;

				if ( m_TrapCraft.Acquire( targeted, out message ) )
					m_TrapCraft.CraftItem.CompleteCraft( m_TrapCraft.Quality, false, m_TrapCraft.From, m_TrapCraft.CraftSystem, m_TrapCraft.TypeRes, m_TrapCraft.Tool, m_TrapCraft );
				else
					Failure( message );
			}

			protected override void OnTargetCancel( Mobile from, TargetCancelType cancelType )
			{
				if ( cancelType == TargetCancelType.Canceled )
					Failure( 0 );
			}

			private void Failure( int message )
			{
				Mobile from = m_TrapCraft.From;
				BaseTool tool = m_TrapCraft.Tool;

				if ( tool != null && !tool.Deleted && tool.UsesRemaining > 0 )
					from.SendGump( new CraftGump( from, m_TrapCraft.CraftSystem, tool, message ) );
				else if ( message > 0 )
					from.SendLocalizedMessage( message );
			}
		}

		public override Item CompleteCraft( out int message )
		{
			message = Verify( this.Container );

			if ( message == 0 )
			{
				int trapLevel = (int)(From.Skills.Tinkering.Value / 10);

				Container.TrapType = this.TrapType;
				Container.TrapPower = trapLevel * 9;
				Container.TrapLevel = trapLevel;
				Container.TrapOnLockpick = true;

				message = 1005639; // Trap is disabled until you lock the chest.
			}

			return null;
		}
	}

	[CraftItemID( 0x1BFC )]
	public class DartTrapCraft : TrapCraft
	{
		public override TrapType TrapType{ get{ return TrapType.DartTrap; } }

		public DartTrapCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality ) : base( from, craftItem, craftSystem, typeRes, tool, quality )
		{
		}
	}

	[CraftItemID( 0x113E )]
	public class PoisonTrapCraft : TrapCraft
	{
		public override TrapType TrapType{ get{ return TrapType.PoisonTrap; } }

		public PoisonTrapCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality ) : base( from, craftItem, craftSystem, typeRes, tool, quality )
		{
		}
	}

	[CraftItemID( 0x370C )]
	public class ExplosionTrapCraft : TrapCraft
	{
		public override TrapType TrapType{ get{ return TrapType.ExplosionTrap; } }

		public ExplosionTrapCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality ) : base( from, craftItem, craftSystem, typeRes, tool, quality )
		{
		}
	}
}