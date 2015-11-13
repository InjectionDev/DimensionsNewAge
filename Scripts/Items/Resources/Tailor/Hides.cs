using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseHides : Item, ICommodity
	{
		private CraftResource m_Resource;

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; InvalidateProperties(); }
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
					OreInfo info = new OreInfo( reader.ReadInt(), reader.ReadInt(), reader.ReadString() );

					m_Resource = CraftResources.GetFromOreInfo( info );
					break;
				}
			}
		}

		public BaseHides( CraftResource resource ) : this( resource, 1 )
		{
		}

		public BaseHides( CraftResource resource, int amount ) : base( 0x1079 )
		{
			Stackable = true;
			Weight = 5.0;
			Amount = amount;
			Hue = CraftResources.GetHue( resource );

			m_Resource = resource;
		}

		public BaseHides( Serial serial ) : base( serial )
		{
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Amount > 1 )
				list.Add( 1050039, "{0}\t#{1}", Amount, 1024216 ); // ~1_NUMBER~ ~2_ITEMNAME~
			else
				list.Add( 1024216 ); // pile of hides
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
				if ( m_Resource >= CraftResource.SpinedLeather && m_Resource <= CraftResource.BarbedLeather )
					return 1049687 + (int)(m_Resource - CraftResource.SpinedLeather);

				return 1047023;
			}
		}
	}

	[FlipableAttribute( 0x1079, 0x1078 )]
	public class Hides : BaseHides, IScissorable
	{
		[Constructable]
		public Hides() : this( 1 )
		{
		}

		[Constructable]
		public Hides( int amount ) : base( CraftResource.RegularLeather, amount )
		{
		}

		public Hides( Serial serial ) : base( serial )
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

		

		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) ) return false;

			if ( !IsChildOf ( from.Backpack ) )
			{
				from.SendLocalizedMessage ( 502437 ); // Items you wish to cut must be in your backpack
				return false;
			}
			base.ScissorHelper( from, new Leather(), 1 );

			return true;
		}
	}

	[FlipableAttribute( 0x1079, 0x1078 )]
	public class SpinedHides : BaseHides, IScissorable
	{
		[Constructable]
		public SpinedHides() : this( 1 )
		{
		}

		[Constructable]
		public SpinedHides( int amount ) : base( CraftResource.SpinedLeather, amount )
		{
		}

		public SpinedHides( Serial serial ) : base( serial )
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

		

		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) ) return false;

			if ( !IsChildOf ( from.Backpack ) )
			{
				from.SendLocalizedMessage ( 502437 ); // Items you wish to cut must be in your backpack
				return false;
			}

			base.ScissorHelper( from, new SpinedLeather(), 1 );

			return true;
		}
	}

	[FlipableAttribute( 0x1079, 0x1078 )]
	public class HornedHides : BaseHides, IScissorable
	{
		[Constructable]
		public HornedHides() : this( 1 )
		{
		}

		[Constructable]
		public HornedHides( int amount ) : base( CraftResource.HornedLeather, amount )
		{
		}

		public HornedHides( Serial serial ) : base( serial )
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

		

		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) ) return false;

			if ( !IsChildOf ( from.Backpack ) )
			{
				from.SendLocalizedMessage ( 502437 ); // Items you wish to cut must be in your backpack
				return false;
			}
			
			base.ScissorHelper( from, new HornedLeather(), 1 );

			return true;
		}
	}

	[FlipableAttribute( 0x1079, 0x1078 )]
	public class BarbedHides : BaseHides, IScissorable
	{
		[Constructable]
		public BarbedHides() : this( 1 )
		{
		}

		[Constructable]
		public BarbedHides( int amount ) : base( CraftResource.BarbedLeather, amount )
		{
		}

		public BarbedHides( Serial serial ) : base( serial )
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

		

		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) ) return false;

			if ( !IsChildOf ( from.Backpack ) )
			{
				from.SendLocalizedMessage ( 502437 ); // Items you wish to cut must be in your backpack
				return false;
			}

			base.ScissorHelper( from, new BarbedLeather(), 1 );

			return true;
		}
	}


    [FlipableAttribute(0x1079, 0x1078)]
    public class CyclopsHides : BaseHides, IScissorable
    {
        [Constructable]
        public CyclopsHides()
            : this(1)
        {
            Name = "Cyclop Hide";
            Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideCyclops; ;
        }

        [Constructable]
        public CyclopsHides(int amount)
            : base(CraftResource.CyclopsLeather, amount)
        {
            Name = "Cyclop Hide";
            Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideCyclops; ;
        }

        public CyclopsHides(Serial serial)
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



        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (Deleted || !from.CanSee(this)) return false;

            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new CyclopsLeather(), 2);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class GargoyleHides : BaseHides, IScissorable
    {
        [Constructable]
        public GargoyleHides()
            : this(1)
        {
            Name = "Gargoyle Hide";
            Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideGargoyle;
        }

        [Constructable]
        public GargoyleHides(int amount)
            : base(CraftResource.GargoyleLeather, amount)
        {
            Name = "Gargoyle Hide";
            Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideGargoyle;
        }

        public GargoyleHides(Serial serial)
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



        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (Deleted || !from.CanSee(this)) return false;

            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new GargoyleLeather(), 2);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class TerathanHides : BaseHides, IScissorable
    {
        [Constructable]
        public TerathanHides()
            : this(1)
        {
            Name = "Terathan Hide";
            Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideTerathan;
        }

        [Constructable]
        public TerathanHides(int amount)
            : base(CraftResource.TerathanLeather, amount)
        {
            Name = "Terathan Hide";
            Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideTerathan;
        }

        public TerathanHides(Serial serial)
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



        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (Deleted || !from.CanSee(this)) return false;

            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new TerathanLeather(), 2);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class DaemonHides : BaseHides, IScissorable
    {
        [Constructable]
        public DaemonHides()
            : this(1)
        {
            Name = "Daemon Hide";
            Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideDaemon;
        }

        [Constructable]
        public DaemonHides(int amount)
            : base(CraftResource.DaemonLeather, amount)
        {
            Name = "Daemon Hide";
            Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideDaemon;
        }

        public DaemonHides(Serial serial)
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



        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (Deleted || !from.CanSee(this)) return false;

            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DaemonLeather(), 2);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class DragonHides : BaseHides, IScissorable
    {
        [Constructable]
        public DragonHides()
            : this(1)
        {
            Name = "Dragon Hide";
            Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideDragon;
        }

        [Constructable]
        public DragonHides(int amount)
            : base(CraftResource.DragonLeather, amount)
        {
            Name = "Dragon Hide";
            Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideDragon;
        }

        public DragonHides(Serial serial)
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



        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (Deleted || !from.CanSee(this)) return false;

            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonLeather(), 2);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class ZZHides : BaseHides, IScissorable
    {
        [Constructable]
        public ZZHides()
            : this(1)
        {
            Name = "ZZ Hide";
            Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideZZ;
        }

        [Constructable]
        public ZZHides(int amount)
            : base(CraftResource.ZZLeather, amount)
        {
            Name = "ZZ Hide";
            Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideZZ;
        }

        public ZZHides(Serial serial)
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



        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (Deleted || !from.CanSee(this)) return false;

            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new ZZLeather(), 2);

            return true;
        }
    }

    [FlipableAttribute(0x1079, 0x1078)]
    public class DragonGreenHides : BaseHides, IScissorable
    {
        [Constructable]
        public DragonGreenHides()
            : this(1)
        {
            Name = "Dragon Green Hide";
            Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideDragonGreen;
        }

        [Constructable]
        public DragonGreenHides(int amount)
            : base(CraftResource.DragonGreenLeather, amount)
        {
            Name = "Dragon Green Hide";
            Hue = DimensionsNewAge.Scripts.HueHideConst.HueHideDragonGreen;
        }

        public DragonGreenHides(Serial serial)
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



        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (Deleted || !from.CanSee(this)) return false;

            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(502437); // Items you wish to cut must be in your backpack
                return false;
            }

            base.ScissorHelper(from, new DragonGreenLeather(), 2);

            return true;
        }
    }
}