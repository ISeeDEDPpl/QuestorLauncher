/*
 * ---------------------------------------
 * User: duketwo
 * Date: 29.12.2013
 * Time: 21:20
 * 
 * ---------------------------------------
 */
using System;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace HookManager
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		/// 
		[STAThread]
		private static void Main(string[] args)
		{
			
			
			Win32Hooks.HookManager.Instance.AccountName = args[0];
			Win32Hooks.HookManager.Instance.CharName = args[1];
			Win32Hooks.HookManager.Instance.Password = args[2];
			Win32Hooks.HookManager.Instance.UseRedGuard = bool.Parse(args[3]);
			Win32Hooks.HookManager.Instance.HideHookManager = bool.Parse(args[4]);
			Win32Hooks.HookManager.Instance.UseAdaptEve = bool.Parse(args[5]);
			Win32Hooks.HookManager.Instance.PipeName = args[6];
			
			Win32Hooks.HookManager.Instance.InitializeHookManager();
			
			try {
				
				WCFClient.Instance.GetPipeProxy.Ping();
				
				
			} catch (Exception) {
				
				Environment.Exit(0);
				Environment.FailFast("");
				
			}
			

			Win32Hooks.HookManager.Instance.HWSettings = WCFClient.Instance.GetPipeProxy.GetHWSettings(Win32Hooks.HookManager.Instance.AccountName);
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(args));
			
		}
		
	}
}
