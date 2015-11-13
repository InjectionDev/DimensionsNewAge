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
    public class BaseEventStatistics
    {
        List<PlayerKills> playerKillsList;

        public BaseEventStatistics()
        {
            this.playerKillsList = new List<PlayerKills>();
        }

        public List<PlayerKills> PlayerKillsList
        { 
            get { return this.playerKillsList; }
            set { this.playerKillsList = value; }
        }

        public void InsertKill(PlayerMobile player, Mobile killed)
        {
            foreach (PlayerKills playerKills in this.playerKillsList)
            {
                if (playerKills.player.Serial == player.Serial)
                {
                    playerKills.playerKillList.Add(killed);
                    return;
                }
            }

            // Se chegou aqui ainda nao tem na lista
            PlayerKills playerKill = new PlayerKills();
            playerKill.player = player;
            playerKill.playerKillList.Add(killed);

            this.playerKillsList.Add(playerKill);
        }

        public List<Mobile> GetPlayerKillList(PlayerMobile player)
        {
            foreach (PlayerKills playerKills in this.playerKillsList)
            {
                if (playerKills.player != null && playerKills.player.Serial == player.Serial)
                {
                    return playerKills.playerKillList;
                }
            }

            return new List<Mobile>();
        }

    }

    public class PlayerKills
    {
        public PlayerMobile player { get; set; }
        public List<Mobile> playerKillList { get; set; }

        public PlayerKills()
        {
            this.playerKillList = new List<Mobile>();
        }
    }
}
