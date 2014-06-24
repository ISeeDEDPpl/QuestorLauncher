/*
 * ---------------------------------------
 * User: duketwo
 * Date: 12.12.2013
 * Time: 12:51
 * 
 * ---------------------------------------
 */
using System;
using EasyHook;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using QuestorLauncher;

namespace Win32Hooks
{
	/// <summary>
	/// Description of IsDebuggerPresent.
	/// </summary>
	/// 
	
	
	public class GetModuleHandleAController : IHook, IDisposable
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
		public static extern IntPtr GetModuleHandleA(IntPtr lpModuleName);
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern void SetLastError(int errorCode);
		private delegate IntPtr GetModuleHandleADelegate(IntPtr lpModuleName);
		private LocalHook _hook;
		public bool Error { get; set; }
		public string Name  { get; set; }
		
		
		public GetModuleHandleAController()
		{
			this.Error = false;
			this.Name = typeof(GetModuleHandleAController).Name;
			
			try {
				this._hook = LocalHook.Create(
					LocalHook.GetProcAddress("Kernel32.dll", "GetModuleHandleA"),
					new Win32Hooks.GetModuleHandleAController.GetModuleHandleADelegate(GetModuleHandleADetour),
					this);
				
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 1 });
				this.Error = false;
			} catch (Exception) {
				this.Error = true;
				
			}
		}
		
		private IntPtr GetModuleHandleADetour(IntPtr lpModuleName)
		{
			try {
				string lpModName = Marshal.PtrToStringAnsi(lpModuleName);
				if (lpModName != null && lpModName != ""){
					if(HookManager.NeedsToBeCloaked(lpModName)) {
						HookManager.Log("[GetModuleHandleADetour] Found & cloaked: " + lpModName);
						return IntPtr.Zero;
					} else {
						if(!HookManager.IsWhiteListedFileName(lpModName)) HookManager.Log("[GetModuleHandleADetour] File wasn't found in QuestorManager-Directory - not claoking: " + lpModName);
						return GetModuleHandleA(lpModuleName);
					}
				}
				return GetModuleHandleA(lpModuleName);
				
			} catch (Exception) {
				return IntPtr.Zero;
			}
		}
		
		public void Dispose(){
			
			_hook.Dispose();
		}
		
		
		
	}
}
