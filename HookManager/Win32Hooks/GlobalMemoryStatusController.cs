/*
 * ---------------------------------------
 * User: duketwo
 * Date: 21.06.2014
 * Time: 16:48
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
	/// Description of GlobalMemoryStatusController.
	/// </summary>
	public class GlobalMemoryStatusController : IDisposable, IHook
	{
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate bool GlobalMemoryStatusDelegate(IntPtr memStruct);

		private string _name;
		private LocalHook _hook;
		public bool Error { get; set; }
		public string Name  { get; set; }

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GlobalMemoryStatusEx(IntPtr memStruct);

		public GlobalMemoryStatusController(IntPtr address, ulong totalPhys)
		{
			_totalPhys = totalPhys;
			Name = typeof(GlobalMemoryStatusController).Name;
			
			try
            {
				_name = string.Format("MemoryHook{0:X}", address.ToInt32());
				_hook = LocalHook.Create(address, new GlobalMemoryStatusDelegate(GlobalMemoryStatusDetour), this);
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });
				
			}
            catch (Exception)
            {	
				Error = true;
			}
			
		}

		private ulong _totalPhys;
		private MEMORYSTATUSEX _struct;
		private bool GlobalMemoryStatusDetour(IntPtr memStruct)
		{
			if (_struct == null)
			{
				var result = GlobalMemoryStatusEx(memStruct);
				_struct = (MEMORYSTATUSEX)Marshal.PtrToStructure(memStruct, typeof(MEMORYSTATUSEX));
				var before = ((_struct.ullTotalPhys/1024)/1024);
				_struct.ullTotalPhys = _totalPhys * 1024 * 1024;
				var after = ((_struct.ullTotalPhys/1024)/1024);
				global::HookManager.Win32Hooks.HookManager.Log("[GlobalMemoryStatusDetour] Memorysize was called. Before: " + before.ToString() +  " After: " + after.ToString(), Color.Orange);
			}
			
			Marshal.StructureToPtr(_struct, memStruct, true);
			return true;
		}

		[StructLayout(LayoutKind.Sequential)]
		public class MEMORYSTATUSEX
		{
			public uint dwLength;
			public uint dwMemoryLoad;
			public ulong ullTotalPhys;
			public ulong ullAvailPhys;
			public ulong ullTotalPageFile;
			public ulong ullAvailPageFile;
			public ulong ullTotalVirtual;
			public ulong ullAvailVirtual;
			public ulong ullAvailExtendedVirtual;
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
