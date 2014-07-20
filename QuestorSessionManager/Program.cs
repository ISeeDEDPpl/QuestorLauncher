/*
 * Created by SharpDevelop.
 * User: dserver
 * Date: 02.12.2013
 * Time: 09:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Windows.Forms;
using Utility;

namespace QuestorSessionManager
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		private static void Main(string[] args)
		{
            Logging.Log("QuestorSessionManager","Starting UI",Logging.White);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
