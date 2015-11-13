using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

using DimensionsNewAge.Scripts.Customs.Engines;

namespace Server.Items
{

    public class PublicMoongateGump : Gump
    {
        Mobile caller;


        public PublicMoongateGump(Mobile from)
            : this()
        {
            caller = from;
            this.InitializeGump();
        }

        public PublicMoongateGump()
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
            AddBackground(59, 13, 737, 583, 9270);
            AddLabel(90, 43, 42, @"D I M E N S");
            AddLabel(105, 63, 141, @"New Age");
            AddImage(8, 7, 10400);
            AddLabel(78, 111, 37, @"Mundos");

            // Radio
            AddLabel(113, 140, 545, @"Felluca");
            AddButton(87, 140, 2117, 2118, 1, GumpButtonType.Reply, 0);
            AddLabel(113, 165, 545, @"Illshenar");
            AddButton(87, 165, 2117, 2118, 2, GumpButtonType.Reply, 0);
            AddLabel(113, 190, 545, @"Malas");
            AddButton(87, 190, 2117, 2118, 3, GumpButtonType.Reply, 0);
            //AddLabel(113, 215, 545, @"Tokuno off");
            //AddRadio(87, 215, 210, 211, false, 0);
            //AddLabel(113, 240, 545, @"Star Room");
            //AddRadio(87, 240, 210, 211, false, 10);

            int HueBlueCity = 4; // 1790;
            int HueRedCity = 38; // 1645;

