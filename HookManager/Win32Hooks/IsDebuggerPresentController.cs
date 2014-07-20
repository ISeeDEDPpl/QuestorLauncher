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

namespace HookManager.Win32Hooks
{
	/// <summary>
	/// Description of IsDebuggerPresent.
	/// </summary>
	public class IsDebuggerPresentController : IHook, IDisposable
	{
		
		private delegate bool IsDebuggerPresentDelegate();
		private LocalHook _hook;
		public bool Error { get; set; }
		public string Name  { get; set; }
		
		public IsDebuggerPresentController()
		{
			Error = false;
			Name = typeof(IsDebuggerPresentController).Name;
			
			try 
            {
				_hook = LocalHook.Create(
					LocalHook.GetProcAddress("kernel32.dll", "IsDebuggerPresent"),
					new global::HookManager.Win32Hooks.IsDebuggerPresentController.IsDebuggerPresentDelegate(IsDebuggerPresentDetour),
					this);
				
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 1 });
				Error = false;
			}
            catch (Exception)
            {
				Error = true;	
			}
		}
		
		private bool IsDebuggerPresentDetour()
		{
			return false;
		}
		
		public void Dispose()
        {	
			_hook.Dispose();
		}
	}
}
