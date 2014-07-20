﻿/*
* ---------------------------------------
* User: duketwo
* Date: 12.12.2013
* Time: 12:51
* 
* ---------------------------------------
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using EasyHook;

namespace HookManager.Win32Hooks
{
	/// <summary>
	/// Description of IsDebuggerPresent.
	/// </summary>
	/// 
	
	
	public class EnumProcessesController : IHook, IDisposable
	{
		[DllImport("psapi")]
		private static extern bool EnumProcesses([In] [Out] IntPtr processIds, uint arrayBytesSize, [In] [Out] IntPtr bytesCopied);
		
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern void SetLastError(int errorCode);
		private delegate bool EnumProcessesDelegate([In] [Out] IntPtr processIds, uint arrayBytesSize, [In] [Out] IntPtr bytesCopied);
		private LocalHook _hook;
		public bool Error { get; set; }
		public string Name  { get; set; }
		private int currentEvePID = -1;
		List<int> ExePidsBefore = new List<int>();
		public EnumProcessesController()
		{
			Error = false;
			Name = typeof(EnumProcessesController).Name;
			try 
            {
				currentEvePID = Process.GetCurrentProcess().Id;
				_hook = LocalHook.Create(
					LocalHook.GetProcAddress("kernel32.dll", "K32EnumProcesses"),
					new global::HookManager.Win32Hooks.EnumProcessesController.EnumProcessesDelegate(EnumProcessesDetour),
					this);
				
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 1 });
				Error = false;
			}
            catch (Exception)
            {
				Error = true;
			}
		}
		
		private bool EnumProcessesDetour([In] [Out] IntPtr processIds, uint arrayBytesSize, [In] [Out] IntPtr bytesCopied)
		{
			global::HookManager.Win32Hooks.HookManager.Log("---------EnumProcessesDetourStart------");
			bool success = EnumProcesses(processIds,arrayBytesSize,bytesCopied);
			List<int> exeFilePids = new List<int>();
			
			foreach(string exeName in global::HookManager.Win32Hooks.HookManager.ExeNamesToHide)
            {
				Process [] exeFiles = Process.GetProcessesByName(exeName);
				foreach(Process exeFile in exeFiles)
                {
					if(exeFile.Id != currentEvePID)
                    {
						exeFilePids.Add(exeFile.Id);
					}
				}
			}
			
			uint bytesCopiedUint = (uint)Marshal.ReadIntPtr(bytesCopied);
			uint pIDsCopied = bytesCopiedUint/sizeof(uint);
			uint pidsDeleted = 0;
			
			uint i=0;
			while(i<pIDsCopied){
				uint currentPID = (uint)Marshal.ReadIntPtr(processIds,(int)i*sizeof(uint));
				if(exeFilePids.Contains((int)currentPID))
                {
					pidsDeleted++;
					Marshal.WriteIntPtr(processIds,sizeof(uint) * (int)i,(IntPtr)0);	
					
					for(uint b=i;b<(pIDsCopied-1);b++)
                    {
						uint nextPID = (uint)Marshal.ReadIntPtr(processIds,(int)(b+1)*sizeof(uint));
						Marshal.WriteIntPtr(processIds,sizeof(uint) * (int)b,(IntPtr)nextPID);
					}

					continue;
				} 
				i++;
			}
			Marshal.WriteIntPtr(bytesCopied,0,(IntPtr)((pIDsCopied-pidsDeleted)*sizeof(uint)));
			global::HookManager.Win32Hooks.HookManager.Log("---------EnumProcessesDetourEnd------");
			return success;			
		}
		
		public void Dispose()
        {	
			_hook.Dispose();
		}
	}
}
