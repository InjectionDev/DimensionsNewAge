using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Items;
using Server.Gumps;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Targeting;

namespace Server.Spells.Fifth
{
	public class IncognitoSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Incognito", "Kal In Ex",
				206,
				9002,
				Reagent.Bloodmoss,
				Reagent.Garlic,
				Reagent.Nightshade
			);

        // EFFECT=
        public override double CastDelayFastScalar { get { return 0; } }
        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(1.5); } }
        public override bool ClearHandsOnCast { get { return false; } }
        public override int ScaleMana(int mana) { return 14; }

		public override SpellCircle Circle { get { return SpellCircle.Fifth; } }


        public override void SelectTarget()
        {
            Caster.Target = new InternalSphereTarget(this);
        }

        public override void OnSphereCast()
        {
            if (SpellTarget != null)
            {
                if (SpellTarget is Mobile)
                {
                    Target((Mobile)SpellTarget);
                }
                else
                {
                    Caster.SendAsciiMessage("This spell needs a target object");
                }
            }
            FinishSequence();
        }

	    public IncognitoSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (!CheckLineOfSight(m))
            {
                this.DoFizzle();
                Caster.SendAsciiMessage("Target is not in line of sight");
            }
            else if ( Factions.Sigil.ExistsOn( Caster ) )
			{
				Caster.SendLocalizedMessage( 1010445 ); // You cannot incognito if you have a sigil
			}
			else if ( !Caster.CanBeginAction( typeof( IncognitoSpell ) ) )
			{
				Caster.SendLocalizedMessage( 1005559 ); // This spell is already in effect.
			}
			else if ( Caster.BodyMod == 183 || Caster.BodyMod == 184 )
			{
				Caster.SendLocalizedMessage( 1042402 ); // You cannot use incognito while wearing body paint
			}
			else if ( DisguiseTimers.IsDisguised( Caster ) )
			{
				Caster.SendLocalizedMessage( 1061631 ); // You can't do that while disguised.
			}
			else if ( !Caster.CanBeginAction( typeof( PolymorphSpell ) ) || Caster.IsBodyMod )
			{
				DoFizzle();
			}
			else if ( CheckSequence() )
			{
				if ( Caster.BeginAction( typeof( IncognitoSpell ) ) )
				{
					DisguiseTimers.StopTimer( Caster );

					m.HueMod = Caster.Race.RandomSkinHue();
					m.NameMod = m.Female ? NameList.RandomName( "female" ) : NameList.RandomName( "male" );

					PlayerMobile pm = m as PlayerMobile;

					if ( pm != null && pm.Race != null )
					{
						pm.SetHairMods( pm.Race.RandomHair( pm.Female ), pm.Race.RandomFacialHair( pm.Female ) );
						pm.HairHue = pm.Race.RandomHairHue();
						pm.FacialHairHue = pm.Race.RandomHairHue();
					}

					Caster.FixedParticles( 0x373A, 10, 15, 5036, EffectLayer.Head );
					Caster.PlaySound( 0x3BD );

					BaseArmor.ValidateMobile( m );
					BaseClothing.ValidateMobile( m );

					StopTimer( m );


					int timeVal = ((6 * Caster.Skills.Magery.Fixed) / 50) + 1;

					if( timeVal > 144 )
						timeVal = 144;

					TimeSpan length = TimeSpan.FromSeconds( timeVal );


					Timer t = new InternalTimer( m, length );

					m_Timers[m] = t;

					t.Start();

					BuffInfo.AddBuff( m, new BuffInfo( BuffIcon.Incognito, 1075819, length, m ) );

				}
				else
				{
					Caster.SendLocalizedMessage( 1079022 ); // You're already incognitoed!
				}
			}

            FinishSequence();
        }

	    public override bool CheckCast()
		{
			if ( Factions.Sigil.ExistsOn( Caster ) )
			{
				Caster.SendLocalizedMessage( 1010445 ); // You cannot incognito if you have a sigil
				return false;
			}
			else if ( !Caster.CanBeginAction( typeof( IncognitoSpell ) ) )
			{
				Caster.SendLocalizedMessage( 1005559 ); // This spell is already in effect.
				return false;
			}
			else if ( Caster.BodyMod == 183 || Caster.BodyMod == 184 )
			{
				Caster.SendLocalizedMessage( 1042402 ); // You cannot use incognito while wearing body paint
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			if ( Factions.Sigil.ExistsOn( Caster ) )
			{
				Caster.SendLocalizedMessage( 1010445 ); // You cannot incognito if you have a sigil
			}
			else if ( !Caster.CanBeginAction( typeof( IncognitoSpell ) ) )
			{
				Caster.SendLocalizedMessage( 1005559 ); // This spell is already in effect.
			}
			else if ( Caster.BodyMod == 183 || Caster.BodyMod == 184 )
			{
				Caster.SendLocalizedMessage( 1042402 ); // You cannot use incognito while wearing body paint
			}
			else if ( DisguiseTimers.IsDisguised( Caster ) )
			{
				Caster.SendLocalizedMessage( 1061631 ); // You can't do that while disguised.
			}
			else if ( !Caster.CanBeginAction( typeof( PolymorphSpell ) ) || Caster.IsBodyMod )
			{
				DoFizzle();
			}
			else if ( CheckSequence() )
			{
				if ( Caster.BeginAction( typeof( IncognitoSpell ) ) )
				{
					DisguiseTimers.StopTimer( Caster );

					Caster.HueMod = Caster.Race.RandomSkinHue();
					Caster.NameMod = Caster.Female ? NameList.RandomName( "female" ) : NameList.RandomName( "male" );

					PlayerMobile pm = Caster as PlayerMobile;

					if ( pm != null && pm.Race != null )
					{
						pm.SetHairMods( pm.Race.RandomHair( pm.Female ), pm.Race.RandomFacialHair( pm.Female ) );
						pm.HairHue = pm.Race.RandomHairHue();
						pm.FacialHairHue = pm.Race.RandomHairHue();
					}

					Caster.FixedParticles( 0x373A, 10, 15, 5036, EffectLayer.Head );
					Caster.PlaySound( 0x3BD );

					BaseArmor.ValidateMobile( Caster );
					BaseClothing.ValidateMobile( Caster );

					StopTimer( Caster );


					int timeVal = ((6 * Caster.Skills.Magery.Fixed) / 50) + 1;

					if( timeVal > 144 )
						timeVal = 144;

					TimeSpan length = TimeSpan.FromSeconds( timeVal );


					Timer t = new InternalTimer( Caster, length );

					m_Timers[Caster] = t;

					t.Start();

					BuffInfo.AddBuff( Caster, new BuffInfo( BuffIcon.Incognito, 1075819, length, Caster ) );

				}
				else
				{
					Caster.SendLocalizedMessage( 1079022 ); // You're already incognitoed!
				}
			}

			FinishSequence();
		}

        private class InternalSphereTarget : Target
        {
            private IncognitoSpell m_Owner;

            public InternalSphereTarget(IncognitoSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.None)
            {
                m_Owner = owner;
                m_Owner.Caster.SendAsciiMessage("Selecione o alvo...");
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    m_Owner.SpellTarget = o;
                    m_Owner.CastSpell();
                }
                else
                {
                    m_Owner.Caster.SendAsciiMessage("This spell needs a target object");
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                if (m_Owner.SpellTarget == null)
                {
                    m_Owner.Caster.SendAsciiMessage("Target cancelado.");
                }
            }
        }

		private static Hashtable m_Timers = new Hashtable();

		public static bool StopTimer( Mobile m )
		{
			Timer t = (Timer)m_Timers[m];

			if ( t != null )
			{
				t.Stop();
				m_Timers.Remove( m );
				BuffInfo.RemoveBuff( m, BuffIcon.Incognito );
			}

			return ( t != null );
		}

		private static int[] m_HairIDs = new int[]
			{
				0x2044, 0x2045, 0x2046,
				0x203C, 0x203B, 0x203D,
				0x2047, 0x2048, 0x2049,
				0x204A, 0x0000
			};

		private static int[] m_BeardIDs = new int[]
			{
				0x203E, 0x203F, 0x2040,
				0x2041, 0x204B, 0x204C,
				0x204D, 0x0000
			};

		private class InternalTimer : Timer
		{
			private Mobile m_Owner;

			public InternalTimer( Mobile owner, TimeSpan length ) : base( length )
			{
				m_Owner = owner;

				/*
				int val = ((6 * owner.Skills.Magery.Fixed) / 50) + 1;

				if ( val > 144 )
					val = 144;

				Delay = TimeSpan.FromSeconds( val );
				 * */
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				if ( !m_Owner.CanBeginAction( typeof( IncognitoSpell ) ) )
				{
					if ( m_Owner is PlayerMobile )
						((PlayerMobile)m_Owner).SetHairMods( -1, -1 );

					m_Owner.BodyMod = 0;
					m_Owner.HueMod = -1;
					m_Owner.NameMod = null;
					m_Owner.EndAction( typeof( IncognitoSpell ) );

					BaseArmor.ValidateMobile( m_Owner );
					BaseClothing.ValidateMobile( m_Owner );
				}
			}
		}
	}
}
