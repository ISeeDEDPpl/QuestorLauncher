/*
* ---------------------------------------
* User: duketwo
* Date: 29.12.2013
* Time: 21:20
* 
* ---------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using EasyHook;
using Win32Hooks;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Runtime.CompilerServices;

namespace HookManager
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		
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
		
		
		Thread QuestorThread = null;
		AppDomain QuestorManagerDomain = null;
		Win32Hooks.HookManager hookManager;
		private string[] _args;
		private object _lock;
		
		
		
		string _assemblyPath;
		string AssemblyPath{
			get {
				if (_assemblyPath == null) {
					
					_assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				}
				return _assemblyPath;
			}
		}
		
		public MainForm(string[] args)
		{
			_args = args;
			
			// 									0				1					2					3						4		
			//string[] args = new string[] {this.AccountName,this.CharacterName,this.Password,this.UseRedGuard.ToString(),this.HideHookManager.ToString()};
			
			if(args.Length > 0) {
				string currentAssemblyPathSettings = AssemblyPath + "\\EveSettings\\";
				Win32Hooks.HookManager.newPathLocalAppData = currentAssemblyPathSettings + args[0] + "_AppData\\";
				Win32Hooks.HookManager.newPathPersonal = currentAssemblyPathSettings + args[0] + "_Personal\\";
				string eveExecutionDir = System.IO.Directory.GetCurrentDirectory();
				AppDomain.MonitoringIsEnabled = true;
				
			} else {
				Environment.Exit(0);
			}
			
			InitializeComponent();
			Win32Hooks.HookManager.OnMessage += ThreadSafeAddlog;
			InitHooks();
			RemoteHooking.WakeUpProcess();
			
			if(Boolean.Parse(_args[3])){
				
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
			Text = "HookManager [" + _args[1] + "]";	
			
			_StartQuestor();
	
		}
	
		public void ThreadSafeAddlog(string str){
			if(this.InvokeRequired){
				this.Invoke( new Action(() => AddLog(str) ));
				
			} else {
				AddLog(str);
			}
		}
		
		void AddLog(string msg){
			
			try {
				
				msg = DateTime.UtcNow.ToString() + " " + msg;
				
				if (logbox.Items.Count >= 10000)
				{
					logbox.Items.Clear();
				}
				logbox.Items.Add(msg);
				
				using (StreamWriter w = File.AppendText(AssemblyPath + "\\" + _args[0] + "-HookManager.log"))
				{
					w.WriteLine(msg);
				}
				
				if(logbox.Items.Count>1)
					logbox.SelectedIndex = logbox.Items.Count - 1;
				
			} catch (Exception) {
				
				
			}
			
			
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			
		}
		
		
		void InitHooks(){
			
			hookManager = new Win32Hooks.HookManager();
			
			//hookManager.AddController(new Win32Hooks.IsDebuggerPresentController());
			hookManager.AddController(new Win32Hooks.LoadLibraryAController()); 
			hookManager.AddController(new Win32Hooks.LoadLibraryWController());
			hookManager.AddController(new Win32Hooks.GetModuleHandleWController());
			hookManager.AddController(new Win32Hooks.GetModuleHandleAController());
			//hookManager.AddController(new Win32Hooks.EnumProcessesController());
			//hookManager.AddController(new Win32Hooks.MiniWriteDumpController());
			
			hookManager.AddController(new Win32Hooks.CreateFileWController());
			hookManager.AddController(new Win32Hooks.CreateFileAController());
			
			
			if(!hookManager.EverythingHooked()) {
				MessageBox.Show("Hook error");
				Environment.Exit(0);
				Environment.FailFast("exit");
			}
			Win32Hooks.HookManager.Log("-----------Hooks initialized-----------");
		}
		
		void DisposeHooks(){
			if(hookManager != null){
				hookManager.Dispose();
				hookManager = null;
				Win32Hooks.HookManager.Log("-----------Hooks disposed-----------");
			}
		}
		
		private bool doOnceStartQuestorManagerThread = true;
		
		void StartQuestorThread(int param){
			
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
			
			doOnceStartQuestorManagerThread = false;
			
			string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			
			switch(param){
				case 0:
					QuestorManagerDomain.ExecuteAssembly(assemblyFolder + "\\Questor\\Questor.exe",args: new String[] {"-i","-c", _args[1],"-u", _args[0], "-p", _args[2]});
					break;
				case 1:
					QuestorManagerDomain.ExecuteAssembly(assemblyFolder + "\\Questor\\QuestorManager.exe",args: new String[] {"-i"});
					break;
				case 2:
					QuestorManagerDomain.ExecuteAssembly(assemblyFolder + "\\Questor\\ValueDump.exe",args: new String[] {"-i"});
					break;
				default: 
					break;
		} 
			
		}
		
		void StartQuestor(){
			if(QuestorThread == null) {
				DisposeHooks();
				_StartQuestor();
				InitHooks();
			}
		}
		
		void _StartQuestor(){
			if(QuestorThread == null) {
				QuestorManagerDomain = AppDomain.CreateDomain("QuestorDomain");
				QuestorThread = new Thread(delegate() { StartQuestorThread(0); });
				QuestorThread.Start();
			}
		}
		
		void StartQuestorManager(){
			
			if(QuestorThread == null) {
				DisposeHooks();
				QuestorManagerDomain = AppDomain.CreateDomain("QuestorDomain");
				QuestorThread = new Thread(delegate() { StartQuestorThread(1); });
				QuestorThread.Start();
				InitHooks();
			}
		}
		
		void StartValueDump(){
			
			if(QuestorThread == null) {
				DisposeHooks();
				QuestorManagerDomain = AppDomain.CreateDomain("QuestorDomain");
				QuestorThread = new Thread(delegate() { StartQuestorThread(2); });
				QuestorThread.Start();
				InitHooks();
			}
		}
		
		void UnloadQuestorAppDomain(){
			if(QuestorThread != null){
				while(QuestorThread.IsAlive){
					
					Thread.Sleep(2);
					QuestorThread.Abort();
				}
				DisposeHooks();
				AppDomain.Unload(QuestorManagerDomain);
				QuestorManagerDomain = null;
				QuestorThread = null;
			}
		}
		
		void Button1Click(object sender, EventArgs e){
			
			UnloadQuestorAppDomain();
			StartQuestor();
			
			
		}
		
		void Button2Click(object sender, EventArgs e){
			UnloadQuestorAppDomain();
		}
		
		void Button3Click(object sender, System.EventArgs e)
		{
			UnloadQuestorAppDomain();
			StartQuestorManager();
		}
		void Button4Click(object sender, System.EventArgs e)
		{
			UnloadQuestorAppDomain();
			StartValueDump();
		}
		
		
		
		private bool doOnceTimerAppDomainMemoryTick = true;
		void TimerAppDomainMemoryTick(object sender, EventArgs e)
		{
			if(doOnceTimerAppDomainMemoryTick) {
				uint min = 104857600;
				uint max = 838860800;
				AddLog("SetProcessWorkingSetSize()");
				SetProcessWorkingSetSize(GetCurrentProcess(), min, max);
				doOnceTimerAppDomainMemoryTick = false;
				
			}
			
			if(QuestorManagerDomain != null) {
				
				labelTotalAllocated.Text = Math.Round(QuestorManagerDomain.MonitoringTotalAllocatedMemorySize/1048576D,2).ToString() + " mb";
				labelSurvived.Text = Math.Round(QuestorManagerDomain.MonitoringSurvivedMemorySize/1048576D,2).ToString() + " mb";
			}
		}
		
		void MainFormShown(object sender, EventArgs e)
		{
			if(bool.Parse(_args[4]))
				this.Hide();
		}
		
		
	}
}
