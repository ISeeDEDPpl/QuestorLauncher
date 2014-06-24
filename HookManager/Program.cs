/*
 * ---------------------------------------
 * User: duketwo
 * Date: 29.12.2013
 * Time: 21:20
 * 
 * ---------------------------------------
 */
using System;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using QuestorLauncher;

namespace QuestorLauncher
{
    
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	public static class Program
	{
	    private static QuestorLauncher _Launcher;

	    /// <summary>
		/// Program entry point.
		/// </summary>
		/// 
		public static void Main(string[] args)
		{
		    try
		    {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                _Launcher = new QuestorLauncher();
		        QuestorLauncher.HookManagerParamaters = args;
		        Application.Run(new QuestorLauncherUI());
		    }
		    catch (Exception)
		    {
		        
		    }
		}
	}
}
