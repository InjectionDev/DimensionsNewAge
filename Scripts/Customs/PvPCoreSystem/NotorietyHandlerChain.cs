using System;
using Server;
using Server.Mobiles;
using System.Collections;
using System.Collections.Generic;
using Server.Network;

namespace Server
{
   public abstract class NotorietyHandlerChain
    {
        private readonly AllowBeneficialHandler _allowBeneficialHandlerSuccessor;
        private readonly AllowHarmfulHandler _allowHarmfulHandlerSuccessor;
        private readonly NotorietyHandler _notorietyHandlerSuccessor;


        protected NotorietyHandlerChain()
        {
            // Store the original handlers in some read only private variables 
            // so we can call them later if need be.
            _notorietyHandlerSuccessor = Notoriety.Handler;
            _allowBeneficialHandlerSuccessor = Mobile.AllowBeneficialHandler;
            _allowHarmfulHandlerSuccessor = Mobile.AllowHarmfulHandler;

            // Set the current handlers to our new chain handlers.
            Notoriety.Handler = HandleNotoriety;
            Mobile.AllowBeneficialHandler = AllowBeneficial;
            Mobile.AllowHarmfulHandler = AllowHarmful;
        }

        protected bool InvokeAllowBeneficialHandlerSuccessor(Mobile source, Mobile target)
        {
            return _allowBeneficialHandlerSuccessor(source, target);
        }

        protected bool InvokeAllowHarmfulHandlerSuccessor(Mobile source, Mobile target)
        {
            return _allowHarmfulHandlerSuccessor(source, target);
        }

        protected int InvokeNotorietyHandlerSuccessor(Mobile source, Mobile target)
        {
            return _notorietyHandlerSuccessor(source, target);
        }

        public virtual bool AllowBeneficial(Mobile source, Mobile target)
        {
            // Call the successor since there is no implementation in this base class.
            return InvokeAllowBeneficialHandlerSuccessor(source, target);
        }

        public virtual bool AllowHarmful(Mobile source, Mobile target)
        {
            // Call the successor since there is no implementation in this base class.
            return InvokeAllowHarmfulHandlerSuccessor(source, target);
        }

        public virtual int HandleNotoriety(Mobile source, Mobile target)
        {
            // Call the successor since there is no implementation in this base class.
            return InvokeNotorietyHandlerSuccessor(source, target);
        }
    }
}
