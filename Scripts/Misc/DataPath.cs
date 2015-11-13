using System;
using System.IO;
using Microsoft.Win32;
using Server;

namespace Server.Misc
{
	public class DataPath
	{
		/* If you have not installed Ultima Online,
		 * or wish the server to use a separate set of datafiles,
		 * change the 'CustomPath' value, example:
		 * 
		 * private const string CustomPath = @"C:\Ultima Online";
		 */
        private static string CustomPath 
        { 
            get 
            {
                if (Environment.MachineName.ToUpper() == "DANIEL-NOTE" || Environment.MachineName.ToUpper() == "DANIEL-PC")
                {
                    Console.WriteLine("Path: " + @"C:\UO\MLDimens");
                    return @"C:\UO\MLDimens";
                }
                else
                {
                    Console.WriteLine("Path: " + @"C:\Ultima Online");
                    return @"C:\Ultima Online";
                }
            } 
        }

		/* The following is a list of files which a required for proper execution:
		 * 
		 * Multi.idx
		 * Multi.mul
		 * VerData.mul
		 * TileData.mul
		 * Map*.mul
		 * StaIdx*.mul
		 * Statics*.mul
		 * MapDif*.mul
		 * MapDifL*.mul
		 * StaDif*.mul
		 * StaDifL*.mul
		 * StaDifI*.mul
		 */

		public static void Configure()
		{
			string pathUO = GetPath( @"Origin Worlds Online\Ultima Online\1.0", "ExePath" );
			string pathTD = GetPath( @"Origin Worlds Online\Ultima Online Third Dawn\1.0", "ExePath" ); //These refer to 2D & 3D, not the Third Dawn expansion
			string pathKR = GetPath( @"Origin Worlds Online\Ultima Online\KR Legacy Beta", "ExePath" ); //After KR, This is the new registry key for the 2D client
			string pathSA = GetPath( @"Electronic Arts\EA Games\Ultima Online Stygian Abyss Classic", "InstallDir" );

            Console.WriteLine(string.Format("pathUO:{0} pathTD:{1} pathKR:{2} pathSA:{3}", pathUO, pathTD, pathKR, pathSA));

			if ( CustomPath != null ) 
				Core.DataDirectories.Add( CustomPath ); 

			if ( pathUO != null ) 
				Core.DataDirectories.Add( pathUO ); 

			if ( pathTD != null ) 
				Core.DataDirectories.Add( pathTD );

			if ( pathKR != null )
				Core.DataDirectories.Add( pathKR );

			if ( pathSA != null )
				Core.DataDirectories.Add( pathSA );

			if ( Core.DataDirectories.Count == 0 && !Core.Service )
			{
				Console.WriteLine( "Enter the Ultima Online directory:" );
				Console.Write( "> " );

				Core.DataDirectories.Add( Console.ReadLine() );
			}
		}

		private static string GetPath( string subName, string keyName )
		{
			try
			{
				string keyString;

				if( Core.Is64Bit )
					keyString = @"SOFTWARE\Wow6432Node\{0}";
				else
					keyString = @"SOFTWARE\{0}";

				using( RegistryKey key = Registry.LocalMachine.OpenSubKey( String.Format( keyString, subName ) ) )
				{
					if( key == null )
						return null;

					string v = key.GetValue( keyName ) as string;

					if( String.IsNullOrEmpty( v ) )
						return null;

					if ( keyName == "InstallDir" )
						v = v + @"\";

					v = Path.GetDirectoryName( v );

					if ( String.IsNullOrEmpty( v ) )
						return null;

					return v;
				}
			}
			catch
			{
				return null;
			}
		}
	}
}