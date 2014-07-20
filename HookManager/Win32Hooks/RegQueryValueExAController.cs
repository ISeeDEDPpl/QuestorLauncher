/*
 * ---------------------------------------
 * User: duketwo
 * Date: 21.06.2014
 * Time: 15:03
 * 
 * ---------------------------------------
 */

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using EasyHook;

namespace HookManager.Win32Hooks
{
	/// <summary>
	/// Description of RegistryController.
	/// </summary>
	/// 
	
	public class RegQueryValueExAController : IDisposable, IHook {
		
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
		private delegate int RegQueryValueExADelegate(UIntPtr hKey, IntPtr lpValueName, int lpReserved, IntPtr lpType, IntPtr lpData, IntPtr lpcbData);

		private string _name;
		private LocalHook _hook;
		public bool Error { get; set; }
		public string Name  { get; set; }

		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern int RegQueryValueExA(UIntPtr hKey, IntPtr lpValueName, int lpReserved, IntPtr lpType, IntPtr lpData, IntPtr lpcbData);

		private string _prodKey;
		public RegQueryValueExAController(IntPtr address, string prodKey)
		{
			_prodKey = prodKey;
			this.Name = typeof(RegQueryValueExAController).Name;
			
			try 
            {	
				_name = string.Format("RegQueryValueExAHook_{0:X}", address.ToInt32());
				_hook = LocalHook.Create(address, new RegQueryValueExADelegate(RegQueryValueExADetour), this);
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 1 });	
			} 
            catch (Exception)
            {	
				Error = true;
			}
		}

		private int RegQueryValueExADetour(UIntPtr hKey, IntPtr lpValueName, int lpReserved, IntPtr lpType, IntPtr lpData, IntPtr lpcbData)
		{
			int result = RegQueryValueExA(hKey, lpValueName, lpReserved, lpType, lpData, lpcbData);

			string keyValue = Marshal.PtrToStringAnsi(lpValueName);
			string lpDataString = Marshal.PtrToStringAnsi(lpData);
			bool log = false;
			if (keyValue == "ProductId")
			{
				log = true;
				var returnValue = Marshal.PtrToStringAnsi(lpData);
				IntPtr newValue = IntPtr.Zero;
				try
				{
					newValue = Marshal.StringToHGlobalAnsi(_prodKey);
					var size = Marshal.ReadInt32(lpcbData);
					Utility.Utility.CopyMemory(lpData, newValue, (uint)size);
				}
				finally
				{
					if (newValue != IntPtr.Zero)
						Marshal.FreeHGlobal(newValue);
				}
			}

			string lpDataStringAfter = Marshal.PtrToStringAnsi(lpData);
			if (log)
            {
				global::HookManager.Win32Hooks.HookManager.Log("[RegQueryValueExADetour] Before: " + lpDataString.ToString(), Color.Orange);
				global::HookManager.Win32Hooks.HookManager.Log("[RegQueryValueExADetour] After: " + lpDataStringAfter.ToString(), Color.Orange);
			}

			return result;
		}

		public void Dispose()
		{
			if (_hook == null)
				return;

			_hook.Dispose();
			_hook = null;
		}
	}
}
