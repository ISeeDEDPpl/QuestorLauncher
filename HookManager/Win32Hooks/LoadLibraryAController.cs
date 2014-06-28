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
using HookManager;


namespace Win32Hooks
{
	/// <summary>
	/// Description of IsDebuggerPresent.
	/// </summary>
	/// 
	
	
	public class LoadLibraryAController : IHook, IDisposable
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
		public static extern IntPtr LoadLibraryA(IntPtr lpModuleName);
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern void SetLastError(int errorCode);
		private delegate IntPtr LoadLibraryADelegate(IntPtr lpModuleName);
		private LocalHook _hook;
		public bool Error { get; set; }
		public string Name  { get; set; }
		
		public LoadLibraryAController()
		{
			this.Error = false;
			this.Name = typeof(LoadLibraryAController).Name;
			HookManager.Log(this.Name);
			
			try {
				this._hook = LocalHook.Create(
					LocalHook.GetProcAddress("Kernel32.dll", "LoadLibraryA"),
					new Win32Hooks.LoadLibraryAController.LoadLibraryADelegate(LoadLibraryADetour),
					this);
				
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 1 });
				this.Error = false;
			} catch (Exception) {
				this.Error = true;
				
			}
		}
		
		private IntPtr LoadLibraryADetour(IntPtr lpModuleName)
		{
			try {
				string lpModName = Marshal.PtrToStringAnsi(lpModuleName);
				if (lpModName != null && lpModName != ""){
					if(HookManager.NeedsToBeCloaked(lpModName)) {
						HookManager.Log("[LoadLibraryADetour] Found & cloaked: " + lpModName);
						return IntPtr.Zero;
					}
					else {
						if(!HookManager.IsWhiteListedFileName(lpModName)) HookManager.Log("[LoadLibraryADetour] File wasn't found in Directory - not claoking: " + lpModName);
						return LoadLibraryA(lpModuleName);
					}
				}
				return LoadLibraryA(lpModuleName);
				
			} catch (Exception) {
				return IntPtr.Zero;
			}
		}
		
		public void Dispose(){
			
			_hook.Dispose();
		}
	}
}
