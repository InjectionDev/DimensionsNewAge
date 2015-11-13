using System;
using System.Text;

namespace DimensionsNewAge.Scripts
{
    public class HueOreConst
    {
        public const int HueRusty = 1872;
        public const int HueIron = 0;
        public const int HueOldCopper = 1424;
        public const int HueDullCopper = 1546;
        public const int HueCopper = 1601;
        public const int HueBronze = 1750;
        public const int HueShadow = 1904;
        public const int HueSilver = 561;
        public const int HueRose = 1637;
        public const int HueGold = 1118;
        public const int HueAgapite = 1024;
        public const int HueVerite = 2001;
        public const int HueBloodRock = 1218;
        public const int HueValorite = 1301;
        public const int HueBlackRock = 1109;
        public const int HueMytheril = 1325;

        public const int HueRuby = 0x022;
        public const int HueMercury = 0x030;
        public const int HuePlutonio = 0x06f;
        public const int HueAqua = 0x060;

        private const int HueConvertionOffset = 21; // offset das cores especiais DimensSphere para novo Hue DimensNewAge

        public const int HueEndurium = 1154;
        public const int HueOldEndurium = 1953 + HueConvertionOffset;
        public const int HueGoldStone = 1965 + HueConvertionOffset;
        public const int HueMaxMytheril = 1952 + HueConvertionOffset;
        public const int HueMagma = 1939 + HueConvertionOffset;
        

        //color_o_oldend     07a1
        //color_o_goldstone  07ad
        //color_o_maxmyt     07ad
        //color_o_magma      0793
        //color_o_endurium   0482

        //color_o_ruby       022
        //color_o_mercury    030
        //color_o_plutonium  06f
        //color_o_aqua       060
            
        //color_o_rusty      0750
        //color_o_iron	   0
        //color_o_oldcopper  0590
        //color_o_dullcopper 060a
        //color_o_copper	   0641
        //color_o_bronze     06d6
        //color_o_shadow     0770
        //color_o_silver     0231
        //color_o_rose       0665
        //color_o_gold	   045e
        //color_o_agapite    0400
        //color_o_verite     07d1
        //color_o_bloodrock  04c2
        //color_o_valorite   0515
        //color_o_blackrock  0455
        //color_o_mytheril   052d
    }
}
