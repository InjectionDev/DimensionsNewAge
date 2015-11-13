using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Fifth
{
	public class SummonCreatureSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Summon Creature", "Kal Xen",
				266,
				9040,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk
			);

        // EFFECT=
        public override double CastDelayFastScalar { get { return 0; } }
        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(4.0); } }
        public override bool ClearHandsOnCast { get { return false; } }
        public override int ScaleMana(int mana) { return 14; }

		public override SpellCircle Circle { get { return SpellCircle.Fifth; } }

        public override void SelectTarget()
        {
            Caster.Target = new InternalSphereTarget(this);
        }

        public override void OnSphereCast()
        {
            //TODO: Show menu to choose the creature
            if (SpellTarget != null)
            {
                if (SpellTarget is LandTarget)
                {
                    Target((LandTarget)SpellTarget);
                }
                else
                {
                    Caster.SendAsciiMessage("You must target the ground with this spell.");
                }
            }
            FinishSequence();
        }

	    public SummonCreatureSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

        public void Target(LandTarget p)
        {
            if (!Caster.CanSee(p))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (!CheckLineOfSight(p))
            {
                this.DoFizzle();
                Caster.SendAsciiMessage("Target is not in line of sight");
            }
            else
            {
                if (CheckSequence())
                {
                    try
                    {
                        BaseCreature creature = (BaseCreature)Activator.CreateInstance(m_Types[Utility.Random(m_Types.Length)]);

                        creature.ControlSlots = 2;

                        TimeSpan duration;

                        if (Core.AOS)
                            duration = TimeSpan.FromSeconds((2 * Caster.Skills.Magery.Fixed) / 5);
                        else
                            duration = TimeSpan.FromSeconds(4.0 * Caster.Skills[SkillName.Magery].Value);

                        SpellHelper.Summon(creature, Caster, 0x215, duration, false, false);

                        creature.MoveToWorld(new Point3D(p), Caster.Map );
                    }
                    catch
                    {
                    }
                }

                FinishSequence();
            }
        }

	    // TODO: Get real list
		private static Type[] m_Types = new Type[]
			{
				typeof( PolarBear ),
				typeof( GrizzlyBear ),
				typeof( BlackBear ),
				typeof( BrownBear ),
				typeof( Horse ),
				typeof( Walrus ),
				typeof( GreatHart ),
				typeof( Hind ),
				typeof( Dog ),
				typeof( Boar ),
				typeof( Chicken ),
				typeof( Rabbit )
			};

		public override bool CheckCast()
		{
			if ( !base.CheckCast() )
				return false;

			if ( (Caster.Followers + 2) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				try
				{
					BaseCreature creature = (BaseCreature)Activator.CreateInstance( m_Types[Utility.Random( m_Types.Length )] );

					creature.ControlSlots = 2;

					TimeSpan duration;

					if ( Core.AOS )
						duration = TimeSpan.FromSeconds( (2 * Caster.Skills.Magery.Fixed) / 5 );
					else
						duration = TimeSpan.FromSeconds( 4.0 * Caster.Skills[SkillName.Magery].Value );

					SpellHelper.Summon( creature, Caster, 0x215, duration, false, false );
				}
				catch
				{
				}
			}

			FinishSequence();
		}

		public override TimeSpan GetCastDelay()
		{
			if ( Core.AOS )
				return TimeSpan.FromTicks( base.GetCastDelay().Ticks * 5 );

			return base.GetCastDelay() + TimeSpan.FromSeconds( 6.0 );
		}

        private class InternalSphereTarget : Target
        {
            private SummonCreatureSpell m_Owner;

            public InternalSphereTarget(SummonCreatureSpell owner)
                : base(Core.ML ? 10 : 12, true, TargetFlags.None)
            {
                m_Owner = owner;
                m_Owner.Caster.SendAsciiMessage("Selecione o alvo...");
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is LandTarget)
                {
                    m_Owner.SpellTarget = o;
                    m_Owner.CastSpell();
                }
                else
                {
                    m_Owner.Caster.SendAsciiMessage("You must target the ground with this spell.");
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
	}
}