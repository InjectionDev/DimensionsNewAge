using System;
using Server.Targeting;
using Server.Network;
using Server;

namespace Server.Spells.First
{
	public class NightSightSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Night Sight", "In Lor",
				236,
				9031,
				Reagent.SulfurousAsh,
				Reagent.SpidersSilk
			);

        // EFFECT=
        public override double CastDelayFastScalar { get { return 0; } }
        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(0.5); } }
        public override bool ClearHandsOnCast { get { return false; } }
        public override int ScaleMana(int mana) { return 4; }

		public override SpellCircle Circle { get { return SpellCircle.First; } }

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
            if (CheckBSequence(m))
            {
                Mobile targ = m;

                SpellHelper.Turn(Caster, targ);

                if (targ.BeginAction(typeof(LightCycle)))
                {
                    new LightCycle.NightSightTimer(targ).Start();
                    int level = (int)(LightCycle.DungeonLevel * ((Core.AOS ? targ.Skills[SkillName.Magery].Value : Caster.Skills[SkillName.Magery].Value) / 100));

                    if (level < 0)
                        level = 0;

                    targ.LightLevel = level;

                    targ.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
                    targ.PlaySound(0x1E3);

                    BuffInfo.AddBuff(targ, new BuffInfo(BuffIcon.NightSight, 1075643));	//Night Sight/You ignore lighting effects
                }
                else
                {
                    Caster.SendMessage("{0} already have nightsight.", Caster == targ ? "You" : "They");
                }
            }

            FinishSequence();
        }

	    public NightSightSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new NightSightTarget( this );
		}

        private class InternalSphereTarget : Target
        {
            private NightSightSpell m_Owner;

            public InternalSphereTarget(NightSightSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Beneficial)
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

		private class NightSightTarget : Target
		{
			private Spell m_Spell;

			public NightSightTarget( Spell spell ) : base( 12, false, TargetFlags.Beneficial )
			{
				m_Spell = spell;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Mobile && m_Spell.CheckBSequence( (Mobile) targeted ) )
				{
					Mobile targ = (Mobile)targeted;

					SpellHelper.Turn( m_Spell.Caster, targ );

					if ( targ.BeginAction( typeof( LightCycle ) ) )
					{
						new LightCycle.NightSightTimer( targ ).Start();
						int level = (int)( LightCycle.DungeonLevel * ( (Core.AOS ? targ.Skills[SkillName.Magery].Value : from.Skills[SkillName.Magery].Value )/ 100 ) );

						if ( level < 0 )
							level = 0;

						targ.LightLevel = level;

						targ.FixedParticles( 0x376A, 9, 32, 5007, EffectLayer.Waist );
						targ.PlaySound( 0x1E3 );

						BuffInfo.AddBuff( targ, new BuffInfo( BuffIcon.NightSight, 1075643 ) );	//Night Sight/You ignore lighting effects
					}
					else
					{
						from.SendMessage( "{0} already have nightsight.", from == targ ? "You" : "They" );
					}
				}

				m_Spell.FinishSequence();
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Spell.FinishSequence();
			}
		}
	}
}
