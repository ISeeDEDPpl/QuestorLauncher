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
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using HookManager;
using System.IO;

namespace Win32Hooks
{
	/// <summary>
	/// Description of IsDebuggerPresent.
	/// </summary>
	public class CreateFileAController : IHook, IDisposable
	{
		[DllImport("kernel32.dll",
		           CharSet = CharSet.Ansi,
		           SetLastError = true,
		           CallingConvention = CallingConvention.StdCall)]
		static extern IntPtr CreateFile(
			String InFileName,
			UInt32 InDesiredAccess,
			UInt32 InShareMode,
			IntPtr InSecurityAttributes,
			UInt32 InCreationDisposition,
			UInt32 InFlagsAndAttributes,
			IntPtr InTemplateFile);
		
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
		delegate IntPtr CreateFileADelegate(
			String InFileName,
			UInt32 InDesiredAccess,
			UInt32 InShareMode,
			IntPtr InSecurityAttributes,
			UInt32 InCreationDisposition,
			UInt32 InFlagsAndAttributes,
			IntPtr InTemplateFile);
		
		private LocalHook _hook;
		public bool Error { get; set; }
		public string Name  { get; set; }
		
		public CreateFileAController()
		{
			this.Error = false;
			this.Name = typeof(CreateFileAController).Name;
			
			try {
				this._hook = LocalHook.Create(
					LocalHook.GetProcAddress("kernel32.dll", "CreateFileA"),
					new Win32Hooks.CreateFileAController.CreateFileADelegate(CreateFileADetour),
					this);
				
				_hook.ThreadACL.SetExclusiveACL(new Int32[] { 1 });
				this.Error = false;
			} catch (Exception) {
				this.Error = true;
				
			}
		}
		static IntPtr CreateFileADetour(
			String InFileName,
			UInt32 InDesiredAccess,
			UInt32 InShareMode,
			IntPtr InSecurityAttributes,
			UInt32 InCreationDisposition,
			UInt32 InFlagsAndAttributes,
			IntPtr InTemplateFile)
		{
			
			string fileName = String.Empty, pathName = String.Empty;
			try {
				
				fileName  = Path.GetFileName(InFileName);
				pathName = Path.GetDirectoryName(InFileName);
				
				
			} catch (Exception e) {
				
				HookManager.Log("CreateFileADetour - Exception:  " + e.ToString());
			}
			

			
			if(HookManager.IsBacklistedDirectory(pathName)){
				HookManager.Log("[-----BLACKLISTED-----] CreateFileADetour-lpFileName: " + pathName + "\\" + fileName + " Desired Access: " + InDesiredAccess.ToString());
			}
			
			if((IsRead(InDesiredAccess) && HookManager.IsWhiteListedReadDirectory(pathName))){
				
			} else {
				
				if(IsWrite(InDesiredAccess) && HookManager.IsWhiteListedWriteDirectory(pathName)) {
					//	HookManager.Log("[Whitelisted] CreateFileADetour-lpFileName(WRITE): " + pathName + "\\" + fileName + " Desired Access: " + InDesiredAccess.ToString());
				}
				
				else {
					
					if(InDesiredAccess != 0){
						HookManager.Log("[not Whitelisted] CreateFileADetour-lpFileName: " + pathName + "\\" + fileName + " Desired Access: " + InDesiredAccess.ToString());
						//return new IntPtr(-1);
					}
				}
			}
			
			IntPtr tmp = CreateFile(InFileName,InDesiredAccess,InShareMode,InSecurityAttributes,InCreationDisposition,InFlagsAndAttributes,InTemplateFile);
			return tmp;
		}
		
		// GENERIC_WRITE = 1073741824
		// GENERIC READ+WRITE = 0xC0000000 -> 3221225472
		// fILE_ATTRIBUTE_TEMPORARY      =  256
		// sTANDARD_RIGHTS_READ      =  131072
		
		public static bool IsRead(UInt32 InDesiredAccess) {
			return (InDesiredAccess == 2147483648 || InDesiredAccess == 131072 || InDesiredAccess == 256) ? true : false;
		}
		public static bool IsWrite(UInt32 InDesiredAccess) {
			return (InDesiredAccess == 1073741824 || InDesiredAccess == 3221225472) ? true : false;
		}
		
		public void Dispose(){
			
			_hook.Dispose();
		}
		
		
		
	}
}
