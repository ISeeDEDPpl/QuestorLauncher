/*
 * ---------------------------------------
 * User: duketwo
 * Date: 21.06.2014
 * Time: 11:10
 * 
 * ---------------------------------------
 */
namespace Injector
{
	partial class HWProfileForm
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
			this.button1 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxWindowsKey = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxComputername = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxWindowsUserLogin = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxTotalPhysRam = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textBoxMacAddress = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxNetworkAddress = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxNetworkAdapterGuid = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.textBoxProcessorLevel = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textBoxProcessorCoreAmount = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxProcessorRev = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxProcessorIdent = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.textBoxGpuIdentifier = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.textBoxGpuDriverversion = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.textBoxGpuRevision = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.textBoxGpuVendorId = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.textBoxGpuDeviceId = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.textBoxGpuDescription = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.textBoxProxyPassword = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.textBoxProxyUsername = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.textBoxProxyIP = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(300, 287);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(282, 21);
			this.button1.TabIndex = 0;
			this.button1.Text = "save settings";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(300, 338);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(282, 20);
			this.button3.TabIndex = 2;
			this.button3.Text = "generate random hardware profile";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBoxWindowsKey);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textBoxComputername);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textBoxWindowsUserLogin);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.textBoxTotalPhysRam);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(282, 119);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Windows settings";
			// 
			// textBoxWindowsKey
			// 
			this.textBoxWindowsKey.Location = new System.Drawing.Point(129, 91);
			this.textBoxWindowsKey.Name = "textBoxWindowsKey";
			this.textBoxWindowsKey.Size = new System.Drawing.Size(147, 20);
			this.textBoxWindowsKey.TabIndex = 7;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 95);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(99, 16);
			this.label4.TabIndex = 6;
			this.label4.Text = "WindowsKey  ";
			// 
			// textBoxComputername
			// 
			this.textBoxComputername.Location = new System.Drawing.Point(129, 65);
			this.textBoxComputername.Name = "textBoxComputername";
			this.textBoxComputername.Size = new System.Drawing.Size(147, 20);
			this.textBoxComputername.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 69);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(99, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "Computername ";
			// 
			// textBoxWindowsUserLogin
			// 
			this.textBoxWindowsUserLogin.Location = new System.Drawing.Point(129, 38);
			this.textBoxWindowsUserLogin.Name = "textBoxWindowsUserLogin";
			this.textBoxWindowsUserLogin.Size = new System.Drawing.Size(147, 20);
			this.textBoxWindowsUserLogin.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(99, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "WindowsUserLogin";
			// 
			// textBoxTotalPhysRam
			// 
			this.textBoxTotalPhysRam.Location = new System.Drawing.Point(129, 12);
			this.textBoxTotalPhysRam.Name = "textBoxTotalPhysRam";
			this.textBoxTotalPhysRam.Size = new System.Drawing.Size(147, 20);
			this.textBoxTotalPhysRam.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(99, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "TotalPhysRam ";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textBoxMacAddress);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.textBoxNetworkAddress);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.textBoxNetworkAdapterGuid);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Location = new System.Drawing.Point(300, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(282, 95);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Networkadapter settings";
			// 
			// textBoxMacAddress
			// 
			this.textBoxMacAddress.Location = new System.Drawing.Point(129, 65);
			this.textBoxMacAddress.Name = "textBoxMacAddress";
			this.textBoxMacAddress.Size = new System.Drawing.Size(147, 20);
			this.textBoxMacAddress.TabIndex = 5;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6, 69);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(117, 16);
			this.label6.TabIndex = 4;
			this.label6.Text = "MacAddress";
			// 
			// textBoxNetworkAddress
			// 
			this.textBoxNetworkAddress.Location = new System.Drawing.Point(129, 38);
			this.textBoxNetworkAddress.Name = "textBoxNetworkAddress";
			this.textBoxNetworkAddress.Size = new System.Drawing.Size(147, 20);
			this.textBoxNetworkAddress.TabIndex = 3;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(6, 42);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(117, 16);
			this.label7.TabIndex = 2;
			this.label7.Text = "NetworkAddress ";
			// 
			// textBoxNetworkAdapterGuid
			// 
			this.textBoxNetworkAdapterGuid.Location = new System.Drawing.Point(129, 12);
			this.textBoxNetworkAdapterGuid.Name = "textBoxNetworkAdapterGuid";
			this.textBoxNetworkAdapterGuid.Size = new System.Drawing.Size(147, 20);
			this.textBoxNetworkAdapterGuid.TabIndex = 1;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(6, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(117, 16);
			this.label8.TabIndex = 0;
			this.label8.Text = "NetworkAdapterGuid  ";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.textBoxProcessorLevel);
			this.groupBox3.Controls.Add(this.label11);
			this.groupBox3.Controls.Add(this.textBoxProcessorCoreAmount);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.textBoxProcessorRev);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.textBoxProcessorIdent);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Location = new System.Drawing.Point(12, 137);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(282, 119);
			this.groupBox3.TabIndex = 5;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "CPU settings";
			// 
			// textBoxProcessorLevel
			// 
			this.textBoxProcessorLevel.Location = new System.Drawing.Point(129, 91);
			this.textBoxProcessorLevel.Name = "textBoxProcessorLevel";
			this.textBoxProcessorLevel.Size = new System.Drawing.Size(147, 20);
			this.textBoxProcessorLevel.TabIndex = 7;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(6, 95);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(117, 16);
			this.label11.TabIndex = 6;
			this.label11.Text = "ProcessorLevel";
			// 
			// textBoxProcessorCoreAmount
			// 
			this.textBoxProcessorCoreAmount.Location = new System.Drawing.Point(129, 65);
			this.textBoxProcessorCoreAmount.Name = "textBoxProcessorCoreAmount";
			this.textBoxProcessorCoreAmount.Size = new System.Drawing.Size(147, 20);
			this.textBoxProcessorCoreAmount.TabIndex = 5;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 69);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(117, 16);
			this.label5.TabIndex = 4;
			this.label5.Text = "ProcessorCoreAmount";
			// 
			// textBoxProcessorRev
			// 
			this.textBoxProcessorRev.Location = new System.Drawing.Point(129, 38);
			this.textBoxProcessorRev.Name = "textBoxProcessorRev";
			this.textBoxProcessorRev.Size = new System.Drawing.Size(147, 20);
			this.textBoxProcessorRev.TabIndex = 3;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(6, 42);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(117, 16);
			this.label9.TabIndex = 2;
			this.label9.Text = "ProcessorRev";
			// 
			// textBoxProcessorIdent
			// 
			this.textBoxProcessorIdent.Location = new System.Drawing.Point(129, 12);
			this.textBoxProcessorIdent.Name = "textBoxProcessorIdent";
			this.textBoxProcessorIdent.Size = new System.Drawing.Size(147, 20);
			this.textBoxProcessorIdent.TabIndex = 1;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(6, 16);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(117, 16);
			this.label10.TabIndex = 0;
			this.label10.Text = "ProcessorIdent";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.textBoxGpuIdentifier);
			this.groupBox4.Controls.Add(this.label17);
			this.groupBox4.Controls.Add(this.textBoxGpuDriverversion);
			this.groupBox4.Controls.Add(this.label16);
			this.groupBox4.Controls.Add(this.textBoxGpuRevision);
			this.groupBox4.Controls.Add(this.label12);
			this.groupBox4.Controls.Add(this.textBoxGpuVendorId);
			this.groupBox4.Controls.Add(this.label13);
			this.groupBox4.Controls.Add(this.textBoxGpuDeviceId);
			this.groupBox4.Controls.Add(this.label14);
			this.groupBox4.Controls.Add(this.textBoxGpuDescription);
			this.groupBox4.Controls.Add(this.label15);
			this.groupBox4.Location = new System.Drawing.Point(300, 113);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(282, 168);
			this.groupBox4.TabIndex = 8;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "GPU settings";
			// 
			// textBoxGpuIdentifier
			// 
			this.textBoxGpuIdentifier.Location = new System.Drawing.Point(129, 139);
			this.textBoxGpuIdentifier.Name = "textBoxGpuIdentifier";
			this.textBoxGpuIdentifier.Size = new System.Drawing.Size(147, 20);
			this.textBoxGpuIdentifier.TabIndex = 11;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(6, 143);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(117, 16);
			this.label17.TabIndex = 10;
			this.label17.Text = "GpuIdentifier";
			// 
			// textBoxGpuDriverversion
			// 
			this.textBoxGpuDriverversion.Location = new System.Drawing.Point(129, 115);
			this.textBoxGpuDriverversion.Name = "textBoxGpuDriverversion";
			this.textBoxGpuDriverversion.Size = new System.Drawing.Size(147, 20);
			this.textBoxGpuDriverversion.TabIndex = 9;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(6, 119);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(117, 16);
			this.label16.TabIndex = 8;
			this.label16.Text = "GpuDriverversion";
			// 
			// textBoxGpuRevision
			// 
			this.textBoxGpuRevision.Location = new System.Drawing.Point(129, 91);
			this.textBoxGpuRevision.Name = "textBoxGpuRevision";
			this.textBoxGpuRevision.Size = new System.Drawing.Size(147, 20);
			this.textBoxGpuRevision.TabIndex = 7;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(6, 95);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(117, 16);
			this.label12.TabIndex = 6;
			this.label12.Text = "GpuRevision";
			// 
			// textBoxGpuVendorId
			// 
			this.textBoxGpuVendorId.Location = new System.Drawing.Point(129, 65);
			this.textBoxGpuVendorId.Name = "textBoxGpuVendorId";
			this.textBoxGpuVendorId.Size = new System.Drawing.Size(147, 20);
			this.textBoxGpuVendorId.TabIndex = 5;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(6, 69);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(117, 16);
			this.label13.TabIndex = 4;
			this.label13.Text = "GpuVendorId";
			// 
			// textBoxGpuDeviceId
			// 
			this.textBoxGpuDeviceId.Location = new System.Drawing.Point(129, 38);
			this.textBoxGpuDeviceId.Name = "textBoxGpuDeviceId";
			this.textBoxGpuDeviceId.Size = new System.Drawing.Size(147, 20);
			this.textBoxGpuDeviceId.TabIndex = 3;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(6, 42);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(117, 16);
			this.label14.TabIndex = 2;
			this.label14.Text = "GpuDeviceId";
			// 
			// textBoxGpuDescription
			// 
			this.textBoxGpuDescription.Location = new System.Drawing.Point(129, 12);
			this.textBoxGpuDescription.Name = "textBoxGpuDescription";
			this.textBoxGpuDescription.Size = new System.Drawing.Size(147, 20);
			this.textBoxGpuDescription.TabIndex = 1;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(6, 16);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(117, 16);
			this.label15.TabIndex = 0;
			this.label15.Text = "GpuDescription ";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.textBoxProxyPassword);
			this.groupBox5.Controls.Add(this.label18);
			this.groupBox5.Controls.Add(this.textBoxProxyUsername);
			this.groupBox5.Controls.Add(this.label19);
			this.groupBox5.Controls.Add(this.textBoxProxyIP);
			this.groupBox5.Controls.Add(this.label20);
			this.groupBox5.Location = new System.Drawing.Point(12, 262);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(282, 95);
			this.groupBox5.TabIndex = 6;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Proxy settings";
			// 
			// textBoxProxyPassword
			// 
			this.textBoxProxyPassword.Location = new System.Drawing.Point(129, 65);
			this.textBoxProxyPassword.Name = "textBoxProxyPassword";
			this.textBoxProxyPassword.Size = new System.Drawing.Size(147, 20);
			this.textBoxProxyPassword.TabIndex = 5;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(6, 69);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(117, 16);
			this.label18.TabIndex = 4;
			this.label18.Text = "ProxyPassword";
			// 
			// textBoxProxyUsername
			// 
			this.textBoxProxyUsername.Location = new System.Drawing.Point(129, 38);
			this.textBoxProxyUsername.Name = "textBoxProxyUsername";
			this.textBoxProxyUsername.Size = new System.Drawing.Size(147, 20);
			this.textBoxProxyUsername.TabIndex = 3;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(6, 42);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(117, 16);
			this.label19.TabIndex = 2;
			this.label19.Text = "ProxyUsername";
			// 
			// textBoxProxyIP
			// 
			this.textBoxProxyIP.Location = new System.Drawing.Point(129, 12);
			this.textBoxProxyIP.Name = "textBoxProxyIP";
			this.textBoxProxyIP.Size = new System.Drawing.Size(147, 20);
			this.textBoxProxyIP.TabIndex = 1;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(6, 16);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(117, 16);
			this.label20.TabIndex = 0;
			this.label20.Text = "ProxyIP";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(300, 314);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(282, 21);
			this.button4.TabIndex = 10;
			this.button4.Text = "load settings";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// HWProfileForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(593, 364);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "HWProfileForm";
			this.Text = "HWProfile [AccountName]";
			this.Load += new System.EventHandler(this.HWProfileFormLoad);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox textBoxProxyIP;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox textBoxProxyUsername;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox textBoxProxyPassword;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox textBoxGpuDescription;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textBoxGpuDeviceId;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textBoxGpuVendorId;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox textBoxGpuRevision;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox textBoxGpuDriverversion;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox textBoxGpuIdentifier;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textBoxProcessorIdent;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBoxProcessorRev;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxProcessorCoreAmount;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox textBoxProcessorLevel;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBoxNetworkAdapterGuid;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBoxNetworkAddress;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxMacAddress;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxTotalPhysRam;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxWindowsUserLogin;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxComputername;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxWindowsKey;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button1;
	}
}
