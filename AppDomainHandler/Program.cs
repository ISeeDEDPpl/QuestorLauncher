/*
 * ---------------------------------------
 * User: duketwo
 * Date: 11.12.2013
 * Time: 12:51
 * 
 * ---------------------------------------
 */

using System;
using System.Collections.Generic;
using EasyHook;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;

namespace QuestorLauncherInterface
{

	public class QuestorLauncherInterface : MarshalByRefObject
	{
		public void Ping()
		{
		}
	}
}

namespace QuestorManager
{
	// output name: QuestorManager.dll
	public class Main : IEntryPoint
	{
		[DllImport("kernel32.dll")]
		static extern void FreeLibraryAndExitThread(IntPtr hModule, uint dwExitCode);
		[DllImport("kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		public static extern IntPtr GetModuleHandle(string lpModuleName);
		
		QuestorLauncherInterface.QuestorLauncherInterface Interface;
		Stack<String> Queue = new Stack<String>();

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// 
		
		public Main(EasyHook.RemoteHooking.IContext InContext, string ChannelName, string[] args)
		{
			Interface = RemoteHooking.IpcConnectClient<QuestorLauncherInterface.QuestorLauncherInterface>(ChannelName);
			Interface.Ping();
		}
		
		
		public void Run(EasyHook.RemoteHooking.IContext InContext, string ChannelName, string[] args)
		{
			
			AppDomain currentDomain = AppDomain.CurrentDomain;
			AppDomain hookManagerDomain = AppDomain.CreateDomain("hookManagerDomain");
			string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			hookManagerDomain.ExecuteAssembly(assemblyFolder + "\\HookManager.exe", args: args);
			
			try 
            {	
				while (true) 
                {
					Thread.Sleep(100);
					Interface.Ping();
				}	
			} 
            catch 
            {
				AppDomain.Unload(hookManagerDomain);
				AppDomain.Unload(currentDomain);
			}
		}
	}
}
