using System;
using Server;
using Server.Commands;
using Server.Mobiles;
using Server.Regions;
using Server.Gumps;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DimensionsNewAge.Scripts.Customs.Engines;

namespace DimensionsNewAge.Scripts.Customs.Engines
{
    public static class BaseEventHelper
    {

        //public virtual bool AllowMaleWearer { get ;set; }


        public static bool GoEvent(Mobile caller)
        {
            if (SingletonEvent.Instance.IsEventRunning)
            {
                caller.MoveToWorld(SingletonEvent.Instance.CurrentEventLocation, SingletonEvent.Instance.CurrentEventMap);
                return true;
            }
            else
            {
                caller.SendMessage("Nao existe Evento em andamento.");
                return false;
            }
        }

        public static void GoEvent(Mobile caller, EnumEventBase.EnumEventType pEvent)
        {
            switch ((int)pEvent)
            { 
                case 0:
                    caller.MoveToWorld(new Point3D(47, 836, -28), Map.Ilshenar);
                    break;

                default:
                    break;
            }
        }





    }
}
