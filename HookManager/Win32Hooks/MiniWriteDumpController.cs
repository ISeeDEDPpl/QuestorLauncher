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
	
	public class MiniWriteDumpController : IHook, IDisposable
	{
		
		[DllImport("kernel32", SetLastError=true, CharSet = CharSet.Unicode)]
		static extern IntPtr LoadLibrary(string lpFileName);
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern void SetLastError(int errorCode);
		private delegate bool MiniDumpWriteDumpDelegate(IntPtr lpModuleName, IntPtr processId, IntPtr hFile, IntPtr dumpType, IntPtr exceptionParam, IntPtr userStreamParam, IntPtr callbackParam);
		private LocalHook _hook;
		public bool Error { get; set; }
		public string Name  { get; set; }
		
		public MiniWriteDumpController()
		{
			Error = false;
			Name = typeof(MiniWriteDumpController).Name;
			IntPtr ptr = IntPtr.Zero;
			while(ptr == IntPtr.Zero)
            {
				ptr = LoadLibrary("DbgHelp.dll");
			}
			
			try 
            {
				_hook = LocalHook.Create(
					LocalHook.GetProcAddress("DbgHelp.dll", "MiniDumpWriteDump"),
					new global::HookManager.Win32Hooks.MiniWriteDumpController.MiniDumpWriteDumpDelegate(MiniDumpWriteDumpDetour),
					this);
				
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 1 });
				Error = false;
			}
            catch (Exception)
            {
				Error = true;	
			}
		}
		
		private bool MiniDumpWriteDumpDetour(IntPtr lpModuleName, IntPtr processId, IntPtr hFile, IntPtr dumpType, IntPtr exceptionParam, IntPtr userStreamParam, IntPtr callbackParam)
		{
			SetLastError(8);
			return false;
		}
		
		public void Dispose()
        {	
			_hook.Dispose();
		}
	}
}
