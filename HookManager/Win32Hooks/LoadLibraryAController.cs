/*
 * ---------------------------------------
 * User: duketwo
 * Date: 12.12.2013
 * Time: 12:51
 * 
 * ---------------------------------------
 */

using System;
using System.Runtime.InteropServices;
using EasyHook;

namespace HookManager.Win32Hooks
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
			Error = false;
			Name = typeof(LoadLibraryAController).Name;
			global::HookManager.Win32Hooks.HookManager.Log(this.Name);
			
			try 
            {
				_hook = LocalHook.Create(
					LocalHook.GetProcAddress("Kernel32.dll", "LoadLibraryA"),
					new global::HookManager.Win32Hooks.LoadLibraryAController.LoadLibraryADelegate(LoadLibraryADetour),
					this);
				
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 1 });
				Error = false;
			} 
            catch (Exception) 
            {
				Error = true;	
			}
		}
		
		private IntPtr LoadLibraryADetour(IntPtr lpModuleName)
		{
			try 
            {
				string lpModName = Marshal.PtrToStringAnsi(lpModuleName);
				if (!string.IsNullOrEmpty(lpModName))
                {
					if(global::HookManager.Win32Hooks.HookManager.NeedsToBeCloaked(lpModName)) 
                    {
						global::HookManager.Win32Hooks.HookManager.Log("[LoadLibraryADetour] Found & cloaked: " + lpModName);
						return IntPtr.Zero;
					}
					
                    if(!global::HookManager.Win32Hooks.HookManager.IsWhiteListedFileName(lpModName)) global::HookManager.Win32Hooks.HookManager.Log("[LoadLibraryADetour] File wasn't found in Directory - not claoking: " + lpModName);
					return LoadLibraryA(lpModuleName);
				}

				return LoadLibraryA(lpModuleName);
			} 
            catch (Exception) 
            {
				return IntPtr.Zero;
			}
		}
		
		public void Dispose()
        {	
			_hook.Dispose();
		}
	}
}
