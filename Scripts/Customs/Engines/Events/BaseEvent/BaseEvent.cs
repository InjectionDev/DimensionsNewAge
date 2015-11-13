using System;
using Server;
using Server.Commands;
using Server.Mobiles;
using Server.Regions;
using Server.Gumps;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace DimensionsNewAge.Scripts.Customs.Engines
{
    public class BaseEvent
    {

        public virtual bool IsAcceptingPlayers { get; set; }

        public BaseEvent()
        { 
            
        }

    }
}
