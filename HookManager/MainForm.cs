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

namespace QuestorLauncher
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class QuestorLauncherUI : Form
	{
        private static QuestorLauncher _QuestorLauncher;

        public QuestorLauncherUI()
		{
		    InitializeComponent();
            _QuestorLauncher = new QuestorLauncher {};
		}
	
		void MainFormLoad(object sender, EventArgs e)
		{
            
		}
		
		void Button1Click(object sender, EventArgs e)
        {
            _QuestorLauncher.UnloadQuestorAppDomain();
            _QuestorLauncher.StartQuestor();
		}
		
		void Button2Click(object sender, EventArgs e){
            _QuestorLauncher.UnloadQuestorAppDomain();
		}
		
		void Button3Click(object sender, System.EventArgs e)
		{
            _QuestorLauncher.UnloadQuestorAppDomain();
            _QuestorLauncher.StartQuestorManager();
		}
		void Button4Click(object sender, System.EventArgs e)
		{
            _QuestorLauncher.UnloadQuestorAppDomain();
            _QuestorLauncher.StartValueDump();
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			Win32Hooks.Stealthtest.Test();
		}	
	}
}
