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
using Utility;

namespace QuestorSessionManager
{
	
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
		    try
		    {
		        //
		        // The InitializeComponent() call is required for Windows Forms designer support.
		        //
		        InitializeComponent();

		        Cache.OnMessage += ThreadSafeAddlog;
		    }
		    catch (Exception ex)
		    {
		        Log("[MainForm] Exception [" + ex + "]");
		    }
		}
		
		public void ThreadSafeAddlog(string str)
        {
		    try
		    {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => Log(str)));
                }
                else
                {
                    Log(str);
                }
		    }
            catch (Exception ex)
            {
                try
                {
                    Log("[ThreadSafeAddlog] Exception [" + ex + "]");
                }
                catch (Exception)
                {
                    Console.WriteLine("[ThreadSafeAddlog] Exception [" + ex + "]");
                }
            }
		}
		
		
		public void Log(string msg)
        {
		    try
		    {
                logbox.DataSource = null;
                if (logbox.Items.Count >= 1000)
                {
                    logbox.Items.Clear();
                }

                logbox.Items.Add(msg);
                logbox.SelectedIndex = logbox.Items.Count - 1;

                Logging.Log("QuestorSessionManager",msg, Logging.White);
		    }
            catch (Exception ex)
            {
                Console.WriteLine("[Log] Exception [" + ex + "]");
            }
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
		    try
		    {
		        if (Cache.Instance.EveAccountSerializeableSortableBindingList != null)
		        {
                    dataGridEveAccounts.DataSource = Cache.Instance.EveAccountSerializeableSortableBindingList.List;    
		        }

                if (Cache.Instance.EveSettings != null)
                {
                    if(Cache.Instance.EveSettings.EXEFileLocation != null && !string.IsNullOrEmpty(Cache.Instance.EveSettings.EXEFileLocation))
		            {
                        textBoxEveLocation.Text = Cache.Instance.EveSettings.EXEFileLocation;    
		            }

                    if (Cache.Instance.EveSettings.EveServerStatusThread != null)
                    {
                        checkBoxEveServerStatusThread.Checked = (bool)Cache.Instance.EveSettings.EveServerStatusThread;
                        
                        //
                        // todo: seems like this should be located elsewhere
                        //
                        if ((bool)Cache.Instance.EveSettings.EveServerStatusThread)
                        {
                            EveServerStatus.Instance.StartEveServerStatusThread();
                        }
                    }
                }
                
                //WCFServer.Instance.StartWCFServer();
		    }
            catch (Exception ex)
            {
                try
                {
                    Log("[MainFormLoad] Exception [" + ex + "]");
                }
                catch (Exception)
                {
                    Console.WriteLine("[MainFormLoad] Exception [" + ex + "]");
                }
            }
		}
		
		void MainFormFormClosed(object sender, FormClosedEventArgs e)
		{
			EveManager.Instance.Dispose();
			EveServerStatus.Instance.Dispose();
		}
		
		void EveLocationTextChanged(object sender, EventArgs e)
		{
		    if (textBoxEveLocation.Text != null && textBoxEveLocation.Text.ToLower().EndsWith("exefile.exe"))
		    {
		        if (Cache.Instance.EveSettings != null)
		        {
                    Cache.Instance.EveSettings.EXEFileLocation = textBoxEveLocation.Text;    
		        }
		    }
		}
		
		void DeleteToolStripMenuItem1Click(object sender, EventArgs e)
		{
		    try
		    {
                DataGridView dgv = this.ActiveControl as DataGridView;
                if (dgv == null) return;

                int selected = dgv.SelectedCells[0].OwningRow.Index;

                if (selected >= 0)
                {
                    Cache.Instance.EveAccountSerializeableSortableBindingList.List.RemoveAt(selected);
                    dataGridEveAccounts.DataSource = Cache.Instance.EveAccountSerializeableSortableBindingList.List;
                }
		    }
            catch (Exception ex)
            {
                Log("[DeleteToolStripMenuItem1Click] Exception [" + ex + "]");
            }
		}
		
		void StartInjectToolStripMenuItemClick(object sender, EventArgs e)
		{
		    try
		    {
                DataGridView dgv = this.ActiveControl as DataGridView;
                if (dgv == null) return;
                int index = (dgv.SelectedCells[0].OwningRow.Index);
                EveAccount eA = Cache.Instance.EveAccountSerializeableSortableBindingList.List[index];
                Logging.Log("MainForm", "StartInjectToolStripMenuItemClick", Logging.White);
                eA.StartEveInject();
                return;
		    }
            catch (Exception ex)
            {
                Log("[StartInjectToolStripMenuItemClick] Exception [" + ex + "]");
            }
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
		
		void MainFormResize(object sender, EventArgs e)
		{
			if ( WindowState == FormWindowState.Minimized )
			{
				this.Visible = false;
				this.notifyIconQL.Visible = true;
			}
		}
		
		void NotifyIconQLMouseDoubleClick(object sender, MouseEventArgs e)
		{
			
			((NotifyIcon)sender).Visible = !((NotifyIcon)sender).Visible;
			this.Visible = !this.Visible;
			WindowState = FormWindowState.Normal;	
		}
		
		void CheckBoxEveServerStatusThreadCheckedChanged(object sender, EventArgs e)
		{
			Cache.Instance.EveSettings.EveServerStatusThread = ((CheckBox)sender).Checked;
			
			if(((CheckBox)sender).Checked) 
            {
				EveServerStatus.Instance.StartEveServerStatusThread();
			} 
            else 
            {
				EveServerStatus.Instance.Dispose();
			}
		}
		
		void EditAdapteveHWProfileToolStripMenuItemClick(object sender, EventArgs e)
		{
			DataGridView dgv = this.ActiveControl as DataGridView;
			if ( dgv == null) return;
			int index = (dgv.SelectedCells[0].OwningRow.Index);
			EveAccount eA = Cache.Instance.EveAccountSerializeableSortableBindingList.List[index];
			
			var hwPf = new HWProfileForm(eA);
   			hwPf.Show();
		}
	}
}