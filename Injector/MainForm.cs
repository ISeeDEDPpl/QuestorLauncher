/*
* Created by SharpDevelop.
* User: dserver
* Date: 02.12.2013
* Time: 09:09
* 
* To change this template use Tools | Options | Coding | Edit Standard Headers.
*/
using System;
using System.Windows.Forms;
using System.IO;


namespace Injector
{
	
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			dataGridEveAccounts.DataSource = Cache.Instance.EveAccountSerializeableSortableBindingList.List;
			textBoxEveLocation.Text = Cache.Instance.EveSettings.EveDirectory;
			EveServerStatus.Instance.StartEveServerStatusThread();
		}
		
		void MainFormFormClosed(object sender, FormClosedEventArgs e)
		{
			EveManager.Instance.Dispose();
			EveServerStatus.Instance.Dispose();	
		}
		
		void EveLocationTextChanged(object sender, EventArgs e)
		{
			Cache.Instance.EveSettings.EveDirectory = textBoxEveLocation.Text;
		}
		
		void DeleteToolStripMenuItem1Click(object sender, EventArgs e)
		{
			DataGridView dgv = this.ActiveControl as DataGridView;
			if ( dgv == null) return;
			int selected = dgv.SelectedCells[0].OwningRow.Index;
			
			if(selected >= 0)
            {
				Cache.Instance.EveAccountSerializeableSortableBindingList.List.RemoveAt(selected);
				dataGridEveAccounts.DataSource = Cache.Instance.EveAccountSerializeableSortableBindingList.List;
			}
		}
		
		void StartInjectToolStripMenuItemClick(object sender, EventArgs e)
		{
			DataGridView dgv = this.ActiveControl as DataGridView;
			if ( dgv == null) return;
			int index = (dgv.SelectedCells[0].OwningRow.Index);
			EveAccount eA = Cache.Instance.EveAccountSerializeableSortableBindingList.List[index];
			eA.StartEveInject();
			return;
		}
		
		void ButtonStartEveMangerClick(object sender, EventArgs e)
		{
			EveManager.Instance.StartEveMangerThread();
		}
		
		void ButtonStopEveMangerClick(object sender, EventArgs e)
		{
			EveManager.Instance.Dispose();
		}
		
		void ButtonKillAllEveInstancesClick(object sender, EventArgs e)
		{
			EveManager.Instance.KillAllEveInstances();
		}
		
		void ButtonGenNewClick(object sender, System.EventArgs e)
		{
			foreach(EveAccount eA in Cache.Instance.EveAccountSerializeableSortableBindingList.List)
            {
				eA.GenerateNewBeginEnd();
			}
		}

		void ButtonGenNewBeginEndClick(object sender, System.EventArgs e)
		{
			foreach(EveAccount eA in Cache.Instance.EveAccountSerializeableSortableBindingList.List)
            {
				eA.GenerateNewBeginEnd();
			}
		}

		void ButtonQuestorDownloadExtractClick(object sender, System.EventArgs e)
		{
			Questor.Instance.DownloadQuestor();
			Questor.Instance.ExtractQuestor();
		}

		void ButtonCompileCopyClick(object sender, System.EventArgs e)
		{
			Questor.Instance.CompileQuestor();
			Questor.Instance.CopyQuestorBinary();
		}
	}
}