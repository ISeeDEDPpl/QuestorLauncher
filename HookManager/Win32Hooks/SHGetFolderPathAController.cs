/*
 * ---------------------------------------
 * User: duketwo
 * Date: 31.12.2013
 * Time: 00:17
 * 
 * ---------------------------------------
 */

using System;
using System.Runtime.InteropServices;
using System.Text;
using EasyHook;

namespace HookManager.Win32Hooks
{
	/// <summary>
	/// Description of SHGetFolderPathWController.
	/// </summary>
	///
	public class SHGetFolderPathAController : IHook, IDisposable
	{
		
		[DllImport("shell32.dll")]
		static extern int SHGetFolderPathA(IntPtr hwndOwner, int nFolder, IntPtr hToken,
		                                   UInt32 dwFlags,[In] [Out] IntPtr pszPath);
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern void SetLastError(int errorCode);
		
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
		delegate int SHGetFolderPathADelegate(IntPtr hwndOwner, int nFolder, IntPtr hToken,
		                                      UInt32 dwFlags, [In][Out] IntPtr pszPath);
		private LocalHook _hook;
		public bool Error { get; set; }
		public string Name  { get; set; }
		
		public SHGetFolderPathAController()
		{
			Error = false;
			Name = typeof(SHGetFolderPathAController).Name;
			try 
            {
				_hook = LocalHook.Create(
					LocalHook.GetProcAddress("shell32.dll", "SHGetFolderPathA"),
					new global::HookManager.Win32Hooks.SHGetFolderPathAController.SHGetFolderPathADelegate(SHGetFolderPathADetour),
					this);
				
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 1 });
				Error = false;
			}
            catch (Exception)
            {
				Error = true;	
			}
		}
		
		private static int SHGetFolderPathADetour(IntPtr hwndOwner, int nFolder, IntPtr hToken, UInt32 dwFlags, [In] [Out] IntPtr pszPath)
		{
			int ret = SHGetFolderPathA(hwndOwner,nFolder,hToken,dwFlags,pszPath);
            if (nFolder == 0x0005 && global::HookManager.Win32Hooks.HookManager.Instance.newPathPersonal != null) // PERSONAL
            { 
				string str = global::HookManager.Win32Hooks.HookManager.Instance.newPathPersonal  + Char.MinValue;
				byte[] buffer = ASCIIEncoding.ASCII.GetBytes(str);
				for(int i =0; i<buffer.Length; i++)
                {
					Marshal.WriteByte(pszPath,i,buffer[i]);
				}
			}

            if (nFolder == 0x001c && global::HookManager.Win32Hooks.HookManager.Instance.newPathLocalAppData != null) // LOCAL APP DATA
            {
				string str = global::HookManager.Win32Hooks.HookManager.Instance.newPathLocalAppData  + Char.MinValue;
				byte[] buffer = ASCIIEncoding.ASCII.GetBytes(str);
				for(int i =0; i<buffer.Length; i++)
                {
					Marshal.WriteByte(pszPath,i,buffer[i]);
				}
			}

			return ret;
		}
		
		public void Dispose()
        {	
			_hook.Dispose();
		}	
	}
}

