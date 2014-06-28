/*
 * ---------------------------------------
 * User: duketwo
 * Date: 21.06.2014
 * Time: 17:20
 * 
 * ---------------------------------------
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using EasyHook;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;

namespace HookManager
{
	/// <summary>
	/// Description of Enviroment.
	/// </summary>
	public class EnvVars
	{
		public static void SetEnvironment(Injector.HWSettings settings)
		{
			IntPtr myUsernamePointer = getenv("USERNAME");
			string myUsername = Marshal.PtrToStringAnsi(myUsernamePointer);
			
			CheckCreateDirectorys(settings.WindowsUserLogin);

			_putenv("COMPUTERNAME=" + settings.Computername.ToUpper());
			_putenv("USERDOMAIN=" + settings.Computername.ToUpper());
			_putenv("USERDOMAIN_ROAMINGPROFILE=" + settings.Computername.ToUpper());
			_putenv("USERNAME=" + settings.WindowsUserLogin);
			_putenv(@"TMP=C:\Users\" + settings.WindowsUserLogin + @"\AppData\Local\Temp");
			_putenv("VISUALSTUDIODIR=");

			if (settings.ProcessorIdent != null && settings.ProcessorIdent != null && settings.ProcessorCoreAmount != null && settings.ProcessorLevel != null)
			{
				_putenv("PROCESSOR_IDENTIFIER=" + settings.ProcessorIdent);
				_putenv("PROCESSOR_REVISION=" + settings.ProcessorRev);
				_putenv("NUMBER_OF_PROCESSORS=" + settings.ProcessorCoreAmount);
				_putenv("PROCESSOR_LEVEL=" + settings.ProcessorLevel);
			}

			_putenv(@"USERPROFILE=C:\Users\" + settings.WindowsUserLogin);
			_putenv(@"HOMEPATH=C:\Users\" + settings.WindowsUserLogin);
			_putenv(@"LOCALAPPDATA=C:\Users\" + settings.WindowsUserLogin + @"\AppData\Local");
			_putenv(@"TEMP=C:\Users\" + settings.WindowsUserLogin + @"\AppData\Local\Temp");
			_putenv(@"APPDATA=C:\Users\" + settings.WindowsUserLogin + @"\AppData\Roaming");

			var pathPointer = getenv("PATH");
			var path = Marshal.PtrToStringAnsi(pathPointer);
			path = path.Replace(myUsername, settings.WindowsUserLogin, StringComparison.InvariantCultureIgnoreCase);
			_putenv("PATH=" + path);

		}
		
		public static void CheckCreateDirectorys(string windowsUserLogin){
			string userDir = "C:\\Users\\" + windowsUserLogin + "\\";
			var path = new String[] {userDir + "Documents",userDir + "AppData\\Local\\Temp", userDir + "AppData\\Local\\Roaming", userDir + "AppData\\Local\\Temp"};
			
			foreach(string d in path){
				if(!Directory.Exists(d))
					Directory.CreateDirectory(d);
			}
		}
		
		
		public static void PrintEnvVars(){
			
			foreach(DictionaryEntry  env in System.Environment.GetEnvironmentVariables()){
				Win32Hooks.HookManager.Log(env.Key + " " + env.Value );
			}
			
		}

		[DllImport("msvcr100.dll", SetLastError = true)]
		static extern IntPtr getenv(string lpName);

		[DllImport("msvcr100.dll", SetLastError = true)]
		static extern bool _putenv(string lpName);
	}

	public static class StringExtensions
	{
		public static string Replace(this string originalString, string oldValue, string newValue, StringComparison comparisonType)
		{
			int startIndex = 0;
			while (true)
			{
				startIndex = originalString.IndexOf(oldValue, startIndex, comparisonType);
				if (startIndex == -1)
					break;

				originalString = originalString.Substring(0, startIndex) + newValue + originalString.Substring(startIndex + oldValue.Length);

				startIndex += newValue.Length;
			}

			return originalString;
		}

	}
}
