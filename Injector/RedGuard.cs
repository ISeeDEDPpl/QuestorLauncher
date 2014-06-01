/*
 * ---------------------------------------
 * User: duketwo
 * Date: 23.03.2014
 * Time: 18:09
 * 
 * ---------------------------------------
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using EasyHook;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using Library.Forms;
using System.Threading;
using Utility;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Injector
{
	/// <summary>
	/// Description of RedGuard.
	/// </summary>
	public class RedGuard
	{
		
		private static RedGuard _instance = new RedGuard();
		public static int RedGuardInstances = 0;
		
		[DllImport("user32.dll")]
		public static extern int FindWindow(string lpClassName,string lpWindowName);
		
		[DllImport("user32.dll", CharSet=CharSet.Unicode)]
		static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

		[DllImport("user32.dll")]
		public static extern int SendMessage(int hWnd, uint Msg, int wParam, StringBuilder lParam);
		
		[DllImport("user32.dll")]
		public static extern bool CloseWindow(int hWnd);
		
		
		
		private const int WM_LBUTTONDOWN = 0x0201;
		private const int WM_LBUTTONUP = 0x0202;
		private const int LB_GETCOUNT = 0x018B;
		private const int LB_GETTEXT = 0x0189;
		private const int LB_SETCURSEL = 0x186;
		
		public const int WM_SYSCOMMAND = 0x0112;
		public const int SC_CLOSE = 0xF060;
		
		public RedGuard()
		{
			Interlocked.Increment(ref RedGuardInstances);
		}
		
		
		public void AddNewAccount(string accountName, string password, string proxyIpPort, string proxyUsername, string proxyPassword){
			string redGuardConfigFile = Cache.Instance.EveSettings.RedGuardDirectory + "\\config.ini";
			if(File.Exists(redGuardConfigFile)){
				File.AppendAllText(redGuardConfigFile, Environment.NewLine);
				File.AppendAllText(redGuardConfigFile, "[" + accountName + "]" + Environment.NewLine);
				File.AppendAllText(redGuardConfigFile, "signature_key=" + accountName + password + Environment.NewLine);
				File.AppendAllText(redGuardConfigFile, "ComputerName=" + UserPassGen.Instance.GenerateFirstname()+ "-PC" + Environment.NewLine);
				File.AppendAllText(redGuardConfigFile, "UserLogin=" + accountName + Environment.NewLine);
				File.AppendAllText(redGuardConfigFile, "proxy=" + proxyIpPort + Environment.NewLine);
				File.AppendAllText(redGuardConfigFile, "proxy-username=" + proxyUsername + Environment.NewLine);
				File.AppendAllText(redGuardConfigFile, "proxy-password=" + proxyPassword + Environment.NewLine);
				File.AppendAllText(redGuardConfigFile, Environment.NewLine);
			} else {
				Cache.Instance.Log("[AddNewAccount] - Exception - RedGuardConfig file does not exist!");
			}
		}
		
		public bool DoRedguard(string accountName)
		{
			
				
				int hwndRedGuard=0;
				IntPtr hwndListBox=(IntPtr)0;
				IntPtr hwndOKButton=(IntPtr)0;
				
				int i=0;
				while(hwndRedGuard == 0)
				{
					if(i>=150) return false;
					hwndRedGuard=FindWindow(null,"Red Guard Account Selection");
					Application.DoEvents();
					Thread.Sleep(200);
					i++;
				}
				
				
				hwndListBox = FindWindowEx((IntPtr)hwndRedGuard,IntPtr.Zero,"ListBox","");
				hwndOKButton = FindWindowEx((IntPtr)hwndRedGuard,IntPtr.Zero,"Button","OK");
				
				int cnt = SendMessage((int)hwndListBox, LB_GETCOUNT, 0, null);
				
				bool found = false;
				for (i=0; i < cnt; i++)
				{
					StringBuilder sb = new StringBuilder(256);
					SendMessage((int)hwndListBox, LB_GETTEXT, (int)i, sb);
					if (sb.ToString().Contains(accountName))
					{
						found = true;
						SendMessage((int)hwndListBox, LB_SETCURSEL, (int)i, null);
					}
				}
				
				if(!found){
					CloseWindow(hwndRedGuard);
					return false;
				}
				
				
				SendMessage((int)hwndOKButton, WM_LBUTTONDOWN, 0, null);
				SendMessage((int)hwndOKButton, WM_LBUTTONUP, 0, null);
				SendMessage((int)hwndOKButton, WM_LBUTTONDOWN, 0, null);
				SendMessage((int)hwndOKButton, WM_LBUTTONUP, 0, null);
				
				return true;
				
			
		}
		
		public static RedGuard Instance
		{
			get { return _instance; }
		}
		
		~RedGuard(){
			Interlocked.Decrement(ref RedGuardInstances);
		}
	}
}
