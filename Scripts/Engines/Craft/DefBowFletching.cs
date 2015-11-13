using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefBowFletching : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Fletching; }
		}

		public override int GumpTitleNumber
		{
			get { return 1044006; } // <CENTER>BOWCRAFT AND FLETCHING MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefBowFletching();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefBowFletching() : base( 3, 3, 1.20 )// base( 1, 2, 1.7 )
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

		public override void PlayCraftEffect( Mobile from )
		{
			// no animation
			//if ( from.Body.Type == BodyType.Human && !from.Mounted )
			//	from.Animate( 33, 5, 1, true, false, 0 );

			from.PlaySound( 0x55 );
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

		public override CraftECA ECA{ get{ return CraftECA.FiftyPercentChanceMinusTenPercent; } }

		public override void InitCraftList()
		{
			int index = -1;

			// Materials
			AddCraft( typeof( Kindling ), "Material", 1023553, 0.0, 00.0, typeof( Log ), 1044041, 1, 1044351 );

            index = AddCraft(typeof(Shaft), "Material", 1027124, 0.0, 40.0, typeof(Log), 1044041, 1, 1044351);
			SetUseAllRes( index, true );

			// Ammunition
			index = AddCraft( typeof( Arrow ), "Flechas e Dardos", 1023903, 0.0, 40.0, typeof( Shaft ), 1044560, 1, 1044561 );
			AddRes( index, typeof( Feather ), 1044562, 1, 1044563 );
			SetUseAllRes( index, true );

            index = AddCraft(typeof(Bolt), "Flechas e Dardos", 1027163, 0.0, 40.0, typeof(Shaft), 1044560, 1, 1044561);
			AddRes( index, typeof( Feather ), 1044562, 1, 1044563 );
			SetUseAllRes( index, true );

            //if( Core.SE )
            //{
            //    index = AddCraft( typeof( FukiyaDarts ), 1044565, 1030246, 50.0, 90.0, typeof( Log ), 1044041, 1, 1044351 );
            //    SetUseAllRes( index, true );
            //    SetNeededExpansion( index, Expansion.SE );
            //}

			// Weapons
			AddCraft( typeof( Bow ), "Arcos Normais", 1025042, 30.0, 70.0, typeof( Log ), 1044041, 7, 1044351 );
            AddCraft(typeof(Crossbow), "Arcos Normais", 1023919, 60.0, 100.0, typeof(Log), 1044041, 7, 1044351);
            AddCraft(typeof(HeavyCrossbow), "Arcos Normais", 1025117, 80.0, 120.0, typeof(Log), 1044041, 10, 1044351);

            AddCraft(typeof(BowIron), "Color Bow", "Bow Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowRusty), "Color Bow", "Bow Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowOldCopper), "Color Bow", "Bow Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowDullCopper), "Color Bow", "Bow Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowRuby), "Color Bow", "Color Bow Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowCopper), "Color Bow", "Bow Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowBronze), "Color Bow", "Bow Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowShadow), "Color Bow", "Bow Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowSilver), "Color Bow", "Bow Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowMercury), "Color Bow", "Bow Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowRose), "Color Bow", "Bow Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowGold), "Color Bow", "Bow Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowAgapite), "Color Bow", "Bow Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowVerite), "Color Bow", "Bow Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowPlutonio), "Color Bow", "Bow Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowBloodRock), "Color Bow", "Bow BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowValorite), "Color Bow", "Bow Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowBlackRock), "Color Bow", "Bow BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowMytheril), "Color Bow", "Bow Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(BowAqua), "Color Bow", "Bow Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);

            AddCraft(typeof(CrossbowIron), "Color Crossbow", "Crossbow Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowRusty), "Color Crossbow", "Crossbow Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowOldCopper), "Color Crossbow", "Crossbow Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowDullCopper), "Color Crossbow", "Crossbow Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowRuby), "Color Crossbow", "Crossbow Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowCopper), "Color Crossbow", "Crossbow Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowBronze), "Color Crossbow", "Crossbow Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowShadow), "Color Crossbow", "Crossbow Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowSilver), "Color Crossbow", "Crossbow Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowMercury), "Color Crossbow", "Crossbow Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowRose), "Color Crossbow", "Crossbow Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowGold), "Color Crossbow", "Crossbow Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowAgapite), "Color Crossbow", "Crossbow Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowVerite), "Color Crossbow", "Crossbow Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowPlutonio), "Color Crossbow", "Crossbow Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowBloodRock), "Color Crossbow", "Crossbow BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowValorite), "Color Crossbow", "Crossbow Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowBlackRock), "Color Crossbow", "Crossbow BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowMytheril), "Color Crossbow", "Crossbow Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(CrossbowAqua), "Color Crossbow", "Crossbow Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);

            AddCraft(typeof(HeavyCrossbowIron), "Color HeavyCrossbow", "HeavyCrossbow Iron", 45.0, 100.0, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowRusty), "Color HeavyCrossbow", "HeavyCrossbow Rusty", 45.0, 100.0, typeof(RustyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowOldCopper), "Color HeavyCrossbow", "HeavyCrossbow Old Copper", 50.0, 100.0, typeof(OldCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowDullCopper), "Color HeavyCrossbow", "HeavyCrossbow Dull Copper", 55.0, 100.0, typeof(DullCopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowRuby), "Color HeavyCrossbow", "HeavyCrossbow Ruby", 60.0, 100.0, typeof(RubyIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowCopper), "Color HeavyCrossbow", "HeavyCrossbow Copper", 65.0, 100.0, typeof(CopperIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowBronze), "Color HeavyCrossbow", "HeavyCrossbow Bronze", 70.0, 100.0, typeof(BronzeIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowShadow), "Color HeavyCrossbow", "HeavyCrossbow Shadow", 73.0, 100.0, typeof(ShadowIronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowSilver), "Color HeavyCrossbow", "HeavyCrossbow Silver", 75.0, 100.0, typeof(SilverIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowMercury), "Color HeavyCrossbow", "HeavyCrossbow Mercury", 78.0, 100.0, typeof(MercuryIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowRose), "Color HeavyCrossbow", "HeavyCrossbow Rose", 80.0, 100.0, typeof(RoseIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowGold), "Color HeavyCrossbow", "HeavyCrossbow Gold", 84.0, 120.0, typeof(GoldIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowAgapite), "Color HeavyCrossbow", "HeavyCrossbow Agapite", 86.0, 120.0, typeof(AgapiteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowVerite), "Color HeavyCrossbow", "HeavyCrossbow Verite", 88.0, 120.0, typeof(VeriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowPlutonio), "Color HeavyCrossbow", "HeavyCrossbow Plutonio", 90.0, 120.0, typeof(PlutoniumIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowBloodRock), "Color HeavyCrossbow", "HeavyCrossbow BloodRock", 92.0, 120.0, typeof(BloodRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowValorite), "Color HeavyCrossbow", "HeavyCrossbow Valorite", 95.0, 120.0, typeof(ValoriteIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowBlackRock), "Color HeavyCrossbow", "HeavyCrossbow BlackRock", 93.0, 120.0, typeof(BlackRockIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowMytheril), "Color HeavyCrossbow", "HeavyCrossbow Mytheril", 95.0, 120.0, typeof(MytherilIngot), 1044036, 14, 1044037);
            AddCraft(typeof(HeavyCrossbowAqua), "Color HeavyCrossbow", "HeavyCrossbow Aqua", 100.0, 120.0, typeof(AquaIngot), 1044036, 14, 1044037);


            index = AddCraft(typeof(RayBow), "Arcos Especiais", "Ray Bow", 86.0, 100.0, typeof(FineLog), "Fine Log", 30, 1044351);
            AddRes(index, typeof(BronzeIngot), "Bronze Ingot", 30, 1044037);
            AddRes(index, typeof(LightningScroll), "Lightning Scroll", 30, 1044037);
            AddSkill(index, SkillName.Magery, 70, 100);

            index = AddCraft(typeof(PoisonBow), "Arcos Especiais", "Poison Bow", 92.0, 100.0, typeof(PoisonLog), "Poison Log", 30, 1044351);
            AddRes(index, typeof(SilverIngot), "Silver Ingot", 30, 1044037);
            AddRes(index, typeof(PoisonScroll), "Poison Scroll", 30, 1044037);
            AddSkill(index, SkillName.Magery, 70, 100);
            AddSkill(index, SkillName.Poisoning, 80, 100);

            index = AddCraft(typeof(AdvancedPoisonBow), "Arcos Especiais", "Advanced Poison Bow", 96.0, 100.0, typeof(PoisonLog), "Poison Log", 30, 1044351);
            AddRes(index, typeof(VeriteIngot), "Verite Ingot", 30, 1044037);
            AddRes(index, typeof(PoisonScroll), "Poison Scroll", 100, 1044037);
            AddSkill(index, SkillName.Magery, 70, 100);
            AddSkill(index, SkillName.Poisoning, 100, 100);

            index = AddCraft(typeof(FireBow), "Arcos Especiais", "Fire Bow", 105.0, 120.0, typeof(FireLog), "Fire Log", 30, 1044351);
            AddRes(index, typeof(ValoriteIngot), "Valorite Ingot", 30, 1044037);
            AddRes(index, typeof(FlamestrikeScroll), "Flamestrike Scroll", 30, 1044037);
            AddRes(index, typeof(Torch), "Torch", 30, 1044037);
            AddSkill(index, SkillName.Magery, 100, 100);

            //index = AddCraft(typeof(ElvenBow), "Arcos Especiais", "Elven Bow", 99.5, 100.0, typeof(FineLog), "Fine Log", 30, 1044351);
            //AddRes(index, typeof(ValoriteIngot), "Valorite Ingot", 30, 1044037);
            //AddRes(index, typeof(FlamestrikeScroll), "Flamestrike Scroll", 30, 1044037);
            //AddRes(index, typeof(Torch), "Torch", 30, 1044037);
            //AddSkill(index, SkillName.Magery, 50, 100);

            //if ( Core.AOS )
            //{
            //    AddCraft( typeof( CompositeBow ), 1044566, 1029922, 70.0, 110.0, typeof( Log ), 1044041, 7, 1044351 );
            //    AddCraft( typeof( RepeatingCrossbow ), 1044566, 1029923, 90.0, 130.0, typeof( Log ), 1044041, 10, 1044351 );
            //}

            //if( Core.SE )
            //{
            //    index = AddCraft( typeof( Yumi ), 1044566, 1030224, 90.0, 130.0, typeof( Log ), 1044041, 10, 1044351 );
            //    SetNeededExpansion( index, Expansion.SE );
            //}

			MarkOption = true;
			Repair = Core.AOS;
		}
	}
}