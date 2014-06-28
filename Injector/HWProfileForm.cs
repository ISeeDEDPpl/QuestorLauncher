/*
 * ---------------------------------------
 * User: duketwo
 * Date: 21.06.2014
 * Time: 11:10
 * 
 * ---------------------------------------
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Utility;

namespace Injector
{
	/// <summary>
	/// Description of HWProfileForm.
	/// </summary>
	public partial class HWProfileForm : Form
	{
		
		

		
		
		private EveAccount EA;
		
		
		public HWProfileForm(EveAccount eA)
		{
			
			this.EA = eA;
			
			
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			Text = string.Format("HWProfile [{0}]",EA.CharacterName);
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			SaveSettings();
		}
		
		void HWProfileFormLoad(object sender, EventArgs e)
		{
			LoadSettings();
			
		}
		
		void LoadSettings(){
			
			
			if(EA.HWSettings != null) {
				this.textBoxTotalPhysRam.Text = EA.HWSettings.TotalPhysRam.ToString();
				this.textBoxWindowsUserLogin.Text = EA.HWSettings.WindowsUserLogin;
				this.textBoxComputername.Text = EA.HWSettings.Computername;
				this.textBoxWindowsKey.Text  = EA.HWSettings.WindowsKey;
				
				this.textBoxNetworkAdapterGuid.Text = EA.HWSettings.NetworkAdapterGuid.ToString();
				this.textBoxNetworkAddress.Text = EA.HWSettings.NetworkAddress.ToString();
				this.textBoxMacAddress.Text = EA.HWSettings.MacAddress.ToString();
				
				this.textBoxProcessorIdent.Text = EA.HWSettings.ProcessorIdent.ToString();
				this.textBoxProcessorRev.Text = EA.HWSettings.ProcessorRev.ToString();
				this.textBoxProcessorCoreAmount.Text = EA.HWSettings.ProcessorCoreAmount.ToString();
				this.textBoxProcessorLevel.Text = EA.HWSettings.ProcessorLevel.ToString();
				
				this.textBoxGpuDescription.Text = EA.HWSettings.GpuDescription.ToString();
				this.textBoxGpuDeviceId.Text = EA.HWSettings.GpuDeviceId.ToString();
				this.textBoxGpuVendorId.Text = EA.HWSettings.GpuVendorId.ToString();
				this.textBoxGpuRevision.Text = EA.HWSettings.GpuRevision.ToString();
				this.textBoxGpuDriverversion.Text = EA.HWSettings.GpuDriverversion.ToString();
				this.textBoxGpuIdentifier.Text = EA.HWSettings.GpuIdentifier.ToString();
				
				this.textBoxProxyIP.Text = EA.HWSettings.ProxyIP.ToString();
				this.textBoxProxyUsername.Text = EA.HWSettings.ProxyUsername.ToString();
				this.textBoxProxyPassword.Text = EA.HWSettings.ProxyPassword.ToString();
			}
			
		}
		
		
		void SaveSettings() {
			
			
			EA.HWSettings = new HWSettings(
				ulong.Parse(this.textBoxTotalPhysRam.Text),
				this.textBoxWindowsUserLogin.Text,
				this.textBoxComputername.Text,
				this.textBoxWindowsKey.Text,
				this.textBoxNetworkAdapterGuid.Text,
				this.textBoxNetworkAddress.Text,
				this.textBoxMacAddress.Text,
				this.textBoxProcessorIdent.Text,
				this.textBoxProcessorRev.Text,
				this.textBoxProcessorCoreAmount.Text,
				this.textBoxProcessorLevel.Text,
				this.textBoxGpuDescription.Text,
				uint.Parse(this.textBoxGpuDeviceId.Text),
				uint.Parse(this.textBoxGpuVendorId.Text),
				uint.Parse(this.textBoxGpuRevision.Text),
				long.Parse(this.textBoxGpuDriverversion.Text),
				this.textBoxGpuIdentifier.Text,
				this.textBoxProxyIP.Text,
				this.textBoxProxyUsername.Text,
				this.textBoxProxyPassword.Text);
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			LoadSettings();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			
			GenerateRandom();
			SaveSettings();
		}
		
		void GenerateRandom() {
			// generate random
			
			if(EA.HWSettings == null)
				EA.HWSettings = new HWSettings();
			
			string name =  UserPassGen.Instance.GenerateFirstname();
			
			this.textBoxTotalPhysRam.Text = EA.HWSettings.GenerateRamSize();
			this.textBoxWindowsUserLogin.Text = name;
			this.textBoxComputername.Text = name + "-PC";
			this.textBoxWindowsKey.Text  = EA.HWSettings.GenerateWindowsKey();
			
			this.textBoxNetworkAdapterGuid.Text = Guid.NewGuid().ToString();
			this.textBoxNetworkAddress.Text = EA.HWSettings.GenerateIpAddress();
			this.textBoxMacAddress.Text = EA.HWSettings.GenerateMacAddress();
			
			this.textBoxProcessorIdent.Text = "Intel64 Family 6 Model 26 Stepping 5, GenuineIntel";
			this.textBoxProcessorRev.Text = "6661";
			this.textBoxProcessorCoreAmount.Text = "8";
			this.textBoxProcessorLevel.Text = "6";
			
			this.textBoxGpuDescription.Text = "NVIDIA GeForce 8400 GS";
			this.textBoxGpuDeviceId.Text = "1028";
			this.textBoxGpuVendorId.Text = "4318";
			this.textBoxGpuRevision.Text = "6";
			this.textBoxGpuDriverversion.Text = "2533352100660607";
			this.textBoxGpuIdentifier.Text = "34f60eaf-dd66-4edb-982e-a89b9f0a6d02";
			
			this.textBoxProxyIP.Text = EA.HWSettings.ProxyIP == string.Empty ? "ip:port" : EA.HWSettings.ProxyIP;
			this.textBoxProxyUsername.Text = EA.HWSettings.ProxyUsername == string.Empty ? "proxyuser" : EA.HWSettings.ProxyUsername;
			this.textBoxProxyPassword.Text = EA.HWSettings.ProxyPassword == string.Empty ? "proxypass" : EA.HWSettings.ProxyPassword;
		}
		
	}
}
