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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using EasyHook;

namespace Win32Hooks
{
	public class HookManager
	{
		List<IHook> _controllerList;
		static string[] filesNameToHide;
		
		public delegate void Message(string msg, Color? col);
		public static event Message OnMessage;
		protected static readonly object _lock = new object();
		
		public string AccountName { get; set; }
		public string CharName { get; set; }
		public string Password { get; set; }
		public bool  UseRedGuard { get; set; }
		public bool HideHookManager { get; set; }
		public bool UseAdaptEve { get; set; }
		public string PipeName { get; set; }
		public Injector.HWSettings HWSettings { get; set; }
		
		//string[] args = new string[] {this.AccountName,this.CharacterName,this.Password,this.UseRedGuard.ToString(),this.HideHookManager.ToString(), this.UseAdaptEve.ToString(), WCFServer.Instance.GetPipeName};
		
		[DllImport("kernel32.dll")]
		static extern bool SetProcessWorkingSetSize(IntPtr hProcess, uint
		                                            dwMinimumWorkingSetSize, uint dwMaximumWorkingSetSize);
		[DllImport("kernel32.dll")]
		static extern IntPtr GetCurrentProcess();
		
		[DllImport("user32.dll")]
		public static extern IntPtr FindWindow(string lpClassName,string lpWindowName);
		
		[DllImport("user32.dll", CharSet=CharSet.Unicode)]
		static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);
		
		[DllImport("user32.dll", SetLastError=true)]
		static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
		
		public HookManager()
		{
			
			
			
		}
		
		public void InitializeHookManager(){
			
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
			
			
			string currentAssemblyPathSettings = AssemblyPath + "\\EveSettings\\";
			newPathLocalAppData = currentAssemblyPathSettings + AccountName + "_AppData\\";
			newPathPersonal = currentAssemblyPathSettings + AccountName + "_Personal\\";
			string eveExecutionDir = System.IO.Directory.GetCurrentDirectory();
			
			
			if(newPathLocalAppData != null && newPathPersonal != null) {
				_controllerList.Add(new SHGetFolderPathAController());
				_controllerList.Add(new SHGetFolderPathWController());
				
			}
		}
		
		
		string _assemblyPath;
		public string AssemblyPath{
			get {
				if (_assemblyPath == null) {
					
					_assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				}
				return _assemblyPath;
			}
		}
		