            switch(currentWorldOption)
            {

                case 1:
                    {
                        // Felluca
                        AddImage(221, 27, 65000);
                        AddButton(485, 75, 2103, 2104, 101, GumpButtonType.Reply, 0);
                        AddLabel(500, 70, HueBlueCity, @"Minoc");
                        AddButton(542, 105, 2103, 2104, 102, GumpButtonType.Reply, 0);
                        AddLabel(557, 100, HueBlueCity, @"Vesper");
                        AddButton(684, 96, 2103, 2104, 103, GumpButtonType.Reply, 0);
                        AddLabel(699, 91, HueRedCity, @"Asgard");
                        AddButton(735, 65, 2103, 2104, 104, GumpButtonType.Reply, 0);
                        AddLabel(750, 60, HueBlueCity, @"Spital");
                        AddButton(719, 172, 2103, 2104, 105, GumpButtonType.Reply, 0);
                        AddLabel(734, 167, HueBlueCity, @"Moonglow");
                        AddButton(626, 164, 2103, 2104, 106, GumpButtonType.Reply, 0);
                        AddLabel(641, 159, HueBlueCity, @"Nujel'm");
                        AddButton(243, 53, 2103, 2104, 107, GumpButtonType.Reply, 0);
                        AddLabel(258, 48, HueBlueCity, @"Excalabria");
                        AddButton(461, 157, 2103, 2104, 108, GumpButtonType.Reply, 0);
                        AddLabel(476, 152, HueBlueCity, @"Cove");
                        AddButton(366, 221, 2103, 2104, 109, GumpButtonType.Reply, 0);
                        AddLabel(381, 216, HueBlueCity, @"Britain");
                        AddButton(425, 346, 2103, 2104, 110, GumpButtonType.Reply, 0);
                        AddLabel(440, 341, HueBlueCity, @"Trinsic");
                        AddButton(368, 451, 2103, 2104, 111, GumpButtonType.Reply, 0);
                        AddLabel(383, 446, HueBlueCity, @"Jhelon");
                        AddButton(301, 268, 2103, 2104, 112, GumpButtonType.Reply, 0);
                        AddLabel(316, 263, HueBlueCity, @"Skara Brae");
                        AddButton(287, 134, 2103, 2104, 113, GumpButtonType.Reply, 0);
                        AddLabel(302, 129, HueBlueCity, @"Yew");
                        AddButton(718, 245, 2103, 2104, 114, GumpButtonType.Reply, 0);
                        AddLabel(733, 240, HueBlueCity, @"Warclau");
                        AddButton(623, 259, 2103, 2104, 115, GumpButtonType.Reply, 0);
                        AddLabel(638, 254, HueBlueCity, @"Magincia");
                        AddButton(621, 303, 2103, 2104, 116, GumpButtonType.Reply, 0);
                        AddLabel(636, 298, HueRedCity, @"Occlo");
                        AddButton(521, 262, 2103, 2104, 117, GumpButtonType.Reply, 0);
                        AddLabel(536, 257, HueRedCity, @"Buc's Den");
                        AddButton(567, 348, 2103, 2104, 118, GumpButtonType.Reply, 0);
                        AddLabel(582, 343, HueBlueCity, @"Danube");
                        AddButton(659, 384, 2103, 2104, 119, GumpButtonType.Reply, 0);
                        AddLabel(674, 379, HueRedCity, @"Tenebrae");
                        AddButton(548, 409, 2103, 2104, 120, GumpButtonType.Reply, 0);
                        AddLabel(563, 404, HueRedCity, @"Serpents Hold");
                        AddButton(600, 455, 2103, 2104, 121, GumpButtonType.Reply, 0);
                        AddLabel(615, 450, HueRedCity, @"Tsarkon");
                        AddButton(249, 438, 2103, 2104, 122, GumpButtonType.Reply, 0);
                        AddLabel(264, 433, HueRedCity, @"Spandau");
                        AddButton(246, 325, 2103, 2104, 123, GumpButtonType.Reply, 0);
                        AddLabel(261, 320, HueBlueCity, @"Stow");

                        // Dungeons
                        int btnPos = 245;
                        int lblPos = 240;
                        int HueDung = 32;
                        AddLabel(78, lblPos, 37, @"Dungeons Felluca");
                        AddButton(87, btnPos += 30, 2103, 2104, 150, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 30, HueDung, @"Covetous");
                        AddButton(87, btnPos += 20, 2103, 2104, 151, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 20, HueDung, @"Deceit");
                        AddButton(87, btnPos += 20, 2103, 2104, 152, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 20, HueDung, @"Despice");
                        AddButton(87, btnPos += 20, 2103, 2104, 153, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 20, HueDung, @"Destard");
                        AddButton(87, btnPos += 20, 2103, 2104, 154, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 20, HueDung, @"Fire");
                        AddButton(87, btnPos += 20, 2103, 2104, 155, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 20, HueDung, @"Hythloth");
                        AddButton(87, btnPos += 20, 2103, 2104, 156, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 20, HueDung, @"Ice");
                        AddButton(87, btnPos += 20, 2103, 2104, 157, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 20, HueDung, @"Khaldun");
                        AddButton(87, btnPos += 20, 2103, 2104, 158, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 20, HueDung, @"Orc Cave");
                        AddButton(87, btnPos += 20, 2103, 2104, 159, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 20, HueDung, @"Shame");
                        AddButton(87, btnPos += 20, 2103, 2104, 160, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 20, HueDung, @"Wrong");


                        break;
                    }

                case 2:
                    {
                        // Illshenar
                        AddImage(218, 29, 65001);
                        AddButton(554, 134, 2103, 2104, 301, GumpButtonType.Reply, 0);
                        AddLabel(569, 129, HueRedCity, @"Compassion");
                        AddButton(400, 406, 2103, 2104, 302, GumpButtonType.Reply, 0);
                        AddLabel(415, 401, HueRedCity, @"Honesty");
                        AddButton(409, 212, 2103, 2104, 303, GumpButtonType.Reply, 0);
                        AddLabel(424, 207, HueRedCity, @"Honor");
                        AddButton(270, 302, 2103, 2104, 304, GumpButtonType.Reply, 0);
                        AddLabel(285, 297, HueRedCity, @"Humility");
                        AddButton(483, 300, 2103, 2104, 305, GumpButtonType.Reply, 0);
                        AddLabel(498, 295, HueRedCity, @"Justice");
                        AddButton(544, 382, 2103, 2104, 306, GumpButtonType.Reply, 0);
                        AddLabel(559, 377, HueRedCity, @"Sacrifice");
                        AddButton(652, 397, 2103, 2104, 307, GumpButtonType.Reply, 0);
                        AddLabel(667, 392, HueRedCity, @"Spirituality");
                        AddButton(348, 52, 2103, 2104, 308, GumpButtonType.Reply, 0);
                        AddLabel(363, 47, HueRedCity, @"Valor");
                        AddButton(705, 59, 2103, 2104, 309, GumpButtonType.Reply, 0);
                        AddLabel(720, 54, HueRedCity, @"Chaos");

                        // Dungeons
                        int btnPos = 245;
                        int lblPos = 240;
                        int HueDung = 32;
                        AddLabel(78, lblPos, 37, @"Dungeons Illshenar");
                        AddButton(87, btnPos += 30, 2103, 2104, 350, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 30, HueDung, @"Ankh");
                        AddButton(87, btnPos += 20, 2103, 2104, 351, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 20, HueDung, @"Blood");
                        AddButton(87, btnPos += 20, 2103, 2104, 352, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 20, HueDung, @"Exodus");
                        AddButton(87, btnPos += 20, 2103, 2104, 353, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 20, HueDung, @"Rock");
                        AddButton(87, btnPos += 20, 2103, 2104, 354, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 20, HueDung, @"Sorcerers");
                        AddButton(87, btnPos += 20, 2103, 2104, 355, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 20, HueDung, @"Spectre");
                        AddButton(87, btnPos += 20, 2103, 2104, 356, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 20, HueDung, @"Wisp");


                        break;
                    }

                case 3:
                    {
                        // Malas
                        AddImage(221, 25, 65002);
                        AddButton(350, 166, 2103, 2104, 401, GumpButtonType.Reply, 0);
                        AddLabel(365, 161, HueBlueCity, @"Luna");
                        AddButton(636, 393, 2103, 2104, 402, GumpButtonType.Reply, 0);
                        AddLabel(651, 388, HueRedCity, @"Umbra");

                        // Dungeons
                        int btnPos = 245;
                        int lblPos = 240;
                        int HueDung = 32;
                        AddLabel(78, lblPos, 37, @"Dungeons Malas");
                        AddButton(87, btnPos += 30, 2103, 2104, 450, GumpButtonType.Reply, 0);
                        AddLabel(102, lblPos += 30, HueDung, @"Doom");


                        break;


                    }


            }



        }

