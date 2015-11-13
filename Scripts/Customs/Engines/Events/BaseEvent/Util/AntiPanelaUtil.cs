using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Factions;
using Server.Commands;
using System.Collections.Generic;

using Server.Mobiles;
using Server.Regions;
using Server.Gumps;
using Server.Items;

namespace DimensionsNewAge.Scripts.Customs.Engines.Events.BaseEvent.Util
{
    public class AntiPanelaUtil
    {

        public static string EventRandomName
        {
            get { return m_EventRandomNameList[Utility.Random(m_EventRandomNameList.Length)]; }
        }

        private static string[] m_EventRandomNameList = new string[]
        {
            "Pipoqueiro",
            "Lixeiro",
            "Padeiro",
            "Coveiro",
            "Barbeiro",
            "Acougueiro",
            "Flanelinha",
            "Cafetao",
            "Gandula",
            "Equilibrista",
            "Malabarista",
            "Contorcionista",
            "Garcon",
            "Osama Bin Laden",
            "Saddan Hussein",
            "Coltrane",
            "Habdud Hallah",
            "Ayaman",
            "Beira Mar",
            "Supremo Allah",
            "Elias Maluco",
            "Escadinha",
            "Mussum",
            "Didi",
            "Zacarias",
            "Dede",
            "Seyss",
            "Ozzy",
            "Pr0t0n"
        };
    }
}