		public string newPathPersonal = null;
		public string newPathLocalAppData = null;
		
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
		public void DisposeHooks()
		{
			foreach (var controller in _controllerList) {

				if(controller != null && !controller.Name.Equals("SHGetFolderPathAController") && !controller.Name.Equals("SHGetFolderPathWController")
				   && !controller.Name.Equals("WinSockConnectController") && !controller.Name.Equals("RegQueryValueExAController")
				   && !controller.Name.Equals("GlobalMemoryStatusController")&& !controller.Name.Equals("GetAdaptersInfoController")
				   && !controller.Name.Equals("DX9Controller") && !controller.Name.Equals("WinSockConnectController")){

					Log("Disposing Controller: " + controller.Name);
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
		
		
		public Thread t = null;
		public AppDomain QAppDomain = null;
		public void LaunchAppDomain(int param){
			
			UnloadAppDomain();
			QAppDomain = AppDomain.CreateDomain("QAppDomain");
			AppDomain.MonitoringIsEnabled = true;
			
			string path = Win32Hooks.HookManager.Instance.AssemblyPath;
			
			switch(param){
				case 0:
					//QuestorManagerDomain.ExecuteAssembly(assemblyFolder + "\\Questor\\Questor.exe",args: new String[] {"-i"});
					t = new Thread(delegate() { QAppDomain.ExecuteAssembly(path + "\\Questor\\Questor.exe",args: new String[] {"-i","-c", Win32Hooks.HookManager.Instance.CharName,"-u", Win32Hooks.HookManager.Instance.AccountName, "-p", Win32Hooks.HookManager.Instance.Password}); });
					t.Start();
					break;
				case 1:
					t = new Thread(delegate() { QAppDomain.ExecuteAssembly(path + "\\Questor\\QuestorManager.exe",args: new String[] {"-i"}); });
					t.Start();
					break;
				case 2:
					t = new Thread(delegate() { QAppDomain.ExecuteAssembly(path + "\\Questor\\ValueDump.exe",args: new String[] {"-i"}); });
					t.Start();
					break;
				default:
					break;
			}
			
		}
		
		public void UnloadAppDomain(){
			//Win32Hooks.HookManager.Instance.DisposeHooks();
			if(t != null && t.IsAlive){
				Thread.Sleep(1);
				t.Abort();
			}
			if(QAppDomain != null) {
				AppDomain.Unload(QAppDomain);
				QAppDomain = null;
			}
		}
		
		
		public void InitHooks(){
			
			Utility.Utility.LoadLibrary("blue.dll");
			Utility.Utility.LoadLibrary("python27.dll");
			Utility.Utility.LoadLibrary("WS2_32.dll");
			Utility.Utility.LoadLibrary("kernel32.dll");
			Utility.Utility.LoadLibrary("advapi32.dll");
			Utility.Utility.LoadLibrary("Iphlpapi.dll");
			Utility.Utility.LoadLibrary("dbghelp.dll");
			Utility.Utility.LoadLibrary("_ctypes.pyd");
			//Utility.Utility.LoadLibrary("d3d11.dll");
			Utility.Utility.LoadLibrary("d3d9.dll");
			//[DllImport("d3d9.dll")]
			
			
			if(UseAdaptEve){ // adapteve

				EnvVars.SetEnvironment(HWSettings);
				string[] proxyIpPort = HWSettings.ProxyIP.Split(':');
				AddController(new WinSockConnectController(LocalHook.GetProcAddress("WS2_32.dll", "connect"),proxyIpPort[0], proxyIpPort[1], HWSettings.ProxyUsername, HWSettings.ProxyPassword));
				AddController(new RegQueryValueExAController(LocalHook.GetProcAddress("advapi32.dll", "RegQueryValueExA"),HWSettings.WindowsKey));
				AddController(new GlobalMemoryStatusController(LocalHook.GetProcAddress("kernel32.dll", "GlobalMemoryStatusEx"), HWSettings.TotalPhysRam));
				AddController(new GetAdaptersInfoController(LocalHook.GetProcAddress("Iphlpapi.dll", "GetAdaptersInfo"), HWSettings.NetworkAdapterGuid, HWSettings.MacAddress, HWSettings.NetworkAddress));
				AddController(new DX9Controller(HWSettings));
			}
			
			AddController(new Win32Hooks.IsDebuggerPresentController());
			AddController(new Win32Hooks.LoadLibraryAController());
			AddController(new Win32Hooks.LoadLibraryWController());
			AddController(new Win32Hooks.GetModuleHandleWController());
			AddController(new Win32Hooks.GetModuleHandleAController());
			AddController(new Win32Hooks.EnumProcessesController());
			AddController(new Win32Hooks.MiniWriteDumpController());
			
			AddController(new Win32Hooks.CreateFileWController());
			AddController(new Win32Hooks.CreateFileAController());
			

			
			
			if(!EverythingHooked()) {
				MessageBox.Show("Hook error");
				Environment.Exit(0);
				Environment.FailFast("exit");
			}
			Win32Hooks.HookManager.Log("-----------Hooks initialized-----------");
		}
		
		
		public void WaitForRedGuard(){
			
			if(UseRedGuard){
				
				IntPtr hwndRedGuard = IntPtr.Zero;
				int i=0;
				while(hwndRedGuard == IntPtr.Zero)
				{
					if(i>=150) return;
					hwndRedGuard=FindWindow(null,"Red Guard Account Selection");
					Application.DoEvents();
					Thread.Sleep(200);
					i++;
				}
			}
			
		}
		
		private bool doOnceStartQuestorManagerThread = true;
		public void WaitForEVE() {
			bool ready = false;
			int i=0;
			int currentPid = Process.GetCurrentProcess().Id;
			uint hWndpid = 0;
			while(!ready && doOnceStartQuestorManagerThread)
			{
				if(i>=150) return;
				IntPtr hwndEVE=FindWindow(null,"EVE");
				GetWindowThreadProcessId(hwndEVE, out hWndpid);
				if(currentPid == hWndpid)
					ready = true;
				Application.DoEvents();
				Thread.Sleep(200);
				i++;
			}
			
		}
		
		
		
		public static void Log(string text, Color? col = null){
			lock(_lock){
				Thread thread = new Thread(delegate() { _Log(text, col); });
				thread.Start();
			}
		}
		
		private static void _Log(string text, Color? col)
		{
			try {
				if (OnMessage != null)
				{
					OnMessage(text, col);
				}
			} catch (Exception) {
			}
			
		}
		
		private static readonly HookManager _instance = new HookManager();
		public static HookManager Instance
		{
			get { return _instance; }
		}
	}
}
