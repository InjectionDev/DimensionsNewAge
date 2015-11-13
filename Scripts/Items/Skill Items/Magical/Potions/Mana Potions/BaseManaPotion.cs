using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseManaPotion : BasePotion
	{
		public abstract int MinMana { get; }
        public abstract int MaxMana { get; }
		public abstract double Delay { get; }


        public BaseManaPotion(PotionEffect effect)
            : base(0x0EFB, effect)
		{
            
		}

        public BaseManaPotion(Serial serial)
            : base(serial)
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

		public void DoMana( Mobile from )
		{
            int min = Scale(from, MinMana);
            int max = Scale(from, MaxMana);

            int qtMana = Utility.RandomMinMax(min, max);
            from.Mana += qtMana;

            from.SendAsciiMessage(5, string.Format("+{0} Mana", qtMana));

            if (from.Mana > from.ManaMax)
                from.Mana = from.ManaMax;

		}

		public override void Drink( Mobile from )
		{
            if (from.Mana < from.ManaMax)
			{
                if (MortalStrike.IsWounded(from)) // if (from.Poisoned || MortalStrike.IsWounded(from))
                {
                    from.LocalOverheadMessage(MessageType.Regular, 0x22, 1005000); // You can not heal yourself in your current state.
                }
                else
                {
                    if (from.BeginAction(typeof(BaseHealPotion)))
                    {
                        DoMana(from);

                        BasePotion.PlayDrinkEffect(from);

                        this.Consume(); // this.Consume();

                        Timer.DelayCall(TimeSpan.FromSeconds(Delay), new TimerStateCallback(ReleaseManaLock), from);
                    }
                    else
                    {
                        from.SendMessage(0x22, "Voce nao pode usar outra potion em tao pouco tempo");
                        //from.LocalOverheadMessage(MessageType.Regular, 0x22, 500235); // You must wait 10 seconds before using another healing potion.
                    }
                }
			}
			else
			{
                from.SendMessage(0x22, "Voce decide nao usar a potion, pois esta com Mana cheia");
				//from.SendLocalizedMessage( 1049547 ); // You decide against drinking this potion, as you are already at full health.
			}
		}

        private static void ReleaseManaLock(object state)
		{
			((Mobile)state).EndAction( typeof( BaseHealPotion ) );
		}
	}
}