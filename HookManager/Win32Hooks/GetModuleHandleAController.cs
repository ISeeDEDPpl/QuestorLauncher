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
			Error = false;
			Name = typeof(GetModuleHandleAController).Name;
			try 
            {
				_hook = LocalHook.Create(
					LocalHook.GetProcAddress("Kernel32.dll", "GetModuleHandleA"),
					new global::HookManager.Win32Hooks.GetModuleHandleAController.GetModuleHandleADelegate(GetModuleHandleADetour),
					this);
				
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 1 });
				Error = false;
			} 
            catch (Exception)
            {
				Error = true;
			}
		}
		
		private IntPtr GetModuleHandleADetour(IntPtr lpModuleName)
		{
			try 
            {
				string lpModName = Marshal.PtrToStringAnsi(lpModuleName);
				if (!string.IsNullOrEmpty(lpModName))
                {
					if(global::HookManager.Win32Hooks.HookManager.NeedsToBeCloaked(lpModName)) 
                    {
						global::HookManager.Win32Hooks.HookManager.Log("[GetModuleHandleADetour] Found & cloaked: " + lpModName);
						return IntPtr.Zero;
					} 
                    
                    if(!global::HookManager.Win32Hooks.HookManager.IsWhiteListedFileName(lpModName)) global::HookManager.Win32Hooks.HookManager.Log("[GetModuleHandleADetour] File wasn't found in QuestorManager-Directory - not claoking: " + lpModName);
					return GetModuleHandleA(lpModuleName);
				}

				return GetModuleHandleA(lpModuleName);
				
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
