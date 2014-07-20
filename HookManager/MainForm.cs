/*
 * ---------------------------------------
 * User: duketwo
 * Date: 29.12.2013
 * Time: 21:20
 * 
 * ---------------------------------------
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using EasyHook;
using System.IO;
using System.Runtime.InteropServices;
using Utility;

namespace HookManager
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm(string[] args)
		{
		    try
		    {
                InitializeComponent();
                Win32Hooks.HookManager.OnMessage += ThreadSafeAddlog;
                Win32Hooks.HookManager.Instance.InitHooks();
                RemoteHooking.WakeUpProcess();
                Win32Hooks.HookManager.Instance.WaitForRedGuard();
                Text = "HookManager [" + Win32Hooks.HookManager.Instance.CharName + "]";
                Win32Hooks.HookManager.Instance.WaitForEVE();
                Win32Hooks.HookManager.Instance.LaunchAppDomain(0);
		    }
		    catch (Exception ex)
		    {
                Logging.Log("HookManager", "Exception [" + ex + "]", Logging.Debug);
		    }
		}
		
		public void ThreadSafeAddlog(string str, Color? col)
        {
		    try
		    {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => AddLog(str, col)));

                }
                else
                {
                    AddLog(str);
                }
		    }
		    catch (Exception ex)
		    {
		        try
		        {
                    AddLog("[ThreadSafeAddlog] Exception [" + ex + "]");
		        }
		        catch (Exception)
		        {
                    Console.WriteLine("[ThreadSafeAddlog] Exception while throwing exception, lovely: [" + ex + "]");
		        }
		    }
		}
		
		void AddLog(string msg, Color? col = null)
        {	
			try 
            {	
				col = col==null ? Color.Black : col;
				msg = DateTime.UtcNow.ToString() + " " + msg;
				ListViewItem item = new ListViewItem();
				item.Text = msg;
				item.ForeColor = (Color)col;
				
				if (logbox.Items.Count >= 10000)
				{
					logbox.Items.Clear();
				}

				logbox.Items.Add(item);
				
				using (StreamWriter w = File.AppendText(Win32Hooks.HookManager.Instance.AssemblyPath + "\\" + Win32Hooks.HookManager.Instance.CharName + "-HookManager.log"))
				{
					w.WriteLine(msg);
				}

			    if (logbox.Items.Count > 1)
			    {
                    logbox.Items[logbox.Items.Count - 1].EnsureVisible();
			    }
			}
            catch (Exception ex)
            {
                try
                {
                    AddLog("[AddLog] Exception [" + ex + "]");
                }
                catch (Exception)
                {
                    Console.WriteLine("[AddLog] Exception while throwing exception, lovely: [" + ex + "]");
                }
            }
		}
		

		
		void Button1Click(object sender, EventArgs e)
        {
			// start questor
			Win32Hooks.HookManager.Instance.LaunchAppDomain(0);
		}
		
		void Button2Click(object sender, EventArgs e)
        {
			// unload appdomain
			Win32Hooks.HookManager.Instance.UnloadAppDomain();
			
		}
		
		void Button3Click(object sender, System.EventArgs e)
		{
			// start qm
			Win32Hooks.HookManager.Instance.LaunchAppDomain(1);
		}
		void Button4Click(object sender, System.EventArgs e)
		{
			// start valuedump
			Win32Hooks.HookManager.Instance.LaunchAppDomain(2);
		}
		
		
		[DllImport("kernel32.dll")]
		static extern IntPtr GetCurrentProcess();
		[DllImport("kernel32.dll")]
		static extern bool SetProcessWorkingSetSize(IntPtr hProcess, uint dwMinimumWorkingSetSize, uint dwMaximumWorkingSetSize);
		private bool doOnceTimerAppDomainMemoryTick = true;
		void TimerAppDomainMemoryTick(object sender, EventArgs e)
		{
			if (doOnceTimerAppDomainMemoryTick) 
            {
				uint min = 104857600;
				uint max = 838860800;
				AddLog("SetProcessWorkingSetSize()");
				SetProcessWorkingSetSize(GetCurrentProcess(), min, max);
				doOnceTimerAppDomainMemoryTick = false;
				
			}
			
			if (Win32Hooks.HookManager.Instance.QAppDomain != null) 
            {
				
				labelTotalAllocated.Text = Math.Round(Win32Hooks.HookManager.Instance.QAppDomain.MonitoringTotalAllocatedMemorySize/1048576D,2).ToString() + " mb";
				labelSurvived.Text = Math.Round(Win32Hooks.HookManager.Instance.QAppDomain.MonitoringSurvivedMemorySize/1048576D,2).ToString() + " mb";
			}
		}
		
		void MainFormShown(object sender, EventArgs e)
		{
			if(Win32Hooks.HookManager.Instance.HideHookManager)
				this.Hide();
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			EnvVars.PrintEnvVars();
			Win32Hooks.Stealthtest.Test();
		}
	}
}
