using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HookManager;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.CompilerServices;

namespace Win32Hooks
{
	public class HookManager : IDisposable
	{
		List<IHook> _controllerList;
		static string[] filesNameToHide;
		
		public delegate void Message(string msg);
		public static event Message OnMessage;
		protected static readonly object _lock = new object();
		
		public HookManager()
		{
			
			_controllerList = new List<IHook>();
			
			filesNameToHide = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.*");
			
			DirectoryWhiteListRead.Add(Environment.GetFolderPath(Environment.SpecialFolder.Windows)); // win folder
			DirectoryWhiteListRead.Add(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)); // folder of current assembly
			DirectoryWhiteListWrite.Add(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)); // folder of current assembly
			DirectoryWhiteListRead.Add(Directory.GetParent(Environment.CurrentDirectory).ToString()); // eve folder
			DirectoryWhiteListWrite.Add(Directory.GetParent(Environment.CurrentDirectory).ToString() + "\\bulkdata"); // eve folder bulkdata write
			DirectoryWhiteListRead.Add(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).ToString()); // appdata for whatever reason (rg using IE stuff)
			DirectoryWhiteListWrite.Add(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).ToString()); // appdata for whatever reason (rg using IE stuff)
			DirectoryWhiteListRead.Add("\\\\.\\mailslot"); // rg's ipc
			DirectoryWhiteListWrite.Add("\\\\.\\mailslot"); // rg's ipc
			if(newPathLocalAppData != null && newPathPersonal != null) {
				_controllerList.Add(new SHGetFolderPathAController());
				_controllerList.Add(new SHGetFolderPathWController());
				
			}
			
		}
		
		public static string newPathPersonal = null;
		public static string newPathLocalAppData = null;
		
		public static bool IsWhiteListedReadDirectory(string path){
			if (path == string.Empty || path == null || path == "") return false;
			foreach(string dir in DirectoryWhiteListRead){
				if (path.Contains(dir)) return true;
			}
			return false;
		}
		
		public static bool IsWhiteListedWriteDirectory(string path){
			if (path == string.Empty || path == null || path == "") return false;
			foreach(string dir in DirectoryWhiteListWrite){
				if (path.Contains(dir)) return true;
			}
			return false;
		}
		
		public static bool IsBacklistedDirectory(string path){
			if (path == string.Empty || path == null || path == "") return false;
			foreach(string dir in DirectoryBlackList){
				if (path.Contains(dir)) return true;
			}
			return false;
		}
		
		public static bool IsWhiteListedFileName(string lpModName){
			int fileExtPos = lpModName.LastIndexOf(".");
			string lpModNameWithoutExtension = (fileExtPos >= 0 ) ? lpModName.Substring(0, fileExtPos) : lpModName;
			return (FileWhiteList.Contains(lpModNameWithoutExtension.ToLower())) ? true : false;
		}
		
		public static bool NeedsToBeCloaked(string lpModName){
			List<string> found = filesNameToHide.Where(e => e.IndexOf(lpModName) != -1).ToList();
			return (found.Any()) ? true : false;
		}
		
	
		public static List<string> FileWhiteList = new List<string>(new string[] { "comctl32", "kernel32", "dbghelp", "wtsapi", "ntdll", "psapi", "blue", "python27" });
		public static List<string> ExeNamesToHide = new List<string>(new string[] { "exefile", "injector", "teamviewer", "tor", "notepad++", "sharpdevelop", "evetimer", "l0rn", "questor" });
		
		public static List<string> DirectoryWhiteListRead = new List<string>(new string[] { } );
		public static List<string> DirectoryWhiteListWrite = new List<string>(new string[] { } );
		public static List<string> DirectoryBlackList = new List<string>(new string[] { } );
		
		public void AddController(IHook controller)
		{
			if (!_controllerList.Contains(controller))
				_controllerList.Add(controller);
		}
		public void RemoveController(IHook controller)
		{
			_controllerList.Remove(controller);
		}
		public void Dispose()
		{
			foreach (var controller in _controllerList) {
		
				if(controller != null && !controller.Name.Equals("SHGetFolderPathAController") && !controller.Name.Equals("SHGetFolderPathWController")){
					
					controller.Dispose();
				}	
			}
		}
		
		public bool EverythingHooked(){
			
			foreach (var controller in _controllerList)
			{
				if (controller != null)
				{
					if(controller.Error) return false;
				}
			}
			return true;
		}
		
		
		public static void Log(string text){
			lock(_lock){
				Thread thread = new Thread(delegate() { _Log(text); });
				thread.Start();
			}
		}
		
		private static void _Log(string text)
		{
			try {
				if (OnMessage != null)
				{
					OnMessage(text);
				}
			} catch (Exception) {
			}
			
		}
	}
}
	