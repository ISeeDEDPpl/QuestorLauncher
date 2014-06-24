/*
 * ---------------------------------------
 * User: duketwo
 * Date: 29.12.2013
 * Time: 21:20
 * 
 * ---------------------------------------
 */

using Injector;

namespace QuestorLauncher
{
    partial class QuestorLauncherUI
	{
		private System.ComponentModel.IContainer components = null;
		
		//protected override void Dispose(bool disposing)
		//{
		//	if (disposing) {
		//		if (components != null) {
		//			components.Dispose();
		//		}
		//	}
		//	base.Dispose(disposing);
		//}
		
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.logbox = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.labelTotalAllocated = new System.Windows.Forms.Label();
			this.timerAppDomainMemory = new System.Windows.Forms.Timer(this.components);
			this.label2 = new System.Windows.Forms.Label();
			this.labelSurvived = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			//this.SuspendLayout();
			// 
			// logbox
			// 
			this.logbox.FormattingEnabled = true;
			this.logbox.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.logbox.Location = new System.Drawing.Point(12, 12);
			this.logbox.Name = "logbox";
			this.logbox.Size = new System.Drawing.Size(1518, 446);
			this.logbox.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 476);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(160, 30);
			this.button1.TabIndex = 1;
			this.button1.Text = "Start Questor";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(510, 476);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(160, 30);
			this.button2.TabIndex = 2;
			this.button2.Text = "Unload AppDomain";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(1176, 485);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(140, 18);
			this.label1.TabIndex = 3;
			this.label1.Text = "total_allocated_memory:";
			// 
			// labelTotalAllocated
			// 
			this.labelTotalAllocated.Location = new System.Drawing.Point(1302, 485);
			this.labelTotalAllocated.Name = "labelTotalAllocated";
			this.labelTotalAllocated.Size = new System.Drawing.Size(82, 20);
			this.labelTotalAllocated.TabIndex = 4;
			this.labelTotalAllocated.Text = "0 mb";
			// 
			// timerAppDomainMemory
			// 
			this.timerAppDomainMemory.Enabled = true;
			this.timerAppDomainMemory.Interval = 1000;
			this.timerAppDomainMemory.Tick += new System.EventHandler(QuestorLauncher.TimerAppDomainMemoryTick);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(1390, 485);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(140, 18);
			this.label2.TabIndex = 5;
			this.label2.Text = "survived_memory:";
			// 
			// labelSurvived
			// 
			this.labelSurvived.Location = new System.Drawing.Point(1483, 485);
			this.labelSurvived.Name = "labelSurvived";
			this.labelSurvived.Size = new System.Drawing.Size(58, 21);
			this.labelSurvived.TabIndex = 6;
			this.labelSurvived.Text = "0 mb";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(178, 476);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(160, 30);
			this.button3.TabIndex = 7;
			this.button3.Text = "Start QuestorManager";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(344, 476);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(160, 30);
			this.button4.TabIndex = 8;
			this.button4.Text = "Start ValueDump";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(751, 476);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(160, 30);
			this.button5.TabIndex = 9;
			this.button5.Text = "Stealth Test";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.Button5Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1551, 524);
			this.ControlBox = false;
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.labelSurvived);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.labelTotalAllocated);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.logbox);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "HookManager";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.Shown += new System.EventHandler(QuestorLauncher.MainFormShown);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
	    public System.Windows.Forms.Label labelSurvived;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Timer timerAppDomainMemory;
	    public System.Windows.Forms.Label labelTotalAllocated;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
	    internal System.Windows.Forms.ListBox logbox;
	}
}
