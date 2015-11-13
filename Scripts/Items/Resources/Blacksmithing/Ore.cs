using System;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Engines.Craft;
using Server.Mobiles;

namespace Server.Items
{
	public abstract class BaseOre : Item, ICommodity
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

		public abstract BaseIngot GetIngot();

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

		public BaseOre( CraftResource resource ) : this( resource, 1 )
		{
		}

		public BaseOre( CraftResource resource, int amount ) : base( Utility.Random( 4 ) )
		{
			{
				double random = Utility.RandomDouble();
				if ( 0.12 >= random )
					ItemID = 0x19B7;
				else if ( 0.18 >= random )
					ItemID = 0x19B8;
				else if ( 0.25 >= random )
					ItemID = 0x19BA;
				else
					ItemID = 0x19B9;
			}
			
			Stackable = true;
			Amount = amount;
			Hue = CraftResources.GetHue( resource );

			m_Resource = resource;
		}

		public BaseOre( Serial serial ) : base( serial )
		{
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Amount > 1 )
				list.Add( 1050039, "{0}\t#{1}", Amount, 1026583 ); // ~1_NUMBER~ ~2_ITEMNAME~
			else
				list.Add( 1026583 ); // ore
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
                    return 1042845 + (int)(m_Resource - CraftResource.Rusty);

				return 1042853; // iron ore;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;
			
