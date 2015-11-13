using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Commands;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "taskmaster corpse" )]
	public class QuestGiver : Mobile
	{
        public virtual bool IsInvulnerable{ get{ return true; } }

		[Constructable]
		public QuestGiver()
		{
			InitStats(31, 41, 51);

			Hue = Utility.RandomSkinHue(); 
			Body = 0x190;
			Blessed = true;

			AddItem( new Robe(2526) );
			AddItem( new Boots(2526) );
			Utility.AssignRandomHair( this );
			Direction = Direction.South;
			Name = NameList.RandomName( "male" ); 
			Title = "O Aventureiro"; 
			CantWalk = true;
		}

		public QuestGiver( Serial serial ) : base( serial )
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
        { 
            base.GetContextMenuEntries( from, list ); 
	        list.Add( new QuestGiverEntry( from, this ) ); 
        } 

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.HasGump(typeof(QuestGiver_gump)))
            {
                from.SendGump(new QuestGiver_gump(from));
            } 

            //base.OnDoubleClick(from);
        }

		public class QuestGiverEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public QuestGiverEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
                if( !( m_Mobile is PlayerMobile ) )
					return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;

				if ( ! mobile.HasGump( typeof( QuestGiver_gump ) ) )
				{
					mobile.SendGump( new QuestGiver_gump( mobile ));
				} 

			}
		}

		private static void GetRandomAOSStats( out int attributeCount, out int min, out int max, int level )
		{
			int rnd = Utility.Random( 15 );

			if ( level == 6 )
			{
				attributeCount = Utility.RandomMinMax( 2, 6 );
				min = 20; max = 70;
			}
			else if ( level == 5 )
			{
				attributeCount = Utility.RandomMinMax( 2, 4 );
				min = 20; max = 50;
			}
			else if ( level == 4 )
			{
				attributeCount = Utility.RandomMinMax( 2, 3 );
				min = 20; max = 40;
			}
			else if ( level == 3 )
			{
				attributeCount = Utility.RandomMinMax( 1, 3 );
				min = 10; max = 30;
			}
			else if ( level == 2 )
			{
				attributeCount = Utility.RandomMinMax( 1, 2 );
				min = 10; max = 30;
			}
			else
			{
				attributeCount = 1;
				min = 10; max = 20;
			}
		}

		public override bool OnDragDrop(Mobile from, Item dropped)
		{
			Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;

			if ( mobile != null)
			{
                if (dropped is Gold && dropped.Amount == 100)
         			{
					    mobile.AddToBackpack ( new QuestScroll(1) );
					    this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Me devolva este Scroll quando terminar...para receber sua recompensa..", mobile.NetState );
         				return true;
         			}
                    else if (dropped is Gold && dropped.Amount == 200)
         			{
					    mobile.AddToBackpack ( new QuestScroll(2) );
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Me devolva este Scroll quando terminar...para receber sua recompensa..", mobile.NetState);
         				return true;
         			}
                else if (dropped is Gold && dropped.Amount == 300)
         			{
					    mobile.AddToBackpack ( new QuestScroll(3) );
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Me devolva este Scroll quando terminar...para receber sua recompensa..", mobile.NetState);
         				return true;
         			}
                else if (dropped is Gold && dropped.Amount == 400)
         			{
					    mobile.AddToBackpack ( new QuestScroll(4) );
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Me devolva este Scroll quando terminar...para receber sua recompensa..", mobile.NetState);
         				return true;
         			}
                else if (dropped is Gold && dropped.Amount == 500)
         			{
					    mobile.AddToBackpack ( new QuestScroll(5) );
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Me devolva este Scroll quando terminar...para receber sua recompensa..", mobile.NetState);
         				return true;
         			}
                else if (dropped is Gold && dropped.Amount == 600)
         			{
					    mobile.AddToBackpack ( new QuestScroll(6) );
                        this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Me devolva este Scroll quando terminar...para receber sua recompensa..", mobile.NetState);
         				return true;
         			}
         			else if(dropped is Gold)
         			{
					    this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Nao tenho nada para voce por esta quantia de gold.", mobile.NetState );
         				return false;
         			}
				    else if( dropped is QuestScroll )
         			{
					    QuestScroll m_Quest = (QuestScroll)dropped;

         				if(m_Quest.NNeed > m_Quest.NGot)
         				{
						    mobile.AddToBackpack ( dropped );
						    this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Voce nao completou essa quest.", mobile.NetState );
         					return false;
         				}

						string sMessage = "";
                        if (m_Quest.NType == 1) { sMessage = "Vejo que voce voltou vitorioso. Aqui esta sua recompensa!"; }
						else { sMessage = "Ahh... voce encontrou " + m_Quest.NItemName + "! Aqui esta sua recompensa!"; }

                        Bag bagReward = new Bag();

                        // Skill Reward
                        int randomSkillType = 1;
                        if (Utility.RandomDouble() >= 0.8)
                            randomSkillType = (int)SkillRewardItemType.Tammer;
                        else
                            randomSkillType = Utility.Random(1, 3);

                        int randomSkillAmount = 1;
                        randomSkillAmount = m_Quest.NLevel;
                        if (randomSkillAmount > 5)
                            randomSkillAmount = 5;

                        bagReward.DropItem(new SkillRewardItem(1, randomSkillType, randomSkillAmount));
                        //mobile.AddToBackpack(new SkillRewardItem(1, randomSkillType, randomSkillAmount));
                        // Skill Reward


					    if ( Utility.RandomMinMax( 1, 4 ) == 1 )
					    {
                            bagReward.DropItem(new Gold(m_Quest.NLevel * 500));
						    //mobile.AddToBackpack ( new Gold( m_Quest.NLevel * 500 ) );
					    }
					    else
					    {
                            bagReward.DropItem(new Gold(m_Quest.NLevel * 400));
						    //mobile.AddToBackpack ( new Gold( m_Quest.NLevel * 400 ) );
    	
						    Item item;

						    if ( Core.AOS )
							    item = Loot.RandomArmorOrShieldOrWeaponOrJewelry();
						    else
							    item = Loot.RandomArmorOrShieldOrWeapon();

                            bagReward.DropItem(item);
                            //mobile.AddToBackpack(item);

                            //if ( item is BaseWeapon )
                            //{
                            //    BaseWeapon weapon = (BaseWeapon)item;
    	
                            //    if ( Core.AOS )
                            //    {
                            //        int attributeCount;
                            //        int min, max;

                            //        GetRandomAOSStats( out attributeCount, out min, out max, m_Quest.NLevel );
    	
                            //        BaseRunicTool.ApplyAttributesTo( weapon, attributeCount, min, max );
                            //    }
                            //    else
                            //    {
                            //        weapon.DamageLevel = (WeaponDamageLevel)Utility.Random( 6 );
                            //        weapon.AccuracyLevel = (WeaponAccuracyLevel)Utility.Random( 6 );
                            //        weapon.DurabilityLevel = (WeaponDurabilityLevel)Utility.Random( 6 );
                            //    }

                            //    mobile.AddToBackpack ( item );
                            //}
                            //else if ( item is BaseArmor )
                            //{
                            //    BaseArmor armor = (BaseArmor)item;

                            //    if ( Core.AOS )
                            //    {
                            //        int attributeCount;
                            //        int min, max;
    	
                            //        GetRandomAOSStats( out attributeCount, out min, out max, m_Quest.NLevel );

                            //        BaseRunicTool.ApplyAttributesTo( armor, attributeCount, min, max );
                            //    }
                            //    else
                            //    {
                            //        armor.ProtectionLevel = (ArmorProtectionLevel)Utility.Random( 6 );
                            //        armor.Durability = (ArmorDurabilityLevel)Utility.Random( 6 );
                            //    }

                            //    mobile.AddToBackpack ( item );
                            //}
                            //else if( item is BaseHat )
                            //{
                            //    BaseHat hat = (BaseHat)item;
    	
                            //    if( Core.AOS )
                            //    {
                            //        int attributeCount;
                            //        int min, max;

                            //        GetRandomAOSStats( out attributeCount, out min, out  max, m_Quest.NLevel );
    	
                            //        BaseRunicTool.ApplyAttributesTo( hat, attributeCount, min, max );
                            //    }

                            //    mobile.AddToBackpack ( item );
                            //}
                            //else if( item is BaseJewel )
                            //{
                            //    int attributeCount;
                            //    int min, max;

                            //    GetRandomAOSStats( out attributeCount, out min, out max, m_Quest.NLevel );

                            //    BaseRunicTool.ApplyAttributesTo( (BaseJewel)item, attributeCount, min, max );

                            //    mobile.AddToBackpack ( item );
                            //}
					    }

                        mobile.AddToBackpack(bagReward);

                    	this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, mobile.NetState);

						dropped.Delete();
				
						return true;
				}
				else
				{
					mobile.AddToBackpack ( dropped );
					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I have no need for this...", mobile.NetState); return true;
				}

			}

            return false;
		}
	}
}

