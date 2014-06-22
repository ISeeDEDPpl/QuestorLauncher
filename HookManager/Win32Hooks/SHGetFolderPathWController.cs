/*
 * ---------------------------------------
 * User: duketwo
 * Date: 31.12.2013
 * Time: 00:17
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
using System.Text;

namespace Win32Hooks
{
	/// <summary>
	/// Description of SHGetFolderPathWController.
	/// </summary>
	///
	public class SHGetFolderPathWController : IHook, IDisposable
	{
		
		[DllImport("shell32.dll")]
		static extern int SHGetFolderPathW(IntPtr hwndOwner, int nFolder, IntPtr hToken,
		                                   UInt32 dwFlags,[In] [Out] IntPtr pszPath);
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern void SetLastError(int errorCode);
		
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		delegate int SHGetFolderPathWDelegate(IntPtr hwndOwner, int nFolder, IntPtr hToken,
		                                      UInt32 dwFlags, [In][Out] IntPtr pszPath);
		private LocalHook _hook;
		public bool Error { get; set; }
		public string Name  { get; set; }
		
	
		
		public SHGetFolderPathWController()
		{
			
			this.Error = false;
			this.Name = typeof(SHGetFolderPathWController).Name;
			try {
				this._hook = LocalHook.Create(
					LocalHook.GetProcAddress("shell32.dll", "SHGetFolderPathW"),
					new Win32Hooks.SHGetFolderPathWController.SHGetFolderPathWDelegate(SHGetFolderPathWDetour),
					this);
				
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 1 });
				this.Error = false;
			} catch (Exception e) {
				HookManager.Log("SHGetFolderPathWController Exception: " + e.ToString());
				this.Error = true;
				
			}
		}
		
		
		private static int SHGetFolderPathWDetour(IntPtr hwndOwner, int nFolder, IntPtr hToken,
		                                          UInt32 dwFlags, [In] [Out] IntPtr pszPath)
		{
			int ret = SHGetFolderPathW(hwndOwner,nFolder,hToken,dwFlags,pszPath);
			if(nFolder == 0x0005 && Win32Hooks.HookManager.Instance.newPathPersonal != null){ // PERSONAL
				
				
				string str = Win32Hooks.HookManager.Instance.newPathPersonal  + Char.MinValue;
				byte[] buffer = UnicodeEncoding.Unicode.GetBytes(str);
				for(int i =0; i<buffer.Length; i++){
					Marshal.WriteByte(pszPath,i,buffer[i]);
				}
			}
			
			if(nFolder == 0x001c && Win32Hooks.HookManager.Instance.newPathLocalAppData != null) { // LOCAL APP DATA

				string str = Win32Hooks.HookManager.Instance.newPathLocalAppData  + Char.MinValue;
				byte[] buffer = UnicodeEncoding.Unicode.GetBytes(str);
				for(int i =0; i<buffer.Length; i++){
					Marshal.WriteByte(pszPath,i,buffer[i]);
				}
			}
			return ret;
		}
		
		public void Dispose(){
			
			_hook.Dispose();
		}
		
		
	}
}

