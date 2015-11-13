using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Server
{
    public static class Logger
    {
        
        //private static string filePath = @"Logs\RunUODev.Log.txt";
        private static object objLock = new object();

        public static void LogMessage(string pMessage, string pType)
        {
            string timestamp = string.Format(string.Format("[{0:d2}/{1:d2} {2:d2}:{3:d2}] ", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Hour, DateTime.Now.Minute));

            string filePath = string.Format(@"Logs\Log.{0}.txt", (string.IsNullOrEmpty(pType) ? "Generic" : pType));

            lock (objLock)
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(timestamp + pType +": "+pMessage);
                }
            }
        }

        public static void LogRespawnControllerMessage(string pMessage)
        {
            string filePath = string.Format(@"Logs\Dimens.Respawn.txt");
            string timestamp = string.Format(string.Format("[{0:d2}/{1:d2} {2:d2}:{3:d2}] ", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Hour, DateTime.Now.Minute));

            lock (objLock)
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(timestamp + ": " + pMessage);
                }
            }

            //Console.WriteLine(timestamp + ": " + pMessage);
        }

        public static void LogSphereImport(string pMessage, string pCharName)
        {
            string filePath = string.Format(@"Logs\SphereImport\Dimens.SphereImport.{0}.txt", pCharName);

            string timestamp = string.Format(string.Format("[{0:d2}/{1:d2} {2:d2}:{3:d2}] ", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Hour, DateTime.Now.Minute));

            lock (objLock)
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(timestamp + pCharName + ": " + pMessage);
                }
            }

        }

    }
}
