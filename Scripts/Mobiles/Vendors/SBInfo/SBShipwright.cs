using System;
using System.Collections.Generic;
using Server.Items;
using Server.Multis;

namespace Server.Mobiles
{
	public class SBShipwright : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBShipwright()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "1041205", typeof( SmallBoatDeed ), 20177, 20, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "1041206", typeof( SmallDragonBoatDeed ), 22177, 20, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "1041207", typeof( MediumBoatDeed ), 25552, 20, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "1041208", typeof( MediumDragonBoatDeed ), 27552, 20, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "1041209", typeof( LargeBoatDeed ), 35927, 20, 0x14F2, 0 ) );
				Add( new GenericBuyInfo( "1041210", typeof( LargeDragonBoatDeed ), 37927, 20, 0x14F2, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				//You technically CAN sell them back, *BUT* the vendors do not carry enough money to buy with
			}
		}
	}
}