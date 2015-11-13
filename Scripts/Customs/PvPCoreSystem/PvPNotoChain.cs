using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Regions;

namespace Server
{
   public sealed class PvPNotoChain : NotorietyHandlerChain
    {
        public override int HandleNotoriety(Server.Mobile source, Server.Mobile target)
        {
			if(source is PlayerMobile && target is PlayerMobile)
			{
				if(((PlayerMobile)source).Region is PvPRegion && ((PlayerMobile)target).Region is PvPRegion)
					return Notoriety.Enemy;
				if(((PlayerMobile)source).Region is PvPStagingRegion && ((PlayerMobile)target).Region is PvPRegion)
					return Notoriety.Invulnerable;
				if(((PlayerMobile)source).Region is PvPRegion && ((PlayerMobile)target).Region is PvPStagingRegion)
					return Notoriety.Invulnerable;					
			}
			
            return base.HandleNotoriety(source, target);
        }

        public override bool AllowBeneficial(Server.Mobile source, Server.Mobile target)
        {
			if(source is PlayerMobile && target is PlayerMobile)
			{
				if(source == target)
					return true;
				if(((PlayerMobile)source).Region is PvPRegion && ((PlayerMobile)target).Region is PvPRegion)
					return false;
				if(((PlayerMobile)source).Region is PvPStagingRegion && ((PlayerMobile)target).Region is PvPRegion)
					return false;
				if(((PlayerMobile)source).Region is PvPRegion && ((PlayerMobile)target).Region is PvPStagingRegion)
					return false;					
			}            
			
			return base.AllowBeneficial(source, target);
        }

        public override bool AllowHarmful(Server.Mobile source, Server.Mobile target)
        {
			if(source is PlayerMobile && target is PlayerMobile)
			{
				if(source == target)
					return false;
				if(((PlayerMobile)source).Region is PvPRegion && ((PlayerMobile)target).Region is PvPRegion)
					return true;
				if(((PlayerMobile)source).Region is PvPStagingRegion && ((PlayerMobile)target).Region is PvPRegion)
					return false;
				if(((PlayerMobile)source).Region is PvPRegion && ((PlayerMobile)target).Region is PvPStagingRegion)
					return false;					
			}
			
            return base.AllowHarmful(source, target);
        }
    }
	
   public abstract class PvPNotoLoader
    {
		public static void Initialize()
		{
			new PvPNotoChain();
		}
	}	
}