        private static int currentWorldOption = 1;

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;


            switch (info.ButtonID)
            {
                case 1: // Trammel
                    currentWorldOption = 1;
                    InitializeGump();
                    from.SendGump(this);
                    break;
                case 2: // IIshnar
                    currentWorldOption = 2;
                    InitializeGump();
                    from.SendGump(this);
                    break;
                case 3: // Malas
                    currentWorldOption = 3;
                    InitializeGump();
                    from.SendGump(this);
                    break;

                case 101: // Minoc
                    from.MoveToWorld(new Point3D(2701, 692, 5), Map.Felucca);
                    break;
                case 102: // Vesper
                    from.MoveToWorld(new Point3D(3013,828,0), Map.Felucca);
                    break;
                case 103: // Asgard
                    from.MoveToWorld(new Point3D(3987,410,5), Map.Felucca);
                    break;
                case 104: // Spital
                    from.MoveToWorld(new Point3D(4674,567,5), Map.Felucca);
                    break;
                case 105: // Moonglow
                    from.MoveToWorld(new Point3D(4467, 1283, 5), Map.Felucca);
                    break;
                case 106: // Nujel'm
                    from.MoveToWorld(new Point3D(3803,1279,10), Map.Felucca);
                    break;
                case 107: // Excalabria
                    from.MoveToWorld(new Point3D(181,150,5), Map.Felucca);
                    break;
                case 108: // Cove
                    from.MoveToWorld(new Point3D(2218,1116,24), Map.Felucca);
                    break;
                case 109: // Britain
                    from.MoveToWorld(new Point3D(1336, 1997, 5), Map.Felucca);
                    break;
                case 110: // Trinsic
                    from.MoveToWorld(new Point3D(1828, 2948, -20), Map.Felucca);
                    break;
                case 111: // Jhelon
                    from.MoveToWorld(new Point3D(1499, 3771, 5), Map.Felucca);
                    break;
                case 112: // Skara Brae
                    from.MoveToWorld(new Point3D(  643, 2067, 5 ), Map.Felucca);
                    break;
                case 113: // Yew
                    from.MoveToWorld(new Point3D(771, 752, 5), Map.Felucca);
                    break;
                case 114: // Warclau
                    from.MoveToWorld(new Point3D(4521,1940,5), Map.Felucca);
                    break;
                case 115: // Magincia
                    from.MoveToWorld(new Point3D(3563, 2139, 34), Map.Felucca);
                    break;
                case 116: // Occlo
                    from.MoveToWorld(new Point3D(3650,2653,5), Map.Felucca);
                    break;
                case 117: // Buc's Den
                    from.MoveToWorld(new Point3D(2711, 2234, 0), Map.Felucca);
                    break;
                case 118: // Danube
                    from.MoveToWorld(new Point3D(3220,3109,5), Map.Felucca);
                    break;
                case 119: // Tenebrae
                    from.MoveToWorld(new Point3D(3830,3249,5), Map.Felucca);
                    break;
                case 120: // Serpents Hold
                    from.MoveToWorld(new Point3D(2791,3263,5), Map.Felucca);
                    break;
                case 121: // Tsarkon
                    from.MoveToWorld(new Point3D(3629,4022,5), Map.Felucca);
                    break;
                case 122: // Spandau
                    from.MoveToWorld(new Point3D(816,3611,5), Map.Felucca);
                    break;
                case 123: // Stow
                    from.MoveToWorld(new Point3D(391,2603,5), Map.Felucca);
                    break;


                case 150: // Covetous
                    from.MoveToWorld(new Point3D(2499,919,0), Map.Felucca);
                    break;
                case 151: // Deceit
                    from.MoveToWorld(new Point3D(4111,638,5), Map.Felucca);
                    break;
                case 152: // Despise
                    from.MoveToWorld(new Point3D(1298,1080,0), Map.Felucca);
                    break;
                case 153: // Destard
                    from.MoveToWorld(new Point3D(1176,2637,0), Map.Felucca);
                    break;
                case 154: // Fire
                    from.MoveToWorld(new Point3D(5760,2908,15), Map.Felucca);
                    break;
                case 155: // Hythloth
                    from.MoveToWorld(new Point3D(4721,3822,0), Map.Felucca);
                    break;
                case 156: // Ice
                    from.MoveToWorld(new Point3D(5210,2322,30), Map.Felucca);
                    break;
                case 157: // Khaldun
                    from.MoveToWorld(new Point3D(6009,3775,19), Map.Felucca);
                    break;
                case 158: // Orc Cave
                    from.MoveToWorld(new Point3D(1019, 1431, 0), Map.Felucca);
                    break;
                case 159: // Shame
                    from.MoveToWorld(new Point3D(514, 1561, 0), Map.Felucca);
                    break;
                case 160: // Wrong
                    from.MoveToWorld(new Point3D(2043, 238, 10), Map.Felucca);
                    break;
                    



                case 301: // Compassion
                    from.MoveToWorld(new Point3D( 1215,  467, -13 ), Map.Ilshenar);
                    break;
                case 302: // Honesty
                    from.MoveToWorld(new Point3D(  722, 1366, -60 ), Map.Ilshenar);
                    break;
                case 303: // Honor
                    from.MoveToWorld(new Point3D(  744,  724, -28 ), Map.Ilshenar);
                    break;
                case 304: // Humility
                    from.MoveToWorld(new Point3D(  281, 1016,   0 ), Map.Ilshenar);
                    break;
                case 305: // Justice
                    from.MoveToWorld(new Point3D(  987, 1011, -32 ), Map.Ilshenar);
                    break;
                case 306: // Sacrifice
                    from.MoveToWorld(new Point3D( 1174, 1286, -30 ), Map.Ilshenar);
                    break;
                case 307: // Spirituality
                    from.MoveToWorld(new Point3D( 1532, 1340, - 3 ), Map.Ilshenar);
                    break;
                case 308: // Valor
                    from.MoveToWorld(new Point3D(  528,  216, -45 ), Map.Ilshenar);
                    break;
                case 309: // Chaos
                    from.MoveToWorld(new Point3D( 1721,  218,  96 ), Map.Ilshenar);
                    break;


                case 350: // Ankh
                    from.MoveToWorld(new Point3D(577, 1143, -100), Map.Ilshenar);
                    break;
                case 351: // Blood
                    from.MoveToWorld(new Point3D(1747, 1184, -1), Map.Ilshenar);
                    break;
                case 352: // Exodus
                    from.MoveToWorld(new Point3D(974, 692, -80), Map.Ilshenar);
                    break;
                case 353: // Rock
                    from.MoveToWorld(new Point3D(1788, 573, 70), Map.Ilshenar);
                    break;
                case 354: // Sorceror
                    from.MoveToWorld(new Point3D(547, 462, -53), Map.Ilshenar);
                    break;
                case 355: // Spectre
                    from.MoveToWorld(new Point3D(1363, 1089, -13), Map.Ilshenar);
                    break;
                case 356: // Wisp
                    from.MoveToWorld(new Point3D(651, 1301, -58), Map.Ilshenar);
                    break;


                case 401: // Luna
                    from.MoveToWorld(new Point3D(1015, 527, -65), Map.Malas);
                    BaseCreature.TeleportPets(from, new Point3D(1015, 527, -65), Map.Malas);
                    Effects.PlaySound(new Point3D(1015, 527, -65), Map.Malas, 0x1FE);
                    break;
                case 402: // Umbra
                    from.MoveToWorld(new Point3D(1997, 1386, -85), Map.Malas);
                    BaseCreature.TeleportPets(from, new Point3D(1997, 1386, -85), Map.Malas);
                    Effects.PlaySound(new Point3D(1997, 1386, -85), Map.Malas, 0x1FE);
                    break;

                case 450: // Doom
                    from.MoveToWorld(new Point3D(2367, 1268, -85), Map.Malas);
                    break;

                case 0:
                default:
                    break;
            }
        }

    }
}