			if ( RootParent is BaseCreature )
			{
				from.SendLocalizedMessage( 500447 ); // That is not accessible
				return;
			}
			else if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				from.SendLocalizedMessage( 501971 ); // Select the forge on which to smelt the ore, or another pile of ore with which to combine it.
				from.Target = new InternalTarget( this );
			}
			else
			{
				from.SendLocalizedMessage( 501976 ); // The ore is too far away.
			}
		}

		private class InternalTarget : Target
		{
			private BaseOre m_Ore;

			public InternalTarget( BaseOre ore ) :  base ( 2, false, TargetFlags.None )
			{
				m_Ore = ore;
			}

			private bool IsForge( object obj )
			{
				if ( Core.ML && obj is Mobile && ((Mobile)obj).IsDeadBondedPet )
					return false;

				if ( obj.GetType().IsDefined( typeof( ForgeAttribute ), false ) )
					return true;

				int itemID = 0;

				if ( obj is Item )
					itemID = ((Item)obj).ItemID;
				else if ( obj is StaticTarget )
					itemID = ((StaticTarget)obj).ItemID;

				return ( itemID == 4017 || (itemID >= 6522 && itemID <= 6569) );
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Ore.Deleted )
					return;

				if ( !from.InRange( m_Ore.GetWorldLocation(), 2 ) )
				{
					from.SendLocalizedMessage( 501976 ); // The ore is too far away.
					return;
				}
				
				#region Combine Ore
				if ( targeted is BaseOre )
				{
					BaseOre ore = (BaseOre)targeted;
					if ( !ore.Movable )
						return;
					else if ( m_Ore == ore )
					{
						from.SendLocalizedMessage( 501972 ); // Select another pile or ore with which to combine this.
						from.Target = new InternalTarget( ore );
						return;
					}
					else if ( ore.Resource != m_Ore.Resource )
					{
						from.SendLocalizedMessage( 501979 ); // You cannot combine ores of different metals.
						return;
					}

					int worth = ore.Amount;
					if ( ore.ItemID == 0x19B9 )
						worth *= 8;
					else if ( ore.ItemID == 0x19B7 )
						worth *= 2;
					else 
						worth *= 4;
					int sourceWorth = m_Ore.Amount;
					if ( m_Ore.ItemID == 0x19B9 )
						sourceWorth *= 8;
					else if ( m_Ore.ItemID == 0x19B7 )
						sourceWorth *= 2;
					else
						sourceWorth *= 4;
					worth += sourceWorth;

					int plusWeight = 0;
					int newID = ore.ItemID;
					if ( ore.DefaultWeight != m_Ore.DefaultWeight )
					{
						if ( ore.ItemID == 0x19B7 || m_Ore.ItemID == 0x19B7 )
						{
							newID = 0x19B7;
						}
						else if ( ore.ItemID == 0x19B9 )
						{
							newID = m_Ore.ItemID;
							plusWeight = ore.Amount * 2;
						}
						else
						{
							plusWeight = m_Ore.Amount * 2;
						}
					}

					if ( (ore.ItemID == 0x19B9 && worth > 120000) || (( ore.ItemID == 0x19B8 || ore.ItemID == 0x19BA ) && worth > 60000) || (ore.ItemID == 0x19B7 && worth > 30000))
					{
						from.SendLocalizedMessage( 1062844 ); // There is too much ore to combine.
						return;
					}
					else if ( ore.RootParent is Mobile && (plusWeight + ((Mobile)ore.RootParent).Backpack.TotalWeight) > ((Mobile)ore.RootParent).Backpack.MaxWeight )
					{ 
						from.SendLocalizedMessage( 501978 ); // The weight is too great to combine in a container.
						return;
					}

					ore.ItemID = newID;
					if ( ore.ItemID == 0x19B9 )
					{
						ore.Amount = worth / 8;
						m_Ore.Delete();
					}
					else if ( ore.ItemID == 0x19B7 )
					{
						ore.Amount = worth / 2;
						m_Ore.Delete();
					}
					else
					{
						ore.Amount = worth / 4;
						m_Ore.Delete();
					}	
					return;
				}
				#endregion

				if ( IsForge( targeted ) )
				{
					double difficulty;

					switch ( m_Ore.Resource )
					{
						default: difficulty = 50.0; break;
                        case CraftResource.Iron: difficulty = 50.0; break;
                        case CraftResource.Rusty: difficulty = 20.0; break;
                        case CraftResource.OldCopper: difficulty = 55.0; break;
						case CraftResource.DullCopper: difficulty = 60.0; break;
                        case CraftResource.Ruby: difficulty = 60.0; break;
                        case CraftResource.Copper: difficulty = 65.0; break;
                        case CraftResource.Bronze: difficulty = 70.0; break;
						case CraftResource.ShadowIron: difficulty = 74.0; break;
                        case CraftResource.Silver: difficulty = 78.0; break;
                        case CraftResource.Mercury: difficulty = 78.0; break;
                        case CraftResource.Rose: difficulty = 82.0; break;
						case CraftResource.Gold: difficulty = 85.0; break;
						case CraftResource.Agapite: difficulty = 88.0; break;
						case CraftResource.Verite: difficulty = 91.0; break;
                        case CraftResource.Plutonio: difficulty = 91.0; break;
                        case CraftResource.BloodRock: difficulty = 94.0; break;
						case CraftResource.Valorite: difficulty = 97.0; break;
                        case CraftResource.BlackRock: difficulty = 100.0; break;
                        case CraftResource.Mytheril: difficulty = 105.0; break;
                        case CraftResource.Aqua: difficulty = 105.0; break;

                        case CraftResource.Endurium: difficulty = 105.0; break;
                        case CraftResource.OldEndurium: difficulty = 105.0; break;
                        case CraftResource.GoldStone: difficulty = 105.0; break;
                        case CraftResource.MaxMytheril: difficulty = 105.0; break;
                        case CraftResource.Magma: difficulty = 105.0; break;
					}

					double minSkill = difficulty - 25.0;
					double maxSkill = difficulty + 25.0;
					
					if ( difficulty > 50.0 && difficulty > from.Skills[SkillName.Mining].Value )
					{
						from.SendLocalizedMessage( 501986 ); // You have no idea how to smelt this strange ore!
						return;
					}
					
					if ( m_Ore.Amount <= 1 && m_Ore.ItemID == 0x19B7 )
					{
						from.SendLocalizedMessage( 501987 ); // There is not enough metal-bearing ore in this pile to make an ingot.
						return;
					}

					if ( from.CheckTargetSkill( SkillName.Mining, targeted, minSkill, maxSkill ) )
					{
						if ( m_Ore.Amount <= 0 )
						{
							from.SendLocalizedMessage( 501987 ); // There is not enough metal-bearing ore in this pile to make an ingot.
						}
						else
						{
							int amount = m_Ore.Amount;
							if ( m_Ore.Amount > 30000 )
								amount = 30000;

							BaseIngot ingot = m_Ore.GetIngot();
							
							if ( m_Ore.ItemID == 0x19B7 )
							{
								if ( m_Ore.Amount % 2 == 0 )
								{
									amount /= 2;
									m_Ore.Delete();
								}
								else
								{
									amount /= 2;
									m_Ore.Amount = 1;
								}
							}
								
							else if ( m_Ore.ItemID == 0x19B9 )
							{
								amount *= 2;
								m_Ore.Delete();
							}
							
							else
							{
								amount /= 1;
								m_Ore.Delete();
							}

							ingot.Amount = amount;
							from.AddToBackpack( ingot );
							//from.PlaySound( 0x57 );


							from.SendLocalizedMessage( 501988 ); // You smelt the ore removing the impurities and put the metal in your backpack.
						}
					}
					else if ( m_Ore.Amount < 2 && m_Ore.ItemID == 0x19B9 )
					{
						from.SendLocalizedMessage( 501990 ); // You burn away the impurities but are left with less useable metal.
						m_Ore.ItemID = 0x19B8;
					}
					else if ( m_Ore.Amount < 2 && m_Ore.ItemID == 0x19B8 || m_Ore.ItemID == 0x19BA )
					{
						from.SendLocalizedMessage( 501990 ); // You burn away the impurities but are left with less useable metal.
						m_Ore.ItemID = 0x19B7;
					}
					else
					{
						from.SendLocalizedMessage( 501990 ); // You burn away the impurities but are left with less useable metal.
						m_Ore.Amount /= 2;
					}
				}
			}
		}
	}

	public class IronOre : BaseOre
	{
		[Constructable]
		public IronOre() : this( 1 )
		{
            Name = "Iron Ore";
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueIron;
		}

		[Constructable]
		public IronOre( int amount ) : base( CraftResource.Iron, amount )
        {
            Name = "Iron Ore";
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueIron;
		}

		public IronOre( Serial serial ) : base( serial )
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

		public override BaseIngot GetIngot()
		{
			return new IronIngot();
		}
	}

    public class RustyOre : BaseOre
    {
        [Constructable]
        public RustyOre()
            : this(1)
        {
            Name = "Rusty Ore";
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueRusty;
        }

        [Constructable]
        public RustyOre(int amount)
            : base(CraftResource.Rusty, amount)
        {
            Name = "Rusty Ore";
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueRusty;
        }

        public RustyOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new RustyIngot();
        }
    }

    public class OldCopperOre : BaseOre
    {
        [Constructable]
        public OldCopperOre()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueOldCopper;
        }

        [Constructable]
        public OldCopperOre(int amount)
            : base(CraftResource.OldCopper, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueOldCopper;
        }

        public OldCopperOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new OldCopperIngot();
        }
    }

	public class DullCopperOre : BaseOre
	{
		[Constructable]
		public DullCopperOre() : this( 1 )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueDullCopper;
		}

		[Constructable]
		public DullCopperOre( int amount ) : base( CraftResource.DullCopper, amount )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueDullCopper;
		}

		public DullCopperOre( Serial serial ) : base( serial )
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

		public override BaseIngot GetIngot()
		{
			return new DullCopperIngot();
		}
	}

    public class RubyOre : BaseOre
    {
        [Constructable]
        public RubyOre()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueRuby;
        }

        [Constructable]
        public RubyOre(int amount)
            : base(CraftResource.Ruby, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueRuby;
        }

        public RubyOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new RubyIngot();
        }
    }

    public class CopperOre : BaseOre
    {
        [Constructable]
        public CopperOre()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueCopper;
        }

        [Constructable]
        public CopperOre(int amount)
            : base(CraftResource.Copper, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueCopper;
        }

        public CopperOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new CopperIngot();
        }
    }

    public class BronzeOre : BaseOre
    {
        [Constructable]
        public BronzeOre()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBronze;
        }

        [Constructable]
        public BronzeOre(int amount)
            : base(CraftResource.Bronze, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBronze;
        }

        public BronzeOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new BronzeIngot();
        }
    }

	public class ShadowIronOre : BaseOre
	{
		[Constructable]
		public ShadowIronOre() : this( 1 )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueShadow;
		}

		[Constructable]
		public ShadowIronOre( int amount ) : base( CraftResource.ShadowIron, amount )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueShadow;
		}

		public ShadowIronOre( Serial serial ) : base( serial )
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

		public override BaseIngot GetIngot()
		{
			return new ShadowIronIngot();
		}
	}

    public class SilverOre : BaseOre
    {
        [Constructable]
        public SilverOre()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueSilver;
        }

        [Constructable]
        public SilverOre(int amount)
            : base(CraftResource.Silver, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueSilver;
        }

        public SilverOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new SilverIngot();
        }
    }

    public class MercuryOre : BaseOre
    {
        [Constructable]
        public MercuryOre()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMercury;
        }

        [Constructable]
        public MercuryOre(int amount)
            : base(CraftResource.Mercury, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMercury;
        }

        public MercuryOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new MercuryIngot();
        }
    }

    public class RoseOre : BaseOre
    {
        [Constructable]
        public RoseOre()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueRose;
        }

        [Constructable]
        public RoseOre(int amount)
            : base(CraftResource.Rose, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueRose;
        }

        public RoseOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new RoseIngot();
        }
    }

	public class GoldOre : BaseOre
	{
		[Constructable]
		public GoldOre() : this( 1 )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueGold;
		}

		[Constructable]
		public GoldOre( int amount ) : base( CraftResource.Gold, amount )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueGold;
		}

		public GoldOre( Serial serial ) : base( serial )
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
		
		public override BaseIngot GetIngot()
		{
			return new GoldIngot();
		}
	}

	public class AgapiteOre : BaseOre
	{
		[Constructable]
		public AgapiteOre() : this( 1 )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueAgapite;
		}

		[Constructable]
		public AgapiteOre( int amount ) : base( CraftResource.Agapite, amount )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueAgapite;
		}

		public AgapiteOre( Serial serial ) : base( serial )
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

		public override BaseIngot GetIngot()
		{
			return new AgapiteIngot();
		}
	}

	public class VeriteOre : BaseOre
	{
		[Constructable]
		public VeriteOre() : this( 1 )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueVerite;
		}

		[Constructable]
		public VeriteOre( int amount ) : base( CraftResource.Verite, amount )
		{
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueVerite;
		}

		public VeriteOre( Serial serial ) : base( serial )
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

		public override BaseIngot GetIngot()
		{
			return new VeriteIngot();
		}
	}

    public class PlutoniumOre : BaseOre
    {
        [Constructable]
        public PlutoniumOre()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HuePlutonio;
        }

        [Constructable]
        public PlutoniumOre(int amount)
            : base(CraftResource.Plutonio, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HuePlutonio;
        }

        public PlutoniumOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new PlutoniumIngot();
        }
    }

    public class BloodRockOre : BaseOre
    {
        [Constructable]
        public BloodRockOre()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBloodRock;
        }

        [Constructable]
        public BloodRockOre(int amount)
            : base(CraftResource.BloodRock, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBloodRock;
        }

        public BloodRockOre(Serial serial)
            : base(serial)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBloodRock;
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

        public override BaseIngot GetIngot()
        {
            return new BloodRockIngot();
        }
    }

	public class ValoriteOre : BaseOre
	{
		[Constructable]
		public ValoriteOre() : this( 1 )
		{
		}

		[Constructable]
		public ValoriteOre( int amount ) : base( CraftResource.Valorite, amount )
		{
		}

		public ValoriteOre( Serial serial ) : base( serial )
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

		public override BaseIngot GetIngot()
		{
			return new ValoriteIngot();
		}
	}

    public class BlackRockOre : BaseOre
    {
        [Constructable]
        public BlackRockOre()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBlackRock;
        }

        [Constructable]
        public BlackRockOre(int amount)
            : base(CraftResource.BlackRock, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueBlackRock;
        }

        public BlackRockOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new BlackRockIngot();
        }
    }

    public class MytherilOre : BaseOre
    {
        [Constructable]
        public MytherilOre()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMytheril;
        }

        [Constructable]
        public MytherilOre(int amount)
            : base(CraftResource.Mytheril, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMytheril;
        }

        public MytherilOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new MytherilIngot();
        }
    }

    public class AquaOre : BaseOre
    {
        [Constructable]
        public AquaOre()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueAqua;
        }

        [Constructable]
        public AquaOre(int amount)
            : base(CraftResource.Aqua, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueAqua;
        }

        public AquaOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new AquaIngot();
        }
    }

    public class EnduriumOre : BaseOre
    {
        [Constructable]
        public EnduriumOre()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueEndurium;
        }

        [Constructable]
        public EnduriumOre(int amount)
            : base(CraftResource.Endurium, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueEndurium;
        }

        public EnduriumOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new EnduriumIngot();
        }
    }

    public class OldEnduriumOre : BaseOre
    {
        [Constructable]
        public OldEnduriumOre()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueOldEndurium;
        }

        [Constructable]
        public OldEnduriumOre(int amount)
            : base(CraftResource.OldEndurium, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueOldEndurium;
        }

        public OldEnduriumOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new OldEnduriumIngot();
        }
    }

    public class GoldStoneOre : BaseOre
    {
        [Constructable]
        public GoldStoneOre()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueGoldStone;
        }

        [Constructable]
        public GoldStoneOre(int amount)
            : base(CraftResource.GoldStone, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueGoldStone;
        }

        public GoldStoneOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new GoldStoneIngot();
        }
    }

    public class MaxMytherilOre : BaseOre
    {
        [Constructable]
        public MaxMytherilOre()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMaxMytheril;
        }

        [Constructable]
        public MaxMytherilOre(int amount)
            : base(CraftResource.MaxMytheril, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMaxMytheril;
        }

        public MaxMytherilOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new MaxMytherilIngot();
        }
    }

    public class MagmaOre : BaseOre
    {
        [Constructable]
        public MagmaOre()
            : this(1)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMagma;
        }

        [Constructable]
        public MagmaOre(int amount)
            : base(CraftResource.Magma, amount)
        {
            Hue = DimensionsNewAge.Scripts.HueOreConst.HueMagma;
        }

        public MagmaOre(Serial serial)
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

        public override BaseIngot GetIngot()
        {
            return new MagmaIngot();
        }
    }
}