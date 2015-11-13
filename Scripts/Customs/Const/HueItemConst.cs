using System;
using System.Text;

namespace DimensionsNewAge.Scripts
{
    public class HueItemConst
    {
        public const int HueRayBow = 280;
        public const int HuePoisonBow = 57;
        public const int HueAdvancedPoisonBow = 69;
        public const int HueFireBow = 32;
        public const int HueElvenBow = 567;

        // Todas as cores da DimensOld com novo range de ID
        public static int HueMagicColorRandom
        { 
            get
            {
                //return HuesMagicColor[new Random().Next(HuesMagicColor.Length)];
                return new Random().Next(1930, 1989);
            }
        }

        private const int HueConvertionOffset = 21; // offset das cores especiais DimensSphere para novo Hue DimensNewAge

        // Cores da dimensOld, utiliza nos dyetub
        private static readonly int[] HuesDyeTubColor = new int[]
			{
                // range das cores no hues dimensOld 1909 - 1968
				1152,
                1153,
                1953 + HueConvertionOffset,
                1965 + HueConvertionOffset,
                1952 + HueConvertionOffset,
                1941 + HueConvertionOffset,
                1940 + HueConvertionOffset,
                1944 + HueConvertionOffset,
                1962 + HueConvertionOffset,
                1964 + HueConvertionOffset,
                1947 + HueConvertionOffset,
                1946 + HueConvertionOffset,
                1957 + HueConvertionOffset,
                1954 + HueConvertionOffset,
                1955 + HueConvertionOffset,
                1958 + HueConvertionOffset,
                1960 + HueConvertionOffset, 
                1930 + HueConvertionOffset,
                1932 + HueConvertionOffset,
                1934 + HueConvertionOffset,
                1935 + HueConvertionOffset,
                1939 + HueConvertionOffset,
                1928 + HueConvertionOffset,
                1961 + HueConvertionOffset,
                1948 + HueConvertionOffset,
                1949 + HueConvertionOffset,
                1950 + HueConvertionOffset,
                2047,
                1920 + HueConvertionOffset,
                1922 + HueConvertionOffset,
                1923 + HueConvertionOffset,
                1925 + HueConvertionOffset,
                1926 + HueConvertionOffset,
                1936 + HueConvertionOffset,
                1937 + HueConvertionOffset,
                1942 + HueConvertionOffset
			};

        public static int HuesDyeTubColorRandom
        {
            get
            {
                return HuesDyeTubColor[new Random().Next(HuesDyeTubColor.Length)];
            }
        }

        public static int HueMustangColorRandom
        {
            get
            {
                return HuesMustangColor[new Random().Next(HuesMustangColor.Length)];
            }
        }

        public static int GetNewHueBySphereHue(int pSphereHueInt)
        {
            if (pSphereHueInt >= 1909 && pSphereHueInt <= 1968)
                return pSphereHueInt + HueConvertionOffset;
            else
                return pSphereHueInt;
        }

        // Balron DyeTube -> HUES Random().Next(1930, 1989)
        //private static readonly int[] HuesMagicColor = new int[]
        //    {
        //        1152,1153,1974,1973,1962,0x794,0x798,0x7aa,
        //        0x7ac,0x79b,0x79a,0x7a5,0x7a2,0x7a3,0x7a6,0x7a8,0x78a,
        //        0x78c,0x78e,0x78f,0x793,0x788,0x7a9,0x79c,0x79d,0x79e,
        //        0x7ff,0x780,0x782,0x783,0x785,0x786,0x790,0x791,0x796
        //    };

        // Mustang Hue
        private static readonly int[] HuesMustangColor = new int[]
			{
				1109, // Black Mustang
                438, // Crimson Mustang
                796, // SkyGray Mustang
                344, // Wimmimate Mustang
                51, // Pamamino Mustang
                611, // Sky Mustang
                279, // Redroan Mustang
                443, // Chocolate/Roan Mustang
                999 // Grey Mustang
			};

    }
}
