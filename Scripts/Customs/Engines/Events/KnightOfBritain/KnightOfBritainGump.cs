using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using System.Collections.Generic;
using System.Linq;
using DimensionsNewAge.Scripts.Customs.Engines;

namespace Server.Items
{

    public class KnightOfBritainGump : Gump
    {
        Mobile caller;



        public KnightOfBritainGump(Mobile from)
            : this()
        {
            caller = from;

            this.InitializeGump();
        }

        public KnightOfBritainGump()
            : base(200, 100)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
        }

        private void InitializeGump()
        {

            AddPage(0);
            AddBackground(178, 8, 423, 586, 9270);
            AddImage(126, 4, 10400);
            AddLabel(276, 43, 1259, @"K N I G H T   O F   B R I T A I N");
            //AddLabel(457, 134, 1973, @"1200");
            //AddLabel(457, 164, 1973, @"1000");
            //AddLabel(457, 194, 1973, @"900");
            AddLabel(232, 285, 545, @"");
            AddLabel(415, 205, 545, @"");
            AddImage(339, 74, 96, 1259);
            AddImage(277, 74, 96, 1259);
            AddImage(205, 26, 111);
            AddImage(515, 26, 111);
            AddLabel(234, 134, 1973, @"");
            AddLabel(208, 104, 37, @"Ranking Atual");
            //AddLabel(218, 134, 1973, @"Dev");
            //AddLabel(218, 164, 1973, @"Joao");
            //AddLabel(218, 194, 1973, @"Teste");
            AddLabel(447, 104, 37, @"Pontos");
            AddLabel(507, 104, 37, @"Atividade");
            AddLabel(257, 507, 37, @"Knight of Britain  ( Ultima Temporada )");
            //AddLabel(277, 536, 1259, @"Dev");
            AddImage(207, 499, 9004);

            List<PlayerMobile> playerList = new List<PlayerMobile>();
            foreach (Mobile mobile in World.Mobiles.Values)
            {
                if (mobile is PlayerMobile && (((PlayerMobile)mobile).KnightOfBritainKills > 0 || ((PlayerMobile)mobile).KnightOfBritainPoints > 0))
                    playerList.Add((PlayerMobile)mobile);
            }

            int currentHeight = 134;
            int count = 0;
            playerList = playerList.OrderByDescending(x => x.KnightOfBritainKills).ThenByDescending(x => x.KnightOfBritainPoints).ToList();
            foreach (PlayerMobile player in playerList)
            {
                if (count >= 10)
                    break;

                AddLabel(208, currentHeight, 1973, player.RawName);
                AddLabel(447, currentHeight, 1973, player.KnightOfBritainKills.ToString());
                AddLabel(507, currentHeight, 1973, player.KnightOfBritainPoints.ToString());
                currentHeight += 30;
                count++;
            }

            PlayerMobile currentKnight = playerList.FirstOrDefault();
            if (currentKnight != default(PlayerMobile))
                AddLabel(260, 536, 1259, currentKnight.RawName);
        }


    }
}