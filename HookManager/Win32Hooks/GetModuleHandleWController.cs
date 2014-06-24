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
	
	
	public class GetModuleHandleWController : IHook, IDisposable
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr GetModuleHandleW(IntPtr lpModuleName);
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern void SetLastError(int errorCode);
		private delegate IntPtr GetModuleHandleWDelegate(IntPtr lpModuleName);
		private LocalHook _hook;
		public bool Error { get; set; }
		public string Name  { get; set; }
		
		public GetModuleHandleWController()
		{
			this.Error = false;
			this.Name = typeof(GetModuleHandleWController).Name;
			
			try {
				this._hook = LocalHook.Create(
					LocalHook.GetProcAddress("Kernel32.dll", "GetModuleHandleW"),
					new Win32Hooks.GetModuleHandleWController.GetModuleHandleWDelegate(GetModuleHandleWDetour),
					this);
				
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 1 });
				this.Error = false;
			} catch (Exception) {
				this.Error = true;
				
			}
		}
		
		private IntPtr GetModuleHandleWDetour(IntPtr lpModuleName)
		{
			try {
				
				string lpModName = Marshal.PtrToStringUni(lpModuleName);
				if (lpModName != null && lpModName != ""){
					if(HookManager.NeedsToBeCloaked(lpModName)) {
						HookManager.Log("[GetModuleHandleWDetour] Found & cloaked: " + lpModName);
						return IntPtr.Zero;
					} else {
						if(!HookManager.IsWhiteListedFileName(lpModName)) HookManager.Log("[GetModuleHandleWDetour] File wasn't found in QuestorManager-Directory - not claoking: " + lpModName);
						return GetModuleHandleW(lpModuleName);
					}
				}
				return GetModuleHandleW(lpModuleName);
				
			} catch (Exception) {
				return IntPtr.Zero;
			}
			
		}
		
		public void Dispose(){
			
			_hook.Dispose();
		}
		
		
		
	}
}
