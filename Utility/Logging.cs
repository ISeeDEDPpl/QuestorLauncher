// ------------------------------------------------------------------------------
//   <copyright from='2010' to='2015' company='THEHACKERWITHIN.COM'>
//     Copyright (c) TheHackerWithin.COM. All Rights Reserved.
//
//     Please look in the accompanying license.htm file for the license that
//     applies to this source code. (a copy can also be found at:
//     http://www.thehackerwithin.com/license.htm)
//   </copyright>
// -------------------------------------------------------------------------------

namespace Utility
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    
    public static class Logging
    {
        
        static Logging()
        {
            Logging.PathToCurrentDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static string PathToCurrentDirectory;
        public static bool DebugMaintainConsoleLogs { get; set; }
        
        public static DateTime DateTimeForLogs;
        //list of colors
        public const string Green = "\ag";    //traveler mission control
        public const string Yellow = "\ay";
        public const string Blue = "\ab";     //DO NOT USE - blends into default lavish GUIs background.
        public const string Red = "\ar";      //error panic
        public const string Orange = "\ao";   //error can fix
        public const string Purple = "\ap";   //combat
        public const string Magenta = "\am";  //drones
        public const string Teal = "\at";     //log debug
        public const string White = "\aw";    //questor


        public const string Debug = Teal;     //log debug

        public const string CombatUserCorrectableErrors = Orange;
        public const string CombatFatalErrors = Red;
        public const string CombatGenericLogging = White;

        public const string DronesUserCorrectableErrors = Orange;
        public const string DronesFatalErrors = Red;
        public const string DronesGenericLogging = White;

        public const string TravelerUserCorrectableErrors = Orange;
        public const string TravelerFatalErrors = Red;
        public const string TravelerGenericLogging = White;
        public const string TravelerDestinationColor = White;

        public static string EVELoginUserName;
        public static string EVELoginPassword;
        public static string MyCharacterName;

        public static string CharacterSettingsPath;

        public static bool tryToLogToFile = true;  //we should set this to a sane value (via get { blah } when we are pre-login.... 
        //public static List<string> _QuestorParamaters;

        private static string colorLogLine;
        private static string plainLogLine;
        private static string ConsoleLogText;
        public static bool SaveConsoleLog;  //we should set this to a sane value (via get { blah } when we are pre-login.... 
        public static bool ConsoleLogOpened = false;
        public static string ExtConsole { get; set; }
        //public static string ConsoleLog { get; set; }
        //public static string ConsoleLogRedacted { get; set; }
        public static string SessionDataCachePath { get; set; }
        public static string Logpath { get; set; }

        public static bool InnerspaceGeneratedConsoleLog { get; set; }
        public static bool UseInnerspace { get; set; }
        //public static bool ConsoleLog { get; set; }
        public static string ConsoleLogPath { get; set; } //we should set this to a sane value (via get { blah } when we are pre-login.... 
        public static string ConsoleLogFile { get; set; } //we should set this to a sane value (via get { blah } when we are pre-login.... 
        public static bool SaveLogRedacted { get; set; } //we should set this to a sane value (via get { blah } when we are pre-login.... 
        //public static bool ConsoleLogRedacted { get; set; }
        public static string redactedPlainLogLine { get; set; }
        public static string redactedColorLogLine { get; set; }
        public static string ConsoleLogPathRedacted { get; set; }  //we should set this to a sane value (via get { blah } when we are pre-login.... 
        public static string ConsoleLogFileRedacted { get; set; }  //we should set this to a sane value (via get { blah } when we are pre-login.... 
        
        //
        // number of days of console logs to keep (anything older will be deleted on startup)
        //
        public static int ConsoleLogDaysOfLogsToKeep { get; set; }


        //public  void Log(string line)
        //public static void Log(string module, string line, string color = Logging.White)
        public static void Log(string module, string line, string color, bool verbose = false)
        {
            try
            {
                DateTimeForLogs = DateTime.Now;

                if (verbose) //tons of info
                {
                    //
                    // verbose text logging - with line numbers, filenames and Methods listed ON EVERY LOGGING LINE - this is ALOT more detail
                    //
                    System.Diagnostics.StackFrame sf = new System.Diagnostics.StackFrame(1, true);
                    module += "-[line" + sf.GetFileLineNumber().ToString() + "]in[" + System.IO.Path.GetFileName(sf.GetFileName()) + "][" + sf.GetMethod().Name + "]";
                }

                colorLogLine = line;
                Logging.redactedColorLogLine = String.Format("{0:HH:mm:ss} {1}", DateTimeForLogs, "[" + module + "] " + FilterSensitiveInfo(plainLogLine) + "\r\n");  //In memory Console Log with sensitive info redacted
                
                plainLogLine = FilterColorsFromLogs(line);

                Logging.redactedPlainLogLine = String.Format("{0:HH:mm:ss} {1}", DateTimeForLogs, "[" + module + "] " + FilterSensitiveInfo(plainLogLine) + "\r\n");  //In memory Console Log with sensitive info redacted

                //Logging.ConsoleLogText = String.Format("{0:HH:mm:ss} {1}", DateTimeForLogs, "[" + module + "] " + plainLogLine + "\r\n");
                Logging.ConsoleLogText = Logging.redactedPlainLogLine;
                Logging.ExtConsole = Logging.ConsoleLogText;
                
                Console.WriteLine(Logging.ConsoleLogText);
                
                if (Logging.tryToLogToFile)
                {
                    if (Logging.SaveConsoleLog)//(Settings.Instance.SaveConsoleLog)
                    {
                        if (!Logging.ConsoleLogOpened)
                        {
                            //
                            // begin logging to file
                            //
                            if (Logging.ConsoleLogPath != null && Logging.ConsoleLogFile != null)
                            {
                                if (!string.IsNullOrEmpty(Logging.ConsoleLogFile))
                                {
                                    Directory.CreateDirectory(Logging.ConsoleLogPath);
                                    if (Directory.Exists(Logging.ConsoleLogPath))
                                    {
                                        Logging.ConsoleLogOpened = true;
                                    }
                                    line = "";
                                }
                            }
                        }

                        if (Logging.ConsoleLogOpened)
                        {
                            //
                            // log file ready: add next logging entry...
                            //
                            //
                            // normal text logging
                            //
                            if (Logging.ConsoleLogFile != null && !verbose) //normal
                            {
                                File.AppendAllText(Logging.ConsoleLogFile, Logging.ConsoleLogText); //Write In Memory Console log to File
                            }

                            //
                            // redacted text logging - sensitive info removed so you can generally paste the contents of this log publicly w/o fear of easily exposing user identifiable info
                            //
                            if (Logging.ConsoleLogFileRedacted != null)
                            {
                                File.AppendAllText(Logging.ConsoleLogFileRedacted, Logging.redactedPlainLogLine);               //Write In Memory Console log to File
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                BasicLog(module, exception.Message);
            }
        }

        public static void BasicLog(string module, string logmessage)
        {
            Console.WriteLine(string.Format("{0:HH:mm:ss} {1}", DateTime.UtcNow,"[" + module + "] " + logmessage));
            if (Logging.SaveLogRedacted && Logging.ConsoleLogFileRedacted != null)
            {
                if (Directory.Exists(Logging.ConsoleLogPathRedacted))
                {
                    File.AppendAllText(Logging.ConsoleLogFileRedacted, string.Format("{0:HH:mm:ss} {1}", DateTime.UtcNow,"[" + module + "] " + logmessage));
                }
            }

            if (Logging.SaveLogRedacted && Logging.ConsoleLogFile != null)
            {
                if (Directory.Exists(Logging.ConsoleLogPath))
                {
                    File.AppendAllText(Logging.ConsoleLogFile, string.Format("{0:HH:mm:ss} {1}", DateTime.UtcNow, "[" + module + "] " + logmessage));
                }
            }
        }

        //path = path.Replace(Environment.CommandLine, "");
        //path = path.Replace(Environment.GetCommandLineArgs(), "");

        public static string FilterSensitiveInfo(string line)
        {
            try
            {
                if (line == null)
                    return string.Empty;
                if (!string.IsNullOrEmpty(Logging.MyCharacterName))
                {
                    line = line.Replace(Logging.MyCharacterName, Logging.MyCharacterName.Substring(0, 2) + "_MyEVECharacterNameRedacted_");
                    line = line.Replace("/" + Logging.MyCharacterName, "/" + Logging.MyCharacterName.Substring(0, 2) + "_MyEVECharacterNameRedacted_");
                    line = line.Replace("\\" + Logging.MyCharacterName, "\\" + Logging.MyCharacterName.Substring(0, 2) + "_MyEVECharacterNameRedacted_");
                    line = line.Replace("[" + Logging.MyCharacterName + "]", "[" + Logging.MyCharacterName.Substring(0, 2) + "_MyEVECharacterNameRedacted_]");
                    line = line.Replace(Logging.MyCharacterName + ".xml", Logging.MyCharacterName.Substring(0, 2) + "_MyEVECharacterNameRedacted_.xml");
                }

                if (!string.IsNullOrEmpty(Logging.CharacterSettingsPath))
                {
                    line = line.Replace(Logging.CharacterSettingsPath, Logging.CharacterSettingsPath.Substring(0, 2) + "_MySettingsFileNameRedacted_.xml");
                }

                //if (!string.IsNullOrEmpty(Cache.Instance.CurrentAgent))
                //{
                //    if (Logging.DebugLogging) InnerSpace.Echo("Logging.Log: FilterSensitiveInfo: CurrentAgent exists [" + Cache.Instance.CurrentAgent + "]");
                //    line = line.Replace(" " + Cache.Instance.CurrentAgent + " ", " _MyCurrentAgentRedacted_ ");
                //    line = line.Replace("[" + Cache.Instance.CurrentAgent + "]", "[_MyCurrentAgentRedacted_]");
                //}
                //if (Cache.Instance.AgentId != -1)
                //{
                //    if(Logging.DebugLogging) InnerSpace.Echo("Logging.Log: FilterSensitiveInfo: AgentId is not -1");
                //    line = line.Replace(" " + Cache.Instance.AgentId + " ", " _MyAgentIdRedacted_ ");
                //    line = line.Replace("[" + Cache.Instance.AgentId + "]", "[_MyAgentIdRedacted_]");
                //}
                if (!String.IsNullOrEmpty(Logging.EVELoginUserName))
                {
                    line = line.Replace(Logging.EVELoginUserName, Logging.EVELoginUserName.Substring(0, 2) + "_HiddenEVELoginName_");
                }
                if (!String.IsNullOrEmpty(Logging.EVELoginPassword))
                {
                    line = line.Replace(Logging.EVELoginPassword, "_HiddenPassword_");
                }
                if (!string.IsNullOrEmpty(Environment.UserName))
                {
                    line = line.Replace("\\" + Environment.UserName + "\\", "\\_MyWindowsLoginNameRedacted_\\");
                    line = line.Replace("/" + Environment.UserName + "/", "/_MyWindowsLoginNameRedacted_/");
                }
                if (!string.IsNullOrEmpty(Environment.UserDomainName))
                {
                    line = line.Replace(Environment.UserDomainName, "_MyWindowsDomainNameRedacted_");
                }
                return line;
            }
            catch (Exception exception)
            {
                BasicLog("FilterSensitiveInfo", exception.Message);
                return line;
            }
        }

         public static string ReplaceUnderscoresWithSpaces(string line)
        {
            try
            {
                if (line == null)
                    return string.Empty;
                if (!string.IsNullOrEmpty(line))
                {
                    line = line.Replace("_", " ");
                }

                return line;
            }
            catch (Exception exception)
            {
                BasicLog("ReplaceUnderscoresWithSpaces", exception.Message);
                return line;
            }
        }

        //public static class RichTextBoxExtensions
        //{
        //    public static void AppendText(RichTextBox box, string text, Color color)
        //    {
        //        box.SelectionStart = box.TextLength;
        //        box.SelectionLength = 0;
        //
        //        box.SelectionColor = color;
        //        box.AppendText(text);
        //        box.SelectionColor = box.ForeColor;
        //    }
        //}

        public static string FilterPath(string path)
        {
            try
            {
                if (path == null)
                {
                    return string.Empty;
                }

                path = path.Replace("\"", "");
                path = path.Replace("?", "");
                path = path.Replace("\\", "");
                path = path.Replace("/", "");
                path = path.Replace("'", "");
                path = path.Replace("*", "");
                path = path.Replace(":", "");
                path = path.Replace(">", "");
                path = path.Replace("<", "");
                path = path.Replace(".", "");
                path = path.Replace(",", "");
                path = path.Replace("'", "");
                while (path.IndexOf("  ", System.StringComparison.Ordinal) >= 0)
                    path = path.Replace("  ", " ");
                return path.Trim();
            }
            catch (Exception exception)
            {
                Logging.Log("Cache.FilterPath", "Exception [" + exception + "]", Logging.Debug);
                return null;
            }
        }

        public static string FilterColorsFromLogs(string line)
        {
            try
            {
                if (line == null)
                    return string.Empty;

                line = line.Replace("\ag", "");
                line = line.Replace("\ay", "");
                line = line.Replace("\ab", "");
                line = line.Replace("\ar", "");
                line = line.Replace("\ao", "");
                line = line.Replace("\ap", "");
                line = line.Replace("\am", "");
                line = line.Replace("\at", "");
                line = line.Replace("\aw", "");
                while (line.IndexOf("  ", System.StringComparison.Ordinal) >= 0)
                    line = line.Replace("  ", " ");
                return line.Trim();
            }
            catch (Exception exception)
            {
                BasicLog("FilterSensitiveInfo", exception.Message);
                return null;
            }
        }

        public static void MaintainConsoleLogs()
        {
            const string searchpattern = ".log";

            //calculate the current date - the number of keep days (make sure you use the negative value if Settings.Instance.ConsoleLogDaysOfLogsToKeep as we want to keep that many days in the past, not that many days in the future)
            DateTime keepdate = DateTime.UtcNow.AddDays(-Logging.ConsoleLogDaysOfLogsToKeep);

            //this is where it gets the directory and looks at
            //the files in the directory to compare the last write time
            //against the keepdate variable.
            try
            {
                if (Logging.DebugMaintainConsoleLogs) Logging.Log("Logging.MaintainConsoleLogs", "ConsoleLogPath is [" + Logging.ConsoleLogPath + "]", Logging.White);
                DirectoryInfo fileListing = new DirectoryInfo(Logging.ConsoleLogPath);

                if (fileListing.Exists)
                {
                    if (Logging.DebugMaintainConsoleLogs) Logging.Log("Logging.MaintainConsoleLogs", "if (fileListing.Exists)", Logging.White);
                    foreach (FileInfo log in fileListing.GetFiles(searchpattern))
                    {
                        if (Logging.DebugMaintainConsoleLogs) Logging.Log("Logging.MaintainConsoleLogs", "foreach (FileInfo log in fileListing.GetFiles(searchpattern))", Logging.White);
                        if (log.LastWriteTime <= keepdate)
                        {
                            if (Logging.DebugMaintainConsoleLogs) Logging.Log("Logging.MaintainConsoleLogs", "if (log.LastWriteTime <= keepdate)", Logging.White);
                            try
                            {
                                Logging.Log("Logging", "Removing old console log named [" + log.Name + "] Dated [" + log.LastWriteTime + "]", Logging.White);
                                log.Delete();
                            }
                            catch (Exception ex)
                            {
                                Logging.Log("Logging", "Unable to delete log [" + ex.Message + "]", Logging.White);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                BasicLog("FilterSensitiveInfo", exception.Message);
            }
        }

        public static IEnumerable<string> SplitArguments(string commandLine)
        {
            try
            {
                char[] parmChars = commandLine.ToCharArray();
                bool inSingleQuote = false;
                bool inDoubleQuote = false;
                for (int index = 0; index < parmChars.Length; index++)
                {
                    if (parmChars[index] == '"' && !inSingleQuote)
                    {
                        inDoubleQuote = !inDoubleQuote;
                        parmChars[index] = '\n';
                    }
                    if (parmChars[index] == '\'' && !inDoubleQuote)
                    {
                        inSingleQuote = !inSingleQuote;
                        parmChars[index] = '\n';
                    }
                    if (!inSingleQuote && !inDoubleQuote && parmChars[index] == ' ')
                        parmChars[index] = '\n';
                }
                return (new string(parmChars)).Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch (Exception exception)
            {
                BasicLog("SplitArguments", exception.Message);
                return null;
            }
        }
    }
}