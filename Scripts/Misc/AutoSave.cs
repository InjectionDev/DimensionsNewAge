using System;
using System.IO;
using Server;
using Server.Commands;

namespace Server.Misc
{
	public class AutoSave : Timer
	{
		private static TimeSpan m_Delay = TimeSpan.FromMinutes( 10.0 );
		private static TimeSpan m_Warning = TimeSpan.Zero;
		//private static TimeSpan m_Warning = TimeSpan.FromSeconds( 15.0 );

		public static void Initialize()
		{
			new AutoSave().Start();
			CommandSystem.Register( "SetSaves", AccessLevel.Administrator, new CommandEventHandler( SetSaves_OnCommand ) );
		}

		private static bool m_SavesEnabled = true;

		public static bool SavesEnabled
		{
			get{ return m_SavesEnabled; }
			set{ m_SavesEnabled = value; }
		}

		[Usage( "SetSaves <true | false>" )]
		[Description( "Enables or disables automatic shard saving." )]
		public static void SetSaves_OnCommand( CommandEventArgs e )
		{
			if ( e.Length == 1 )
			{
				m_SavesEnabled = e.GetBoolean( 0 );
				e.Mobile.SendMessage( "Saves have been {0}.", m_SavesEnabled ? "enabled" : "disabled" );
			}
			else
			{
				e.Mobile.SendMessage( "Format: SetSaves <true | false>" );
			}
		}

		public AutoSave() : base( m_Delay - m_Warning, m_Delay )
		{
			Priority = TimerPriority.OneMinute;
		}

		protected override void OnTick()
		{
			if ( !m_SavesEnabled || AutoRestart.Restarting )
				return;

			if ( m_Warning == TimeSpan.Zero )
			{
				Save( true );
			}
			else
			{
				int s = (int)m_Warning.TotalSeconds;
				int m = s / 60;
				s %= 60;

				if ( m > 0 && s > 0 )
					World.Broadcast( 0x35, true, "The world will save in {0} minute{1} and {2} second{3}.", m, m != 1 ? "s" : "", s, s != 1 ? "s" : "" );
				else if ( m > 0 )
					World.Broadcast( 0x35, true, "The world will save in {0} minute{1}.", m, m != 1 ? "s" : "" );
				else
					World.Broadcast( 0x35, true, "The world will save in {0} second{1}.", s, s != 1 ? "s" : "" );

				Timer.DelayCall( m_Warning, new TimerCallback( Save ) );
			}
		}

		public static void Save()
		{
			AutoSave.Save( false );
		}

		public static void Save( bool permitBackgroundWrite )
		{
			if ( AutoRestart.Restarting )
				return;

			World.WaitForWriteCompletion();

            try
            {
                BackupDiario();
            }
            catch (Exception e) { Console.WriteLine("WARNING: Backup Diario FAILED: {0}", e); }

			try
            {
                Backup();
            }
			catch ( Exception e ) { Console.WriteLine("WARNING: Automatic backup DISC 1 FAILED: {0}", e); }

			World.Save( true, permitBackgroundWrite );
		}

		private static string[] m_Backups = new string[]
			{
                "30 Backup",
                "29 Backup",
                "28 Backup",
                "27 Backup",
                "26 Backup",
                "25 Backup",
                "24 Backup",
                "23 Backup",
                "22 Backup",
                "21 Backup",
                "20 Backup",
                "19 Backup",
                "18 Backup",
                "17 Backup",
                "16 Backup",
                "15 Backup",
                "14 Backup",
                "13 Backup",
                "12 Backup",
                "11 Backup",
                "10 Backup",
                "9 Backup",
                "8 Backup",
                "7 Backup",
                "6 Backup",
                "5 Backup",
                "4 Backup",
				"Third Backup",
				"Second Backup",
				"Most Recent"
			};

        private static void BackupDiario()
        {
            string root = Path.Combine(Core.BaseDirectory, "Backups/Diario");

            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);

            string folderName = GetTimeStampDay();

            foreach(string folder in Directory.GetDirectories(root))
            {
                
                if (folder.EndsWith(folderName))
                    return; // Já fez o backup
            }

            string saves = Path.Combine(Core.BaseDirectory, "Saves");

            if (Directory.Exists(saves))
                DirectoryCopy(saves, Path.Combine(root, folderName), true);
        }

		private static void Backup()
		{
			if ( m_Backups.Length == 0 )
				return;

            string root = Path.Combine(Core.BaseDirectory, "Backups/Automatic");

			if ( !Directory.Exists( root ) )
				Directory.CreateDirectory( root );

			string[] existing = Directory.GetDirectories( root );

            for (int i = 0; i < m_Backups.Length; ++i)
            {
                DirectoryInfo dir = Match(existing, m_Backups[i]);

                if (dir == null)
                    continue;

                if (i > 0)
                {
                    string timeStamp = FindTimeStamp(dir.Name);

                    if (timeStamp != null)
                    {
                        try { dir.MoveTo(FormatDirectory(root, m_Backups[i - 1], timeStamp)); }
                        catch { }
                    }
                }
                else
                {
                    try { dir.Delete(true); }
                    catch { }
                }
            }

			string saves = Path.Combine( Core.BaseDirectory, "Saves" );

			if ( Directory.Exists( saves ) )
                Directory.Move(saves, FormatDirectory(root, m_Backups[m_Backups.Length - 1], GetTimeStamp()));
		}

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

		private static DirectoryInfo Match( string[] paths, string match )
		{
			for ( int i = 0; i < paths.Length; ++i )
			{
				DirectoryInfo info = new DirectoryInfo( paths[i] );

				if ( info.Name.StartsWith( match ) )
					return info;
			}

			return null;
		}

		private static string FormatDirectory( string root, string name, string timeStamp )
		{
			return Path.Combine( root, String.Format( "{0} ({1})", name, timeStamp ) );
		}

		private static string FindTimeStamp( string input )
		{
			int start = input.IndexOf( '(' );

			if ( start >= 0 )
			{
				int end = input.IndexOf( ')', ++start );

				if ( end >= start )
					return input.Substring( start, end-start );
			}

			return null;
		}

		private static string GetTimeStamp()
		{
			DateTime now = DateTime.Now;

			return String.Format( "{0}-{1}-{2} {3}-{4:D2}-{5:D2}",
					now.Day,
					now.Month,
					now.Year,
					now.Hour,
					now.Minute,
					now.Second
				);
		}

        private static string GetTimeStampDay()
        {
            DateTime now = DateTime.Now;

            string timestamp = String.Format("{2}{1:D2}{0:D2}", now.Day, now.Month, now.Year);
            string complement = string.Empty;

            if (now.Hour >= 0)
                complement = "meianoite";
            if (now.Hour >= 6)
                complement = "madrugada";
            if (now.Hour >= 12)
                complement = "meiodia";
            if (now.Hour >= 18)
                complement = "noite";

            return timestamp + "-" + complement;
        }
	}
}