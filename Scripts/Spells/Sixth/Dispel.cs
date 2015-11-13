using System;
using Server.Misc;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.Sixth
{
	public class DispelSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Dispel", "An Ort",
				218,
				9002,
				Reagent.Garlic,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
			);

        // EFFECT=
        public override double CastDelayFastScalar { get { return 0; } }
        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(1.7); } }
        public override bool ClearHandsOnCast { get { return false; } }
        public override int ScaleMana(int mana) { return 20; }

		public override SpellCircle Circle { get { return SpellCircle.Sixth; } }


        public override void SelectTarget()
        {
            Caster.Target = new InternalSphereTarget(this);
        }

        public override void OnSphereCast()
        {
            if (SpellTarget != null)
            {
                if (SpellTarget is IPoint3D)
                {
                    Target((IPoint3D) SpellTarget);
                }
                else
                {
                    Caster.SendAsciiMessage("Alvo invalido");
                }
            }
            FinishSequence();
        }

	    public DispelSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

        public void Target(IPoint3D o)
        {
            if (!Caster.CanSee(o))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (!CheckLineOfSight(o))
            {
                this.DoFizzle();
                Caster.SendAsciiMessage("Target is not in line of sight");
            }
            else if (o is Mobile)
            {
                Mobile m = (Mobile)o;
                BaseCreature bc = m as BaseCreature;
                Mobile from = Caster;

                if (!from.CanSee(m))
                {
                    from.SendLocalizedMessage(500237); // Target can not be seen.
                }
                else if (bc == null || !bc.IsDispellable)
                {
                    from.SendLocalizedMessage(1005049); // That cannot be dispelled.
                }
                else if (CheckHSequence(m))
                {
                    SpellHelper.Turn(from, m);

                    double dispelChance = (50.0 + ((100 * (from.Skills.Magery.Value - bc.DispelDifficulty)) / (bc.DispelFocus * 2))) / 100;

                    if (dispelChance > Utility.RandomDouble())
                    {
                        Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                        Effects.PlaySound(m, m.Map, 0x201);

                        m.Delete();
                    }
                    else
                    {
                        m.FixedEffect(0x3779, 10, 20);
                        from.SendLocalizedMessage(1010084); // The creature resisted the attempt to dispel it!
                    }
                }
            }
        }

	    public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

        private class InternalSphereTarget : Target
        {
            private DispelSpell m_Owner;

            public InternalSphereTarget(DispelSpell owner)
                : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
                m_Owner.Caster.SendAsciiMessage("Selecione o alvo...");
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is IPoint3D)
                {
                    m_Owner.SpellTarget = o;
                    m_Owner.CastSpell();
                }
                else
                {
                    m_Owner.Caster.SendAsciiMessage("Alvo invalido");
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

		public class InternalTarget : Target
		{
			private DispelSpell m_Owner;

			public InternalTarget( DispelSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					Mobile m = (Mobile)o;
					BaseCreature bc = m as BaseCreature;

					if ( !from.CanSee( m ) )
					{
						from.SendLocalizedMessage( 500237 ); // Target can not be seen.
					}
					else if ( bc == null || !bc.IsDispellable )
					{
						from.SendLocalizedMessage( 1005049 ); // That cannot be dispelled.
					}
					else if ( m_Owner.CheckHSequence( m ) )
					{
						SpellHelper.Turn( from, m );

						double dispelChance = (50.0 + ((100 * (from.Skills.Magery.Value - bc.DispelDifficulty)) / (bc.DispelFocus*2))) / 100;

						if ( dispelChance > Utility.RandomDouble() )
						{
							Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
							Effects.PlaySound( m, m.Map, 0x201 );

							m.Delete();
						}
						else
						{
							m.FixedEffect( 0x3779, 10, 20 );
							from.SendLocalizedMessage( 1010084 ); // The creature resisted the attempt to dispel it!
						}
					}
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}