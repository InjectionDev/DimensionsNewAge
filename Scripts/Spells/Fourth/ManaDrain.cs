using System;
using System.Collections.Generic;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Fourth
{
	public class ManaDrainSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Mana Drain", "Ort Rel",
				215,
				9031,
				Reagent.BlackPearl,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk
			);

        // EFFECT=
        public override double CastDelayFastScalar { get { return 0; } }
        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(1.2); } }
        public override bool ClearHandsOnCast { get { return false; } }
        public override int ScaleMana(int mana) { return 11; }

		public override SpellCircle Circle { get { return SpellCircle.Fourth; } }

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

	    public ManaDrainSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		private static Dictionary<Mobile, Timer> m_Table = new Dictionary<Mobile, Timer>();

		private void AosDelay_Callback( object state )
		{
			object[] states = (object[])state;

			Mobile m = (Mobile)states[0];
			int mana = (int)states[1];

			if ( m.Alive && !m.IsDeadBondedPet )
			{
				m.Mana += mana;

				m.FixedEffect( 0x3779, 10, 25 );
				m.PlaySound( 0x28E );
			}

			m_Table.Remove( m );
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
            else if (!CheckLineOfSight(m))
            {
                this.DoFizzle();
                Caster.SendAsciiMessage("Target is not in line of sight");
            }
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( (int)this.Circle, Caster, ref m );

				if ( m.Spell != null )
					m.Spell.OnCasterHurt();

				m.Paralyzed = false;

				if ( Core.AOS )
				{
					int toDrain = 40 + (int)(GetDamageSkill( Caster ) - GetResistSkill( m ));

					if ( toDrain < 0 )
						toDrain = 0;
					else if ( toDrain > m.Mana )
						toDrain = m.Mana;

					if ( m_Table.ContainsKey( m ) )
						toDrain = 0;

					m.FixedParticles( 0x3789, 10, 25, 5032, EffectLayer.Head );
					m.PlaySound( 0x1F8 );

					if ( toDrain > 0 )
					{
						m.Mana -= toDrain;

						m_Table[m] = Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), new TimerStateCallback( AosDelay_Callback ), new object[]{ m, toDrain } );
					}
				}
				else
				{
					if ( CheckResisted( m ) )
						m.SendMessage("Voce sente seu corpo resistindo a magia"); // You feel yourself resisting magical energy.
					else if ( m.Mana >= 100 )
						m.Mana -= Utility.Random( 1, 100 );
					else
						m.Mana -= Utility.Random( 1, m.Mana );

					m.FixedParticles( 0x374A, 10, 15, 5032, EffectLayer.Head );
					m.PlaySound( 0x1F8 );
				}
			}

			FinishSequence();
		}

		public override double GetResistPercent( Mobile target )
		{
			return 99.0;
		}

        private class InternalSphereTarget : Target
        {
            private ManaDrainSpell m_Owner;

            public InternalSphereTarget(ManaDrainSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
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

		private class InternalTarget : Target
		{
			private ManaDrainSpell m_Owner;

			public InternalTarget( ManaDrainSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}