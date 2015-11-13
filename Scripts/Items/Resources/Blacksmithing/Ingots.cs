using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseIngot : Item, ICommodity
	{
		private CraftResource m_Resource;

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; InvalidateProperties(); }
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}
		
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (int) m_Resource );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_Resource = (CraftResource)reader.ReadInt();
					break;
				}
				case 0:
				{
					OreInfo info;

					switch ( reader.ReadInt() )
					{
						case 0: info = OreInfo.Iron; break;
                        case 1: info = OreInfo.Rusty; break;
                        case 2: info = OreInfo.OldCopper; break;
						case 3: info = OreInfo.DullCopper; break;
                        case 4: info = OreInfo.Ruby; break;
                        case 5: info = OreInfo.Copper; break;
                        case 6: info = OreInfo.Bronze; break;
						case 7: info = OreInfo.ShadowIron; break;
                        case 8: info = OreInfo.Silver; break;
                        case 9: info = OreInfo.Mercury; break;
                        case 10: info = OreInfo.Rose; break;
						case 11: info = OreInfo.Gold; break;
						case 12: info = OreInfo.Agapite; break;
						case 13: info = OreInfo.Verite; break;
                        case 14: info = OreInfo.Plutonio; break;
                        case 15: info = OreInfo.BloodRock; break;
						case 16: info = OreInfo.Valorite; break;
                        case 17: info = OreInfo.BlackRock; break;
                        case 18: info = OreInfo.Mytheril; break;
                        case 19: info = OreInfo.Aqua; break;
                        case 20: info = OreInfo.Endurium; break;
                        case 21: info = OreInfo.OldEndurium; break;
                        case 22: info = OreInfo.GoldStone; break;
                        case 23: info = OreInfo.MaxMytheril; break;
                        case 24: info = OreInfo.Magma; break;
						default: info = null; break;
					}

					m_Resource = CraftResources.GetFromOreInfo( info );
					break;
				}
			}
		}

		public BaseIngot( CraftResource resource ) : this( resource, 1 )
		{
		}

		public BaseIngot( CraftResource resource, int amount ) : base( 0x1BF2 )
		{
			Stackable = true;
			Amount = amount;
			Hue = CraftResources.GetHue( resource );

			m_Resource = resource;
		}

		public BaseIngot( Serial serial ) : base( serial )
		{
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Amount > 1 )
				list.Add( 1050039, "{0}\t#{1}", Amount, 1027154 ); // ~1_NUMBER~ ~2_ITEMNAME~
			else
				list.Add( 1027154 ); // ingots
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( !CraftResources.IsStandard( m_Resource ) )
			{
				int num = CraftResources.GetLocalizationNumber( m_Resource );

				if ( num > 0 )
					list.Add( num );
				else
					list.Add( CraftResources.GetName( m_Resource ) );
			}
		}

		public override int LabelNumber
		{
			get
			{
				if ( m_Resource >= CraftResource.Rusty && m_Resource <= CraftResource.Magma )
                    return 1042684 + (int)(m_Resource - CraftResource.Rusty);

				return 1042692;
			}
		}
	}

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class IronIngot : BaseIngot
	{
		[Constructable]
		public IronIngot() : this( 1 )
		{
		}

		[Constructable]
		public IronIngot( int amount ) : base( CraftResource.Iron, amount )
		{
		}

		public IronIngot( Serial serial ) : base( serial )
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

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class RustyIngot : BaseIngot
    {
        [Constructable]
        public RustyIngot()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueRusty;
        }

        [Constructable]
        public RustyIngot(int amount)
            : base(CraftResource.Rusty, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueRusty;
        }

        public RustyIngot(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }


    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class OldCopperIngot : BaseIngot
    {
        [Constructable]
        public OldCopperIngot()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueOldCopper;
        }

        [Constructable]
        public OldCopperIngot(int amount)
            : base(CraftResource.OldCopper, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueOldCopper;
        }

        public OldCopperIngot(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }


    }

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class DullCopperIngot : BaseIngot
	{
		[Constructable]
		public DullCopperIngot() : this( 1 )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueDullCopper;
		}

		[Constructable]
		public DullCopperIngot( int amount ) : base( CraftResource.DullCopper, amount )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueDullCopper;
		}

		public DullCopperIngot( Serial serial ) : base( serial )
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

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class RubyIngot : BaseIngot
    {
        [Constructable]
        public RubyIngot()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueRuby;
        }

        [Constructable]
        public RubyIngot(int amount)
            : base(CraftResource.Ruby, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueRuby;
        }

        public RubyIngot(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }


    }

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class CopperIngot : BaseIngot
	{
		[Constructable]
		public CopperIngot() : this( 1 )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueCopper;
		}

		[Constructable]
		public CopperIngot( int amount ) : base( CraftResource.Copper, amount )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueCopper;
		}

		public CopperIngot( Serial serial ) : base( serial )
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

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class BronzeIngot : BaseIngot
    {
        [Constructable]
        public BronzeIngot()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBronze;
        }

        [Constructable]
        public BronzeIngot(int amount)
            : base(CraftResource.Bronze, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBronze;
        }

        public BronzeIngot(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }


    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class ShadowIronIngot : BaseIngot
    {
        [Constructable]
        public ShadowIronIngot()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueShadow;
        }

        [Constructable]
        public ShadowIronIngot(int amount)
            : base(CraftResource.ShadowIron, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueShadow;
        }

        public ShadowIronIngot(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }


    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class SilverIngot : BaseIngot
    {
        [Constructable]
        public SilverIngot()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueSilver;
        }

        [Constructable]
        public SilverIngot(int amount)
            : base(CraftResource.Silver, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueSilver;
        }

        public SilverIngot(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }


    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class MercuryIngot : BaseIngot
    {
        [Constructable]
        public MercuryIngot()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMercury;
        }

        [Constructable]
        public MercuryIngot(int amount)
            : base(CraftResource.Mercury, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMercury;
        }

        public MercuryIngot(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }


    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class RoseIngot : BaseIngot
    {
        [Constructable]
        public RoseIngot()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueRose;
        }

        [Constructable]
        public RoseIngot(int amount)
            : base(CraftResource.Rose, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueRose;
        }

        public RoseIngot(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }


    }

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class GoldIngot : BaseIngot
	{
		[Constructable]
		public GoldIngot() : this( 1 )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueGold;
		}

		[Constructable]
		public GoldIngot( int amount ) : base( CraftResource.Gold, amount )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueGold;
		}

		public GoldIngot( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class AgapiteIngot : BaseIngot
	{
		[Constructable]
		public AgapiteIngot() : this( 1 )
		{
		}

		[Constructable]
		public AgapiteIngot( int amount ) : base( CraftResource.Agapite, amount )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueAgapite;
		}

		public AgapiteIngot( Serial serial ) : base( serial )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueAgapite;
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

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class VeriteIngot : BaseIngot
	{
		[Constructable]
		public VeriteIngot() : this( 1 )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueVerite;
		}

		[Constructable]
		public VeriteIngot( int amount ) : base( CraftResource.Verite, amount )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueVerite;
		}

		public VeriteIngot( Serial serial ) : base( serial )
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

    [FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class PlutoniumIngot : BaseIngot
	{
		[Constructable]
		public PlutoniumIngot() : this( 1 )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HuePlutonio;
		}

		[Constructable]
		public PlutoniumIngot( int amount ) : base( CraftResource.Plutonio, amount )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HuePlutonio;
		}

		public PlutoniumIngot( Serial serial ) : base( serial )
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

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class BloodRockIngot : BaseIngot
    {
        [Constructable]
        public BloodRockIngot()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBloodRock;
        }

        [Constructable]
        public BloodRockIngot(int amount)
            : base(CraftResource.BloodRock, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBloodRock;
             
        }

        public BloodRockIngot(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }


    }

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class ValoriteIngot : BaseIngot
	{
		[Constructable]
		public ValoriteIngot() : this( 1 )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueValorite;
		}

		[Constructable]
		public ValoriteIngot( int amount ) : base( CraftResource.Valorite, amount )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueValorite;
		}

		public ValoriteIngot( Serial serial ) : base( serial )
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

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class BlackRockIngot : BaseIngot
    {
        [Constructable]
        public BlackRockIngot()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBlackRock;
        }

        [Constructable]
        public BlackRockIngot(int amount)
            : base(CraftResource.BlackRock, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBlackRock;
        }

        public BlackRockIngot(Serial serial)
            : base(serial)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBlackRock;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }


    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class MytherilIngot : BaseIngot
    {
        [Constructable]
        public MytherilIngot()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMytheril;
        }

        [Constructable]
        public MytherilIngot(int amount)
            : base(CraftResource.Mytheril, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMytheril;
        }

        public MytherilIngot(Serial serial)
            : base(serial)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMytheril;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }


    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class AquaIngot : BaseIngot
    {
        [Constructable]
        public AquaIngot()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueAqua;
        }

        [Constructable]
        public AquaIngot(int amount)
            : base(CraftResource.Aqua, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueAqua;
        }

        public AquaIngot(Serial serial)
            : base(serial)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueAqua;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class EnduriumIngot : BaseIngot
    {
        [Constructable]
        public EnduriumIngot()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueEndurium;
        }

        [Constructable]
        public EnduriumIngot(int amount)
            : base(CraftResource.Endurium, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueEndurium;
        }

        public EnduriumIngot(Serial serial)
            : base(serial)
        {

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class OldEnduriumIngot : BaseIngot
    {
        [Constructable]
        public OldEnduriumIngot()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueOldEndurium;
        }

        [Constructable]
        public OldEnduriumIngot(int amount)
            : base(CraftResource.OldEndurium, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueOldEndurium;
        }

        public OldEnduriumIngot(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class GoldStoneIngot : BaseIngot
    {
        [Constructable]
        public GoldStoneIngot()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueGoldStone;
        }

        [Constructable]
        public GoldStoneIngot(int amount)
            : base(CraftResource.GoldStone, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueGoldStone;
        }

        public GoldStoneIngot(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class MaxMytherilIngot : BaseIngot
    {
        [Constructable]
        public MaxMytherilIngot()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMaxMytheril;
        }

        [Constructable]
        public MaxMytherilIngot(int amount)
            : base(CraftResource.MaxMytheril, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMaxMytheril;
        }

        public MaxMytherilIngot(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [FlipableAttribute(0x1BF2, 0x1BEF)]
    public class MagmaIngot : BaseIngot
    {
        [Constructable]
        public MagmaIngot()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMagma;
        }

        [Constructable]
        public MagmaIngot(int amount)
            : base(CraftResource.Magma, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMagma;
        }

        public MagmaIngot(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}