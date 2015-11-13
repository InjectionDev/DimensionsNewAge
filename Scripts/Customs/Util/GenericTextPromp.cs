using System;
using Server;
using Server.Guilds;
using Server.Prompts;
using Server.Mobiles;

namespace Server
{
    public class GenericTextPromp : Prompt
    {

        private PlayerMobile player;

        public GenericTextPromp(PlayerMobile pPlayer)
        {
            player = pPlayer;
        }


        public override void OnResponse(Mobile from, string text)
        {

            text = text.Trim();
            player.LastGenericTextPromp = text;

        }
    }
}