namespace Server.Gumps
{
	public class QuestGiver_gump : Gump 
	{
		public static void Initialize()
		{ 
			CommandSystem.Register( "QuestGiver_gump", AccessLevel.GameMaster, new CommandEventHandler( QuestGiver_gump_OnCommand ) ); 
		}

		private static void QuestGiver_gump_OnCommand( CommandEventArgs e ) 
		{
			e.Mobile.SendGump( new QuestGiver_gump( e.Mobile ) );
		}
 
		public QuestGiver_gump( Mobile owner ) : base( 50,50 ) 
		{
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            string questText = @"Olá guerreiro. Se alguma coisa precisa ser feita nesta cidade, eu sou o primeiro a saber... Embora eu não deva contratar qualquer cidadão, parece que você pode cuidar de si mesmo. Sabe, eu poderia ter problemas se eles descobrirem que eu deixei falhar uma missão importante.<BR><BR>";
            questText += @"Faça o seguinte, me adiante uma quantia em gold, e eu lhe darei uma missão que precisa ser feita.<BR><BR>";
            questText += @"100 Gold - Quest Nível 1<BR>200 Gold - Quest Nível 2<BR>300 Gold - Quest Nível 3<BR>400 Ouro - Quest Nível 4<BR>500 Ouro - Quest Nível 5<BR>600 Ouro - Quest Nível 6<BR><BR>";
            questText += @"Voce deverá achar algum item perdido, ou eliminar alguns inimigos. Basta seguir as instruções no Scroll. Clique nele 2x para ganhar os  créditos por um monstro morto, ou entao para procurar algum item  perdido em seu loot.<BR>";

            AddPage(0);
            AddBackground(177, 10, 423, 581, 9270);
            AddLabel(303, 60, 42, @"D I M E N S I O N S");
            AddLabel(347, 80, 141, @"New Age");
            AddImageTiled(109, -49, 198, 181, 50992);
            AddImageTiled(398, -50, 198, 181, 50993);
            AddImage(126, 4, 10400);
            AddLabel(207, 137, 37, @"O Aventureiro");
            AddHtml(202, 168, 373, 397, questText, true, true);

		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			switch ( info.ButtonID )
			{
				case 0:{ break; }
			}
		}
	}
}