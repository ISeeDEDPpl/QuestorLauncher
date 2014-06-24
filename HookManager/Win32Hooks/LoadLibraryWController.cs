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
	
	
	public class LoadLibraryWController : IHook, IDisposable
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr LoadLibraryW(IntPtr lpModuleName);
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern void SetLastError(int errorCode);
		private delegate IntPtr LoadLibraryWDelegate(IntPtr lpModuleName);
		private LocalHook _hook;
		public bool Error { get; set; }
		public string Name  { get; set; }
		
		public LoadLibraryWController()
		{
			this.Error = false;
			this.Name = typeof(LoadLibraryWController).Name;
			
			try {
				this._hook = LocalHook.Create(
					LocalHook.GetProcAddress("Kernel32.dll", "LoadLibraryW"),
					new Win32Hooks.LoadLibraryWController.LoadLibraryWDelegate(LoadLibraryWDetour),
					this);
				
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 1 });
				this.Error = false;
			} catch (Exception) {
				this.Error = true;
				
			}
		}
		
		private IntPtr LoadLibraryWDetour(IntPtr lpModuleName)
		{
			
			try {
				string lpModName = Marshal.PtrToStringUni(lpModuleName);
				if (lpModName != null && lpModName != ""){
					if(HookManager.NeedsToBeCloaked(lpModName)) {
						HookManager.Log("[LoadLibraryWDetour] Found & cloaked: " + lpModName);
						return IntPtr.Zero;
					} else {
						if(!HookManager.IsWhiteListedFileName(lpModName)) HookManager.Log("[LoadLibraryWDetour] File wasn't found in Directory - not claoking: " + lpModName);
						return LoadLibraryW(lpModuleName);
					}
				}
				return LoadLibraryW(lpModuleName);
				
			} catch (Exception) {
				return IntPtr.Zero;
			}
		}
		
		public void Dispose(){
			
			_hook.Dispose();
		}
		
		
		
	}
}
