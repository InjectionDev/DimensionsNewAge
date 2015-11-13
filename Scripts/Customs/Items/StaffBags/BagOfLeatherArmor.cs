using System; 
using Server; 
using Server.Items;
using System.Collections.Generic;

namespace Server.Items 
{ 
	public class BagOfLeatherArmor : Bag 
	{ 
		[Constructable] 
		public BagOfLeatherArmor() : this( 1 ) 
		{ 
		} 

		[Constructable] 
		public BagOfLeatherArmor( int amount ) 
		{
            this.Name = "Bag Of Leather Armor";
            this.Hue = DimensionsNewAge.Scripts.HueItemConst.HueMagicColorRandom;

            Bag bagLeatherCyclop = new Bag();
            bagLeatherCyclop.DropItem(new StuddedChestCyclop());
            bagLeatherCyclop.DropItem(new StuddedArmsCyclop());
            bagLeatherCyclop.DropItem(new LeatherCapCyclop());
            bagLeatherCyclop.DropItem(new StuddedGlovesCyclop());
            bagLeatherCyclop.DropItem(new StuddedGorgetCyclop());
            bagLeatherCyclop.DropItem(new StuddedLegsCyclop());
            bagLeatherCyclop.DropItem(new CyclopsHides(5000));
            bagLeatherCyclop.DropItem(new CyclopsLeather(5000));
            bagLeatherCyclop.Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideCyclops;
            this.DropItem(bagLeatherCyclop);

            Bag bagLeatherDaemon = new Bag();
            bagLeatherDaemon.DropItem(new StuddedChestDaemon());
            bagLeatherDaemon.DropItem(new StuddedArmsDaemon());
            bagLeatherDaemon.DropItem(new LeatherCapDaemon());
            bagLeatherDaemon.DropItem(new StuddedGlovesDaemon());
            bagLeatherDaemon.DropItem(new StuddedGorgetDaemon());
            bagLeatherDaemon.DropItem(new StuddedLegsDaemon());
            bagLeatherDaemon.DropItem(new DaemonHides(5000));
            bagLeatherDaemon.DropItem(new DaemonLeather(5000));
            bagLeatherDaemon.Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideDaemon;
            this.DropItem(bagLeatherDaemon);

            Bag bagLeatherDragon = new Bag();
            bagLeatherDragon.DropItem(new StuddedChestDragon());
            bagLeatherDragon.DropItem(new StuddedArmsDragon());
            bagLeatherDragon.DropItem(new LeatherCapDragon());
            bagLeatherDragon.DropItem(new StuddedGlovesDragon());
            bagLeatherDragon.DropItem(new StuddedGorgetDragon());
            bagLeatherDragon.DropItem(new StuddedLegsDragon());
            bagLeatherDragon.DropItem(new DragonHides(5000));
            bagLeatherDragon.DropItem(new DragonLeather(5000));
            bagLeatherDragon.Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideDragon;
            this.DropItem(bagLeatherDragon);

            Bag bagLeatherDragonGreen = new Bag();
            bagLeatherDragonGreen.DropItem(new StuddedChestDragonGreen());
            bagLeatherDragonGreen.DropItem(new StuddedArmsDragonGreen());
            bagLeatherDragonGreen.DropItem(new LeatherCapDragonGreen());
            bagLeatherDragonGreen.DropItem(new StuddedGlovesDragonGreen());
            bagLeatherDragonGreen.DropItem(new StuddedGorgetDragonGreen());
            bagLeatherDragonGreen.DropItem(new StuddedLegsDragonGreen());
            bagLeatherDragonGreen.DropItem(new DragonGreenHides(5000));
            bagLeatherDragonGreen.DropItem(new DragonGreenLeather(5000));
            bagLeatherDragonGreen.Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideDragonGreen;
            this.DropItem(bagLeatherDragonGreen);

            Bag bagLeatherGargoyle = new Bag();
            bagLeatherGargoyle.DropItem(new StuddedChestGargoyle());
            bagLeatherGargoyle.DropItem(new StuddedArmsGargoyle());
            bagLeatherGargoyle.DropItem(new LeatherCapGargoyle());
            bagLeatherGargoyle.DropItem(new StuddedGlovesGargoyle());
            bagLeatherGargoyle.DropItem(new StuddedGorgetGargoyle());
            bagLeatherGargoyle.DropItem(new StuddedLegsGargoyle());
            bagLeatherGargoyle.DropItem(new GargoyleHides(5000));
            bagLeatherGargoyle.DropItem(new GargoyleLeather(5000));
            bagLeatherGargoyle.Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideGargoyle;
            this.DropItem(bagLeatherGargoyle);

            Bag bagLeatherTerathan = new Bag();
            bagLeatherTerathan.DropItem(new StuddedChestTerathan());
            bagLeatherTerathan.DropItem(new StuddedArmsTerathan());
            bagLeatherTerathan.DropItem(new LeatherCapTerathan());
            bagLeatherTerathan.DropItem(new StuddedGlovesTerathan());
            bagLeatherTerathan.DropItem(new StuddedGorgetTerathan());
            bagLeatherTerathan.DropItem(new StuddedLegsTerathan());
            bagLeatherTerathan.DropItem(new TerathanHides(5000));
            bagLeatherTerathan.DropItem(new TerathanLeather(5000));
            bagLeatherTerathan.Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideTerathan;
            this.DropItem(bagLeatherTerathan);

            Bag bagLeatherZZ = new Bag();
            bagLeatherZZ.DropItem(new StuddedChestZZ());
            bagLeatherZZ.DropItem(new StuddedArmsZZ());
            bagLeatherZZ.DropItem(new LeatherCapZZ());
            bagLeatherZZ.DropItem(new StuddedGlovesZZ());
            bagLeatherZZ.DropItem(new StuddedGorgetZZ());
            bagLeatherZZ.DropItem(new StuddedLegsZZ());
            bagLeatherZZ.DropItem(new ZZHides(5000));
            bagLeatherZZ.DropItem(new ZZLeather(5000));
            bagLeatherZZ.Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideZZ;
            this.DropItem(bagLeatherZZ);

		}

        public BagOfLeatherArmor(Serial serial)
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
