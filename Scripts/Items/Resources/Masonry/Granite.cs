using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseGranite : Item
	{
		private CraftResource m_Resource;

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; InvalidateProperties(); }
		}

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
				case 0:
				{
					m_Resource = (CraftResource)reader.ReadInt();
					break;
				}
			}
			
			if ( version < 1 )
				Stackable = Core.ML;
		}

		public override double DefaultWeight
		{
			get { return Core.ML ? 1.0 : 10.0; } // Pub 57
		}

		public BaseGranite( CraftResource resource ) : base( 0x1779 )
		{
			Hue = CraftResources.GetHue( resource );
			Stackable = Core.ML;

			m_Resource = resource;
		}

		public BaseGranite( Serial serial ) : base( serial )
		{
		}

		public override int LabelNumber{ get{ return 1044607; } } // high quality granite

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
	}

	public class Granite : BaseGranite
	{
		[Constructable]
		public Granite() : base( CraftResource.Iron )
		{
		}

		public Granite( Serial serial ) : base( serial )
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

    public class RustyGranite : BaseGranite
    {
        [Constructable]
        public RustyGranite()
            : base(CraftResource.Rusty)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueRusty;
        }

        public RustyGranite(Serial serial)
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

    public class OldCopperGranite : BaseGranite
    {
        [Constructable]
        public OldCopperGranite()
            : base(CraftResource.OldCopper)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueOldCopper;
        }

        public OldCopperGranite(Serial serial)
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

	public class DullCopperGranite : BaseGranite
	{
		[Constructable]
		public DullCopperGranite() : base( CraftResource.DullCopper )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueDullCopper;
		}

		public DullCopperGranite( Serial serial ) : base( serial )
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

    public class RubyGranite : BaseGranite
    {
        [Constructable]
        public RubyGranite()
            : base(CraftResource.Ruby)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueRuby;
        }

        public RubyGranite(Serial serial)
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

    public class CopperGranite : BaseGranite
    {
        [Constructable]
        public CopperGranite()
            : base(CraftResource.Copper)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueCopper;
        }

        public CopperGranite(Serial serial)
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

    public class BronzeGranite : BaseGranite
    {
        [Constructable]
        public BronzeGranite()
            : base(CraftResource.Bronze)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBronze;
        }

        public BronzeGranite(Serial serial)
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

	public class ShadowIronGranite : BaseGranite
	{
		[Constructable]
		public ShadowIronGranite() : base( CraftResource.ShadowIron )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueShadow;
		}

		public ShadowIronGranite( Serial serial ) : base( serial )
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

    public class SilverGranite : BaseGranite
    {
        [Constructable]
        public SilverGranite()
            : base(CraftResource.Silver)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueSilver;
        }

        public SilverGranite(Serial serial)
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

    public class MercuryGranite : BaseGranite
    {
        [Constructable]
        public MercuryGranite()
            : base(CraftResource.Mercury)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMercury;
        }

        public MercuryGranite(Serial serial)
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

    public class RoseGranite : BaseGranite
    {
        [Constructable]
        public RoseGranite()
            : base(CraftResource.Rose)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueRose;
        }

        public RoseGranite(Serial serial)
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

	public class GoldGranite : BaseGranite
	{
		[Constructable]
		public GoldGranite() : base( CraftResource.Gold )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueGold;
		}

		public GoldGranite( Serial serial ) : base( serial )
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

	public class AgapiteGranite : BaseGranite
	{
		[Constructable]
		public AgapiteGranite() : base( CraftResource.Agapite )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueAgapite;
		}

		public AgapiteGranite( Serial serial ) : base( serial )
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

	public class VeriteGranite : BaseGranite
	{
		[Constructable]
		public VeriteGranite() : base( CraftResource.Verite )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueVerite;
		}

		public VeriteGranite( Serial serial ) : base( serial )
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

    public class PlutonioGranite : BaseGranite
    {
        [Constructable]
        public PlutonioGranite()
            : base(CraftResource.Plutonio)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HuePlutonio;
        }

        public PlutonioGranite(Serial serial)
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

    public class BloodRockGranite : BaseGranite
    {
        [Constructable]
        public BloodRockGranite()
            : base(CraftResource.BloodRock)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBloodRock;
        }

        public BloodRockGranite(Serial serial)
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

	public class ValoriteGranite : BaseGranite
	{
		[Constructable]
		public ValoriteGranite() : base( CraftResource.Valorite )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueValorite;
		}

		public ValoriteGranite( Serial serial ) : base( serial )
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

    public class BlackRockGranite : BaseGranite
    {
        [Constructable]
        public BlackRockGranite()
            : base(CraftResource.BlackRock)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBlackRock;
        }

        public BlackRockGranite(Serial serial)
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

    public class MytherilGranite : BaseGranite
    {
        [Constructable]
        public MytherilGranite()
            : base(CraftResource.Mytheril)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMytheril;
        }

        public MytherilGranite(Serial serial)
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

    public class AquaGranite : BaseGranite
    {
        [Constructable]
        public AquaGranite()
            : base(CraftResource.Aqua)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueAqua;
        }

        public AquaGranite(Serial serial)
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

    public class EnduriumGranite : BaseGranite
    {
        [Constructable]
        public EnduriumGranite()
            : base(CraftResource.Endurium)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueEndurium;
        }

        public EnduriumGranite(Serial serial)
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

    public class OldEnduriumGranite : BaseGranite
    {
        [Constructable]
        public OldEnduriumGranite()
            : base(CraftResource.OldEndurium)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueOldEndurium;
        }

        public OldEnduriumGranite(Serial serial)
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

    public class GoldStoneGranite : BaseGranite
    {
        [Constructable]
        public GoldStoneGranite()
            : base(CraftResource.GoldStone)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueGoldStone;
        }

        public GoldStoneGranite(Serial serial)
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

    public class MaxMytherilGranite : BaseGranite
    {
        [Constructable]
        public MaxMytherilGranite()
            : base(CraftResource.MaxMytheril)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMaxMytheril;
        }

        public MaxMytherilGranite(Serial serial)
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

    public class MagmaGranite : BaseGranite
    {
        [Constructable]
        public MagmaGranite()
            : base(CraftResource.Magma)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMagma;
        }

        public MagmaGranite(Serial serial)
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