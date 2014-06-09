/*
 * Created by SharpDevelop.
 * User: dserver
 * Date: 02.12.2013
 * Time: 09:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Injector
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.logLabel = new System.Windows.Forms.Label();
			this.logbox = new System.Windows.Forms.ListBox();
			this.exeFileLocationLabel = new System.Windows.Forms.Label();
			this.textBoxEveLocation = new System.Windows.Forms.TextBox();
			this.dataGridEveAccounts = new System.Windows.Forms.DataGridView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.startInjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonGenNewBeginEnd = new System.Windows.Forms.Button();
			this.buttonKillAllEveInstances = new System.Windows.Forms.Button();
			this.buttonStopEveManger = new System.Windows.Forms.Button();
			this.buttonStartEveManger = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonCompileCopy = new System.Windows.Forms.Button();
			this.buttonQuestorDownloadExtract = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridEveAccounts)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// logLabel
			// 
			this.logLabel.Location = new System.Drawing.Point(24, 243);
			this.logLabel.Name = "logLabel";
			this.logLabel.Size = new System.Drawing.Size(450, 20);
			this.logLabel.TabIndex = 3;
			this.logLabel.Text = "log";
			// 
			// logbox
			// 
			this.logbox.FormattingEnabled = true;
			this.logbox.Location = new System.Drawing.Point(24, 257);
			this.logbox.Name = "logbox";
			this.logbox.Size = new System.Drawing.Size(814, 329);
			this.logbox.TabIndex = 4;
			// 
			// exeFileLocationLabel
			// 
			this.exeFileLocationLabel.Location = new System.Drawing.Point(14, 26);
			this.exeFileLocationLabel.Name = "exeFileLocationLabel";
			this.exeFileLocationLabel.Size = new System.Drawing.Size(152, 16);
			this.exeFileLocationLabel.TabIndex = 6;
			this.exeFileLocationLabel.Text = "Eve Location:";
			// 
			// textBoxEveLocation
			// 
			this.textBoxEveLocation.Location = new System.Drawing.Point(14, 45);
			this.textBoxEveLocation.Name = "textBoxEveLocation";
			this.textBoxEveLocation.Size = new System.Drawing.Size(296, 20);
			this.textBoxEveLocation.TabIndex = 7;
			this.textBoxEveLocation.TextChanged += new System.EventHandler(this.EveLocationTextChanged);
			// 
			// dataGridEveAccounts
			// 
			this.dataGridEveAccounts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
			this.dataGridEveAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridEveAccounts.ContextMenuStrip = this.contextMenuStrip1;
			this.dataGridEveAccounts.Location = new System.Drawing.Point(24, 12);
			this.dataGridEveAccounts.MultiSelect = false;
			this.dataGridEveAccounts.Name = "dataGridEveAccounts";
			this.dataGridEveAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dataGridEveAccounts.Size = new System.Drawing.Size(1147, 228);
			this.dataGridEveAccounts.TabIndex = 18;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.deleteToolStripMenuItem,
			this.startInjectToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(105, 48);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.deleteToolStripMenuItem1});
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
			this.deleteToolStripMenuItem.Text = "delete";
			// 
			// deleteToolStripMenuItem1
			// 
			this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
			this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(104, 22);
			this.deleteToolStripMenuItem1.Text = "delete";
			this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.DeleteToolStripMenuItem1Click);
			// 
			// startInjectToolStripMenuItem
			// 
			this.startInjectToolStripMenuItem.Name = "startInjectToolStripMenuItem";
			this.startInjectToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
			this.startInjectToolStripMenuItem.Text = "start";
			this.startInjectToolStripMenuItem.Click += new System.EventHandler(this.StartInjectToolStripMenuItemClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.buttonGenNewBeginEnd);
			this.groupBox1.Controls.Add(this.buttonKillAllEveInstances);
			this.groupBox1.Controls.Add(this.buttonStopEveManger);
			this.groupBox1.Controls.Add(this.buttonStartEveManger);
			this.groupBox1.Controls.Add(this.textBoxEveLocation);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.exeFileLocationLabel);
			this.groupBox1.Location = new System.Drawing.Point(853, 257);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(318, 229);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Settings";
			this.groupBox1.Enter += new System.EventHandler(this.GroupBox1Enter);
			// 
			// buttonGenNewBeginEnd
			// 
			this.buttonGenNewBeginEnd.Location = new System.Drawing.Point(14, 138);
			this.buttonGenNewBeginEnd.Name = "buttonGenNewBeginEnd";
			this.buttonGenNewBeginEnd.Size = new System.Drawing.Size(178, 24);
			this.buttonGenNewBeginEnd.TabIndex = 27;
			this.buttonGenNewBeginEnd.Text = "Generate new begin/end times";
			this.buttonGenNewBeginEnd.UseVisualStyleBackColor = true;
			this.buttonGenNewBeginEnd.Click += new System.EventHandler(this.ButtonGenNewBeginEndClick);
			// 
			// buttonKillAllEveInstances
			// 
			this.buttonKillAllEveInstances.Location = new System.Drawing.Point(14, 108);
			this.buttonKillAllEveInstances.Name = "buttonKillAllEveInstances";
			this.buttonKillAllEveInstances.Size = new System.Drawing.Size(178, 24);
			this.buttonKillAllEveInstances.TabIndex = 26;
			this.buttonKillAllEveInstances.Text = "Kill all eve instances";
			this.buttonKillAllEveInstances.UseVisualStyleBackColor = true;
			this.buttonKillAllEveInstances.Click += new System.EventHandler(this.ButtonKillAllEveInstancesClick);
			// 
			// buttonStopEveManger
			// 
			this.buttonStopEveManger.Location = new System.Drawing.Point(106, 84);
			this.buttonStopEveManger.Name = "buttonStopEveManger";
			this.buttonStopEveManger.Size = new System.Drawing.Size(86, 21);
			this.buttonStopEveManger.TabIndex = 23;
			this.buttonStopEveManger.Text = "Stop";
			this.buttonStopEveManger.UseVisualStyleBackColor = true;
			this.buttonStopEveManger.Click += new System.EventHandler(this.ButtonStopEveMangerClick);
			// 
			// buttonStartEveManger
			// 
			this.buttonStartEveManger.Location = new System.Drawing.Point(14, 84);
			this.buttonStartEveManger.Name = "buttonStartEveManger";
			this.buttonStartEveManger.Size = new System.Drawing.Size(86, 21);
			this.buttonStartEveManger.TabIndex = 22;
			this.buttonStartEveManger.Text = "Start";
			this.buttonStartEveManger.UseVisualStyleBackColor = true;
			this.buttonStartEveManger.Click += new System.EventHandler(this.ButtonStartEveMangerClick);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(14, 68);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(204, 37);
			this.label2.TabIndex = 21;
			this.label2.Text = "start/stop EveManager thread";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.buttonCompileCopy);
			this.groupBox2.Controls.Add(this.buttonQuestorDownloadExtract);
			this.groupBox2.Location = new System.Drawing.Point(853, 492);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(318, 118);
			this.groupBox2.TabIndex = 20;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Questor";
			// 
			// buttonCompileCopy
			// 
			this.buttonCompileCopy.Location = new System.Drawing.Point(14, 46);
			this.buttonCompileCopy.Name = "buttonCompileCopy";
			this.buttonCompileCopy.Size = new System.Drawing.Size(178, 21);
			this.buttonCompileCopy.TabIndex = 1;
			this.buttonCompileCopy.Text = "Compile Questor + Copy";
			this.buttonCompileCopy.UseVisualStyleBackColor = true;
			this.buttonCompileCopy.Click += new System.EventHandler(this.ButtonCompileCopyClick);
			// 
			// buttonQuestorDownloadExtract
			// 
			this.buttonQuestorDownloadExtract.Location = new System.Drawing.Point(14, 19);
			this.buttonQuestorDownloadExtract.Name = "buttonQuestorDownloadExtract";
			this.buttonQuestorDownloadExtract.Size = new System.Drawing.Size(178, 21);
			this.buttonQuestorDownloadExtract.TabIndex = 0;
			this.buttonQuestorDownloadExtract.Text = "Download Questor + Extract";
			this.buttonQuestorDownloadExtract.UseVisualStyleBackColor = true;
			this.buttonQuestorDownloadExtract.Click += new System.EventHandler(this.ButtonQuestorDownloadExtractClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1181, 614);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.dataGridEveAccounts);
			this.Controls.Add(this.logbox);
			this.Controls.Add(this.logLabel);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "QuestorLauncher";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFormFormClosed);
			this.Load += new System.EventHandler(this.MainFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.dataGridEveAccounts)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		
		
		
		
		
		
		
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonStartEveManger;
		private System.Windows.Forms.Button buttonStopEveManger;
		private System.Windows.Forms.Button buttonGenNewBeginEnd;
		private System.Windows.Forms.Button buttonKillAllEveInstances;
		private System.Windows.Forms.Button buttonQuestorDownloadExtract;
		private System.Windows.Forms.Button buttonCompileCopy;
		private System.Windows.Forms.ToolStripMenuItem startInjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.DataGridView dataGridEveAccounts;
		private System.Windows.Forms.TextBox textBoxEveLocation;
		private System.Windows.Forms.Label exeFileLocationLabel;
		private System.Windows.Forms.ListBox logbox;
		private System.Windows.Forms.Label logLabel;
		
		void GroupBox1Enter(object sender, System.EventArgs e)
		{
			
		}
	}
}
