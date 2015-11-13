using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Eighth
{
	public class FireElementalSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Fire Elemental", "Kal Vas Xen Flam",
				269,
				9050,
				false,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk,
				Reagent.SulfurousAsh
			);

        // EFFECT=16,20
        public override double CastDelayFastScalar { get { return 0; } }
        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(2.2); } }
        public override bool ClearHandsOnCast { get { return false; } }
        public override int ScaleMana(int mana) { return 50; }

		public override SpellCircle Circle { get { return SpellCircle.Eighth; } }

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
                    Target((IPoint3D)SpellTarget);
                }
                else
                {
                    Caster.SendAsciiMessage("Alvo invalido");
                }
            }
            FinishSequence();
        }

	    public FireElementalSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

        public void Target(IPoint3D p)
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
            else if (CheckSequence())
            {
            }

            FinishSequence();
        }

		public override bool CheckCast()
		{
			if ( !base.CheckCast() )
				return false;

			if ( (Caster.Followers + 4) > Caster.FollowersMax )
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
				TimeSpan duration = TimeSpan.FromSeconds( (2 * Caster.Skills.Magery.Fixed) / 5 );

				if ( Core.AOS )
					SpellHelper.Summon( new SummonedFireElemental(), Caster, 0x217, duration, false, false );
				else
					SpellHelper.Summon( new FireElemental(), Caster, 0x217, duration, false, false );
			}

			FinishSequence();
		}

        private class InternalSphereTarget : Target
        {
            private FireElementalSpell m_Owner;

            public InternalSphereTarget(FireElementalSpell owner)
                : base(8, true, TargetFlags.Harmful)
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
	}
}