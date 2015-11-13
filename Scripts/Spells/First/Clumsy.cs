using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.First
{
	public class ClumsySpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Clumsy", "Uus Jux",
				212,
				9031,
				Reagent.Bloodmoss,
				Reagent.Nightshade
			);

        // EFFECT=4,18
        public override double CastDelayFastScalar { get { return 0; } }
        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(0.5); } }
        public override bool ClearHandsOnCast { get { return false; } }
        public override int ScaleMana(int mana) { return 4; }

		public override SpellCircle Circle { get { return SpellCircle.First; } }

		public ClumsySpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

        public override void SelectTarget()
        {            
            Caster.Target = new InternalSphereTarget(this);
        }

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
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

		public void Target( Mobile m )
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
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( (int)this.Circle, Caster, ref m );

				SpellHelper.AddStatCurse( Caster, m, StatType.Dex );

				if ( m.Spell != null )
					m.Spell.OnCasterHurt();

				m.Paralyzed = false;

				m.FixedParticles( 0x3779, 10, 15, 5002, EffectLayer.Head );
				m.PlaySound( 0x1DF );

				int percentage = (int)(SpellHelper.GetOffsetScalar( Caster, m, true )*100);
				TimeSpan length = SpellHelper.GetDuration( Caster, m );

				BuffInfo.AddBuff( m, new BuffInfo( BuffIcon.Clumsy, 1075831, length, m, percentage.ToString() ) );
			}

			FinishSequence();
		}


        private class InternalSphereTarget : Target
        {
            private ClumsySpell m_Owner;

            public InternalSphereTarget(ClumsySpell owner)
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
			private ClumsySpell m_Owner;

			public InternalTarget( ClumsySpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}