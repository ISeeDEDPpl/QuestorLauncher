/*
 * ---------------------------------------
 * User: duketwo
 * Date: 02.06.2014
 * Time: 19:29
 * 
 * ---------------------------------------
 */

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HookManager.Win32Hooks
{
	/// <summary>
	/// Description of Stealthtest.
	/// </summary>
	public static class Stealthtest
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
		public static extern IntPtr LoadLibraryA(IntPtr lpModuleName);
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr LoadLibraryW(IntPtr lpModuleName);
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
		public static extern IntPtr GetModuleHandleA(IntPtr lpModuleName);
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr GetModuleHandleW(IntPtr lpModuleName);
		[DllImport("kernel32", SetLastError=true, CharSet = CharSet.Unicode)]
		static extern IntPtr LoadLibrary(string lpFileName);
		[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr GetModuleHandle(string lpModuleName);

		[DllImport("psapi.dll")]
		private static extern bool EnumProcesses(
			[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)] [In][Out] UInt32[] processIds,
			UInt32 arraySizeBytes,
			[MarshalAs(UnmanagedType.U4)] out UInt32 bytesCopied);
		
		public static void Test()
        {	
			global::HookManager.Win32Hooks.HookManager.Log("[Stealthtest] Starting Stealthtest.");
			global::HookManager.Win32Hooks.HookManager.Log("[Stealthtest] Enumprocesses start.");
			
			UInt32 arraySize = 200;
			UInt32 arrayBytesSize = arraySize * sizeof(UInt32);
			UInt32[] processIds = new UInt32[arraySize];
			UInt32[] processIdsToReturn = new UInt32[arraySize];
			UInt32 bytesCopied;
			

			bool success = EnumProcesses(processIds, arrayBytesSize, out bytesCopied);

			if (!success) return;
			if (0 == bytesCopied) return;
			
			int numIdsCopied = Convert.ToInt32(bytesCopied/4);
			int currentEvePID = Process.GetCurrentProcess().Id;


		    for(int i = 0; i < numIdsCopied; i++)
			{
			    int processID = Convert.ToInt32(processIds[i]);
			    global::HookManager.Win32Hooks.HookManager.Log("ProcID i: (" +  i  + ") " + processID + " ExeName: " + Process.GetProcessById(processID).ProcessName);
			}

		    global::HookManager.Win32Hooks.HookManager.Log("[Stealthtest] Enumprocesses end.");
			
			string[] dllNames = {"EveDiscovery", "Utility.dll", "EasyHook.dll", "AppDomainHandler.dll", "NotExisting.dll", "HookManager.exe", "Questor.exe", "DirectEve.dll" };
			
			foreach(string dllName in dllNames)
            {
				IntPtr ptr=LoadLibrary(dllName);
				if (ptr  != IntPtr.Zero) {
					global::HookManager.Win32Hooks.HookManager.Log("[Stealthtest-LoadLibrary] Handle found for " + dllName);
				} else {
					global::HookManager.Win32Hooks.HookManager.Log("[Stealthtest-LoadLibrary] Handle NOT found for " + dllName);
				}
				
				ptr= IntPtr.Zero;
				
				ptr=GetModuleHandle(dllName);
				if (ptr  != IntPtr.Zero) {
					global::HookManager.Win32Hooks.HookManager.Log("[Stealthtest-GetModuleHandle] Handle found for: " + dllName);
				} else {
					global::HookManager.Win32Hooks.HookManager.Log("[Stealthtest-GetModuleHandle] Handle not found for: " + dllName);
				}
				
				ptr= IntPtr.Zero;
			}
		}
	}
}
