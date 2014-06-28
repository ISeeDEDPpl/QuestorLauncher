using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyHook;
using Injector;

namespace QuestorLauncher
{
    public class QuestorLauncher
    {
        [DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(IntPtr hProcess, uint
            dwMinimumWorkingSetSize, uint dwMaximumWorkingSetSize);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentProcess();

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);


        Thread QuestorThread = null;
        static AppDomain QuestorManagerDomain = null;
        Win32Hooks.HookManager hookManager;
        public object _lock;
        public static string[] HookManagerParamaters;
        private static QuestorLauncherUI _QuestorLauncherUI;

        private static string _assemblyPath;
        public static string AssemblyPath
        {
            get
            {
                if (_assemblyPath == null)
                {

                    _assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                }
                return _assemblyPath;
            }
        }

        public QuestorLauncher()
        {
            if (HookManagerParamaters.Length > 0) 
            {
			    string currentAssemblyPathSettings = AssemblyPath + "\\EveSettings\\";
                Win32Hooks.HookManager.newPathLocalAppData = currentAssemblyPathSettings + HookManagerParamaters[0] + "_AppData\\";
                Win32Hooks.HookManager.newPathPersonal = currentAssemblyPathSettings + HookManagerParamaters[0] + "_Personal\\";
			    string eveExecutionDir = System.IO.Directory.GetCurrentDirectory();
			    AppDomain.MonitoringIsEnabled = true;
                Cache.OnMessage += ThreadSafeAddlog;
		    } 
            else 
            {
			    Environment.Exit(0);
		    }

            Win32Hooks.HookManager.OnMessage += ThreadSafeAddlog;
			InitHooks();
			RemoteHooking.WakeUpProcess();

            if (Boolean.Parse(HookManagerParamaters[3]))
            {
				
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
            if (Form.ActiveForm != null) Form.ActiveForm.Text = "HookManager [" + HookManagerParamaters[1] + "]";

            _StartQuestor();
            _QuestorLauncherUI = new QuestorLauncherUI() {};
        }
		// 									0				1					2					3						4		
		//string[] args = new string[] {this.AccountName,this.CharacterName,this.Password,this.UseRedGuard.ToString(),this.HideHookManager.ToString()};

        public void ThreadSafeAddlog(string str)
        {
            //if (InvokeRequired)
            //{
            //    Invoke(new Action(() => AddLog(str)));
            //}
            //else
            //{
            //    AddLog(str);
            //}
        }

        public static void AddLog(string msg)
        {
            try
            {
                msg = DateTime.UtcNow.ToString() + " " + msg;

                if (_QuestorLauncherUI.logbox.Items.Count >= 10000)
                {
                    _QuestorLauncherUI.logbox.Items.Clear();
                }
                _QuestorLauncherUI.logbox.Items.Add(msg);

                using (StreamWriter w = File.AppendText(AssemblyPath + "\\" + HookManagerParamaters[0] + "-HookManager.log"))
                {
                    w.WriteLine(msg);
                }

                if (_QuestorLauncherUI.logbox.Items.Count > 1)
                    _QuestorLauncherUI.logbox.SelectedIndex = _QuestorLauncherUI.logbox.Items.Count - 1;

            }
            catch (Exception)
            {


            }
        }

        public void Log(string msg)
        {
            try
            {
                _QuestorLauncherUI.logbox.DataSource = null;
                if (_QuestorLauncherUI.logbox.Items.Count >= 1000)
                {
                    _QuestorLauncherUI.logbox.Items.Clear();
                }

                using (StreamWriter w = File.AppendText(Cache.Instance.AssemblyPath + "\\Injector.log"))
                {
                    w.WriteLine(msg);
                }

                _QuestorLauncherUI.logbox.Items.Add(msg);
                _QuestorLauncherUI.logbox.SelectedIndex = _QuestorLauncherUI.logbox.Items.Count - 1;
            }
            catch (Exception)
            {
                
            }
        }

        void InitHooks()
        {
            try
            {
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


                if (!hookManager.EverythingHooked())
                {
                    MessageBox.Show("Hook error");
                    Environment.Exit(0);
                    Environment.FailFast("exit");
                }
                Win32Hooks.HookManager.Log("-----------Hooks initialized-----------");
            }
            catch (Exception)
            {
                Utility.Logging.Log("", "", Utility.Logging.Debug);
            }
        }

        void DisposeHooks()
        {
            if (hookManager != null)
            {
                hookManager.Dispose();
                hookManager = null;
                Win32Hooks.HookManager.Log("-----------Hooks disposed-----------");
            }
        }

        private bool doOnceStartQuestorManagerThread = true;

        void StartQuestorThread(int param)
        {

            bool ready = false;
            int i = 0;
            int currentPid = Process.GetCurrentProcess().Id;
            uint hWndpid = 0;

            while (!ready && doOnceStartQuestorManagerThread)
            {
                if (i >= 150) return;
                IntPtr hwndEVE = FindWindow(null, "EVE");
                GetWindowThreadProcessId(hwndEVE, out hWndpid);
                if (currentPid == hWndpid)
                    ready = true;
                Application.DoEvents();
                Thread.Sleep(200);
                i++;
            }

            doOnceStartQuestorManagerThread = false;

            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            switch (param)
            {
                case 0:
                    QuestorManagerDomain.ExecuteAssembly(assemblyFolder + "\\Questor\\Questor.exe", args: new String[] { "-i", "-c", HookManagerParamaters[1], "-u", HookManagerParamaters[0], "-p", HookManagerParamaters[2] });
                    break;
                case 1:
                    QuestorManagerDomain.ExecuteAssembly(assemblyFolder + "\\Questor\\QuestorManager.exe", args: new String[] { "-i" });
                    break;
                case 2:
                    QuestorManagerDomain.ExecuteAssembly(assemblyFolder + "\\Questor\\ValueDump.exe", args: new String[] { "-i" });
                    break;
                default:
                    break;
            }

        }

        internal void StartQuestor()
        {
            if (QuestorThread == null)
            {
                DisposeHooks();
                _StartQuestor();
                InitHooks();
            }
        }

        void _StartQuestor()
        {
            if (QuestorThread == null)
            {
                QuestorManagerDomain = AppDomain.CreateDomain("QuestorDomain");
                QuestorThread = new Thread(delegate() { StartQuestorThread(0); });
                QuestorThread.Start();
            }
        }

        internal void StartQuestorManager()
        {
            if (QuestorThread == null)
            {
                DisposeHooks();
                QuestorManagerDomain = AppDomain.CreateDomain("QuestorDomain");
                QuestorThread = new Thread(delegate() { StartQuestorThread(1); });
                QuestorThread.Start();
                InitHooks();
            }
        }

        internal void StartValueDump()
        {
            if (QuestorThread == null)
            {
                DisposeHooks();
                QuestorManagerDomain = AppDomain.CreateDomain("QuestorDomain");
                QuestorThread = new Thread(delegate() { StartQuestorThread(2); });
                QuestorThread.Start();
                InitHooks();
            }
        }

        internal void UnloadQuestorAppDomain()
        {
            if (QuestorThread != null)
            {
                while (QuestorThread.IsAlive)
                {
                    Thread.Sleep(2);
                    QuestorThread.Abort();
                }
                DisposeHooks();
                AppDomain.Unload(QuestorManagerDomain);
                QuestorManagerDomain = null;
                QuestorThread = null;
            }
        }

        private static bool doOnceTimerAppDomainMemoryTick = true;

        public static void TimerAppDomainMemoryTick(object sender, EventArgs e)
        {
            if (doOnceTimerAppDomainMemoryTick)
            {
                uint min = 104857600;
                uint max = 838860800;
                AddLog("SetProcessWorkingSetSize()");
                SetProcessWorkingSetSize(GetCurrentProcess(), min, max);
                doOnceTimerAppDomainMemoryTick = false;
            }

            if (QuestorManagerDomain != null)
            {
                _QuestorLauncherUI.labelTotalAllocated.Text = Math.Round(QuestorManagerDomain.MonitoringTotalAllocatedMemorySize / 1048576D, 2).ToString() + " mb";
                _QuestorLauncherUI.labelSurvived.Text = Math.Round(QuestorManagerDomain.MonitoringSurvivedMemorySize / 1048576D, 2).ToString() + " mb";
            }
        }

        public static void MainFormShown(object sender, EventArgs e)
        {
            if (bool.Parse(HookManagerParamaters[4]))
            {
                _QuestorLauncherUI.Hide();
            }
        }
    }
}

namespace wildert
{
    public class TimeOfTick : EventArgs
    {
        private DateTime TimeNow;
        public DateTime Time
        {
            set
            {
                TimeNow = value;
            }
            get
            {
                return this.TimeNow;
            }
        }
    }

    public class Metronome
    {
        public event TickHandler Tick;
        public delegate void TickHandler(Metronome m, TimeOfTick e);
        public void Start()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(3000);
                if (Tick != null)
                {
                    TimeOfTick TOT = new TimeOfTick();
                    TOT.Time = DateTime.Now;
                    Tick(this, TOT);
                }
            }
        }
    }

    public class Listener
    {
        public void Subscribe(Metronome m)
        {
            m.Tick += new Metronome.TickHandler(HeardIt);
        }
        private void HeardIt(Metronome m, TimeOfTick e)
        {
            System.Console.WriteLine("HEARD IT AT {0}", e.Time);
        }
    }

    //class Test
    //{
    //    static void Main()
    //    {
    //        Metronome m = new Metronome();
    //        Listener l = new Listener();
    //        l.Subscribe(m);
    //        m.Start();
    //    }
    //}
}